using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007F0 RID: 2032
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxAssociationPerformanceTracker : IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x06004C16 RID: 19478
		void IncrementAssociationsRead();

		// Token: 0x06004C17 RID: 19479
		void IncrementAssociationsCreated();

		// Token: 0x06004C18 RID: 19480
		void IncrementAssociationsUpdated();

		// Token: 0x06004C19 RID: 19481
		void IncrementAssociationsDeleted();

		// Token: 0x06004C1A RID: 19482
		void IncrementFailedAssociationsSearch();

		// Token: 0x06004C1B RID: 19483
		void IncrementNonUniqueAssociationsFound();

		// Token: 0x06004C1C RID: 19484
		void IncrementAutoSubscribedMembers();

		// Token: 0x06004C1D RID: 19485
		void IncrementMissingLegacyDns();

		// Token: 0x06004C1E RID: 19486
		void IncrementAssociationReplicationAttempts();

		// Token: 0x06004C1F RID: 19487
		void IncrementFailedAssociationReplications();

		// Token: 0x06004C20 RID: 19488
		void SetNewSessionRequired(bool isRequired);

		// Token: 0x06004C21 RID: 19489
		void SetNewSessionWrongServer(bool isWrongServer);

		// Token: 0x06004C22 RID: 19490
		void SetNewSessionLatency(long milliseconds);

		// Token: 0x06004C23 RID: 19491
		void SetAADQueryLatency(long milliseconds);
	}
}
