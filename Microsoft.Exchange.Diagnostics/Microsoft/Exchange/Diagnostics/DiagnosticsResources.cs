using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000411 RID: 1041
	internal static class DiagnosticsResources
	{
		// Token: 0x0600191E RID: 6430 RVA: 0x0005F12C File Offset: 0x0005D32C
		static DiagnosticsResources()
		{
			DiagnosticsResources.stringIDs.Add(3725486369U, "NullSourceName");
			DiagnosticsResources.stringIDs.Add(1209655894U, "BreadCrumbSize");
			DiagnosticsResources.stringIDs.Add(306844145U, "InvalidPrivilegeName");
			DiagnosticsResources.stringIDs.Add(3193246957U, "RequestDetailsLoggerWasDisposed");
			DiagnosticsResources.stringIDs.Add(780120066U, "UnauthorizedAccess");
			DiagnosticsResources.stringIDs.Add(1925645844U, "AppendColumnNullKey");
			DiagnosticsResources.stringIDs.Add(966025339U, "SourceAlreadyExists");
			DiagnosticsResources.stringIDs.Add(2224301001U, "WrongThread");
			DiagnosticsResources.stringIDs.Add(2131565734U, "DatacenterInvalidRegistryException");
			DiagnosticsResources.stringIDs.Add(4000883259U, "TypeNotSupported");
			DiagnosticsResources.stringIDs.Add(595440494U, "InvalidCharacterInLoggedText");
			DiagnosticsResources.stringIDs.Add(2482240435U, "ToomanyParams");
			DiagnosticsResources.stringIDs.Add(3949951791U, "ExceptionActivityContextEnumMetadataOnly");
			DiagnosticsResources.stringIDs.Add(2281395385U, "RevertPrivilege");
			DiagnosticsResources.stringIDs.Add(3384754759U, "InvalidSourceName");
			DiagnosticsResources.stringIDs.Add(18235917U, "ExcInvalidOpPropertyBeforeEnd");
			DiagnosticsResources.stringIDs.Add(3490062012U, "ExceptionActivityContextKeyCollision");
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0005F2BC File Offset: 0x0005D4BC
		public static LocalizedString ArgumentValueCannotBeParsed(string key, string value, string typeName)
		{
			return new LocalizedString("ArgumentValueCannotBeParsed", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				key,
				value,
				typeName
			});
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0005F2F4 File Offset: 0x0005D4F4
		public static LocalizedString ExceptionStartInvokedTwice(string debugInfo)
		{
			return new LocalizedString("ExceptionStartInvokedTwice", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				debugInfo
			});
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005F323 File Offset: 0x0005D523
		public static LocalizedString NullSourceName
		{
			get
			{
				return new LocalizedString("NullSourceName", "Ex8F1FD3", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0005F344 File Offset: 0x0005D544
		public static LocalizedString ExceptionWantedVersionButFileNotFound(string filename)
		{
			return new LocalizedString("ExceptionWantedVersionButFileNotFound", "ExAA26C5", false, true, DiagnosticsResources.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0005F373 File Offset: 0x0005D573
		public static LocalizedString BreadCrumbSize
		{
			get
			{
				return new LocalizedString("BreadCrumbSize", "Ex3A077B", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0005F391 File Offset: 0x0005D591
		public static LocalizedString InvalidPrivilegeName
		{
			get
			{
				return new LocalizedString("InvalidPrivilegeName", "Ex08AC8B", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0005F3AF File Offset: 0x0005D5AF
		public static LocalizedString RequestDetailsLoggerWasDisposed
		{
			get
			{
				return new LocalizedString("RequestDetailsLoggerWasDisposed", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0005F3CD File Offset: 0x0005D5CD
		public static LocalizedString UnauthorizedAccess
		{
			get
			{
				return new LocalizedString("UnauthorizedAccess", "Ex3E7948", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0005F3EB File Offset: 0x0005D5EB
		public static LocalizedString AppendColumnNullKey
		{
			get
			{
				return new LocalizedString("AppendColumnNullKey", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0005F40C File Offset: 0x0005D60C
		public static LocalizedString ExceptionSetupVersionInformationCorrupt(string keyPath)
		{
			return new LocalizedString("ExceptionSetupVersionInformationCorrupt", "ExC7F7D6", false, true, DiagnosticsResources.ResourceManager, new object[]
			{
				keyPath
			});
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0005F43B File Offset: 0x0005D63B
		public static LocalizedString SourceAlreadyExists
		{
			get
			{
				return new LocalizedString("SourceAlreadyExists", "ExA0479A", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0005F459 File Offset: 0x0005D659
		public static LocalizedString WrongThread
		{
			get
			{
				return new LocalizedString("WrongThread", "ExD42091", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0005F477 File Offset: 0x0005D677
		public static LocalizedString DatacenterInvalidRegistryException
		{
			get
			{
				return new LocalizedString("DatacenterInvalidRegistryException", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0005F495 File Offset: 0x0005D695
		public static LocalizedString TypeNotSupported
		{
			get
			{
				return new LocalizedString("TypeNotSupported", "ExC7DC8A", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0005F4B3 File Offset: 0x0005D6B3
		public static LocalizedString InvalidCharacterInLoggedText
		{
			get
			{
				return new LocalizedString("InvalidCharacterInLoggedText", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x0005F4D1 File Offset: 0x0005D6D1
		public static LocalizedString ToomanyParams
		{
			get
			{
				return new LocalizedString("ToomanyParams", "ExB54861", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0005F4EF File Offset: 0x0005D6EF
		public static LocalizedString ExceptionActivityContextEnumMetadataOnly
		{
			get
			{
				return new LocalizedString("ExceptionActivityContextEnumMetadataOnly", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0005F510 File Offset: 0x0005D710
		public static LocalizedString ExceptionMshSetupInformationCorrupt(string keyPath)
		{
			return new LocalizedString("ExceptionMshSetupInformationCorrupt", "ExABC22E", false, true, DiagnosticsResources.ResourceManager, new object[]
			{
				keyPath
			});
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0005F540 File Offset: 0x0005D740
		public static LocalizedString ExceptionMustStartBeforeSuspend(string debugInfo)
		{
			return new LocalizedString("ExceptionMustStartBeforeSuspend", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				debugInfo
			});
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0005F56F File Offset: 0x0005D76F
		public static LocalizedString RevertPrivilege
		{
			get
			{
				return new LocalizedString("RevertPrivilege", "Ex23E94D", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0005F590 File Offset: 0x0005D790
		public static LocalizedString ExceptionMustStartBeforeEnd(string debugInfo)
		{
			return new LocalizedString("ExceptionMustStartBeforeEnd", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				debugInfo
			});
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
		public static LocalizedString ArgumentNotSupported(string argumentName, string supportedArguments)
		{
			return new LocalizedString("ArgumentNotSupported", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				argumentName,
				supportedArguments
			});
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0005F5F3 File Offset: 0x0005D7F3
		public static LocalizedString InvalidSourceName
		{
			get
			{
				return new LocalizedString("InvalidSourceName", "Ex1016A8", false, true, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0005F614 File Offset: 0x0005D814
		public static LocalizedString ExceptionFileVersionNotFound(string filename)
		{
			return new LocalizedString("ExceptionFileVersionNotFound", "ExD1B2B2", false, true, DiagnosticsResources.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0005F644 File Offset: 0x0005D844
		public static LocalizedString ExceptionScopeAlreadyExists(string debugInfo)
		{
			return new LocalizedString("ExceptionScopeAlreadyExists", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				debugInfo
			});
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0005F674 File Offset: 0x0005D874
		public static LocalizedString ExceptionOutOfScope(string debugInfo)
		{
			return new LocalizedString("ExceptionOutOfScope", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				debugInfo
			});
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x0005F6A3 File Offset: 0x0005D8A3
		public static LocalizedString ExcInvalidOpPropertyBeforeEnd
		{
			get
			{
				return new LocalizedString("ExcInvalidOpPropertyBeforeEnd", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0005F6C4 File Offset: 0x0005D8C4
		public static LocalizedString ArgumentDuplicated(string msg)
		{
			return new LocalizedString("ArgumentDuplicated", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0005F6F4 File Offset: 0x0005D8F4
		public static LocalizedString ExceptionActivityContextMustBeCleared(string debugInfo)
		{
			return new LocalizedString("ExceptionActivityContextMustBeCleared", "", false, false, DiagnosticsResources.ResourceManager, new object[]
			{
				debugInfo
			});
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0005F723 File Offset: 0x0005D923
		public static LocalizedString ExceptionActivityContextKeyCollision
		{
			get
			{
				return new LocalizedString("ExceptionActivityContextKeyCollision", "", false, false, DiagnosticsResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0005F741 File Offset: 0x0005D941
		public static LocalizedString GetLocalizedString(DiagnosticsResources.IDs key)
		{
			return new LocalizedString(DiagnosticsResources.stringIDs[(uint)key], DiagnosticsResources.ResourceManager, new object[0]);
		}

		// Token: 0x04001DC8 RID: 7624
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(17);

		// Token: 0x04001DC9 RID: 7625
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Diagnostics.DiagnosticsResources", typeof(DiagnosticsResources).GetTypeInfo().Assembly);

		// Token: 0x02000412 RID: 1042
		public enum IDs : uint
		{
			// Token: 0x04001DCB RID: 7627
			NullSourceName = 3725486369U,
			// Token: 0x04001DCC RID: 7628
			BreadCrumbSize = 1209655894U,
			// Token: 0x04001DCD RID: 7629
			InvalidPrivilegeName = 306844145U,
			// Token: 0x04001DCE RID: 7630
			RequestDetailsLoggerWasDisposed = 3193246957U,
			// Token: 0x04001DCF RID: 7631
			UnauthorizedAccess = 780120066U,
			// Token: 0x04001DD0 RID: 7632
			AppendColumnNullKey = 1925645844U,
			// Token: 0x04001DD1 RID: 7633
			SourceAlreadyExists = 966025339U,
			// Token: 0x04001DD2 RID: 7634
			WrongThread = 2224301001U,
			// Token: 0x04001DD3 RID: 7635
			DatacenterInvalidRegistryException = 2131565734U,
			// Token: 0x04001DD4 RID: 7636
			TypeNotSupported = 4000883259U,
			// Token: 0x04001DD5 RID: 7637
			InvalidCharacterInLoggedText = 595440494U,
			// Token: 0x04001DD6 RID: 7638
			ToomanyParams = 2482240435U,
			// Token: 0x04001DD7 RID: 7639
			ExceptionActivityContextEnumMetadataOnly = 3949951791U,
			// Token: 0x04001DD8 RID: 7640
			RevertPrivilege = 2281395385U,
			// Token: 0x04001DD9 RID: 7641
			InvalidSourceName = 3384754759U,
			// Token: 0x04001DDA RID: 7642
			ExcInvalidOpPropertyBeforeEnd = 18235917U,
			// Token: 0x04001DDB RID: 7643
			ExceptionActivityContextKeyCollision = 3490062012U
		}

		// Token: 0x02000413 RID: 1043
		private enum ParamIDs
		{
			// Token: 0x04001DDD RID: 7645
			ArgumentValueCannotBeParsed,
			// Token: 0x04001DDE RID: 7646
			ExceptionStartInvokedTwice,
			// Token: 0x04001DDF RID: 7647
			ExceptionWantedVersionButFileNotFound,
			// Token: 0x04001DE0 RID: 7648
			ExceptionSetupVersionInformationCorrupt,
			// Token: 0x04001DE1 RID: 7649
			ExceptionMshSetupInformationCorrupt,
			// Token: 0x04001DE2 RID: 7650
			ExceptionMustStartBeforeSuspend,
			// Token: 0x04001DE3 RID: 7651
			ExceptionMustStartBeforeEnd,
			// Token: 0x04001DE4 RID: 7652
			ArgumentNotSupported,
			// Token: 0x04001DE5 RID: 7653
			ExceptionFileVersionNotFound,
			// Token: 0x04001DE6 RID: 7654
			ExceptionScopeAlreadyExists,
			// Token: 0x04001DE7 RID: 7655
			ExceptionOutOfScope,
			// Token: 0x04001DE8 RID: 7656
			ArgumentDuplicated,
			// Token: 0x04001DE9 RID: 7657
			ExceptionActivityContextMustBeCleared
		}
	}
}
