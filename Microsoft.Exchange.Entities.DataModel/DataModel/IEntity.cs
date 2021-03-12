using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x0200000F RID: 15
	public interface IEntity : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55
		// (set) Token: 0x06000038 RID: 56
		string Id { get; set; }
	}
}
