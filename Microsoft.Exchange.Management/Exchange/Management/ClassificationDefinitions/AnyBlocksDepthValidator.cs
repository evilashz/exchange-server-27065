using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000863 RID: 2147
	internal sealed class AnyBlocksDepthValidator : IDataClassificationComplexityValidator
	{
		// Token: 0x06004A43 RID: 19011 RVA: 0x001317F6 File Offset: 0x0012F9F6
		public void Initialize(DataClassificationConfig dcDataClassificationValidationConfig)
		{
			if (dcDataClassificationValidationConfig == null)
			{
				throw new ArgumentNullException("dcDataClassificationValidationConfig");
			}
			this.nestedAnyBlocksDepthLimit = dcDataClassificationValidationConfig.MaxNestedAnyBlocks;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00131812 File Offset: 0x0012FA12
		public bool IsRuleComplexityLimitExceeded(RuleComplexityData ruleComplexityData)
		{
			if (ruleComplexityData == null)
			{
				throw new ArgumentNullException("ruleComplexityData");
			}
			return ruleComplexityData.MaxAnyBlocksDepth > this.nestedAnyBlocksDepthLimit;
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00131830 File Offset: 0x0012FA30
		public LocalizedString CreateExceptionMessage(IList<string> offendingRulesList)
		{
			return Strings.ClassificationRuleCollectionNestedAnyDepthExceedLimit(this.nestedAnyBlocksDepthLimit, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, offendingRulesList));
		}

		// Token: 0x04002CAB RID: 11435
		private int nestedAnyBlocksDepthLimit;
	}
}
