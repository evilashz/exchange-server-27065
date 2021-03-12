using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200001B RID: 27
	internal sealed class UnknownSpfMechanism : SpfMechanism
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003EC6 File Offset: 0x000020C6
		public UnknownSpfMechanism(SenderIdValidationContext context) : base(context, SenderIdStatus.None)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003ED0 File Offset: 0x000020D0
		public override void Process()
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing unknown mechanism - returning PermError");
			this.context.ValidationCompleted(SenderIdStatus.PermError);
		}
	}
}
