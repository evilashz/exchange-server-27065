using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x02000090 RID: 144
	internal class ExchangeRemoteMoveEndpoint : MigrationEndpointBase
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x000254FB File Offset: 0x000236FB
		public ExchangeRemoteMoveEndpoint(MigrationEndpoint presentationObject) : base(presentationObject)
		{
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00025504 File Offset: 0x00023704
		public ExchangeRemoteMoveEndpoint() : base(MigrationType.ExchangeRemoteMove)
		{
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002550E File Offset: 0x0002370E
		public override ConnectionSettingsBase ConnectionSettings
		{
			get
			{
				return ExchangeConnectionSettings.Create(base.Username, base.Domain, base.EncryptedPassword, this.RemoteServer.ToString(), this.RemoteServer.ToString(), base.AuthenticationMethod, true, true);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00025545 File Offset: 0x00023745
		public override MigrationType PreferredMigrationType
		{
			get
			{
				return MigrationType.ExchangeRemoteMove;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002554C File Offset: 0x0002374C
		public override void VerifyConnectivity()
		{
			LocalizedException innerException;
			if (!MigrationServiceFactory.Instance.GetMigrationMrsClient().CanConnectToMrsProxy(this.RemoteServer, Guid.Empty, base.NetworkCredentials, out innerException))
			{
				throw new MigrationServerConnectionFailedException(this.RemoteServer, innerException);
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002558F File Offset: 0x0002378F
		protected override void ApplyAutodiscoverSettings(AutodiscoverClientResponse response)
		{
			this.RemoteServer = new Fqdn(response.RPCProxyServer);
		}
	}
}
