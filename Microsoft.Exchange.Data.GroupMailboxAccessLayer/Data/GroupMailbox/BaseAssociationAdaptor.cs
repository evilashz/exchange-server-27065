using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BaseAssociationAdaptor : IAssociationAdaptor
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000504C File Offset: 0x0000324C
		public BaseAssociationAdaptor(IAssociationStore associationStore, IRecipientSession adSession, MailboxLocator masterMailboxLocator)
		{
			ArgumentValidator.ThrowIfNull("associationStore", associationStore);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfNull("masterMailboxLocator", masterMailboxLocator);
			this.associationStore = associationStore;
			this.adSession = adSession;
			this.masterMailboxLocator = masterMailboxLocator;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000059 RID: 89 RVA: 0x0000508C File Offset: 0x0000328C
		// (remove) Token: 0x0600005A RID: 90 RVA: 0x000050C4 File Offset: 0x000032C4
		public event Action<IMailboxLocator> OnAfterJoin;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000050F9 File Offset: 0x000032F9
		public IMailboxLocator MasterLocator
		{
			get
			{
				return this.masterMailboxLocator;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00005101 File Offset: 0x00003301
		public IRecipientSession AdSession
		{
			get
			{
				return this.adSession;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00005109 File Offset: 0x00003309
		public IAssociationStore AssociationStore
		{
			get
			{
				return this.associationStore;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00005111 File Offset: 0x00003311
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00005119 File Offset: 0x00003319
		public MasterMailboxType MasterMailboxData { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00005122 File Offset: 0x00003322
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000512A File Offset: 0x0000332A
		public bool UseAlternateLocatorLookup { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98
		protected abstract Trace Tracer { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99
		protected abstract PropertyDefinition[] PropertiesToLoad { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100
		protected abstract string ItemClass { get; }

		// Token: 0x06000065 RID: 101
		public abstract MailboxLocator GetSlaveMailboxLocator(MailboxAssociation association);

		// Token: 0x06000066 RID: 102
		public abstract MailboxAssociation GetAssociation(VersionedId itemId);

		// Token: 0x06000067 RID: 103 RVA: 0x00005134 File Offset: 0x00003334
		public void DeleteAssociation(MailboxAssociation association)
		{
			MailboxAssociationFromStore mailboxAssociationFromStore = association as MailboxAssociationFromStore;
			IMailboxAssociationBaseItem mailboxAssociationBaseItem;
			if (mailboxAssociationFromStore != null)
			{
				this.Tracer.TraceDebug<VersionedId, MailboxAssociationFromStore>((long)this.GetHashCode(), "BaseAssociationAdaptor.DeleteAssociation: Found MailboxAssociationFromStore querying store by ItemId. ItemId={0}. Association={1}.", mailboxAssociationFromStore.ItemId, mailboxAssociationFromStore);
				mailboxAssociationBaseItem = this.GetAssociationByItemId(mailboxAssociationFromStore.ItemId);
			}
			else
			{
				IMailboxLocator slaveMailboxLocator = this.GetSlaveMailboxLocator(association);
				this.Tracer.TraceDebug<IMailboxLocator, MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.DeleteAssociation: Found in memory MailboxAssociation, querying store by slave locator. Slave Locator={0}. Association={1}.", slaveMailboxLocator, association);
				mailboxAssociationBaseItem = this.GetItemFromStore(slaveMailboxLocator);
			}
			if (mailboxAssociationBaseItem != null)
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "BaseAssociationAdaptor.DeleteAssociation: Association item found in store, Deleting.");
				this.associationStore.DeleteAssociation(mailboxAssociationBaseItem);
				return;
			}
			this.Tracer.TraceDebug((long)this.GetHashCode(), "BaseAssociationAdaptor.DeleteAssociation: Association item not found. No action needed.");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000051E0 File Offset: 0x000033E0
		public MailboxAssociation GetAssociation(IMailboxLocator locator)
		{
			ArgumentValidator.ThrowIfNull("locator", locator);
			this.ValidateTargetLocatorType(locator);
			MailboxAssociation mailboxAssociation;
			using (IMailboxAssociationBaseItem itemFromStore = this.GetItemFromStore(locator))
			{
				if (itemFromStore != null)
				{
					this.Tracer.TraceDebug<IMailboxLocator>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetAssociation: Creating association from store item. Locator={0}", locator);
					mailboxAssociation = this.CreateMailboxAssociationFromItem(itemFromStore, true);
				}
				else
				{
					this.Tracer.TraceDebug<IMailboxLocator>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetAssociation: Creating default association. Locator={0}", locator);
					mailboxAssociation = this.CreateMailboxAssociationWithDefaultValues(locator);
				}
			}
			this.Tracer.TraceDebug<IMailboxLocator, MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetAssociation: Returning association for Locator={0}. Association: {1}", locator, mailboxAssociation);
			return mailboxAssociation;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005478 File Offset: 0x00003678
		public IEnumerable<MailboxAssociation> GetAllAssociations()
		{
			IEnumerable<IPropertyBag> foundItems = this.associationStore.GetAllAssociations(this.ItemClass, this.PropertiesToLoad);
			foreach (IPropertyBag item in foundItems)
			{
				MailboxAssociation association = this.CreateMailboxAssociationFromItem(item, false);
				this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetAllAssociations: Returning association: {0}", association);
				yield return association;
			}
			yield break;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000056A0 File Offset: 0x000038A0
		public IEnumerable<MailboxAssociation> GetMembershipAssociations(int? maxItems)
		{
			IEnumerable<IPropertyBag> foundItems = this.associationStore.GetAssociationsByType(this.ItemClass, MailboxAssociationBaseSchema.IsMember, maxItems, this.PropertiesToLoad);
			foreach (IPropertyBag item in foundItems)
			{
				MailboxAssociation association = this.CreateMailboxAssociationFromItem(item, false);
				this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetMembershipAssociations: Returning association: {0}", association);
				yield return association;
			}
			yield break;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000058BC File Offset: 0x00003ABC
		public IEnumerable<MailboxAssociation> GetEscalatedAssociations()
		{
			IEnumerable<IPropertyBag> foundItems = this.associationStore.GetAssociationsByType(this.ItemClass, MailboxAssociationBaseSchema.ShouldEscalate, this.PropertiesToLoad);
			foreach (IPropertyBag item in foundItems)
			{
				MailboxAssociation association = this.CreateMailboxAssociationFromItem(item, false);
				this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetEscalatedAssociations: Returning association: {0}", association);
				yield return association;
			}
			yield break;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public IEnumerable<MailboxAssociation> GetPinAssociations()
		{
			IEnumerable<IPropertyBag> foundItems = this.associationStore.GetAssociationsByType(this.ItemClass, MailboxAssociationBaseSchema.IsPin, this.PropertiesToLoad);
			foreach (IPropertyBag item in foundItems)
			{
				MailboxAssociation association = this.CreateMailboxAssociationFromItem(item, false);
				this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetPinAssociations: Returning association: {0}", association);
				yield return association;
			}
			yield break;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005CEC File Offset: 0x00003EEC
		public IEnumerable<MailboxAssociation> GetAssociationsWithMembershipChangedAfter(ExDateTime date)
		{
			IEnumerable<IPropertyBag> foundItems = this.associationStore.GetAssociationsWithMembershipChangedAfter(date, this.PropertiesToLoad);
			foreach (IPropertyBag item in foundItems)
			{
				MailboxAssociation association = this.CreateMailboxAssociationFromItem(item, false);
				this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetMembershipChangedAfterAssociations: Returning association: {0}", association);
				yield return association;
			}
			yield break;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005D10 File Offset: 0x00003F10
		public void SaveAssociation(MailboxAssociation association, bool markForReplication)
		{
			this.SaveAssociationInternal(association, markForReplication, new Action<MailboxAssociation, IMailboxAssociationBaseItem>(this.UpdateStoreAssociationMasterData));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005D27 File Offset: 0x00003F27
		public void ReplicateAssociation(MailboxAssociation association)
		{
			this.SaveAssociationInternal(association, false, new Action<MailboxAssociation, IMailboxAssociationBaseItem>(this.UpdateStoreAssociationSlaveData));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005D3E File Offset: 0x00003F3E
		public void SaveSyncState(MailboxAssociation association)
		{
			this.SaveAssociationInternal(association, false, new Action<MailboxAssociation, IMailboxAssociationBaseItem>(BaseAssociationAdaptor.UpdateStoreAssociationSyncState));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005D54 File Offset: 0x00003F54
		protected static void UpdateLocatorDataInStoreItem(IMailboxLocator mailboxLocator, IMailboxAssociationBaseItem item)
		{
			if (!string.IsNullOrEmpty(mailboxLocator.LegacyDn))
			{
				item.LegacyDN = mailboxLocator.LegacyDn;
			}
			if (!string.IsNullOrEmpty(mailboxLocator.ExternalId))
			{
				item.ExternalId = mailboxLocator.ExternalId;
			}
		}

		// Token: 0x06000072 RID: 114
		protected abstract MailboxAssociation CreateMailboxAssociationWithDefaultValues(IMailboxLocator locator);

		// Token: 0x06000073 RID: 115
		protected abstract MailboxAssociation CreateMailboxAssociationFromItem(IPropertyBag item, bool setExtendedProperties = false);

		// Token: 0x06000074 RID: 116
		protected abstract void ValidateTargetLocatorType(IMailboxLocator locator);

		// Token: 0x06000075 RID: 117 RVA: 0x00005D88 File Offset: 0x00003F88
		protected IMailboxAssociationBaseItem ReadOrCreateMailboxItem(MailboxAssociation mailboxAssociation)
		{
			MailboxAssociationFromStore mailboxAssociationFromStore = mailboxAssociation as MailboxAssociationFromStore;
			if (mailboxAssociationFromStore != null)
			{
				this.Tracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "BaseAssociationAdaptor.ReadOrCreateMailboxItem. Binding item using entry id found in property bag. Id = {0}", mailboxAssociationFromStore.ItemId);
				IMailboxAssociationBaseItem associationByItemId = this.GetAssociationByItemId(mailboxAssociationFromStore.ItemId);
				this.associationStore.OpenAssociationAsReadWrite(associationByItemId);
				return associationByItemId;
			}
			this.Tracer.TraceDebug((long)this.GetHashCode(), "BaseAssociationAdaptor.ReadOrCreateMailboxItem. MailboxAssociation was not instantiated from store item, querying store");
			MailboxLocator slaveMailboxLocator = this.GetSlaveMailboxLocator(mailboxAssociation);
			return this.ReadOrCreateMailboxItem(slaveMailboxLocator);
		}

		// Token: 0x06000076 RID: 118
		protected abstract IMailboxAssociationBaseItem GetAssociationByItemId(VersionedId itemId);

		// Token: 0x06000077 RID: 119
		protected abstract IMailboxAssociationBaseItem CreateStoreItem(MailboxLocator locator);

		// Token: 0x06000078 RID: 120
		protected abstract IMailboxAssociationBaseItem GetAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues);

		// Token: 0x06000079 RID: 121
		protected abstract void UpdateStoreAssociationMasterData(MailboxAssociation association, IMailboxAssociationBaseItem item);

		// Token: 0x0600007A RID: 122
		protected abstract void UpdateStoreAssociationSlaveData(MailboxAssociation association, IMailboxAssociationBaseItem item);

		// Token: 0x0600007B RID: 123 RVA: 0x00005E00 File Offset: 0x00004000
		private static void UpdateStoreAssociationSyncState(MailboxAssociation association, IMailboxAssociationBaseItem item)
		{
			item.SyncedVersion = association.SyncedVersion;
			item.LastSyncError = (association.LastSyncError ?? string.Empty);
			item.SyncAttempts = association.SyncAttempts;
			item.SyncedSchemaVersion = (association.SyncedSchemaVersion ?? string.Empty);
			if (association.SyncedIdentityHash != null)
			{
				item.SyncedIdentityHash = association.SyncedIdentityHash;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005E64 File Offset: 0x00004064
		private void SaveAssociationInternal(MailboxAssociation association, bool incrementReplicationVersion, Action<MailboxAssociation, IMailboxAssociationBaseItem> updateFunction)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			ArgumentValidator.ThrowIfNull("updateFunction", updateFunction);
			using (IMailboxAssociationBaseItem mailboxAssociationBaseItem = this.ReadOrCreateMailboxItem(association))
			{
				bool isMember = mailboxAssociationBaseItem.IsMember;
				updateFunction(association, mailboxAssociationBaseItem);
				if (incrementReplicationVersion)
				{
					this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.SaveAssociationInternal. Incrementing CurrentVersion of the association item. Association = {0}", association);
					mailboxAssociationBaseItem.CurrentVersion++;
					association.CurrentVersion++;
				}
				else
				{
					this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "BaseAssociationAdaptor.SaveAssociationInternal. Saving association without affecting CurrentVersion of the item. Association = {0}", association);
				}
				this.associationStore.SaveAssociation(mailboxAssociationBaseItem);
				if (!isMember && association.IsMember && this.OnAfterJoin != null)
				{
					this.OnAfterJoin(this.GetSlaveMailboxLocator(association));
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005F3C File Offset: 0x0000413C
		private IMailboxAssociationBaseItem ReadOrCreateMailboxItem(MailboxLocator mailboxLocator)
		{
			IMailboxAssociationBaseItem mailboxAssociationBaseItem = this.GetItemFromStore(mailboxLocator);
			if (mailboxAssociationBaseItem != null)
			{
				this.Tracer.TraceDebug<MailboxLocator>((long)this.GetHashCode(), "GroupAssociationAdaptor.ReadOrCreateMailboxItem: Association item found in store, opening for read/write. Locator={0}.", mailboxLocator);
				this.associationStore.OpenAssociationAsReadWrite(mailboxAssociationBaseItem);
			}
			else
			{
				this.Tracer.TraceDebug<MailboxLocator>((long)this.GetHashCode(), "GroupAssociationAdaptor.ReadOrCreateMailboxItem: Association item not found in store, creating new item. Locator={0}.", mailboxLocator);
				mailboxAssociationBaseItem = this.CreateStoreItem(mailboxLocator);
			}
			return mailboxAssociationBaseItem;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005F9C File Offset: 0x0000419C
		private IMailboxAssociationBaseItem GetItemFromStore(IMailboxLocator locator)
		{
			IMailboxAssociationBaseItem mailboxAssociationBaseItem = null;
			if (!string.IsNullOrEmpty(locator.ExternalId))
			{
				this.Tracer.TraceDebug<IMailboxLocator>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetItemFromStore: Querying item in store by ExternalDirectoryObjectId. Locator={0}.", locator);
				mailboxAssociationBaseItem = this.GetAssociationByIdProperty(MailboxAssociationBaseSchema.ExternalId, new object[]
				{
					locator.ExternalId
				});
			}
			if (string.IsNullOrEmpty(locator.ExternalId) || (mailboxAssociationBaseItem == null && this.UseAlternateLocatorLookup))
			{
				this.Tracer.TraceDebug<IMailboxLocator>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetItemFromStore: Querying item in store by LegacyExchangeDN. Locator={0}.", locator);
				mailboxAssociationBaseItem = this.GetAssociationByIdProperty(MailboxAssociationBaseSchema.LegacyDN, new object[]
				{
					locator.LegacyDn
				});
				if (mailboxAssociationBaseItem == null)
				{
					this.Tracer.TraceDebug<IMailboxLocator>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetItemFromStore: Querying item in store by Alternate LegacyExchangeDN. Locator={0}.", locator);
					try
					{
						string[] idValues = locator.FindAlternateLegacyDNs();
						mailboxAssociationBaseItem = this.GetAssociationByIdProperty(MailboxAssociationBaseSchema.LegacyDN, idValues);
					}
					catch (MailboxNotFoundException arg)
					{
						this.Tracer.TraceDebug<IMailboxLocator, MailboxNotFoundException>((long)this.GetHashCode(), "BaseAssociationAdaptor.GetItemFromStore: Couldn't find Alternate Legacy DNs for the locator as the ADObject was not found. Returning NULL item. Locator={0}. Exception={1}", locator, arg);
					}
				}
			}
			return mailboxAssociationBaseItem;
		}

		// Token: 0x0400002C RID: 44
		private readonly IAssociationStore associationStore;

		// Token: 0x0400002D RID: 45
		private readonly IRecipientSession adSession;

		// Token: 0x0400002E RID: 46
		private readonly MailboxLocator masterMailboxLocator;
	}
}
