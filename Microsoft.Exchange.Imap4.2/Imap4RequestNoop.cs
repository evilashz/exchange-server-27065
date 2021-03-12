using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000046 RID: 70
	internal sealed class Imap4RequestNoop : Imap4Request
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00012FEC File Offset: 0x000111EC
		public Imap4RequestNoop(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_NOOP_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_NOOP_Failures;
			base.AllowedStates = (Imap4State.Nonauthenticated | Imap4State.Authenticated | Imap4State.Selected | Imap4State.AuthenticatedButFailed);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00013021 File Offset: 0x00011221
		public override ProtocolResponse Process()
		{
			Imap4Server.FaultInjectionTracer.TraceTest(2732993853U);
			return new Imap4Response(this, Imap4Response.Type.ok, "NOOP completed.");
		}

		// Token: 0x040001F5 RID: 501
		internal const string NOOPResponse = "NOOP completed.";
	}
}
