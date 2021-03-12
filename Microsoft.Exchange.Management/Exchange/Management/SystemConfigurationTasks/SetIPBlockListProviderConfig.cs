using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A81 RID: 2689
	[Cmdlet("Set", "IPBlockListProvidersConfig", SupportsShouldProcess = true)]
	public sealed class SetIPBlockListProviderConfig : SetSingletonSystemConfigurationObjectTask<IPBlockListProviderConfig>
	{
		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x0018FE8C File Offset: 0x0018E08C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIPBlockListProvidersConfig;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x0018FE93 File Offset: 0x0018E093
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
