using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000827 RID: 2087
	[Serializable]
	internal abstract class ClassificationIdentityMatcher<TObjectWithIdentity> where TObjectWithIdentity : class
	{
		// Token: 0x0600482F RID: 18479 RVA: 0x00128B90 File Offset: 0x00126D90
		protected ClassificationIdentityMatcher(string searchName, string rawSearchName, MatchOptions matchOptions)
		{
			this.searchName = searchName;
			this.rawSearchName = rawSearchName;
			this.areSearchNameAndRawSearchNameEqual = string.Equals(this.searchName, this.rawSearchName, StringComparison.Ordinal);
			if (ClassificationIdentityMatcher<TObjectWithIdentity>.matchOperatorsTable.Value.ContainsKey(matchOptions))
			{
				this.matchOperator = ClassificationIdentityMatcher<TObjectWithIdentity>.matchOperatorsTable.Value[matchOptions];
				return;
			}
			ExAssert.RetailAssert(false, "Invalid MatchOptions value specified for ClassificationIdentityMatcher c'tor");
		}

		// Token: 0x06004830 RID: 18480
		protected abstract bool EvaluateMatch(TObjectWithIdentity objectWithIdentityToMatch, Func<string, CultureInfo, CompareOptions, bool> matchEvaluator);

		// Token: 0x06004831 RID: 18481 RVA: 0x00128C00 File Offset: 0x00126E00
		protected static bool SubStringMatch(string objectPropertyValue, string searchTerm, CultureInfo cultureInfo, CompareOptions compareOption)
		{
			return cultureInfo.CompareInfo.IndexOf(objectPropertyValue, searchTerm, compareOption) != -1;
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00128C24 File Offset: 0x00126E24
		protected static bool PrefixMatch(string objectPropertyValue, string searchTerm, CultureInfo cultureInfo, CompareOptions compareOption)
		{
			return cultureInfo.CompareInfo.IsPrefix(objectPropertyValue, searchTerm, compareOption);
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00128C44 File Offset: 0x00126E44
		protected static bool SuffixMatch(string objectPropertyValue, string searchTerm, CultureInfo cultureInfo, CompareOptions compareOption)
		{
			return cultureInfo.CompareInfo.IsSuffix(objectPropertyValue, searchTerm, compareOption);
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x00128C64 File Offset: 0x00126E64
		protected static bool ExactMatch(string objectPropertyValue, string searchTerm, CultureInfo cultureInfo, CompareOptions compareOption)
		{
			return 0 == cultureInfo.CompareInfo.Compare(objectPropertyValue, searchTerm, compareOption);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x00128C84 File Offset: 0x00126E84
		protected virtual bool MatchObjectPropertyValue(string objectPropertyValue, CultureInfo cultureInfo, CompareOptions compareOption)
		{
			bool flag = this.matchOperator(objectPropertyValue, this.searchName, cultureInfo, compareOption);
			if (!flag && this.matchOperator != new Func<string, string, CultureInfo, CompareOptions, bool>(ClassificationIdentityMatcher<TObjectWithIdentity>.ExactMatch) && !string.IsNullOrEmpty(this.rawSearchName) && !this.areSearchNameAndRawSearchNameEqual)
			{
				flag = ClassificationIdentityMatcher<TObjectWithIdentity>.ExactMatch(objectPropertyValue, this.rawSearchName, cultureInfo, compareOption);
			}
			return flag;
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x00128CE8 File Offset: 0x00126EE8
		internal bool Match(TObjectWithIdentity objectWithIdentityToMatch)
		{
			return this.EvaluateMatch(objectWithIdentityToMatch, new Func<string, CultureInfo, CompareOptions, bool>(this.MatchObjectPropertyValue));
		}

		// Token: 0x04002BEE RID: 11246
		private static readonly Lazy<Dictionary<MatchOptions, Func<string, string, CultureInfo, CompareOptions, bool>>> matchOperatorsTable = new Lazy<Dictionary<MatchOptions, Func<string, string, CultureInfo, CompareOptions, bool>>>(() => new Dictionary<MatchOptions, Func<string, string, CultureInfo, CompareOptions, bool>>
		{
			{
				MatchOptions.FullString,
				new Func<string, string, CultureInfo, CompareOptions, bool>(ClassificationIdentityMatcher<TObjectWithIdentity>.ExactMatch)
			},
			{
				MatchOptions.Prefix,
				new Func<string, string, CultureInfo, CompareOptions, bool>(ClassificationIdentityMatcher<TObjectWithIdentity>.PrefixMatch)
			},
			{
				MatchOptions.Suffix,
				new Func<string, string, CultureInfo, CompareOptions, bool>(ClassificationIdentityMatcher<TObjectWithIdentity>.SuffixMatch)
			},
			{
				MatchOptions.SubString,
				new Func<string, string, CultureInfo, CompareOptions, bool>(ClassificationIdentityMatcher<TObjectWithIdentity>.SubStringMatch)
			}
		}, LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x04002BEF RID: 11247
		private readonly string searchName;

		// Token: 0x04002BF0 RID: 11248
		private readonly string rawSearchName;

		// Token: 0x04002BF1 RID: 11249
		private readonly bool areSearchNameAndRawSearchNameEqual;

		// Token: 0x04002BF2 RID: 11250
		protected readonly Func<string, string, CultureInfo, CompareOptions, bool> matchOperator;
	}
}
