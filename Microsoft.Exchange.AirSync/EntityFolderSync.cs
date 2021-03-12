using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000254 RID: 596
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EntityFolderSync : FolderSync
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x0008087A File Offset: 0x0007EA7A
		public EntityFolderSync(ISyncProvider syncProvider, IFolderSyncState syncState, ConflictResolutionPolicy policy, bool deferStateModifications) : base(syncProvider, syncState, policy, deferStateModifications)
		{
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00080887 File Offset: 0x0007EA87
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x000808B6 File Offset: 0x0007EAB6
		internal AirSyncCalendarSyncState AirSyncCalendarSyncState
		{
			get
			{
				if (!this.syncState.Contains(CustomStateDatumType.CalendarSyncState))
				{
					return AirSyncCalendarSyncState.Empty;
				}
				return (AirSyncCalendarSyncState)this.syncState[CustomStateDatumType.CalendarSyncState];
			}
			set
			{
				this.syncState[CustomStateDatumType.CalendarSyncState] = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000808C9 File Offset: 0x0007EAC9
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x000808F8 File Offset: 0x0007EAF8
		internal AirSyncCalendarSyncState AirSyncRecoveryCalendarSyncState
		{
			get
			{
				if (!this.syncState.Contains(CustomStateDatumType.RecoveryCalendarSyncState))
				{
					return AirSyncCalendarSyncState.Empty;
				}
				return (AirSyncCalendarSyncState)this.syncState[CustomStateDatumType.RecoveryCalendarSyncState];
			}
			set
			{
				this.syncState[CustomStateDatumType.RecoveryCalendarSyncState] = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0008090B File Offset: 0x0007EB0B
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0008093F File Offset: 0x0007EB3F
		internal Dictionary<ISyncItemId, EntityFolderSync.OcurrenceInformation> MasterItems
		{
			get
			{
				if (!this.syncState.Contains(CustomStateDatumType.CalendarMasterItems))
				{
					return new Dictionary<ISyncItemId, EntityFolderSync.OcurrenceInformation>();
				}
				return ((GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, EntityFolderSync.OcurrenceInformation>)this.syncState[CustomStateDatumType.CalendarMasterItems]).Data;
			}
			set
			{
				this.syncState[CustomStateDatumType.CalendarMasterItems] = new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, EntityFolderSync.OcurrenceInformation>(value);
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00080957 File Offset: 0x0007EB57
		public override void Recover(ISyncClientOperation[] clientOperations)
		{
			base.Recover(clientOperations);
			this.AirSyncCalendarSyncState = this.AirSyncRecoveryCalendarSyncState;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0008096C File Offset: 0x0007EB6C
		protected override void SavePreviousState()
		{
			base.SavePreviousState();
			this.AirSyncRecoveryCalendarSyncState = this.AirSyncCalendarSyncState;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00080980 File Offset: 0x0007EB80
		protected override bool GetNewOperations(int windowSize, Dictionary<ISyncItemId, ServerManifestEntry> tempServerManifest)
		{
			bool newOperations = base.GetNewOperations(windowSize, tempServerManifest);
			Dictionary<ISyncItemId, EntityFolderSync.OcurrenceInformation> masterItems = this.MasterItems;
			Dictionary<ISyncItemId, ServerManifestEntry> dictionary = new Dictionary<ISyncItemId, ServerManifestEntry>();
			foreach (ServerManifestEntry serverManifestEntry in tempServerManifest.Values)
			{
				if (serverManifestEntry.ChangeType == ChangeType.Delete)
				{
					if (masterItems.ContainsKey(serverManifestEntry.Id))
					{
						EntityFolderSync.OcurrenceInformation ocurrenceInformation = masterItems[serverManifestEntry.Id];
						foreach (ISyncItemId syncItemId in ocurrenceInformation.Ocurrences)
						{
							dictionary.Add(syncItemId, new ServerManifestEntry(ChangeType.Delete, syncItemId, null));
						}
						masterItems.Remove(serverManifestEntry.Id);
					}
				}
				else if (serverManifestEntry.ChangeType == ChangeType.Add)
				{
					if (serverManifestEntry.CalendarItemType == CalendarItemType.RecurringMaster)
					{
						if (masterItems.ContainsKey(serverManifestEntry.Id))
						{
							EntityFolderSync.OcurrenceInformation ocurrenceInformation = masterItems[serverManifestEntry.Id];
							for (int i = ocurrenceInformation.Ocurrences.Count - 1; i >= 0; i--)
							{
								if (!tempServerManifest.ContainsKey(ocurrenceInformation.Ocurrences[i]))
								{
									dictionary.Add(ocurrenceInformation.Ocurrences[i], new ServerManifestEntry(ChangeType.Delete, ocurrenceInformation.Ocurrences[i], null));
									ocurrenceInformation.Ocurrences.RemoveAt(i);
								}
							}
						}
					}
					else if (serverManifestEntry.CalendarItemType == CalendarItemType.Occurrence || serverManifestEntry.CalendarItemType == CalendarItemType.Exception)
					{
						EntitySyncItemId key = EntitySyncItemId.CreateFromId(serverManifestEntry.SeriesMasterId);
						EntityFolderSync.OcurrenceInformation ocurrenceInformation;
						if (masterItems.ContainsKey(key))
						{
							ocurrenceInformation = masterItems[key];
						}
						else
						{
							ocurrenceInformation = new EntityFolderSync.OcurrenceInformation();
							masterItems[key] = ocurrenceInformation;
						}
						if (!ocurrenceInformation.Ocurrences.Contains(serverManifestEntry.Id))
						{
							ocurrenceInformation.Ocurrences.Add(serverManifestEntry.Id);
						}
					}
				}
			}
			foreach (ServerManifestEntry serverManifestEntry2 in dictionary.Values)
			{
				if (!tempServerManifest.ContainsKey(serverManifestEntry2.Id))
				{
					tempServerManifest.Add(serverManifestEntry2.Id, serverManifestEntry2);
				}
			}
			this.MasterItems = masterItems;
			return newOperations;
		}

		// Token: 0x02000255 RID: 597
		public sealed class OcurrenceInformation : ICustomSerializable
		{
			// Token: 0x060015B9 RID: 5561 RVA: 0x00080C0C File Offset: 0x0007EE0C
			public OcurrenceInformation()
			{
				this.Ocurrences = new List<ISyncItemId>();
			}

			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x060015BA RID: 5562 RVA: 0x00080C1F File Offset: 0x0007EE1F
			// (set) Token: 0x060015BB RID: 5563 RVA: 0x00080C27 File Offset: 0x0007EE27
			public List<ISyncItemId> Ocurrences { get; set; }

			// Token: 0x060015BC RID: 5564 RVA: 0x00080C30 File Offset: 0x0007EE30
			public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
			{
				GenericListData<DerivedData<ISyncItemId>, ISyncItemId> genericListData = new GenericListData<DerivedData<ISyncItemId>, ISyncItemId>();
				genericListData.DeserializeData(reader, componentDataPool);
				this.Ocurrences = genericListData.Data;
			}

			// Token: 0x060015BD RID: 5565 RVA: 0x00080C58 File Offset: 0x0007EE58
			public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
			{
				GenericListData<DerivedData<ISyncItemId>, ISyncItemId> genericListData = new GenericListData<DerivedData<ISyncItemId>, ISyncItemId>(this.Ocurrences);
				genericListData.SerializeData(writer, componentDataPool);
			}
		}
	}
}
