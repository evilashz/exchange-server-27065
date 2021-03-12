using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000013 RID: 19
	internal sealed class AllSpfMechanism : SpfMechanism
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000376D File Offset: 0x0000196D
		public AllSpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix) : base(context, prefix)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003777 File Offset: 0x00001977
		public override void Process()
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing All mechanism");
			base.SetMatchResult();
		}
	}
}
