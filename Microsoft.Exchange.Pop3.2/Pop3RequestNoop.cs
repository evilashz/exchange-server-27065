using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200000E RID: 14
	internal sealed class Pop3RequestNoop : Pop3Request
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000369B File Offset: 0x0000189B
		public Pop3RequestNoop(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_NOOP_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_NOOP_Failures;
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000036CE File Offset: 0x000018CE
		public override ProtocolResponse Process()
		{
			Pop3Server.FaultInjectionTracer.TraceTest(3806735677U);
			return new Pop3Response(Pop3Response.Type.ok);
		}
	}
}
