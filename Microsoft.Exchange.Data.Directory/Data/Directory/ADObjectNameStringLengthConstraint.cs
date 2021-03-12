using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	internal sealed class ADObjectNameStringLengthConstraint : MandatoryStringLengthConstraint
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x000159C0 File Offset: 0x00013BC0
		public ADObjectNameStringLengthConstraint(int minLength, int maxLength) : base(minLength, maxLength)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000159CA File Offset: 0x00013BCA
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return null;
			}
			if (ADObjectNameHelper.ReservedADNameStringRegex.IsMatch(value.ToString()))
			{
				return null;
			}
			return base.Validate(value, propertyDefinition, propertyBag);
		}
	}
}
