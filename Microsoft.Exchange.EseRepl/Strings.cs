using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000040 RID: 64
	internal static class Strings
	{
		// Token: 0x06000223 RID: 547 RVA: 0x000087BC File Offset: 0x000069BC
		static Strings()
		{
			Strings.stringIDs.Add(1344658319U, "NetworkSecurityFailed");
			Strings.stringIDs.Add(726910307U, "NetworkNoUsableEndpoints");
			Strings.stringIDs.Add(3273471454U, "NetworkReadEOF");
			Strings.stringIDs.Add(1148446483U, "NetworkFailedToAuthServer");
			Strings.stringIDs.Add(3985107625U, "NetworkCancelled");
			Strings.stringIDs.Add(4232351636U, "NetworkIsDisabled");
			Strings.stringIDs.Add(3508567603U, "NetworkDataOverflowGeneric");
			Strings.stringIDs.Add(2364081162U, "NetworkCorruptDataGeneric");
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008898 File Offset: 0x00006A98
		public static LocalizedString NetworkConnectionTimeout(int waitInsecs)
		{
			return new LocalizedString("NetworkConnectionTimeout", Strings.ResourceManager, new object[]
			{
				waitInsecs
			});
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000088C8 File Offset: 0x00006AC8
		public static LocalizedString NetworkTimeoutError(string remoteNodeName, string errorText)
		{
			return new LocalizedString("NetworkTimeoutError", Strings.ResourceManager, new object[]
			{
				remoteNodeName,
				errorText
			});
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000088F4 File Offset: 0x00006AF4
		public static LocalizedString NetworkSecurityFailed
		{
			get
			{
				return new LocalizedString("NetworkSecurityFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000890C File Offset: 0x00006B0C
		public static LocalizedString NetworkAddressResolutionFailedNoDnsEntry(string nodeName)
		{
			return new LocalizedString("NetworkAddressResolutionFailedNoDnsEntry", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008934 File Offset: 0x00006B34
		public static LocalizedString NetworkNoUsableEndpoints
		{
			get
			{
				return new LocalizedString("NetworkNoUsableEndpoints", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000894B File Offset: 0x00006B4B
		public static LocalizedString NetworkReadEOF
		{
			get
			{
				return new LocalizedString("NetworkReadEOF", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008964 File Offset: 0x00006B64
		public static LocalizedString NetworkEndOfData(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkEndOfData", Strings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008990 File Offset: 0x00006B90
		public static LocalizedString NetworkNotUsable(string netName, string nodeName, string reason)
		{
			return new LocalizedString("NetworkNotUsable", Strings.ResourceManager, new object[]
			{
				netName,
				nodeName,
				reason
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000089C0 File Offset: 0x00006BC0
		public static LocalizedString NetworkTransportError(string err)
		{
			return new LocalizedString("NetworkTransportError", Strings.ResourceManager, new object[]
			{
				err
			});
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000089E8 File Offset: 0x00006BE8
		public static LocalizedString SourceDatabaseNotFound(Guid g, string sourceServer)
		{
			return new LocalizedString("SourceDatabaseNotFound", Strings.ResourceManager, new object[]
			{
				g,
				sourceServer
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008A1C File Offset: 0x00006C1C
		public static LocalizedString NetworkRemoteErrorUnknown(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkRemoteErrorUnknown", Strings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008A48 File Offset: 0x00006C48
		public static LocalizedString UnexpectedEOF(string filename)
		{
			return new LocalizedString("UnexpectedEOF", Strings.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008A70 File Offset: 0x00006C70
		public static LocalizedString NetworkNameNotFound(string netName)
		{
			return new LocalizedString("NetworkNameNotFound", Strings.ResourceManager, new object[]
			{
				netName
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008A98 File Offset: 0x00006C98
		public static LocalizedString DatabaseNotFound(Guid dbGuid)
		{
			return new LocalizedString("DatabaseNotFound", Strings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008AC5 File Offset: 0x00006CC5
		public static LocalizedString NetworkFailedToAuthServer
		{
			get
			{
				return new LocalizedString("NetworkFailedToAuthServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008ADC File Offset: 0x00006CDC
		public static LocalizedString NetworkCancelled
		{
			get
			{
				return new LocalizedString("NetworkCancelled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public static LocalizedString NetworkReadTimeout(int waitInsecs)
		{
			return new LocalizedString("NetworkReadTimeout", Strings.ResourceManager, new object[]
			{
				waitInsecs
			});
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008B21 File Offset: 0x00006D21
		public static LocalizedString NetworkIsDisabled
		{
			get
			{
				return new LocalizedString("NetworkIsDisabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008B38 File Offset: 0x00006D38
		public static LocalizedString SourceLogBreakStallsPassiveError(string sourceServerName, string error)
		{
			return new LocalizedString("SourceLogBreakStallsPassiveError", Strings.ResourceManager, new object[]
			{
				sourceServerName,
				error
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008B64 File Offset: 0x00006D64
		public static LocalizedString NetworkAddressResolutionFailed(string nodeName, string errMsg)
		{
			return new LocalizedString("NetworkAddressResolutionFailed", Strings.ResourceManager, new object[]
			{
				nodeName,
				errMsg
			});
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008B90 File Offset: 0x00006D90
		public static LocalizedString NetworkDataOverflowGeneric
		{
			get
			{
				return new LocalizedString("NetworkDataOverflowGeneric", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public static LocalizedString NetworkUnexpectedMessage(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkUnexpectedMessage", Strings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public static LocalizedString NetworkCommunicationError(string remoteNodeName, string errorText)
		{
			return new LocalizedString("NetworkCommunicationError", Strings.ResourceManager, new object[]
			{
				remoteNodeName,
				errorText
			});
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008C00 File Offset: 0x00006E00
		public static LocalizedString NetworkCorruptDataGeneric
		{
			get
			{
				return new LocalizedString("NetworkCorruptDataGeneric", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008C18 File Offset: 0x00006E18
		public static LocalizedString NetworkRemoteError(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkRemoteError", Strings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008C44 File Offset: 0x00006E44
		public static LocalizedString NetworkCorruptData(string srcNode)
		{
			return new LocalizedString("NetworkCorruptData", Strings.ResourceManager, new object[]
			{
				srcNode
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008C6C File Offset: 0x00006E6C
		public static LocalizedString FileIOonSourceException(string serverName, string fileFullPath, string ioErrorMessage)
		{
			return new LocalizedString("FileIOonSourceException", Strings.ResourceManager, new object[]
			{
				serverName,
				fileFullPath,
				ioErrorMessage
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008C9C File Offset: 0x00006E9C
		public static LocalizedString CorruptLogDetectedError(string filename, string errorText)
		{
			return new LocalizedString("CorruptLogDetectedError", Strings.ResourceManager, new object[]
			{
				filename,
				errorText
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008CC8 File Offset: 0x00006EC8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000138 RID: 312
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(8);

		// Token: 0x04000139 RID: 313
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.EseRepl.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000041 RID: 65
		public enum IDs : uint
		{
			// Token: 0x0400013B RID: 315
			NetworkSecurityFailed = 1344658319U,
			// Token: 0x0400013C RID: 316
			NetworkNoUsableEndpoints = 726910307U,
			// Token: 0x0400013D RID: 317
			NetworkReadEOF = 3273471454U,
			// Token: 0x0400013E RID: 318
			NetworkFailedToAuthServer = 1148446483U,
			// Token: 0x0400013F RID: 319
			NetworkCancelled = 3985107625U,
			// Token: 0x04000140 RID: 320
			NetworkIsDisabled = 4232351636U,
			// Token: 0x04000141 RID: 321
			NetworkDataOverflowGeneric = 3508567603U,
			// Token: 0x04000142 RID: 322
			NetworkCorruptDataGeneric = 2364081162U
		}

		// Token: 0x02000042 RID: 66
		private enum ParamIDs
		{
			// Token: 0x04000144 RID: 324
			NetworkConnectionTimeout,
			// Token: 0x04000145 RID: 325
			NetworkTimeoutError,
			// Token: 0x04000146 RID: 326
			NetworkAddressResolutionFailedNoDnsEntry,
			// Token: 0x04000147 RID: 327
			NetworkEndOfData,
			// Token: 0x04000148 RID: 328
			NetworkNotUsable,
			// Token: 0x04000149 RID: 329
			NetworkTransportError,
			// Token: 0x0400014A RID: 330
			SourceDatabaseNotFound,
			// Token: 0x0400014B RID: 331
			NetworkRemoteErrorUnknown,
			// Token: 0x0400014C RID: 332
			UnexpectedEOF,
			// Token: 0x0400014D RID: 333
			NetworkNameNotFound,
			// Token: 0x0400014E RID: 334
			DatabaseNotFound,
			// Token: 0x0400014F RID: 335
			NetworkReadTimeout,
			// Token: 0x04000150 RID: 336
			SourceLogBreakStallsPassiveError,
			// Token: 0x04000151 RID: 337
			NetworkAddressResolutionFailed,
			// Token: 0x04000152 RID: 338
			NetworkUnexpectedMessage,
			// Token: 0x04000153 RID: 339
			NetworkCommunicationError,
			// Token: 0x04000154 RID: 340
			NetworkRemoteError,
			// Token: 0x04000155 RID: 341
			NetworkCorruptData,
			// Token: 0x04000156 RID: 342
			FileIOonSourceException,
			// Token: 0x04000157 RID: 343
			CorruptLogDetectedError
		}
	}
}
