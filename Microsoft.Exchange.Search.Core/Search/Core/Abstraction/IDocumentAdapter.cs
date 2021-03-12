using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200001A RID: 26
	internal interface IDocumentAdapter
	{
		// Token: 0x06000077 RID: 119
		bool ContainsPropertyMapping(PropertyDefinition propertyDefinition);

		// Token: 0x06000078 RID: 120
		bool TryGetProperty(PropertyDefinition propertyDefinition, out object value);

		// Token: 0x06000079 RID: 121
		void SetProperty(PropertyDefinition propertyDefinition, object value);
	}
}
