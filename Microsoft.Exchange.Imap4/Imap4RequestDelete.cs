using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000024 RID: 36
	internal sealed class Imap4RequestDelete : Imap4RequestWithStringParameters
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000B114 File Offset: 0x00009314
		public Imap4RequestDelete(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 1)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_DELETE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_DELETE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B14C File Offset: 0x0000934C
		public override ProtocolResponse Process()
		{
			string path;
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out path))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			Imap4Mailbox imap4Mailbox;
			if (!((Imap4Session)base.Factory.Session).TryGetMailbox(path, out imap4Mailbox, false))
			{
				return new Imap4Response(this, Imap4Response.Type.no, "The requested item could not be found.");
			}
			if (imap4Mailbox.IsNonselect)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (imap4Mailbox.HasChildren)
			{
				return new Imap4Response(this, Imap4Response.Type.no, "The mailbox has inferiors.");
			}
			if (imap4Mailbox.IsInbox)
			{
				return new Imap4Response(this, Imap4Response.Type.no, string.Format("The special name {0} cannot be used in this context.", Imap4Mailbox.PathToString(base.ArrayOfArguments[0])));
			}
			if (imap4Mailbox.Equals(base.Factory.SelectedMailbox))
			{
				base.Factory.SelectedMailbox = null;
			}
			AggregateOperationResult aggregateOperationResult = base.Factory.Store.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
			{
				imap4Mailbox.Uid
			});
			Imap4Response.Type imap4ResponseType;
			string input;
			if (aggregateOperationResult.OperationResult == OperationResult.Succeeded)
			{
				imap4ResponseType = Imap4Response.Type.ok;
				input = "DELETE completed.";
			}
			else
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<OperationResult>(base.Session.SessionId, "Delete did not succeed: {0}.", aggregateOperationResult.OperationResult);
				imap4ResponseType = Imap4Response.Type.no;
				input = "DELETE failed.";
			}
			((Imap4Session)base.Factory.Session).NeedToRebuildFolderTable = true;
			return new Imap4Response(this, imap4ResponseType, input);
		}

		// Token: 0x04000132 RID: 306
		private const string DELETEResponseCompleted = "DELETE completed.";

		// Token: 0x04000133 RID: 307
		private const string DELETEResponseFailed = "DELETE failed.";

		// Token: 0x04000134 RID: 308
		private const string DELETEResponseHasChildren = "The mailbox has inferiors.";

		// Token: 0x04000135 RID: 309
		private const string DELETEResponseUnableToDelete = "The special name {0} cannot be used in this context.";

		// Token: 0x04000136 RID: 310
		private const string DELETEResponseDoesNotExist = "The requested item could not be found.";
	}
}
