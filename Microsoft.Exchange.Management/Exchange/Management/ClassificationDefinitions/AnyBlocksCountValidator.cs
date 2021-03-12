using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000862 RID: 2146
	internal sealed class AnyBlocksCountValidator : IDataClassificationComplexityValidator
	{
		// Token: 0x06004A3F RID: 19007 RVA: 0x0013178A File Offset: 0x0012F98A
		public void Initialize(DataClassificationConfig dcDataClassificationValidationConfig)
		{
			if (dcDataClassificationValidationConfig == null)
			{
				throw new ArgumentNullException("dcDataClassificationValidationConfig");
			}
			this.anyBlocksCountLimit = dcDataClassificationValidationConfig.MaxAnyBlocks;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x001317A6 File Offset: 0x0012F9A6
		public bool IsRuleComplexityLimitExceeded(RuleComplexityData ruleComplexityData)
		{
			if (ruleComplexityData == null)
			{
				throw new ArgumentNullException("ruleComplexityData");
			}
			return ruleComplexityData.AnyBlocksCount > this.anyBlocksCountLimit;
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x001317C4 File Offset: 0x0012F9C4
		public LocalizedString CreateExceptionMessage(IList<string> offendingRulesList)
		{
			return Strings.ClassificationRuleCollectionAnyBlocksExceedLimit(this.anyBlocksCountLimit, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, offendingRulesList));
		}

		// Token: 0x04002CAA RID: 11434
		private int anyBlocksCountLimit;
	}
}
