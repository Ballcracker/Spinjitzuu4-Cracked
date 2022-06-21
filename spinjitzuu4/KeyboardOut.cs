using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace spinjitzuu4
{
	// Token: 0x02000012 RID: 18
	internal class KeyboardOut
	{
		// Token: 0x06000095 RID: 149
		[DllImport("User32.dll", SetLastError = true)]
		private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		// Token: 0x06000096 RID: 150 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public static void HoldKey(byte key, int duration)
		{
			KeyboardOut.keybd_event(key, 0, 1, 0);
			Thread.Sleep(duration);
			KeyboardOut.keybd_event(key, 0, 2, 0);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0001479C File Offset: 0x0001299C
		public static void SendKeyDown(KeyboardOut.ScanCodeShort a)
		{
			KeyboardOut.INPUT[] array = new KeyboardOut.INPUT[1];
			KeyboardOut.INPUT input = default(KeyboardOut.INPUT);
			input.type = 1U;
			input.U.ki.wScan = a;
			input.U.ki.dwFlags = KeyboardOut.KEYEVENTF.SCANCODE;
			array[0] = input;
			KeyboardOut.SendInput(1U, array, KeyboardOut.INPUT.Size);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000147FC File Offset: 0x000129FC
		public static void SendKeyUp(KeyboardOut.ScanCodeShort a)
		{
			KeyboardOut.INPUT[] array = new KeyboardOut.INPUT[1];
			KeyboardOut.INPUT input = default(KeyboardOut.INPUT);
			input.type = 1U;
			input.U.ki.wScan = a;
			input.U.ki.dwFlags = (KeyboardOut.KEYEVENTF.KEYUP | KeyboardOut.KEYEVENTF.SCANCODE);
			array[0] = input;
			KeyboardOut.SendInput(1U, array, KeyboardOut.INPUT.Size);
		}

		// Token: 0x06000099 RID: 153
		[DllImport("User32.dll")]
		internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray)] [In] KeyboardOut.INPUT[] pInputs, int cbSize);

		// Token: 0x040001D5 RID: 469
		public static KeyboardOut.ScanCodeShort attackmoveKey = KeyboardOut.ScanCodeShort.KEY_A;

		// Token: 0x02000013 RID: 19
		public struct INPUT
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00002ACF File Offset: 0x00000CCF
			public static int Size
			{
				get
				{
					return Marshal.SizeOf(typeof(KeyboardOut.INPUT));
				}
			}

			// Token: 0x040001D6 RID: 470
			public uint type;

			// Token: 0x040001D7 RID: 471
			public KeyboardOut.InputUnion U;
		}

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Explicit)]
		public struct InputUnion
		{
			// Token: 0x040001D8 RID: 472
			[FieldOffset(0)]
			internal KeyboardOut.MOUSEINPUT mi;

			// Token: 0x040001D9 RID: 473
			[FieldOffset(0)]
			internal KeyboardOut.KEYBDINPUT ki;

			// Token: 0x040001DA RID: 474
			[FieldOffset(0)]
			internal KeyboardOut.HARDWAREINPUT hi;
		}

		// Token: 0x02000015 RID: 21
		public struct MOUSEINPUT
		{
			// Token: 0x040001DB RID: 475
			internal int dx;

			// Token: 0x040001DC RID: 476
			internal int dy;

			// Token: 0x040001DD RID: 477
			internal KeyboardOut.MouseEventDataXButtons mouseData;

			// Token: 0x040001DE RID: 478
			internal KeyboardOut.MOUSEEVENTF dwFlags;

			// Token: 0x040001DF RID: 479
			internal uint time;

			// Token: 0x040001E0 RID: 480
			internal UIntPtr dwExtraInfo;
		}

		// Token: 0x02000016 RID: 22
		[Flags]
		public enum MouseEventDataXButtons : uint
		{
			// Token: 0x040001E2 RID: 482
			Nothing = 0U,
			// Token: 0x040001E3 RID: 483
			XBUTTON1 = 1U,
			// Token: 0x040001E4 RID: 484
			XBUTTON2 = 2U
		}

		// Token: 0x02000017 RID: 23
		[Flags]
		public enum MOUSEEVENTF : uint
		{
			// Token: 0x040001E6 RID: 486
			ABSOLUTE = 32768U,
			// Token: 0x040001E7 RID: 487
			HWHEEL = 4096U,
			// Token: 0x040001E8 RID: 488
			MOVE = 1U,
			// Token: 0x040001E9 RID: 489
			MOVE_NOCOALESCE = 8192U,
			// Token: 0x040001EA RID: 490
			LEFTDOWN = 2U,
			// Token: 0x040001EB RID: 491
			LEFTUP = 4U,
			// Token: 0x040001EC RID: 492
			RIGHTDOWN = 8U,
			// Token: 0x040001ED RID: 493
			RIGHTUP = 16U,
			// Token: 0x040001EE RID: 494
			MIDDLEDOWN = 32U,
			// Token: 0x040001EF RID: 495
			MIDDLEUP = 64U,
			// Token: 0x040001F0 RID: 496
			VIRTUALDESK = 16384U,
			// Token: 0x040001F1 RID: 497
			WHEEL = 2048U,
			// Token: 0x040001F2 RID: 498
			XDOWN = 128U,
			// Token: 0x040001F3 RID: 499
			XUP = 256U
		}

		// Token: 0x02000018 RID: 24
		public struct KEYBDINPUT
		{
			// Token: 0x040001F4 RID: 500
			internal KeyboardOut.VirtualKeyShort wVk;

			// Token: 0x040001F5 RID: 501
			internal KeyboardOut.ScanCodeShort wScan;

			// Token: 0x040001F6 RID: 502
			internal KeyboardOut.KEYEVENTF dwFlags;

			// Token: 0x040001F7 RID: 503
			internal int time;

			// Token: 0x040001F8 RID: 504
			internal UIntPtr dwExtraInfo;
		}

		// Token: 0x02000019 RID: 25
		[Flags]
		public enum KEYEVENTF : uint
		{
			// Token: 0x040001FA RID: 506
			EXTENDEDKEY = 1U,
			// Token: 0x040001FB RID: 507
			KEYUP = 2U,
			// Token: 0x040001FC RID: 508
			SCANCODE = 8U,
			// Token: 0x040001FD RID: 509
			UNICODE = 4U
		}

		// Token: 0x0200001A RID: 26
		public enum VirtualKeyShort : short
		{
			// Token: 0x040001FF RID: 511
			LBUTTON = 1,
			// Token: 0x04000200 RID: 512
			RBUTTON,
			// Token: 0x04000201 RID: 513
			CANCEL,
			// Token: 0x04000202 RID: 514
			MBUTTON,
			// Token: 0x04000203 RID: 515
			XBUTTON1,
			// Token: 0x04000204 RID: 516
			XBUTTON2,
			// Token: 0x04000205 RID: 517
			BACK = 8,
			// Token: 0x04000206 RID: 518
			TAB,
			// Token: 0x04000207 RID: 519
			CLEAR = 12,
			// Token: 0x04000208 RID: 520
			RETURN,
			// Token: 0x04000209 RID: 521
			SHIFT = 16,
			// Token: 0x0400020A RID: 522
			CONTROL,
			// Token: 0x0400020B RID: 523
			MENU,
			// Token: 0x0400020C RID: 524
			PAUSE,
			// Token: 0x0400020D RID: 525
			CAPITAL,
			// Token: 0x0400020E RID: 526
			KANA,
			// Token: 0x0400020F RID: 527
			HANGUL = 21,
			// Token: 0x04000210 RID: 528
			JUNJA = 23,
			// Token: 0x04000211 RID: 529
			FINAL,
			// Token: 0x04000212 RID: 530
			HANJA,
			// Token: 0x04000213 RID: 531
			KANJI = 25,
			// Token: 0x04000214 RID: 532
			ESCAPE = 27,
			// Token: 0x04000215 RID: 533
			CONVERT,
			// Token: 0x04000216 RID: 534
			NONCONVERT,
			// Token: 0x04000217 RID: 535
			ACCEPT,
			// Token: 0x04000218 RID: 536
			MODECHANGE,
			// Token: 0x04000219 RID: 537
			SPACE,
			// Token: 0x0400021A RID: 538
			PRIOR,
			// Token: 0x0400021B RID: 539
			NEXT,
			// Token: 0x0400021C RID: 540
			END,
			// Token: 0x0400021D RID: 541
			HOME,
			// Token: 0x0400021E RID: 542
			LEFT,
			// Token: 0x0400021F RID: 543
			UP,
			// Token: 0x04000220 RID: 544
			RIGHT,
			// Token: 0x04000221 RID: 545
			DOWN,
			// Token: 0x04000222 RID: 546
			SELECT,
			// Token: 0x04000223 RID: 547
			PRINT,
			// Token: 0x04000224 RID: 548
			EXECUTE,
			// Token: 0x04000225 RID: 549
			SNAPSHOT,
			// Token: 0x04000226 RID: 550
			INSERT,
			// Token: 0x04000227 RID: 551
			DELETE,
			// Token: 0x04000228 RID: 552
			HELP,
			// Token: 0x04000229 RID: 553
			KEY_0,
			// Token: 0x0400022A RID: 554
			KEY_1,
			// Token: 0x0400022B RID: 555
			KEY_2,
			// Token: 0x0400022C RID: 556
			KEY_3,
			// Token: 0x0400022D RID: 557
			KEY_4,
			// Token: 0x0400022E RID: 558
			KEY_5,
			// Token: 0x0400022F RID: 559
			KEY_6,
			// Token: 0x04000230 RID: 560
			KEY_7,
			// Token: 0x04000231 RID: 561
			KEY_8,
			// Token: 0x04000232 RID: 562
			KEY_9,
			// Token: 0x04000233 RID: 563
			KEY_A = 65,
			// Token: 0x04000234 RID: 564
			KEY_B,
			// Token: 0x04000235 RID: 565
			KEY_C,
			// Token: 0x04000236 RID: 566
			KEY_D,
			// Token: 0x04000237 RID: 567
			KEY_E,
			// Token: 0x04000238 RID: 568
			KEY_F,
			// Token: 0x04000239 RID: 569
			KEY_G,
			// Token: 0x0400023A RID: 570
			KEY_H,
			// Token: 0x0400023B RID: 571
			KEY_I,
			// Token: 0x0400023C RID: 572
			KEY_J,
			// Token: 0x0400023D RID: 573
			KEY_K,
			// Token: 0x0400023E RID: 574
			KEY_L,
			// Token: 0x0400023F RID: 575
			KEY_M,
			// Token: 0x04000240 RID: 576
			KEY_N,
			// Token: 0x04000241 RID: 577
			KEY_O,
			// Token: 0x04000242 RID: 578
			KEY_P,
			// Token: 0x04000243 RID: 579
			KEY_Q,
			// Token: 0x04000244 RID: 580
			KEY_R,
			// Token: 0x04000245 RID: 581
			KEY_S,
			// Token: 0x04000246 RID: 582
			KEY_T,
			// Token: 0x04000247 RID: 583
			KEY_U,
			// Token: 0x04000248 RID: 584
			KEY_V,
			// Token: 0x04000249 RID: 585
			KEY_W,
			// Token: 0x0400024A RID: 586
			KEY_X,
			// Token: 0x0400024B RID: 587
			KEY_Y,
			// Token: 0x0400024C RID: 588
			KEY_Z,
			// Token: 0x0400024D RID: 589
			LWIN,
			// Token: 0x0400024E RID: 590
			RWIN,
			// Token: 0x0400024F RID: 591
			APPS,
			// Token: 0x04000250 RID: 592
			SLEEP = 95,
			// Token: 0x04000251 RID: 593
			NUMPAD0,
			// Token: 0x04000252 RID: 594
			NUMPAD1,
			// Token: 0x04000253 RID: 595
			NUMPAD2,
			// Token: 0x04000254 RID: 596
			NUMPAD3,
			// Token: 0x04000255 RID: 597
			NUMPAD4,
			// Token: 0x04000256 RID: 598
			NUMPAD5,
			// Token: 0x04000257 RID: 599
			NUMPAD6,
			// Token: 0x04000258 RID: 600
			NUMPAD7,
			// Token: 0x04000259 RID: 601
			NUMPAD8,
			// Token: 0x0400025A RID: 602
			NUMPAD9,
			// Token: 0x0400025B RID: 603
			MULTIPLY,
			// Token: 0x0400025C RID: 604
			ADD,
			// Token: 0x0400025D RID: 605
			SEPARATOR,
			// Token: 0x0400025E RID: 606
			SUBTRACT,
			// Token: 0x0400025F RID: 607
			DECIMAL,
			// Token: 0x04000260 RID: 608
			DIVIDE,
			// Token: 0x04000261 RID: 609
			F1,
			// Token: 0x04000262 RID: 610
			F2,
			// Token: 0x04000263 RID: 611
			F3,
			// Token: 0x04000264 RID: 612
			F4,
			// Token: 0x04000265 RID: 613
			F5,
			// Token: 0x04000266 RID: 614
			F6,
			// Token: 0x04000267 RID: 615
			F7,
			// Token: 0x04000268 RID: 616
			F8,
			// Token: 0x04000269 RID: 617
			F9,
			// Token: 0x0400026A RID: 618
			F10,
			// Token: 0x0400026B RID: 619
			F11,
			// Token: 0x0400026C RID: 620
			F12,
			// Token: 0x0400026D RID: 621
			F13,
			// Token: 0x0400026E RID: 622
			F14,
			// Token: 0x0400026F RID: 623
			F15,
			// Token: 0x04000270 RID: 624
			F16,
			// Token: 0x04000271 RID: 625
			F17,
			// Token: 0x04000272 RID: 626
			F18,
			// Token: 0x04000273 RID: 627
			F19,
			// Token: 0x04000274 RID: 628
			F20,
			// Token: 0x04000275 RID: 629
			F21,
			// Token: 0x04000276 RID: 630
			F22,
			// Token: 0x04000277 RID: 631
			F23,
			// Token: 0x04000278 RID: 632
			F24,
			// Token: 0x04000279 RID: 633
			NUMLOCK = 144,
			// Token: 0x0400027A RID: 634
			SCROLL,
			// Token: 0x0400027B RID: 635
			LSHIFT = 160,
			// Token: 0x0400027C RID: 636
			RSHIFT,
			// Token: 0x0400027D RID: 637
			LCONTROL,
			// Token: 0x0400027E RID: 638
			RCONTROL,
			// Token: 0x0400027F RID: 639
			LMENU,
			// Token: 0x04000280 RID: 640
			RMENU,
			// Token: 0x04000281 RID: 641
			BROWSER_BACK,
			// Token: 0x04000282 RID: 642
			BROWSER_FORWARD,
			// Token: 0x04000283 RID: 643
			BROWSER_REFRESH,
			// Token: 0x04000284 RID: 644
			BROWSER_STOP,
			// Token: 0x04000285 RID: 645
			BROWSER_SEARCH,
			// Token: 0x04000286 RID: 646
			BROWSER_FAVORITES,
			// Token: 0x04000287 RID: 647
			BROWSER_HOME,
			// Token: 0x04000288 RID: 648
			VOLUME_MUTE,
			// Token: 0x04000289 RID: 649
			VOLUME_DOWN,
			// Token: 0x0400028A RID: 650
			VOLUME_UP,
			// Token: 0x0400028B RID: 651
			MEDIA_NEXT_TRACK,
			// Token: 0x0400028C RID: 652
			MEDIA_PREV_TRACK,
			// Token: 0x0400028D RID: 653
			MEDIA_STOP,
			// Token: 0x0400028E RID: 654
			MEDIA_PLAY_PAUSE,
			// Token: 0x0400028F RID: 655
			LAUNCH_MAIL,
			// Token: 0x04000290 RID: 656
			LAUNCH_MEDIA_SELECT,
			// Token: 0x04000291 RID: 657
			LAUNCH_APP1,
			// Token: 0x04000292 RID: 658
			LAUNCH_APP2,
			// Token: 0x04000293 RID: 659
			OEM_1 = 186,
			// Token: 0x04000294 RID: 660
			OEM_PLUS,
			// Token: 0x04000295 RID: 661
			OEM_COMMA,
			// Token: 0x04000296 RID: 662
			OEM_MINUS,
			// Token: 0x04000297 RID: 663
			OEM_PERIOD,
			// Token: 0x04000298 RID: 664
			OEM_2,
			// Token: 0x04000299 RID: 665
			OEM_3,
			// Token: 0x0400029A RID: 666
			OEM_4 = 219,
			// Token: 0x0400029B RID: 667
			OEM_5,
			// Token: 0x0400029C RID: 668
			OEM_6,
			// Token: 0x0400029D RID: 669
			OEM_7,
			// Token: 0x0400029E RID: 670
			OEM_8,
			// Token: 0x0400029F RID: 671
			OEM_102 = 226,
			// Token: 0x040002A0 RID: 672
			PROCESSKEY = 229,
			// Token: 0x040002A1 RID: 673
			PACKET = 231,
			// Token: 0x040002A2 RID: 674
			ATTN = 246,
			// Token: 0x040002A3 RID: 675
			CRSEL,
			// Token: 0x040002A4 RID: 676
			EXSEL,
			// Token: 0x040002A5 RID: 677
			EREOF,
			// Token: 0x040002A6 RID: 678
			PLAY,
			// Token: 0x040002A7 RID: 679
			ZOOM,
			// Token: 0x040002A8 RID: 680
			NONAME,
			// Token: 0x040002A9 RID: 681
			PA1,
			// Token: 0x040002AA RID: 682
			OEM_CLEAR
		}

		// Token: 0x0200001B RID: 27
		public enum ScanCodeShort : short
		{
			// Token: 0x040002AC RID: 684
			LBUTTON,
			// Token: 0x040002AD RID: 685
			RBUTTON = 0,
			// Token: 0x040002AE RID: 686
			CANCEL = 70,
			// Token: 0x040002AF RID: 687
			MBUTTON = 0,
			// Token: 0x040002B0 RID: 688
			XBUTTON1 = 0,
			// Token: 0x040002B1 RID: 689
			XBUTTON2 = 0,
			// Token: 0x040002B2 RID: 690
			BACK = 14,
			// Token: 0x040002B3 RID: 691
			TAB,
			// Token: 0x040002B4 RID: 692
			CLEAR = 76,
			// Token: 0x040002B5 RID: 693
			RETURN = 28,
			// Token: 0x040002B6 RID: 694
			SHIFT = 42,
			// Token: 0x040002B7 RID: 695
			CONTROL = 29,
			// Token: 0x040002B8 RID: 696
			MENU = 56,
			// Token: 0x040002B9 RID: 697
			PAUSE = 0,
			// Token: 0x040002BA RID: 698
			CAPITAL = 58,
			// Token: 0x040002BB RID: 699
			KANA = 0,
			// Token: 0x040002BC RID: 700
			HANGUL = 0,
			// Token: 0x040002BD RID: 701
			JUNJA = 0,
			// Token: 0x040002BE RID: 702
			FINAL = 0,
			// Token: 0x040002BF RID: 703
			HANJA = 0,
			// Token: 0x040002C0 RID: 704
			KANJI = 0,
			// Token: 0x040002C1 RID: 705
			ESCAPE,
			// Token: 0x040002C2 RID: 706
			CONVERT = 0,
			// Token: 0x040002C3 RID: 707
			NONCONVERT = 0,
			// Token: 0x040002C4 RID: 708
			ACCEPT = 0,
			// Token: 0x040002C5 RID: 709
			MODECHANGE = 0,
			// Token: 0x040002C6 RID: 710
			SPACE = 57,
			// Token: 0x040002C7 RID: 711
			PRIOR = 73,
			// Token: 0x040002C8 RID: 712
			NEXT = 81,
			// Token: 0x040002C9 RID: 713
			END = 79,
			// Token: 0x040002CA RID: 714
			HOME = 71,
			// Token: 0x040002CB RID: 715
			LEFT = 75,
			// Token: 0x040002CC RID: 716
			UP = 72,
			// Token: 0x040002CD RID: 717
			RIGHT = 77,
			// Token: 0x040002CE RID: 718
			DOWN = 80,
			// Token: 0x040002CF RID: 719
			SELECT = 0,
			// Token: 0x040002D0 RID: 720
			PRINT = 0,
			// Token: 0x040002D1 RID: 721
			EXECUTE = 0,
			// Token: 0x040002D2 RID: 722
			SNAPSHOT = 84,
			// Token: 0x040002D3 RID: 723
			INSERT = 82,
			// Token: 0x040002D4 RID: 724
			DELETE,
			// Token: 0x040002D5 RID: 725
			HELP = 99,
			// Token: 0x040002D6 RID: 726
			KEY_0 = 11,
			// Token: 0x040002D7 RID: 727
			KEY_1 = 2,
			// Token: 0x040002D8 RID: 728
			KEY_2,
			// Token: 0x040002D9 RID: 729
			KEY_3,
			// Token: 0x040002DA RID: 730
			KEY_4,
			// Token: 0x040002DB RID: 731
			KEY_5,
			// Token: 0x040002DC RID: 732
			KEY_6,
			// Token: 0x040002DD RID: 733
			KEY_7,
			// Token: 0x040002DE RID: 734
			KEY_8,
			// Token: 0x040002DF RID: 735
			KEY_9,
			// Token: 0x040002E0 RID: 736
			KEY_A = 30,
			// Token: 0x040002E1 RID: 737
			KEY_B = 48,
			// Token: 0x040002E2 RID: 738
			KEY_C = 46,
			// Token: 0x040002E3 RID: 739
			KEY_D = 32,
			// Token: 0x040002E4 RID: 740
			KEY_E = 18,
			// Token: 0x040002E5 RID: 741
			KEY_F = 33,
			// Token: 0x040002E6 RID: 742
			KEY_G,
			// Token: 0x040002E7 RID: 743
			KEY_H,
			// Token: 0x040002E8 RID: 744
			KEY_I = 23,
			// Token: 0x040002E9 RID: 745
			KEY_J = 36,
			// Token: 0x040002EA RID: 746
			KEY_K,
			// Token: 0x040002EB RID: 747
			KEY_L,
			// Token: 0x040002EC RID: 748
			KEY_M = 50,
			// Token: 0x040002ED RID: 749
			KEY_N = 49,
			// Token: 0x040002EE RID: 750
			KEY_O = 24,
			// Token: 0x040002EF RID: 751
			KEY_P,
			// Token: 0x040002F0 RID: 752
			KEY_Q = 16,
			// Token: 0x040002F1 RID: 753
			KEY_R = 19,
			// Token: 0x040002F2 RID: 754
			KEY_S = 31,
			// Token: 0x040002F3 RID: 755
			KEY_T = 20,
			// Token: 0x040002F4 RID: 756
			KEY_U = 22,
			// Token: 0x040002F5 RID: 757
			KEY_V = 47,
			// Token: 0x040002F6 RID: 758
			KEY_W = 17,
			// Token: 0x040002F7 RID: 759
			KEY_X = 45,
			// Token: 0x040002F8 RID: 760
			KEY_Y = 21,
			// Token: 0x040002F9 RID: 761
			KEY_Z = 44,
			// Token: 0x040002FA RID: 762
			LWIN = 91,
			// Token: 0x040002FB RID: 763
			RWIN,
			// Token: 0x040002FC RID: 764
			APPS,
			// Token: 0x040002FD RID: 765
			SLEEP = 95,
			// Token: 0x040002FE RID: 766
			NUMPAD0 = 82,
			// Token: 0x040002FF RID: 767
			NUMPAD1 = 79,
			// Token: 0x04000300 RID: 768
			NUMPAD2,
			// Token: 0x04000301 RID: 769
			NUMPAD3,
			// Token: 0x04000302 RID: 770
			NUMPAD4 = 75,
			// Token: 0x04000303 RID: 771
			NUMPAD5,
			// Token: 0x04000304 RID: 772
			NUMPAD6,
			// Token: 0x04000305 RID: 773
			NUMPAD7 = 71,
			// Token: 0x04000306 RID: 774
			NUMPAD8,
			// Token: 0x04000307 RID: 775
			NUMPAD9,
			// Token: 0x04000308 RID: 776
			MULTIPLY = 55,
			// Token: 0x04000309 RID: 777
			ADD = 78,
			// Token: 0x0400030A RID: 778
			SEPARATOR = 0,
			// Token: 0x0400030B RID: 779
			SUBTRACT = 74,
			// Token: 0x0400030C RID: 780
			DECIMAL = 83,
			// Token: 0x0400030D RID: 781
			DIVIDE = 53,
			// Token: 0x0400030E RID: 782
			F1 = 59,
			// Token: 0x0400030F RID: 783
			F2,
			// Token: 0x04000310 RID: 784
			F3,
			// Token: 0x04000311 RID: 785
			F4,
			// Token: 0x04000312 RID: 786
			F5,
			// Token: 0x04000313 RID: 787
			F6,
			// Token: 0x04000314 RID: 788
			F7,
			// Token: 0x04000315 RID: 789
			F8,
			// Token: 0x04000316 RID: 790
			F9,
			// Token: 0x04000317 RID: 791
			F10,
			// Token: 0x04000318 RID: 792
			F11 = 87,
			// Token: 0x04000319 RID: 793
			F12,
			// Token: 0x0400031A RID: 794
			F13 = 100,
			// Token: 0x0400031B RID: 795
			F14,
			// Token: 0x0400031C RID: 796
			F15,
			// Token: 0x0400031D RID: 797
			F16,
			// Token: 0x0400031E RID: 798
			F17,
			// Token: 0x0400031F RID: 799
			F18,
			// Token: 0x04000320 RID: 800
			F19,
			// Token: 0x04000321 RID: 801
			F20,
			// Token: 0x04000322 RID: 802
			F21,
			// Token: 0x04000323 RID: 803
			F22,
			// Token: 0x04000324 RID: 804
			F23,
			// Token: 0x04000325 RID: 805
			F24 = 118,
			// Token: 0x04000326 RID: 806
			NUMLOCK = 69,
			// Token: 0x04000327 RID: 807
			SCROLL,
			// Token: 0x04000328 RID: 808
			LSHIFT = 42,
			// Token: 0x04000329 RID: 809
			RSHIFT = 54,
			// Token: 0x0400032A RID: 810
			LCONTROL = 29,
			// Token: 0x0400032B RID: 811
			RCONTROL = 29,
			// Token: 0x0400032C RID: 812
			LMENU = 56,
			// Token: 0x0400032D RID: 813
			RMENU = 56,
			// Token: 0x0400032E RID: 814
			BROWSER_BACK = 106,
			// Token: 0x0400032F RID: 815
			BROWSER_FORWARD = 105,
			// Token: 0x04000330 RID: 816
			BROWSER_REFRESH = 103,
			// Token: 0x04000331 RID: 817
			BROWSER_STOP,
			// Token: 0x04000332 RID: 818
			BROWSER_SEARCH = 101,
			// Token: 0x04000333 RID: 819
			BROWSER_FAVORITES,
			// Token: 0x04000334 RID: 820
			BROWSER_HOME = 50,
			// Token: 0x04000335 RID: 821
			VOLUME_MUTE = 32,
			// Token: 0x04000336 RID: 822
			VOLUME_DOWN = 46,
			// Token: 0x04000337 RID: 823
			VOLUME_UP = 48,
			// Token: 0x04000338 RID: 824
			MEDIA_NEXT_TRACK = 25,
			// Token: 0x04000339 RID: 825
			MEDIA_PREV_TRACK = 16,
			// Token: 0x0400033A RID: 826
			MEDIA_STOP = 36,
			// Token: 0x0400033B RID: 827
			MEDIA_PLAY_PAUSE = 34,
			// Token: 0x0400033C RID: 828
			LAUNCH_MAIL = 108,
			// Token: 0x0400033D RID: 829
			LAUNCH_MEDIA_SELECT,
			// Token: 0x0400033E RID: 830
			LAUNCH_APP1 = 107,
			// Token: 0x0400033F RID: 831
			LAUNCH_APP2 = 33,
			// Token: 0x04000340 RID: 832
			OEM_1 = 39,
			// Token: 0x04000341 RID: 833
			OEM_PLUS = 13,
			// Token: 0x04000342 RID: 834
			OEM_COMMA = 51,
			// Token: 0x04000343 RID: 835
			OEM_MINUS = 12,
			// Token: 0x04000344 RID: 836
			OEM_PERIOD = 52,
			// Token: 0x04000345 RID: 837
			OEM_2,
			// Token: 0x04000346 RID: 838
			OEM_3 = 41,
			// Token: 0x04000347 RID: 839
			OEM_4 = 26,
			// Token: 0x04000348 RID: 840
			OEM_5 = 43,
			// Token: 0x04000349 RID: 841
			OEM_6 = 27,
			// Token: 0x0400034A RID: 842
			OEM_7 = 40,
			// Token: 0x0400034B RID: 843
			OEM_8 = 0,
			// Token: 0x0400034C RID: 844
			OEM_102 = 86,
			// Token: 0x0400034D RID: 845
			PROCESSKEY = 0,
			// Token: 0x0400034E RID: 846
			PACKET = 0,
			// Token: 0x0400034F RID: 847
			ATTN = 0,
			// Token: 0x04000350 RID: 848
			CRSEL = 0,
			// Token: 0x04000351 RID: 849
			EXSEL = 0,
			// Token: 0x04000352 RID: 850
			EREOF = 93,
			// Token: 0x04000353 RID: 851
			PLAY = 0,
			// Token: 0x04000354 RID: 852
			ZOOM = 98,
			// Token: 0x04000355 RID: 853
			NONAME = 0,
			// Token: 0x04000356 RID: 854
			PA1 = 0,
			// Token: 0x04000357 RID: 855
			OEM_CLEAR = 0
		}

		// Token: 0x0200001C RID: 28
		public struct HARDWAREINPUT
		{
			// Token: 0x04000358 RID: 856
			internal int uMsg;

			// Token: 0x04000359 RID: 857
			internal short wParamL;

			// Token: 0x0400035A RID: 858
			internal short wParamH;
		}
	}
}
