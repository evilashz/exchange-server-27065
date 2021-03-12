using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000074 RID: 116
	internal class EmbededItemTranslationRule : ITranslationRule<ItemAttachment, ItemAttachment>
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000086E0 File Offset: 0x000068E0
		public void FromLeftToRightType(ItemAttachment left, ItemAttachment right)
		{
			using (Item item = left.GetItem())
			{
				right.Item = StorageEntityFactory.CreateFromItem(item);
				right.Item.Id = null;
				this.UpdateNestedAttachmentIds(right.Item.Attachments, right.Id);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008740 File Offset: 0x00006940
		public void FromRightToLeftType(ItemAttachment left, ItemAttachment right)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008744 File Offset: 0x00006944
		private void UpdateNestedAttachmentIds(List<IAttachment> attachments, string embededItemAttachmentId)
		{
			if (attachments.Count > 0)
			{
				IList<AttachmentId> attachmentIds = IdConverter.GetAttachmentIds(embededItemAttachmentId);
				int count = attachmentIds.Count;
				foreach (IAttachment attachment in attachments)
				{
					IList<AttachmentId> attachmentIds2 = IdConverter.GetAttachmentIds(attachment.Id);
					attachmentIds.Add(attachmentIds2[0]);
					attachment.Id = IdConverter.GetHierarchicalAttachmentStringId(attachmentIds);
					attachmentIds.RemoveAt(count);
				}
			}
		}
	}
}
