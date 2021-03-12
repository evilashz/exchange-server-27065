using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMService.Exceptions
{
	// Token: 0x02000225 RID: 549
	internal static class Strings
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x0003A27C File Offset: 0x0003847C
		static Strings()
		{
			Strings.stringIDs.Add(254072865U, "WPFatalError");
			Strings.stringIDs.Add(2561408674U, "ConnectionManagerRestartFailure");
			Strings.stringIDs.Add(538835726U, "WorkerProcessStartTimeout");
			Strings.stringIDs.Add(519842198U, "WorkerProcessStartFailed");
			Strings.stringIDs.Add(3373645747U, "DialPlanObjectInvalid");
			Strings.stringIDs.Add(1549735078U, "ReadyThreadStartFailed");
			Strings.stringIDs.Add(214038602U, "FatalKeyFailed");
			Strings.stringIDs.Add(1422214618U, "ServiceName");
			Strings.stringIDs.Add(824705909U, "ResetKeyFailed");
			Strings.stringIDs.Add(4289272219U, "ReadyKeyFailed");
			Strings.stringIDs.Add(88115702U, "ResetThreadStartFailed");
			Strings.stringIDs.Add(2514021662U, "AssignProcessToJobObjectFailed");
			Strings.stringIDs.Add(3790914792U, "StopKeyFailed");
			Strings.stringIDs.Add(509941095U, "WorkerProcessManagerInitFailed");
			Strings.stringIDs.Add(3452399285U, "Server");
			Strings.stringIDs.Add(729817298U, "WorkerProcessRestartFailure");
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003A3F8 File Offset: 0x000385F8
		public static LocalizedString UMServiceHeartbeatException(string extraInfo)
		{
			return new LocalizedString("UMServiceHeartbeatException", Strings.ResourceManager, new object[]
			{
				extraInfo
			});
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003A420 File Offset: 0x00038620
		public static LocalizedString UMServiceSetJobObjectFailed(string name, string win32Message)
		{
			return new LocalizedString("UMServiceSetJobObjectFailed", Strings.ResourceManager, new object[]
			{
				name,
				win32Message
			});
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0003A44C File Offset: 0x0003864C
		public static LocalizedString WPFatalError
		{
			get
			{
				return new LocalizedString("WPFatalError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0003A463 File Offset: 0x00038663
		public static LocalizedString ConnectionManagerRestartFailure
		{
			get
			{
				return new LocalizedString("ConnectionManagerRestartFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003A47C File Offset: 0x0003867C
		public static LocalizedString UMServiceControlChannelException(int port, string errorMessage)
		{
			return new LocalizedString("UMServiceControlChannelException", Strings.ResourceManager, new object[]
			{
				port,
				errorMessage
			});
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003A4AD File Offset: 0x000386AD
		public static LocalizedString WorkerProcessStartTimeout
		{
			get
			{
				return new LocalizedString("WorkerProcessStartTimeout", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0003A4C4 File Offset: 0x000386C4
		public static LocalizedString WorkerProcessStartFailed
		{
			get
			{
				return new LocalizedString("WorkerProcessStartFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0003A4DC File Offset: 0x000386DC
		public static LocalizedString UMWorkerStartTimeoutException(int seconds)
		{
			return new LocalizedString("UMWorkerStartTimeoutException", Strings.ResourceManager, new object[]
			{
				seconds
			});
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0003A509 File Offset: 0x00038709
		public static LocalizedString DialPlanObjectInvalid
		{
			get
			{
				return new LocalizedString("DialPlanObjectInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003A520 File Offset: 0x00038720
		public static LocalizedString ReadyThreadStartFailed
		{
			get
			{
				return new LocalizedString("ReadyThreadStartFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0003A537 File Offset: 0x00038737
		public static LocalizedString FatalKeyFailed
		{
			get
			{
				return new LocalizedString("FatalKeyFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003A550 File Offset: 0x00038750
		public static LocalizedString InvalidWorkerProcessPath(string path)
		{
			return new LocalizedString("InvalidWorkerProcessPath", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0003A578 File Offset: 0x00038778
		public static LocalizedString ServiceName
		{
			get
			{
				return new LocalizedString("ServiceName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003A590 File Offset: 0x00038790
		public static LocalizedString UMServiceCreateJobObjectFailed(string name, string win32Message)
		{
			return new LocalizedString("UMServiceCreateJobObjectFailed", Strings.ResourceManager, new object[]
			{
				name,
				win32Message
			});
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0003A5BC File Offset: 0x000387BC
		public static LocalizedString ResetKeyFailed
		{
			get
			{
				return new LocalizedString("ResetKeyFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003A5D4 File Offset: 0x000387D4
		public static LocalizedString TLSEndPointInitializationFailure(string msg)
		{
			return new LocalizedString("TLSEndPointInitializationFailure", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003A5FC File Offset: 0x000387FC
		public static LocalizedString UMWorkerProcessStartFailed(string message)
		{
			return new LocalizedString("UMWorkerProcessStartFailed", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0003A624 File Offset: 0x00038824
		public static LocalizedString ReadyKeyFailed
		{
			get
			{
				return new LocalizedString("ReadyKeyFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003A63C File Offset: 0x0003883C
		public static LocalizedString UMWorkerProcessExceededMaxThrashCount(int count)
		{
			return new LocalizedString("UMWorkerProcessExceededMaxThrashCount", Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0003A669 File Offset: 0x00038869
		public static LocalizedString ResetThreadStartFailed
		{
			get
			{
				return new LocalizedString("ResetThreadStartFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003A680 File Offset: 0x00038880
		public static LocalizedString SipPortsUnavailable(int port1, int port2)
		{
			return new LocalizedString("SipPortsUnavailable", Strings.ResourceManager, new object[]
			{
				port1,
				port2
			});
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0003A6B6 File Offset: 0x000388B6
		public static LocalizedString AssignProcessToJobObjectFailed
		{
			get
			{
				return new LocalizedString("AssignProcessToJobObjectFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0003A6CD File Offset: 0x000388CD
		public static LocalizedString StopKeyFailed
		{
			get
			{
				return new LocalizedString("StopKeyFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003A6E4 File Offset: 0x000388E4
		public static LocalizedString UMInvalidWorkerProcessPath(string path)
		{
			return new LocalizedString("UMInvalidWorkerProcessPath", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003A70C File Offset: 0x0003890C
		public static LocalizedString FailedToCreateVoiceMailFilePath(string path)
		{
			return new LocalizedString("FailedToCreateVoiceMailFilePath", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0003A734 File Offset: 0x00038934
		public static LocalizedString WorkerProcessManagerInitFailed
		{
			get
			{
				return new LocalizedString("WorkerProcessManagerInitFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0003A74B File Offset: 0x0003894B
		public static LocalizedString Server
		{
			get
			{
				return new LocalizedString("Server", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0003A762 File Offset: 0x00038962
		public static LocalizedString WorkerProcessRestartFailure
		{
			get
			{
				return new LocalizedString("WorkerProcessRestartFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0003A779 File Offset: 0x00038979
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040008A5 RID: 2213
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(16);

		// Token: 0x040008A6 RID: 2214
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.UMService.Exceptions.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000226 RID: 550
		public enum IDs : uint
		{
			// Token: 0x040008A8 RID: 2216
			WPFatalError = 254072865U,
			// Token: 0x040008A9 RID: 2217
			ConnectionManagerRestartFailure = 2561408674U,
			// Token: 0x040008AA RID: 2218
			WorkerProcessStartTimeout = 538835726U,
			// Token: 0x040008AB RID: 2219
			WorkerProcessStartFailed = 519842198U,
			// Token: 0x040008AC RID: 2220
			DialPlanObjectInvalid = 3373645747U,
			// Token: 0x040008AD RID: 2221
			ReadyThreadStartFailed = 1549735078U,
			// Token: 0x040008AE RID: 2222
			FatalKeyFailed = 214038602U,
			// Token: 0x040008AF RID: 2223
			ServiceName = 1422214618U,
			// Token: 0x040008B0 RID: 2224
			ResetKeyFailed = 824705909U,
			// Token: 0x040008B1 RID: 2225
			ReadyKeyFailed = 4289272219U,
			// Token: 0x040008B2 RID: 2226
			ResetThreadStartFailed = 88115702U,
			// Token: 0x040008B3 RID: 2227
			AssignProcessToJobObjectFailed = 2514021662U,
			// Token: 0x040008B4 RID: 2228
			StopKeyFailed = 3790914792U,
			// Token: 0x040008B5 RID: 2229
			WorkerProcessManagerInitFailed = 509941095U,
			// Token: 0x040008B6 RID: 2230
			Server = 3452399285U,
			// Token: 0x040008B7 RID: 2231
			WorkerProcessRestartFailure = 729817298U
		}

		// Token: 0x02000227 RID: 551
		private enum ParamIDs
		{
			// Token: 0x040008B9 RID: 2233
			UMServiceHeartbeatException,
			// Token: 0x040008BA RID: 2234
			UMServiceSetJobObjectFailed,
			// Token: 0x040008BB RID: 2235
			UMServiceControlChannelException,
			// Token: 0x040008BC RID: 2236
			UMWorkerStartTimeoutException,
			// Token: 0x040008BD RID: 2237
			InvalidWorkerProcessPath,
			// Token: 0x040008BE RID: 2238
			UMServiceCreateJobObjectFailed,
			// Token: 0x040008BF RID: 2239
			TLSEndPointInitializationFailure,
			// Token: 0x040008C0 RID: 2240
			UMWorkerProcessStartFailed,
			// Token: 0x040008C1 RID: 2241
			UMWorkerProcessExceededMaxThrashCount,
			// Token: 0x040008C2 RID: 2242
			SipPortsUnavailable,
			// Token: 0x040008C3 RID: 2243
			UMInvalidWorkerProcessPath,
			// Token: 0x040008C4 RID: 2244
			FailedToCreateVoiceMailFilePath
		}
	}
}
