using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Messaging
{
	// Token: 0x0200005F RID: 95
	public interface IEmailMessage : IItem, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002FA RID: 762
		bool IsRead { get; set; }
	}
}
