using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IValidatablePropertyBag
	{
		// Token: 0x060009AC RID: 2476
		bool IsPropertyDirty(PropertyDefinition propertyDefinition);

		// Token: 0x060009AD RID: 2477
		object TryGetProperty(PropertyDefinition propertyDefinition);

		// Token: 0x060009AE RID: 2478
		PropertyValueTrackingData GetOriginalPropertyInformation(PropertyDefinition propertyDefinition);
	}
}
