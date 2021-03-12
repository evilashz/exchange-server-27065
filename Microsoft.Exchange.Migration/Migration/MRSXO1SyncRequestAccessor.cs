using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MRSXO1SyncRequestAccessor : MRSSyncRequestAccessorBase
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x0004AE0A File Offset: 0x0004900A
		public MRSXO1SyncRequestAccessor(IMigrationDataProvider dataProvider, string batchName) : base(dataProvider, batchName)
		{
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004AE14 File Offset: 0x00049014
		public override bool IsSnapshotCompatible(SubscriptionSnapshot subscriptionSnapshot, MigrationJobItem migrationJobItem)
		{
			if (!base.IsSnapshotCompatible(subscriptionSnapshot, migrationJobItem))
			{
				return false;
			}
			SyncSubscriptionSnapshot syncSubscriptionSnapshot = subscriptionSnapshot as SyncSubscriptionSnapshot;
			return syncSubscriptionSnapshot != null && syncSubscriptionSnapshot.Protocol == SyncProtocol.Olc;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0004AE48 File Offset: 0x00049048
		protected override void ApplySubscriptionSettings(NewSyncRequestCommandBase command, string identifier, IMailboxData localMailbox, ISubscriptionSettings endpointSettings, ISubscriptionSettings jobSettings, ISubscriptionSettings jobItemSettings)
		{
			NewSyncRequestCommand newSyncRequestCommand = command as NewSyncRequestCommand;
			if (newSyncRequestCommand != null)
			{
				newSyncRequestCommand.Mailbox = localMailbox.GetIdParameter<MailboxIdParameter>();
				newSyncRequestCommand.Olc = true;
				newSyncRequestCommand.WorkloadType = RequestWorkloadType.Onboarding;
				newSyncRequestCommand.SkipMerging = "InitialConnectionValidation";
			}
		}
	}
}
