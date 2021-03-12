using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupAssociationAdaptor : BaseAssociationAdaptor, IGroupAssociationAdaptor, IAssociationAdaptor
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000060A4 File Offset: 0x000042A4
		public GroupAssociationAdaptor(IAssociationStore associationStore, IRecipientSession adSession, UserMailboxLocator currentUser) : base(associationStore, adSession, currentUser)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000060AF File Offset: 0x000042AF
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.GroupAssociationAdaptorTracer;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000060B6 File Offset: 0x000042B6
		protected override string ItemClass
		{
			get
			{
				return "IPM.MailboxAssociation.Group";
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000060BD File Offset: 0x000042BD
		protected override PropertyDefinition[] PropertiesToLoad
		{
			get
			{
				return GroupAssociationAdaptor.AllProperties;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000060C4 File Offset: 0x000042C4
		public override MailboxLocator GetSlaveMailboxLocator(MailboxAssociation association)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			return association.Group;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000060D8 File Offset: 0x000042D8
		public override MailboxAssociation GetAssociation(VersionedId itemId)
		{
			ArgumentValidator.ThrowIfNull("itemId", itemId);
			MailboxAssociation result = null;
			using (IMailboxAssociationBaseItem associationByItemId = this.GetAssociationByItemId(itemId))
			{
				if (associationByItemId != null)
				{
					this.Tracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "GroupAssociationAdaptor.GetAssociation: Creating association from store item. itemId={0}", itemId);
					result = this.CreateMailboxAssociationFromItem(associationByItemId, true);
				}
			}
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000613C File Offset: 0x0000433C
		protected override void UpdateStoreAssociationMasterData(MailboxAssociation association, IMailboxAssociationBaseItem item)
		{
			IMailboxAssociationGroup mailboxAssociationGroup = (IMailboxAssociationGroup)item;
			BaseAssociationAdaptor.UpdateLocatorDataInStoreItem(association.Group, mailboxAssociationGroup);
			mailboxAssociationGroup.IsPin = association.IsPin;
			mailboxAssociationGroup.PinDate = association.PinDate;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006174 File Offset: 0x00004374
		protected override void UpdateStoreAssociationSlaveData(MailboxAssociation association, IMailboxAssociationBaseItem item)
		{
			IMailboxAssociationGroup mailboxAssociationGroup = (IMailboxAssociationGroup)item;
			BaseAssociationAdaptor.UpdateLocatorDataInStoreItem(association.Group, mailboxAssociationGroup);
			mailboxAssociationGroup.SyncedIdentityHash = association.User.IdentityHash;
			mailboxAssociationGroup.IsMember = association.IsMember;
			mailboxAssociationGroup.JoinDate = association.JoinDate;
			if (!association.IsMember)
			{
				mailboxAssociationGroup.IsPin = false;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000061CC File Offset: 0x000043CC
		protected override void ValidateTargetLocatorType(IMailboxLocator locator)
		{
			ArgumentValidator.ThrowIfTypeInvalid<GroupMailboxLocator>("locator", locator);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000061DC File Offset: 0x000043DC
		protected override MailboxAssociation CreateMailboxAssociationWithDefaultValues(IMailboxLocator group)
		{
			this.ValidateTargetLocatorType(group);
			MailboxAssociation mailboxAssociation = new MailboxAssociation
			{
				User = (base.MasterLocator as UserMailboxLocator),
				Group = (group as GroupMailboxLocator),
				IsPin = false,
				IsMember = false,
				JoinDate = default(ExDateTime),
				PinDate = default(ExDateTime)
			};
			this.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "GroupAssociationAdaptor.CreateMailboxAssociationWithDefaultValues: Creating new association with default values. Association={0}", mailboxAssociation);
			return mailboxAssociation;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000625A File Offset: 0x0000445A
		protected override IMailboxAssociationBaseItem GetAssociationByItemId(VersionedId itemId)
		{
			return base.AssociationStore.GetGroupAssociationByItemId(itemId);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006268 File Offset: 0x00004468
		protected override IMailboxAssociationBaseItem GetAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues)
		{
			return base.AssociationStore.GetGroupAssociationByIdProperty(idProperty, idValues);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006278 File Offset: 0x00004478
		protected override IMailboxAssociationBaseItem CreateStoreItem(MailboxLocator locator)
		{
			IMailboxAssociationGroup mailboxAssociationGroup = base.AssociationStore.CreateGroupAssociation();
			mailboxAssociationGroup[MailboxAssociationBaseSchema.ExternalId] = (locator.ExternalId ?? string.Empty);
			mailboxAssociationGroup[MailboxAssociationBaseSchema.LegacyDN] = locator.LegacyDn;
			mailboxAssociationGroup[MailboxAssociationBaseSchema.IsPin] = false;
			return mailboxAssociationGroup;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000062D0 File Offset: 0x000044D0
		protected override MailboxAssociation CreateMailboxAssociationFromItem(IPropertyBag item, bool setExtendedProperties = false)
		{
			GroupMailboxLocator group = new GroupMailboxLocator(base.AdSession, base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.ExternalId, string.Empty), base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.LegacyDN, string.Empty));
			MailboxAssociationFromStore mailboxAssociationFromStore = new MailboxAssociationFromStore
			{
				User = (base.MasterLocator as UserMailboxLocator),
				Group = group,
				ItemId = base.AssociationStore.GetValueOrDefault<VersionedId>(item, ItemSchema.Id, null),
				IsPin = base.AssociationStore.GetValueOrDefault<bool>(item, MailboxAssociationBaseSchema.IsPin, false),
				IsMember = base.AssociationStore.GetValueOrDefault<bool>(item, MailboxAssociationBaseSchema.IsMember, false),
				JoinDate = base.AssociationStore.GetValueOrDefault<ExDateTime>(item, MailboxAssociationBaseSchema.JoinDate, default(ExDateTime)),
				PinDate = base.AssociationStore.GetValueOrDefault<ExDateTime>(item, MailboxAssociationGroupSchema.PinDate, default(ExDateTime)),
				LastModified = base.AssociationStore.GetValueOrDefault<ExDateTime>(item, StoreObjectSchema.LastModifiedTime, default(ExDateTime)),
				SyncedVersion = base.AssociationStore.GetValueOrDefault<int>(item, MailboxAssociationBaseSchema.SyncedVersion, 0),
				CurrentVersion = base.AssociationStore.GetValueOrDefault<int>(item, MailboxAssociationBaseSchema.CurrentVersion, 0),
				SyncedIdentityHash = base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.SyncedIdentityHash, null)
			};
			if (setExtendedProperties)
			{
				mailboxAssociationFromStore.SyncAttempts = base.AssociationStore.GetValueOrDefault<int>(item, MailboxAssociationBaseSchema.SyncAttempts, 0);
				mailboxAssociationFromStore.SyncedSchemaVersion = base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.SyncedSchemaVersion, string.Empty);
				mailboxAssociationFromStore.LastSyncError = base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.LastSyncError, string.Empty);
			}
			this.Tracer.TraceDebug<bool, MailboxAssociationFromStore>((long)this.GetHashCode(), "GroupAssociationAdaptor.CreateMailboxAssociationFromItem: Creating association from information found in store item. SetExtendedProperties={0}, Association={1}", setExtendedProperties, mailboxAssociationFromStore);
			return mailboxAssociationFromStore;
		}

		// Token: 0x04000032 RID: 50
		private static readonly PropertyDefinition[] PropertiesToMaster = new PropertyDefinition[]
		{
			MailboxAssociationBaseSchema.ExternalId,
			MailboxAssociationBaseSchema.LegacyDN,
			MailboxAssociationBaseSchema.IsPin,
			MailboxAssociationGroupSchema.PinDate,
			MailboxAssociationBaseSchema.CurrentVersion,
			MailboxAssociationBaseSchema.SyncedVersion,
			MailboxAssociationBaseSchema.SyncedIdentityHash
		};

		// Token: 0x04000033 RID: 51
		private static readonly PropertyDefinition[] PropertiesToLoadForReadOnly = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MailboxAssociationBaseSchema.IsMember,
			MailboxAssociationBaseSchema.JoinDate,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04000034 RID: 52
		private static readonly PropertyDefinition[] AllProperties = PropertyDefinitionCollection.Merge<PropertyDefinition>(GroupAssociationAdaptor.PropertiesToMaster, GroupAssociationAdaptor.PropertiesToLoadForReadOnly);
	}
}
