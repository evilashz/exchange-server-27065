using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000072 RID: 114
	internal class AttachmentTranslationRule<TStorageItem, TItem, TSchema> : ITranslationRule<TStorageItem, TItem> where TStorageItem : IItem where TItem : Item<TSchema> where TSchema : ItemSchema, new()
	{
		// Token: 0x0600026E RID: 622 RVA: 0x000085D9 File Offset: 0x000067D9
		public AttachmentTranslationRule()
		{
			this.AttachmentConverter = new AttachmentConverter();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000085EC File Offset: 0x000067EC
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000085F4 File Offset: 0x000067F4
		public IConverter<Attachment, IAttachment> AttachmentConverter { get; private set; }

		// Token: 0x06000271 RID: 625 RVA: 0x00008600 File Offset: 0x00006800
		public void FromLeftToRightType(TStorageItem left, TItem right)
		{
			List<IAttachment> list = new List<IAttachment>();
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(left);
			IList<AttachmentHandle> handles = attachmentCollection.GetHandles();
			foreach (AttachmentHandle handle in handles)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					IAttachment item = this.AttachmentConverter.Convert(attachment);
					list.Add(item);
				}
			}
			right.Attachments = list;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000086A8 File Offset: 0x000068A8
		public void FromRightToLeftType(TStorageItem left, TItem right)
		{
		}
	}
}
