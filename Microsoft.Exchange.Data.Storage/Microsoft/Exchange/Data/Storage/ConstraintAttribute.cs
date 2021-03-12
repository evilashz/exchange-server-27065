using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA0 RID: 3744
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ConstraintAttribute : Attribute
	{
		// Token: 0x0600820F RID: 33295
		internal abstract StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition);
	}
}
