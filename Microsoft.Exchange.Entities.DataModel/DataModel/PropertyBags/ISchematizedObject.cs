using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x0200000D RID: 13
	public interface ISchematizedObject<out TSchema> : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49
		TSchema Schema { get; }
	}
}
