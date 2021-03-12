using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000088 RID: 136
	[DataContract]
	internal class MoveAction : ReplayAction
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0000AAA5 File Offset: 0x00008CA5
		public MoveAction()
		{
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000AAAD File Offset: 0x00008CAD
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		[DataMember]
		public byte[] PreviousFolderId { get; set; }

		// Token: 0x060005DA RID: 1498 RVA: 0x0000AABE File Offset: 0x00008CBE
		public MoveAction(byte[] itemId, byte[] folderId, byte[] prevFolderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
			this.PreviousFolderId = prevFolderId;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000AADD File Offset: 0x00008CDD
		internal override ActionId Id
		{
			get
			{
				return ActionId.Move;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				", EntryId: ",
				TraceUtils.DumpEntryId(base.ItemId),
				", FolderId: ",
				TraceUtils.DumpEntryId(base.FolderId),
				", PreviousFolderId: ",
				TraceUtils.DumpEntryId(this.PreviousFolderId)
			});
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000AB48 File Offset: 0x00008D48
		internal override void TranslateEntryIds(IEntryIdTranslator translator)
		{
			base.TranslateEntryIds(translator);
			byte[] previousFolderId = this.PreviousFolderId;
			byte[] sourceFolderIdFromTargetFolderId = translator.GetSourceFolderIdFromTargetFolderId(previousFolderId);
			if (sourceFolderIdFromTargetFolderId == null)
			{
				MrsTracer.Common.Warning("Previous destination folder {0} doesn't have mapped source folder for action {1}", new object[]
				{
					TraceUtils.DumpEntryId(previousFolderId),
					this
				});
				base.Ignored = true;
			}
			this.PreviousFolderId = sourceFolderIdFromTargetFolderId;
		}
	}
}
