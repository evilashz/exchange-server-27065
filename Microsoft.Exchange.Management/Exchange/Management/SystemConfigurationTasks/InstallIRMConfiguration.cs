using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200070B RID: 1803
	[Cmdlet("Install", "IRMConfiguration")]
	public sealed class InstallIRMConfiguration : InstallContainerTaskBase<IRMConfiguration>
	{
		// Token: 0x06004012 RID: 16402 RVA: 0x00105EEE File Offset: 0x001040EE
		protected override IConfigurable PrepareDataObject()
		{
			return IRMConfiguration.Read((IConfigurationSession)base.DataSession);
		}
	}
}
