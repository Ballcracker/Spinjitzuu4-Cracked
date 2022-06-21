using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace spinjitzuu4
{
	// Token: 0x0200000B RID: 11
	internal class KeyboardDirectInput
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00014720 File Offset: 0x00012920
		private static KeyboardDirectInput.INPUT[] CreateKeyBoardInput(short wVk, short wScan, int dwFlags, int time, IntPtr dwExtraInfo)
		{
			KeyboardDirectInput.INPUT[] array = new KeyboardDirectInput.INPUT[1];
			array[0].type = 1;
			array[0].ki.wVk = wVk;
			array[0].ki.wScan = wScan;
			array[0].ki.dwFlags = dwFlags;
			array[0].ki.time = time;
			array[0].ki.dwExtraInfo = dwExtraInfo;
			return array;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000299B File Offset: 0x00000B9B
		private static KeyboardDirectInput.INPUT[] CreateKeyBoardInput(short wVk, short wScan, int dwFlags)
		{
			return KeyboardDirectInput.CreateKeyBoardInput(wVk, wScan, dwFlags, 0, IntPtr.Zero);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000029AB File Offset: 0x00000BAB
		private static KeyboardDirectInput.INPUT[] CreateKeyBoardInput(short wVk, short wScan)
		{
			return KeyboardDirectInput.CreateKeyBoardInput(wVk, wScan, 0, 0, IntPtr.Zero);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000029BB File Offset: 0x00000BBB
		public static void SendKeyDown(short keyBoardScanCode)
		{
			NativeImport.SendInput(1U, KeyboardDirectInput.CreateKeyBoardInput(0, keyBoardScanCode), Marshal.SizeOf(typeof(KeyboardDirectInput.INPUT)));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000029DB File Offset: 0x00000BDB
		public static void SendKeyDown(KeyboardDirectInput.KeyBoardScanCodes keyBoardScanCode)
		{
			KeyboardDirectInput.SendKeyDown((short)keyBoardScanCode);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000029E3 File Offset: 0x00000BE3
		public static void SendKeyUp(short keyBoardScanCode)
		{
			Thread.Sleep(100);
			NativeImport.SendInput(1U, KeyboardDirectInput.CreateKeyBoardInput(0, keyBoardScanCode, 2), Marshal.SizeOf(typeof(KeyboardDirectInput.INPUT)));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002A0B File Offset: 0x00000C0B
		public static void SendKeyUp(KeyboardDirectInput.KeyBoardScanCodes keyBoardScanCode)
		{
			KeyboardDirectInput.SendKeyUp((short)keyBoardScanCode);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002A13 File Offset: 0x00000C13
		public static void SendKey(short keyBoardScanCode)
		{
			KeyboardDirectInput.SendKeyDown(keyBoardScanCode);
			KeyboardDirectInput.SendKeyUp(keyBoardScanCode);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002A21 File Offset: 0x00000C21
		public static void SendKey(short keyBoardScanCode, int KeyUporDown)
		{
			Thread.Sleep(100);
			NativeImport.SendInput(1U, KeyboardDirectInput.CreateKeyBoardInput(0, keyBoardScanCode, KeyUporDown), Marshal.SizeOf(typeof(KeyboardDirectInput.INPUT)));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002A49 File Offset: 0x00000C49
		public static void SendVirtualKeyDown(KeyboardDirectInput.VirtualKeyCodes virtualKeyCode)
		{
			Thread.Sleep(100);
			NativeImport.SendInput(1U, KeyboardDirectInput.CreateKeyBoardInput((short)virtualKeyCode, 0), Marshal.SizeOf(typeof(KeyboardDirectInput.INPUT)));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002A71 File Offset: 0x00000C71
		public static void SendVirtualKeyUp(KeyboardDirectInput.VirtualKeyCodes virtualKeyCode)
		{
			Thread.Sleep(100);
			NativeImport.SendInput(1U, KeyboardDirectInput.CreateKeyBoardInput((short)virtualKeyCode, 0, 2), Marshal.SizeOf(typeof(KeyboardDirectInput.INPUT)));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002A9A File Offset: 0x00000C9A
		public static void SendVirtualKey(KeyboardDirectInput.VirtualKeyCodes virtualKeyCode)
		{
			KeyboardDirectInput.SendVirtualKeyDown(virtualKeyCode);
			KeyboardDirectInput.SendVirtualKeyUp(virtualKeyCode);
		}

		// Token: 0x0200000C RID: 12
		public struct MOUSEINPUT
		{
			// Token: 0x040000E5 RID: 229
			public int dx;

			// Token: 0x040000E6 RID: 230
			public int dy;

			// Token: 0x040000E7 RID: 231
			public int mouseData;

			// Token: 0x040000E8 RID: 232
			public int dwFlags;

			// Token: 0x040000E9 RID: 233
			public int time;

			// Token: 0x040000EA RID: 234
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200000D RID: 13
		public struct KEYBDINPUT
		{
			// Token: 0x040000EB RID: 235
			public short wVk;

			// Token: 0x040000EC RID: 236
			public short wScan;

			// Token: 0x040000ED RID: 237
			public int dwFlags;

			// Token: 0x040000EE RID: 238
			public int time;

			// Token: 0x040000EF RID: 239
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200000E RID: 14
		public struct HARDWAREINPUT
		{
			// Token: 0x040000F0 RID: 240
			public int uMsg;

			// Token: 0x040000F1 RID: 241
			public short wParamL;

			// Token: 0x040000F2 RID: 242
			public short wParamH;
		}

		// Token: 0x0200000F RID: 15
		[StructLayout(LayoutKind.Explicit)]
		public struct INPUT
		{
			// Token: 0x040000F3 RID: 243
			[FieldOffset(0)]
			public int type;

			// Token: 0x040000F4 RID: 244
			[FieldOffset(4)]
			public KeyboardDirectInput.MOUSEINPUT mi;

			// Token: 0x040000F5 RID: 245
			[FieldOffset(4)]
			public KeyboardDirectInput.KEYBDINPUT ki;

			// Token: 0x040000F6 RID: 246
			[FieldOffset(4)]
			public KeyboardDirectInput.HARDWAREINPUT hi;
		}

		// Token: 0x02000010 RID: 16
		public enum VirtualKeyCodes
		{
			// Token: 0x040000F8 RID: 248
			LBUTTON = 1,
			// Token: 0x040000F9 RID: 249
			RBUTTON,
			// Token: 0x040000FA RID: 250
			CANCEL,
			// Token: 0x040000FB RID: 251
			MBUTTON,
			// Token: 0x040000FC RID: 252
			XBUTTON1,
			// Token: 0x040000FD RID: 253
			XBUTTON2,
			// Token: 0x040000FE RID: 254
			BACK = 8,
			// Token: 0x040000FF RID: 255
			TAB,
			// Token: 0x04000100 RID: 256
			CLEAR = 12,
			// Token: 0x04000101 RID: 257
			RETURN,
			// Token: 0x04000102 RID: 258
			SHIFT = 16,
			// Token: 0x04000103 RID: 259
			CONTROL,
			// Token: 0x04000104 RID: 260
			MENU,
			// Token: 0x04000105 RID: 261
			PAUSE,
			// Token: 0x04000106 RID: 262
			CAPITAL,
			// Token: 0x04000107 RID: 263
			KANA,
			// Token: 0x04000108 RID: 264
			HANGUL = 21,
			// Token: 0x04000109 RID: 265
			JUNJA = 23,
			// Token: 0x0400010A RID: 266
			FINAL,
			// Token: 0x0400010B RID: 267
			HANJA,
			// Token: 0x0400010C RID: 268
			KANJI = 25,
			// Token: 0x0400010D RID: 269
			ESCAPE = 27,
			// Token: 0x0400010E RID: 270
			CONVERT,
			// Token: 0x0400010F RID: 271
			NONCONVERT,
			// Token: 0x04000110 RID: 272
			ACCEPT,
			// Token: 0x04000111 RID: 273
			MODECHANGE,
			// Token: 0x04000112 RID: 274
			SPACE,
			// Token: 0x04000113 RID: 275
			PRIOR,
			// Token: 0x04000114 RID: 276
			NEXT,
			// Token: 0x04000115 RID: 277
			END,
			// Token: 0x04000116 RID: 278
			HOME,
			// Token: 0x04000117 RID: 279
			LEFT,
			// Token: 0x04000118 RID: 280
			UP,
			// Token: 0x04000119 RID: 281
			RIGHT,
			// Token: 0x0400011A RID: 282
			DOWN,
			// Token: 0x0400011B RID: 283
			SELECT,
			// Token: 0x0400011C RID: 284
			PRINT,
			// Token: 0x0400011D RID: 285
			EXECUTE,
			// Token: 0x0400011E RID: 286
			SNAPSHOT,
			// Token: 0x0400011F RID: 287
			INSERT,
			// Token: 0x04000120 RID: 288
			DELETE,
			// Token: 0x04000121 RID: 289
			HELP,
			// Token: 0x04000122 RID: 290
			KEY_0,
			// Token: 0x04000123 RID: 291
			KEY_1,
			// Token: 0x04000124 RID: 292
			KEY_2,
			// Token: 0x04000125 RID: 293
			KEY_3,
			// Token: 0x04000126 RID: 294
			KEY_4,
			// Token: 0x04000127 RID: 295
			KEY_5,
			// Token: 0x04000128 RID: 296
			KEY_6,
			// Token: 0x04000129 RID: 297
			KEY_7,
			// Token: 0x0400012A RID: 298
			KEY_8,
			// Token: 0x0400012B RID: 299
			KEY_9,
			// Token: 0x0400012C RID: 300
			KEY_A = 65,
			// Token: 0x0400012D RID: 301
			KEY_B,
			// Token: 0x0400012E RID: 302
			KEY_C,
			// Token: 0x0400012F RID: 303
			KEY_D,
			// Token: 0x04000130 RID: 304
			KEY_E,
			// Token: 0x04000131 RID: 305
			KEY_F,
			// Token: 0x04000132 RID: 306
			KEY_G,
			// Token: 0x04000133 RID: 307
			KEY_H,
			// Token: 0x04000134 RID: 308
			KEY_I,
			// Token: 0x04000135 RID: 309
			KEY_J,
			// Token: 0x04000136 RID: 310
			KEY_K,
			// Token: 0x04000137 RID: 311
			KEY_L,
			// Token: 0x04000138 RID: 312
			KEY_M,
			// Token: 0x04000139 RID: 313
			KEY_N,
			// Token: 0x0400013A RID: 314
			KEY_O,
			// Token: 0x0400013B RID: 315
			KEY_P,
			// Token: 0x0400013C RID: 316
			KEY_Q,
			// Token: 0x0400013D RID: 317
			KEY_R,
			// Token: 0x0400013E RID: 318
			KEY_S,
			// Token: 0x0400013F RID: 319
			KEY_T,
			// Token: 0x04000140 RID: 320
			KEY_U,
			// Token: 0x04000141 RID: 321
			KEY_V,
			// Token: 0x04000142 RID: 322
			KEY_W,
			// Token: 0x04000143 RID: 323
			KEY_X,
			// Token: 0x04000144 RID: 324
			KEY_Y,
			// Token: 0x04000145 RID: 325
			KEY_Z,
			// Token: 0x04000146 RID: 326
			LWIN,
			// Token: 0x04000147 RID: 327
			RWIN,
			// Token: 0x04000148 RID: 328
			APPS,
			// Token: 0x04000149 RID: 329
			SLEEP = 95,
			// Token: 0x0400014A RID: 330
			NUMPAD0,
			// Token: 0x0400014B RID: 331
			NUMPAD1,
			// Token: 0x0400014C RID: 332
			NUMPAD2,
			// Token: 0x0400014D RID: 333
			NUMPAD3,
			// Token: 0x0400014E RID: 334
			NUMPAD4,
			// Token: 0x0400014F RID: 335
			NUMPAD5,
			// Token: 0x04000150 RID: 336
			NUMPAD6,
			// Token: 0x04000151 RID: 337
			NUMPAD7,
			// Token: 0x04000152 RID: 338
			NUMPAD8,
			// Token: 0x04000153 RID: 339
			NUMPAD9,
			// Token: 0x04000154 RID: 340
			MULTIPLY,
			// Token: 0x04000155 RID: 341
			ADD,
			// Token: 0x04000156 RID: 342
			SEPARATOR,
			// Token: 0x04000157 RID: 343
			SUBTRACT,
			// Token: 0x04000158 RID: 344
			DECIMAL,
			// Token: 0x04000159 RID: 345
			DIVIDE,
			// Token: 0x0400015A RID: 346
			F1,
			// Token: 0x0400015B RID: 347
			F2,
			// Token: 0x0400015C RID: 348
			F3,
			// Token: 0x0400015D RID: 349
			F4,
			// Token: 0x0400015E RID: 350
			F5,
			// Token: 0x0400015F RID: 351
			F6,
			// Token: 0x04000160 RID: 352
			F7,
			// Token: 0x04000161 RID: 353
			F8,
			// Token: 0x04000162 RID: 354
			F9,
			// Token: 0x04000163 RID: 355
			F10,
			// Token: 0x04000164 RID: 356
			F11,
			// Token: 0x04000165 RID: 357
			F12,
			// Token: 0x04000166 RID: 358
			F13,
			// Token: 0x04000167 RID: 359
			F14,
			// Token: 0x04000168 RID: 360
			F15,
			// Token: 0x04000169 RID: 361
			F16,
			// Token: 0x0400016A RID: 362
			F17,
			// Token: 0x0400016B RID: 363
			F18,
			// Token: 0x0400016C RID: 364
			F19,
			// Token: 0x0400016D RID: 365
			F20,
			// Token: 0x0400016E RID: 366
			F21,
			// Token: 0x0400016F RID: 367
			F22,
			// Token: 0x04000170 RID: 368
			F23,
			// Token: 0x04000171 RID: 369
			F24,
			// Token: 0x04000172 RID: 370
			NUMLOCK = 144,
			// Token: 0x04000173 RID: 371
			SCROLL,
			// Token: 0x04000174 RID: 372
			LSHIFT = 160,
			// Token: 0x04000175 RID: 373
			RSHIFT,
			// Token: 0x04000176 RID: 374
			LCONTROL,
			// Token: 0x04000177 RID: 375
			RCONTROL,
			// Token: 0x04000178 RID: 376
			LMENU,
			// Token: 0x04000179 RID: 377
			RMENU,
			// Token: 0x0400017A RID: 378
			BROWSER_BACK,
			// Token: 0x0400017B RID: 379
			BROWSER_FORWARD,
			// Token: 0x0400017C RID: 380
			BROWSER_REFRESH,
			// Token: 0x0400017D RID: 381
			BROWSER_STOP,
			// Token: 0x0400017E RID: 382
			BROWSER_SEARCH,
			// Token: 0x0400017F RID: 383
			BROWSER_FAVORITES,
			// Token: 0x04000180 RID: 384
			BROWSER_HOME,
			// Token: 0x04000181 RID: 385
			VOLUME_MUTE,
			// Token: 0x04000182 RID: 386
			VOLUME_DOWN,
			// Token: 0x04000183 RID: 387
			VOLUME_UP,
			// Token: 0x04000184 RID: 388
			MEDIA_NEXT_TRACK,
			// Token: 0x04000185 RID: 389
			MEDIA_PREV_TRACK,
			// Token: 0x04000186 RID: 390
			MEDIA_STOP,
			// Token: 0x04000187 RID: 391
			MEDIA_PLAY_PAUSE,
			// Token: 0x04000188 RID: 392
			LAUNCH_MAIL,
			// Token: 0x04000189 RID: 393
			LAUNCH_MEDIA_SELECT,
			// Token: 0x0400018A RID: 394
			LAUNCH_APP1,
			// Token: 0x0400018B RID: 395
			LAUNCH_APP2,
			// Token: 0x0400018C RID: 396
			OEM_1 = 186,
			// Token: 0x0400018D RID: 397
			OEM_PLUS,
			// Token: 0x0400018E RID: 398
			OEM_COMMA,
			// Token: 0x0400018F RID: 399
			OEM_MINUS,
			// Token: 0x04000190 RID: 400
			OEM_PERIOD,
			// Token: 0x04000191 RID: 401
			OEM_2,
			// Token: 0x04000192 RID: 402
			OEM_3,
			// Token: 0x04000193 RID: 403
			OEM_4 = 219,
			// Token: 0x04000194 RID: 404
			OEM_5,
			// Token: 0x04000195 RID: 405
			OEM_6,
			// Token: 0x04000196 RID: 406
			OEM_7,
			// Token: 0x04000197 RID: 407
			OEM_8,
			// Token: 0x04000198 RID: 408
			OEM_102 = 226,
			// Token: 0x04000199 RID: 409
			PROCESSKEY = 229,
			// Token: 0x0400019A RID: 410
			PACKET = 231,
			// Token: 0x0400019B RID: 411
			ATTN = 246,
			// Token: 0x0400019C RID: 412
			CRSEL,
			// Token: 0x0400019D RID: 413
			EXSEL,
			// Token: 0x0400019E RID: 414
			EREOF,
			// Token: 0x0400019F RID: 415
			PLAY,
			// Token: 0x040001A0 RID: 416
			ZOOM,
			// Token: 0x040001A1 RID: 417
			NONAME,
			// Token: 0x040001A2 RID: 418
			PA1,
			// Token: 0x040001A3 RID: 419
			OEM_CLEAR
		}

		// Token: 0x02000011 RID: 17
		public enum KeyBoardScanCodes : short
		{
			// Token: 0x040001A5 RID: 421
			ESC = 1,
			// Token: 0x040001A6 RID: 422
			KEY_1,
			// Token: 0x040001A7 RID: 423
			KEY_2,
			// Token: 0x040001A8 RID: 424
			KEY_3,
			// Token: 0x040001A9 RID: 425
			KEY_4,
			// Token: 0x040001AA RID: 426
			KEY_5,
			// Token: 0x040001AB RID: 427
			KEY_6,
			// Token: 0x040001AC RID: 428
			KEY_7,
			// Token: 0x040001AD RID: 429
			KEY_8,
			// Token: 0x040001AE RID: 430
			KEY_9,
			// Token: 0x040001AF RID: 431
			KEY_0,
			// Token: 0x040001B0 RID: 432
			KEY_MINUS,
			// Token: 0x040001B1 RID: 433
			KEY_EQUAL,
			// Token: 0x040001B2 RID: 434
			KEY_BACKSPACE,
			// Token: 0x040001B3 RID: 435
			KEY_TAB,
			// Token: 0x040001B4 RID: 436
			KEY_Q,
			// Token: 0x040001B5 RID: 437
			KEY_W,
			// Token: 0x040001B6 RID: 438
			KEY_E,
			// Token: 0x040001B7 RID: 439
			KEY_R,
			// Token: 0x040001B8 RID: 440
			KEY_T,
			// Token: 0x040001B9 RID: 441
			KEY_Y,
			// Token: 0x040001BA RID: 442
			KEY_U,
			// Token: 0x040001BB RID: 443
			KEY_I,
			// Token: 0x040001BC RID: 444
			KEY_O,
			// Token: 0x040001BD RID: 445
			KEY_P,
			// Token: 0x040001BE RID: 446
			KEY_OPENING_BRACKETS,
			// Token: 0x040001BF RID: 447
			KEY_CLOSENING_BRACKETS,
			// Token: 0x040001C0 RID: 448
			KEY_ENTER,
			// Token: 0x040001C1 RID: 449
			KEY_CONTROL,
			// Token: 0x040001C2 RID: 450
			KEY_A,
			// Token: 0x040001C3 RID: 451
			KEY_S,
			// Token: 0x040001C4 RID: 452
			KEY_D,
			// Token: 0x040001C5 RID: 453
			KEY_F,
			// Token: 0x040001C6 RID: 454
			KEY_G,
			// Token: 0x040001C7 RID: 455
			KEY_H,
			// Token: 0x040001C8 RID: 456
			KEY_J,
			// Token: 0x040001C9 RID: 457
			KEY_K,
			// Token: 0x040001CA RID: 458
			KEY_L,
			// Token: 0x040001CB RID: 459
			KEY_NUMPAD0 = 82,
			// Token: 0x040001CC RID: 460
			KEY_NUMPAD1 = 79,
			// Token: 0x040001CD RID: 461
			KEY_NUMPAD2,
			// Token: 0x040001CE RID: 462
			KEY_NUMPAD3,
			// Token: 0x040001CF RID: 463
			KEY_NUMPAD4 = 75,
			// Token: 0x040001D0 RID: 464
			KEY_NUMPAD5,
			// Token: 0x040001D1 RID: 465
			KEY_NUMPAD6,
			// Token: 0x040001D2 RID: 466
			KEY_NUMPAD7 = 71,
			// Token: 0x040001D3 RID: 467
			KEY_NUMPAD8,
			// Token: 0x040001D4 RID: 468
			KEY_NUMPAD9
		}
	}
}
