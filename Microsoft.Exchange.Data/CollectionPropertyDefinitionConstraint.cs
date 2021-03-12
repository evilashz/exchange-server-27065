using System;
using System.Collections;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal abstract class CollectionPropertyDefinitionConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000022D0 File Offset: 0x000004D0
		public sealed override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable == null)
			{
				throw new ArgumentException(DataStrings.PropertyNotACollection((value == null) ? "<null>" : value.GetType().Name));
			}
			return this.Validate(enumerable, propertyDefinition, propertyBag);
		}

		// Token: 0x0600000C RID: 12
		public abstract PropertyConstraintViolationError Validate(IEnumerable collection, PropertyDefinition propertyDefinition, IPropertyBag propertyBag);
	}
}
