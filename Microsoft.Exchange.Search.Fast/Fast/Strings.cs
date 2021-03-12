using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200002A RID: 42
	internal static class Strings
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		static Strings()
		{
			Strings.stringIDs.Add(135840484U, "ConnectionFailure");
			Strings.stringIDs.Add(1417813431U, "PerformingFastOperationException");
			Strings.stringIDs.Add(3134189720U, "FailureToDetectFastInstallation");
			Strings.stringIDs.Add(4004591056U, "NoFASTNodesFound");
			Strings.stringIDs.Add(1948702154U, "UpdateConfigurationFailed");
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000EE53 File Offset: 0x0000D053
		public static LocalizedString ConnectionFailure
		{
			get
			{
				return new LocalizedString("ConnectionFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public static LocalizedString FailedToAcquireFolder(Guid mdbGuid, string folderName)
		{
			return new LocalizedString("FailedToAcquireFolder", Strings.ResourceManager, new object[]
			{
				mdbGuid,
				folderName
			});
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public static LocalizedString FailedToConnectToSystemMailbox(Guid mdbGuid)
		{
			return new LocalizedString("FailedToConnectToSystemMailbox", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000EECD File Offset: 0x0000D0CD
		public static LocalizedString PerformingFastOperationException
		{
			get
			{
				return new LocalizedString("PerformingFastOperationException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		public static LocalizedString FailedToCreateNewItem(Guid mdbGuid, Guid mailboxGuid)
		{
			return new LocalizedString("FailedToCreateNewItem", Strings.ResourceManager, new object[]
			{
				mdbGuid,
				mailboxGuid
			});
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000EF1A File Offset: 0x0000D11A
		public static LocalizedString FailureToDetectFastInstallation
		{
			get
			{
				return new LocalizedString("FailureToDetectFastInstallation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000EF34 File Offset: 0x0000D134
		public static LocalizedString FailedToReadRetriableTableEntry(Guid mdbGuid, string itemId)
		{
			return new LocalizedString("FailedToReadRetriableTableEntry", Strings.ResourceManager, new object[]
			{
				mdbGuid,
				itemId
			});
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000EF68 File Offset: 0x0000D168
		public static LocalizedString FailedToQueryPermanentFailures(Guid mdbGuid)
		{
			return new LocalizedString("FailedToQueryPermanentFailures", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000EF95 File Offset: 0x0000D195
		public static LocalizedString NoFASTNodesFound
		{
			get
			{
				return new LocalizedString("NoFASTNodesFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public static LocalizedString UpdateConfigurationFailed
		{
			get
			{
				return new LocalizedString("UpdateConfigurationFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public static LocalizedString FastCannotProcessDocument(string msg)
		{
			return new LocalizedString("FastCannotProcessDocument", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public static LocalizedString FailedToQueryRetriableItems(Guid mdbGuid)
		{
			return new LocalizedString("FailedToQueryRetriableItems", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000F01C File Offset: 0x0000D21C
		public static LocalizedString LostCallbackFailure(string msg)
		{
			return new LocalizedString("LostCallbackFailure", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000F044 File Offset: 0x0000D244
		public static LocalizedString DatabasePathDoesNotExist(string databasePath)
		{
			return new LocalizedString("DatabasePathDoesNotExist", Strings.ResourceManager, new object[]
			{
				databasePath
			});
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000F06C File Offset: 0x0000D26C
		public static LocalizedString FailedToQueryTable(Guid mdbGuid, string folderName)
		{
			return new LocalizedString("FailedToQueryTable", Strings.ResourceManager, new object[]
			{
				mdbGuid,
				folderName
			});
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		public static LocalizedString IndexSystemForFlowDoesNotExist(string flowName)
		{
			return new LocalizedString("IndexSystemForFlowDoesNotExist", Strings.ResourceManager, new object[]
			{
				flowName
			});
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400012F RID: 303
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(5);

		// Token: 0x04000130 RID: 304
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Search.Fast.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200002B RID: 43
		public enum IDs : uint
		{
			// Token: 0x04000132 RID: 306
			ConnectionFailure = 135840484U,
			// Token: 0x04000133 RID: 307
			PerformingFastOperationException = 1417813431U,
			// Token: 0x04000134 RID: 308
			FailureToDetectFastInstallation = 3134189720U,
			// Token: 0x04000135 RID: 309
			NoFASTNodesFound = 4004591056U,
			// Token: 0x04000136 RID: 310
			UpdateConfigurationFailed = 1948702154U
		}

		// Token: 0x0200002C RID: 44
		private enum ParamIDs
		{
			// Token: 0x04000138 RID: 312
			FailedToAcquireFolder,
			// Token: 0x04000139 RID: 313
			FailedToConnectToSystemMailbox,
			// Token: 0x0400013A RID: 314
			FailedToCreateNewItem,
			// Token: 0x0400013B RID: 315
			FailedToReadRetriableTableEntry,
			// Token: 0x0400013C RID: 316
			FailedToQueryPermanentFailures,
			// Token: 0x0400013D RID: 317
			FastCannotProcessDocument,
			// Token: 0x0400013E RID: 318
			FailedToQueryRetriableItems,
			// Token: 0x0400013F RID: 319
			LostCallbackFailure,
			// Token: 0x04000140 RID: 320
			DatabasePathDoesNotExist,
			// Token: 0x04000141 RID: 321
			FailedToQueryTable,
			// Token: 0x04000142 RID: 322
			IndexSystemForFlowDoesNotExist
		}
	}
}
