using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB6 RID: 3766
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RequiredPropertyConstraint : StoreObjectConstraint
	{
		// Token: 0x0600824D RID: 33357 RVA: 0x00239424 File Offset: 0x00237624
		internal RequiredPropertyConstraint(StorePropertyDefinition propertyDefinition) : base(new PropertyDefinition[]
		{
			propertyDefinition
		})
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x0600824E RID: 33358 RVA: 0x0023944A File Offset: 0x0023764A
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x00239454 File Offset: 0x00237654
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			object obj = validatablePropertyBag.TryGetProperty(this.propertyDefinition);
			if (PropertyError.IsPropertyError(obj) && !PropertyError.IsPropertyValueTooBig(obj))
			{
				return new StoreObjectValidationError(context, this.propertyDefinition, obj, this);
			}
			return null;
		}

		// Token: 0x06008250 RID: 33360 RVA: 0x0023948E File Offset: 0x0023768E
		public override string ToString()
		{
			return string.Format("Property {0} is required.", this.propertyDefinition);
		}

		// Token: 0x04005773 RID: 22387
		private readonly StorePropertyDefinition propertyDefinition;
	}
}
