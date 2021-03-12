using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ReplicatorEnabledAssociationEnumerator : IEnumerable<MailboxAssociation>, IEnumerable
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public ReplicatorEnabledAssociationEnumerator(IAssociationReplicator replicator, IEnumerable<MailboxAssociation> baseEnumerator, IAssociationStore storeProvider)
		{
			ArgumentValidator.ThrowIfNull("replicator", replicator);
			ArgumentValidator.ThrowIfNull("baseEnumerator", baseEnumerator);
			ArgumentValidator.ThrowIfNull("storeProvider", storeProvider);
			this.replicator = replicator;
			this.baseEnumerator = baseEnumerator;
			this.storeProvider = storeProvider;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B910 File Offset: 0x00009B10
		public void TriggerReplication(IAssociationAdaptor masterAdaptor)
		{
			ReplicatorEnabledAssociationEnumerator.Tracer.TraceDebug<int>((long)this.GetHashCode(), "ReplicatorEnabledAssociationEnumerator.TriggerReplication: Found {0} associations out of sync", this.outOfSyncAssociations.Count);
			if (this.outOfSyncAssociations.Count > 0)
			{
				this.replicator.ReplicateAssociation(masterAdaptor, this.outOfSyncAssociations.ToArray());
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000BB44 File Offset: 0x00009D44
		public IEnumerator<MailboxAssociation> GetEnumerator()
		{
			ExDateTime minTime = ExDateTime.UtcNow - TimeSpan.FromMinutes(2.0);
			foreach (MailboxAssociation mailboxAssociation in this.baseEnumerator)
			{
				ReplicatorEnabledAssociationEnumerator.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "ReplicatorEnabledAssociationEnumerator.GetEnumerator: Found association: {0}", mailboxAssociation);
				if (this.IsOutOfSyncAssociation(mailboxAssociation) && mailboxAssociation.LastModified < minTime)
				{
					ReplicatorEnabledAssociationEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ReplicatorEnabledAssociationEnumerator.GetEnumerator: Association is out of sync");
					this.outOfSyncAssociations.Add(mailboxAssociation);
				}
				yield return mailboxAssociation;
			}
			yield break;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000BB60 File Offset: 0x00009D60
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BB6C File Offset: 0x00009D6C
		private bool IsOutOfSyncAssociation(MailboxAssociation association)
		{
			if (association.CurrentVersion > association.SyncedVersion)
			{
				ReplicatorEnabledAssociationEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMailboxAccessLayer::IsOutOfSyncAssociation. Association {0}/{1} is out of sync because current version ({2}) is greater than synced version ({3})", new object[]
				{
					association.User,
					association.Group,
					association.CurrentVersion,
					association.SyncedVersion
				});
				return true;
			}
			if (!StringComparer.OrdinalIgnoreCase.Equals(association.SyncedIdentityHash, this.storeProvider.MailboxLocator.IdentityHash))
			{
				ReplicatorEnabledAssociationEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMailboxAccessLayer::IsOutOfSyncAssociation. Association {0}/{1} is out of sync because current identity hash of mailbox ({2}) is different than the one synced ({3})", new object[]
				{
					association.User,
					association.Group,
					this.storeProvider.MailboxLocator.IdentityHash,
					association.SyncedIdentityHash
				});
				return true;
			}
			return false;
		}

		// Token: 0x040000BF RID: 191
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;

		// Token: 0x040000C0 RID: 192
		private readonly IAssociationReplicator replicator;

		// Token: 0x040000C1 RID: 193
		private readonly IEnumerable<MailboxAssociation> baseEnumerator;

		// Token: 0x040000C2 RID: 194
		private readonly IAssociationStore storeProvider;

		// Token: 0x040000C3 RID: 195
		private List<MailboxAssociation> outOfSyncAssociations = new List<MailboxAssociation>(10);
	}
}
