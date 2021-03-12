using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000090 RID: 144
	[DataContract]
	internal sealed class SendAction : ReplayAction
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0000ADBB File Offset: 0x00008FBB
		public SendAction()
		{
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0000ADCB File Offset: 0x00008FCB
		[DataMember]
		public string[] Recipients { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0000ADDC File Offset: 0x00008FDC
		[DataMember]
		public byte[] Data { get; set; }

		// Token: 0x06000604 RID: 1540 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		public SendAction(byte[] itemId, byte[] folderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000ADFC File Offset: 0x00008FFC
		internal override ActionId Id
		{
			get
			{
				return ActionId.Send;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000AE00 File Offset: 0x00009000
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", EntryId: ",
				TraceUtils.DumpEntryId(base.ItemId),
				", Data: [len=",
				this.Data.Length,
				"], Recipients: ",
				string.Join(",", this.Recipients)
			});
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000AE6C File Offset: 0x0000906C
		internal override EntryIdMap<byte[]> GetMessageEntryIdsToTranslate()
		{
			return SendAction.EmptyEntryIdMap;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000AE73 File Offset: 0x00009073
		internal override void TranslateEntryIds(IEntryIdTranslator translator)
		{
			base.OriginalItemId = base.ItemId;
			base.OriginalFolderId = base.FolderId;
			base.ItemId = null;
			base.FolderId = null;
		}

		// Token: 0x04000374 RID: 884
		private static readonly EntryIdMap<byte[]> EmptyEntryIdMap = new EntryIdMap<byte[]>(0);
	}
}
