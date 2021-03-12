using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000015 RID: 21
	internal interface IReadOnlyPropertyBag
	{
		// Token: 0x06000069 RID: 105
		TPropertyValue GetProperty<TPropertyValue>(PropertyDefinition property);

		// Token: 0x0600006A RID: 106
		bool TryGetProperty(PropertyDefinition property, out object value);
	}
}
