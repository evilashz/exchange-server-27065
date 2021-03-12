using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000027 RID: 39
	internal sealed class Imap4RequestExamine : Imap4RequestSelect
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000B571 File Offset: 0x00009771
		public Imap4RequestExamine(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_EXAMINE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_EXAMINE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B5A5 File Offset: 0x000097A5
		public override ProtocolResponse Process()
		{
			return this.DoSelect(true);
		}
	}
}
