using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C0D RID: 3085
	[Cmdlet("Get", "HostedEncryptionVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetHostedEncryptionVirtualDirectory : GetExchangeVirtualDirectory<ADE4eVirtualDirectory>
	{
		// Token: 0x06007491 RID: 29841 RVA: 0x001DBE58 File Offset: 0x001DA058
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADE4eVirtualDirectory ade4eVirtualDirectory = (ADE4eVirtualDirectory)dataObject;
			ade4eVirtualDirectory.LoadSettings();
			WebAppVirtualDirectoryHelper.UpdateFromMetabase(ade4eVirtualDirectory);
			TaskLogger.LogExit();
		}

		// Token: 0x06007492 RID: 29842 RVA: 0x001DBE89 File Offset: 0x001DA089
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}
	}
}
