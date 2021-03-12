using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	internal sealed class NullableEnumValueDefinedConstraint : EnumValueDefinedConstraint
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x00026781 File Offset: 0x00024981
		public NullableEnumValueDefinedConstraint(Type enumType) : base(enumType)
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002678A File Offset: 0x0002498A
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
