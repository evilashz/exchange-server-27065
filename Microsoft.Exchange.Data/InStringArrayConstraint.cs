using System;
using System.Collections;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200015A RID: 346
	[Serializable]
	internal class InStringArrayConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x00023409 File Offset: 0x00021609
		public InStringArrayConstraint(string[] targetArray, bool ignoreCase)
		{
			this.targetArray = targetArray;
			this.stringComparer = (ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002342D File Offset: 0x0002162D
		public IList TargetArray
		{
			get
			{
				return this.targetArray;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00023438 File Offset: 0x00021638
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.targetArray != null)
			{
				for (int i = 0; i < this.targetArray.Length; i++)
				{
					if (this.stringComparer.Compare(this.targetArray[i], value as string) == 0)
					{
						return null;
					}
				}
				if (this.targetArray.Length > 0)
				{
					stringBuilder.Append(this.targetArray[0]);
					for (int j = 1; j < this.targetArray.Length; j++)
					{
						stringBuilder.AppendFormat(",{0}", this.targetArray[j]);
					}
				}
			}
			return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsNotInGivenStringArray(stringBuilder.ToString(), value.ToString()), propertyDefinition, value, this);
		}

		// Token: 0x04000705 RID: 1797
		private string[] targetArray;

		// Token: 0x04000706 RID: 1798
		private StringComparer stringComparer;
	}
}
