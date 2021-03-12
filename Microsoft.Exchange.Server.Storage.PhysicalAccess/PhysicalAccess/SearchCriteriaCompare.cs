using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200003A RID: 58
	public abstract class SearchCriteriaCompare : SearchCriteria
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x000100DD File Offset: 0x0000E2DD
		protected SearchCriteriaCompare(Column lhs, SearchCriteriaCompare.SearchRelOp op, Column rhs)
		{
			this.lhs = lhs;
			this.relOp = op;
			this.rhs = rhs;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000100FA File Offset: 0x0000E2FA
		public Column Lhs
		{
			get
			{
				return this.lhs;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00010102 File Offset: 0x0000E302
		public SearchCriteriaCompare.SearchRelOp RelOp
		{
			get
			{
				return this.relOp;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001010A File Offset: 0x0000E30A
		public Column Rhs
		{
			get
			{
				return this.rhs;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010114 File Offset: 0x0000E314
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			object obj = this.Lhs.Evaluate(twir);
			object obj2 = this.Rhs.Evaluate(twir);
			bool flag = false;
			bool flag2 = (byte)(this.Lhs.ExtendedTypeCode & ExtendedTypeCode.MVFlag) == 16 && (this.Lhs.ExtendedTypeCode & (ExtendedTypeCode)239) == this.Rhs.ExtendedTypeCode;
			bool flag3 = (byte)(this.Rhs.ExtendedTypeCode & ExtendedTypeCode.MVFlag) == 16 && (this.Rhs.ExtendedTypeCode & (ExtendedTypeCode)239) == this.Lhs.ExtendedTypeCode;
			if (flag2 || flag3)
			{
				if (this.RelOp != SearchCriteriaCompare.SearchRelOp.Equal && this.RelOp != SearchCriteriaCompare.SearchRelOp.NotEqual)
				{
					throw new StoreException((LID)46056U, ErrorCodeValue.TooComplex, "One of comparison supports only Equal or NotEqual operators.");
				}
				Array array;
				object obj3;
				if (flag2)
				{
					array = (Array)obj;
					obj3 = obj2;
				}
				else
				{
					array = (Array)obj2;
					obj3 = obj;
				}
				if (obj3 is LargeValue)
				{
					throw new StoreException((LID)48768U, ErrorCodeValue.TooComplex, "One of comparison is LargeValue.");
				}
				int num = (array == null) ? 0 : array.Length;
				for (int i = 0; i < num; i++)
				{
					if (ValueHelper.ValuesCompare(array.GetValue(i), obj3, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0)
					{
						flag = true;
						break;
					}
				}
				if (this.RelOp == SearchCriteriaCompare.SearchRelOp.NotEqual)
				{
					flag = !flag;
				}
			}
			else
			{
				if (obj is LargeValue || obj2 is LargeValue)
				{
					throw new StoreException((LID)49280U, ErrorCodeValue.TooComplex, "One of comparison is LargeValue.");
				}
				int num2 = ValueHelper.ValuesCompare(obj, obj2, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				switch (this.RelOp)
				{
				case SearchCriteriaCompare.SearchRelOp.Equal:
					flag = (num2 == 0);
					break;
				case SearchCriteriaCompare.SearchRelOp.NotEqual:
					flag = (num2 != 0);
					break;
				case SearchCriteriaCompare.SearchRelOp.LessThan:
					flag = (num2 < 0);
					break;
				case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
					flag = (num2 <= 0);
					break;
				case SearchCriteriaCompare.SearchRelOp.GreaterThan:
					flag = (num2 > 0);
					break;
				case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
					flag = (num2 >= 0);
					break;
				default:
					flag = false;
					break;
				}
			}
			return flag;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010308 File Offset: 0x0000E508
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

		// Token: 0x060002CC RID: 716 RVA: 0x00010370 File Offset: 0x0000E570
		protected override SearchCriteria InspectAndFixChildren(SearchCriteria.InspectAndFixCriteriaDelegate callback, CompareInfo compareInfo, bool simplifyNegation)
		{
			ConstantColumn col = this.lhs as ConstantColumn;
			if (!(col != null))
			{
				return this;
			}
			ConstantColumn col2 = this.rhs as ConstantColumn;
			if (!(col2 != null))
			{
				return Factory.CreateSearchCriteriaCompare(this.rhs, this.InvertSearchRelOp(), this.lhs);
			}
			if (!this.Evaluate(EmptyTwir.Instance, compareInfo))
			{
				return Factory.CreateSearchCriteriaFalse();
			}
			return Factory.CreateSearchCriteriaTrue();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000103DC File Offset: 0x0000E5DC
		internal SearchCriteriaCompare.SearchRelOp InvertSearchRelOp()
		{
			SearchCriteriaCompare.SearchRelOp result = this.relOp;
			switch (this.relOp)
			{
			case SearchCriteriaCompare.SearchRelOp.LessThan:
				result = SearchCriteriaCompare.SearchRelOp.GreaterThan;
				break;
			case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
				result = SearchCriteriaCompare.SearchRelOp.GreaterThanEqual;
				break;
			case SearchCriteriaCompare.SearchRelOp.GreaterThan:
				result = SearchCriteriaCompare.SearchRelOp.LessThan;
				break;
			case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
				result = SearchCriteriaCompare.SearchRelOp.LessThanEqual;
				break;
			}
			return result;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010428 File Offset: 0x0000E628
		internal SearchCriteriaCompare.SearchRelOp NegateSearchRelOp()
		{
			SearchCriteriaCompare.SearchRelOp result = this.relOp;
			switch (this.relOp)
			{
			case SearchCriteriaCompare.SearchRelOp.Equal:
				result = SearchCriteriaCompare.SearchRelOp.NotEqual;
				break;
			case SearchCriteriaCompare.SearchRelOp.NotEqual:
				result = SearchCriteriaCompare.SearchRelOp.Equal;
				break;
			case SearchCriteriaCompare.SearchRelOp.LessThan:
				result = SearchCriteriaCompare.SearchRelOp.GreaterThanEqual;
				break;
			case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
				result = SearchCriteriaCompare.SearchRelOp.GreaterThan;
				break;
			case SearchCriteriaCompare.SearchRelOp.GreaterThan:
				result = SearchCriteriaCompare.SearchRelOp.LessThanEqual;
				break;
			case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
				result = SearchCriteriaCompare.SearchRelOp.LessThan;
				break;
			}
			return result;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0001047A File Offset: 0x0000E67A
		internal override bool CanBeNegated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00010480 File Offset: 0x0000E680
		internal override SearchCriteria Negate()
		{
			if (!this.CanBeNegated)
			{
				return base.Negate();
			}
			SearchCriteriaCompare.SearchRelOp searchRelOp = this.NegateSearchRelOp();
			if (searchRelOp == this.relOp)
			{
				return base.Negate();
			}
			return Factory.CreateSearchCriteriaCompare(this.lhs, searchRelOp, this.rhs);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000104C5 File Offset: 0x0000E6C5
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("CMP(");
			this.lhs.AppendToString(sb, formatOptions);
			SearchCriteriaCompare.RelOpAsString(this.relOp, sb);
			this.rhs.AppendToString(sb, formatOptions);
			sb.Append(")");
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00010508 File Offset: 0x0000E708
		internal static void RelOpAsString(SearchCriteriaCompare.SearchRelOp op, StringBuilder sb)
		{
			switch (op)
			{
			case SearchCriteriaCompare.SearchRelOp.Equal:
				sb.Append(" = ");
				return;
			case SearchCriteriaCompare.SearchRelOp.NotEqual:
				sb.Append(" <> ");
				return;
			case SearchCriteriaCompare.SearchRelOp.LessThan:
				sb.Append(" < ");
				return;
			case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
				sb.Append(" <= ");
				return;
			case SearchCriteriaCompare.SearchRelOp.GreaterThan:
				sb.Append(" > ");
				return;
			case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
				sb.Append(" >= ");
				return;
			default:
				return;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00010584 File Offset: 0x0000E784
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			SearchCriteriaCompare searchCriteriaCompare = other as SearchCriteriaCompare;
			return searchCriteriaCompare != null && this.lhs == searchCriteriaCompare.lhs && this.rhs == searchCriteriaCompare.rhs && this.relOp == searchCriteriaCompare.relOp;
		}

		// Token: 0x040000D0 RID: 208
		private readonly Column lhs;

		// Token: 0x040000D1 RID: 209
		private readonly SearchCriteriaCompare.SearchRelOp relOp;

		// Token: 0x040000D2 RID: 210
		private readonly Column rhs;

		// Token: 0x0200003B RID: 59
		public enum SearchRelOp
		{
			// Token: 0x040000D4 RID: 212
			Equal,
			// Token: 0x040000D5 RID: 213
			NotEqual,
			// Token: 0x040000D6 RID: 214
			LessThan,
			// Token: 0x040000D7 RID: 215
			LessThanEqual,
			// Token: 0x040000D8 RID: 216
			GreaterThan,
			// Token: 0x040000D9 RID: 217
			GreaterThanEqual
		}
	}
}
