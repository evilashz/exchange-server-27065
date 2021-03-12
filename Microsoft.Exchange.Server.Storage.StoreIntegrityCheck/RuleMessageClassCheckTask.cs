using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000030 RID: 48
	public class RuleMessageClassCheckTask : IntegrityCheckTaskBase
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00007D24 File Offset: 0x00005F24
		public RuleMessageClassCheckTask(IJobExecutionTracker tracker) : base(tracker)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00007D7C File Offset: 0x00005F7C
		public override string TaskName
		{
			get
			{
				return "RuleMessageClass";
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007D84 File Offset: 0x00005F84
		public override ErrorCode ExecuteOneFolder(Mailbox mailbox, MailboxEntry mailboxEntry, FolderEntry folderEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<string, string>(0L, "Execute task {0} on folder {1}", this.TaskName, folderEntry.ToString());
			}
			this.currentMailbox = mailboxEntry;
			this.currentFolder = folderEntry;
			List<RuleMessageClassCheckTask.RuleMessageEntry> list = null;
			ErrorCode first = this.DetectCorruption(mailbox, mailboxEntry, folderEntry, out list, shouldContinue);
			if (first != ErrorCode.NoError)
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError<string>(0L, "Unexpected error while detecting corruption on folder {0}", folderEntry.ToString());
				}
				return first.Propagate((LID)58856U);
			}
			if (list.Count == 0)
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceDebug(0L, "No corruption detected");
				}
				return ErrorCode.NoError;
			}
			if (!shouldContinue())
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
				}
				return ErrorCode.CreateExiting((LID)42472U);
			}
			foreach (RuleMessageClassCheckTask.RuleMessageEntry ruleMessageEntry in list)
			{
				bool problemFixed = false;
				if (!detectOnly)
				{
					using (TopMessage topMessage = TopMessage.OpenMessage(mailbox.CurrentOperationContext, mailbox, ruleMessageEntry.DocumentId))
					{
						if (topMessage == null)
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceDebug<int>(0L, "Message with documentId {0} gone, no fix needed", ruleMessageEntry.DocumentId);
							}
							continue;
						}
						topMessage.SetMessageClass(mailbox.CurrentOperationContext, ruleMessageEntry.DesiredClass);
						topMessage.SaveChanges(mailbox.CurrentOperationContext, SaveMessageChangesFlags.SkipQuotaCheck);
						problemFixed = true;
					}
				}
				base.ReportCorruption("Incorrect rule message class", this.currentMailbox, this.currentFolder, ruleMessageEntry, CorruptionType.IncorrectRuleMessageClass, problemFixed);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008138 File Offset: 0x00006338
		private ErrorCode DetectCorruption(Mailbox mailbox, MailboxEntry mailboxEntry, FolderEntry folderEntry, out List<RuleMessageClassCheckTask.RuleMessageEntry> corruptedMessages, Func<bool> shouldContinue)
		{
			MessagePropValueGetter messagePropValueGetter = new MessagePropValueGetter(mailbox.CurrentOperationContext, mailboxEntry.MailboxPartitionNumber, folderEntry.FolderId);
			List<RuleMessageClassCheckTask.RuleMessageEntry> messageEntries = new List<RuleMessageClassCheckTask.RuleMessageEntry>();
			Column ruleProviderColumn = PropertySchema.MapToColumn(mailbox.CurrentOperationContext.Database, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message, PropTag.Message.RuleMsgProvider);
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.CurrentOperationContext.Database);
			ErrorCode result = messagePropValueGetter.Execute(true, null, new Column[]
			{
				messageTable.MessageDocumentId,
				messageTable.MessageId,
				messageTable.MessageClass,
				ruleProviderColumn
			}, delegate(Reader reader)
			{
				int @int = reader.GetInt32(messageTable.MessageDocumentId);
				string @string = reader.GetString(ruleProviderColumn);
				if (@string == null)
				{
					return ErrorCode.NoError;
				}
				byte[] binary = reader.GetBinary(messageTable.MessageId);
				ExchangeId messageId = ExchangeId.CreateFrom26ByteArray(mailbox.CurrentOperationContext, mailbox.ReplidGuidMap, binary);
				string string2 = reader.GetString(messageTable.MessageClass);
				if (string2 == null)
				{
					return ErrorCode.NoError;
				}
				bool flag = 0 == string.Compare(string2, "IPM.ExtendedRule.Message", StringComparison.OrdinalIgnoreCase);
				bool flag2 = 0 == string.Compare(string2, "IPM.Rule.Version2.Message", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					for (int i = 0; i < this.knownClassicProviders.Length; i++)
					{
						if (string.Compare(@string, this.knownClassicProviders[i], StringComparison.OrdinalIgnoreCase) == 0)
						{
							messageEntries.Add(new RuleMessageClassCheckTask.RuleMessageEntry(@int, messageId, "IPM.Rule.Version2.Message"));
							break;
						}
						Guid guid;
						if (@string.StartsWith(this.knownClassicProviders[i], StringComparison.OrdinalIgnoreCase) && @string.Length == this.knownClassicProviders[i].Length + 32 && Guid.TryParseExact(@string.Substring(this.knownClassicProviders[i].Length), "N", out guid))
						{
							messageEntries.Add(new RuleMessageClassCheckTask.RuleMessageEntry(@int, messageId, "IPM.Rule.Version2.Message"));
							break;
						}
					}
				}
				if (flag2)
				{
					for (int j = 0; j < this.knownExtendedProviders.Length; j++)
					{
						if (string.Compare(@string, this.knownExtendedProviders[j], StringComparison.OrdinalIgnoreCase) == 0)
						{
							messageEntries.Add(new RuleMessageClassCheckTask.RuleMessageEntry(@int, messageId, "IPM.ExtendedRule.Message"));
							break;
						}
					}
				}
				return ErrorCode.NoError;
			}, shouldContinue);
			corruptedMessages = messageEntries;
			return result;
		}

		// Token: 0x040000AF RID: 175
		private readonly string[] knownClassicProviders = new string[]
		{
			"RuleProvider",
			"RuleProvider2",
			"MSFT:TDX OOF Rules"
		};

		// Token: 0x040000B0 RID: 176
		private readonly string[] knownExtendedProviders = new string[]
		{
			"JunkEmailRule",
			"Microsoft Exchange OOF Assistant"
		};

		// Token: 0x040000B1 RID: 177
		private MailboxEntry currentMailbox;

		// Token: 0x040000B2 RID: 178
		private FolderEntry currentFolder;

		// Token: 0x02000031 RID: 49
		private class RuleMessageEntry : MessageEntry
		{
			// Token: 0x060000FD RID: 253 RVA: 0x00008219 File Offset: 0x00006419
			public RuleMessageEntry(int documentId, ExchangeId messageId, string desiredClass) : base(documentId, messageId)
			{
				this.desiredClass = desiredClass;
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000FE RID: 254 RVA: 0x0000822A File Offset: 0x0000642A
			public string DesiredClass
			{
				get
				{
					return this.desiredClass;
				}
			}

			// Token: 0x040000B3 RID: 179
			private readonly string desiredClass;
		}
	}
}
