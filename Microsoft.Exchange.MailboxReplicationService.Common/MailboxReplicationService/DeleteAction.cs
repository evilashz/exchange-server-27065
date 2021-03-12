using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000089 RID: 137
	[DataContract]
	internal sealed class DeleteAction : MoveAction
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public DeleteAction()
		{
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public DeleteAction(byte[] itemId, byte[] folderId, byte[] prevFolderId, string watermark) : base(itemId, folderId, prevFolderId, watermark)
		{
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000ABB5 File Offset: 0x00008DB5
		internal override ActionId Id
		{
			get
			{
				return ActionId.Delete;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		internal override void TranslateEntryIds(IEntryIdTranslator translator)
		{
			base.TranslateEntryIds(translator);
			if (base.ItemId != null && base.PreviousFolderId != null)
			{
				base.Ignored = false;
			}
		}
	}
}
