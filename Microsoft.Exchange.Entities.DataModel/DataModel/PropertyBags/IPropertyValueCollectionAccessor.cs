using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x020000A1 RID: 161
	public interface IPropertyValueCollectionAccessor<in TContainer, TProperty, TValue> : IPropertyAccessor<TContainer, TValue>
	{
		// Token: 0x0600040C RID: 1036
		bool TryGetValue(IDictionary<TProperty, int> propertyIndices, IList values, out TValue value);
	}
}
