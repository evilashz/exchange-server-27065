using System;
using System.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BEA RID: 3050
	internal class DnsAddressRecord : DnsRecord
	{
		// Token: 0x060042D4 RID: 17108 RVA: 0x000B20D8 File Offset: 0x000B02D8
		protected DnsAddressRecord(string name) : base(name)
		{
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000B20E1 File Offset: 0x000B02E1
		protected DnsAddressRecord(string name, IPAddress address) : base(name)
		{
			this.address = address;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000B20F1 File Offset: 0x000B02F1
		protected DnsAddressRecord(Win32DnsRecordHeader header) : base(header)
		{
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x000B20FA File Offset: 0x000B02FA
		public IPAddress IPAddress
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000B2102 File Offset: 0x000B0302
		public override string ToString()
		{
			return base.ToString() + " " + this.address.ToString();
		}

		// Token: 0x04003900 RID: 14592
		protected IPAddress address;
	}
}
