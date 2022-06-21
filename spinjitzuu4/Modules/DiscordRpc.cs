using System;
using System.Runtime.InteropServices;

namespace spinjitzuu4.Modules
{
	// Token: 0x02000039 RID: 57
	public class DiscordRpc
	{
		// Token: 0x060001C7 RID: 455
		[DllImport("discord-rpc-w32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
		public static extern void Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

		// Token: 0x060001C8 RID: 456
		[DllImport("discord-rpc-w32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
		public static extern void RunCallbacks();

		// Token: 0x060001C9 RID: 457
		[DllImport("discord-rpc-w32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
		public static extern void Shutdown();

		// Token: 0x060001CA RID: 458
		[DllImport("discord-rpc-w32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
		public static extern void UpdatePresence(ref DiscordRpc.RichPresence presence);

		// Token: 0x060001CB RID: 459 RVA: 0x000032FD File Offset: 0x000014FD
		internal static void Initialize(string v1, ref object handlers, bool v2, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0200003A RID: 58
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public sealed class DisconnectedCallback : MulticastDelegate
		{
			// Token: 0x060001CE RID: 462
			public extern DisconnectedCallback(object @object, IntPtr method);

			// Token: 0x060001CF RID: 463
			public extern void Invoke(int errorCode, string message);

			// Token: 0x060001D0 RID: 464
			public extern IAsyncResult BeginInvoke(int errorCode, string message, AsyncCallback callback, object @object);

			// Token: 0x060001D1 RID: 465
			public extern void EndInvoke(IAsyncResult result);
		}

		// Token: 0x0200003B RID: 59
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public sealed class ErrorCallback : MulticastDelegate
		{
			// Token: 0x060001D3 RID: 467
			public extern ErrorCallback(object @object, IntPtr method);

			// Token: 0x060001D4 RID: 468
			public extern void Invoke(int errorCode, string message);

			// Token: 0x060001D5 RID: 469
			public extern IAsyncResult BeginInvoke(int errorCode, string message, AsyncCallback callback, object @object);

			// Token: 0x060001D6 RID: 470
			public extern void EndInvoke(IAsyncResult result);
		}

		// Token: 0x0200003C RID: 60
		public struct EventHandlers
		{
			// Token: 0x04000410 RID: 1040
			public DiscordRpc.ReadyCallback readyCallback;

			// Token: 0x04000411 RID: 1041
			public DiscordRpc.DisconnectedCallback disconnectedCallback;

			// Token: 0x04000412 RID: 1042
			public DiscordRpc.ErrorCallback errorCallback;
		}

		// Token: 0x0200003D RID: 61
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public sealed class ReadyCallback : MulticastDelegate
		{
			// Token: 0x060001D8 RID: 472
			public extern ReadyCallback(object @object, IntPtr method);

			// Token: 0x060001D9 RID: 473
			public extern void Invoke();

			// Token: 0x060001DA RID: 474
			public extern IAsyncResult BeginInvoke(AsyncCallback callback, object @object);

			// Token: 0x060001DB RID: 475
			public extern void EndInvoke(IAsyncResult result);
		}

		// Token: 0x0200003E RID: 62
		[Serializable]
		public struct RichPresence
		{
			// Token: 0x04000413 RID: 1043
			public string state;

			// Token: 0x04000414 RID: 1044
			public string details;

			// Token: 0x04000415 RID: 1045
			public long startTimestamp;

			// Token: 0x04000416 RID: 1046
			public long endTimestamp;

			// Token: 0x04000417 RID: 1047
			public string largeImageKey;

			// Token: 0x04000418 RID: 1048
			public string largeImageText;

			// Token: 0x04000419 RID: 1049
			public string smallImageKey;

			// Token: 0x0400041A RID: 1050
			public string smallImageText;

			// Token: 0x0400041B RID: 1051
			public string partyId;

			// Token: 0x0400041C RID: 1052
			public int partySize;

			// Token: 0x0400041D RID: 1053
			public int partyMax;

			// Token: 0x0400041E RID: 1054
			public string matchSecret;

			// Token: 0x0400041F RID: 1055
			public string joinSecret;

			// Token: 0x04000420 RID: 1056
			public string spectateSecret;

			// Token: 0x04000421 RID: 1057
			public bool instance;
		}
	}
}
