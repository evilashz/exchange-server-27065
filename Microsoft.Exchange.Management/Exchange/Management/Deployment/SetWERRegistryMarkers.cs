using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200025D RID: 605
	[Cmdlet("Set", "WERRegistryMarkers")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class SetWERRegistryMarkers : ManageWERRegistryMarkers
	{
		// Token: 0x060016A8 RID: 5800 RVA: 0x00060A0A File Offset: 0x0005EC0A
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.WriteRegistryMarkers();
			TaskLogger.LogExit();
		}
	}
}
