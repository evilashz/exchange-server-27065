using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200085E RID: 2142
	internal interface IDataClassificationComplexityValidator
	{
		// Token: 0x06004A30 RID: 18992
		void Initialize(DataClassificationConfig dcDataClassificationValidationConfig);

		// Token: 0x06004A31 RID: 18993
		bool IsRuleComplexityLimitExceeded(RuleComplexityData ruleComplexityData);

		// Token: 0x06004A32 RID: 18994
		LocalizedString CreateExceptionMessage(IList<string> offendingRulesList);
	}
}
