using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200002D RID: 45
	internal sealed class Imap4RequestLogout : Imap4Request
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000C4F1 File Offset: 0x0000A6F1
		public Imap4RequestLogout(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_LOGOUT_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_LOGOUT_Failures;
			base.AllowedStates = (Imap4State.Nonauthenticated | Imap4State.Authenticated | Imap4State.Selected | Imap4State.AuthenticatedButFailed);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C528 File Offset: 0x0000A728
		public override ProtocolResponse Process()
		{
			base.Factory.SelectedMailbox = null;
			base.Factory.SessionState = Imap4State.Disconnected;
			return new Imap4Response(this, Imap4Response.Type.ok, "* BYE Microsoft Exchange Server 2013 IMAP4 server signing off." + "\r\n[*] LOGOUT completed.")
			{
				IsDisconnectResponse = true
			};
		}

		// Token: 0x04000161 RID: 353
		internal const string LOGOUTBanner = "* BYE Microsoft Exchange Server 2013 IMAP4 server signing off.";

		// Token: 0x04000162 RID: 354
		internal const string LOGOUTResponse = "\r\n[*] LOGOUT completed.";
	}
}
