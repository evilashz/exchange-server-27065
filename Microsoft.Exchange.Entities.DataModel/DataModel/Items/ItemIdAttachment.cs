using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000091 RID: 145
	public sealed class ItemIdAttachment : Attachment<ItemIdAttachmentSchema>, IItemIdAttachment, IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000717A File Offset: 0x0000537A
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000718D File Offset: 0x0000538D
		public string ItemToAttachId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.ItemToAttachIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.ItemToAttachIdProperty, value);
			}
		}

		// Token: 0x02000092 RID: 146
		public new static class Accessors
		{
			// Token: 0x040001D8 RID: 472
			public static readonly EntityPropertyAccessor<ItemIdAttachment, string> ItemToAttachId = new EntityPropertyAccessor<ItemIdAttachment, string>(SchematizedObject<ItemIdAttachmentSchema>.SchemaInstance.ItemToAttachIdProperty, (ItemIdAttachment itemIdAttachment) => itemIdAttachment.ItemToAttachId, delegate(ItemIdAttachment itemIdAttachment, string itemToAttachId)
			{
				itemIdAttachment.ItemToAttachId = itemToAttachId;
			});
		}
	}
}
