using System;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000269 RID: 617
	internal class FractionalPageResult : BasePageResult
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x0004DCE5 File Offset: 0x0004BEE5
		public FractionalPageResult(BaseQueryView view, int numerator, int denominator) : base(view)
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0004DCFC File Offset: 0x0004BEFC
		public int NumeratorOffset
		{
			get
			{
				return this.numerator;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0004DD04 File Offset: 0x0004BF04
		public int AbsoluteDenominator
		{
			get
			{
				return this.denominator;
			}
		}

		// Token: 0x04000C02 RID: 3074
		private int numerator;

		// Token: 0x04000C03 RID: 3075
		private int denominator;
	}
}
