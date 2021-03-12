using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C0F RID: 3087
	[Cmdlet("Get", "ActiveSyncVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetMobileSyncVirtualDirectory : GetExchangeVirtualDirectory<ADMobileVirtualDirectory>
	{
		// Token: 0x06007496 RID: 29846 RVA: 0x001DBEA0 File Offset: 0x001DA0A0
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			ADMobileVirtualDirectory vdirObject = (ADMobileVirtualDirectory)dataObject;
			try
			{
				MobileSyncVirtualDirectoryHelper.ReadFromMetabase(vdirObject, this);
			}
			catch (IISNotInstalledException exception)
			{
				this.WriteError(exception, ErrorCategory.ObjectNotFound, null, false);
			}
			catch (UnauthorizedAccessException exception2)
			{
				this.WriteError(exception2, ErrorCategory.PermissionDenied, null, false);
			}
		}

		// Token: 0x06007497 RID: 29847 RVA: 0x001DBEF8 File Offset: 0x001DA0F8
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}
	}
}
