using System;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000073 RID: 115
	public class DelegatedTranslationRule<TLeft, TRight> : ITranslationRule<TLeft, TRight>
	{
		// Token: 0x06000273 RID: 627 RVA: 0x000086AA File Offset: 0x000068AA
		public DelegatedTranslationRule(Action<TLeft, TRight> leftToRightDelegate, Action<TLeft, TRight> rightToLeftDelegate)
		{
			this.leftToRightDelegate = leftToRightDelegate;
			this.rightToLeftDelegate = rightToLeftDelegate;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000086C0 File Offset: 0x000068C0
		public static void NoOpDelegate(TLeft left, TRight right)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000086C2 File Offset: 0x000068C2
		public void FromLeftToRightType(TLeft left, TRight right)
		{
			this.leftToRightDelegate(left, right);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000086D1 File Offset: 0x000068D1
		public void FromRightToLeftType(TLeft left, TRight right)
		{
			this.rightToLeftDelegate(left, right);
		}

		// Token: 0x040000EB RID: 235
		private readonly Action<TLeft, TRight> leftToRightDelegate;

		// Token: 0x040000EC RID: 236
		private readonly Action<TLeft, TRight> rightToLeftDelegate;
	}
}
