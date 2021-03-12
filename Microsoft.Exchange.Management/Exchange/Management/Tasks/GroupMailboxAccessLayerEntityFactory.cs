using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000464 RID: 1124
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class GroupMailboxAccessLayerEntityFactory
	{
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x0009CA54 File Offset: 0x0009AC54
		// (set) Token: 0x060027A1 RID: 10145 RVA: 0x0009CA5C File Offset: 0x0009AC5C
		private protected IRecipientSession AdSession { protected get; private set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x0009CA65 File Offset: 0x0009AC65
		// (set) Token: 0x060027A3 RID: 10147 RVA: 0x0009CA6D File Offset: 0x0009AC6D
		private protected ADUser CurrentMailbox { protected get; private set; }

		// Token: 0x060027A4 RID: 10148
		public abstract MailboxLocator CreateMasterLocator();

		// Token: 0x060027A5 RID: 10149 RVA: 0x0009CA76 File Offset: 0x0009AC76
		protected GroupMailboxAccessLayerEntityFactory(IRecipientSession adSession, ADUser currentMailbox)
		{
			this.AdSession = adSession;
			this.CurrentMailbox = currentMailbox;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0009CA8C File Offset: 0x0009AC8C
		public static GroupMailboxAccessLayerEntityFactory Instantiate(IRecipientSession adSession, ADUser currentMailbox)
		{
			if (currentMailbox.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				return new GroupMailboxAccessLayerEntityFactory.GroupMailboxAccessLayerFactoryForGroupMailbox(adSession, currentMailbox);
			}
			if (currentMailbox.RecipientTypeDetails == RecipientTypeDetails.UserMailbox)
			{
				return new GroupMailboxAccessLayerEntityFactory.GroupMailboxAccessLayerFactoryForUserMailbox(adSession, currentMailbox);
			}
			throw new InvalidOperationException("Unsupported type of mailbox");
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0009CAC4 File Offset: 0x0009ACC4
		public IMailboxLocator CreateSlaveLocator(MailboxAssociationIdParameter mailboxAssociationIdParameter)
		{
			string externalId = null;
			string associationIdValue = mailboxAssociationIdParameter.AssociationIdValue;
			if (mailboxAssociationIdParameter.AssociationIdType == MailboxAssociationIdParameter.IdTypeExternalId)
			{
				externalId = mailboxAssociationIdParameter.AssociationIdValue;
			}
			return this.CreateSlaveLocator(externalId, associationIdValue);
		}

		// Token: 0x060027A8 RID: 10152
		protected abstract IMailboxLocator CreateSlaveLocator(string externalId, string legacyDn);

		// Token: 0x060027A9 RID: 10153
		public abstract BaseAssociationAdaptor CreateAssociationAdaptor(MailboxLocator master, IAssociationStore associationStore);

		// Token: 0x02000465 RID: 1125
		private class GroupMailboxAccessLayerFactoryForGroupMailbox : GroupMailboxAccessLayerEntityFactory
		{
			// Token: 0x060027AA RID: 10154 RVA: 0x0009CAFB File Offset: 0x0009ACFB
			public GroupMailboxAccessLayerFactoryForGroupMailbox(IRecipientSession adSession, ADUser currentMailbox) : base(adSession, currentMailbox)
			{
			}

			// Token: 0x060027AB RID: 10155 RVA: 0x0009CB05 File Offset: 0x0009AD05
			public override MailboxLocator CreateMasterLocator()
			{
				return GroupMailboxLocator.Instantiate(base.AdSession, base.CurrentMailbox);
			}

			// Token: 0x060027AC RID: 10156 RVA: 0x0009CB18 File Offset: 0x0009AD18
			protected override IMailboxLocator CreateSlaveLocator(string externalId, string legacyDn)
			{
				return new UserMailboxLocator(base.AdSession, externalId, legacyDn);
			}

			// Token: 0x060027AD RID: 10157 RVA: 0x0009CB27 File Offset: 0x0009AD27
			public override BaseAssociationAdaptor CreateAssociationAdaptor(MailboxLocator master, IAssociationStore associationStore)
			{
				return new UserAssociationAdaptor(associationStore, base.AdSession, (GroupMailboxLocator)master);
			}
		}

		// Token: 0x02000466 RID: 1126
		private class GroupMailboxAccessLayerFactoryForUserMailbox : GroupMailboxAccessLayerEntityFactory
		{
			// Token: 0x060027AE RID: 10158 RVA: 0x0009CB3B File Offset: 0x0009AD3B
			public GroupMailboxAccessLayerFactoryForUserMailbox(IRecipientSession adSession, ADUser currentMailbox) : base(adSession, currentMailbox)
			{
			}

			// Token: 0x060027AF RID: 10159 RVA: 0x0009CB45 File Offset: 0x0009AD45
			public override MailboxLocator CreateMasterLocator()
			{
				return UserMailboxLocator.Instantiate(base.AdSession, base.CurrentMailbox);
			}

			// Token: 0x060027B0 RID: 10160 RVA: 0x0009CB58 File Offset: 0x0009AD58
			protected override IMailboxLocator CreateSlaveLocator(string externalId, string legacyDn)
			{
				return new GroupMailboxLocator(base.AdSession, externalId, legacyDn);
			}

			// Token: 0x060027B1 RID: 10161 RVA: 0x0009CB67 File Offset: 0x0009AD67
			public override BaseAssociationAdaptor CreateAssociationAdaptor(MailboxLocator master, IAssociationStore associationStore)
			{
				return new GroupAssociationAdaptor(associationStore, base.AdSession, (UserMailboxLocator)master);
			}
		}
	}
}
