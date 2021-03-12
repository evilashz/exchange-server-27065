using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.PerimeterConfig
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class DCPerimeterConfig
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000C77A File Offset: 0x0000A97A
		public DCPerimeterConfig(IPAddress[] inboundIPAddresses, IPAddress[] outboundIPAddresses)
		{
			if (inboundIPAddresses != null)
			{
				this.inboundIPAddresses = new MultiValuedProperty<IPAddress>(inboundIPAddresses);
			}
			else
			{
				this.inboundIPAddresses = MultiValuedProperty<IPAddress>.Empty;
			}
			if (outboundIPAddresses != null)
			{
				this.outboundIPAddresses = new MultiValuedProperty<IPAddress>(outboundIPAddresses);
				return;
			}
			this.outboundIPAddresses = MultiValuedProperty<IPAddress>.Empty;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C7B9 File Offset: 0x0000A9B9
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000C7C1 File Offset: 0x0000A9C1
		[Parameter]
		public MultiValuedProperty<IPAddress> InboundIPAddresses
		{
			get
			{
				return this.inboundIPAddresses;
			}
			set
			{
				this.inboundIPAddresses = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		[Parameter]
		public MultiValuedProperty<IPAddress> OutboundIPAddresses
		{
			get
			{
				return this.outboundIPAddresses;
			}
			set
			{
				this.outboundIPAddresses = value;
			}
		}

		// Token: 0x04000130 RID: 304
		private MultiValuedProperty<IPAddress> inboundIPAddresses;

		// Token: 0x04000131 RID: 305
		private MultiValuedProperty<IPAddress> outboundIPAddresses;
	}
}
