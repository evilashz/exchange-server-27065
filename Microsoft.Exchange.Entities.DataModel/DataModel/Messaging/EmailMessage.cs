using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Messaging
{
	// Token: 0x02000060 RID: 96
	public abstract class EmailMessage<TSchema> : Item<TSchema>, IEmailMessage, IItem, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned where TSchema : EmailMessageSchema, new()
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000062DC File Offset: 0x000044DC
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00006304 File Offset: 0x00004504
		public bool IsRead
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<bool>(schema.IsReadProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<bool>(schema.IsReadProperty, value);
			}
		}
	}
}
