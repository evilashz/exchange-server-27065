using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000037 RID: 55
	internal class ConditionalMatch : IMatch
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000E1AD File Offset: 0x0000C3AD
		internal ConditionalMatch(IMatch match, IMatch precondition)
		{
			this.precondition = precondition;
			this.match = match;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E1C3 File Offset: 0x0000C3C3
		internal ConditionalMatch(ConditionalMatch original, MatchFactory factory)
		{
			this.precondition = factory.Copy(original.precondition);
			this.match = factory.Copy(original.match);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E1EF File Offset: 0x0000C3EF
		public bool IsMatch(TextScanContext data)
		{
			return this.precondition.IsMatch(data) && this.match.IsMatch(data);
		}

		// Token: 0x04000131 RID: 305
		private IMatch match;

		// Token: 0x04000132 RID: 306
		private IMatch precondition;
	}
}
