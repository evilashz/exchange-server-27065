using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Entities.TypeConversion
{
	// Token: 0x0200007F RID: 127
	internal class TranslationStrategy<TLeft, TLeftProperties, TRight> : IPropertyValueCollectionTranslationRule<TLeft, TLeftProperties, TRight>, ITranslationRule<TLeft, TRight>, IEnumerable<ITranslationRule<TLeft, TRight>>, IEnumerable
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00009430 File Offset: 0x00007630
		public TranslationStrategy(params ITranslationRule<TLeft, TRight>[] propertyTranslationRules) : this(propertyTranslationRules.ToList<ITranslationRule<TLeft, TRight>>())
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000943E File Offset: 0x0000763E
		public TranslationStrategy(IList<ITranslationRule<TLeft, TRight>> propertyTranslationRules)
		{
			this.propertyTranslationRules = propertyTranslationRules;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000944D File Offset: 0x0000764D
		public void Add(ITranslationRule<TLeft, TRight> propertyTranslationRule)
		{
			this.propertyTranslationRules.Add(propertyTranslationRule);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000945C File Offset: 0x0000765C
		public void FromLeftToRightType(TLeft left, TRight right)
		{
			foreach (ITranslationRule<TLeft, TRight> translationRule in this.propertyTranslationRules)
			{
				translationRule.FromLeftToRightType(left, right);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000094AC File Offset: 0x000076AC
		public void FromRightToLeftType(TLeft left, TRight right)
		{
			foreach (ITranslationRule<TLeft, TRight> translationRule in this.propertyTranslationRules)
			{
				translationRule.FromRightToLeftType(left, right);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000094FC File Offset: 0x000076FC
		public void FromPropertyValues(IDictionary<TLeftProperties, int> propertyIndices, IList values, TRight right)
		{
			foreach (ITranslationRule<TLeft, TRight> translationRule in this.propertyTranslationRules)
			{
				IPropertyValueCollectionTranslationRule<TLeft, TLeftProperties, TRight> propertyValueCollectionTranslationRule = translationRule as IPropertyValueCollectionTranslationRule<TLeft, TLeftProperties, TRight>;
				if (propertyValueCollectionTranslationRule != null)
				{
					propertyValueCollectionTranslationRule.FromPropertyValues(propertyIndices, values, right);
				}
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009558 File Offset: 0x00007758
		public IEnumerator<ITranslationRule<TLeft, TRight>> GetEnumerator()
		{
			return this.propertyTranslationRules.GetEnumerator();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009565 File Offset: 0x00007765
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000105 RID: 261
		private readonly IList<ITranslationRule<TLeft, TRight>> propertyTranslationRules;
	}
}
