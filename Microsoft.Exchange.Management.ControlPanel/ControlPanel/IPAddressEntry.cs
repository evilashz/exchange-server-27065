using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	public class IPAddressEntry
	{
		// Token: 0x06001993 RID: 6547 RVA: 0x00052071 File Offset: 0x00050271
		public IPAddressEntry(IPRange range)
		{
			this.address = range.ToString();
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00052085 File Offset: 0x00050285
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x0005208D File Offset: 0x0005028D
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

		// Token: 0x06001996 RID: 6550 RVA: 0x00052098 File Offset: 0x00050298
		public override bool Equals(object obj)
		{
			IPAddressEntry ipaddressEntry = obj as IPAddressEntry;
			return ipaddressEntry != null && string.Compare(ipaddressEntry.Address, this.Address, true) == 0;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000520C6 File Offset: 0x000502C6
		public override int GetHashCode()
		{
			return this.Address.GetHashCode();
		}

		// Token: 0x04001AC5 RID: 6853
		private string address;
	}
}
