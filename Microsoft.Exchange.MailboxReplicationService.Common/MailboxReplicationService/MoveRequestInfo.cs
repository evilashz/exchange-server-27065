using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001B RID: 27
	[DataContract]
	internal sealed class MoveRequestInfo
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000434A File Offset: 0x0000254A
		public MoveRequestInfo()
		{
			this.Message = LocalizedString.Empty;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000435D File Offset: 0x0000255D
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00004365 File Offset: 0x00002565
		[DataMember(IsRequired = true)]
		public Guid MailboxGuid { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000436E File Offset: 0x0000256E
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00004376 File Offset: 0x00002576
		[DataMember(IsRequired = true)]
		public SyncStage SyncStage { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000437F File Offset: 0x0000257F
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00004387 File Offset: 0x00002587
		[DataMember(IsRequired = true)]
		public int PercentComplete { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004390 File Offset: 0x00002590
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00004398 File Offset: 0x00002598
		[DataMember]
		public ulong ItemsTransfered { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000043A1 File Offset: 0x000025A1
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000043A9 File Offset: 0x000025A9
		[DataMember]
		public ulong BytesTransfered { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000043B2 File Offset: 0x000025B2
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000043BA File Offset: 0x000025BA
		[DataMember]
		public ulong BytesPerMinute { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000043C3 File Offset: 0x000025C3
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000043CB File Offset: 0x000025CB
		[DataMember]
		public TransferProgressTracker ProgressTracker { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000043D4 File Offset: 0x000025D4
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000043DC File Offset: 0x000025DC
		[DataMember]
		public int BadItemsEncountered { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000043E5 File Offset: 0x000025E5
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000043ED File Offset: 0x000025ED
		[DataMember]
		public int LargeItemsEncountered { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000043F6 File Offset: 0x000025F6
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00004403 File Offset: 0x00002603
		[IgnoreDataMember]
		public LocalizedString Message
		{
			get
			{
				return CommonUtils.ByteDeserialize(this.MessageData);
			}
			set
			{
				this.MessageData = CommonUtils.ByteSerialize(value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004411 File Offset: 0x00002611
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00004419 File Offset: 0x00002619
		[DataMember(Name = "Message")]
		public byte[] MessageData { get; set; }
	}
}
