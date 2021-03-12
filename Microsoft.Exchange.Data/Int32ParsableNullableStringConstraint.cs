using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	internal class Int32ParsableNullableStringConstraint : Int32ParsableStringConstraint
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x0002354B File Offset: 0x0002174B
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return null;
			}
			return base.Validate(value, propertyDefinition, propertyBag);
		}
	}
}
