using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000045 RID: 69
	internal sealed class Imap4RequestNamespace : Imap4Request
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00012FAA File Offset: 0x000111AA
		public Imap4RequestNamespace(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_NAMESPACE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_NAMESPACE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00012FDE File Offset: 0x000111DE
		public override ProtocolResponse Process()
		{
			return new Imap4Response(this, Imap4Response.Type.ok, "* NAMESPACE ((\"\" \"/\")) NIL NIL\r\n[*] NAMESPACE completed.");
		}

		// Token: 0x040001F4 RID: 500
		internal const string NAMESPACEResponse = "* NAMESPACE ((\"\" \"/\")) NIL NIL\r\n[*] NAMESPACE completed.";
	}
}
