using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000089 RID: 137
	public interface IItemAttachment : IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003AB RID: 939
		// (set) Token: 0x060003AC RID: 940
		IItem Item { get; set; }
	}
}
