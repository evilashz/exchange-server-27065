using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Mdb;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000016 RID: 22
	internal static class Strings
	{
		// Token: 0x060000EC RID: 236 RVA: 0x000080A4 File Offset: 0x000062A4
		static Strings()
		{
			Strings.stringIDs.Add(404224661U, "InterlockedCounterDisposed");
			Strings.stringIDs.Add(3118336813U, "InterlockedCounterTimeout");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008108 File Offset: 0x00006308
		public static LocalizedString FeedingSkipped(MdbInfo mdbInfo, ContentIndexStatusType state, IndexStatusErrorCode indexStatusErrorCode)
		{
			return new LocalizedString("FeedingSkipped", Strings.ResourceManager, new object[]
			{
				mdbInfo,
				state,
				indexStatusErrorCode
			});
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008144 File Offset: 0x00006344
		public static LocalizedString SearchFeedingControllerException(string databaseName, string exceptionDetails)
		{
			return new LocalizedString("SearchFeedingControllerException", Strings.ResourceManager, new object[]
			{
				databaseName,
				exceptionDetails
			});
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008170 File Offset: 0x00006370
		public static LocalizedString ReseedOnPassiveServer(string database)
		{
			return new LocalizedString("ReseedOnPassiveServer", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008198 File Offset: 0x00006398
		public static LocalizedString MissingNotifications(string database)
		{
			return new LocalizedString("MissingNotifications", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000081C0 File Offset: 0x000063C0
		public static LocalizedString InterlockedCounterDisposed
		{
			get
			{
				return new LocalizedString("InterlockedCounterDisposed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000081D7 File Offset: 0x000063D7
		public static LocalizedString InterlockedCounterTimeout
		{
			get
			{
				return new LocalizedString("InterlockedCounterTimeout", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000081F0 File Offset: 0x000063F0
		public static LocalizedString GracefulDegradationManagerException(string memoryUsageInfo, string catalogItemsInfo)
		{
			return new LocalizedString("GracefulDegradationManagerException", Strings.ResourceManager, new object[]
			{
				memoryUsageInfo,
				catalogItemsInfo
			});
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000821C File Offset: 0x0000641C
		public static LocalizedString FeedingSkippedWithFailureCode(MdbInfo mdbInfo, ContentIndexStatusType state, IndexStatusErrorCode indexStatusErrorCode, int? failureCode, string failureReason)
		{
			return new LocalizedString("FeedingSkippedWithFailureCode", Strings.ResourceManager, new object[]
			{
				mdbInfo,
				state,
				indexStatusErrorCode,
				failureCode,
				failureReason
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008264 File Offset: 0x00006464
		public static LocalizedString ReadLastEventFailure(string database, long lastEvent)
		{
			return new LocalizedString("ReadLastEventFailure", Strings.ResourceManager, new object[]
			{
				database,
				lastEvent
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008295 File Offset: 0x00006495
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400006F RID: 111
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000070 RID: 112
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Search.Engine.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000017 RID: 23
		public enum IDs : uint
		{
			// Token: 0x04000072 RID: 114
			InterlockedCounterDisposed = 404224661U,
			// Token: 0x04000073 RID: 115
			InterlockedCounterTimeout = 3118336813U
		}

		// Token: 0x02000018 RID: 24
		private enum ParamIDs
		{
			// Token: 0x04000075 RID: 117
			FeedingSkipped,
			// Token: 0x04000076 RID: 118
			SearchFeedingControllerException,
			// Token: 0x04000077 RID: 119
			ReseedOnPassiveServer,
			// Token: 0x04000078 RID: 120
			MissingNotifications,
			// Token: 0x04000079 RID: 121
			GracefulDegradationManagerException,
			// Token: 0x0400007A RID: 122
			FeedingSkippedWithFailureCode,
			// Token: 0x0400007B RID: 123
			ReadLastEventFailure
		}
	}
}
