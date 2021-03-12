using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA2 RID: 3746
	[ClassAccessLevel(AccessLevel.Implementation)]
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ConditionallyRequiredAttribute : ConstraintAttribute
	{
		// Token: 0x06008213 RID: 33299 RVA: 0x00238B00 File Offset: 0x00236D00
		internal static StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition, CustomConstraintDelegateEnum isPropertyRequired)
		{
			return new OrConstraint(new StoreObjectConstraint[]
			{
				new RequiredPropertyConstraint(propertyDefinition),
				new CustomConstraint(isPropertyRequired, false)
			});
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x00238B2D File Offset: 0x00236D2D
		internal ConditionallyRequiredAttribute(CustomConstraintDelegateEnum isPropertyRequired)
		{
			this.isPropertyRequired = isPropertyRequired;
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x00238B3C File Offset: 0x00236D3C
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return ConditionallyRequiredAttribute.GetConstraint(propertyDefinition, this.isPropertyRequired);
		}

		// Token: 0x04005759 RID: 22361
		private readonly CustomConstraintDelegateEnum isPropertyRequired;
	}
}
