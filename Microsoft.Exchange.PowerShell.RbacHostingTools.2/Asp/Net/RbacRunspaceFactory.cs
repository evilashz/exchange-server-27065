using System;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net
{
	// Token: 0x0200000B RID: 11
	public class RbacRunspaceFactory : RunspaceFactory
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003405 File Offset: 0x00001605
		public RbacRunspaceFactory(InitialSessionStateSectionFactory issFactory) : base(issFactory, null, true)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003410 File Offset: 0x00001610
		public RbacRunspaceFactory(InitialSessionStateSectionFactory issFactory, PSHostFactory hostFactory) : base(issFactory, hostFactory, true)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000341C File Offset: 0x0000161C
		protected override void InitializeRunspace(Runspace runspace)
		{
			base.InitializeRunspace(runspace);
			try
			{
				runspace.SessionStateProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, this.CreateRunspaceServerSettings());
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

		// Token: 0x0600004E RID: 78 RVA: 0x0000346C File Offset: 0x0000166C
		internal virtual RunspaceServerSettings CreateRunspaceServerSettings()
		{
			string runspaceServerSettingsToken = this.GetRunspaceServerSettingsToken();
			if (runspaceServerSettingsToken == null)
			{
				return RunspaceServerSettings.CreateRunspaceServerSettings(false);
			}
			return RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(runspaceServerSettingsToken.ToLowerInvariant(), false);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003496 File Offset: 0x00001696
		protected virtual string GetRunspaceServerSettingsToken()
		{
			if (RbacPrincipal.Current.ExecutingUserId == null)
			{
				return RbacPrincipal.Current.CacheKeys[0];
			}
			return RbacPrincipal.Current.ExecutingUserId.ToString();
		}
	}
}
