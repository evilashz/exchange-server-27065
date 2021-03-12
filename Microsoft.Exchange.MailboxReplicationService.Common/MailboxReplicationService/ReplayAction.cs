using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000083 RID: 131
	[KnownType(typeof(UpdateCalendarEventAction))]
	[KnownType(typeof(CreateCalendarEventAction))]
	[KnownType(typeof(MoveAction))]
	[DataContract]
	[KnownType(typeof(MarkAsReadAction))]
	[KnownType(typeof(MarkAsUnReadAction))]
	[KnownType(typeof(DeleteAction))]
	[KnownType(typeof(SendAction))]
	[KnownType(typeof(FlagAction))]
	[KnownType(typeof(FlagClearAction))]
	[KnownType(typeof(FlagCompleteAction))]
	internal abstract class ReplayAction
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x0000A704 File Offset: 0x00008904
		public ReplayAction()
		{
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000A70C File Offset: 0x0000890C
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000A714 File Offset: 0x00008914
		[DataMember]
		public string Watermark { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000A71D File Offset: 0x0000891D
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000A725 File Offset: 0x00008925
		[DataMember]
		public byte[] ItemId { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000A72E File Offset: 0x0000892E
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0000A736 File Offset: 0x00008936
		[DataMember]
		public byte[] FolderId { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005B4 RID: 1460
		internal abstract ActionId Id { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000A73F File Offset: 0x0000893F
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000A747 File Offset: 0x00008947
		internal bool Ignored { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000A750 File Offset: 0x00008950
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000A758 File Offset: 0x00008958
		internal byte[] OriginalItemId { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000A761 File Offset: 0x00008961
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000A769 File Offset: 0x00008969
		internal byte[] OriginalFolderId { get; set; }

		// Token: 0x060005BB RID: 1467 RVA: 0x0000A772 File Offset: 0x00008972
		protected ReplayAction(string watermark)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("watermark", watermark);
			this.Watermark = watermark;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000A78C File Offset: 0x0000898C
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000A7A0 File Offset: 0x000089A0
		internal virtual EntryIdMap<byte[]> GetMessageEntryIdsToTranslate()
		{
			return new EntryIdMap<byte[]>(1)
			{
				{
					this.FolderId,
					this.ItemId
				}
			};
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000A7C7 File Offset: 0x000089C7
		internal virtual void TranslateEntryIds(IEntryIdTranslator translator)
		{
			this.TranslateFolderId(translator);
			this.TranslateEntryId(translator);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000A7D8 File Offset: 0x000089D8
		internal void TranslateEntryId(IEntryIdTranslator translator)
		{
			byte[] itemId = this.ItemId;
			byte[] sourceMessageIdFromTargetMessageId = translator.GetSourceMessageIdFromTargetMessageId(itemId);
			if (sourceMessageIdFromTargetMessageId == null)
			{
				MrsTracer.Common.Warning("Destination message {0} doesn't have mapped source message for action {1}", new object[]
				{
					TraceUtils.DumpEntryId(itemId),
					this
				});
				this.Ignored = true;
			}
			this.OriginalItemId = itemId;
			this.ItemId = sourceMessageIdFromTargetMessageId;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000A830 File Offset: 0x00008A30
		internal void TranslateFolderId(IEntryIdTranslator translator)
		{
			byte[] folderId = this.FolderId;
			byte[] sourceFolderIdFromTargetFolderId = translator.GetSourceFolderIdFromTargetFolderId(folderId);
			if (sourceFolderIdFromTargetFolderId == null)
			{
				MrsTracer.Common.Warning("Destination folder {0} doesn't have mapped source folder for action {1}", new object[]
				{
					TraceUtils.DumpEntryId(folderId),
					this
				});
				this.Ignored = true;
			}
			this.OriginalFolderId = folderId;
			this.FolderId = sourceFolderIdFromTargetFolderId;
		}
	}
}
