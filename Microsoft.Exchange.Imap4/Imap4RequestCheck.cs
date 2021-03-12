using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000021 RID: 33
	internal sealed class Imap4RequestCheck : Imap4Request
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public Imap4RequestCheck(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_CHECK_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_CHECK_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		public override ProtocolResponse Process()
		{
			return new Imap4Response(this, Imap4Response.Type.ok, "CHECK completed.");
		}

		// Token: 0x0400012E RID: 302
		private const string CHECKResponse = "CHECK completed.";
	}
}
