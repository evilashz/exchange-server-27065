using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DiscoverWebProxyTask : BaseTask
	{
		// Token: 0x060001FC RID: 508 RVA: 0x000070A0 File Offset: 0x000052A0
		public DiscoverWebProxyTask(IContext context) : base(context, Strings.DiscoverWebProxyTaskTitle, Strings.DiscoverWebProxyTaskDescription, TaskType.Knowledge, new ContextProperty[0])
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000071E4 File Offset: 0x000053E4
		protected override IEnumerator<ITask> Process()
		{
			string destinationUrl = string.Format("{0}://{1}", RpcHelper.DetectShouldUseSsl(base.Get<RpcProxyPort>(DiscoverWebProxyTask.DestinationPort)) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp, base.Get<string>(DiscoverWebProxyTask.DestinationServer));
			try
			{
				base.Set<string>(DiscoverWebProxyTask.WebProxy, (DiscoverWebProxyTask.GetProxies(destinationUrl) ?? "<none>").Split(new char[]
				{
					';'
				})[0]);
				base.Result = TaskResult.Success;
				yield break;
			}
			catch (COMException ex)
			{
				base.Set<COMException>(BaseTask.Exception, ex);
				base.Set<string>(BaseTask.ErrorDetails, ((DiscoverWebProxyTask.WinHttp.ErrorCodes)ex.ErrorCode).ToString());
				base.Result = TaskResult.Failed;
				yield break;
			}
			yield break;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007200 File Offset: 0x00005400
		private static string GetProxies(string destinationUrl)
		{
			DiscoverWebProxyTask.WinHttp.WINHTTP_CURRENT_USER_IE_PROXY_CONFIG winhttp_CURRENT_USER_IE_PROXY_CONFIG = default(DiscoverWebProxyTask.WinHttp.WINHTTP_CURRENT_USER_IE_PROXY_CONFIG);
			DiscoverWebProxyTask.WinHttp.WINHTTP_AUTOPROXY_OPTIONS winhttp_AUTOPROXY_OPTIONS = new DiscoverWebProxyTask.WinHttp.WINHTTP_AUTOPROXY_OPTIONS
			{
				AutoLogonIfChallenged = false
			};
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (DiscoverWebProxyTask.WinHttp.WinHttpGetIEProxyConfigForCurrentUser(ref winhttp_CURRENT_USER_IE_PROXY_CONFIG))
				{
					if (winhttp_CURRENT_USER_IE_PROXY_CONFIG.AutoDetect)
					{
						winhttp_AUTOPROXY_OPTIONS.Flags = DiscoverWebProxyTask.WinHttp.AutoProxyFlags.AutoDetect;
						winhttp_AUTOPROXY_OPTIONS.AutoConfigUrl = null;
						winhttp_AUTOPROXY_OPTIONS.AutoDetectFlags = (DiscoverWebProxyTask.WinHttp.AutoDetectType.Dhcp | DiscoverWebProxyTask.WinHttp.AutoDetectType.DnsA);
					}
					else if (winhttp_CURRENT_USER_IE_PROXY_CONFIG.AutoConfigUrl != IntPtr.Zero)
					{
						winhttp_AUTOPROXY_OPTIONS.Flags = DiscoverWebProxyTask.WinHttp.AutoProxyFlags.AutoProxyConfigUrl;
						winhttp_AUTOPROXY_OPTIONS.AutoConfigUrl = Marshal.PtrToStringUni(winhttp_CURRENT_USER_IE_PROXY_CONFIG.AutoConfigUrl);
						winhttp_AUTOPROXY_OPTIONS.AutoDetectFlags = DiscoverWebProxyTask.WinHttp.AutoDetectType.None;
					}
				}
				else
				{
					Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeHGlobal(winhttp_CURRENT_USER_IE_PROXY_CONFIG.AutoConfigUrl);
				Marshal.FreeHGlobal(winhttp_CURRENT_USER_IE_PROXY_CONFIG.Proxy);
				Marshal.FreeHGlobal(winhttp_CURRENT_USER_IE_PROXY_CONFIG.ProxyBypass);
			}
			string result;
			using (DiscoverWebProxyTask.SafeInternetHandle safeInternetHandle = DiscoverWebProxyTask.WinHttp.WinHttpOpen("MSRPC", DiscoverWebProxyTask.WinHttp.AccessType.NoProxy, null, null, DiscoverWebProxyTask.WinHttp.SessionOpenFlags.Async))
			{
				string text;
				DiscoverWebProxyTask.WinHttpGetProxyForUrl(safeInternetHandle, destinationUrl, ref winhttp_AUTOPROXY_OPTIONS, out text);
				result = text;
			}
			return result;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007310 File Offset: 0x00005510
		private static void WinHttpGetProxyForUrl(DiscoverWebProxyTask.SafeInternetHandle session, string destination, ref DiscoverWebProxyTask.WinHttp.WINHTTP_AUTOPROXY_OPTIONS autoProxyOptions, out string proxyListString)
		{
			DiscoverWebProxyTask.WinHttp.WINHTTP_PROXY_INFO winhttp_PROXY_INFO = default(DiscoverWebProxyTask.WinHttp.WINHTTP_PROXY_INFO);
			proxyListString = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (DiscoverWebProxyTask.WinHttp.WinHttpGetProxyForUrl(session, destination, ref autoProxyOptions, out winhttp_PROXY_INFO))
				{
					proxyListString = ((winhttp_PROXY_INFO.AccessType == DiscoverWebProxyTask.WinHttp.AccessType.NamedProxy) ? Marshal.PtrToStringUni(winhttp_PROXY_INFO.Proxy) : null);
				}
				else
				{
					Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeHGlobal(winhttp_PROXY_INFO.Proxy);
				Marshal.FreeHGlobal(winhttp_PROXY_INFO.ProxyBypass);
			}
		}

		// Token: 0x040000EE RID: 238
		public static readonly ContextProperty<string> DestinationServer = ContextPropertySchema.RpcProxyServer.GetOnly();

		// Token: 0x040000EF RID: 239
		public static readonly ContextProperty<RpcProxyPort> DestinationPort = ContextPropertySchema.RpcProxyPort.GetOnly();

		// Token: 0x040000F0 RID: 240
		public static readonly ContextProperty<string> WebProxy = ContextPropertySchema.WebProxyServer.SetOnly();

		// Token: 0x02000054 RID: 84
		[SuppressUnmanagedCodeSecurity]
		private static class WinHttp
		{
			// Token: 0x06000201 RID: 513
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpCloseHandle(IntPtr httpSession);

			// Token: 0x06000202 RID: 514
			[DllImport("winhttp.dll", SetLastError = true)]
			internal static extern bool WinHttpGetIEProxyConfigForCurrentUser(ref DiscoverWebProxyTask.WinHttp.WINHTTP_CURRENT_USER_IE_PROXY_CONFIG proxyConfig);

			// Token: 0x06000203 RID: 515
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpGetProxyForUrl(DiscoverWebProxyTask.SafeInternetHandle session, string url, [In] ref DiscoverWebProxyTask.WinHttp.WINHTTP_AUTOPROXY_OPTIONS autoProxyOptions, out DiscoverWebProxyTask.WinHttp.WINHTTP_PROXY_INFO proxyInfo);

			// Token: 0x06000204 RID: 516
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern DiscoverWebProxyTask.SafeInternetHandle WinHttpOpen(string userAgent, DiscoverWebProxyTask.WinHttp.AccessType accessType, string proxyName, string proxyBypass, DiscoverWebProxyTask.WinHttp.SessionOpenFlags flags);

			// Token: 0x02000055 RID: 85
			public enum AccessType
			{
				// Token: 0x040000F2 RID: 242
				DefaultProxy,
				// Token: 0x040000F3 RID: 243
				NoProxy,
				// Token: 0x040000F4 RID: 244
				NamedProxy = 3
			}

			// Token: 0x02000056 RID: 86
			[Flags]
			public enum AutoDetectType
			{
				// Token: 0x040000F6 RID: 246
				None = 0,
				// Token: 0x040000F7 RID: 247
				Dhcp = 1,
				// Token: 0x040000F8 RID: 248
				DnsA = 2
			}

			// Token: 0x02000057 RID: 87
			[Flags]
			public enum AutoProxyFlags
			{
				// Token: 0x040000FA RID: 250
				AutoDetect = 1,
				// Token: 0x040000FB RID: 251
				AutoProxyConfigUrl = 2,
				// Token: 0x040000FC RID: 252
				RunInProcess = 65536,
				// Token: 0x040000FD RID: 253
				RunOutProcessOnly = 131072
			}

			// Token: 0x02000058 RID: 88
			[Flags]
			public enum SessionOpenFlags
			{
				// Token: 0x040000FF RID: 255
				Async = 268435456
			}

			// Token: 0x02000059 RID: 89
			public enum ErrorCodes
			{
				// Token: 0x04000101 RID: 257
				AudodetectionFailed = 12180,
				// Token: 0x04000102 RID: 258
				AuthCertNeeded = 12044,
				// Token: 0x04000103 RID: 259
				AutoProxyServiceError = 12178,
				// Token: 0x04000104 RID: 260
				BadAutoProxyScript = 12166,
				// Token: 0x04000105 RID: 261
				CannotCallAfterOpen = 12103,
				// Token: 0x04000106 RID: 262
				CannotCallAfterSend = 12102,
				// Token: 0x04000107 RID: 263
				CannotCallBeforeOpen = 12100,
				// Token: 0x04000108 RID: 264
				CannotCallBeforeSend,
				// Token: 0x04000109 RID: 265
				CannotConnect = 12029,
				// Token: 0x0400010A RID: 266
				ChunkedEncodingHeaderSizeOverflow = 12183,
				// Token: 0x0400010B RID: 267
				ClientCertNoAccessPrivateKey = 12186,
				// Token: 0x0400010C RID: 268
				ClientCertNoPrivateKey = 12185,
				// Token: 0x0400010D RID: 269
				ConnectionError = 12030,
				// Token: 0x0400010E RID: 270
				HeaderAlreadyExists = 12155,
				// Token: 0x0400010F RID: 271
				HeaderCountExceeded = 12181,
				// Token: 0x04000110 RID: 272
				HeaderNotFound = 12150,
				// Token: 0x04000111 RID: 273
				HeaderSizeOverflow = 12182,
				// Token: 0x04000112 RID: 274
				IncorrectHandleState = 12019,
				// Token: 0x04000113 RID: 275
				IncorrectHandleType = 12018,
				// Token: 0x04000114 RID: 276
				InternalError = 12004,
				// Token: 0x04000115 RID: 277
				InvalidHeader = 12153,
				// Token: 0x04000116 RID: 278
				InvalidOption = 12009,
				// Token: 0x04000117 RID: 279
				InvalidQueryRequest = 12154,
				// Token: 0x04000118 RID: 280
				InvalidServerResponse = 12152,
				// Token: 0x04000119 RID: 281
				InvalidUrl = 12005,
				// Token: 0x0400011A RID: 282
				LoginFailure = 12015,
				// Token: 0x0400011B RID: 283
				NameNotResolved = 12007,
				// Token: 0x0400011C RID: 284
				NotInitialized = 12172,
				// Token: 0x0400011D RID: 285
				OperationCancelled = 12017,
				// Token: 0x0400011E RID: 286
				OptionNotSettable = 12011,
				// Token: 0x0400011F RID: 287
				OutOfHandles = 12001,
				// Token: 0x04000120 RID: 288
				RedirectFailed = 12156,
				// Token: 0x04000121 RID: 289
				ResendRequest = 12032,
				// Token: 0x04000122 RID: 290
				ResponseDrainOverflow = 12184,
				// Token: 0x04000123 RID: 291
				SecureCertCNInvalid = 12038,
				// Token: 0x04000124 RID: 292
				SecureCertDateInvalid = 12037,
				// Token: 0x04000125 RID: 293
				SecureCertRevFailed = 12057,
				// Token: 0x04000126 RID: 294
				SecureCertRevoked = 12170,
				// Token: 0x04000127 RID: 295
				SecureCertWrongUsage = 12179,
				// Token: 0x04000128 RID: 296
				SecureChannelError = 12157,
				// Token: 0x04000129 RID: 297
				SecureFailure = 12175,
				// Token: 0x0400012A RID: 298
				SecureInvalidCA = 12045,
				// Token: 0x0400012B RID: 299
				SecureInvalidCert = 12169,
				// Token: 0x0400012C RID: 300
				Shutdown = 12012,
				// Token: 0x0400012D RID: 301
				Success = 0,
				// Token: 0x0400012E RID: 302
				Timeout = 12002,
				// Token: 0x0400012F RID: 303
				UnableToDownloadScript = 12167,
				// Token: 0x04000130 RID: 304
				UnrecognizedScheme = 12006
			}

			// Token: 0x0200005A RID: 90
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct WINHTTP_AUTOPROXY_OPTIONS
			{
				// Token: 0x04000131 RID: 305
				public DiscoverWebProxyTask.WinHttp.AutoProxyFlags Flags;

				// Token: 0x04000132 RID: 306
				public DiscoverWebProxyTask.WinHttp.AutoDetectType AutoDetectFlags;

				// Token: 0x04000133 RID: 307
				[MarshalAs(UnmanagedType.LPWStr)]
				public string AutoConfigUrl;

				// Token: 0x04000134 RID: 308
				public IntPtr Reserved1;

				// Token: 0x04000135 RID: 309
				public int Reserved2;

				// Token: 0x04000136 RID: 310
				public bool AutoLogonIfChallenged;
			}

			// Token: 0x0200005B RID: 91
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct WINHTTP_CURRENT_USER_IE_PROXY_CONFIG
			{
				// Token: 0x04000137 RID: 311
				public bool AutoDetect;

				// Token: 0x04000138 RID: 312
				public IntPtr AutoConfigUrl;

				// Token: 0x04000139 RID: 313
				public IntPtr Proxy;

				// Token: 0x0400013A RID: 314
				public IntPtr ProxyBypass;
			}

			// Token: 0x0200005C RID: 92
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct WINHTTP_PROXY_INFO
			{
				// Token: 0x0400013B RID: 315
				public DiscoverWebProxyTask.WinHttp.AccessType AccessType;

				// Token: 0x0400013C RID: 316
				public IntPtr Proxy;

				// Token: 0x0400013D RID: 317
				public IntPtr ProxyBypass;
			}
		}

		// Token: 0x0200005D RID: 93
		[SuppressUnmanagedCodeSecurity]
		private sealed class SafeInternetHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06000205 RID: 517 RVA: 0x000073BB File Offset: 0x000055BB
			public SafeInternetHandle() : base(true)
			{
			}

			// Token: 0x06000206 RID: 518 RVA: 0x000073C4 File Offset: 0x000055C4
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			protected override bool ReleaseHandle()
			{
				return DiscoverWebProxyTask.WinHttp.WinHttpCloseHandle(this.handle);
			}
		}
	}
}
