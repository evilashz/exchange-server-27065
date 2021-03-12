using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200003E RID: 62
	public abstract class SearchCriteriaText : SearchCriteria
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0001083F File Offset: 0x0000EA3F
		protected SearchCriteriaText(Column lhs, SearchCriteriaText.SearchTextFullness fullnessFlags, SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, Column rhs)
		{
			this.lhs = lhs;
			this.fullnessFlags = fullnessFlags;
			this.fuzzynessFlags = fuzzynessFlags;
			this.rhs = rhs;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00010864 File Offset: 0x0000EA64
		public Column Lhs
		{
			get
			{
				return this.lhs;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001086C File Offset: 0x0000EA6C
		public SearchCriteriaText.SearchTextFullness FullnessFlags
		{
			get
			{
				return this.fullnessFlags;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00010874 File Offset: 0x0000EA74
		public SearchCriteriaText.SearchTextFuzzyLevel FuzzynessFlags
		{
			get
			{
				return this.fuzzynessFlags;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0001087C File Offset: 0x0000EA7C
		public Column Rhs
		{
			get
			{
				return this.rhs;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00010884 File Offset: 0x0000EA84
		public override bool Evaluate(ITWIR twir, CompareInfo compareInfo)
		{
			object obj = this.Lhs.Evaluate(twir);
			object obj2 = this.Rhs.Evaluate(twir);
			bool flag = (byte)(this.Lhs.ExtendedTypeCode & ExtendedTypeCode.MVFlag) == 16 && (this.Lhs.ExtendedTypeCode & (ExtendedTypeCode)239) == this.Rhs.ExtendedTypeCode;
			bool flag2 = (byte)(this.Rhs.ExtendedTypeCode & ExtendedTypeCode.MVFlag) == 16 && (this.Rhs.ExtendedTypeCode & (ExtendedTypeCode)239) == this.Lhs.ExtendedTypeCode;
			if (flag || flag2)
			{
				Array array;
				if (flag)
				{
					array = (Array)obj;
				}
				else
				{
					array = (Array)obj2;
				}
				int num = (array == null) ? 0 : array.Length;
				for (int i = 0; i < num; i++)
				{
					bool flag3;
					if (flag)
					{
						flag3 = this.EvaluateHelper((string)array.GetValue(i), (string)obj2, compareInfo);
					}
					else
					{
						flag3 = this.EvaluateHelper((string)obj, (string)array.GetValue(i), compareInfo);
					}
					if (flag3)
					{
						return true;
					}
				}
				return false;
			}
			return this.EvaluateHelper(obj as string, obj2 as string, compareInfo);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000109B0 File Offset: 0x0000EBB0
		private bool EvaluateHelper(string lhsString, string rhsString, CompareInfo compareInfo)
		{
			SearchCriteriaText.SearchTextFullness searchTextFullness = this.FullnessFlags & ~(SearchCriteriaText.SearchTextFullness.PrefixOnAnyWord | SearchCriteriaText.SearchTextFullness.PhraseMatch);
			if (searchTextFullness == SearchCriteriaText.SearchTextFullness.FullString)
			{
				if (compareInfo == null)
				{
					return string.Compare(lhsString, rhsString, (this.FuzzynessFlags != (SearchCriteriaText.SearchTextFuzzyLevel)0) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
				}
				return compareInfo.Compare(lhsString, rhsString, (this.FuzzynessFlags != (SearchCriteriaText.SearchTextFuzzyLevel)0) ? CompareOptions.IgnoreCase : CompareOptions.None) == 0;
			}
			else
			{
				if (lhsString == null || rhsString == null)
				{
					return rhsString == null;
				}
				if (searchTextFullness == SearchCriteriaText.SearchTextFullness.Prefix)
				{
					if (compareInfo == null)
					{
						return lhsString.StartsWith(rhsString, (this.FuzzynessFlags != (SearchCriteriaText.SearchTextFuzzyLevel)0) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
					}
					return compareInfo.IsPrefix(lhsString, rhsString, (this.FuzzynessFlags != (SearchCriteriaText.SearchTextFuzzyLevel)0) ? CompareOptions.IgnoreCase : CompareOptions.None);
				}
				else
				{
					if ((ushort)(searchTextFullness & SearchCriteriaText.SearchTextFullness.SubString) != 1)
					{
						return false;
					}
					if (compareInfo == null)
					{
						return -1 != lhsString.IndexOf(rhsString, (this.FuzzynessFlags != (SearchCriteriaText.SearchTextFuzzyLevel)0) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
					}
					return -1 != compareInfo.IndexOf(lhsString, rhsString, (this.FuzzynessFlags != (SearchCriteriaText.SearchTextFuzzyLevel)0) ? CompareOptions.IgnoreCase : CompareOptions.None);
				}
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00010A7C File Offset: 0x0000EC7C
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

		// Token: 0x060002E8 RID: 744 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("TEXT(");
			this.lhs.AppendToString(sb, formatOptions);
			sb.Append(", ");
			this.rhs.AppendToString(sb, formatOptions);
			sb.Append(", ");
			SearchCriteriaText.FullnessFlagsAsString(this.fullnessFlags, sb);
			sb.Append(", ");
			SearchCriteriaText.FuzzynessFlagsAsString(this.fuzzynessFlags, sb);
			sb.Append(")");
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00010B5F File Offset: 0x0000ED5F
		internal static void FullnessFlagsAsString(SearchCriteriaText.SearchTextFullness fullnessFlags, StringBuilder sb)
		{
			sb.Append(fullnessFlags.ToString());
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00010B73 File Offset: 0x0000ED73
		internal static void FuzzynessFlagsAsString(SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, StringBuilder sb)
		{
			sb.Append(fuzzynessFlags.ToString());
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00010B88 File Offset: 0x0000ED88
		protected override bool CriteriaEquivalent(SearchCriteria other)
		{
			SearchCriteriaText searchCriteriaText = other as SearchCriteriaText;
			return searchCriteriaText != null && this.lhs == searchCriteriaText.lhs && this.fullnessFlags == searchCriteriaText.fullnessFlags && this.fuzzynessFlags == searchCriteriaText.fuzzynessFlags && this.rhs == searchCriteriaText.rhs;
		}

		// Token: 0x040000E0 RID: 224
		private readonly Column lhs;

		// Token: 0x040000E1 RID: 225
		private readonly SearchCriteriaText.SearchTextFullness fullnessFlags;

		// Token: 0x040000E2 RID: 226
		private readonly SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags;

		// Token: 0x040000E3 RID: 227
		private readonly Column rhs;

		// Token: 0x0200003F RID: 63
		public enum SearchTextFuzzyLevel : ushort
		{
			// Token: 0x040000E5 RID: 229
			IgnoreCase = 1,
			// Token: 0x040000E6 RID: 230
			IgnoreNonSpace,
			// Token: 0x040000E7 RID: 231
			Loose = 4
		}

		// Token: 0x02000040 RID: 64
		[Flags]
		public enum SearchTextFullness : ushort
		{
			// Token: 0x040000E9 RID: 233
			FullString = 0,
			// Token: 0x040000EA RID: 234
			SubString = 1,
			// Token: 0x040000EB RID: 235
			Prefix = 2,
			// Token: 0x040000EC RID: 236
			PrefixOnAnyWord = 16,
			// Token: 0x040000ED RID: 237
			PhraseMatch = 32
		}
	}
}
