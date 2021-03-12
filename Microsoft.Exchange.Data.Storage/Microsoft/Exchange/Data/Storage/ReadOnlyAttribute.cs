using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA3 RID: 3747
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ReadOnlyAttribute : ConstraintAttribute
	{
		// Token: 0x06008216 RID: 33302 RVA: 0x00238B4A File Offset: 0x00236D4A
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return new ReadOnlyPropertyConstraint(propertyDefinition);
		}
	}
}
