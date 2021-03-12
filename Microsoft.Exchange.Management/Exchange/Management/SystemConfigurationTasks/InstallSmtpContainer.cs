using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B31 RID: 2865
	[Cmdlet("Install", "SmtpContainer")]
	public sealed class InstallSmtpContainer : InstallContainerTaskBase<SmtpContainer>
	{
		// Token: 0x060066F9 RID: 26361 RVA: 0x001A9721 File Offset: 0x001A7921
		protected override ADObjectId GetBaseContainer()
		{
			return (base.DataSession as ITopologyConfigurationSession).FindLocalServer().Id;
		}
	}
}
