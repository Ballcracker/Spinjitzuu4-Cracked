using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using spinjitzuu4.Scripts;

namespace spinjitzuu4.Orbwalk
{
	// Token: 0x0200002C RID: 44
	internal class NewOrbwalker
	{
		// Token: 0x0600011D RID: 285
		[DllImport("User32.dll", SetLastError = true)]
		private static extern bool BlockInput(bool fBlockIt);

		// Token: 0x0600011E RID: 286
		[DllImport("User32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x0600011F RID: 287 RVA: 0x00002F24 File Offset: 0x00001124
		public static double GetSecondsPerAttack()
		{
			return 1.0 / NewOrbwalker.ClientAttackSpeed;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00002F35 File Offset: 0x00001135
		public static double GetWindupDuration()
		{
			return (NewOrbwalker.GetSecondsPerAttack() * NewOrbwalker.ChampionAttackDelayPercent - NewOrbwalker.ChampionAttackCastTime) * NewOrbwalker.ChampionAttackDelayScaling + NewOrbwalker.ChampionAttackCastTime;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00002F54 File Offset: 0x00001154
		public static double GetBufferedWindupDuration()
		{
			return NewOrbwalker.GetWindupDuration() + NewOrbwalker.WindupBuffer;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00016E8C File Offset: 0x0001508C
		public static void OrbwalkInit()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			NewOrbwalker.OrbWalkTimer.Elapsed += NewOrbwalker.OrbWalkTimer_Elapsed;
			NewOrbwalker.ExperimentalOrbWalkTimer.Elapsed += NewOrbwalker.ExperimentalOrbWalkTimer_Elapsed;
			NewOrbwalker.attackSpeedCacheTimer.Elapsed += NewOrbwalker.AttackSpeedCacheTimer_Elapsed;
			NewOrbwalker.attackSpeedCacheTimer.Start();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00016EF8 File Offset: 0x000150F8
		private static void ExperimentalOrbWalkTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (NewOrbwalker.GetForegroundWindow() == GUI.LeagueProcess.MainWindowHandle)
			{
				DateTime signalTime = e.SignalTime;
				Point point = Point.Empty;
				switch (GUI.scanningType)
				{
				case 0:
					point = PixelBot.GetEnemyPositionCenter((double)NewOrbwalker.width);
					break;
				case 1:
					point = Orbwalk.FindEnemyNew();
					break;
				case 2:
					point = PixelBot.GetEnemyPositionCenterDynamicScreen();
					break;
				case 3:
					point = PixelBot.GetEnemyPositionLegacy();
					break;
				}
				NewOrbwalker.EnemyChampionPosition = point;
				if (NewOrbwalker.experimentalNextAttack < signalTime && point != Point.Empty)
				{
					NewOrbwalker.BlockInput(true);
					NewOrbwalker.LastMovePoint = Cursor.Position;
					if (GUI.champDetection)
					{
						KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CustomAttackUnit, point);
					}
					if (!GUI.champDetection)
					{
						KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.AutoAttack, default(Point));
					}
					NewOrbwalker.experimentalNextInput = signalTime.AddSeconds(NewOrbwalker.MinInputDelay);
					Thread.Sleep(4);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, NewOrbwalker.LastMovePoint);
					DateTime now = DateTime.Now;
					NewOrbwalker.BlockInput(false);
					NewOrbwalker.experimentalNextMove = now.AddSeconds(NewOrbwalker.GetBufferedWindupDuration());
					NewOrbwalker.experimentalNextAttack = now.AddSeconds(NewOrbwalker.GetSecondsPerAttack());
					return;
				}
				if (NewOrbwalker.experimentalNextMove < signalTime)
				{
					if (GUI.scripts)
					{
						KogMaw.CastSpells(point, GUI.kogCastQ, GUI.kogCastE, GUI.kogCastR);
					}
					NewOrbwalker.experimentalNextInput = signalTime.AddSeconds(NewOrbwalker.MinInputDelay);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.RightClick, default(Point));
					NewOrbwalker.experimentalNextMove = signalTime.AddTicks((long)NewOrbwalker.rnd.Next(50, 70));
					return;
				}
				if (point == Point.Empty)
				{
					NewOrbwalker.LastMovePoint = Cursor.Position;
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0001709C File Offset: 0x0001529C
		private static void OrbWalkTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (NewOrbwalker.GetForegroundWindow() == GUI.LeagueProcess.MainWindowHandle)
			{
				DateTime signalTime = e.SignalTime;
				if (NewOrbwalker.nextAttack < signalTime)
				{
					Point point;
					if (GUI.drawRange)
					{
						point = Orbwalk.FindEnemyNew();
					}
					else if (GUI.customResMode)
					{
						point = PixelBot.GetEnemyPositionCenterDynamicScreen();
					}
					else
					{
						point = PixelBot.GetEnemyPositionCenter((double)NewOrbwalker.width);
					}
					NewOrbwalker.EnemyChampionPosition = point;
					NewOrbwalker.nextInput = signalTime.AddSeconds(NewOrbwalker.MinInputDelay);
					NewOrbwalker.LastMovePoint = Cursor.Position;
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CustomAttackUnit, point);
					Thread.Sleep(10);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, NewOrbwalker.LastMovePoint);
					DateTime now = DateTime.Now;
					NewOrbwalker.nextMove = now.AddSeconds(NewOrbwalker.GetBufferedWindupDuration());
					NewOrbwalker.nextAttack = now.AddSeconds(NewOrbwalker.GetSecondsPerAttack());
					return;
				}
				if (NewOrbwalker.nextMove < signalTime)
				{
					NewOrbwalker.nextInput = signalTime.AddSeconds(NewOrbwalker.MinInputDelay);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.RightClick, default(Point));
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00017194 File Offset: 0x00015394
		public static string getChampionName()
		{
			JToken jtoken = null;
			try
			{
				jtoken = JToken.Parse(NewOrbwalker.Client.DownloadString("https://127.0.0.1:2999/liveclientdata/activeplayer"));
			}
			catch
			{
			}
			if (string.IsNullOrEmpty(NewOrbwalker.ChampionName))
			{
				NewOrbwalker.ActivePlayerName = ((jtoken != null) ? jtoken["summonerName"].ToString() : null);
				NewOrbwalker.IsIntializingValues = true;
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)JToken.Parse(NewOrbwalker.Client.DownloadString("https://127.0.0.1:2999/liveclientdata/playerlist"))).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken2 = enumerator.Current;
						if (jtoken2["summonerName"].ToString().Equals(NewOrbwalker.ActivePlayerName))
						{
							NewOrbwalker.ChampionName = jtoken2["championName"].ToString();
							string[] array = jtoken2["rawChampionName"].ToString().Split(new char[]
							{
								'_',
								'\u0001'
							});
							NewOrbwalker.RawChampionName = array[array.Length - 1];
							return NewOrbwalker.RawChampionName;
						}
					}
					goto IL_F4;
				}
				string result;
				return result;
			}
			IL_F4:
			return NewOrbwalker.RawChampionName;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000172B8 File Offset: 0x000154B8
		private static void AttackSpeedCacheTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (GUI.HasProcess && !GUI.IsExiting && !NewOrbwalker.IsIntializingValues && !NewOrbwalker.IsUpdatingAttackValues)
			{
				NewOrbwalker.IsUpdatingAttackValues = true;
				JToken jtoken = null;
				try
				{
					jtoken = JToken.Parse(NewOrbwalker.Client.DownloadString("https://127.0.0.1:2999/liveclientdata/activeplayer"));
				}
				catch
				{
					NewOrbwalker.IsUpdatingAttackValues = false;
					return;
				}
				if (string.IsNullOrEmpty(NewOrbwalker.ChampionName))
				{
					NewOrbwalker.ActivePlayerName = ((jtoken != null) ? jtoken["summonerName"].ToString() : null);
					NewOrbwalker.IsIntializingValues = true;
					foreach (JToken jtoken2 in ((IEnumerable<JToken>)JToken.Parse(NewOrbwalker.Client.DownloadString("https://127.0.0.1:2999/liveclientdata/playerlist"))))
					{
						if (jtoken2["summonerName"].ToString().Equals(NewOrbwalker.ActivePlayerName))
						{
							NewOrbwalker.ChampionName = jtoken2["championName"].ToString();
							string[] array = jtoken2["rawChampionName"].ToString().Split(new char[]
							{
								'_',
								'\u0001'
							});
							NewOrbwalker.RawChampionName = array[array.Length - 1];
							GUI.Champion = NewOrbwalker.RawChampionName;
						}
					}
					if (!NewOrbwalker.GetChampionBaseValues(NewOrbwalker.RawChampionName))
					{
						NewOrbwalker.IsIntializingValues = false;
						NewOrbwalker.IsUpdatingAttackValues = false;
						return;
					}
					NewOrbwalker.IsIntializingValues = false;
				}
				NewOrbwalker.ClientAttackSpeed = jtoken["championStats"]["attackSpeed"].Value<double>();
				NewOrbwalker.IsUpdatingAttackValues = false;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00017454 File Offset: 0x00015654
		private static bool GetChampionBaseValues(string championName)
		{
			string text = championName.ToLower();
			JToken jtoken = null;
			bool result;
			try
			{
				jtoken = JToken.Parse(NewOrbwalker.Client.DownloadString(string.Concat(new string[]
				{
					"https://raw.communitydragon.org/latest/game/data/characters/",
					text,
					"/",
					text,
					".bin.json"
				})));
				goto IL_52;
			}
			catch
			{
				result = false;
			}
			return result;
			IL_52:
			JToken jtoken2 = jtoken["Characters/" + championName + "/CharacterRecords/Root"];
			NewOrbwalker.ChampionAttackSpeedRatio = jtoken2["attackSpeedRatio"].Value<double>();
			JToken jtoken3 = jtoken2["basicAttack"];
			JToken jtoken4 = jtoken3["mAttackDelayCastOffsetPercent"];
			JToken jtoken5 = jtoken3["mAttackDelayCastOffsetPercentAttackSpeedRatio"];
			if (jtoken5 != null && jtoken5.Value<double?>() != null)
			{
				NewOrbwalker.ChampionAttackDelayScaling = jtoken5.Value<double>();
			}
			if (jtoken4 == null || jtoken4.Value<double?>() == null)
			{
				JToken jtoken6 = jtoken3["mAttackTotalTime"];
				JToken jtoken7 = jtoken3["mAttackCastTime"];
				if ((jtoken6 == null || jtoken6.Value<double?>() == null) && (jtoken7 == null || jtoken7.Value<double?>() == null))
				{
					string text2 = jtoken3["mAttackName"].ToString();
					string key = "Characters/" + text2.Split(new string[]
					{
						"BasicAttack"
					}, StringSplitOptions.RemoveEmptyEntries)[0] + "/Spells/" + text2;
					NewOrbwalker.ChampionAttackDelayPercent += jtoken[key]["mSpell"]["delayCastOffsetPercent"].Value<double>();
				}
				else
				{
					NewOrbwalker.ChampionAttackTotalTime = jtoken6.Value<double>();
					NewOrbwalker.ChampionAttackCastTime = jtoken7.Value<double>();
					NewOrbwalker.ChampionAttackDelayPercent = NewOrbwalker.ChampionAttackCastTime / NewOrbwalker.ChampionAttackTotalTime;
				}
			}
			else
			{
				NewOrbwalker.ChampionAttackDelayPercent += jtoken4.Value<double>();
			}
			return true;
		}

		// Token: 0x040003A8 RID: 936
		private static Point LastMovePoint;

		// Token: 0x040003A9 RID: 937
		private static int width = Screen.PrimaryScreen.Bounds.Height;

		// Token: 0x040003AA RID: 938
		private static bool IsIntializingValues = false;

		// Token: 0x040003AB RID: 939
		private static bool IsUpdatingAttackValues = false;

		// Token: 0x040003AC RID: 940
		private static readonly WebClient Client = new WebClient();

		// Token: 0x040003AD RID: 941
		public static readonly System.Timers.Timer OrbWalkTimer = new System.Timers.Timer(33.333333333333336);

		// Token: 0x040003AE RID: 942
		public static readonly System.Timers.Timer ExperimentalOrbWalkTimer = new System.Timers.Timer(33.333333333333336);

		// Token: 0x040003AF RID: 943
		private static readonly Stopwatch owStopWatch = new Stopwatch();

		// Token: 0x040003B0 RID: 944
		public static string ActivePlayerName = string.Empty;

		// Token: 0x040003B1 RID: 945
		public static string ChampionName = string.Empty;

		// Token: 0x040003B2 RID: 946
		public static string RawChampionName = string.Empty;

		// Token: 0x040003B3 RID: 947
		public static double ClientAttackSpeed = 0.625;

		// Token: 0x040003B4 RID: 948
		public static double ChampionAttackCastTime = 0.625;

		// Token: 0x040003B5 RID: 949
		public static double ChampionAttackTotalTime = 0.625;

		// Token: 0x040003B6 RID: 950
		public static double ChampionAttackSpeedRatio = 0.625;

		// Token: 0x040003B7 RID: 951
		public static double ChampionAttackDelayPercent = 0.3;

		// Token: 0x040003B8 RID: 952
		public static double ChampionAttackDelayScaling = 1.0;

		// Token: 0x040003B9 RID: 953
		public static Point EnemyChampionPosition;

		// Token: 0x040003BA RID: 954
		public static readonly double WindupBuffer = 0.066666666666666666;

		// Token: 0x040003BB RID: 955
		public static readonly double MinInputDelay = 0.033333333333333333;

		// Token: 0x040003BC RID: 956
		private static DateTime nextInput = default(DateTime);

		// Token: 0x040003BD RID: 957
		private static DateTime nextMove = default(DateTime);

		// Token: 0x040003BE RID: 958
		private static DateTime nextAttack = default(DateTime);

		// Token: 0x040003BF RID: 959
		public static Random rnd = new Random();

		// Token: 0x040003C0 RID: 960
		public static readonly double OrderTickRate = 0.033333333333333333;

		// Token: 0x040003C1 RID: 961
		public static readonly System.Timers.Timer attackSpeedCacheTimer = new System.Timers.Timer(NewOrbwalker.OrderTickRate);

		// Token: 0x040003C2 RID: 962
		public static long LastAATick = 0L;

		// Token: 0x040003C3 RID: 963
		private static DateTime experimentalNextInput = default(DateTime);

		// Token: 0x040003C4 RID: 964
		private static DateTime experimentalNextMove = default(DateTime);

		// Token: 0x040003C5 RID: 965
		private static DateTime experimentalNextAttack = default(DateTime);
	}
}
