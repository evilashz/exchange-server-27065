using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000049 RID: 73
	internal sealed class Imap4RequestSubscribe : Imap4RequestWithStringParameters
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0001383A File Offset: 0x00011A3A
		public Imap4RequestSubscribe(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 1)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_SUBSCRIBE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_SUBSCRIBE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00013870 File Offset: 0x00011A70
		public override ProtocolResponse Process()
		{
			string text;
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (string.IsNullOrEmpty(text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (!SubscribeListHelper.TryModifyList(base.Factory, SubscribeListHelper.Operation.Add, text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "SUBSCRIBE failed.");
			}
			return new Imap4Response(this, Imap4Response.Type.ok, "SUBSCRIBE completed.");
		}

		// Token: 0x04000203 RID: 515
		internal const string SUBSCRIBEResponseCompleted = "SUBSCRIBE completed.";

		// Token: 0x04000204 RID: 516
		internal const string SUBSCRIBEResponseFailed = "SUBSCRIBE failed.";
	}
}
