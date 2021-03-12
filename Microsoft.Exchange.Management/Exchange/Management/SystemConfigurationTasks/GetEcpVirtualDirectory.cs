using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C0C RID: 3084
	[Cmdlet("Get", "EcpVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetEcpVirtualDirectory : GetExchangeVirtualDirectory<ADEcpVirtualDirectory>
	{
		// Token: 0x0600748D RID: 29837 RVA: 0x001DBE10 File Offset: 0x001DA010
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADEcpVirtualDirectory webAppVirtualDirectory = (ADEcpVirtualDirectory)dataObject;
			WebAppVirtualDirectoryHelper.UpdateFromMetabase(webAppVirtualDirectory);
			TaskLogger.LogExit();
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x001DBE3B File Offset: 0x001DA03B
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x001DBE3E File Offset: 0x001DA03E
		protected override LocalizedString GetMissingMetabaseEntryWarning(ExchangeVirtualDirectory vdir)
		{
			return Strings.EcpAdOrphanFound(vdir.Id.Name);
		}
	}
}
