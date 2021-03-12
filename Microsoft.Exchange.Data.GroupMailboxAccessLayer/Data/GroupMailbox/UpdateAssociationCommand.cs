using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UpdateAssociationCommand
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000087E8 File Offset: 0x000069E8
		protected UpdateAssociationCommand(IExtensibleLogger logger, IAssociationAdaptor masterAdaptor, params IMailboxLocator[] itemLocators)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			ArgumentValidator.ThrowIfNull("masterAdaptor", masterAdaptor);
			ArgumentValidator.ThrowIfNull("itemLocators", itemLocators);
			ArgumentValidator.ThrowIfZeroOrNegative("itemLocators.Length", itemLocators.Length);
			this.MasterAdaptor = masterAdaptor;
			this.Logger = logger;
			this.ItemLocators = itemLocators;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000883E File Offset: 0x00006A3E
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00008846 File Offset: 0x00006A46
		private protected IMailboxLocator[] ItemLocators { protected get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000884F File Offset: 0x00006A4F
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00008857 File Offset: 0x00006A57
		private protected IAssociationAdaptor MasterAdaptor { protected get; private set; }

		// Token: 0x06000118 RID: 280 RVA: 0x00008860 File Offset: 0x00006A60
		public void Execute()
		{
			List<MailboxAssociation> list = new List<MailboxAssociation>(this.ItemLocators.Length);
			foreach (IMailboxLocator mailboxLocator in this.ItemLocators)
			{
				MailboxAssociation mailboxAssociation = this.LoadAssociation(mailboxLocator);
				if (this.UpdateAssociation(mailboxAssociation))
				{
					this.SaveAssociation(mailboxAssociation);
					list.Add(mailboxAssociation);
					UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator>((long)this.GetHashCode(), "Saved association for user {0}", mailboxAssociation.User);
				}
				else if (mailboxAssociation.IsOutOfSync(this.MasterAdaptor.MasterLocator.IdentityHash))
				{
					list.Add(mailboxAssociation);
					UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator>((long)this.GetHashCode(), "Ignored saving association for user {0}, but association is out of sync, so replication will be attempted.", mailboxAssociation.User);
				}
				else
				{
					UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator>((long)this.GetHashCode(), "Ignored saving association for user {0}", mailboxAssociation.User);
				}
			}
			IAssociationReplicator associationReplicator = this.GetAssociationReplicator();
			if (list.Count > 0 && associationReplicator != null)
			{
				associationReplicator.ReplicateAssociation(this.MasterAdaptor, list.ToArray());
			}
			this.OnPostExecute();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008964 File Offset: 0x00006B64
		protected virtual void OnPostExecute()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008968 File Offset: 0x00006B68
		protected MailboxAssociation LoadAssociation(IMailboxLocator mailboxLocator)
		{
			ArgumentValidator.ThrowIfNull("mailboxLocator", mailboxLocator);
			MailboxAssociation association = this.MasterAdaptor.GetAssociation(mailboxLocator);
			UpdateAssociationCommand.Tracer.TraceDebug<IMailboxLocator, MailboxAssociation>((long)this.GetHashCode(), "LoadAssociation: mailboxLocator={0}, association={1}", mailboxLocator, association);
			return association;
		}

		// Token: 0x0600011B RID: 283
		protected abstract bool UpdateAssociation(MailboxAssociation association);

		// Token: 0x0600011C RID: 284 RVA: 0x000089A6 File Offset: 0x00006BA6
		protected virtual IAssociationReplicator GetAssociationReplicator()
		{
			return null;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000089AC File Offset: 0x00006BAC
		protected void SaveAssociation(MailboxAssociation association)
		{
			UpdateAssociationCommand.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "SaveAssociation: {0}", association);
			ArgumentValidator.ThrowIfNull("association", association);
			bool markForReplication = this.GetAssociationReplicator() != null;
			this.MasterAdaptor.SaveAssociation(association, markForReplication);
		}

		// Token: 0x04000075 RID: 117
		protected static readonly Trace Tracer = ExTraceGlobals.UpdateAssociationCommandTracer;

		// Token: 0x04000076 RID: 118
		protected readonly IExtensibleLogger Logger;
	}
}
