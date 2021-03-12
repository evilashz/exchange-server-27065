using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000280 RID: 640
	[Cmdlet("Update", "LegacyGwart", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class UpdateLegacyGwart : SetupTaskBase
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x00063764 File Offset: 0x00061964
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			((ITopologyConfigurationSession)this.configurationSession).UpdateGwartLastModified();
			TaskLogger.LogExit();
		}
	}
}
