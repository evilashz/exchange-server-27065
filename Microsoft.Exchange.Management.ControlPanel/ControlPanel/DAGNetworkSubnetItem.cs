using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000FC RID: 252
	[DataContract]
	public class DAGNetworkSubnetItem
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x0005CA76 File Offset: 0x0005AC76
		public DAGNetworkSubnetItem(DatabaseAvailabilityGroupNetworkSubnet subnet)
		{
			this.state = subnet.State.ToString();
			this.ipAddress = subnet.SubnetId.ToString();
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0005CAA5 File Offset: 0x0005ACA5
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0005CAAD File Offset: 0x0005ACAD
		[DataMember]
		public string SubnetIPAddress
		{
			get
			{
				return this.ipAddress;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0005CAB4 File Offset: 0x0005ACB4
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0005CABC File Offset: 0x0005ACBC
		[DataMember]
		public string SubnetState
		{
			get
			{
				return this.state;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0005CAC3 File Offset: 0x0005ACC3
		public override string ToString()
		{
			return string.Format("Subnet IP: {0}, Subnet State: {1}", this.ipAddress, this.state);
		}

		// Token: 0x04001C35 RID: 7221
		private readonly string state;

		// Token: 0x04001C36 RID: 7222
		private readonly string ipAddress;
	}
}
