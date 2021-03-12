using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200008A RID: 138
	public interface IItemIdAttachment : IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003AD RID: 941
		// (set) Token: 0x060003AE RID: 942
		string ItemToAttachId { get; set; }
	}
}
