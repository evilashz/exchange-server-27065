using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EF4 RID: 3828
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WorkingSetPublisherPerformanceTracker : PerformanceTrackerBase, IWorkingSetPublisherPerformanceTracker, IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x060083EB RID: 33771 RVA: 0x0023CE58 File Offset: 0x0023B058
		public WorkingSetPublisherPerformanceTracker(IMailboxSession mailboxSession) : base(mailboxSession)
		{
			this.OriginalMessageSender = string.Empty;
			this.OriginalMessageSenderRecipientType = string.Empty;
			this.OriginalMessageClass = string.Empty;
			this.OriginalMessageId = string.Empty;
			this.OriginalInternetMessageId = string.Empty;
			this.PublishedMessageId = string.Empty;
			this.PublishedIntnernetMessageId = string.Empty;
		}

		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x060083EC RID: 33772 RVA: 0x0023CEB9 File Offset: 0x0023B0B9
		// (set) Token: 0x060083ED RID: 33773 RVA: 0x0023CEC1 File Offset: 0x0023B0C1
		public string OriginalMessageSender { get; set; }

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x060083EE RID: 33774 RVA: 0x0023CECA File Offset: 0x0023B0CA
		// (set) Token: 0x060083EF RID: 33775 RVA: 0x0023CED2 File Offset: 0x0023B0D2
		public string OriginalMessageSenderRecipientType { get; set; }

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x060083F0 RID: 33776 RVA: 0x0023CEDB File Offset: 0x0023B0DB
		// (set) Token: 0x060083F1 RID: 33777 RVA: 0x0023CEE3 File Offset: 0x0023B0E3
		public string OriginalMessageClass { get; set; }

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x060083F2 RID: 33778 RVA: 0x0023CEEC File Offset: 0x0023B0EC
		// (set) Token: 0x060083F3 RID: 33779 RVA: 0x0023CEF4 File Offset: 0x0023B0F4
		public string OriginalMessageId { get; set; }

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x060083F4 RID: 33780 RVA: 0x0023CEFD File Offset: 0x0023B0FD
		// (set) Token: 0x060083F5 RID: 33781 RVA: 0x0023CF05 File Offset: 0x0023B105
		public string OriginalInternetMessageId { get; set; }

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x060083F6 RID: 33782 RVA: 0x0023CF0E File Offset: 0x0023B10E
		// (set) Token: 0x060083F7 RID: 33783 RVA: 0x0023CF16 File Offset: 0x0023B116
		public int ParticipantsInOriginalMessage { get; set; }

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x060083F8 RID: 33784 RVA: 0x0023CF1F File Offset: 0x0023B11F
		// (set) Token: 0x060083F9 RID: 33785 RVA: 0x0023CF27 File Offset: 0x0023B127
		public string PublishedMessageId { get; set; }

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x060083FA RID: 33786 RVA: 0x0023CF30 File Offset: 0x0023B130
		// (set) Token: 0x060083FB RID: 33787 RVA: 0x0023CF38 File Offset: 0x0023B138
		public string PublishedIntnernetMessageId { get; set; }

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x060083FC RID: 33788 RVA: 0x0023CF41 File Offset: 0x0023B141
		// (set) Token: 0x060083FD RID: 33789 RVA: 0x0023CF49 File Offset: 0x0023B149
		public bool IsGroupParticipantAddedToParticipants { get; set; }

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x060083FE RID: 33790 RVA: 0x0023CF52 File Offset: 0x0023B152
		// (set) Token: 0x060083FF RID: 33791 RVA: 0x0023CF5A File Offset: 0x0023B15A
		public long EnsureGroupParticipantAddedMilliseconds { get; set; }

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x06008400 RID: 33792 RVA: 0x0023CF63 File Offset: 0x0023B163
		// (set) Token: 0x06008401 RID: 33793 RVA: 0x0023CF6B File Offset: 0x0023B16B
		public long DedupeParticipantsMilliseconds { get; set; }

		// Token: 0x06008402 RID: 33794 RVA: 0x0023CF74 File Offset: 0x0023B174
		public void IncrementParticipantsAddedToPublishedMessage()
		{
			this.participantsAddedToPublishedMessage++;
		}

		// Token: 0x06008403 RID: 33795 RVA: 0x0023CF84 File Offset: 0x0023B184
		public void IncrementParticipantsSkippedInPublishedMessage()
		{
			this.participantsSkippedInPublishedMessage++;
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x06008404 RID: 33796 RVA: 0x0023CF94 File Offset: 0x0023B194
		// (set) Token: 0x06008405 RID: 33797 RVA: 0x0023CF9C File Offset: 0x0023B19C
		public bool HasWorkingSetUser { get; set; }

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x06008406 RID: 33798 RVA: 0x0023CFA5 File Offset: 0x0023B1A5
		// (set) Token: 0x06008407 RID: 33799 RVA: 0x0023CFAD File Offset: 0x0023B1AD
		public string Exception { get; set; }

		// Token: 0x06008408 RID: 33800 RVA: 0x0023CFB8 File Offset: 0x0023B1B8
		public ILogEvent GetLogEvent(string operationName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("operationName", operationName);
			base.EnforceInternalState(PerformanceTrackerBase.InternalState.Stopped, "GetLogEvent");
			return new SchemaBasedLogEvent<WorkingSetPublisherLogSchema.OperationEnd>
			{
				{
					WorkingSetPublisherLogSchema.OperationEnd.OperationName,
					operationName
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.Elapsed,
					base.ElapsedTime.TotalMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.CPU,
					base.CpuTime.TotalMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.RPCCount,
					base.StoreRpcCount
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.RPCLatency,
					base.StoreRpcLatency.TotalMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.DirectoryCount,
					base.DirectoryCount
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.DirectoryLatency,
					base.DirectoryLatency.TotalMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.StoreTimeInServer,
					base.StoreTimeInServer.TotalMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.StoreTimeInCPU,
					base.StoreTimeInCPU.TotalMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.StorePagesRead,
					base.StorePagesRead
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.StorePagesPreRead,
					base.StorePagesPreread
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.StoreLogRecords,
					base.StoreLogRecords
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.StoreLogBytes,
					base.StoreLogBytes
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.OrigMsgSender,
					ExtensibleLogger.FormatPIIValue(this.OriginalMessageSender)
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.OrigMsgSndRcpType,
					this.OriginalMessageSenderRecipientType
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.OrigMsgClass,
					this.OriginalMessageClass
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.OrigMsgId,
					this.OriginalMessageId
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.OrigMsgInetId,
					this.OriginalInternetMessageId
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.PartOrigMsg,
					this.ParticipantsInOriginalMessage
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.GroupPart,
					this.IsGroupParticipantAddedToParticipants
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.EnsGroupPart,
					this.EnsureGroupParticipantAddedMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.DedupePart,
					this.DedupeParticipantsMilliseconds
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.PartAddedPub,
					this.participantsAddedToPublishedMessage
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.PartSkippedPub,
					this.participantsSkippedInPublishedMessage
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.PubMsgId,
					this.PublishedMessageId
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.PubMsgInetId,
					this.PublishedIntnernetMessageId
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.HasWorkingSet,
					this.HasWorkingSetUser
				},
				{
					WorkingSetPublisherLogSchema.OperationEnd.Exception,
					this.Exception
				}
			};
		}

		// Token: 0x04005851 RID: 22609
		private int participantsAddedToPublishedMessage;

		// Token: 0x04005852 RID: 22610
		private int participantsSkippedInPublishedMessage;
	}
}
