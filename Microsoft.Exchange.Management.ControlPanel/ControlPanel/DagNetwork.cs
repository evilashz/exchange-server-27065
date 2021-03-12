using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000FE RID: 254
	[DataContract]
	public class DagNetwork
	{
		// Token: 0x06001EEF RID: 7919 RVA: 0x0005CB40 File Offset: 0x0005AD40
		public DagNetwork(DatabaseAvailabilityGroupNetwork network)
		{
			this.Identity = network.Identity.ToIdentity(network.Name);
			this.ReplicationEnabled = network.ReplicationEnabled;
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0005CB6B File Offset: 0x0005AD6B
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0005CB73 File Offset: 0x0005AD73
		[DataMember]
		public Identity Identity { get; set; }

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0005CB7C File Offset: 0x0005AD7C
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0005CB84 File Offset: 0x0005AD84
		[DataMember]
		public bool ReplicationEnabled { get; set; }
	}
}
