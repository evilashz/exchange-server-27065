using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EAD RID: 3757
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FixedValueOnlyPropertyConstraint : ReadOnlyPropertyConstraint
	{
		// Token: 0x0600822F RID: 33327 RVA: 0x00238E13 File Offset: 0x00237013
		internal FixedValueOnlyPropertyConstraint(StorePropertyDefinition propertyDefinition, object fixedValue) : base(propertyDefinition)
		{
			this.fixedValue = fixedValue;
		}

		// Token: 0x06008230 RID: 33328 RVA: 0x00238E24 File Offset: 0x00237024
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			if (!validatablePropertyBag.IsPropertyDirty(base.PropertyDefinition))
			{
				return null;
			}
			object obj = validatablePropertyBag.TryGetProperty(base.PropertyDefinition);
			if (this.fixedValue.Equals(obj))
			{
				return null;
			}
			return base.Validate(context, validatablePropertyBag);
		}

		// Token: 0x06008231 RID: 33329 RVA: 0x00238E66 File Offset: 0x00237066
		public override string ToString()
		{
			return string.Format("Property {0} can be set to value {1} only.", base.PropertyDefinition, this.fixedValue);
		}

		// Token: 0x0400576B RID: 22379
		private readonly object fixedValue;
	}
}
