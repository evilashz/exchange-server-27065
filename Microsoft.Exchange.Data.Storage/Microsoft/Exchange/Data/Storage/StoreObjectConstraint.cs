using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BBF RID: 3007
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class StoreObjectConstraint
	{
		// Token: 0x06006B6A RID: 27498 RVA: 0x001CBCEF File Offset: 0x001C9EEF
		protected StoreObjectConstraint(ICollection<PropertyDefinition> relevantProperties)
		{
			this.relevantProperties = relevantProperties;
		}

		// Token: 0x06006B6B RID: 27499
		internal abstract StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag);

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x06006B6C RID: 27500 RVA: 0x001CBCFE File Offset: 0x001C9EFE
		internal ICollection<PropertyDefinition> RelevantProperties
		{
			get
			{
				return this.relevantProperties;
			}
		}

		// Token: 0x04003D70 RID: 15728
		private readonly ICollection<PropertyDefinition> relevantProperties;
	}
}
