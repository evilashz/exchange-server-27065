using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000057 RID: 87
	[DataContract]
	public class IPBindingEntry
	{
		// Token: 0x06001A0C RID: 6668 RVA: 0x00053820 File Offset: 0x00051A20
		public IPBindingEntry(IPBinding ipBinding)
		{
			this.address = ipBinding.Address.ToString();
			if (string.Equals(this.address, "0.0.0.0"))
			{
				this.displayAddress = Strings.ConnectorAllAvailableIPv4;
			}
			else if (string.Equals(this.address, "::"))
			{
				this.displayAddress = Strings.ConnectorAllAvailableIPv6;
			}
			else
			{
				this.displayAddress = this.address;
			}
			this.port = ipBinding.Port;
			this.ipBindingKey = this.address + ":" + this.port;
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x000538C5 File Offset: 0x00051AC5
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x000538CD File Offset: 0x00051ACD
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

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000538D6 File Offset: 0x00051AD6
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x000538DE File Offset: 0x00051ADE
		[DataMember]
		public string DisplayAddress
		{
			get
			{
				return this.displayAddress;
			}
			set
			{
				this.displayAddress = value;
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x000538E7 File Offset: 0x00051AE7
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000538EF File Offset: 0x00051AEF
		[DataMember]
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000538F8 File Offset: 0x00051AF8
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x00053900 File Offset: 0x00051B00
		[DataMember]
		public string IPBindingKey
		{
			get
			{
				return this.ipBindingKey;
			}
			set
			{
				this.ipBindingKey = value;
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0005390C File Offset: 0x00051B0C
		public override bool Equals(object obj)
		{
			IPBindingEntry ipbindingEntry = obj as IPBindingEntry;
			return ipbindingEntry != null && string.Compare(ipbindingEntry.Address, this.Address, false) == 0 && ipbindingEntry.Port == this.Port;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00053949 File Offset: 0x00051B49
		public override int GetHashCode()
		{
			return this.Address.GetHashCode() + this.Port;
		}

		// Token: 0x04001AFD RID: 6909
		private string address;

		// Token: 0x04001AFE RID: 6910
		private string displayAddress;

		// Token: 0x04001AFF RID: 6911
		private int port;

		// Token: 0x04001B00 RID: 6912
		private string ipBindingKey;
	}
}
