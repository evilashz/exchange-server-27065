using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200003C RID: 60
	public abstract class SearchCriteriaBitMask : SearchCriteria
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000105D1 File Offset: 0x0000E7D1
		protected SearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op)
		{
			this.lhs = lhs;
			this.op = op;
			this.rhs = rhs;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000105EE File Offset: 0x0000E7EE
		public Column Lhs
		{
			get
			{
				return this.lhs;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000105F6 File Offset: 0x0000E7F6
		public SearchCriteriaBitMask.SearchBitMaskOp Op
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000105FE File Offset: 0x0000E7FE
		public Column Rhs
		{
			get
			{
				return this.rhs;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010608 File Offset: 0x0000E808
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			object o = this.Lhs.Evaluate(twir);
			object o2 = this.Rhs.Evaluate(twir);
			long num = SearchCriteriaBitMask.ConvertToInt64(o);
			long num2 = SearchCriteriaBitMask.ConvertToInt64(o2);
			bool result;
			switch (this.Op)
			{
			case SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero:
				result = ((num & num2) == 0L);
				break;
			case SearchCriteriaBitMask.SearchBitMaskOp.NotEqualToZero:
				result = ((num & num2) != 0L);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00010674 File Offset: 0x0000E874
		public override void EnumerateColumns(Action<Column, object> callback, object state, bool explodeCompositeColumns)
		{
			if (this.lhs != null)
			{
				if (explodeCompositeColumns)
				{
					this.lhs.EnumerateColumns(callback, state);
				}
				else
				{
					callback(this.lhs, state);
				}
			}
			if (this.rhs != null)
			{
				if (explodeCompositeColumns)
				{
					this.rhs.EnumerateColumns(callback, state);
					return;
				}
				callback(this.rhs, state);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000106DA File Offset: 0x0000E8DA
		internal override bool CanBeNegated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000106E0 File Offset: 0x0000E8E0
		internal static void BitMaskOpAsString(SearchCriteriaBitMask.SearchBitMaskOp op, StringBuilder sb)
		{
			switch (op)
			{
			case SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero:
				sb.Append(" = 0");
				return;
			case SearchCriteriaBitMask.SearchBitMaskOp.NotEqualToZero:
				sb.Append(" <> 0");
				return;
			default:
				return;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00010718 File Offset: 0x0000E918
		internal override SearchCriteria Negate()
		{
			if (!this.CanBeNegated)
			{
				return base.Negate();
			}
			SearchCriteriaBitMask.SearchBitMaskOp searchBitMaskOp;
			switch (this.op)
			{
			case SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero:
				searchBitMaskOp = SearchCriteriaBitMask.SearchBitMaskOp.NotEqualToZero;
				break;
			case SearchCriteriaBitMask.SearchBitMaskOp.NotEqualToZero:
				searchBitMaskOp = SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero;
				break;
			default:
				return base.Negate();
			}
			return Factory.CreateSearchCriteriaBitMask(this.lhs, this.rhs, searchBitMaskOp);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001076C File Offset: 0x0000E96C
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("BITMASK(");
			this.lhs.AppendToString(sb, formatOptions);
			sb.Append(" & ");
			this.rhs.AppendToString(sb, formatOptions);
			SearchCriteriaBitMask.BitMaskOpAsString(this.op, sb);
			sb.Append(")");
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000107C3 File Offset: 0x0000E9C3
		private static long ConvertToInt64(object o)
		{
			if (o == null)
			{
				return 0L;
			}
			if (o is int)
			{
				return (long)((ulong)((int)o));
			}
			if (o is short)
			{
				return (long)((ulong)((ushort)((short)o)));
			}
			return (long)o;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000107F4 File Offset: 0x0000E9F4
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			SearchCriteriaBitMask searchCriteriaBitMask = other as SearchCriteriaBitMask;
			return searchCriteriaBitMask != null && this.lhs == searchCriteriaBitMask.lhs && this.op == searchCriteriaBitMask.op && this.rhs == searchCriteriaBitMask.rhs;
		}

		// Token: 0x040000DA RID: 218
		private readonly Column lhs;

		// Token: 0x040000DB RID: 219
		private readonly SearchCriteriaBitMask.SearchBitMaskOp op;

		// Token: 0x040000DC RID: 220
		private readonly Column rhs;

		// Token: 0x0200003D RID: 61
		public enum SearchBitMaskOp
		{
			// Token: 0x040000DE RID: 222
			EqualToZero,
			// Token: 0x040000DF RID: 223
			NotEqualToZero
		}
	}
}
