using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C14 RID: 3092
	[Cmdlet("Get", "PowerShellVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetPowerShellVirtualDirectory : GetPowerShellCommonVirtualDirectory<ADPowerShellVirtualDirectory>
	{
		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x060074A7 RID: 29863 RVA: 0x001DC160 File Offset: 0x001DA360
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.PowerShell;
			}
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x001DC164 File Offset: 0x001DA364
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADPowerShellVirtualDirectory adpowerShellVirtualDirectory = (ADPowerShellVirtualDirectory)dataObject;
			adpowerShellVirtualDirectory.RequireSSL = ExchangeServiceVDirHelper.IsSSLRequired(adpowerShellVirtualDirectory, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}
	}
}
