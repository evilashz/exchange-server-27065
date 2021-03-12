using System;
using System.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000274 RID: 628
	internal class TargetHostTraceWrapper : ITraceWrapper<TargetHost>
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x0006FCC0 File Offset: 0x0006DEC0
		public TargetHostTraceWrapper()
		{
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0006FCC8 File Offset: 0x0006DEC8
		public TargetHostTraceWrapper(TargetHost host)
		{
			this.host = host;
		}

		// Token: 0x1700072F RID: 1839
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0006FCD7 File Offset: 0x0006DED7
		public TargetHost Element
		{
			set
			{
				this.host = value;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0006FCE0 File Offset: 0x0006DEE0
		public override string ToString()
		{
			if (this.host == null)
			{
				return "<null>";
			}
			return string.Format("Target host {{ name='{0}' addresses={{ {1} }} TTL={2} }}", this.host.Name ?? "<null>", new ListTracer<IPAddress>(this.host.IPAddresses), this.host.TimeToLive);
		}

		// Token: 0x04000CDC RID: 3292
		private TargetHost host;
	}
}
