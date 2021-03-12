using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200004A RID: 74
	internal sealed class Imap4RequestUnsubscribe : Imap4RequestWithStringParameters
	{
		// Token: 0x0600029F RID: 671 RVA: 0x000138DC File Offset: 0x00011ADC
		public Imap4RequestUnsubscribe(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 1)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_UNSUBSCRIBE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_UNSUBSCRIBE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00013914 File Offset: 0x00011B14
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
			if (!SubscribeListHelper.TryModifyList(base.Factory, SubscribeListHelper.Operation.Remove, text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "UNSUBSCRIBE failed.");
			}
			return new Imap4Response(this, Imap4Response.Type.ok, "UNSUBSCRIBE completed.");
		}

		// Token: 0x04000205 RID: 517
		internal const string UNSUBSCRIBEResponseCompleted = "UNSUBSCRIBE completed.";

		// Token: 0x04000206 RID: 518
		internal const string UNSUBSCRIBEResponseFailed = "UNSUBSCRIBE failed.";
	}
}
