using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000035 RID: 53
	public abstract class SearchCriteriaFalse : SearchCriteria
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			return false;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000F0E9 File Offset: 0x0000D2E9
		internal override bool CanBeNegated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		internal override SearchCriteria Negate()
		{
			return Factory.CreateSearchCriteriaTrue();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("FALSE");
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000F101 File Offset: 0x0000D301
		public override string ToString()
		{
			return "FALSE";
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F108 File Offset: 0x0000D308
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			return other is SearchCriteriaFalse;
		}
	}
}
