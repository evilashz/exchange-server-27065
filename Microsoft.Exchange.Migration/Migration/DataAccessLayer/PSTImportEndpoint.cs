using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x02000092 RID: 146
	internal class PSTImportEndpoint : MigrationEndpointBase
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0002589D File Offset: 0x00023A9D
		public PSTImportEndpoint(MigrationEndpoint presentationObject) : base(presentationObject)
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000258A6 File Offset: 0x00023AA6
		public PSTImportEndpoint() : base(MigrationType.PSTImport)
		{
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x000258B0 File Offset: 0x00023AB0
		public override ConnectionSettingsBase ConnectionSettings
		{
			get
			{
				return ExchangeConnectionSettings.Create(base.Username, base.Domain, base.EncryptedPassword, (this.RemoteServer != null) ? this.RemoteServer.ToString() : null, (this.RemoteServer != null) ? this.RemoteServer.ToString() : null, base.AuthenticationMethod, true, true);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00025908 File Offset: 0x00023B08
		public override MigrationType PreferredMigrationType
		{
			get
			{
				return MigrationType.PSTImport;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002590C File Offset: 0x00023B0C
		public override void VerifyConnectivity()
		{
			LocalizedException innerException;
			if (!MigrationServiceFactory.Instance.GetMigrationMrsClient().CanConnectToMrsProxy(this.RemoteServer, Guid.Empty, base.NetworkCredentials, out innerException))
			{
				throw new MigrationServerConnectionFailedException(this.RemoteServer, innerException);
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002594F File Offset: 0x00023B4F
		protected override void ApplyAutodiscoverSettings(AutodiscoverClientResponse response)
		{
			this.RemoteServer = new Fqdn(response.RPCProxyServer);
		}
	}
}
