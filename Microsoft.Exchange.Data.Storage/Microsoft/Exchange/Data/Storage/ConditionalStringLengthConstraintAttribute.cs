using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA7 RID: 3751
	[ClassAccessLevel(AccessLevel.Implementation)]
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ConditionalStringLengthConstraintAttribute : StringLengthConstraintAttribute
	{
		// Token: 0x0600821E RID: 33310 RVA: 0x00238BE9 File Offset: 0x00236DE9
		internal ConditionalStringLengthConstraintAttribute(int minLength, int maxLength, CustomConstraintDelegateEnum applyConstraint) : base(minLength, maxLength)
		{
			this.applyConstraint = applyConstraint;
		}

		// Token: 0x0600821F RID: 33311 RVA: 0x00238BFC File Offset: 0x00236DFC
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return new OrConstraint(new StoreObjectConstraint[]
			{
				base.GetConstraint(propertyDefinition),
				new CustomConstraint(this.applyConstraint, false)
			});
		}

		// Token: 0x0400575E RID: 22366
		private readonly CustomConstraintDelegateEnum applyConstraint;
	}
}
