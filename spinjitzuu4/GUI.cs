using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;
using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using DiscordRPC;
using spinjitzuu4.Orbwalk;
using spinjitzuu4.Properties;
using spinjitzuu4.Scripts;
using Utilities.BunifuPages.BunifuAnimatorNS;
using Utilities.BunifuSlider;

namespace spinjitzuu4
{
	// Token: 0x02000008 RID: 8
	public partial class GUI : Form
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002494 File Offset: 0x00000694
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000249C File Offset: 0x0000069C
		public DiscordRpcClient Client { get; private set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000024A5 File Offset: 0x000006A5
		public static string getVersion()
		{
			return GUI.version;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024AC File Offset: 0x000006AC
		private void Cleanup()
		{
			this.Client.Dispose();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024B9 File Offset: 0x000006B9
		private void Setup()
		{
			this.Client = new DiscordRpcClient("986709830347673691");
			this.Client.Initialize();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003850 File Offset: 0x00001A50
		private void SetRPC()
		{
			this.Client.SetPresence(new RichPresence
			{
				Details = "Spinning on lowelo",
				State = "Stomping randoms",
				Assets = new Assets
				{
					LargeImageKey = "logoblur1",
					LargeImageText = "spinjitzuu.lol"
				}
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000038A8 File Offset: 0x00001AA8
		private static void OptimizedChecker()
		{
			GUI.CheckLeagueProcess();
			for (;;)
			{
				if (GUI.inGame)
				{
					if ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) > KeyStates.None)
					{
						if (GUI.showRange)
						{
							if (KeyMouseHandler.IsGameOnDisplay())
							{
								KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_C);
								do
								{
									switch (GUI.orbwalkType)
									{
									case 0:
										Orbwalk.Spinjitzuu();
										break;
									case 1:
										NewOrbwalker.ExperimentalOrbWalkTimer.Start();
										break;
									case 2:
										Orbwalk.ExperimentalSpinjitzuu();
										break;
									case 3:
										Orbwalk.ExperimentalGameTimeSpinjitzuu();
										break;
									}
								}
								while ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) != KeyStates.None);
								KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_C);
								if (GUI.orbwalkType == 1)
								{
									NewOrbwalker.ExperimentalOrbWalkTimer.Stop();
								}
							}
						}
						else
						{
							do
							{
								switch (GUI.orbwalkType)
								{
								case 0:
									Orbwalk.Spinjitzuu();
									break;
								case 1:
									NewOrbwalker.ExperimentalOrbWalkTimer.Start();
									break;
								case 2:
									Orbwalk.ExperimentalSpinjitzuu();
									break;
								case 3:
									Orbwalk.ExperimentalGameTimeSpinjitzuu();
									break;
								}
							}
							while ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) != KeyStates.None);
							if (GUI.orbwalkType == 1)
							{
								NewOrbwalker.ExperimentalOrbWalkTimer.Stop();
							}
						}
					}
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024D8 File Offset: 0x000006D8
		public GUI()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000039D0 File Offset: 0x00001BD0
		public static void CheckLeagueProcess()
		{
			while (GUI.LeagueProcess == null)
			{
				GUI.LeagueProcess = Process.GetProcessesByName("League of Legends").FirstOrDefault<Process>();
				if (GUI.LeagueProcess != null && !GUI.LeagueProcess.HasExited)
				{
					GUI.HasProcess = true;
					GUI.inGame = true;
					PixelBot.LeagueRect = Helper.GetProcessWindowRect("League of Legends");
					GUI.LeagueProcess.EnableRaisingEvents = true;
					GUI.LeagueProcess.Exited += GUI.LeagueProcess_Exited;
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003A4C File Offset: 0x00001C4C
		public void CheckGameState()
		{
			GUI.CheckLeagueProcess();
			for (;;)
			{
				if (GUI.inGame)
				{
					if ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) > KeyStates.None)
					{
						if (GUI.testingOrbwalker)
						{
							NewOrbwalker.OrbWalkTimer.Start();
						}
						if (!GUI.testingOrbwalker)
						{
							if (GUI.showRange && KeyMouseHandler.IsGameOnDisplay())
							{
								KeyboardOut.SendKeyDown(KeyboardOut.ScanCodeShort.KEY_C);
								do
								{
									if (GUI.orbwalkType == 0)
									{
										Orbwalk.Spinjitzuu();
									}
									if (GUI.orbwalkType == 1)
									{
										NewOrbwalker.ExperimentalOrbWalkTimer.Start();
									}
									if (GUI.orbwalkType == 2)
									{
										Orbwalk.ExperimentalSpinjitzuu();
									}
									if (GUI.orbwalkType == 3)
									{
										Orbwalk.ExperimentalGameTimeSpinjitzuu();
									}
								}
								while ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) != KeyStates.None);
								KeyboardOut.SendKeyUp(KeyboardOut.ScanCodeShort.KEY_C);
								if (GUI.orbwalkType == 1)
								{
									NewOrbwalker.ExperimentalOrbWalkTimer.Stop();
								}
							}
							if (!GUI.showRange)
							{
								do
								{
									if (GUI.orbwalkType == 0)
									{
										Orbwalk.Spinjitzuu();
									}
									if (GUI.orbwalkType == 1)
									{
										NewOrbwalker.ExperimentalOrbWalkTimer.Start();
									}
									if (GUI.orbwalkType == 2)
									{
										Orbwalk.ExperimentalSpinjitzuu();
									}
									if (GUI.orbwalkType == 3)
									{
										Orbwalk.ExperimentalGameTimeSpinjitzuu();
									}
								}
								while ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) != KeyStates.None);
								NewOrbwalker.ExperimentalOrbWalkTimer.Stop();
							}
						}
					}
					if ((Keyboard.GetKeyStates(GUI.K_SpacegliderKey) & KeyStates.Down) == KeyStates.None && GUI.testingOrbwalker)
					{
						NewOrbwalker.OrbWalkTimer.Stop();
					}
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024E8 File Offset: 0x000006E8
		private static void LeagueProcess_Exited(object sender, EventArgs e)
		{
			GUI.HasProcess = false;
			GUI.inGame = false;
			GUI.LeagueProcess = null;
			NewOrbwalker.attackSpeedCacheTimer.Stop();
			NewOrbwalker.ChampionName = string.Empty;
			GUI.CheckLeagueProcess();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002515 File Offset: 0x00000715
		private void bunifuImageButton2_Click(object sender, EventArgs e)
		{
			base.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002527 File Offset: 0x00000727
		private void bunifuButton21_Click_1(object sender, EventArgs e)
		{
			this.bunifuPages.SetPage(this.dashboardPage);
			this.indicator.Top = ((Control)sender).Top;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002550 File Offset: 0x00000750
		private void bunifuButton22_Click(object sender, EventArgs e)
		{
			this.bunifuPages.SetPage(this.settingsPage);
			this.indicator.Top = ((Control)sender).Top;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002579 File Offset: 0x00000779
		private void bunifuButton23_Click(object sender, EventArgs e)
		{
			this.bunifuPages.SetPage(this.debugPage);
			this.indicator.Top = ((Control)sender).Top;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003BA4 File Offset: 0x00001DA4
		private void GUI_Load(object sender, EventArgs e)
		{
			NewOrbwalker.OrbwalkInit();
			base.Icon = Resources.spinlogo;
			GUI.Nickname = LoginForm.username;
			this.nicknameLbl.Text = GUI.Username;
			this.attackmoveKeyLbl.Text = "A";
			this.LicenceLbl.Text = "Expires at: " + GUI.Licence_Expiry;
			this.orbwalkerTypeLbl.Text = "Legacy";
			this.scanningTypeLbl.Text = "Standard";
			this.additionalWindupLbl.Text = this.windupBar.Value.ToString() + "ms";
			this.averagePingLbl.Text = GUI.averagePing.ToString() + "ms";
			this.delayLbl.Text = this.delayBar.Value.ToString() + "ms";
			this.mouseSnapLbl.Text = GUI.mouseSnap.ToString() + "ms";
			this.spaceglideKeyLbl.Text = GUI.SpaceglidingKey;
			this.uiUpdateTimer = new System.Threading.Timer(new TimerCallback(this.UpdateUI), null, 200, 200);
			this.AutoLoadConfig();
			this.Setup();
			this.SetRPC();
			Thread thread = new Thread(new ThreadStart(GUI.OptimizedChecker));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025A2 File Offset: 0x000007A2
		private static void PlayerPositionTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003D1C File Offset: 0x00001F1C
		public void AutoLoadConfig()
		{
			if (File.Exists("config.txt"))
			{
				try
				{
					this.ImportSettings();
				}
				catch
				{
					MessageBox.Show("There was a problem with importing your config");
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025A4 File Offset: 0x000007A4
		private void UpdateUI(object state)
		{
			base.BeginInvoke(new MethodInvoker(this.UpdateUI));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003D60 File Offset: 0x00001F60
		private void UpdateUI()
		{
			if (GUI.inGame)
			{
				this.spaceglideKeyLbl.Text = GUI.SpaceglidingKey;
				this.statusLbl.Text = "In Game";
				this.championLbl.Text = NewOrbwalker.RawChampionName;
				if (GUI.updatingGUI)
				{
					this.championLbl.Text = NewOrbwalker.ChampionName;
					this.debugChampion.Text = NewOrbwalker.RawChampionName;
					this.debugAttackSpeedRatio.Text = NewOrbwalker.ChampionAttackSpeedRatio.ToString("0.000");
					this.debugWindupPercent.Text = NewOrbwalker.ChampionAttackDelayPercent.ToString("0.000");
					this.debugAttackSpeed.Text = NewOrbwalker.ClientAttackSpeed.ToString("0.000");
					this.debugSecondsPerAttack.Text = NewOrbwalker.GetSecondsPerAttack().ToString("0.000");
					this.debugWindupDuration.Text = NewOrbwalker.GetWindupDuration().ToString("0.000") + " + " + NewOrbwalker.WindupBuffer.ToString("0.000");
					this.debugAttackDownTime.Text = (NewOrbwalker.GetSecondsPerAttack() - NewOrbwalker.GetWindupDuration()).ToString("0.000");
					this.debugEnemyChampionPos.Text = "X: " + NewOrbwalker.EnemyChampionPosition.X.ToString() + ", Y: " + NewOrbwalker.EnemyChampionPosition.Y.ToString();
					return;
				}
			}
			else
			{
				this.statusLbl.Text = "Not in Game";
				this.championLbl.Text = "No champion";
				NewOrbwalker.RawChampionName = string.Empty;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002515 File Offset: 0x00000715
		private void GUI_FormClosed(object sender, FormClosedEventArgs e)
		{
			base.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003F10 File Offset: 0x00002110
		private void spaceglideKeyDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.spaceglideKeyDropdown.SelectedIndex == 0)
			{
				GUI.K_SpacegliderKey = Key.Q;
				GUI.SpaceglidingKey = "Q";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 1)
			{
				GUI.K_SpacegliderKey = Key.Q;
				GUI.SpaceglidingKey = "W";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 2)
			{
				GUI.K_SpacegliderKey = Key.E;
				GUI.SpaceglidingKey = "E";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 3)
			{
				GUI.K_SpacegliderKey = Key.R;
				GUI.SpaceglidingKey = "R";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 4)
			{
				GUI.K_SpacegliderKey = Key.T;
				GUI.SpaceglidingKey = "T";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 5)
			{
				GUI.K_SpacegliderKey = Key.Y;
				GUI.SpaceglidingKey = "Y";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 6)
			{
				GUI.K_SpacegliderKey = Key.U;
				GUI.SpaceglidingKey = "U";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 7)
			{
				GUI.K_SpacegliderKey = Key.I;
				GUI.SpaceglidingKey = "I";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 8)
			{
				GUI.K_SpacegliderKey = Key.O;
				GUI.SpaceglidingKey = "O";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 9)
			{
				GUI.K_SpacegliderKey = Key.P;
				GUI.SpaceglidingKey = "P";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 10)
			{
				GUI.K_SpacegliderKey = Key.A;
				GUI.SpaceglidingKey = "A";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 11)
			{
				GUI.K_SpacegliderKey = Key.S;
				GUI.SpaceglidingKey = "S";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 12)
			{
				GUI.K_SpacegliderKey = Key.D;
				GUI.SpaceglidingKey = "D";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 13)
			{
				GUI.K_SpacegliderKey = Key.F;
				GUI.SpaceglidingKey = "F";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 14)
			{
				GUI.K_SpacegliderKey = Key.G;
				GUI.SpaceglidingKey = "G";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 15)
			{
				GUI.K_SpacegliderKey = Key.H;
				GUI.SpaceglidingKey = "H";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 16)
			{
				GUI.K_SpacegliderKey = Key.J;
				GUI.SpaceglidingKey = "J";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 17)
			{
				GUI.K_SpacegliderKey = Key.K;
				GUI.SpaceglidingKey = "K";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 18)
			{
				GUI.K_SpacegliderKey = Key.L;
				GUI.SpaceglidingKey = "L";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 19)
			{
				GUI.K_SpacegliderKey = Key.Z;
				GUI.SpaceglidingKey = "Z";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 20)
			{
				GUI.K_SpacegliderKey = Key.X;
				GUI.SpaceglidingKey = "X";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 21)
			{
				GUI.K_SpacegliderKey = Key.C;
				GUI.SpaceglidingKey = "C";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 22)
			{
				GUI.K_SpacegliderKey = Key.V;
				GUI.SpaceglidingKey = "V";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 23)
			{
				GUI.K_SpacegliderKey = Key.B;
				GUI.SpaceglidingKey = "B";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 24)
			{
				GUI.K_SpacegliderKey = Key.N;
				GUI.SpaceglidingKey = "N";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 25)
			{
				GUI.K_SpacegliderKey = Key.M;
				GUI.SpaceglidingKey = "M";
			}
			if (this.spaceglideKeyDropdown.SelectedIndex == 26)
			{
				GUI.K_SpacegliderKey = Key.Space;
				GUI.SpaceglidingKey = "Space";
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004290 File Offset: 0x00002490
		private void windupBar_Scroll(object sender, BunifuHScrollBar.ScrollEventArgs e)
		{
			GUI.additionalWindup = this.windupBar.Value;
			this.additionalWindupLbl.Text = this.windupBar.Value.ToString() + "ms";
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000042D8 File Offset: 0x000024D8
		private void delayBar_Scroll(object sender, BunifuHScrollBar.ScrollEventArgs e)
		{
			GUI.attackDelay = this.delayBar.Value;
			this.delayLbl.Text = this.delayBar.Value.ToString() + "ms";
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000025A2 File Offset: 0x000007A2
		private void drawRangeBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000025A2 File Offset: 0x000007A2
		private void testOrbBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000025B9 File Offset: 0x000007B9
		private void mouseFixBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.mouseFixBox.Checked)
			{
				GUI.mouseFix = true;
				return;
			}
			GUI.mouseFix = false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000025D5 File Offset: 0x000007D5
		private void updateGUIBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.updateGUIBox.Checked)
			{
				GUI.updatingGUI = true;
				return;
			}
			GUI.updatingGUI = false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000025A2 File Offset: 0x000007A2
		private void bunifuLabel8_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000025A2 File Offset: 0x000007A2
		private void header_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004320 File Offset: 0x00002520
		private void attackmoveKeyDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.attackmoveKeyDropdown.SelectedIndex == 0)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_Q;
				GUI.attackmoveKeyString = "Q";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 1)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_W;
				GUI.attackmoveKeyString = "W";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 2)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_E;
				GUI.attackmoveKeyString = "E";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 3)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_R;
				GUI.attackmoveKeyString = "R";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 4)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_T;
				GUI.attackmoveKeyString = "T";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 5)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_Y;
				GUI.attackmoveKeyString = "Y";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 6)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_U;
				GUI.attackmoveKeyString = "U";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 7)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_I;
				GUI.attackmoveKeyString = "I";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 8)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_O;
				GUI.attackmoveKeyString = "O";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 9)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_P;
				GUI.attackmoveKeyString = "P";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 10)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_A;
				GUI.attackmoveKeyString = "A";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 11)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_S;
				GUI.attackmoveKeyString = "S";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 12)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_D;
				GUI.attackmoveKeyString = "D";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 13)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_F;
				GUI.attackmoveKeyString = "F";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 14)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_G;
				GUI.attackmoveKeyString = "G";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 15)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_H;
				GUI.attackmoveKeyString = "H";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 16)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_J;
				GUI.attackmoveKeyString = "J";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 17)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_K;
				GUI.attackmoveKeyString = "K";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 18)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_L;
				GUI.attackmoveKeyString = "L";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 19)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_Z;
				GUI.attackmoveKeyString = "Z";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 20)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_X;
				GUI.attackmoveKeyString = "X";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 21)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_C;
				GUI.attackmoveKeyString = "C";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 22)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_V;
				GUI.attackmoveKeyString = "V";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 23)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_B;
				GUI.attackmoveKeyString = "B";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 24)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_N;
				GUI.attackmoveKeyString = "N";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 25)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_M;
				GUI.attackmoveKeyString = "M";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 26)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.LBUTTON;
				GUI.attackmoveKeyString = "Mouse Left";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 27)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.LBUTTON;
				GUI.attackmoveKeyString = "Mouse Right";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 28)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.LBUTTON;
				GUI.attackmoveKeyString = "Mouse Middle";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 29)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.LBUTTON;
				GUI.attackmoveKeyString = "Mouse Button 4";
			}
			if (this.attackmoveKeyDropdown.SelectedIndex == 30)
			{
				KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.LBUTTON;
				GUI.attackmoveKeyString = "Mouse Button 5";
			}
			this.attackmoveKeyLbl.Text = GUI.attackmoveKeyString;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025A2 File Offset: 0x000007A2
		private void rageModeBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000025F1 File Offset: 0x000007F1
		private void champDetectionBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.champDetectionBox.Checked)
			{
				GUI.champDetection = true;
				return;
			}
			GUI.champDetection = false;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000260D File Offset: 0x0000080D
		private void rangeBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.rangeBox.Checked)
			{
				GUI.showRange = true;
				return;
			}
			GUI.showRange = false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002629 File Offset: 0x00000829
		public bool StringToBool(string i)
		{
			return !(i == "0");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004730 File Offset: 0x00002930
		public bool ImportSettings()
		{
			string[] array = new string[14];
			int num = 0;
			if (File.Exists("config.txt"))
			{
				foreach (string text in File.ReadLines("config.txt"))
				{
					array[num] = text;
					num++;
				}
				GUI.additionalWindup = Convert.ToInt32(array[0]);
				GUI.attackDelay = Convert.ToInt32(array[1]);
				GUI.averagePing = Convert.ToInt32(array[2]);
				GUI.mouseSnap = Convert.ToInt32(array[3]);
				GUI.orbwalkType = Convert.ToInt32(array[4]);
				GUI.scanningType = Convert.ToInt32(array[5]);
				GUI.scripts = this.StringToBool(array[6]);
				GUI.updatingGUI = this.StringToBool(array[7]);
				GUI.mouseFix = this.StringToBool(array[8]);
				GUI.champDetection = this.StringToBool(array[9]);
				GUI.showRange = this.StringToBool(array[10]);
				this.windupBar.Value = GUI.additionalWindup;
				this.additionalWindupLbl.Text = this.windupBar.Value.ToString() + "ms";
				this.delayBar.Value = GUI.attackDelay;
				this.delayLbl.Text = this.delayBar.Value.ToString() + "ms";
				this.averagePingBar.Value = GUI.averagePing;
				this.averagePingLbl.Text = this.averagePingBar.Value.ToString() + "ms";
				this.mouseSnapBar.Value = GUI.mouseSnap;
				this.mouseSnapLbl.Text = this.mouseSnapBar.Value.ToString() + "ms";
				if (GUI.orbwalkType == 0)
				{
					this.bunifuDropdown1.SelectedIndex = 0;
					this.orbwalkerTypeLbl.Text = "Legacy";
				}
				if (GUI.orbwalkType == 1)
				{
					this.bunifuDropdown1.SelectedIndex = 1;
					this.orbwalkerTypeLbl.Text = "Experimental";
				}
				if (GUI.orbwalkType == 2)
				{
					this.bunifuDropdown1.SelectedIndex = 2;
					this.orbwalkerTypeLbl.Text = "Legacy EXP1";
				}
				if (GUI.orbwalkType == 3)
				{
					this.bunifuDropdown1.SelectedIndex = 3;
					this.orbwalkerTypeLbl.Text = "Legacy EXP2";
				}
				if (GUI.scanningType == 0)
				{
					this.scanningModeDropdown.SelectedIndex = 0;
					this.scanningTypeLbl.Text = "Standard";
				}
				if (GUI.scanningType == 1)
				{
					this.scanningModeDropdown.SelectedIndex = 1;
					this.scanningTypeLbl.Text = "Shoot in Range";
				}
				if (GUI.scanningType == 2)
				{
					this.scanningModeDropdown.SelectedIndex = 2;
					this.scanningTypeLbl.Text = "Dynamic";
				}
				if (GUI.scanningType == 3)
				{
					this.scanningModeDropdown.SelectedIndex = 3;
					this.scanningTypeLbl.Text = "Legacy";
				}
				if (GUI.scripts)
				{
					this.championScriptsBox.Checked = true;
				}
				else
				{
					this.championScriptsBox.Checked = false;
				}
				if (GUI.mouseFix)
				{
					this.mouseFixBox.Checked = true;
				}
				if (!GUI.mouseFix)
				{
					this.mouseFixBox.Checked = false;
				}
				if (GUI.updatingGUI)
				{
					this.updateGUIBox.Checked = true;
				}
				if (!GUI.updatingGUI)
				{
					this.updateGUIBox.Checked = false;
				}
				if (GUI.champDetection)
				{
					this.champDetectionBox.Checked = true;
				}
				if (!GUI.champDetection)
				{
					this.champDetectionBox.Checked = false;
				}
				if (GUI.showRange)
				{
					this.rangeBox.Checked = true;
				}
				if (!GUI.showRange)
				{
					this.rangeBox.Checked = false;
				}
				if (array[11] == "Q")
				{
					GUI.K_SpacegliderKey = Key.Q;
					GUI.SpaceglidingKey = "Q";
					this.spaceglideKeyDropdown.SelectedIndex = 0;
				}
				if (array[11] == "W")
				{
					GUI.K_SpacegliderKey = Key.W;
					GUI.SpaceglidingKey = "W";
					this.spaceglideKeyDropdown.SelectedIndex = 1;
				}
				if (array[11] == "E")
				{
					GUI.K_SpacegliderKey = Key.E;
					GUI.SpaceglidingKey = "E";
					this.spaceglideKeyDropdown.SelectedIndex = 2;
				}
				if (array[11] == "R")
				{
					GUI.K_SpacegliderKey = Key.R;
					GUI.SpaceglidingKey = "R";
					this.spaceglideKeyDropdown.SelectedIndex = 3;
				}
				if (array[11] == "T")
				{
					GUI.K_SpacegliderKey = Key.T;
					GUI.SpaceglidingKey = "T";
					this.spaceglideKeyDropdown.SelectedIndex = 4;
				}
				if (array[11] == "Y")
				{
					GUI.K_SpacegliderKey = Key.Y;
					GUI.SpaceglidingKey = "Y";
					this.spaceglideKeyDropdown.SelectedIndex = 5;
				}
				if (array[11] == "U")
				{
					GUI.K_SpacegliderKey = Key.U;
					GUI.SpaceglidingKey = "U";
					this.spaceglideKeyDropdown.SelectedIndex = 6;
				}
				if (array[11] == "I")
				{
					GUI.K_SpacegliderKey = Key.I;
					GUI.SpaceglidingKey = "I";
					this.spaceglideKeyDropdown.SelectedIndex = 7;
				}
				if (array[11] == "O")
				{
					GUI.K_SpacegliderKey = Key.O;
					GUI.SpaceglidingKey = "O";
					this.spaceglideKeyDropdown.SelectedIndex = 8;
				}
				if (array[11] == "P")
				{
					GUI.K_SpacegliderKey = Key.P;
					GUI.SpaceglidingKey = "P";
					this.spaceglideKeyDropdown.SelectedIndex = 9;
				}
				if (array[11] == "A")
				{
					GUI.K_SpacegliderKey = Key.A;
					GUI.SpaceglidingKey = "A";
					this.spaceglideKeyDropdown.SelectedIndex = 10;
				}
				if (array[11] == "S")
				{
					GUI.K_SpacegliderKey = Key.S;
					GUI.SpaceglidingKey = "S";
					this.spaceglideKeyDropdown.SelectedIndex = 11;
				}
				if (array[11] == "D")
				{
					GUI.K_SpacegliderKey = Key.D;
					GUI.SpaceglidingKey = "D";
					this.spaceglideKeyDropdown.SelectedIndex = 12;
				}
				if (array[11] == "F")
				{
					GUI.K_SpacegliderKey = Key.F;
					GUI.SpaceglidingKey = "F";
					this.spaceglideKeyDropdown.SelectedIndex = 13;
				}
				if (array[11] == "G")
				{
					GUI.K_SpacegliderKey = Key.G;
					GUI.SpaceglidingKey = "G";
					this.spaceglideKeyDropdown.SelectedIndex = 14;
				}
				if (array[11] == "H")
				{
					GUI.K_SpacegliderKey = Key.H;
					GUI.SpaceglidingKey = "H";
					this.spaceglideKeyDropdown.SelectedIndex = 15;
				}
				if (array[11] == "J")
				{
					GUI.K_SpacegliderKey = Key.J;
					GUI.SpaceglidingKey = "J";
					this.spaceglideKeyDropdown.SelectedIndex = 16;
				}
				if (array[11] == "K")
				{
					GUI.K_SpacegliderKey = Key.K;
					GUI.SpaceglidingKey = "K";
					this.spaceglideKeyDropdown.SelectedIndex = 17;
				}
				if (array[11] == "L")
				{
					GUI.K_SpacegliderKey = Key.L;
					GUI.SpaceglidingKey = "L";
					this.spaceglideKeyDropdown.SelectedIndex = 18;
				}
				if (array[11] == "Z")
				{
					GUI.K_SpacegliderKey = Key.Z;
					GUI.SpaceglidingKey = "Z";
					this.spaceglideKeyDropdown.SelectedIndex = 19;
				}
				if (array[11] == "X")
				{
					GUI.K_SpacegliderKey = Key.X;
					GUI.SpaceglidingKey = "X";
					this.spaceglideKeyDropdown.SelectedIndex = 20;
				}
				if (array[11] == "C")
				{
					GUI.K_SpacegliderKey = Key.C;
					GUI.SpaceglidingKey = "C";
					this.spaceglideKeyDropdown.SelectedIndex = 21;
				}
				if (array[11] == "V")
				{
					GUI.K_SpacegliderKey = Key.V;
					GUI.SpaceglidingKey = "V";
					this.spaceglideKeyDropdown.SelectedIndex = 22;
				}
				if (array[11] == "B")
				{
					GUI.K_SpacegliderKey = Key.B;
					GUI.SpaceglidingKey = "B";
					this.spaceglideKeyDropdown.SelectedIndex = 23;
				}
				if (array[11] == "N")
				{
					GUI.K_SpacegliderKey = Key.N;
					GUI.SpaceglidingKey = "N";
					this.spaceglideKeyDropdown.SelectedIndex = 24;
				}
				if (array[11] == "M")
				{
					GUI.K_SpacegliderKey = Key.M;
					GUI.SpaceglidingKey = "M";
					this.spaceglideKeyDropdown.SelectedIndex = 25;
				}
				if (array[11] == "Space")
				{
					GUI.K_SpacegliderKey = Key.Space;
					GUI.SpaceglidingKey = "Space";
					this.spaceglideKeyDropdown.SelectedIndex = 26;
				}
				if (array[12] == "Q")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_Q;
					GUI.attackmoveKeyString = "Q";
					this.attackmoveKeyDropdown.SelectedIndex = 0;
				}
				if (array[12] == "W")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_W;
					GUI.attackmoveKeyString = "W";
					this.attackmoveKeyDropdown.SelectedIndex = 1;
				}
				if (array[12] == "E")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_E;
					GUI.attackmoveKeyString = "E";
					this.attackmoveKeyDropdown.SelectedIndex = 2;
				}
				if (array[12] == "R")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_R;
					GUI.attackmoveKeyString = "R";
					this.attackmoveKeyDropdown.SelectedIndex = 3;
				}
				if (array[12] == "T")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_T;
					GUI.attackmoveKeyString = "T";
					this.attackmoveKeyDropdown.SelectedIndex = 4;
				}
				if (array[12] == "Y")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_Y;
					GUI.attackmoveKeyString = "Y";
					this.attackmoveKeyDropdown.SelectedIndex = 5;
				}
				if (array[12] == "U")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_U;
					GUI.attackmoveKeyString = "U";
					this.attackmoveKeyDropdown.SelectedIndex = 6;
				}
				if (array[12] == "I")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_I;
					GUI.attackmoveKeyString = "I";
					this.attackmoveKeyDropdown.SelectedIndex = 7;
				}
				if (array[12] == "O")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_O;
					GUI.attackmoveKeyString = "O";
					this.attackmoveKeyDropdown.SelectedIndex = 8;
				}
				if (array[12] == "P")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_P;
					GUI.attackmoveKeyString = "P";
					this.attackmoveKeyDropdown.SelectedIndex = 9;
				}
				if (array[12] == "A")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_A;
					GUI.attackmoveKeyString = "A";
					this.attackmoveKeyDropdown.SelectedIndex = 10;
				}
				if (array[12] == "S")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_S;
					GUI.attackmoveKeyString = "S";
					this.attackmoveKeyDropdown.SelectedIndex = 11;
				}
				if (array[12] == "D")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_D;
					GUI.attackmoveKeyString = "D";
					this.attackmoveKeyDropdown.SelectedIndex = 12;
				}
				if (array[12] == "F")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_F;
					GUI.attackmoveKeyString = "F";
					this.attackmoveKeyDropdown.SelectedIndex = 13;
				}
				if (array[12] == "G")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_G;
					GUI.attackmoveKeyString = "G";
					this.attackmoveKeyDropdown.SelectedIndex = 14;
				}
				if (array[12] == "H")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_H;
					GUI.attackmoveKeyString = "H";
					this.attackmoveKeyDropdown.SelectedIndex = 15;
				}
				if (array[12] == "J")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_J;
					GUI.attackmoveKeyString = "J";
					this.attackmoveKeyDropdown.SelectedIndex = 16;
				}
				if (array[12] == "K")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_K;
					GUI.attackmoveKeyString = "K";
					this.attackmoveKeyDropdown.SelectedIndex = 17;
				}
				if (array[12] == "L")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_L;
					GUI.attackmoveKeyString = "L";
					this.attackmoveKeyDropdown.SelectedIndex = 18;
				}
				if (array[12] == "Z")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_Z;
					GUI.attackmoveKeyString = "Z";
					this.attackmoveKeyDropdown.SelectedIndex = 19;
				}
				if (array[12] == "X")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_X;
					GUI.attackmoveKeyString = "X";
					this.attackmoveKeyDropdown.SelectedIndex = 20;
				}
				if (array[12] == "C")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_C;
					GUI.attackmoveKeyString = "C";
					this.attackmoveKeyDropdown.SelectedIndex = 21;
				}
				if (array[12] == "V")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_V;
					GUI.attackmoveKeyString = "V";
					this.attackmoveKeyDropdown.SelectedIndex = 22;
				}
				if (array[12] == "B")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_B;
					GUI.attackmoveKeyString = "B";
					this.attackmoveKeyDropdown.SelectedIndex = 23;
				}
				if (array[12] == "N")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_N;
					GUI.attackmoveKeyString = "N";
					this.attackmoveKeyDropdown.SelectedIndex = 24;
				}
				if (array[12] == "M")
				{
					KeyboardOut.attackmoveKey = KeyboardOut.ScanCodeShort.KEY_M;
					GUI.attackmoveKeyString = "M";
					this.attackmoveKeyDropdown.SelectedIndex = 25;
				}
				this.attackmoveKeyLbl.Text = GUI.attackmoveKeyString;
				this.spaceglideKeyLbl.Text = GUI.SpaceglidingKey;
				return true;
			}
			MessageBox.Show("There is no \"config.txt\" file in your folder.", "spinjitzuu");
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005510 File Offset: 0x00003710
		public void ExportConfig()
		{
			File.WriteAllLines("config.txt", new string[]
			{
				GUI.additionalWindup.ToString(),
				GUI.attackDelay.ToString(),
				GUI.averagePing.ToString(),
				GUI.mouseSnap.ToString(),
				GUI.orbwalkType.ToString(),
				GUI.scanningType.ToString(),
				this.BoolToString(GUI.scripts),
				this.BoolToString(GUI.updatingGUI),
				this.BoolToString(GUI.mouseFix),
				this.BoolToString(GUI.champDetection),
				this.BoolToString(GUI.showRange),
				GUI.K_SpacegliderKey.ToString(),
				GUI.attackmoveKeyString,
				"Config by: " + GUI.Username + " | spinjitzuu v" + GUI.version
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000263C File Offset: 0x0000083C
		public string BoolToString(bool b)
		{
			if (b)
			{
				return "1";
			}
			return "0";
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000264E File Offset: 0x0000084E
		private void importButton_Click(object sender, EventArgs e)
		{
			if (this.ImportSettings())
			{
				MessageBox.Show("Successfully imported your config!", "spinjitzuu");
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000266A File Offset: 0x0000086A
		private void exportButton_Click(object sender, EventArgs e)
		{
			this.ExportConfig();
			MessageBox.Show("Successfully exported your config!", "spinjitzuu");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000025A2 File Offset: 0x000007A2
		private void customResBox_CheckedChanged_1(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002684 File Offset: 0x00000884
		private void metroSetLink1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://discord.gg/WJw3wwNZ2a"));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002684 File Offset: 0x00000884
		private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://discord.gg/WJw3wwNZ2a"));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005604 File Offset: 0x00003804
		private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.bunifuDropdown1.SelectedIndex == 0)
			{
				GUI.orbwalkType = 0;
				this.orbwalkerTypeLbl.Text = "Legacy";
			}
			if (this.bunifuDropdown1.SelectedIndex == 1)
			{
				GUI.orbwalkType = 1;
				this.orbwalkerTypeLbl.Text = "Experimental";
			}
			if (this.bunifuDropdown1.SelectedIndex == 2)
			{
				GUI.orbwalkType = 2;
				this.orbwalkerTypeLbl.Text = "Legacy EXP1";
			}
			if (this.bunifuDropdown1.SelectedIndex == 3)
			{
				GUI.orbwalkType = 3;
				this.orbwalkerTypeLbl.Text = "Legacy EXP2";
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000056A4 File Offset: 0x000038A4
		private void bunifuHSlider1_Scroll(object sender, BunifuHScrollBar.ScrollEventArgs e)
		{
			GUI.averagePing = this.averagePingBar.Value;
			this.averagePingLbl.Text = this.averagePingBar.Value.ToString() + "ms";
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000025A2 File Offset: 0x000007A2
		private void drawRangeBox_CheckedChanged_1(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000056EC File Offset: 0x000038EC
		private void mouseSnapBar_Scroll(object sender, BunifuHScrollBar.ScrollEventArgs e)
		{
			GUI.mouseSnap = this.mouseSnapBar.Value;
			this.mouseSnapLbl.Text = this.mouseSnapBar.Value.ToString() + "ms";
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005734 File Offset: 0x00003934
		private void GUI_FormClosing(object sender, FormClosingEventArgs e)
		{
			File.WriteAllLines("logservice.txt", new string[]
			{
				e.CloseReason.ToString()
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002697 File Offset: 0x00000897
		private void bunifuButton24_Click(object sender, EventArgs e)
		{
			this.bunifuPages.SetPage(this.scriptsPage);
			this.indicator.Top = ((Control)sender).Top;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000576C File Offset: 0x0000396C
		private void kogEnabledBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.kogEnabledBox.Checked)
			{
				GUI.kogScripts = true;
				GUI.HandleSpells = new Thread(new ThreadStart(KogMaw.HandleInputs));
				GUI.HandleSpells.SetApartmentState(ApartmentState.STA);
				GUI.HandleSpells.Start();
				return;
			}
			GUI.kogScripts = false;
			if (GUI.HandleSpells.IsAlive)
			{
				GUI.HandleSpells.Abort();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026C0 File Offset: 0x000008C0
		private void kogQBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.kogQBox.Checked)
			{
				GUI.kogCastQ = true;
				return;
			}
			GUI.kogCastQ = false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000026DC File Offset: 0x000008DC
		private void kogEBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.kogEBox.Checked)
			{
				GUI.kogCastE = true;
				return;
			}
			GUI.kogCastE = false;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000026F8 File Offset: 0x000008F8
		private void kogRBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.kogRBox.Checked)
			{
				GUI.kogCastR = true;
				return;
			}
			GUI.kogCastR = false;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002714 File Offset: 0x00000914
		private void twitchEnabledBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.twitchEnabledBox.Checked)
			{
				GUI.twitchScripts = true;
				return;
			}
			GUI.twitchScripts = false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002730 File Offset: 0x00000930
		private void twitchWBoxx_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.twitchWBoxx.Checked)
			{
				GUI.twitchCastW = true;
				return;
			}
			GUI.twitchCastW = false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000274C File Offset: 0x0000094C
		private void championScriptsBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
			if (this.championScriptsBox.Checked)
			{
				GUI.scripts = true;
				return;
			}
			GUI.scripts = false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000057D4 File Offset: 0x000039D4
		private void manaBar_Scroll(object sender, BunifuHScrollBar.ScrollEventArgs e)
		{
			GUI.minMana = this.manaBar.Value;
			this.manaBarLbl.Text = this.manaBar.Value.ToString() + "%";
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000025A2 File Offset: 0x000007A2
		private void fullscreenScanBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000581C File Offset: 0x00003A1C
		private void scanningModeDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.scanningModeDropdown.SelectedIndex == 0)
			{
				GUI.scanningType = 0;
				this.scanningTypeLbl.Text = "Standard";
			}
			if (this.scanningModeDropdown.SelectedIndex == 1)
			{
				GUI.scanningType = 1;
				this.scanningTypeLbl.Text = "Shoot in Range";
			}
			if (this.scanningModeDropdown.SelectedIndex == 2)
			{
				GUI.scanningType = 2;
				this.scanningTypeLbl.Text = "Dynamic";
			}
			if (this.scanningModeDropdown.SelectedIndex == 3)
			{
				GUI.scanningType = 3;
				this.scanningTypeLbl.Text = "Fullscreen";
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000025A2 File Offset: 0x000007A2
		private void bunifuLabel4_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002768 File Offset: 0x00000968
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("http://www.spinjitzuu.lol/"));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000058BC File Offset: 0x00003ABC
		private void button1_Click(object sender, EventArgs e)
		{
			Point point = Point.Empty;
			point = PixelBot.GetEnemyPositionLegacy();
			Mouse.SetCursorPosition(point.X, point.Y);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000277B File Offset: 0x0000097B
		public static int RGB(int a, int r, int g, int b)
		{
			return 0 | a << 24 | r << 16 | g << 8 | b;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000025A2 File Offset: 0x000007A2
		private void button1_Click_1(object sender, EventArgs e)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000025A2 File Offset: 0x000007A2
		private void button2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000025A2 File Offset: 0x000007A2
		private void rangeScannerBox_CheckedChanged(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
		{
		}

		// Token: 0x0400000C RID: 12
		public static string version = "4.4.2";

		// Token: 0x0400000D RID: 13
		public static string Status;

		// Token: 0x0400000E RID: 14
		public static string Nickname;

		// Token: 0x0400000F RID: 15
		public static string Champion = string.Empty;

		// Token: 0x04000010 RID: 16
		public static int orbwalkType = 0;

		// Token: 0x04000011 RID: 17
		public static int scanningType = 0;

		// Token: 0x04000012 RID: 18
		public static bool isSpacegliding = false;

		// Token: 0x04000013 RID: 19
		public static bool inGame = false;

		// Token: 0x04000014 RID: 20
		public static bool drawRange = false;

		// Token: 0x04000015 RID: 21
		public static bool autoQSS = false;

		// Token: 0x04000016 RID: 22
		public static bool testingOrbwalker = false;

		// Token: 0x04000017 RID: 23
		public static bool mouseFix = false;

		// Token: 0x04000018 RID: 24
		public static bool updatingGUI = false;

		// Token: 0x04000019 RID: 25
		public static bool customResMode = false;

		// Token: 0x0400001A RID: 26
		public static bool rageMode = false;

		// Token: 0x0400001B RID: 27
		public static bool champDetection = true;

		// Token: 0x0400001C RID: 28
		public static bool twitchWReset = false;

		// Token: 0x0400001D RID: 29
		public static bool showRange = false;

		// Token: 0x0400001E RID: 30
		public static bool saveConfig = false;

		// Token: 0x0400001F RID: 31
		public static bool fullscreenScan = false;

		// Token: 0x04000020 RID: 32
		public static bool scripts = false;

		// Token: 0x04000021 RID: 33
		public static bool kogScripts = false;

		// Token: 0x04000022 RID: 34
		public static bool twitchScripts = false;

		// Token: 0x04000023 RID: 35
		public static bool twitchCastW = false;

		// Token: 0x04000024 RID: 36
		public static bool kogCastQ = false;

		// Token: 0x04000025 RID: 37
		public static bool kogCastE = false;

		// Token: 0x04000026 RID: 38
		public static bool kogCastR = false;

		// Token: 0x04000027 RID: 39
		public static bool scanningRange = false;

		// Token: 0x04000028 RID: 40
		public static bool HasProcess = false;

		// Token: 0x04000029 RID: 41
		public static bool IsExiting = false;

		// Token: 0x0400002A RID: 42
		public static string Username;

		// Token: 0x0400002B RID: 43
		public static string HWID;

		// Token: 0x0400002C RID: 44
		public static string Licence_Expiry;

		// Token: 0x0400002D RID: 45
		public static string Licence_Timeleft;

		// Token: 0x0400002E RID: 46
		public static string Licence_Name;

		// Token: 0x0400002F RID: 47
		public static short OrbkeyState = 32;

		// Token: 0x04000030 RID: 48
		public static short OrbWDkeyState = 90;

		// Token: 0x04000031 RID: 49
		public static string SpaceglidingKey = "Space";

		// Token: 0x04000032 RID: 50
		public static string NoTargetOrbwalkKey = "Z";

		// Token: 0x04000033 RID: 51
		public static string attackmoveKeyString = "A";

		// Token: 0x04000034 RID: 52
		public static string State;

		// Token: 0x04000035 RID: 53
		public static MouseButton M_SpacegliderKey;

		// Token: 0x04000036 RID: 54
		public static Key K_SpacegliderKey = Key.Space;

		// Token: 0x04000037 RID: 55
		public static Key K_NoTargetOrbwalkKey = Key.Z;

		// Token: 0x04000038 RID: 56
		public static Key K_ShowRange = Key.C;

		// Token: 0x04000039 RID: 57
		public static Key K_ChampionsOnly = Key.X;

		// Token: 0x0400003A RID: 58
		public static float windupPercentage = 0.2f;

		// Token: 0x0400003B RID: 59
		public static int additionalWindup = 0;

		// Token: 0x0400003C RID: 60
		public static int glidingSpeed = 100;

		// Token: 0x0400003D RID: 61
		public static int attackDelay = 0;

		// Token: 0x0400003E RID: 62
		public static int averagePing = 0;

		// Token: 0x0400003F RID: 63
		public static int mouseSnap = 5;

		// Token: 0x04000040 RID: 64
		public static int minMana = 100;

		// Token: 0x04000041 RID: 65
		public static Bitmap LeagueWindow;

		// Token: 0x04000042 RID: 66
		public static Thread HandleSpells;

		// Token: 0x04000043 RID: 67
		public static Process LeagueProcess = null;

		// Token: 0x04000044 RID: 68
		private System.Threading.Timer uiUpdateTimer;

		// Token: 0x04000045 RID: 69
		public static readonly double OrderTickRate = 1.0;

		// Token: 0x04000046 RID: 70
		public static readonly System.Timers.Timer HandleInputs = new System.Timers.Timer(GUI.OrderTickRate);
	}
}
