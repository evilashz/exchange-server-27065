using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200022C RID: 556
	[Cmdlet("Remove", "WERRegistryMarkers")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class RemoveWERRegistryMarkers : ManageWERRegistryMarkers
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00052930 File Offset: 0x00050B30
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.DeleteRegistryMarkers();
			TaskLogger.LogExit();
		}
	}
}
