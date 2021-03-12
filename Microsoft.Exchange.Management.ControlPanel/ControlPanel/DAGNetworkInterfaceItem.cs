using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000FD RID: 253
	[DataContract]
	public class DAGNetworkInterfaceItem
	{
		// Token: 0x06001EE9 RID: 7913 RVA: 0x0005CADB File Offset: 0x0005ACDB
		public DAGNetworkInterfaceItem(DatabaseAvailabilityGroupNetworkInterface networkInterface)
		{
			this.state = networkInterface.State.ToString();
			this.ipAddress = networkInterface.IPAddress.ToString();
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0005CB0A File Offset: 0x0005AD0A
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x0005CB12 File Offset: 0x0005AD12
		[DataMember]
		public string InterfaceState
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

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0005CB19 File Offset: 0x0005AD19
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x0005CB21 File Offset: 0x0005AD21
		[DataMember]
		public string InterfaceIPAddress
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

		// Token: 0x06001EEE RID: 7918 RVA: 0x0005CB28 File Offset: 0x0005AD28
		public override string ToString()
		{
			return string.Format("Interface IP address: {0}, Interface State: {1}", this.ipAddress, this.state);
		}

		// Token: 0x04001C37 RID: 7223
		private readonly string state;

		// Token: 0x04001C38 RID: 7224
		private readonly string ipAddress;
	}
}
