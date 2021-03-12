using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A97 RID: 2711
	[Cmdlet("Set", "SenderFilterConfig", SupportsShouldProcess = true)]
	public sealed class SetSenderFilterConfig : SetSingletonSystemConfigurationObjectTask<SenderFilterConfig>
	{
		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06006038 RID: 24632 RVA: 0x0019150A File Offset: 0x0018F70A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSenderFilterConfig;
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06006039 RID: 24633 RVA: 0x00191511 File Offset: 0x0018F711
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
