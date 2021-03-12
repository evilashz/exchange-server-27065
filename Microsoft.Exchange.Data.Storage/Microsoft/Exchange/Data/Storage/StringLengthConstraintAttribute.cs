using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA6 RID: 3750
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StringLengthConstraintAttribute : ConstraintAttribute
	{
		// Token: 0x0600821C RID: 33308 RVA: 0x00238BBA File Offset: 0x00236DBA
		internal StringLengthConstraintAttribute(int minLength, int maxLength)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		// Token: 0x0600821D RID: 33309 RVA: 0x00238BD0 File Offset: 0x00236DD0
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return new StoreObjectAdditionalPropertyConstraint(propertyDefinition, new StringLengthConstraint(this.minLength, this.maxLength));
		}

		// Token: 0x0400575C RID: 22364
		private readonly int minLength;

		// Token: 0x0400575D RID: 22365
		private readonly int maxLength;
	}
}
