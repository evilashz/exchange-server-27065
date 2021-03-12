using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E26 RID: 3622
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class QueryBasedSyncFilter : ISyncFilter
	{
		// Token: 0x06007D64 RID: 32100 RVA: 0x00229816 File Offset: 0x00227A16
		public QueryBasedSyncFilter(QueryFilter filter, string stringId)
		{
			this.filter = filter;
			this.stringId = stringId;
			this.entriesInFilter = new Dictionary<ISyncItemId, ServerManifestEntry>();
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06007D65 RID: 32101 RVA: 0x00229837 File Offset: 0x00227A37
		public Dictionary<ISyncItemId, ServerManifestEntry> EntriesInFilter
		{
			get
			{
				return this.entriesInFilter;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06007D66 RID: 32102 RVA: 0x0022983F File Offset: 0x00227A3F
		public QueryFilter FilterQuery
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06007D67 RID: 32103 RVA: 0x00229847 File Offset: 0x00227A47
		public virtual string Id
		{
			get
			{
				if (this.stringId == null)
				{
					this.stringId = "QueryBasedSyncFilter: " + this.filter.ToString();
				}
				return this.stringId;
			}
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x00229872 File Offset: 0x00227A72
		public void InitializeAllItemsInFilter(ISyncProvider syncProvider)
		{
			this.entriesInFilter.Clear();
			syncProvider.GetNewOperations(syncProvider.CreateNewWatermark(), null, false, -1, this.filter, this.entriesInFilter);
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x0022989B File Offset: 0x00227A9B
		public virtual bool IsItemInFilter(ISyncItemId id)
		{
			return this.entriesInFilter.ContainsKey(id);
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x002298AC File Offset: 0x00227AAC
		public virtual void UpdateFilterState(SyncOperation syncOperation)
		{
			if (syncOperation.ChangeType == ChangeType.Delete)
			{
				this.entriesInFilter.Remove(syncOperation.Id);
				return;
			}
			try
			{
				try
				{
					bool flag = EvaluatableFilter.Evaluate(this.filter, syncOperation, true);
					if (flag)
					{
						ServerManifestEntry serverManifestEntry = new ServerManifestEntry(syncOperation.Id);
						serverManifestEntry.UpdateManifestFromPropertyBag(syncOperation);
						serverManifestEntry.FirstMessageInConversation = syncOperation.FirstMessageInConversation;
						this.entriesInFilter[syncOperation.Id] = serverManifestEntry;
					}
				}
				catch (PropertyErrorException)
				{
					ISyncItem item = syncOperation.GetItem(MailboxSyncProvider.QueryColumns);
					bool flag = item.IsItemInFilter(this.filter);
					if (flag)
					{
						ServerManifestEntry serverManifestEntry2 = new ServerManifestEntry(syncOperation.Id);
						serverManifestEntry2.UpdateManifestFromItem(item);
						this.entriesInFilter[syncOperation.Id] = serverManifestEntry2;
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
		}

		// Token: 0x04005582 RID: 21890
		private Dictionary<ISyncItemId, ServerManifestEntry> entriesInFilter;

		// Token: 0x04005583 RID: 21891
		private QueryFilter filter;

		// Token: 0x04005584 RID: 21892
		private string stringId;
	}
}
