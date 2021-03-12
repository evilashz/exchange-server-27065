using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000022 RID: 34
	internal sealed class Imap4RequestClose : Imap4Request
	{
		// Token: 0x0600019D RID: 413 RVA: 0x0000ADF2 File Offset: 0x00008FF2
		public Imap4RequestClose(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_CLOSE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_CLOSE_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000AE26 File Offset: 0x00009026
		public override bool NeedsStoreConnection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AE2C File Offset: 0x0000902C
		public override ProtocolResponse Process()
		{
			if (base.Factory.SelectedMailbox.MailboxDoesNotExist)
			{
				base.Factory.SelectedMailbox = null;
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox has been deleted or moved.");
			}
			Imap4Mailbox selectedMailbox = base.Factory.SelectedMailbox;
			if (!selectedMailbox.ExamineMode)
			{
				List<ProtocolMessage> messagesToExpunge = selectedMailbox.GetMessagesToExpunge();
				StoreObjectId[] storeObjectIds = selectedMailbox.DataAccessView.GetStoreObjectIds(messagesToExpunge);
				base.Factory.DeleteMessages(storeObjectIds);
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.ItemsDeleted = new int?(messagesToExpunge.Count);
				}
			}
			base.Factory.SelectedMailbox = null;
			return new Imap4Response(this, Imap4Response.Type.ok, "CLOSE completed.");
		}

		// Token: 0x0400012F RID: 303
		private const string CLOSEResponse = "CLOSE completed.";
	}
}
