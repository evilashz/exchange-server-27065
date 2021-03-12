using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E5F RID: 3679
	internal class ItemAttachmentSchema : AttachmentSchema
	{
		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x00128FF0 File Offset: 0x001271F0
		public new static ItemAttachmentSchema SchemaInstance
		{
			get
			{
				return ItemAttachmentSchema.ItemAttachmentSchemaInstance.Member;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x00128FFC File Offset: 0x001271FC
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return ItemAttachment.EdmEntityType;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x00129003 File Offset: 0x00127203
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return ItemAttachmentSchema.DeclaredItemAttachmentProperties;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06005F33 RID: 24371 RVA: 0x0012900A File Offset: 0x0012720A
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return ItemAttachmentSchema.AllItemAttachmentProperties;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06005F34 RID: 24372 RVA: 0x00129011 File Offset: 0x00127211
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return ItemAttachmentSchema.DefaultItemAttachmentProperties;
			}
		}

		// Token: 0x0400339A RID: 13210
		public static readonly PropertyDefinition Item = new PropertyDefinition("Item", typeof(Item))
		{
			Flags = (PropertyDefinitionFlags.Navigation | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.ChildOnlyEntity),
			NavigationTargetEntity = Microsoft.Exchange.Services.OData.Model.Item.EdmEntityType
		};

		// Token: 0x0400339B RID: 13211
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredItemAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
		{
			ItemAttachmentSchema.Item
		});

		// Token: 0x0400339C RID: 13212
		public static readonly ReadOnlyCollection<PropertyDefinition> AllItemAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(AttachmentSchema.AllAttachmentProperties.Union(ItemAttachmentSchema.DeclaredItemAttachmentProperties)));

		// Token: 0x0400339D RID: 13213
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultItemAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(AttachmentSchema.DefaultAttachmentProperties)
		{
			ItemAttachmentSchema.Item
		});

		// Token: 0x0400339E RID: 13214
		private static readonly LazyMember<ItemAttachmentSchema> ItemAttachmentSchemaInstance = new LazyMember<ItemAttachmentSchema>(() => new ItemAttachmentSchema());
	}
}
