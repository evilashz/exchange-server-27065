using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA5 RID: 3749
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ConditionallyReadOnlyAttribute : ConstraintAttribute
	{
		// Token: 0x0600821A RID: 33306 RVA: 0x00238B77 File Offset: 0x00236D77
		internal ConditionallyReadOnlyAttribute(CustomConstraintDelegateEnum isPropertyReadOnly)
		{
			this.isPropertyReadOnly = isPropertyReadOnly;
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x00238B88 File Offset: 0x00236D88
		internal override StoreObjectConstraint GetConstraint(StorePropertyDefinition propertyDefinition)
		{
			return new OrConstraint(new StoreObjectConstraint[]
			{
				new ReadOnlyPropertyConstraint(propertyDefinition),
				new CustomConstraint(this.isPropertyReadOnly, false)
			});
		}

		// Token: 0x0400575B RID: 22363
		private readonly CustomConstraintDelegateEnum isPropertyReadOnly;
	}
}
