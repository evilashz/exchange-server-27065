using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000052 RID: 82
	public interface IMetaConfirmation : IConfirmableReplicationItem
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B5 RID: 437
		int CustomerId { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B6 RID: 438
		string DatacenterName { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		ReplicationStatus Status { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B9 RID: 441
		IMetaReplicationKey Key { get; }
	}
}
