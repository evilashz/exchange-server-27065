using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA4 RID: 3748
	[ClassAccessLevel(AccessLevel.Implementation)]
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class FixedValueOnlyAttribute : ConstraintAttribute
	{
		// Token: 0x06008218 RID: 33304 RVA: 0x00238B5A File Offset: 0x00236D5A
		internal FixedValueOnlyAttribute(object fixedValue)
		{
			this.fixedValue = fixedValue;
		}

		// Token: 0x06008219 RID: 33305 RVA: 0x00238B69 File Offset: 0x00236D69
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return new FixedValueOnlyPropertyConstraint(propertyDefinition, this.fixedValue);
		}

		// Token: 0x0400575A RID: 22362
		private readonly object fixedValue;
	}
}
