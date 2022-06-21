using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using spinjitzuu4.Scripts;

namespace spinjitzuu4.Orbwalk
{
	// Token: 0x0200002D RID: 45
	internal class Orbwalk
	{
		// Token: 0x0600012A RID: 298
		[DllImport("User32.dll", SetLastError = true)]
		private static extern bool BlockInput(bool fBlockIt);

		// Token: 0x0600012B RID: 299
		[DllImport("User32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x0600012C RID: 300
		[DllImport("User32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		// Token: 0x0600012D RID: 301
		[DllImport("User32.dll")]
		public static extern byte VkKeyScan(char ch);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00002F61 File Offset: 0x00001161
		public static int GameTimeTickCount
		{
			get
			{
				return (int)(float.Parse(API.readGameTime()) * 1000f);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000177D0 File Offset: 0x000159D0
		public static void Spinjitzuu()
		{
			if (Orbwalk.GetForegroundWindow() == GUI.LeagueProcess.MainWindowHandle && Orbwalk.LastAATick < Environment.TickCount)
			{
				Point point = Point.Empty;
				switch (GUI.scanningType)
				{
				case 0:
					point = PixelBot.GetEnemyPositionCenter((double)Orbwalk.width);
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
				if (Orbwalk.CanAttack() && point != Point.Empty)
				{
					Orbwalk.BlockInput(true);
					Orbwalk.LastMovePoint = Cursor.Position;
					if (GUI.champDetection)
					{
						KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CustomAttackUnit, point);
					}
					else
					{
						KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.AutoAttack, default(Point));
					}
					Orbwalk.LastAATick = Environment.TickCount;
					float windup = (float)NewOrbwalker.ChampionAttackDelayPercent;
					Orbwalk.LastMoveCommandT = Environment.TickCount + Orbwalk.GetAttackWindup(windup) + GUI.additionalWindup;
					Thread.Sleep(GUI.mouseSnap);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, Orbwalk.LastMovePoint);
					Orbwalk.BlockInput(false);
					return;
				}
				if (Orbwalk.CanMove((float)GUI.additionalWindup) && Orbwalk.LastMoveCommandT < Environment.TickCount)
				{
					if (GUI.scripts)
					{
						KogMaw.CastSpells(point, GUI.kogCastQ, GUI.kogCastE, GUI.kogCastR);
					}
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.RightClick, default(Point));
					Orbwalk.LastMovePoint = Cursor.Position;
					if (GUI.mouseFix)
					{
						Orbwalk.LastMoveCommandT = Environment.TickCount + Orbwalk.rnd.Next(60, 80);
						return;
					}
				}
				else if (point == Point.Empty)
				{
					Orbwalk.LastMovePoint = Cursor.Position;
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00017960 File Offset: 0x00015B60
		public static void ExperimentalSpinjitzuu()
		{
			if (Orbwalk.expNextInput < (double)Environment.TickCount && Orbwalk.expLastAATick < Environment.TickCount)
			{
				Point point = Point.Empty;
				switch (GUI.scanningType)
				{
				case 0:
					point = PixelBot.GetEnemyPositionCenter((double)Orbwalk.width);
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
				if (Orbwalk.expNextAttack < Environment.TickCount - GUI.averagePing / 2 && point != Point.Empty)
				{
					Orbwalk.BlockInput(true);
					Orbwalk.LastMovePoint = Cursor.Position;
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CustomAttackUnit, point);
					Orbwalk.expNextInput = (double)Environment.TickCount + Orbwalk.MinInputDelay;
					Orbwalk.expLastAATick = Environment.TickCount;
					Thread.Sleep(GUI.mouseSnap);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, Orbwalk.LastMovePoint);
					Orbwalk.BlockInput(false);
					Orbwalk.expNextMove = (double)Orbwalk.expLastAATick + NewOrbwalker.GetWindupDuration() * 1000.0 + (double)GUI.additionalWindup;
					Orbwalk.expNextAttack = Orbwalk.expLastAATick + Orbwalk.GetAttackDelayNew() + GUI.attackDelay;
					return;
				}
				if (Orbwalk.expNextMove >= (double)(Environment.TickCount - GUI.averagePing / 2) && !(GUI.Champion == "Kalista"))
				{
					if (point == Point.Empty)
					{
						Orbwalk.LastMovePoint = Cursor.Position;
					}
				}
				else
				{
					if (GUI.scripts)
					{
						KogMaw.CastSpells(point, GUI.kogCastQ, GUI.kogCastE, GUI.kogCastR);
					}
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.RightClick, default(Point));
					Orbwalk.LastMovePoint = Cursor.Position;
					if (GUI.mouseFix)
					{
						Orbwalk.expNextInput = (double)Environment.TickCount + Orbwalk.MinInputDelay;
						Orbwalk.expNextMove = (double)(Environment.TickCount + Orbwalk.rnd.Next(50, 70));
						return;
					}
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00017B34 File Offset: 0x00015D34
		public static void ExperimentalGameTimeSpinjitzuu()
		{
			if (Orbwalk.exp1NextInput < (double)Orbwalk.getGameTime() && Orbwalk.exp1LastAATick < (double)Orbwalk.getGameTime())
			{
				Point point = Point.Empty;
				switch (GUI.scanningType)
				{
				case 0:
					point = PixelBot.GetEnemyPositionCenter((double)Orbwalk.width);
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
				if (Orbwalk.exp1NextAttack + (double)GUI.attackDelay < (double)Orbwalk.getGameTime() && point != Point.Empty)
				{
					Orbwalk.BlockInput(true);
					Orbwalk.LastMovePoint = Cursor.Position;
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CustomAttackUnit, point);
					Orbwalk.exp1NextInput = (double)Orbwalk.getGameTime() + Orbwalk.MinInputDelay;
					Orbwalk.exp1LastAATick = (double)Orbwalk.getGameTime();
					float windup = (float)NewOrbwalker.ChampionAttackDelayPercent;
					Thread.Sleep(GUI.mouseSnap);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, Orbwalk.LastMovePoint);
					Orbwalk.BlockInput(false);
					Orbwalk.exp1NextMove = (double)(Orbwalk.getGameTime() + (float)Orbwalk.GetAttackWindup(windup) + (float)GUI.additionalWindup);
					Orbwalk.exp1NextAttack = (double)(Orbwalk.getGameTime() + (float)Orbwalk.GetAttackDelayNew());
					return;
				}
				if (Orbwalk.exp1NextMove < (double)Orbwalk.getGameTime())
				{
					if (GUI.scripts)
					{
						KogMaw.CastSpells(point, GUI.kogCastQ, GUI.kogCastE, GUI.kogCastR);
					}
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.RightClick, default(Point));
					Orbwalk.LastMovePoint = Cursor.Position;
					if (GUI.mouseFix)
					{
						Orbwalk.exp1NextInput = (double)Orbwalk.getGameTime() + Orbwalk.MinInputDelay;
						Orbwalk.exp1NextMove = (double)(Orbwalk.getGameTime() + (float)Orbwalk.rnd.Next(50, 70));
						return;
					}
				}
				else if (point == Point.Empty)
				{
					Orbwalk.LastMovePoint = Cursor.Position;
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00002F74 File Offset: 0x00001174
		public static float getGameTime()
		{
			return float.Parse(API.readGameTime());
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00017CE8 File Offset: 0x00015EE8
		public static Point FindEnemyNew()
		{
			Point point = default(Point);
			Point[] array = PixelBot.Search(new Rectangle(0, 0, 1920, 1080), Orbwalk.HY_HP_COLOR, 1);
			if (array.Length != 0)
			{
				point = new Point(array[0].X + 70, array[0].Y + 185);
			}
			Point result = default(Point);
			int num = Convert.ToInt32((double)float.Parse(API.readAttackRange()) / 1.5);
			Rectangle rect = new Rectangle(point.X - num, point.Y - num, num + num, num + num);
			ColorTranslator.FromHtml("#6B3A73");
			Point[] array2 = PixelBot.Search(rect, Orbwalk.RGB_ENEMY_COLOR, 0);
			Point[] array3 = PixelBot.Search(rect, Orbwalk.RGB_BUFFED_ENEMY_COLOR, 0);
			if (array2.Length != 0)
			{
				double[] array4 = new double[array2.Length];
				int num2 = 0;
				foreach (Point point2 in array2)
				{
					double num3 = Math.Sqrt((double)((Math.Abs(point2.X - point.X) ^ 2) + (Math.Abs(point2.Y - point.Y) ^ 2)));
					array4[num2] = num3;
					num2++;
				}
				double value = array4.Min();
				int num4 = Convert.ToInt32((double)Array.IndexOf<double>(array4, value));
				result = new Point(array2[num4].X, array2[num4].Y + 95);
				return result;
			}
			if (array3.Length != 0)
			{
				double[] array6 = new double[array3.Length];
				int num5 = 0;
				foreach (Point point3 in array3)
				{
					double num6 = Math.Sqrt((double)((Math.Abs(point3.X + Orbwalk.offsetX - (point.X + Orbwalk.offsetX)) ^ 2) + (Math.Abs(point3.Y + Orbwalk.offsetY - (point.Y + Orbwalk.offsetY)) ^ 2)));
					array6[num5] = num6;
					num5++;
				}
				double value2 = array6.Min();
				int num7 = Convert.ToInt32((double)Array.IndexOf<double>(array6, value2));
				result = new Point(array3[num7].X + 10, array3[num7].Y + 95);
				return result;
			}
			return result;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00017F44 File Offset: 0x00016144
		private static int GetAttackWindup(float windup)
		{
			float num = float.Parse(API.readAttackSpeed());
			return (int)(1f / num * 1000f * windup);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002F80 File Offset: 0x00001180
		internal float GetAttackDelay()
		{
			return (float)((int)(1000f / float.Parse(API.readAttackSpeed())));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002F94 File Offset: 0x00001194
		private static int GetAttackDelayNew()
		{
			return (int)(1000f / float.Parse(API.readAttackSpeed()));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00017F6C File Offset: 0x0001616C
		private static bool CanAttack()
		{
			if (GUI.rageMode && (double)float.Parse(API.readAttackSpeed()) > 2.5)
			{
				return Orbwalk.LastAATick + Orbwalk.GetAttackDelayNew() < Environment.TickCount;
			}
			return Orbwalk.LastAATick + Orbwalk.GetAttackDelayNew() + GUI.attackDelay < Environment.TickCount;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00002FA7 File Offset: 0x000011A7
		private static bool CanMove(float extraWindup)
		{
			return GUI.Champion == "Kalista" || (float)Orbwalk.LastMoveCommandT <= (float)Environment.TickCount + extraWindup;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00002FD0 File Offset: 0x000011D0
		internal static void ResetAutoAttackTimer()
		{
			Orbwalk.LastAATick = 0;
		}

		// Token: 0x040003C6 RID: 966
		private static int LastAATick = 0;

		// Token: 0x040003C7 RID: 967
		private static Point LastMovePoint;

		// Token: 0x040003C8 RID: 968
		private static int LastMoveCommandT;

		// Token: 0x040003C9 RID: 969
		private static int width = Screen.PrimaryScreen.Bounds.Height;

		// Token: 0x040003CA RID: 970
		private static readonly Color RGB_ENEMY_LEVEL_NUMBER_COLOR = Color.FromArgb(203, 98, 88);

		// Token: 0x040003CB RID: 971
		public static Color RGB_BUFFED_ENEMY_COLOR = ColorTranslator.FromHtml("#6B3A73");

		// Token: 0x040003CC RID: 972
		public static Color RGB_ENEMY_COLOR = ColorTranslator.FromHtml("#3A0400");

		// Token: 0x040003CD RID: 973
		public static Color HY_HP_COLOR = ColorTranslator.FromHtml("#312C00");

		// Token: 0x040003CE RID: 974
		public static int offsetX = 65;

		// Token: 0x040003CF RID: 975
		public static int offsetY = 95;

		// Token: 0x040003D0 RID: 976
		public static Random rnd = new Random();

		// Token: 0x040003D1 RID: 977
		public static int expLastAATick = 0;

		// Token: 0x040003D2 RID: 978
		public static int expNextAttack = 0;

		// Token: 0x040003D3 RID: 979
		public static double expNextMove = 0.0;

		// Token: 0x040003D4 RID: 980
		public static double expNextInput = 0.0;

		// Token: 0x040003D5 RID: 981
		public static readonly double MinInputDelay = 0.033333333333333333;

		// Token: 0x040003D6 RID: 982
		public static double exp1LastAATick = 0.0;

		// Token: 0x040003D7 RID: 983
		public static double exp1NextAttack = 0.0;

		// Token: 0x040003D8 RID: 984
		public static double exp1NextMove = 0.0;

		// Token: 0x040003D9 RID: 985
		public static double exp1NextInput = 0.0;
	}
}
