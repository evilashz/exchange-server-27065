using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000064 RID: 100
	internal static class Strings
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0001BE84 File Offset: 0x0001A084
		static Strings()
		{
			Strings.stringIDs.Add(1997125541U, "InvalidSyncPhase");
			Strings.stringIDs.Add(1732782849U, "InvalidSubscriptionGuid");
			Strings.stringIDs.Add(1067634768U, "InvalidSubscriptionType");
			Strings.stringIDs.Add(4237821923U, "SystemMailboxSessionNotAvailable");
			Strings.stringIDs.Add(1923038367U, "CacheTokenNotAvailable");
			Strings.stringIDs.Add(2820269659U, "InvalidUserLegacyDn");
			Strings.stringIDs.Add(2705969914U, "FailedSetMailboxSubscriptionListTimestamp");
			Strings.stringIDs.Add(2794107333U, "FailureToReadCacheData");
			Strings.stringIDs.Add(39652958U, "FailureToRebuildCacheData");
			Strings.stringIDs.Add(1804653958U, "FailedGetMailboxSubscriptionListTimestamp");
			Strings.stringIDs.Add(157386845U, "TransportSyncManagerServiceName");
			Strings.stringIDs.Add(2176604858U, "InvalidSubscriptionMessageId");
			Strings.stringIDs.Add(816076645U, "InvalidDisabledStatus");
			Strings.stringIDs.Add(1083474379U, "InvalidUserMailboxGuid");
			Strings.stringIDs.Add(4289146560U, "InvalidSubscription");
			Strings.stringIDs.Add(934451165U, "FailedToDeleteCacheData");
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001C000 File Offset: 0x0001A200
		public static LocalizedString InvalidSyncPhase
		{
			get
			{
				return new LocalizedString("InvalidSyncPhase", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001C020 File Offset: 0x0001A220
		public static LocalizedString CachePermanentExceptionInfo(Guid databaseGuid, Guid userMailboxGuid, string exceptionInfo)
		{
			return new LocalizedString("CachePermanentExceptionInfo", "Ex1F3285", false, true, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				userMailboxGuid,
				exceptionInfo
			});
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001C061 File Offset: 0x0001A261
		public static LocalizedString InvalidSubscriptionGuid
		{
			get
			{
				return new LocalizedString("InvalidSubscriptionGuid", "Ex490D64", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001C07F File Offset: 0x0001A27F
		public static LocalizedString InvalidSubscriptionType
		{
			get
			{
				return new LocalizedString("InvalidSubscriptionType", "Ex3BCC2D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001C09D File Offset: 0x0001A29D
		public static LocalizedString SystemMailboxSessionNotAvailable
		{
			get
			{
				return new LocalizedString("SystemMailboxSessionNotAvailable", "ExEDCD39", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001C0BB File Offset: 0x0001A2BB
		public static LocalizedString CacheTokenNotAvailable
		{
			get
			{
				return new LocalizedString("CacheTokenNotAvailable", "ExA96E36", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		public static LocalizedString MailboxNotFoundExceptionInfo(Guid databaseGuid, Guid userMailboxGuid, string exceptionInfo)
		{
			return new LocalizedString("MailboxNotFoundExceptionInfo", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				userMailboxGuid,
				exceptionInfo
			});
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001C11D File Offset: 0x0001A31D
		public static LocalizedString InvalidUserLegacyDn
		{
			get
			{
				return new LocalizedString("InvalidUserLegacyDn", "Ex3CAD93", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001C13B File Offset: 0x0001A33B
		public static LocalizedString FailedSetMailboxSubscriptionListTimestamp
		{
			get
			{
				return new LocalizedString("FailedSetMailboxSubscriptionListTimestamp", "Ex391E91", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001C159 File Offset: 0x0001A359
		public static LocalizedString FailureToReadCacheData
		{
			get
			{
				return new LocalizedString("FailureToReadCacheData", "Ex94881A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001C177 File Offset: 0x0001A377
		public static LocalizedString FailureToRebuildCacheData
		{
			get
			{
				return new LocalizedString("FailureToRebuildCacheData", "Ex58EC13", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001C195 File Offset: 0x0001A395
		public static LocalizedString FailedGetMailboxSubscriptionListTimestamp
		{
			get
			{
				return new LocalizedString("FailedGetMailboxSubscriptionListTimestamp", "Ex3209F2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001C1B3 File Offset: 0x0001A3B3
		public static LocalizedString TransportSyncManagerServiceName
		{
			get
			{
				return new LocalizedString("TransportSyncManagerServiceName", "Ex3EAED1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		public static LocalizedString CacheCorruptExceptionInfo(Guid databaseGuid, Guid userMailboxGuid, string exceptionInfo)
		{
			return new LocalizedString("CacheCorruptExceptionInfo", "ExB4657A", false, true, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				userMailboxGuid,
				exceptionInfo
			});
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001C215 File Offset: 0x0001A415
		public static LocalizedString InvalidSubscriptionMessageId
		{
			get
			{
				return new LocalizedString("InvalidSubscriptionMessageId", "Ex7AB2CE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001C233 File Offset: 0x0001A433
		public static LocalizedString InvalidDisabledStatus
		{
			get
			{
				return new LocalizedString("InvalidDisabledStatus", "Ex150D62", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001C254 File Offset: 0x0001A454
		public static LocalizedString CacheNotFoundExceptionInfo(Guid databaseGuid, Guid userMailboxGuid)
		{
			return new LocalizedString("CacheNotFoundExceptionInfo", "Ex4D8143", false, true, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				userMailboxGuid
			});
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001C291 File Offset: 0x0001A491
		public static LocalizedString InvalidUserMailboxGuid
		{
			get
			{
				return new LocalizedString("InvalidUserMailboxGuid", "Ex4612F2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001C2AF File Offset: 0x0001A4AF
		public static LocalizedString InvalidSubscription
		{
			get
			{
				return new LocalizedString("InvalidSubscription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001C2D0 File Offset: 0x0001A4D0
		public static LocalizedString CacheTransientExceptionInfo(Guid databaseGuid, Guid userMailboxGuid, string exceptionInfo)
		{
			return new LocalizedString("CacheTransientExceptionInfo", "Ex1CE4DE", false, true, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				userMailboxGuid,
				exceptionInfo
			});
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001C311 File Offset: 0x0001A511
		public static LocalizedString FailedToDeleteCacheData
		{
			get
			{
				return new LocalizedString("FailedToDeleteCacheData", "Ex382FFD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001C32F File Offset: 0x0001A52F
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400029C RID: 668
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(16);

		// Token: 0x0400029D RID: 669
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Sync.Manager.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000065 RID: 101
		public enum IDs : uint
		{
			// Token: 0x0400029F RID: 671
			InvalidSyncPhase = 1997125541U,
			// Token: 0x040002A0 RID: 672
			InvalidSubscriptionGuid = 1732782849U,
			// Token: 0x040002A1 RID: 673
			InvalidSubscriptionType = 1067634768U,
			// Token: 0x040002A2 RID: 674
			SystemMailboxSessionNotAvailable = 4237821923U,
			// Token: 0x040002A3 RID: 675
			CacheTokenNotAvailable = 1923038367U,
			// Token: 0x040002A4 RID: 676
			InvalidUserLegacyDn = 2820269659U,
			// Token: 0x040002A5 RID: 677
			FailedSetMailboxSubscriptionListTimestamp = 2705969914U,
			// Token: 0x040002A6 RID: 678
			FailureToReadCacheData = 2794107333U,
			// Token: 0x040002A7 RID: 679
			FailureToRebuildCacheData = 39652958U,
			// Token: 0x040002A8 RID: 680
			FailedGetMailboxSubscriptionListTimestamp = 1804653958U,
			// Token: 0x040002A9 RID: 681
			TransportSyncManagerServiceName = 157386845U,
			// Token: 0x040002AA RID: 682
			InvalidSubscriptionMessageId = 2176604858U,
			// Token: 0x040002AB RID: 683
			InvalidDisabledStatus = 816076645U,
			// Token: 0x040002AC RID: 684
			InvalidUserMailboxGuid = 1083474379U,
			// Token: 0x040002AD RID: 685
			InvalidSubscription = 4289146560U,
			// Token: 0x040002AE RID: 686
			FailedToDeleteCacheData = 934451165U
		}

		// Token: 0x02000066 RID: 102
		private enum ParamIDs
		{
			// Token: 0x040002B0 RID: 688
			CachePermanentExceptionInfo,
			// Token: 0x040002B1 RID: 689
			MailboxNotFoundExceptionInfo,
			// Token: 0x040002B2 RID: 690
			CacheCorruptExceptionInfo,
			// Token: 0x040002B3 RID: 691
			CacheNotFoundExceptionInfo,
			// Token: 0x040002B4 RID: 692
			CacheTransientExceptionInfo
		}
	}
}
