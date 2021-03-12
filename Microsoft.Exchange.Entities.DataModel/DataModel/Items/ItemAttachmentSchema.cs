using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200008F RID: 143
	public sealed class ItemAttachmentSchema : AttachmentSchema
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00007123 File Offset: 0x00005323
		public ItemAttachmentSchema()
		{
			base.RegisterPropertyDefinition(ItemAttachmentSchema.StaticItemProperty);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00007136 File Offset: 0x00005336
		public TypedPropertyDefinition<IItem> ItemProperty
		{
			get
			{
				return ItemAttachmentSchema.StaticItemProperty;
			}
		}

		// Token: 0x040001D5 RID: 469
		private static readonly TypedPropertyDefinition<IItem> StaticItemProperty = new TypedPropertyDefinition<IItem>("ItemAttachment.Item", null, false);
	}
}
