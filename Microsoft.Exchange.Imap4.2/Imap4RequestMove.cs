using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000030 RID: 48
	internal sealed class Imap4RequestMove : Imap4RequestCopy
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		public Imap4RequestMove(Imap4ResponseFactory factory, string tag, string data) : this(factory, tag, data, false)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		public Imap4RequestMove(Imap4ResponseFactory factory, string tag, string data, bool useUids) : base(factory, tag, data, useUids)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_MOVE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_MOVE_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public override ProtocolResponse Process()
		{
			Imap4Mailbox imap4Mailbox;
			List<ProtocolMessage> list;
			Imap4Response imap4Response = base.ProcessParameters(out imap4Mailbox, out list);
			if (imap4Response != null)
			{
				return imap4Response;
			}
			StoreObjectId[] storeObjectIds = base.Factory.SelectedMailbox.DataAccessView.GetStoreObjectIds(list);
			List<int> uidList = null;
			List<int> list2 = null;
			List<int> uidList2 = null;
			using (Folder folder = Folder.Bind(base.Factory.Store, base.Factory.SelectedMailbox.Uid, Imap4Mailbox.FolderProperties))
			{
				AggregateOperationResult aggregateOperationResult = base.Factory.MoveItems(folder, imap4Mailbox.Uid, ((Imap4Server)base.Session.Server).SupportUidPlus, storeObjectIds);
				if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
				{
					return new Imap4Response(this, Imap4Response.Type.no, "MOVE failed or partially completed.");
				}
				imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
				base.ProcessResults(aggregateOperationResult, imap4Mailbox, storeObjectIds, list, false, out uidList, out list2, out uidList2);
			}
			if (base.SupportUidPlus)
			{
				imap4Response.AppendFormat("[COPYUID {0} {1} {2}]", new object[]
				{
					imap4Mailbox.UidValidity,
					Imap4RequestCopy.BuildUidList(uidList),
					Imap4RequestCopy.BuildUidList(uidList2)
				});
				imap4Response.Append("\r\n");
			}
			imap4Response.Append(base.Factory.SelectedMailbox.GetNotifications());
			imap4Response.Append("[*] MOVE completed.");
			return imap4Response;
		}

		// Token: 0x0400016D RID: 365
		private const string MOVEResponseCompleted = "[*] MOVE completed.";

		// Token: 0x0400016E RID: 366
		private const string MOVEResponseNotSucceeded = "MOVE failed or partially completed.";
	}
}
