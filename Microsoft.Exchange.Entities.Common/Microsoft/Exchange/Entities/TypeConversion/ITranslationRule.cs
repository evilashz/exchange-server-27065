using System;

namespace Microsoft.Exchange.Entities.TypeConversion
{
	// Token: 0x02000064 RID: 100
	public interface ITranslationRule<in TLeft, in TRight>
	{
		// Token: 0x06000234 RID: 564
		void FromLeftToRightType(TLeft left, TRight right);

		// Token: 0x06000235 RID: 565
		void FromRightToLeftType(TLeft left, TRight right);
	}
}
