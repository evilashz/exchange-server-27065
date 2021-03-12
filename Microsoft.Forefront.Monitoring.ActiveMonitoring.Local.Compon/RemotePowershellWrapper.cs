using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Migration;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001FC RID: 508
	internal class RemotePowershellWrapper : IDisposable
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x00028A68 File Offset: 0x00026C68
		internal RemotePowershellWrapper(string endPoint, string username, SecureString password, string delegatedTenant = null)
		{
			this.tenantName = username.Split(new char[]
			{
				'@'
			})[1];
			this.remoteConfig = new RemotePowershellDataConfig
			{
				ManagementEndpointUri = endPoint,
				UseCertificateAuth = false,
				BasicAuthUserName = username,
				BasicAuthPassword = password,
				SkipCertificateChecks = true
			};
			this.loggerInstance = new CmdletAuditLog("RemotePowershellHelper", this.remoteConfig);
			this.provider = this.GetDataProvider();
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00028AE8 File Offset: 0x00026CE8
		public IEnumerable<PSObject> Execute(PSCommand psCommand)
		{
			return this.provider.Execute(psCommand, null);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00028B0C File Offset: 0x00026D0C
		public IEnumerable<PSObject> Execute(string command)
		{
			return this.provider.Execute(command, null);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00028B2E File Offset: 0x00026D2E
		public void Dispose()
		{
			if (this.provider != null)
			{
				this.provider.Dispose();
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00028B43 File Offset: 0x00026D43
		private IRemotePowershellDataProvider GetDataProvider()
		{
			return new RemotePowershellDataProvider("RemotePowershellHelper", new ADObjectId("DN=" + this.tenantName), this.loggerInstance, this.remoteConfig);
		}

		// Token: 0x04000764 RID: 1892
		private const string CallerId = "RemotePowershellHelper";

		// Token: 0x04000765 RID: 1893
		private readonly CmdletAuditLog loggerInstance;

		// Token: 0x04000766 RID: 1894
		private readonly RemotePowershellDataConfig remoteConfig;

		// Token: 0x04000767 RID: 1895
		private readonly string tenantName;

		// Token: 0x04000768 RID: 1896
		private readonly IRemotePowershellDataProvider provider;
	}
}
