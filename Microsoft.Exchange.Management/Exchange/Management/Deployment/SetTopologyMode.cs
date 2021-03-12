using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000257 RID: 599
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Set", "TopologyMode")]
	public sealed class SetTopologyMode : Task
	{
		// Token: 0x0600167C RID: 5756 RVA: 0x0005DD4F File Offset: 0x0005BF4F
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADSession.SetAdminTopologyMode();
			if (ExchangePropertyContainer.IsContainerInitialized(base.SessionState))
			{
				ExchangePropertyContainer.SetServerSettings(base.SessionState, null);
			}
			base.SessionState.Variables[ExchangePropertyContainer.ADServerSettingsVarName] = null;
			TaskLogger.LogExit();
		}
	}
}
