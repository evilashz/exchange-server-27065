using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B46 RID: 2886
	[Cmdlet("Remove", "InterceptorRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveInterceptorRule : RemoveSystemConfigurationObjectTask<InterceptorRuleIdParameter, InterceptorRule>
	{
		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x060068B3 RID: 26803 RVA: 0x001AF74B File Offset: 0x001AD94B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveInterceptorRule(this.Identity.ToString());
			}
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x060068B4 RID: 26804 RVA: 0x001AF75D File Offset: 0x001AD95D
		protected override ObjectId RootId
		{
			get
			{
				return base.RootOrgContainerId.GetDescendantId(InterceptorRule.InterceptorRulesContainer);
			}
		}
	}
}
