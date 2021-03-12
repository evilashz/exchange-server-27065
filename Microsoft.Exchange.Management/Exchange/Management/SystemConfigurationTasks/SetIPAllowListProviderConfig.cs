using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A79 RID: 2681
	[Cmdlet("Set", "IPAllowListProvidersConfig", SupportsShouldProcess = true)]
	public sealed class SetIPAllowListProviderConfig : SetSingletonSystemConfigurationObjectTask<IPAllowListProviderConfig>
	{
		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x06005F72 RID: 24434 RVA: 0x0018FDA4 File Offset: 0x0018DFA4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIPAllowListProvidersConfig;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x06005F73 RID: 24435 RVA: 0x0018FDAB File Offset: 0x0018DFAB
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
