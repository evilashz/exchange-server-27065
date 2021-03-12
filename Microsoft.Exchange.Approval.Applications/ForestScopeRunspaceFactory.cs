using System;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Approval.Applications
{
	// Token: 0x02000005 RID: 5
	internal class ForestScopeRunspaceFactory : RunspaceFactory
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002470 File Offset: 0x00000670
		public ForestScopeRunspaceFactory(InitialSessionStateFactory issFactory, PSHostFactory hostFactory) : base(issFactory, hostFactory, true)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000247C File Offset: 0x0000067C
		protected override void InitializeRunspace(Runspace runspace)
		{
			base.InitializeRunspace(runspace);
			try
			{
				RunspaceServerSettings runspaceServerSettings = RunspaceServerSettings.CreateRunspaceServerSettings(false);
				runspaceServerSettings.ViewEntireForest = true;
				runspace.SessionStateProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, runspaceServerSettings);
			}
			catch (ADTransientException)
			{
				throw;
			}
			catch (ADExternalException)
			{
				throw;
			}
		}
	}
}
