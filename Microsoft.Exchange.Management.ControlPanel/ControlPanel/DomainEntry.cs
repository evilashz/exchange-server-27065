using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000054 RID: 84
	[DataContract]
	public class DomainEntry
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x00053630 File Offset: 0x00051830
		internal DomainEntry(string domain)
		{
			this.address = domain;
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0005363F File Offset: 0x0005183F
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x00053647 File Offset: 0x00051847
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

		// Token: 0x060019FF RID: 6655 RVA: 0x00053650 File Offset: 0x00051850
		public override bool Equals(object obj)
		{
			DomainEntry domainEntry = obj as DomainEntry;
			return domainEntry != null && string.Compare(domainEntry.Address, this.Address, true) == 0;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0005367E File Offset: 0x0005187E
		public override int GetHashCode()
		{
			return this.Address.GetHashCode();
		}

		// Token: 0x04001AF8 RID: 6904
		private string address;
	}
}
