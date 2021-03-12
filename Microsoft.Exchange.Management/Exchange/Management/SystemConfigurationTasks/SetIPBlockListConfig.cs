using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A7B RID: 2683
	[Cmdlet("Set", "IPBlockListConfig", SupportsShouldProcess = true)]
	public sealed class SetIPBlockListConfig : SetSingletonSystemConfigurationObjectTask<IPBlockListConfig>
	{
		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x06005F77 RID: 24439 RVA: 0x0018FDC1 File Offset: 0x0018DFC1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIPBlockListConfig;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x0018FDC8 File Offset: 0x0018DFC8
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
