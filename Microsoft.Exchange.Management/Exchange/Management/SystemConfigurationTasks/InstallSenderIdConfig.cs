using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA2 RID: 2722
	[Cmdlet("Install", "SenderIdConfig")]
	public sealed class InstallSenderIdConfig : InstallAntispamConfig<SenderIdConfig>
	{
		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06006053 RID: 24659 RVA: 0x00191646 File Offset: 0x0018F846
		protected override string CanonicalName
		{
			get
			{
				return "SenderIdConfig";
			}
		}
	}
}
