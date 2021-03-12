using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000248 RID: 584
	[Cmdlet("Set", "ExsetdataRegistryMarkers")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class SetExsetdataRegistryMarkers : ManageExsetdataRegistryMarkers
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x0005B924 File Offset: 0x00059B24
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.WriteRegistryMarkers();
			TaskLogger.LogExit();
		}
	}
}
