using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000034 RID: 52
	public abstract class SearchCriteriaTrue : SearchCriteria
	{
		// Token: 0x06000295 RID: 661 RVA: 0x0000F0B1 File Offset: 0x0000D2B1
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			return true;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		internal override bool CanBeNegated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F0B7 File Offset: 0x0000D2B7
		internal override SearchCriteria Negate()
		{
			return Factory.CreateSearchCriteriaFalse();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("TRUE");
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public override string ToString()
		{
			return "TRUE";
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F0D3 File Offset: 0x0000D2D3
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			return other is SearchCriteriaTrue;
		}
	}
}
