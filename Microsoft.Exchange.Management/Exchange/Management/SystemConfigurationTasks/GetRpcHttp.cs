using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C17 RID: 3095
	[Cmdlet("Get", "OutlookAnywhere", DefaultParameterSetName = "Identity")]
	public sealed class GetRpcHttp : GetExchangeVirtualDirectory<ADRpcHttpVirtualDirectory>
	{
		// Token: 0x060074AD RID: 29869 RVA: 0x001DC1BC File Offset: 0x001DA3BC
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x001DC1BF File Offset: 0x001DA3BF
		protected override LocalizedString GetMissingMetabaseEntryWarning(ExchangeVirtualDirectory vdir)
		{
			return Strings.WarnRpcHttpAdOrphanFound(vdir.Id.Name, ADVirtualDirectory.GetServerNameFromVDirObjectId(vdir.Id));
		}
	}
}
