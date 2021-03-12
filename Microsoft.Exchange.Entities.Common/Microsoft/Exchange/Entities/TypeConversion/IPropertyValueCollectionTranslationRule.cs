using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.TypeConversion
{
	// Token: 0x02000065 RID: 101
	internal interface IPropertyValueCollectionTranslationRule<in TLeft, TLeftProperty, in TRight> : ITranslationRule<TLeft, TRight>
	{
		// Token: 0x06000236 RID: 566
		void FromPropertyValues(IDictionary<TLeftProperty, int> propertyIndices, IList values, TRight right);
	}
}
