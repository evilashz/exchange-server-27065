using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000018 RID: 24
	internal sealed class IPSpfMechanism : SpfMechanism
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003B23 File Offset: 0x00001D23
		public IPSpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix, IPNetwork network) : base(context, prefix)
		{
			this.network = network;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003B34 File Offset: 0x00001D34
		public override void Process()
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing IP4/IP6 mechanism");
			if (this.network.Contains(this.context.BaseContext.IPAddress))
			{
				base.SetMatchResult();
				return;
			}
			base.ProcessNextTerm();
		}

		// Token: 0x04000043 RID: 67
		private IPNetwork network;
	}
}
