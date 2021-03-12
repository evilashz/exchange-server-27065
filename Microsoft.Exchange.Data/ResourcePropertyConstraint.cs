using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200018F RID: 399
	[Serializable]
	internal class ResourcePropertyConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x00027E9C File Offset: 0x0002609C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(new char[]
				{
					'/'
				});
				if (array.Length != 2)
				{
					return new PropertyConstraintViolationError(DataStrings.InvalidResourcePropertySyntax, propertyDefinition, value, this);
				}
				if (text.IndexOf('/') != text.LastIndexOf('/'))
				{
					return new PropertyConstraintViolationError(DataStrings.InvalidResourcePropertySyntax, propertyDefinition, value, this);
				}
				if (array[0].Length == 0 || array[1].Length == 0)
				{
					return new PropertyConstraintViolationError(DataStrings.InvalidResourcePropertySyntax, propertyDefinition, value, this);
				}
				if (!ResourcePropertyConstraint.IsLetterNumString(array[0]) || !ResourcePropertyConstraint.IsLetterNumString(array[1]))
				{
					return new PropertyConstraintViolationError(DataStrings.InvalidResourcePropertySyntax, propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00027F48 File Offset: 0x00026148
		private static bool IsLetterNumString(string s)
		{
			foreach (char c in s)
			{
				if (!char.IsNumber(c) && !char.IsLetter(c))
				{
					return false;
				}
			}
			return true;
		}
	}
}
