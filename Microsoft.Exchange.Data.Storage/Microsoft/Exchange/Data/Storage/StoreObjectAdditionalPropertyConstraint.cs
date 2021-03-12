using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB8 RID: 3768
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class StoreObjectAdditionalPropertyConstraint : StoreObjectConstraint
	{
		// Token: 0x06008254 RID: 33364 RVA: 0x00239518 File Offset: 0x00237718
		internal StoreObjectAdditionalPropertyConstraint(StorePropertyDefinition propertyDefinition, PropertyDefinitionConstraint constraint) : base(new PropertyDefinition[]
		{
			propertyDefinition
		})
		{
			this.propertyDefinition = propertyDefinition;
			this.constraint = constraint;
		}

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x06008255 RID: 33365 RVA: 0x00239545 File Offset: 0x00237745
		public PropertyDefinitionConstraint Constraint
		{
			get
			{
				return this.constraint;
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x06008256 RID: 33366 RVA: 0x0023954D File Offset: 0x0023774D
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x06008257 RID: 33367 RVA: 0x00239558 File Offset: 0x00237758
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			object obj = validatablePropertyBag.TryGetProperty(this.propertyDefinition);
			if (!PropertyError.IsPropertyNotFound(obj))
			{
				PropertyConstraintViolationError propertyConstraintViolationError = this.constraint.Validate(obj, this.propertyDefinition, null);
				if (propertyConstraintViolationError != null)
				{
					return new StoreObjectValidationError(context, this.propertyDefinition, obj, this);
				}
			}
			return null;
		}

		// Token: 0x06008258 RID: 33368 RVA: 0x002395A1 File Offset: 0x002377A1
		public override string ToString()
		{
			return string.Format("For objects of this type, property {0} has the additional constraint {1}.", this.propertyDefinition, this.constraint);
		}

		// Token: 0x04005775 RID: 22389
		private readonly PropertyDefinitionConstraint constraint;

		// Token: 0x04005776 RID: 22390
		private readonly StorePropertyDefinition propertyDefinition;
	}
}
