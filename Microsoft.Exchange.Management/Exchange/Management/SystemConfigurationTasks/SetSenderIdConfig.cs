using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A99 RID: 2713
	[Cmdlet("Set", "SenderIdConfig", SupportsShouldProcess = true)]
	public sealed class SetSenderIdConfig : SetSingletonSystemConfigurationObjectTask<SenderIdConfig>
	{
		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x00191527 File Offset: 0x0018F727
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSenderIdConfig;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x0019152E File Offset: 0x0018F72E
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
