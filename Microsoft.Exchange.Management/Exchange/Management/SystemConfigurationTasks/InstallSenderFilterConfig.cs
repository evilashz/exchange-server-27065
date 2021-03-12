using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA1 RID: 2721
	[Cmdlet("Install", "SenderFilterConfig")]
	public sealed class InstallSenderFilterConfig : InstallAntispamConfig<SenderFilterConfig>
	{
		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06006051 RID: 24657 RVA: 0x00191637 File Offset: 0x0018F837
		protected override string CanonicalName
		{
			get
			{
				return "SenderFilterConfig";
			}
		}
	}
}
