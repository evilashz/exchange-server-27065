using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000036 RID: 54
	public abstract class SearchCriteriaAnd : SearchCriteria
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000F113 File Offset: 0x0000D313
		protected SearchCriteriaAnd(params SearchCriteria[] nestedCriteria)
		{
			this.nestedCriteria = nestedCriteria;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000F122 File Offset: 0x0000D322
		public SearchCriteria[] NestedCriteria
		{
			get
			{
				return this.nestedCriteria;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000F12C File Offset: 0x0000D32C
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			for (int i = 0; i < this.nestedCriteria.Length; i++)
			{
				if (!this.nestedCriteria[i].Evaluate(twir, compareInfo))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F160 File Offset: 0x0000D360
		public override void EnumerateColumns(Action<Column, object> callback, object state, bool explodeCompositeColumns)
		{
			for (int i = 0; i < this.nestedCriteria.Length; i++)
			{
				this.nestedCriteria[i].EnumerateColumns(callback, state, explodeCompositeColumns);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000F190 File Offset: 0x0000D390
		protected override SearchCriteria InspectAndFixChildren(SearchCriteria.InspectAndFixCriteriaDelegate callback, CompareInfo compareInfo, bool simplifyNegation)
		{
			List<SearchCriteria> list = null;
			for (int i = 0; i < this.nestedCriteria.Length; i++)
			{
				SearchCriteria searchCriteria = this.nestedCriteria[i].InspectAndFix(callback, compareInfo, simplifyNegation);
				if (list != null || !object.ReferenceEquals(searchCriteria, this.nestedCriteria[i]) || searchCriteria is SearchCriteriaFalse || searchCriteria is SearchCriteriaTrue || searchCriteria is SearchCriteriaAnd)
				{
					if (searchCriteria is SearchCriteriaFalse)
					{
						return Factory.CreateSearchCriteriaFalse();
					}
					if (list == null)
					{
						list = new List<SearchCriteria>(this.nestedCriteria.Length);
						if (i != 0)
						{
							for (int j = 0; j < i; j++)
							{
								list.Add(this.nestedCriteria[j]);
							}
						}
					}
					if (!(searchCriteria is SearchCriteriaTrue))
					{
						if (searchCriteria is SearchCriteriaAnd)
						{
							foreach (SearchCriteria item in ((SearchCriteriaAnd)searchCriteria).NestedCriteria)
							{
								list.Add(item);
							}
						}
						else
						{
							list.Add(searchCriteria);
						}
					}
				}
			}
			IList<SearchCriteria> list2 = (list == null) ? ((IList<SearchCriteria>)this.nestedCriteria) : list;
			if (list2.Count < 100)
			{
				for (int l = list2.Count - 1; l > 0; l--)
				{
					for (int m = 0; m < l; m++)
					{
						if (list2[l].Equals(list2[m]))
						{
							if (list == null)
							{
								list = new List<SearchCriteria>(list2);
								list2 = list;
							}
							list2.RemoveAt(l);
							break;
						}
					}
				}
			}
			if (list2.Count == 0)
			{
				return Factory.CreateSearchCriteriaTrue();
			}
			if (list2.Count == 1)
			{
				return list2[0];
			}
			int count;
			using (SearchCriteria.LegEnumerator orLegs = list2[0].OrLegs)
			{
				count = orLegs.Count;
			}
			if (list2.Count < 100 && count < 100)
			{
				List<SearchCriteria> list3 = null;
				foreach (SearchCriteria searchCriteria2 in list2[0].OrLegs)
				{
					bool flag = true;
					for (int n = 1; n < list2.Count; n++)
					{
						bool flag2 = false;
						foreach (SearchCriteria other in list2[n].OrLegs)
						{
							if (searchCriteria2.Equals(other))
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						if (list3 == null)
						{
							list3 = new List<SearchCriteria>(2);
						}
						list3.Add(searchCriteria2);
					}
				}
				if (list3 != null)
				{
					if (list == null)
					{
						list = new List<SearchCriteria>(list2);
						list2 = list;
					}
					for (int num = 0; num < list2.Count; num++)
					{
						SearchCriteria searchCriteria3 = null;
						SearchCriteriaOr searchCriteriaOr = list2[num] as SearchCriteriaOr;
						if (searchCriteriaOr != null)
						{
							List<SearchCriteria> list4 = new List<SearchCriteria>(searchCriteriaOr.NestedCriteria);
							for (int num2 = 0; num2 < list3.Count; num2++)
							{
								for (int num3 = list4.Count - 1; num3 >= 0; num3--)
								{
									if (list4[num3].Equals(list3[num2]))
									{
										list4.RemoveAt(num3);
										break;
									}
								}
							}
							if (list4.Count != 0)
							{
								if (list4.Count == 1)
								{
									searchCriteria3 = list4[0];
								}
								else
								{
									searchCriteria3 = Factory.CreateSearchCriteriaOr(list4.ToArray());
								}
							}
						}
						if (searchCriteria3 == null)
						{
							list = (list2 = null);
							break;
						}
						list2[num] = searchCriteria3;
					}
					if (list != null)
					{
						list3.Add(Factory.CreateSearchCriteriaAnd(list.ToArray()));
					}
					if (list3.Count == 1)
					{
						return list3[0];
					}
					return Factory.CreateSearchCriteriaOr(list3.ToArray());
				}
			}
			if (list != null)
			{
				return Factory.CreateSearchCriteriaAnd(list.ToArray());
			}
			return this;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000F580 File Offset: 0x0000D780
		internal override bool CanBeNegated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F584 File Offset: 0x0000D784
		internal override SearchCriteria Negate()
		{
			SearchCriteria[] array = new SearchCriteria[this.nestedCriteria.Length];
			for (int i = 0; i < this.nestedCriteria.Length; i++)
			{
				array[i] = this.nestedCriteria[i].Negate();
			}
			return Factory.CreateSearchCriteriaOr(array);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("AND(");
			for (int i = 0; i < this.nestedCriteria.Length; i++)
			{
				if (i > 0)
				{
					sb.Append(", ");
				}
				this.nestedCriteria[i].AppendToString(sb, formatOptions);
			}
			sb.Append(")");
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F620 File Offset: 0x0000D820
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			SearchCriteriaAnd searchCriteriaAnd = other as SearchCriteriaAnd;
			if (searchCriteriaAnd == null || this.nestedCriteria.Length != searchCriteriaAnd.nestedCriteria.Length)
			{
				return false;
			}
			for (int i = 0; i < this.nestedCriteria.Length; i++)
			{
				if (!this.nestedCriteria[i].Equals(searchCriteriaAnd.nestedCriteria[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000C8 RID: 200
		private readonly SearchCriteria[] nestedCriteria;
	}
}
