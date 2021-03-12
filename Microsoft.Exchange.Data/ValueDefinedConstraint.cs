using System;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	internal class ValueDefinedConstraint<T> : PropertyDefinitionConstraint
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x000324DE File Offset: 0x000306DE
		public ValueDefinedConstraint(T[] valuesArray, bool specifyAllowedValues)
		{
			if (valuesArray == null)
			{
				throw new ArgumentNullException("valuesArray");
			}
			this.valuesArray = new T[valuesArray.Length];
			valuesArray.CopyTo(this.valuesArray, 0);
			this.specifyAllowedValues = specifyAllowedValues;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00032516 File Offset: 0x00030716
		public ValueDefinedConstraint(T[] allowedValuesArray) : this(allowedValuesArray, true)
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00032520 File Offset: 0x00030720
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			T t;
			try
			{
				t = (T)((object)ValueConvertor.ConvertValue(value, typeof(T), null));
			}
			catch (NotImplementedException ex)
			{
				return new PropertyConstraintViolationError(new LocalizedString(ex.Message), propertyDefinition, value, this);
			}
			catch (TypeConversionException ex2)
			{
				return new PropertyConstraintViolationError(ex2.LocalizedString, propertyDefinition, value, this);
			}
			if (this.specifyAllowedValues)
			{
				foreach (T t2 in this.valuesArray)
				{
					if (object.Equals(t2, t))
					{
						return null;
					}
				}
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsNotAllowed(this.GetValuesString(this.valuesArray), t.ToString()), propertyDefinition, t, this);
			}
			foreach (T t3 in this.valuesArray)
			{
				if (object.Equals(t3, t))
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsDisallowed(this.GetValuesString(this.valuesArray), t.ToString()), propertyDefinition, t, this);
				}
			}
			return null;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00032674 File Offset: 0x00030874
		private string GetValuesString(T[] values)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format("\"{0}\"", values[0].ToString()));
			for (int i = 1; i < values.Length; i++)
			{
				stringBuilder.Append(string.Format(", \"{0}\"", values[i].ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040009D8 RID: 2520
		private T[] valuesArray;

		// Token: 0x040009D9 RID: 2521
		private bool specifyAllowedValues;
	}
}
