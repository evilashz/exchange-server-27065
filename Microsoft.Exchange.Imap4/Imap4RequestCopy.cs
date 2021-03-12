using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200002F RID: 47
	internal class Imap4RequestCopy : Imap4RequestWithMessageSetSupport
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000C8B3 File Offset: 0x0000AAB3
		public Imap4RequestCopy(Imap4ResponseFactory factory, string tag, string data) : this(factory, tag, data, false)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C8BF File Offset: 0x0000AABF
		public Imap4RequestCopy(Imap4ResponseFactory factory, string tag, string data, bool useUids) : base(factory, tag, data, useUids, 2, 2)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_COPY_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_COPY_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		public override ProtocolResponse Process()
		{
			Imap4Mailbox imap4Mailbox;
			List<ProtocolMessage> list;
			Imap4Response imap4Response = this.ProcessParameters(out imap4Mailbox, out list);
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
				AggregateOperationResult aggregateOperationResult = base.Factory.CopyItems(folder, imap4Mailbox.Uid, ((Imap4Server)base.Session.Server).SupportUidPlus, storeObjectIds);
				if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
				{
					return new Imap4Response(this, Imap4Response.Type.no, "COPY failed or partially completed.");
				}
				imap4Response = new Imap4Response(this, Imap4Response.Type.ok, "COPY completed.");
				this.ProcessResults(aggregateOperationResult, imap4Mailbox, storeObjectIds, list, true, out uidList, out list2, out uidList2);
			}
			if (base.SupportUidPlus)
			{
				imap4Response.ResponseCodes = string.Format("[COPYUID {0} {1} {2}]", imap4Mailbox.UidValidity, Imap4RequestCopy.BuildUidList(uidList), Imap4RequestCopy.BuildUidList(uidList2));
			}
			return imap4Response;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000CA14 File Offset: 0x0000AC14
		protected static StringBuilder BuildUidList(List<int> uidList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (uidList.Count == 0)
			{
				return stringBuilder;
			}
			uidList.Sort();
			int num = uidList[0] - 1;
			int firstInSecuence = uidList[0];
			foreach (int num2 in uidList)
			{
				if (num2 != num + 1)
				{
					Imap4RequestCopy.AddUids(stringBuilder, firstInSecuence, num);
					firstInSecuence = num2;
				}
				num = num2;
			}
			Imap4RequestCopy.AddUids(stringBuilder, firstInSecuence, num);
			return stringBuilder;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		protected static void AddUids(StringBuilder uids, int firstInSecuence, int lastInSecuence)
		{
			if (uids.Length > 0)
			{
				uids.Append(',');
			}
			if (firstInSecuence != lastInSecuence)
			{
				uids.Append(firstInSecuence);
				uids.Append(':');
				uids.Append(lastInSecuence);
				return;
			}
			uids.Append(lastInSecuence);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000CADC File Offset: 0x0000ACDC
		protected Imap4Response ProcessParameters(out Imap4Mailbox destinationMailbox, out List<ProtocolMessage> messages)
		{
			destinationMailbox = null;
			messages = null;
			if (base.Factory.SelectedMailbox.MailboxDoesNotExist)
			{
				base.Factory.SelectedMailbox = null;
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox has been deleted or moved.");
			}
			string path;
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[1], out path))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (!((Imap4Session)base.Factory.Session).TryGetMailbox(path, out destinationMailbox, false))
			{
				return new Imap4Response(this, Imap4Response.Type.no, "The destination mailbox could not be found.")
				{
					ResponseCodes = "[TRYCREATE]"
				};
			}
			if (destinationMailbox.IsNonselect)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			messages = base.GetMessages(base.ArrayOfArguments[0]);
			if (this.ParseResult == ParseResult.invalidMessageSet)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "[*] The specified message set is invalid.");
			}
			if (base.MessageSetIsInvalid)
			{
				return new Imap4Response(this, Imap4Response.Type.no, "[*] The specified message set is invalid.");
			}
			if (base.DeletedMessages)
			{
				return new Imap4Response(this, Imap4Response.Type.no, "[*] Some of the requested messages no longer exist.");
			}
			return null;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		protected void ProcessResults(AggregateOperationResult result, Imap4Mailbox destinationMailbox, StoreObjectId[] messageIds, List<ProtocolMessage> messages, bool copy, out List<int> sourceObjectUids, out List<int> sourceObjectIds, out List<int> resultUids)
		{
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(messages.Count);
			}
			resultUids = (base.SupportUidPlus ? new List<int>(messageIds.Length) : null);
			sourceObjectUids = null;
			sourceObjectIds = null;
			bool flag = destinationMailbox.Equals(base.Factory.SelectedMailbox);
			if (base.SupportUidPlus || flag)
			{
				List<StoreObjectId> list = new List<StoreObjectId>(messageIds.Length);
				if (result.GroupOperationResults != null)
				{
					foreach (GroupOperationResult groupOperationResult in result.GroupOperationResults)
					{
						if (groupOperationResult.ResultObjectIds != null)
						{
							list.AddRange(groupOperationResult.ResultObjectIds);
						}
					}
				}
				Imap4DataAccessViewHandler imap4DataAccessViewHandler = null;
				Folder folder = null;
				try
				{
					Imap4DataAccessViewHandler imap4DataAccessViewHandler2;
					if (flag)
					{
						imap4DataAccessViewHandler2 = base.Factory.SelectedMailbox.DataAccessView;
					}
					else
					{
						folder = Folder.Bind(base.Factory.Store, destinationMailbox.Uid, Imap4Mailbox.FolderProperties);
						imap4DataAccessViewHandler = destinationMailbox.OpenDataAccessView(folder);
						imap4DataAccessViewHandler2 = imap4DataAccessViewHandler;
					}
					QueryResult tableView = imap4DataAccessViewHandler2.TableView;
					foreach (StoreObjectId propertyValue in list)
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Id, propertyValue);
						tableView.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
						object[][] rows = tableView.GetRows(1);
						if (rows.Length == 1)
						{
							if (copy && flag)
							{
								Imap4Flags flags = Imap4FlagsHelper.Parse(rows[0][3], rows[0][4], rows[0][5], rows[0][6], rows[0][7], rows[0][8], rows[0][9]);
								base.Factory.SelectedMailbox.AddMessage((int)rows[0][1], (int)rows[0][2], flags, (int)rows[0][10]);
							}
							else if (!copy && !flag)
							{
								base.Factory.SelectedMailbox.DeleteMessage((int)rows[0][2]);
							}
							if (base.SupportUidPlus)
							{
								resultUids.Add((int)rows[0][1]);
							}
						}
					}
				}
				finally
				{
					if (imap4DataAccessViewHandler != null)
					{
						imap4DataAccessViewHandler.Dispose();
						imap4DataAccessViewHandler = null;
					}
					if (folder != null)
					{
						folder.Dispose();
					}
				}
				if (base.SupportUidPlus)
				{
					sourceObjectUids = new List<int>(messages.Count);
					sourceObjectIds = new List<int>(messages.Count);
					foreach (ProtocolMessage protocolMessage in messages)
					{
						sourceObjectUids.Add(protocolMessage.Id);
						sourceObjectIds.Add(protocolMessage.Index);
					}
				}
				if (!copy && flag)
				{
					resultUids.AddRange(sourceObjectUids);
					sourceObjectIds.Clear();
				}
			}
		}

		// Token: 0x04000169 RID: 361
		protected const string COPYUID = "[COPYUID {0} {1} {2}]";

		// Token: 0x0400016A RID: 362
		private const string COPYResponseCompleted = "COPY completed.";

		// Token: 0x0400016B RID: 363
		private const string DestinationMailboxNotFound = "The destination mailbox could not be found.";

		// Token: 0x0400016C RID: 364
		private const string COPYResponseNotSucceeded = "COPY failed or partially completed.";
	}
}
