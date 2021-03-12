using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA1 RID: 3745
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RequiredAttribute : ConstraintAttribute
	{
		// Token: 0x06008211 RID: 33297 RVA: 0x00238AEE File Offset: 0x00236CEE
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return new RequiredPropertyConstraint(propertyDefinition);
		}
	}
}
