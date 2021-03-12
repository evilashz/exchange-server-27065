using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	internal class EnumValueDefinedConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x00022956 File Offset: 0x00020B56
		public EnumValueDefinedConstraint(Type enumType)
		{
			this.enumType = enumType;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00022965 File Offset: 0x00020B65
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (!Enum.IsDefined(this.enumType, value))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationEnumValueNotDefined(value.ToString(), propertyDefinition.Type.Name), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x040006A6 RID: 1702
		private Type enumType;
	}
}
