using System;
using System.Drawing;
using System.Windows.Forms;

namespace spinjitzuu4
{
	// Token: 0x0200001D RID: 29
	internal class KeyMouseHandler
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public static bool IsGameOnDisplay()
		{
			return NativeImport.GetActiveWindowTitle() == "League of Legends (TM) Client";
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0001485C File Offset: 0x00012A5C
		public static void IssueOrder(KeyMouseHandler.OrderEnum Order, Point Vector2D = default(Point))
		{
			if (KeyMouseHandler.IsGameOnDisplay())
			{
				switch (Order)
				{
				case KeyMouseHandler.OrderEnum.MoveMouse:
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					return;
				case KeyMouseHandler.OrderEnum.RightClick:
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
					return;
				case KeyMouseHandler.OrderEnum.MoveTo:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					if (Vector2D == new Point(Cursor.Position.X, Cursor.Position.Y))
					{
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
					return;
				case KeyMouseHandler.OrderEnum.AttackUnit:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_A);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_A);
					return;
				case KeyMouseHandler.OrderEnum.CustomAutoAttack:
					KeyboardOut.SendKeyDown(KeyboardOut.attackmoveKey);
					KeyboardOut.SendKeyUp(KeyboardOut.attackmoveKey);
					return;
				case KeyMouseHandler.OrderEnum.CustomAttackUnit:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.attackmoveKey);
					KeyboardOut.SendKeyUp(KeyboardOut.attackmoveKey);
					return;
				case KeyMouseHandler.OrderEnum.AutoAttack:
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_A);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_A);
					return;
				case KeyMouseHandler.OrderEnum.Stop:
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_S);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_S);
					return;
				case KeyMouseHandler.OrderEnum.CastQ:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_Q);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_Q);
					return;
				case KeyMouseHandler.OrderEnum.CastW:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_W);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_W);
					return;
				case KeyMouseHandler.OrderEnum.CastE:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_E);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_E);
					return;
				case KeyMouseHandler.OrderEnum.CastR:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_R);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_R);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0001485C File Offset: 0x00012A5C
		public static void GPUIssueOrder(KeyMouseHandler.OrderEnum Order, Point Vector2D = default(Point))
		{
			if (KeyMouseHandler.IsGameOnDisplay())
			{
				switch (Order)
				{
				case KeyMouseHandler.OrderEnum.MoveMouse:
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					return;
				case KeyMouseHandler.OrderEnum.RightClick:
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
					return;
				case KeyMouseHandler.OrderEnum.MoveTo:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					if (Vector2D == new Point(Cursor.Position.X, Cursor.Position.Y))
					{
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
					Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
					return;
				case KeyMouseHandler.OrderEnum.AttackUnit:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_A);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_A);
					return;
				case KeyMouseHandler.OrderEnum.CustomAutoAttack:
					KeyboardOut.SendKeyDown(KeyboardOut.attackmoveKey);
					KeyboardOut.SendKeyUp(KeyboardOut.attackmoveKey);
					return;
				case KeyMouseHandler.OrderEnum.CustomAttackUnit:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.attackmoveKey);
					KeyboardOut.SendKeyUp(KeyboardOut.attackmoveKey);
					return;
				case KeyMouseHandler.OrderEnum.AutoAttack:
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_A);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_A);
					return;
				case KeyMouseHandler.OrderEnum.Stop:
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_S);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_S);
					return;
				case KeyMouseHandler.OrderEnum.CastQ:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_Q);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_Q);
					return;
				case KeyMouseHandler.OrderEnum.CastW:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_W);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_W);
					return;
				case KeyMouseHandler.OrderEnum.CastE:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_E);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_E);
					return;
				case KeyMouseHandler.OrderEnum.CastR:
					if (Vector2D.X == 0 && Vector2D.Y == 0)
					{
						Mouse.SetCursorPosition(Cursor.Position.X, Cursor.Position.Y);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
						Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
						return;
					}
					Mouse.SetCursorPosition(Vector2D.X, Vector2D.Y);
					KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_R);
					KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_R);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0200001E RID: 30
		public enum OrderEnum
		{
			// Token: 0x0400035C RID: 860
			MoveMouse,
			// Token: 0x0400035D RID: 861
			RightClick,
			// Token: 0x0400035E RID: 862
			MoveTo,
			// Token: 0x0400035F RID: 863
			AttackUnit,
			// Token: 0x04000360 RID: 864
			CustomAutoAttack,
			// Token: 0x04000361 RID: 865
			CustomAttackUnit,
			// Token: 0x04000362 RID: 866
			AutoAttack,
			// Token: 0x04000363 RID: 867
			Stop,
			// Token: 0x04000364 RID: 868
			CastQ,
			// Token: 0x04000365 RID: 869
			CastW,
			// Token: 0x04000366 RID: 870
			CastE,
			// Token: 0x04000367 RID: 871
			CastR,
			// Token: 0x04000368 RID: 872
			CastD,
			// Token: 0x04000369 RID: 873
			CastF
		}
	}
}
