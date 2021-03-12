using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A72 RID: 2674
	[Cmdlet("Set", "IPAllowListConfig", SupportsShouldProcess = true)]
	public sealed class SetIPAllowListConfig : SetSingletonSystemConfigurationObjectTask<IPAllowListConfig>
	{
		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x0018F8F3 File Offset: 0x0018DAF3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIPAllowListConfig;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x0018F8FA File Offset: 0x0018DAFA
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
