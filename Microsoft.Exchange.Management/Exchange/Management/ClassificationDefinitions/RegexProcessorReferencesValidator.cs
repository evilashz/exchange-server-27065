using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200085F RID: 2143
	internal sealed class RegexProcessorReferencesValidator : IDataClassificationComplexityValidator
	{
		// Token: 0x06004A33 RID: 18995 RVA: 0x00131644 File Offset: 0x0012F844
		public void Initialize(DataClassificationConfig dcDataClassificationValidationConfig)
		{
			if (dcDataClassificationValidationConfig == null)
			{
				throw new ArgumentNullException("dcDataClassificationValidationConfig");
			}
			this.distinctRegexProcessorReferencesCountLimit = dcDataClassificationValidationConfig.DistinctRegExes;
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x00131660 File Offset: 0x0012F860
		public bool IsRuleComplexityLimitExceeded(RuleComplexityData ruleComplexityData)
		{
			if (ruleComplexityData == null)
			{
				throw new ArgumentNullException("ruleComplexityData");
			}
			return ruleComplexityData.DistinctRegexProcessorReferencesCount > this.distinctRegexProcessorReferencesCountLimit;
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x00131680 File Offset: 0x0012F880
		public LocalizedString CreateExceptionMessage(IList<string> offendingRulesList)
		{
			return Strings.ClassificationRuleCollectionDistinctRegexesExceedLimit(this.distinctRegexProcessorReferencesCountLimit, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, offendingRulesList));
		}

		// Token: 0x04002CA7 RID: 11431
		private int distinctRegexProcessorReferencesCountLimit;
	}
}
