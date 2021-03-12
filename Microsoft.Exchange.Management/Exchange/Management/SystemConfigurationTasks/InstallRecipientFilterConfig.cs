using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA0 RID: 2720
	[Cmdlet("Install", "RecipientFilterConfig")]
	public sealed class InstallRecipientFilterConfig : InstallAntispamConfig<RecipientFilterConfig>
	{
		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x0600604F RID: 24655 RVA: 0x00191628 File Offset: 0x0018F828
		protected override string CanonicalName
		{
			get
			{
				return "RecipientFilterConfig";
			}
		}
	}
}
