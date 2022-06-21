using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using spinjitzuu4.Orbwalk;

namespace spinjitzuu4.Scripts
{
	// Token: 0x02000029 RID: 41
	internal class ChampionScripts
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0001689C File Offset: 0x00014A9C
		public static void CheckSpellState(char Spell, Point enemy)
		{
			Point position = System.Windows.Forms.Cursor.Position;
			if (Spell == 'Q' && (Keyboard.GetKeyStates(Key.Q) & KeyStates.Down) > KeyStates.None)
			{
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CastQ, enemy);
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position);
			}
			if (Spell == 'W' && (Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > KeyStates.None)
			{
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.CastW, enemy);
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position);
			}
			if (Spell == 'E' && (Keyboard.GetKeyStates(Key.E) & KeyStates.Down) > KeyStates.None)
			{
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, enemy);
				Thread.Sleep(23);
				KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_E);
				KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_E);
				Thread.Sleep(23);
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position);
				Orbwalk.ResetAutoAttackTimer();
			}
			if (Spell == 'R' && (Keyboard.GetKeyStates(Key.R) & KeyStates.Down) > KeyStates.None)
			{
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, enemy);
				Thread.Sleep(23);
				KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_R);
				KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_R);
				Thread.Sleep(23);
				KeyMouseHandler.IssueOrder(KeyMouseHandler.OrderEnum.MoveMouse, position);
				Orbwalk.ResetAutoAttackTimer();
			}
		}
	}
}
