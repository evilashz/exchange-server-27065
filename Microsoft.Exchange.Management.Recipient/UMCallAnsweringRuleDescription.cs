using System;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public class UMCallAnsweringRuleDescription : RuleDescription
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00045442 File Offset: 0x00043642
		internal override string RuleDescriptionIf
		{
			get
			{
				return Strings.UMCallAnsweringRuleDescriptionIf;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004544E File Offset: 0x0004364E
		internal override string RuleDescriptionTakeActions
		{
			get
			{
				return Strings.UMCallAnsweringRuleDescriptionTakeActions;
			}
		}
	}
}
