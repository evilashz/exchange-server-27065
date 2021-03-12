using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000042 RID: 66
	internal sealed class Imap4RequestStore : Imap4RequestWithMessageSetSupport
	{
		// Token: 0x06000282 RID: 642 RVA: 0x000127C8 File Offset: 0x000109C8
		public Imap4RequestStore(Imap4ResponseFactory factory, string tag, string data) : this(factory, tag, data, false)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000127D4 File Offset: 0x000109D4
		public Imap4RequestStore(Imap4ResponseFactory factory, string tag, string data, bool useUids) : base(factory, tag, data, useUids, 3, 3)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_STORE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_STORE_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0001280C File Offset: 0x00010A0C
		public override bool AllowsExpungeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00012810 File Offset: 0x00010A10
		public override ProtocolResponse Process()
		{
			if (base.Factory.SelectedMailbox.MailboxDoesNotExist)
			{
				base.Factory.SelectedMailbox = null;
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox has been deleted or moved.");
			}
			if (base.Factory.SelectedMailbox.ExamineMode)
			{
				return new Imap4Response(this, Imap4Response.Type.no, "Command received in Invalid state.");
			}
			Imap4Response imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			List<ProtocolMessage> messages = base.GetMessages(base.ArrayOfArguments[0]);
			if (this.ParseResult == ParseResult.invalidMessageSet)
			{
				imap4Response.Append("[*] The specified message set is invalid.");
				imap4Response.ResponseType = Imap4Response.Type.bad;
				return imap4Response;
			}
			string input = base.ArrayOfArguments[1];
			Match match = Imap4RequestStore.dataItemRegEx.Match(input);
			if (!match.Success)
			{
				imap4Response.Append("Command Argument Error. 11");
				imap4Response.ResponseType = Imap4Response.Type.bad;
				return imap4Response;
			}
			string value;
			Imap4RequestStore.Action action;
			if ((value = match.Groups["action"].Value) != null)
			{
				if (value == "+")
				{
					action = Imap4RequestStore.Action.Add;
					goto IL_FC;
				}
				if (value == "-")
				{
					action = Imap4RequestStore.Action.Remove;
					goto IL_FC;
				}
			}
			action = Imap4RequestStore.Action.Set;
			IL_FC:
			bool flag = match.Groups["silent"].Value.Length != 0;
			Imap4Flags imap4Flags;
			bool flag2;
			if (!Imap4FlagsHelper.TryParse(base.ArrayOfArguments[2], out imap4Flags, out flag2) || flag2)
			{
				imap4Response.Append("Command Argument Error. 11");
				imap4Response.ResponseType = Imap4Response.Type.bad;
				return imap4Response;
			}
			if ((imap4Flags & Imap4Flags.Recent) != Imap4Flags.None)
			{
				imap4Response.Append("Command Argument Error. 11");
				imap4Response.ResponseType = Imap4Response.Type.bad;
				return imap4Response;
			}
			ProtocolResponse result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<Imap4Response>(imap4Response);
				StoreObjectId[] storeObjectIds = base.Factory.SelectedMailbox.DataAccessView.GetStoreObjectIds(messages);
				try
				{
					using (Folder folder = Folder.Bind(base.Factory.Store, base.Factory.SelectedMailbox.Uid, Imap4Mailbox.FolderProperties))
					{
						List<StoreId> list = new List<StoreId>(Math.Min(messages.Count, 256));
						List<StoreId> list2 = new List<StoreId>(Math.Min(messages.Count, 256));
						for (int i = 0; i < messages.Count; i++)
						{
							if (storeObjectIds[i] != null)
							{
								this.StoreFlags(folder, action, flag, messages[i] as Imap4Message, storeObjectIds[i], imap4Flags, list, list2);
								if (list.Count >= 256)
								{
									folder.MarkAsRead(base.Factory.SuppressReadReceipt, base.Factory.SuppressReadReceipt, list.ToArray());
									list.Clear();
								}
								if (list2.Count >= 256)
								{
									folder.MarkAsUnread(base.Factory.SuppressReadReceipt, list2.ToArray());
									list2.Clear();
								}
							}
						}
						if (list.Count > 0)
						{
							folder.MarkAsRead(base.Factory.SuppressReadReceipt, base.Factory.SuppressReadReceipt, list.ToArray());
						}
						if (list2.Count > 0)
						{
							folder.MarkAsUnread(base.Factory.SuppressReadReceipt, list2.ToArray());
						}
					}
				}
				catch (ObjectNotFoundException exception)
				{
					base.Factory.LogHandledException(exception);
				}
				base.Factory.SelectedMailbox.UpdateMessageCounters();
				if (!flag)
				{
					base.Factory.SelectedMailbox.SetNotifications();
				}
				if (base.MessageSetIsInvalid)
				{
					imap4Response.Append("[*] The specified message set is invalid.");
					imap4Response.ResponseType = Imap4Response.Type.no;
				}
				else if (base.DeletedMessages)
				{
					imap4Response.Append("[*] Some of the requested messages no longer exist.");
					imap4Response.ResponseType = Imap4Response.Type.no;
				}
				else
				{
					imap4Response.Append("STORE completed.");
				}
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.RowsProcessed = new int?(messages.Count);
				}
				disposeGuard.Success();
				result = imap4Response;
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00012C1C File Offset: 0x00010E1C
		private void StoreFlags(Folder folder, Imap4RequestStore.Action action, bool silent, Imap4Message message, StoreObjectId storeObjectId, Imap4Flags flagList, List<StoreId> markAsRead, List<StoreId> markAsUnread)
		{
			Imap4Flags imap4Flags = message.Flags & ~Imap4Flags.Recent;
			switch (action)
			{
			case Imap4RequestStore.Action.Set:
				imap4Flags = flagList;
				break;
			case Imap4RequestStore.Action.Add:
				imap4Flags |= flagList;
				break;
			case Imap4RequestStore.Action.Remove:
				imap4Flags &= ~flagList;
				break;
			}
			if ((message.Flags & ~Imap4Flags.Recent) != imap4Flags)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int, Imap4Flags, Imap4Flags>(base.Session.SessionId, "Message[{0}] old flags {1} new flags {2}", message.Index, message.Flags, imap4Flags);
				message.SaveFlags(storeObjectId, folder, imap4Flags, markAsRead, markAsUnread);
			}
			else
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int, Imap4Flags>(base.Session.SessionId, "Message[{0}] flags {1} were not changed", message.Index, message.Flags);
			}
			message.FlagsHaveBeenChanged = !silent;
		}

		// Token: 0x040001ED RID: 493
		private const string STOREResponseCompleted = "STORE completed.";

		// Token: 0x040001EE RID: 494
		private static Regex dataItemRegEx = new Regex("\\A(?<action>[\\+\\-]?)flags(?<silent>(.silent)?)\\Z", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x02000043 RID: 67
		private enum Action
		{
			// Token: 0x040001F0 RID: 496
			Set,
			// Token: 0x040001F1 RID: 497
			Add,
			// Token: 0x040001F2 RID: 498
			Remove
		}
	}
}
