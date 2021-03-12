using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UnseenDataUserAssociationAdaptor : BaseAssociationAdaptor, IUserAssociationAdaptor, IAssociationAdaptor
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00006971 File Offset: 0x00004B71
		public UnseenDataUserAssociationAdaptor(IAssociationStore associationStore, IRecipientSession adSession, GroupMailboxLocator currentGroup) : base(associationStore, adSession, currentGroup)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000697C File Offset: 0x00004B7C
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.UnseenDataUserAssociationAdaptorTracer;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00006983 File Offset: 0x00004B83
		protected override string ItemClass
		{
			get
			{
				return "IPM.MailboxAssociation.User";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000698A File Offset: 0x00004B8A
		protected override PropertyDefinition[] PropertiesToLoad
		{
			get
			{
				return UnseenDataUserAssociationAdaptor.AllProperties;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006991 File Offset: 0x00004B91
		public override MailboxAssociation GetAssociation(VersionedId itemId)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000699D File Offset: 0x00004B9D
		public new IEnumerable<MailboxAssociation> GetAllAssociations()
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000069A9 File Offset: 0x00004BA9
		public new IEnumerable<MailboxAssociation> GetEscalatedAssociations()
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000069B5 File Offset: 0x00004BB5
		public new IEnumerable<MailboxAssociation> GetPinAssociations()
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000069C1 File Offset: 0x00004BC1
		public override MailboxLocator GetSlaveMailboxLocator(MailboxAssociation association)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000069CD File Offset: 0x00004BCD
		public new void DeleteAssociation(MailboxAssociation association)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000069D9 File Offset: 0x00004BD9
		public new void SaveAssociation(MailboxAssociation association, bool markForReplication)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000069E5 File Offset: 0x00004BE5
		public new void SaveSyncState(MailboxAssociation association)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000069F1 File Offset: 0x00004BF1
		public new void ReplicateAssociation(MailboxAssociation association)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000069FD File Offset: 0x00004BFD
		protected override void UpdateStoreAssociationMasterData(MailboxAssociation association, IMailboxAssociationBaseItem item)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006A09 File Offset: 0x00004C09
		protected override void UpdateStoreAssociationSlaveData(MailboxAssociation association, IMailboxAssociationBaseItem item)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006A15 File Offset: 0x00004C15
		protected override void ValidateTargetLocatorType(IMailboxLocator locator)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006A21 File Offset: 0x00004C21
		protected override MailboxAssociation CreateMailboxAssociationWithDefaultValues(IMailboxLocator user)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006A2D File Offset: 0x00004C2D
		protected override IMailboxAssociationBaseItem GetAssociationByItemId(VersionedId itemId)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006A39 File Offset: 0x00004C39
		protected override IMailboxAssociationBaseItem GetAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006A45 File Offset: 0x00004C45
		protected override IMailboxAssociationBaseItem CreateStoreItem(MailboxLocator locator)
		{
			throw new NotImplementedException("UnseenDataUserAssociationAdaptor should only be used to GetMembershipAssociations");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006A54 File Offset: 0x00004C54
		protected override MailboxAssociation CreateMailboxAssociationFromItem(IPropertyBag item, bool setExtendedProperties = false)
		{
			UserMailboxLocator user = new UserMailboxLocator(base.AdSession, base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.ExternalId, string.Empty), base.AssociationStore.GetValueOrDefault<string>(item, MailboxAssociationBaseSchema.LegacyDN, string.Empty));
			MailboxAssociationFromStore mailboxAssociationFromStore = new MailboxAssociationFromStore
			{
				User = user,
				Group = (base.MasterLocator as GroupMailboxLocator),
				IsMember = base.AssociationStore.GetValueOrDefault<bool>(item, MailboxAssociationBaseSchema.IsMember, false),
				JoinDate = base.AssociationStore.GetValueOrDefault<ExDateTime>(item, MailboxAssociationBaseSchema.JoinDate, default(ExDateTime)),
				LastVisitedDate = base.AssociationStore.GetValueOrDefault<ExDateTime>(item, MailboxAssociationUserSchema.LastVisitedDate, default(ExDateTime))
			};
			this.Tracer.TraceDebug<bool, MailboxAssociationFromStore>((long)this.GetHashCode(), "UnseenDataUserAssociationAdaptor .CreateMailboxAssociationFromItem: Creating association from information found in store item. SetExtendedProperties={0}, Association={1}", setExtendedProperties, mailboxAssociationFromStore);
			return mailboxAssociationFromStore;
		}

		// Token: 0x0400004A RID: 74
		private static readonly PropertyDefinition[] AllProperties = new PropertyDefinition[]
		{
			MailboxAssociationBaseSchema.ExternalId,
			MailboxAssociationBaseSchema.LegacyDN,
			MailboxAssociationBaseSchema.IsMember,
			MailboxAssociationBaseSchema.JoinDate,
			MailboxAssociationUserSchema.LastVisitedDate
		};
	}
}
