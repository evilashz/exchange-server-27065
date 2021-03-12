using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders
{
	// Token: 0x020001B9 RID: 441
	public abstract class RemotePowerShellResponder : ResponderWorkItem
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00051649 File Offset: 0x0004F849
		protected RemotePowerShell RemotePowerShell
		{
			get
			{
				if (this.remotePowershell == null)
				{
					this.CreateRunspace();
				}
				return this.remotePowershell;
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0005165F File Offset: 0x0004F85F
		public RemotePowerShellResponder()
		{
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00051668 File Offset: 0x0004F868
		protected void CreateRunspace()
		{
			if (base.Definition.Account == null)
			{
				RemotePowerShellResponder.SetActiveMonitoringCertificateSettings(base.Definition);
				base.Result.StateAttribute5 = "No authentication values were defined in RemoteServerRestartResponder. Certification settings have now been set.";
			}
			if (!string.IsNullOrWhiteSpace(base.Definition.AccountPassword))
			{
				this.remotePowershell = RemotePowerShell.CreateRemotePowerShellByCredential(new Uri(base.Definition.Endpoint), base.Definition.Account, base.Definition.AccountPassword, true);
				return;
			}
			if (base.Definition.Endpoint.Contains(";"))
			{
				this.remotePowershell = RemotePowerShell.CreateRemotePowerShellByCertificate(base.Definition.Endpoint.Split(new char[]
				{
					';'
				}), base.Definition.Account, true);
				return;
			}
			this.remotePowershell = RemotePowerShell.CreateRemotePowerShellByCertificate(new Uri(base.Definition.Endpoint), base.Definition.Account, true);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00051758 File Offset: 0x0004F958
		internal static void SetActiveMonitoringCertificateSettings(ResponderDefinition definition)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RemotePowerShellResponder.ActiveMonitoringRegistryPath, false))
			{
				if (registryKey != null)
				{
					string text;
					if (definition.Account == null && (text = (string)registryKey.GetValue("RPSCertificateSubject", null)) != null)
					{
						definition.Account = text;
					}
					if (definition.Endpoint == null && (text = (string)registryKey.GetValue("RPSEndpoint", null)) != null)
					{
						definition.Endpoint = text;
					}
				}
			}
		}

		// Token: 0x04000940 RID: 2368
		private static readonly string ActiveMonitoringRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";

		// Token: 0x04000941 RID: 2369
		private RemotePowerShell remotePowershell;
	}
}
