using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E5E RID: 3678
	internal class ItemAttachment : Attachment
	{
		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x00128F92 File Offset: 0x00127192
		// (set) Token: 0x06005F2C RID: 24364 RVA: 0x00128FA4 File Offset: 0x001271A4
		public Item Item
		{
			get
			{
				return (Item)base[ItemAttachmentSchema.Item];
			}
			set
			{
				base[ItemAttachmentSchema.Item] = value;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x00128FB2 File Offset: 0x001271B2
		internal override EntitySchema Schema
		{
			get
			{
				return ItemAttachmentSchema.SchemaInstance;
			}
		}

		// Token: 0x04003399 RID: 13209
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(ItemAttachment).Namespace, typeof(ItemAttachment).Name, Attachment.EdmEntityType);
	}
}
