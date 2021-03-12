using System;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200022D RID: 557
	internal class CachedValidationRuleResult
	{
		// Token: 0x060013C7 RID: 5063 RVA: 0x000460B2 File Offset: 0x000442B2
		public CachedValidationRuleResult(string ruleName, bool evaluationResult)
		{
			this.ruleName = ruleName;
			this.result = evaluationResult;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000460C8 File Offset: 0x000442C8
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000460D0 File Offset: 0x000442D0
		public bool EvaluationResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x0400053E RID: 1342
		private string ruleName;

		// Token: 0x0400053F RID: 1343
		private bool result;
	}
}
