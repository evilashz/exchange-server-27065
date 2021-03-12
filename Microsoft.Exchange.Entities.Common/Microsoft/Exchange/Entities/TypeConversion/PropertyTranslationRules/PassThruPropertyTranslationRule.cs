using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000077 RID: 119
	internal class PassThruPropertyTranslationRule<TLeft, TRight, TLeftProperty, TPropertyValue> : PropertyTranslationRule<TLeft, TRight, TLeftProperty, TPropertyValue, TPropertyValue>
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00008A49 File Offset: 0x00006C49
		public PassThruPropertyTranslationRule(IPropertyAccessor<TLeft, TPropertyValue> leftAccessor, IPropertyAccessor<TRight, TPropertyValue> rightAccessor) : base(leftAccessor, rightAccessor, PassThruConverter<TPropertyValue>.SingletonInstance, PassThruConverter<TPropertyValue>.SingletonInstance)
		{
		}
	}
}
