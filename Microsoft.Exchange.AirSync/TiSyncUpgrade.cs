using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000298 RID: 664
	internal class TiSyncUpgrade
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0008E5A8 File Offset: 0x0008C7A8
		public TiSyncUpgrade(string parentServerIdIn, MailboxSession mailboxSessionIn, SyncStateStorage syncStateStorageIn)
		{
			if (parentServerIdIn == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field parentServerIdIn");
				throw new ArgumentNullException("parentServerIdIn");
			}
			this.parentServerId = parentServerIdIn;
			this.mailboxSession = mailboxSessionIn;
			this.syncStateStorage = syncStateStorageIn;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008E5E4 File Offset: 0x0008C7E4
		public void UpdateMappingWithParent(string shortParentId)
		{
			try
			{
				this.syncState = this.syncStateStorage.GetFolderSyncState(new MailboxSyncProviderFactory(this.mailboxSession, this.storeId, true), this.parentServerId);
				ItemIdMapping itemIdMapping = (ItemIdMapping)this.syncState[CustomStateDatumType.IdMapping];
				itemIdMapping.UpdateParent(shortParentId);
				itemIdMapping.CommitChanges();
				this.syncState.Commit();
			}
			finally
			{
				if (this.syncState != null)
				{
					this.syncState.Dispose();
				}
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0008E670 File Offset: 0x0008C870
		public bool Upgrade(StoreObjectId storeId, Dictionary<string, CommonNode> items, Dictionary<string, string> mapping, List<TagNode> tags, StoreObjectType type, string synckeyIn, string shortParentId, int nextShortId, uint version)
		{
			if (storeId == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field storeId");
				throw new ArgumentNullException("storeId");
			}
			if (items == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field items");
				throw new ArgumentNullException("items");
			}
			if (mapping == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field mapping");
				throw new ArgumentNullException("mapping");
			}
			if (tags == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field tags");
				throw new ArgumentNullException("tags");
			}
			if (synckeyIn == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field synckeyIn");
				throw new ArgumentNullException("synckeyIn");
			}
			if (shortParentId == null && mapping.Count > 0)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.TiUpgradeTracer, this, "Input to TiSyncUpgrade.Upgrade is null: field shortParentId, and mapping.Count > 0");
				throw new ArgumentNullException("shortParentId");
			}
			Dictionary<StoreObjectId, string> dictionary = new Dictionary<StoreObjectId, string>();
			this.storeId = storeId;
			try
			{
				this.LoadE12SyncState(storeId, shortParentId, nextShortId);
				Folder sourceFolder = null;
				try
				{
					sourceFolder = Folder.Bind(this.mailboxSession, storeId, null);
				}
				catch (ObjectNotFoundException)
				{
					return false;
				}
				if (!TiSyncUpgrade.ParseSupportedTags((int)version, tags, type))
				{
					return false;
				}
				Dictionary<string, CommonNode> items2 = new Dictionary<string, CommonNode>(items);
				ItemSyncUpgrade itemSyncUpgrade = new ItemSyncUpgrade();
				dictionary = itemSyncUpgrade.Upgrade(this.syncState, items, sourceFolder, this.mailboxSession);
				if (dictionary == null)
				{
					return false;
				}
				DateTimeCustomSyncFilter dateTimeFilter = new DateTimeCustomSyncFilter(this.syncState);
				this.UpdateFilter(dictionary, dateTimeFilter, items2);
				this.UpdateMapping(dictionary, mapping);
				this.syncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data((int)version);
				((ItemIdMapping)this.syncState[CustomStateDatumType.IdMapping]).CommitChanges();
				string text = synckeyIn;
				int num = text.LastIndexOf("}");
				if (num >= 0)
				{
					text = text.Substring(num + 1, text.Length - num - 1);
				}
				uint data = (uint)int.Parse(text, CultureInfo.InvariantCulture);
				this.syncState[CustomStateDatumType.SyncKey] = new UInt32Data(data);
				this.syncState.Commit();
			}
			finally
			{
				if (this.syncState != null)
				{
					this.syncState.Dispose();
				}
			}
			return true;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008E8B4 File Offset: 0x0008CAB4
		private static bool ParseSupportedTags(int version, List<TagNode> tags, StoreObjectType type)
		{
			if (tags.Count == 0 || version < 21)
			{
				return true;
			}
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(tags.Count);
			foreach (TagNode tagNode in tags)
			{
				if (tagNode.NameSpace == 2)
				{
					tagNode.NameSpace = 1;
				}
				else
				{
					if (tagNode.NameSpace != 3)
					{
						return false;
					}
					tagNode.NameSpace = 4;
				}
				int tag = (int)tagNode.NameSpace << 8 | (int)(tagNode.Tag + 4);
				string name = TiSyncUpgrade.wbxmlSchema.GetName(tag);
				dictionary.Add(name, false);
			}
			IAirSyncVersionFactory airSyncVersionFactory = AirSyncProtocolVersionParserBuilder.FromVersion(version);
			AirSyncSchemaState airSyncSchemaState;
			switch (type)
			{
			case StoreObjectType.Folder:
				airSyncSchemaState = airSyncVersionFactory.CreateEmailSchema(null);
				break;
			case StoreObjectType.CalendarFolder:
				airSyncSchemaState = airSyncVersionFactory.CreateCalendarSchema();
				break;
			case StoreObjectType.ContactsFolder:
				airSyncSchemaState = airSyncVersionFactory.CreateContactsSchema();
				break;
			case StoreObjectType.TasksFolder:
				airSyncSchemaState = airSyncVersionFactory.CreateTasksSchema();
				break;
			default:
				return false;
			}
			AirSyncSetToDefaultStrategy airSyncSetToDefaultStrategy = new AirSyncSetToDefaultStrategy(dictionary);
			AirSyncDataObject airSyncDataObject = airSyncSchemaState.GetAirSyncDataObject(new Hashtable(), airSyncSetToDefaultStrategy);
			try
			{
				airSyncSetToDefaultStrategy.Validate(airSyncDataObject);
			}
			catch (ConversionException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008E9FC File Offset: 0x0008CBFC
		private void UpdateFilter(Dictionary<StoreObjectId, string> mapping, DateTimeCustomSyncFilter dateTimeFilter, Dictionary<string, CommonNode> items)
		{
			foreach (StoreObjectId storeObjectId in mapping.Keys)
			{
				string key = mapping[storeObjectId];
				CommonNode commonNode = items[key];
				ISyncItemId syncItemId = MailboxSyncItemId.CreateForNewItem(storeObjectId);
				if (commonNode.IsCalendar)
				{
					dateTimeFilter.UpdateFilterStateWithAddOrChange(syncItemId, true, true, commonNode.EndTime);
				}
				ChangeTrackingNode[] array;
				if (commonNode.IsEmail)
				{
					array = new ChangeTrackingNode[]
					{
						ChangeTrackingNode.AllOtherNodes,
						new ChangeTrackingNode("Email", "Read")
					};
				}
				else
				{
					array = new ChangeTrackingNode[]
					{
						ChangeTrackingNode.AllNodes
					};
				}
				GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderSync.ClientStateInformation> genericDictionaryData = (GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderSync.ClientStateInformation>)this.syncState[SyncStateProp.ClientState];
				if (!genericDictionaryData.Data.ContainsKey(syncItemId))
				{
					genericDictionaryData.Data[syncItemId] = new FolderSync.ClientStateInformation();
				}
				FolderSync.ClientStateInformation clientStateInformation = genericDictionaryData.Data[syncItemId];
				clientStateInformation.ChangeTrackingInformation = new int?[array.Length];
				clientStateInformation.ClientHasItem = true;
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0008EB28 File Offset: 0x0008CD28
		private void LoadE12SyncState(StoreObjectId storeId, string shortParentId, int lastSeen)
		{
			this.syncState = this.syncStateStorage.CreateFolderSyncState(new MailboxSyncProviderFactory(this.mailboxSession, storeId, true), this.parentServerId);
			this.syncState.CustomVersion = new int?(2);
			ItemIdMapping itemIdMapping = new ItemIdMapping(shortParentId);
			itemIdMapping.IncreaseCounterTo((long)(lastSeen + 1));
			this.syncState[CustomStateDatumType.IdMapping] = itemIdMapping;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008EB8C File Offset: 0x0008CD8C
		private void UpdateMapping(Dictionary<StoreObjectId, string> mappingFromStorageIds, Dictionary<string, string> mappingToSP2Id)
		{
			ItemIdMapping itemIdMapping = (ItemIdMapping)this.syncState[CustomStateDatumType.IdMapping];
			foreach (StoreObjectId storeObjectId in mappingFromStorageIds.Keys)
			{
				string key = mappingFromStorageIds[storeObjectId];
				string syncId = mappingToSP2Id[key];
				itemIdMapping.Add(MailboxSyncItemId.CreateForNewItem(storeObjectId), syncId);
			}
		}

		// Token: 0x04000EFA RID: 3834
		private static WbxmlSchema wbxmlSchema = new WbxmlSchema30();

		// Token: 0x04000EFB RID: 3835
		private MailboxSession mailboxSession;

		// Token: 0x04000EFC RID: 3836
		private SyncStateStorage syncStateStorage;

		// Token: 0x04000EFD RID: 3837
		private FolderSyncState syncState;

		// Token: 0x04000EFE RID: 3838
		private string parentServerId;

		// Token: 0x04000EFF RID: 3839
		private StoreObjectId storeId;
	}
}
