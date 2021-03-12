using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox
{
	// Token: 0x020007EB RID: 2027
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupEscalateItemPerformanceTracker : PerformanceTrackerBase, IGroupEscalateItemPerformanceTracker, IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x06004BD1 RID: 19409 RVA: 0x0013B964 File Offset: 0x00139B64
		public GroupEscalateItemPerformanceTracker(IMailboxSession mailboxSession) : base(mailboxSession)
		{
			this.OriginalMessageSender = string.Empty;
			this.OriginalMessageSenderRecipientType = string.Empty;
			this.OriginalMessageClass = string.Empty;
			this.OriginalMessageId = string.Empty;
			this.OriginalInternetMessageId = string.Empty;
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x0013B9A4 File Offset: 0x00139BA4
		// (set) Token: 0x06004BD3 RID: 19411 RVA: 0x0013B9AC File Offset: 0x00139BAC
		public string OriginalMessageSender { get; set; }

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x0013B9B5 File Offset: 0x00139BB5
		// (set) Token: 0x06004BD5 RID: 19413 RVA: 0x0013B9BD File Offset: 0x00139BBD
		public string OriginalMessageSenderRecipientType { get; set; }

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x0013B9C6 File Offset: 0x00139BC6
		// (set) Token: 0x06004BD7 RID: 19415 RVA: 0x0013B9CE File Offset: 0x00139BCE
		public string OriginalMessageClass { get; set; }

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x0013B9D7 File Offset: 0x00139BD7
		// (set) Token: 0x06004BD9 RID: 19417 RVA: 0x0013B9DF File Offset: 0x00139BDF
		public string OriginalMessageId { get; set; }

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x0013B9E8 File Offset: 0x00139BE8
		// (set) Token: 0x06004BDB RID: 19419 RVA: 0x0013B9F0 File Offset: 0x00139BF0
		public string OriginalInternetMessageId { get; set; }

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x0013B9F9 File Offset: 0x00139BF9
		// (set) Token: 0x06004BDD RID: 19421 RVA: 0x0013BA01 File Offset: 0x00139C01
		public int ParticipantsInOriginalMessage { get; set; }

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x0013BA0A File Offset: 0x00139C0A
		// (set) Token: 0x06004BDF RID: 19423 RVA: 0x0013BA12 File Offset: 0x00139C12
		public bool IsGroupParticipantAddedToReplyTo { get; set; }

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x0013BA1B File Offset: 0x00139C1B
		// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x0013BA23 File Offset: 0x00139C23
		public bool IsGroupParticipantReplyToSkipped { get; set; }

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x0013BA2C File Offset: 0x00139C2C
		// (set) Token: 0x06004BE3 RID: 19427 RVA: 0x0013BA34 File Offset: 0x00139C34
		public bool IsGroupParticipantAddedToParticipants { get; set; }

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x0013BA3D File Offset: 0x00139C3D
		// (set) Token: 0x06004BE5 RID: 19429 RVA: 0x0013BA45 File Offset: 0x00139C45
		public long EnsureGroupParticipantAddedMilliseconds { get; set; }

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x0013BA4E File Offset: 0x00139C4E
		// (set) Token: 0x06004BE7 RID: 19431 RVA: 0x0013BA56 File Offset: 0x00139C56
		public long DedupeParticipantsMilliseconds { get; set; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x0013BA5F File Offset: 0x00139C5F
		// (set) Token: 0x06004BE9 RID: 19433 RVA: 0x0013BA67 File Offset: 0x00139C67
		public bool EscalateToYammer { get; set; }

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x0013BA70 File Offset: 0x00139C70
		// (set) Token: 0x06004BEB RID: 19435 RVA: 0x0013BA78 File Offset: 0x00139C78
		public long SendToYammerMilliseconds { get; set; }

		// Token: 0x06004BEC RID: 19436 RVA: 0x0013BA81 File Offset: 0x00139C81
		public void IncrementParticipantsAddedToEscalatedMessage()
		{
			this.participantsAddedToEscalatedMessage++;
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0013BA91 File Offset: 0x00139C91
		public void IncrementParticipantsSkippedInEscalatedMessage()
		{
			this.participantsSkippedInEscalatedMessage++;
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x0013BAA1 File Offset: 0x00139CA1
		// (set) Token: 0x06004BEF RID: 19439 RVA: 0x0013BAA9 File Offset: 0x00139CA9
		public bool HasEscalatedUser { get; set; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x0013BAB2 File Offset: 0x00139CB2
		// (set) Token: 0x06004BF1 RID: 19441 RVA: 0x0013BABA File Offset: 0x00139CBA
		public bool UnsubscribeUrlInserted { get; set; }

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x0013BAC3 File Offset: 0x00139CC3
		// (set) Token: 0x06004BF3 RID: 19443 RVA: 0x0013BACB File Offset: 0x00139CCB
		public long BuildUnsubscribeUrlMilliseconds { get; set; }

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x0013BAD4 File Offset: 0x00139CD4
		// (set) Token: 0x06004BF5 RID: 19445 RVA: 0x0013BADC File Offset: 0x00139CDC
		public long LinkBodySize { get; set; }

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0013BAE5 File Offset: 0x00139CE5
		// (set) Token: 0x06004BF7 RID: 19447 RVA: 0x0013BAED File Offset: 0x00139CED
		public long LinkOnBodyDetectionMilliseconds { get; set; }

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0013BAF6 File Offset: 0x00139CF6
		// (set) Token: 0x06004BF9 RID: 19449 RVA: 0x0013BAFE File Offset: 0x00139CFE
		public long LinkInsertOnBodyMilliseconds { get; set; }

		// Token: 0x06004BFA RID: 19450 RVA: 0x0013BB08 File Offset: 0x00139D08
		public ILogEvent GetLogEvent(string operationName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("operationName", operationName);
			base.EnforceInternalState(PerformanceTrackerBase.InternalState.Stopped, "GetLogEvent");
			return new SchemaBasedLogEvent<GroupEscalateItemLogSchema.OperationEnd>
			{
				{
					GroupEscalateItemLogSchema.OperationEnd.OperationName,
					operationName
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.Elapsed,
					base.ElapsedTime.TotalMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.CPU,
					base.CpuTime.TotalMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.RPCCount,
					base.StoreRpcCount
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.RPCLatency,
					base.StoreRpcLatency.TotalMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.DirectoryCount,
					base.DirectoryCount
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.DirectoryLatency,
					base.DirectoryLatency.TotalMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.StoreTimeInServer,
					base.StoreTimeInServer.TotalMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.StoreTimeInCPU,
					base.StoreTimeInCPU.TotalMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.StorePagesRead,
					base.StorePagesRead
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.StorePagesPreRead,
					base.StorePagesPreread
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.StoreLogRecords,
					base.StoreLogRecords
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.StoreLogBytes,
					base.StoreLogBytes
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.OrigMsgSender,
					ExtensibleLogger.FormatPIIValue(this.OriginalMessageSender)
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.OrigMsgSndRcpType,
					this.OriginalMessageSenderRecipientType
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.OrigMsgClass,
					this.OriginalMessageClass
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.OrigMsgId,
					this.OriginalMessageId
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.OrigMsgInetId,
					this.OriginalInternetMessageId
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.PartOrigMsg,
					this.ParticipantsInOriginalMessage
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.GroupReplyTo,
					this.IsGroupParticipantAddedToReplyTo
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.GroupPart,
					this.IsGroupParticipantAddedToParticipants
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.EnsGroupPart,
					this.EnsureGroupParticipantAddedMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.DedupePart,
					this.DedupeParticipantsMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.PartAddedEsc,
					this.participantsAddedToEscalatedMessage
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.PartSkippedEsc,
					this.participantsSkippedInEscalatedMessage
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.HasEscalated,
					this.HasEscalatedUser
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.GroupReplyToSkipped,
					this.IsGroupParticipantReplyToSkipped
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.SendToYammer,
					this.EscalateToYammer
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.SendToYammerMs,
					this.SendToYammerMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.UnsubscribeUrl,
					this.UnsubscribeUrlInserted
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.UnsubscribeUrlBuildMs,
					this.BuildUnsubscribeUrlMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.UnsubscribeBodySize,
					this.LinkBodySize
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.UnsubscribeUrlDetectionMs,
					this.LinkOnBodyDetectionMilliseconds
				},
				{
					GroupEscalateItemLogSchema.OperationEnd.UnsubscribeUrlInsertMs,
					this.LinkInsertOnBodyMilliseconds
				}
			};
		}

		// Token: 0x0400293D RID: 10557
		private int participantsAddedToEscalatedMessage;

		// Token: 0x0400293E RID: 10558
		private int participantsSkippedInEscalatedMessage;
	}
}
