using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000039 RID: 57
	public abstract class SearchCriteriaNot : SearchCriteria
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000FFD5 File Offset: 0x0000E1D5
		protected SearchCriteriaNot(SearchCriteria criteria)
		{
			this.criteria = criteria;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		public SearchCriteria Criteria
		{
			get
			{
				return this.criteria;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			return !this.criteria.Evaluate(twir, compareInfo);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FFFE File Offset: 0x0000E1FE
		public override void EnumerateColumns(Action<Column, object> callback, object state, bool explodeCompositeColumns)
		{
			this.criteria.EnumerateColumns(callback, state, explodeCompositeColumns);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010010 File Offset: 0x0000E210
		protected override SearchCriteria InspectAndFixChildren(SearchCriteria.InspectAndFixCriteriaDelegate callback, CompareInfo compareInfo, bool simplifyNegation)
		{
			SearchCriteria searchCriteria = this.criteria.InspectAndFix(callback, compareInfo, simplifyNegation);
			if (simplifyNegation)
			{
				if (!object.ReferenceEquals(searchCriteria, this.criteria) || searchCriteria.CanBeNegated)
				{
					return searchCriteria.Negate();
				}
			}
			else if (!object.ReferenceEquals(searchCriteria, this.criteria))
			{
				return Factory.CreateSearchCriteriaNot(searchCriteria);
			}
			return this;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00010062 File Offset: 0x0000E262
		internal override bool CanBeNegated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010065 File Offset: 0x0000E265
		internal override SearchCriteria Negate()
		{
			return this.criteria;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001006D File Offset: 0x0000E26D
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("NOT(");
			this.criteria.AppendToString(sb, formatOptions);
			sb.Append(")");
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010094 File Offset: 0x0000E294
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			SearchCriteriaNot searchCriteriaNot = other as SearchCriteriaNot;
			return searchCriteriaNot != null && (object.ReferenceEquals(this.criteria, searchCriteriaNot.criteria) || (this.criteria != null && this.criteria.Equals(searchCriteriaNot.criteria)));
		}

		// Token: 0x040000CF RID: 207
		private readonly SearchCriteria criteria;
	}
}
