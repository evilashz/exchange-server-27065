using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C52 RID: 3154
	[Cmdlet("Update", "ActiveSyncVirtualDirectory")]
	public sealed class UpdateActiveSyncVirtualDirectory : SetMobileSyncVirtualDirectoryBase
	{
		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x060077CA RID: 30666 RVA: 0x001E8240 File Offset: 0x001E6440
		protected override bool IsInSetup
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060077CB RID: 30667 RVA: 0x001E8244 File Offset: 0x001E6444
		protected override IConfigurable PrepareDataObject()
		{
			ADMobileVirtualDirectory admobileVirtualDirectory = (ADMobileVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (admobileVirtualDirectory.ExchangeVersion.IsOlderThan(admobileVirtualDirectory.MaximumSupportedExchangeObjectVersion))
			{
				admobileVirtualDirectory.SetExchangeVersion(admobileVirtualDirectory.MaximumSupportedExchangeObjectVersion);
			}
			return admobileVirtualDirectory;
		}

		// Token: 0x060077CC RID: 30668 RVA: 0x001E8288 File Offset: 0x001E6488
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			try
			{
				MobileSyncVirtualDirectoryHelper.InstallProxySubDirectory(this.DataObject, this);
			}
			catch (Exception ex)
			{
				TaskLogger.Trace("Exception occurred in InstallProxySubDirectory(): {0}", new object[]
				{
					ex.Message
				});
				this.WriteWarning(Strings.ActiveSyncMetabaseProxyInstallFailure);
				throw;
			}
			TaskLogger.LogExit();
		}
	}
}
