using System;
using System.Runtime.InteropServices;
using Microsoft.Mce.Interop.Api;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000856 RID: 2134
	internal sealed class RulePackageLoader : IRulePackageLoader
	{
		// Token: 0x06004A03 RID: 18947 RVA: 0x00130878 File Offset: 0x0012EA78
		internal void SetRulePackage(string packageKey, string rulePackage)
		{
			this.packageKey = packageKey;
			this.rulePackage = rulePackage;
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x00130888 File Offset: 0x0012EA88
		public void GetRulePackages(uint rulePackageRequestDetailsSize, RULE_PACKAGE_REQUEST_DETAILS[] rulePackageRequestDetails)
		{
			if (rulePackageRequestDetailsSize != 1U || rulePackageRequestDetails[0].RulePackageID != this.packageKey)
			{
				throw new COMException("Unable to find the rule package", -2147220985);
			}
			rulePackageRequestDetails[0].RulePackage = this.rulePackage;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x001308D4 File Offset: 0x0012EAD4
		public void GetUpdatedRulePackageInfo(uint rulePackageTimestampDetailsSize, RULE_PACKAGE_TIMESTAMP_DETAILS[] rulePackageTimestampDetails)
		{
			throw new COMException("Rule package updating not supported", -2147467263);
		}

		// Token: 0x04002C95 RID: 11413
		private string packageKey;

		// Token: 0x04002C96 RID: 11414
		private string rulePackage;
	}
}
