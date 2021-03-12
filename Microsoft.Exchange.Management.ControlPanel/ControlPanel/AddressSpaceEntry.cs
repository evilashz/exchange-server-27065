using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000056 RID: 86
	[DataContract]
	public class AddressSpaceEntry
	{
		// Token: 0x06001A03 RID: 6659 RVA: 0x00053760 File Offset: 0x00051960
		public AddressSpaceEntry(AddressSpace addressSpace)
		{
			this.type = addressSpace.Type;
			this.address = addressSpace.Address;
			this.cost = addressSpace.Cost;
			this.addressSpaceKey = string.Concat(new object[]
			{
				this.type,
				":",
				this.address,
				";",
				this.cost
			});
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x000537DA File Offset: 0x000519DA
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x000537E2 File Offset: 0x000519E2
		[DataMember]
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000537EB File Offset: 0x000519EB
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x000537F3 File Offset: 0x000519F3
		[DataMember]
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000537FC File Offset: 0x000519FC
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x00053804 File Offset: 0x00051A04
		[DataMember]
		public int Cost
		{
			get
			{
				return this.cost;
			}
			set
			{
				this.cost = value;
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0005380D File Offset: 0x00051A0D
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x00053815 File Offset: 0x00051A15
		[DataMember]
		public string AddressSpaceKey
		{
			get
			{
				return this.addressSpaceKey;
			}
			set
			{
				this.addressSpaceKey = value;
			}
		}

		// Token: 0x04001AF9 RID: 6905
		private string type;

		// Token: 0x04001AFA RID: 6906
		private string address;

		// Token: 0x04001AFB RID: 6907
		private int cost;

		// Token: 0x04001AFC RID: 6908
		private string addressSpaceKey;
	}
}
