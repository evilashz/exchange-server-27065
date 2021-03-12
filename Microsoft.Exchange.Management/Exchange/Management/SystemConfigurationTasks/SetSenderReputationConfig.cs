using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200075F RID: 1887
	[Cmdlet("Set", "SenderReputationConfig", SupportsShouldProcess = true)]
	public sealed class SetSenderReputationConfig : SetSingletonSystemConfigurationObjectTask<SenderReputationConfig>
	{
		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x00113C20 File Offset: 0x00111E20
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSenderReputationConfig;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x00113C27 File Offset: 0x00111E27
		protected override ObjectId RootId
		{
			get
			{
				return (base.DataSession as IConfigurationSession).GetOrgContainerId();
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x0600432A RID: 17194 RVA: 0x00113C39 File Offset: 0x00111E39
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
