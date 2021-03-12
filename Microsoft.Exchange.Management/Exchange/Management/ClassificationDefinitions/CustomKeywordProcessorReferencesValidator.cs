using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000860 RID: 2144
	internal sealed class CustomKeywordProcessorReferencesValidator : IDataClassificationComplexityValidator
	{
		// Token: 0x06004A37 RID: 18999 RVA: 0x001316B2 File Offset: 0x0012F8B2
		public void Initialize(DataClassificationConfig dcDataClassificationValidationConfig)
		{
			if (dcDataClassificationValidationConfig == null)
			{
				throw new ArgumentNullException("dcDataClassificationValidationConfig");
			}
			this.distinctTermsFromCustomKeywordProcessorRefsCountLimit = dcDataClassificationValidationConfig.NumberOfKeywords;
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x001316CE File Offset: 0x0012F8CE
		public bool IsRuleComplexityLimitExceeded(RuleComplexityData ruleComplexityData)
		{
			if (ruleComplexityData == null)
			{
				throw new ArgumentNullException("ruleComplexityData");
			}
			return ruleComplexityData.TermsFromCustomKeywordProcessorReferencesCount > this.distinctTermsFromCustomKeywordProcessorRefsCountLimit;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x001316EC File Offset: 0x0012F8EC
		public LocalizedString CreateExceptionMessage(IList<string> offendingRulesList)
		{
			return Strings.ClassificationRuleCollectionCustomTermsCountExceedLimit(this.distinctTermsFromCustomKeywordProcessorRefsCountLimit, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, offendingRulesList));
		}

		// Token: 0x04002CA8 RID: 11432
		private int distinctTermsFromCustomKeywordProcessorRefsCountLimit;
	}
}
