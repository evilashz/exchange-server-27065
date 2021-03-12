using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C13 RID: 3091
	public abstract class GetPowerShellCommonVirtualDirectory<T> : GetExchangeServiceVirtualDirectory<T> where T : ADPowerShellCommonVirtualDirectory, new()
	{
		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x060074A3 RID: 29859
		protected abstract PowerShellVirtualDirectoryType AllowedVirtualDirectoryType { get; }

		// Token: 0x060074A4 RID: 29860 RVA: 0x001DC0F8 File Offset: 0x001DA2F8
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADPowerShellCommonVirtualDirectory adpowerShellCommonVirtualDirectory = (ADPowerShellCommonVirtualDirectory)dataObject;
			adpowerShellCommonVirtualDirectory.CertificateAuthentication = base.GetCertificateAuthentication(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x001DC12C File Offset: 0x001DA32C
		protected override void WriteResult(IConfigurable dataObject)
		{
			ADPowerShellCommonVirtualDirectory adpowerShellCommonVirtualDirectory = dataObject as ADPowerShellCommonVirtualDirectory;
			if (adpowerShellCommonVirtualDirectory != null && adpowerShellCommonVirtualDirectory.VirtualDirectoryType == this.AllowedVirtualDirectoryType)
			{
				base.WriteResult(dataObject);
			}
		}
	}
}
