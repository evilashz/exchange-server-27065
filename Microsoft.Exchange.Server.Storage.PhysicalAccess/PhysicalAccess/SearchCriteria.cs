using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000031 RID: 49
	public abstract class SearchCriteria : IEquatable<SearchCriteria>
	{
		// Token: 0x06000276 RID: 630
		public abstract bool Evaluate(ITWIR twir, CompareInfo compareInfo);

		// Token: 0x06000277 RID: 631 RVA: 0x0000EEC6 File Offset: 0x0000D0C6
		public void EnumerateColumns(Action<Column, object> callback, object state)
		{
			this.EnumerateColumns(callback, state, false);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EED1 File Offset: 0x0000D0D1
		public virtual void EnumerateColumns(Action<Column, object> callback, object state, bool explodeCompositeColumns)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public SearchCriteria InspectAndFix(SearchCriteria.InspectAndFixCriteriaDelegate callback, CompareInfo compareInfo, bool simplifyNegation)
		{
			if (callback != null)
			{
				SearchCriteria searchCriteria = callback(this, compareInfo);
				if (searchCriteria == null)
				{
					return this;
				}
				if (!object.ReferenceEquals(searchCriteria, this))
				{
					return searchCriteria;
				}
			}
			return this.InspectAndFixChildren(callback, compareInfo, simplifyNegation);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000EF06 File Offset: 0x0000D106
		protected virtual SearchCriteria InspectAndFixChildren(SearchCriteria.InspectAndFixCriteriaDelegate callback, CompareInfo compareInfo, bool simplifyNegation)
		{
			return this;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000EF09 File Offset: 0x0000D109
		internal virtual bool CanBeNegated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EF0C File Offset: 0x0000D10C
		internal virtual SearchCriteria Negate()
		{
			return Factory.CreateSearchCriteriaNot(this);
		}

		// Token: 0x0600027D RID: 637
		public abstract void AppendToString(StringBuilder sb, StringFormatOptions formatOptions);

		// Token: 0x0600027E RID: 638 RVA: 0x0000EF14 File Offset: 0x0000D114
		public override string ToString()
		{
			return this.ToString(StringFormatOptions.IncludeDetails);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000EF20 File Offset: 0x0000D120
		public string ToString(StringFormatOptions formatOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToString(stringBuilder, formatOptions);
			return stringBuilder.ToString();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000EF41 File Offset: 0x0000D141
		public bool Equals(SearchCriteria other)
		{
			return object.ReferenceEquals(this, other) || this.CriteriaEquivalent(other);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000EF58 File Offset: 0x0000D158
		public override bool Equals(object other)
		{
			SearchCriteria searchCriteria = other as SearchCriteria;
			return searchCriteria != null && this.Equals(searchCriteria);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EF78 File Offset: 0x0000D178
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000283 RID: 643
		protected abstract bool CriteriaEquivalent(SearchCriteria other);

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000EF7B File Offset: 0x0000D17B
		internal SearchCriteria.LegEnumerator AndLegs
		{
			get
			{
				return new SearchCriteria.LegEnumerator(this, false);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000EF84 File Offset: 0x0000D184
		internal SearchCriteria.LegEnumerator OrLegs
		{
			get
			{
				return new SearchCriteria.LegEnumerator(this, true);
			}
		}

		// Token: 0x040000C5 RID: 197
		protected const int MaxAndOrLegsToOptimize = 100;

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x06000288 RID: 648
		public delegate SearchCriteria InspectAndFixCriteriaDelegate(SearchCriteria criteria, CompareInfo compareInfo);

		// Token: 0x02000033 RID: 51
		internal struct LegEnumerator : IEnumerator<SearchCriteria>, IDisposable, IEnumerator
		{
			// Token: 0x0600028B RID: 651 RVA: 0x0000EF98 File Offset: 0x0000D198
			public LegEnumerator(SearchCriteria criteria, bool orLegs)
			{
				this.legIndex = -1;
				this.legs = criteria;
				if (orLegs)
				{
					SearchCriteriaOr searchCriteriaOr = criteria as SearchCriteriaOr;
					if (searchCriteriaOr != null)
					{
						this.legs = searchCriteriaOr.NestedCriteria;
						return;
					}
				}
				else
				{
					SearchCriteriaAnd searchCriteriaAnd = criteria as SearchCriteriaAnd;
					if (searchCriteriaAnd != null)
					{
						this.legs = searchCriteriaAnd.NestedCriteria;
					}
				}
			}

			// Token: 0x0600028C RID: 652 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
			public SearchCriteria.LegEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600028D RID: 653 RVA: 0x0000EFEC File Offset: 0x0000D1EC
			public int Count
			{
				get
				{
					SearchCriteria[] array = this.legs as SearchCriteria[];
					if (array != null)
					{
						return array.Length;
					}
					return 1;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600028E RID: 654 RVA: 0x0000F010 File Offset: 0x0000D210
			public SearchCriteria Current
			{
				get
				{
					SearchCriteria[] array = this.legs as SearchCriteria[];
					if (array != null)
					{
						return array[this.legIndex];
					}
					return this.legs as SearchCriteria;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600028F RID: 655 RVA: 0x0000F040 File Offset: 0x0000D240
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000290 RID: 656 RVA: 0x0000F048 File Offset: 0x0000D248
			public bool MoveNext()
			{
				int num = 0;
				SearchCriteria[] array = this.legs as SearchCriteria[];
				if (array != null)
				{
					num = array.Length - 1;
				}
				if (this.legIndex >= num)
				{
					return false;
				}
				this.legIndex++;
				return true;
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000F086 File Offset: 0x0000D286
			public void Reset()
			{
				this.legIndex = -1;
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000F08F File Offset: 0x0000D28F
			public void Dispose()
			{
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000F094 File Offset: 0x0000D294
			public SearchCriteria.LegEnumerator Clone()
			{
				return this;
			}

			// Token: 0x040000C6 RID: 198
			private object legs;

			// Token: 0x040000C7 RID: 199
			private int legIndex;
		}
	}
}
