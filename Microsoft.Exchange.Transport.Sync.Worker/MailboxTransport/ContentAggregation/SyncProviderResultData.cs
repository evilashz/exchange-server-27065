using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200021B RID: 539
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncProviderResultData
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x0004158C File Offset: 0x0003F78C
		internal static SyncProviderResultData CreateAcknowledgeChangesResult(IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, int cloudItemsSynced, bool moreItemsAvailable)
		{
			return new SyncProviderResultData(changeList, hasPermanentSyncErrors, hasTransientSyncErrors, cloudItemsSynced, moreItemsAvailable, false);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004159A File Offset: 0x0003F79A
		internal SyncProviderResultData(IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors) : this(changeList, hasPermanentSyncErrors, hasTransientSyncErrors, 0, false, false)
		{
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000415A8 File Offset: 0x0003F7A8
		internal SyncProviderResultData(IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, bool hasNoChangesOnCloud) : this(changeList, hasPermanentSyncErrors, hasTransientSyncErrors, 0, false, hasNoChangesOnCloud)
		{
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000415B7 File Offset: 0x0003F7B7
		internal SyncProviderResultData(IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, int cloudItemsSynced, bool moreItemsAvailable) : this(changeList, hasPermanentSyncErrors, hasTransientSyncErrors, cloudItemsSynced, moreItemsAvailable, false)
		{
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000415C7 File Offset: 0x0003F7C7
		internal SyncProviderResultData(IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, bool moreItemsAvailable, bool hasNoChangesOnCloud) : this(changeList, hasPermanentSyncErrors, hasTransientSyncErrors, 0, moreItemsAvailable, hasNoChangesOnCloud)
		{
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000415D7 File Offset: 0x0003F7D7
		internal SyncProviderResultData(IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, int cloudItemsSynced, bool moreItemsAvailable, bool hasNoChangesOnCloud)
		{
			this.changeList = changeList;
			this.hasPermanentSyncErrors = hasPermanentSyncErrors;
			this.hasTransientSyncErrors = hasTransientSyncErrors;
			this.cloudItemsSynced = cloudItemsSynced;
			this.moreItemsAvailable = moreItemsAvailable;
			this.hasNoChangesOnCloud = hasNoChangesOnCloud;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0004160C File Offset: 0x0003F80C
		internal IList<SyncChangeEntry> ChangeList
		{
			get
			{
				return this.changeList;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00041614 File Offset: 0x0003F814
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x0004161C File Offset: 0x0003F81C
		internal bool HasPermanentSyncErrors
		{
			get
			{
				return this.hasPermanentSyncErrors;
			}
			set
			{
				this.hasPermanentSyncErrors = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00041625 File Offset: 0x0003F825
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x0004162D File Offset: 0x0003F82D
		internal bool HasTransientSyncErrors
		{
			get
			{
				return this.hasTransientSyncErrors;
			}
			set
			{
				this.hasTransientSyncErrors = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00041636 File Offset: 0x0003F836
		internal int CloudItemsSynced
		{
			get
			{
				return this.cloudItemsSynced;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0004163E File Offset: 0x0003F83E
		internal bool MoreItemsAvailable
		{
			get
			{
				return this.moreItemsAvailable;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00041646 File Offset: 0x0003F846
		internal bool HasNoChangesOnCloud
		{
			get
			{
				return this.hasNoChangesOnCloud;
			}
		}

		// Token: 0x04000A28 RID: 2600
		private readonly IList<SyncChangeEntry> changeList;

		// Token: 0x04000A29 RID: 2601
		private readonly int cloudItemsSynced;

		// Token: 0x04000A2A RID: 2602
		private readonly bool moreItemsAvailable;

		// Token: 0x04000A2B RID: 2603
		private readonly bool hasNoChangesOnCloud;

		// Token: 0x04000A2C RID: 2604
		private bool hasTransientSyncErrors;

		// Token: 0x04000A2D RID: 2605
		private bool hasPermanentSyncErrors;
	}
}
