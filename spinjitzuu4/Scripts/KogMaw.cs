using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace spinjitzuu4.Scripts
{
	// Token: 0x0200002B RID: 43
	internal class KogMaw
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00002E47 File Offset: 0x00001047
		public static int GetLevel()
		{
			return API.GetActivePlayerData()["level"].ToObject<int>();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002E5E File Offset: 0x0000105E
		public static int GetQLevel()
		{
			return API.GetActivePlayerData()["Q"]["abilityLevel"].ToObject<int>();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002E80 File Offset: 0x00001080
		public static int GetWLevel()
		{
			return API.GetActivePlayerData()["W"]["abilityLevel"].ToObject<int>();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002EA2 File Offset: 0x000010A2
		public static int GetELevel()
		{
			return API.GetActivePlayerData()["E"]["abilityLevel"].ToObject<int>();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002EC4 File Offset: 0x000010C4
		public static int GetRLevel()
		{
			return API.GetActivePlayerData()["R"]["abilityLevel"].ToObject<int>();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002EE6 File Offset: 0x000010E6
		public static float GetAbilityHaste()
		{
			return API.GetActivePlayerData()["championStats"]["abilityHaste"].ToObject<float>();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000169C0 File Offset: 0x00014BC0
		public static double GetBaseCooldown(char skill)
		{
			if (skill <= 'Q')
			{
				if (skill == 'E')
				{
					return KogMaw.CooldownE;
				}
				if (skill == 'Q')
				{
					return KogMaw.CooldownQ;
				}
			}
			else if (skill != 'R')
			{
				if (skill == 'W')
				{
					return KogMaw.CooldownW;
				}
			}
			else
			{
				if (KogMaw.GetLevel() >= 6 & KogMaw.GetLevel() < 11)
				{
					return KogMaw.CooldownR[0];
				}
				if (KogMaw.GetLevel() >= 11 & KogMaw.GetLevel() < 16)
				{
					return KogMaw.CooldownR[1];
				}
				if (KogMaw.GetLevel() >= 16 & KogMaw.GetLevel() <= 18)
				{
					return KogMaw.CooldownR[2];
				}
				return 0.0;
			}
			return 0.0;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00016A70 File Offset: 0x00014C70
		public static double GetActualSpellCooldown(char skill)
		{
			if (skill <= 'Q')
			{
				if (skill == 'E')
				{
					return KogMaw.GetBaseCooldown('E') * (double)(100f / (100f + KogMaw.GetAbilityHaste()));
				}
				if (skill == 'Q')
				{
					return KogMaw.GetBaseCooldown('Q') * (double)(100f / (100f + KogMaw.GetAbilityHaste()));
				}
			}
			else
			{
				if (skill == 'R')
				{
					return KogMaw.GetBaseCooldown('R') * (double)(100f / (100f + KogMaw.GetAbilityHaste()));
				}
				if (skill == 'W')
				{
					return KogMaw.GetBaseCooldown('W') * (double)(100f / (100f + KogMaw.GetAbilityHaste()));
				}
			}
			return 0.0;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00016B10 File Offset: 0x00014D10
		public static void HandleInputs()
		{
			for (;;)
			{
				if (GUI.kogScripts)
				{
					if ((Keyboard.GetKeyStates(Key.Q) & KeyStates.Down) > KeyStates.None & KogMaw.isQReady)
					{
						KogMaw.isQReady = false;
						KogMaw.NextQUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('Q') * 1000.0;
					}
					if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > KeyStates.None & KogMaw.isWReady)
					{
						KogMaw.isWReady = false;
						KogMaw.NextWUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('W') * 1000.0;
					}
					if ((Keyboard.GetKeyStates(Key.E) & KeyStates.Down) > KeyStates.None & KogMaw.isEReady)
					{
						KogMaw.isEReady = false;
						KogMaw.NextEUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('E') * 1000.0;
					}
					if ((Keyboard.GetKeyStates(Key.R) & KeyStates.Down) > KeyStates.None & KogMaw.isRReady)
					{
						KogMaw.isRReady = false;
						KogMaw.NextRUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('R') * 1000.0;
					}
					if (KogMaw.NextQUse < (double)Environment.TickCount)
					{
						KogMaw.isQReady = true;
					}
					if (KogMaw.NextWUse < (double)Environment.TickCount)
					{
						KogMaw.isWReady = true;
					}
					if (KogMaw.NextEUse < (double)Environment.TickCount)
					{
						KogMaw.isEReady = true;
					}
					if (KogMaw.NextRUse < (double)Environment.TickCount)
					{
						KogMaw.isRReady = true;
					}
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002F08 File Offset: 0x00001108
		public static bool getQReady()
		{
			return KogMaw.isQReady;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002F0F File Offset: 0x0000110F
		public static bool getWReady()
		{
			return KogMaw.isWReady;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002F16 File Offset: 0x00001116
		public static bool getEReady()
		{
			return KogMaw.isEReady;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002F1D File Offset: 0x0000111D
		public static bool getRReady()
		{
			return KogMaw.isRReady;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00016C58 File Offset: 0x00014E58
		public static bool CheckMana()
		{
			float resourceMax = API.GetResourceMax();
			return API.GetResourceValue() / resourceMax * 100f >= (float)GUI.minMana;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00016C84 File Offset: 0x00014E84
		public static void CastSpells(Point enemy, bool q, bool e, bool r)
		{
			if (KogMaw.NextInput < (double)Environment.TickCount && GUI.scripts && KogMaw.CheckMana())
			{
				if ((KogMaw.isQReady && q) & GUI.kogScripts)
				{
					Point position = System.Windows.Forms.Cursor.Position;
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CastQ, enemy);
					KogMaw.isQReady = false;
					KogMaw.NextWUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('Q') * 1000.0;
					Thread.Sleep(GUI.mouseSnap);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position);
					KogMaw.NextInput = (double)(Environment.TickCount + 250);
				}
				if ((KogMaw.isEReady && e) & GUI.kogScripts)
				{
					Point position2 = System.Windows.Forms.Cursor.Position;
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CastE, enemy);
					KogMaw.isEReady = false;
					KogMaw.NextEUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('E') * 1000.0;
					Thread.Sleep(GUI.mouseSnap);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position2);
					KogMaw.NextInput = (double)(Environment.TickCount + 250);
				}
				if ((KogMaw.isRReady && r) & GUI.kogScripts)
				{
					Point position3 = System.Windows.Forms.Cursor.Position;
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CastR, enemy);
					KogMaw.isRReady = false;
					KogMaw.NextRUse = (double)Environment.TickCount + KogMaw.GetActualSpellCooldown('R') * 1000.0;
					Thread.Sleep(GUI.mouseSnap);
					KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position3);
					KogMaw.NextInput = (double)(Environment.TickCount + 250);
				}
			}
		}

		// Token: 0x0400039B RID: 923
		public static double CooldownQ = 8.0;

		// Token: 0x0400039C RID: 924
		public static double CooldownW = 17.0;

		// Token: 0x0400039D RID: 925
		public static double CooldownE = 12.0;

		// Token: 0x0400039E RID: 926
		public static double[] CooldownR = new double[]
		{
			2.0,
			1.5,
			1.0
		};

		// Token: 0x0400039F RID: 927
		public static bool isQReady = true;

		// Token: 0x040003A0 RID: 928
		public static bool isWReady = true;

		// Token: 0x040003A1 RID: 929
		public static bool isEReady = true;

		// Token: 0x040003A2 RID: 930
		public static bool isRReady = true;

		// Token: 0x040003A3 RID: 931
		public static double NextQUse = 0.0;

		// Token: 0x040003A4 RID: 932
		public static double NextWUse = 0.0;

		// Token: 0x040003A5 RID: 933
		public static double NextEUse = 0.0;

		// Token: 0x040003A6 RID: 934
		public static double NextRUse = 0.0;

		// Token: 0x040003A7 RID: 935
		public static double NextInput = 0.0;
	}
}
