using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000051 RID: 81
	public interface IConfirmableReplicationItem
	{
		// Token: 0x060001B4 RID: 436
		void Confirm(IReplicationService confirmer);
	}
}
