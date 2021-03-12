using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A95 RID: 2709
	[Cmdlet("Set", "RecipientFilterConfig", SupportsShouldProcess = true)]
	public sealed class SetRecipientFilterConfig : SetSingletonSystemConfigurationObjectTask<RecipientFilterConfig>
	{
		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x001914ED File Offset: 0x0018F6ED
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRecipientFilterConfig;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06006034 RID: 24628 RVA: 0x001914F4 File Offset: 0x0018F6F4
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
