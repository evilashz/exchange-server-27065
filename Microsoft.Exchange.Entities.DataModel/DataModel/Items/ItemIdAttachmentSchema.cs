using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000093 RID: 147
	public sealed class ItemIdAttachmentSchema : AttachmentSchema
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00007217 File Offset: 0x00005417
		public ItemIdAttachmentSchema()
		{
			base.RegisterPropertyDefinition(ItemIdAttachmentSchema.StaticItemToAttachIdProperty);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000722A File Offset: 0x0000542A
		public TypedPropertyDefinition<string> ItemToAttachIdProperty
		{
			get
			{
				return ItemIdAttachmentSchema.StaticItemToAttachIdProperty;
			}
		}

		// Token: 0x040001DB RID: 475
		private static readonly TypedPropertyDefinition<string> StaticItemToAttachIdProperty = new TypedPropertyDefinition<string>("ItemAttachment.ItemToAttachId", null, true);
	}
}
