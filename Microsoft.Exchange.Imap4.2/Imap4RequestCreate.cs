using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000023 RID: 35
	internal sealed class Imap4RequestCreate : Imap4RequestWithStringParameters
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000AED9 File Offset: 0x000090D9
		public Imap4RequestCreate(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 1)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_CREATE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_CREATE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000AF10 File Offset: 0x00009110
		public override ProtocolResponse Process()
		{
			string text;
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (string.Compare(text, "INBOX", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox already exists.");
			}
			Imap4Mailbox imap4Mailbox;
			if (!((Imap4Session)base.Factory.Session).TryGetMailbox(text, out imap4Mailbox, true))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			ProtocolBaseServices.SessionTracer.TraceDebug<Imap4Mailbox>(base.Session.SessionId, "parentMailbox {0}.", imap4Mailbox);
			if (string.Compare(text, imap4Mailbox.FullPath, StringComparison.OrdinalIgnoreCase) != 0)
			{
				if (!imap4Mailbox.IsRoot)
				{
					text = text.Substring(imap4Mailbox.FullPath.Length);
				}
				Stack<string> stack = new Stack<string>();
				int num;
				while ((num = text.LastIndexOf('/')) > -1)
				{
					stack.Push(text.Substring(num + 1));
					text = text.Substring(0, num);
				}
				if (!string.IsNullOrEmpty(text))
				{
					stack.Push(text);
				}
				ProtocolBaseServices.Assert(stack.Count > 0, "There is nothing to create!", new object[0]);
				try
				{
					StoreObjectId parentFolderId = imap4Mailbox.Uid;
					while (stack.Count > 0)
					{
						using (Folder folder = Folder.Create(base.Factory.Store, parentFolderId, StoreObjectType.Folder))
						{
							folder.DisplayName = stack.Pop();
							folder[FolderSchema.ImapLastSeenArticleId] = 0;
							folder[StoreObjectSchema.ContainerClass] = "IPF.Note";
							folder.Save();
							folder.Load();
							parentFolderId = folder.Id.ObjectId;
						}
					}
				}
				catch (ObjectValidationException)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				((Imap4Session)base.Factory.Session).NeedToRebuildFolderTable = true;
				return new Imap4Response(this, Imap4Response.Type.ok, "CREATE completed.");
			}
			if (imap4Mailbox.IsNonselect)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			return new Imap4Response(this, Imap4Response.Type.no, "Mailbox already exists.");
		}

		// Token: 0x04000130 RID: 304
		private const string CREATEResponseCompleted = "CREATE completed.";

		// Token: 0x04000131 RID: 305
		private const string CREATEResponseAlreadyExists = "Mailbox already exists.";
	}
}
