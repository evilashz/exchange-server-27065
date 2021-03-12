using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000047 RID: 71
	internal sealed class Imap4RequestRename : Imap4RequestWithStringParameters
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0001303E File Offset: 0x0001123E
		public Imap4RequestRename(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 2, 2)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_RENAME_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_RENAME_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00013074 File Offset: 0x00011274
		public override ProtocolResponse Process()
		{
			string[] array = new string[3];
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out array[2]))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[1], out array[0]))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			int num = array[0].LastIndexOf('/');
			string text;
			if (num > -1)
			{
				array[1] = array[0].Substring(0, num);
				text = array[0].Substring(num + 1);
			}
			else
			{
				array[1] = string.Empty;
				text = array[0];
			}
			Imap4Session imap4Session = (Imap4Session)base.Factory.Session;
			Imap4Mailbox imap4Mailbox;
			imap4Session.TryGetMailbox(array[2], out imap4Mailbox, false);
			Imap4Mailbox imap4Mailbox2;
			imap4Session.TryGetMailbox(array[0], out imap4Mailbox2, false);
			Imap4Mailbox imap4Mailbox3;
			imap4Session.TryGetMailbox(array[1], out imap4Mailbox3, false);
			if (imap4Mailbox2 != null)
			{
				if (imap4Mailbox2.IsNonselect)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				return new Imap4Response(this, Imap4Response.Type.no, "Target folder already exists.");
			}
			else
			{
				if (imap4Mailbox == null)
				{
					return new Imap4Response(this, Imap4Response.Type.no, "Source folder is not found.");
				}
				if (imap4Mailbox.IsNonselect)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				if (imap4Mailbox3 == null)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				Imap4Mailbox imap4Mailbox4 = imap4Mailbox3;
				while (!imap4Mailbox4.IsRoot)
				{
					imap4Mailbox4 = imap4Mailbox4.ParentMailbox;
					if (imap4Mailbox.Equals(imap4Mailbox4))
					{
						return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
					}
				}
				try
				{
					if (imap4Mailbox.IsInbox)
					{
						return this.RenameInbox(text);
					}
					using (Folder folder = Folder.Bind(base.Factory.Store, imap4Mailbox.Uid, null))
					{
						folder.DisplayName = text;
						folder.Save();
					}
					if (!imap4Mailbox3.Equals(imap4Mailbox.ParentMailbox))
					{
						using (Folder folder2 = Folder.Bind(base.Factory.Store, imap4Mailbox.ParentMailboxUid, null))
						{
							folder2.MoveFolder(imap4Mailbox3.Uid, imap4Mailbox.Uid);
						}
					}
					if (imap4Mailbox.Equals(base.Factory.SelectedMailbox))
					{
						base.Factory.SelectedMailbox.ParentMailbox = imap4Mailbox3;
					}
				}
				catch (ObjectValidationException)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				((Imap4Session)base.Factory.Session).NeedToRebuildFolderTable = true;
				return new Imap4Response(this, Imap4Response.Type.ok, "RENAME completed.");
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000132F8 File Offset: 0x000114F8
		private Imap4Response RenameInbox(string destinationName)
		{
			StoreObjectId defaultFolderId = base.Factory.Store.GetDefaultFolderId(DefaultFolderType.Root);
			StoreObjectId defaultFolderId2 = base.Factory.Store.GetDefaultFolderId(DefaultFolderType.Inbox);
			using (Folder folder = Folder.Bind(base.Factory.Store, defaultFolderId2, Imap4Mailbox.FolderProperties))
			{
				using (Folder folder2 = Folder.Create(base.Factory.Store, defaultFolderId, StoreObjectType.Folder))
				{
					folder2.DisplayName = destinationName;
					folder2.Save();
					folder2.Load();
					List<StoreObjectId> list = new List<StoreObjectId>(256);
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
					{
						ItemSchema.Id
					}))
					{
						object[][] rows;
						do
						{
							rows = queryResult.GetRows(10000);
							for (int i = 0; i < rows.Length; i++)
							{
								list.Add(((VersionedId)rows[i][0]).ObjectId);
							}
						}
						while (rows.Length > 0);
					}
					if (base.Factory.MoveItems(folder, folder2.Id.ObjectId, list.ToArray()) != OperationResult.Succeeded)
					{
						return new Imap4Response(this, Imap4Response.Type.no, "RENAME failed.");
					}
					if (base.Factory.SelectedMailbox != null && base.Factory.SelectedMailbox.IsInbox)
					{
						base.Factory.SelectedMailbox.ExploreMailbox(true);
					}
				}
			}
			((Imap4Session)base.Factory.Session).NeedToRebuildFolderTable = true;
			return new Imap4Response(this, Imap4Response.Type.ok, "RENAME completed.");
		}

		// Token: 0x040001F6 RID: 502
		private const string RENAMEResponseCompleted = "RENAME completed.";

		// Token: 0x040001F7 RID: 503
		private const string RENAMEResponseNotFound = "Source folder is not found.";

		// Token: 0x040001F8 RID: 504
		private const string RENAMEResponseAlreadyExists = "Target folder already exists.";

		// Token: 0x040001F9 RID: 505
		private const string RENAMEResponseNotSucceeded = "RENAME failed.";

		// Token: 0x040001FA RID: 506
		private const int IdxDestination = 0;

		// Token: 0x040001FB RID: 507
		private const int IdxDestinationParent = 1;

		// Token: 0x040001FC RID: 508
		private const int IdxSource = 2;
	}
}
