using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EAC RID: 3756
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReadOnlyPropertyConstraint : StoreObjectConstraint
	{
		// Token: 0x0600822B RID: 33323 RVA: 0x00238DA8 File Offset: 0x00236FA8
		internal ReadOnlyPropertyConstraint(StorePropertyDefinition propertyDefinition) : base(new PropertyDefinition[]
		{
			propertyDefinition
		})
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x0600822C RID: 33324 RVA: 0x00238DCE File Offset: 0x00236FCE
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x0600822D RID: 33325 RVA: 0x00238DD6 File Offset: 0x00236FD6
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			if (validatablePropertyBag.IsPropertyDirty(this.propertyDefinition))
			{
				return new StoreObjectValidationError(context, this.propertyDefinition, validatablePropertyBag.TryGetProperty(this.propertyDefinition), this);
			}
			return null;
		}

		// Token: 0x0600822E RID: 33326 RVA: 0x00238E01 File Offset: 0x00237001
		public override string ToString()
		{
			return string.Format("Property {0} is read-only.", this.propertyDefinition);
		}

		// Token: 0x0400576A RID: 22378
		private readonly StorePropertyDefinition propertyDefinition;
	}
}
