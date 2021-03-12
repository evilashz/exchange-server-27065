using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000006 RID: 6
	internal sealed class NoMatch : SpfTerm
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023DE File Offset: 0x000005DE
		public NoMatch(SenderIdValidationContext context) : base(context)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E7 File Offset: 0x000005E7
		public override void Process()
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "No matching mechanism, defaulting to result of Neutral");
			this.context.ValidationCompleted(SenderIdStatus.Neutral);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000240B File Offset: 0x0000060B
		public override SpfTerm Append(SpfTerm newTerm)
		{
			throw new InvalidOperationException("Cannot append anything to the chain after a NoMatch term");
		}
	}
}
