using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000273 RID: 627
	internal static class SyncRequestWasHangingCache
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x0008ADDD File Offset: 0x00088FDD
		public static string BuildDiagnosticsString(string request, string cache, string lookup)
		{
			return string.Format("{0}-{1}-{2}", request, cache, lookup);
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0008ADEC File Offset: 0x00088FEC
		public static int Count
		{
			get
			{
				return SyncRequestWasHangingCache.syncRequests.Count;
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0008ADF8 File Offset: 0x00088FF8
		public static bool TryGet(Guid mailboxGuid, DeviceIdentity deviceIdentity, out bool value)
		{
			if (GlobalSettings.DisableCaching)
			{
				value = false;
				return false;
			}
			if (!SyncRequestWasHangingCache.syncRequests.TryGetValue(SyncRequestWasHangingCache.BuildKey(mailboxGuid, deviceIdentity), out value))
			{
				value = false;
				return false;
			}
			return true;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0008AE20 File Offset: 0x00089020
		public static void Set(Guid mailboxGuid, DeviceIdentity deviceIdentity, bool value)
		{
			SyncRequestWasHangingCache.syncRequests[SyncRequestWasHangingCache.BuildKey(mailboxGuid, deviceIdentity)] = value;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0008AE34 File Offset: 0x00089034
		public static bool Remove(Guid mailboxGuid, DeviceIdentity deviceIdentity)
		{
			return SyncRequestWasHangingCache.syncRequests.Remove(SyncRequestWasHangingCache.BuildKey(mailboxGuid, deviceIdentity));
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0008AE47 File Offset: 0x00089047
		internal static string BuildKey(Guid mailboxGuid, DeviceIdentity deviceIdentity)
		{
			return string.Format("{0}~{1}", mailboxGuid.ToString(), deviceIdentity);
		}

		// Token: 0x04000E37 RID: 3639
		public const string RequestHanging = "H";

		// Token: 0x04000E38 RID: 3640
		public const string RequestNonHanging = "N";

		// Token: 0x04000E39 RID: 3641
		public const string RequestEmpty = "E";

		// Token: 0x04000E3A RID: 3642
		public const string CacheHanging = "H";

		// Token: 0x04000E3B RID: 3643
		public const string CacheNonHanging = "N";

		// Token: 0x04000E3C RID: 3644
		public const string CacheMiss = "M";

		// Token: 0x04000E3D RID: 3645
		public const string CacheNotNecessary = "-";

		// Token: 0x04000E3E RID: 3646
		public const string LookupMissing = "M";

		// Token: 0x04000E3F RID: 3647
		public const string LookupHanging = "H";

		// Token: 0x04000E40 RID: 3648
		public const string LookupNonHanging = "N";

		// Token: 0x04000E41 RID: 3649
		public const string LookupNotNecessary = "-";

		// Token: 0x04000E42 RID: 3650
		private static MruDictionaryCache<string, bool> syncRequests = new MruDictionaryCache<string, bool>(GlobalSettings.HangingSyncHintCacheSize, (int)GlobalSettings.HangingSyncHintCacheTimeout.TotalMinutes);
	}
}
