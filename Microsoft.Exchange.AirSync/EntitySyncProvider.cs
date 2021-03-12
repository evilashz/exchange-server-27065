using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000270 RID: 624
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EntitySyncProvider : MailboxSyncProvider
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x0008A8E3 File Offset: 0x00088AE3
		public EntitySyncProvider(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestFirst, bool trackConversations, bool allowTableRestrict) : this(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestFirst, trackConversations, allowTableRestrict, true)
		{
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0008A8F8 File Offset: 0x00088AF8
		public EntitySyncProvider(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestFirst, bool trackConversations, bool allowTableRestrict, bool disposeFolder) : base(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestFirst, trackConversations, allowTableRestrict, disposeFolder, AirSyncDiagnostics.GetSyncLogger())
		{
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0008A91B File Offset: 0x00088B1B
		protected EntitySyncProvider()
		{
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0008A923 File Offset: 0x00088B23
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x0008A92B File Offset: 0x00088B2B
		public ExDateTime WindowStart { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0008A934 File Offset: 0x00088B34
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x0008A93C File Offset: 0x00088B3C
		public ExDateTime WindowEnd { get; set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0008A945 File Offset: 0x00088B45
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x0008A94D File Offset: 0x00088B4D
		public AirSyncCalendarSyncState CalendarSyncState { get; set; }

		// Token: 0x06001744 RID: 5956 RVA: 0x0008A956 File Offset: 0x00088B56
		public override ISyncWatermark CreateNewWatermark()
		{
			base.CheckDisposed("CreateNewWatermark");
			return EntitySyncWatermark.Create();
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0008A970 File Offset: 0x00088B70
		public override bool GetNewOperations(ISyncWatermark minSyncWatermark, ISyncWatermark maxSyncWatermark, bool enumerateDeletes, int numOperations, QueryFilter filter, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			base.CheckDisposed("GetNewOperations");
			AirSyncDiagnostics.TraceInfo<int>(ExTraceGlobals.RequestsTracer, this, "EntitySyncProvider.GetNewOperations. numOperations = {0}", numOperations);
			if (newServerManifest == null)
			{
				throw new ArgumentNullException("newServerManifest");
			}
			if (!enumerateDeletes)
			{
				throw new NotImplementedException("enumerateDeletes is false!");
			}
			if (filter != null)
			{
				throw new NotImplementedException("filter is non-null! Filters are not supported on EntitySyncProvider");
			}
			SyncCalendar syncCalendar = new SyncCalendar(this.CalendarSyncState, base.Folder.Session, base.Folder.Id.ObjectId, (CalendarFolder folder) => EntitySyncProvider.PropertiesToSync, this.WindowStart, this.WindowEnd, false, numOperations);
			IFolderSyncState folderSyncState;
			IList<KeyValuePair<StoreId, LocalizedException>> list;
			SyncCalendarResponse syncCalendarResponse = syncCalendar.Execute(out folderSyncState, out list);
			AirSyncDiagnostics.TraceInfo<IFolderSyncState>(ExTraceGlobals.RequestsTracer, this, "newSyncState:{0}", folderSyncState);
			SyncCalendarFolderSyncState syncCalendarFolderSyncState = (SyncCalendarFolderSyncState)folderSyncState;
			this.CalendarSyncState = new AirSyncCalendarSyncState(syncCalendarFolderSyncState.SerializeAsBase64String(), syncCalendarResponse.QueryResumptionPoint, syncCalendarResponse.OldWindowEnd);
			if (list.Count > 0 && Command.CurrentCommand.MailboxLogger != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<StoreId, LocalizedException> keyValuePair in list)
				{
					stringBuilder.AppendFormat("Exception caught for item {0}\r\n{1}\r\n\r\n", keyValuePair.Key, keyValuePair.Value);
				}
				Command.CurrentCommand.MailboxLogger.SetData(MailboxLogDataName.CalendarSync_Exception, stringBuilder.ToString());
			}
			AirSyncDiagnostics.TraceInfo<int>(ExTraceGlobals.RequestsTracer, this, "DeletedItems:{0}", syncCalendarResponse.DeletedItems.Count);
			foreach (StoreId storeId in syncCalendarResponse.DeletedItems)
			{
				ISyncItemId syncItemId = EntitySyncItemId.CreateFromId(storeId);
				newServerManifest.Add(syncItemId, new ServerManifestEntry(ChangeType.Delete, syncItemId, null));
			}
			this.CopyListToDictionary(syncCalendarResponse.UpdatedItems, "UpdatedItems", newServerManifest);
			this.CopyListToDictionary(syncCalendarResponse.RecurrenceMastersWithInstances, "RecurrenceMastersWithInstances", newServerManifest);
			this.CopyListToDictionary(syncCalendarResponse.RecurrenceMastersWithoutInstances, "RecurrenceMastersWithoutInstances", newServerManifest);
			this.CopyListToDictionary(syncCalendarResponse.UnchangedRecurrenceMastersWithInstances, "UnchangedRecurrenceMastersWithInstances", newServerManifest);
			AirSyncDiagnostics.TraceInfo<bool>(ExTraceGlobals.RequestsTracer, this, "MoreAvailable:{0}", !syncCalendarResponse.IncludesLastItemInRange);
			return !syncCalendarResponse.IncludesLastItemInRange;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		public override OperationResult DeleteItems(params ISyncItemId[] syncIds)
		{
			base.CheckDisposed("DeleteItems");
			throw new NotImplementedException("EntitySyncProvider.DeleteItems");
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0008ABDB File Offset: 0x00088DDB
		public override ISyncItemId CreateISyncItemIdForNewItem(StoreObjectId itemId)
		{
			base.CheckDisposed("CreateISyncItemIdForNewItem");
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			return MailboxSyncItemId.CreateForNewItem(itemId);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0008ABFC File Offset: 0x00088DFC
		protected override ISyncItem GetItem(Item item)
		{
			return EntitySyncItem.Bind(item);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0008AC04 File Offset: 0x00088E04
		private void CopyListToDictionary(IList<SyncCalendarItemType> items, string listName, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			AirSyncDiagnostics.TraceInfo<string, int>(ExTraceGlobals.RequestsTracer, this, "{0}:{1}", listName, items.Count);
			foreach (SyncCalendarItemType syncCalendarItemType in items)
			{
				EntitySyncWatermark watermark = null;
				object obj;
				if (syncCalendarItemType.RowData != null && syncCalendarItemType.RowData.TryGetValue(ItemSchema.ArticleId, out obj) && !(obj is PropertyError))
				{
					watermark = EntitySyncWatermark.CreateWithChangeNumber((int)obj);
				}
				ISyncItemId syncItemId = EntitySyncItemId.CreateFromId(syncCalendarItemType.ItemId);
				ServerManifestEntry serverManifestEntry = new ServerManifestEntry(ChangeType.Add, syncItemId, watermark);
				serverManifestEntry.MessageClass = "IPM.APPOINTMENT";
				serverManifestEntry.CalendarItemType = syncCalendarItemType.CalendarItemType;
				OccurrenceStoreObjectId occurrenceStoreObjectId = StoreId.GetStoreObjectId(syncCalendarItemType.ItemId) as OccurrenceStoreObjectId;
				if (occurrenceStoreObjectId != null)
				{
					serverManifestEntry.SeriesMasterId = occurrenceStoreObjectId.GetMasterStoreObjectId();
				}
				newServerManifest.Add(syncItemId, serverManifestEntry);
			}
		}

		// Token: 0x04000E32 RID: 3634
		public static readonly PropertyDefinition[] PropertiesToSync = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ArticleId,
			CalendarItemBaseSchema.CalendarItemType
		};
	}
}
