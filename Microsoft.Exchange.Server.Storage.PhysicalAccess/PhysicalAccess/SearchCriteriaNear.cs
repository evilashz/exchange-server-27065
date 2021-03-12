using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000038 RID: 56
	public abstract class SearchCriteriaNear : SearchCriteria
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		protected SearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria)
		{
			if (criteria == null || criteria.NestedCriteria == null)
			{
				throw new StoreException((LID)59728U, ErrorCodeValue.TooComplex, "NEAR requires a non-null AND criteria");
			}
			if (criteria.NestedCriteria.Length < 2)
			{
				throw new StoreException((LID)35152U, ErrorCodeValue.TooComplex, "NEAR requires at least two criteria");
			}
			bool foundRootAndCriteria = false;
			criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaAnd && !foundRootAndCriteria)
				{
					foundRootAndCriteria = true;
					return criterion;
				}
				if (criterion is SearchCriteriaNear)
				{
					return null;
				}
				if (criterion is SearchCriteriaOr)
				{
					return criterion;
				}
				if (!(criterion is SearchCriteriaText))
				{
					throw new StoreException((LID)52904U, ErrorCodeValue.TooComplex, "NEAR only supports TEXT, NEAR and OR operators");
				}
				return null;
			}, null, false);
			this.distance = distance;
			this.ordered = ordered;
			this.criteria = criteria;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000FE64 File Offset: 0x0000E064
		public int Distance
		{
			get
			{
				return this.distance;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000FE74 File Offset: 0x0000E074
		public SearchCriteriaAnd Criteria
		{
			get
			{
				return this.criteria;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			return this.criteria.Evaluate(twir, compareInfo);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000FE8B File Offset: 0x0000E08B
		public override void EnumerateColumns(Action<Column, object> callback, object state, bool explodeCompositeColumns)
		{
			this.criteria.EnumerateColumns(callback, state, explodeCompositeColumns);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000FE9C File Offset: 0x0000E09C
		protected override SearchCriteria InspectAndFixChildren(SearchCriteria.InspectAndFixCriteriaDelegate callback, CompareInfo compareInfo, bool simplifyNegation)
		{
			SearchCriteria searchCriteria = this.criteria.InspectAndFix(callback, compareInfo, simplifyNegation);
			if (object.ReferenceEquals(searchCriteria, this.criteria))
			{
				return this;
			}
			if (searchCriteria is SearchCriteriaAnd)
			{
				return Factory.CreateSearchCriteriaNear(this.distance, this.ordered, (SearchCriteriaAnd)searchCriteria);
			}
			return searchCriteria;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("NEAR(");
			for (int i = 0; i < this.criteria.NestedCriteria.Length; i++)
			{
				this.criteria.NestedCriteria[i].AppendToString(sb, formatOptions);
				sb.Append(", ");
			}
			sb.Append(this.distance);
			sb.Append(", ");
			sb.Append(this.ordered);
			sb.Append(")");
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000FF70 File Offset: 0x0000E170
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			SearchCriteriaNear searchCriteriaNear = other as SearchCriteriaNear;
			return searchCriteriaNear != null && this.ordered == searchCriteriaNear.ordered && this.distance == searchCriteriaNear.distance && (object.ReferenceEquals(this.criteria, searchCriteriaNear.criteria) || (this.criteria != null && this.criteria.Equals(searchCriteriaNear.criteria)));
		}

		// Token: 0x040000CC RID: 204
		private readonly int distance;

		// Token: 0x040000CD RID: 205
		private readonly bool ordered;

		// Token: 0x040000CE RID: 206
		private readonly SearchCriteriaAnd criteria;
	}
}
