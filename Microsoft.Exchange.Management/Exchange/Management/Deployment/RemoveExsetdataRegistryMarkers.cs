using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000220 RID: 544
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Remove", "ExsetdataRegistryMarkers")]
	public sealed class RemoveExsetdataRegistryMarkers : ManageExsetdataRegistryMarkers
	{
		// Token: 0x06001283 RID: 4739 RVA: 0x0005165D File Offset: 0x0004F85D
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.DeleteRegistryMarkers();
			TaskLogger.LogExit();
		}
	}
}
