using System;
using System.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Datacenter;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000031 RID: 49
	internal sealed class EhfTargetServerConfig : DatacenterTargetServerConfig
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000F07C File Offset: 0x0000D27C
		public EhfTargetServerConfig(EdgeSyncEhfConnector connector, Uri internetWebProxy) : base(connector.Name, EhfTargetServerConfig.GetProvisioningUrl(connector), connector.PrimaryLeaseLocation, connector.BackupLeaseLocation)
		{
			this.internetWebProxy = internetWebProxy;
			this.userName = null;
			this.password = null;
			if (connector.AuthenticationCredential != null)
			{
				this.userName = connector.AuthenticationCredential.UserName;
				this.password = connector.AuthenticationCredential.Password;
			}
			this.version = connector.Version;
			this.adminSyncEnabled = connector.AdminSyncEnabled;
			this.resellerId = EhfSynchronizationProvider.GetResellerId(connector);
			this.ehfSyncAppConfig = new EhfSyncAppConfig();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000F115 File Offset: 0x0000D315
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000F11D File Offset: 0x0000D31D
		public SecureString Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000F125 File Offset: 0x0000D325
		public int ResellerId
		{
			get
			{
				return this.resellerId;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000F12D File Offset: 0x0000D32D
		public Uri InternetWebProxy
		{
			get
			{
				return this.internetWebProxy;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000F135 File Offset: 0x0000D335
		public EhfWebServiceVersion EhfWebServiceVersion
		{
			get
			{
				return (EhfWebServiceVersion)this.version;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000F13D File Offset: 0x0000D33D
		public bool AdminSyncEnabled
		{
			get
			{
				return this.adminSyncEnabled;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000F145 File Offset: 0x0000D345
		public EhfSyncAppConfig EhfSyncAppConfig
		{
			get
			{
				return this.ehfSyncAppConfig;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F150 File Offset: 0x0000D350
		private static string GetProvisioningUrl(EdgeSyncEhfConnector connector)
		{
			Uri provisioningUrl = connector.ProvisioningUrl;
			EhfSynchronizationProvider.ValidateProvisioningUrl(provisioningUrl, connector.AuthenticationCredential, connector.DistinguishedName);
			return provisioningUrl.AbsoluteUri;
		}

		// Token: 0x040000E3 RID: 227
		private readonly string userName;

		// Token: 0x040000E4 RID: 228
		private readonly SecureString password;

		// Token: 0x040000E5 RID: 229
		private readonly int resellerId;

		// Token: 0x040000E6 RID: 230
		private readonly Uri internetWebProxy;

		// Token: 0x040000E7 RID: 231
		private readonly int version;

		// Token: 0x040000E8 RID: 232
		private readonly bool adminSyncEnabled;

		// Token: 0x040000E9 RID: 233
		private readonly EhfSyncAppConfig ehfSyncAppConfig;
	}
}
