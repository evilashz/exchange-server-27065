using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Hybrid;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000033 RID: 51
	internal class TenantHybridDetectionCmdlet : ITenantHybridDetectionCmdlet
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00004E2C File Offset: 0x0000302C
		public void Connect(PSCredential psCredential, string targetServer, ILogger logger)
		{
			string targetServer2 = "ps.outlook.com";
			if (!string.IsNullOrEmpty(targetServer))
			{
				targetServer2 = targetServer;
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Update-HybridConfiguration"))
			{
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("TenantPowerShellEndpoint");
					if (!string.IsNullOrEmpty(text))
					{
						targetServer2 = text;
					}
				}
			}
			this.remotePowershellSession = new RemotePowershellSession(targetServer2, PowershellConnectionType.Tenant, true, logger);
			this.remotePowershellSession.Connect(psCredential, CultureInfo.CurrentCulture);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004EB4 File Offset: 0x000030B4
		public IEnumerable<OrganizationConfig> GetOrganizationConfig()
		{
			return this.remotePowershellSession.RunOneCommand<OrganizationConfig>("Get-OrganizationConfig", null, false);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004EC8 File Offset: 0x000030C8
		public void Dispose()
		{
			if (this.remotePowershellSession != null)
			{
				this.remotePowershellSession.Dispose();
				this.remotePowershellSession = null;
			}
		}

		// Token: 0x040000B5 RID: 181
		private const string TenantPSEndPoint = "ps.outlook.com";

		// Token: 0x040000B6 RID: 182
		private const string GetOrganizationConfigCmdlet = "Get-OrganizationConfig";

		// Token: 0x040000B7 RID: 183
		private const string RunHybridRegkeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Update-HybridConfiguration";

		// Token: 0x040000B8 RID: 184
		private const string TenantPSEndPointOverride = "TenantPowerShellEndpoint";

		// Token: 0x040000B9 RID: 185
		private RemotePowershellSession remotePowershellSession;
	}
}
