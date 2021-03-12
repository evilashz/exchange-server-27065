using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000861 RID: 2145
	internal sealed class FunctionProcessorReferencesValidator : IDataClassificationComplexityValidator
	{
		// Token: 0x06004A3B RID: 19003 RVA: 0x0013171E File Offset: 0x0012F91E
		public void Initialize(DataClassificationConfig dcDataClassificationValidationConfig)
		{
			if (dcDataClassificationValidationConfig == null)
			{
				throw new ArgumentNullException("dcDataClassificationValidationConfig");
			}
			this.distinctFunctionProcessorReferencesCountLimit = dcDataClassificationValidationConfig.DistinctFunctions;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0013173A File Offset: 0x0012F93A
		public bool IsRuleComplexityLimitExceeded(RuleComplexityData ruleComplexityData)
		{
			if (ruleComplexityData == null)
			{
				throw new ArgumentNullException("ruleComplexityData");
			}
			return ruleComplexityData.DistinctFunctionProcessorReferencesCount > this.distinctFunctionProcessorReferencesCountLimit;
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00131758 File Offset: 0x0012F958
		public LocalizedString CreateExceptionMessage(IList<string> offendingRulesList)
		{
			return Strings.ClassificationRuleCollectionDistinctFunctionsExceedLimit(this.distinctFunctionProcessorReferencesCountLimit, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, offendingRulesList));
		}

		// Token: 0x04002CA9 RID: 11433
		private int distinctFunctionProcessorReferencesCountLimit;
	}
}
