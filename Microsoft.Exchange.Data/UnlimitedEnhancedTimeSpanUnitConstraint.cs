using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	internal class UnlimitedEnhancedTimeSpanUnitConstraint : EnhancedTimeSpanUnitConstraint
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x0003223E File Offset: 0x0003043E
		public UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan unit) : base(unit)
		{
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00032248 File Offset: 0x00030448
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			object value2 = null;
			bool isUnlimited;
			if (value is Unlimited<TimeSpan>)
			{
				isUnlimited = ((Unlimited<TimeSpan>)value).IsUnlimited;
				if (!isUnlimited)
				{
					value2 = ((Unlimited<TimeSpan>)value).Value;
				}
			}
			else
			{
				isUnlimited = ((Unlimited<EnhancedTimeSpan>)value).IsUnlimited;
				if (!isUnlimited)
				{
					value2 = ((Unlimited<EnhancedTimeSpan>)value).Value;
				}
			}
			if (!isUnlimited)
			{
				return base.Validate(value2, propertyDefinition, propertyBag);
			}
			return null;
		}
	}
}
