using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C9 RID: 457
	public class MonadConnectionInfo : RemoteConnectionInfo
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x000315B8 File Offset: 0x0002F7B8
		static MonadConnectionInfo()
		{
			if (ExchangeSetupContext.InstalledVersion != null)
			{
				MonadConnectionInfo.exchangeClientVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					ExchangeSetupContext.InstalledVersion.Major,
					ExchangeSetupContext.InstalledVersion.Minor,
					ExchangeSetupContext.InstalledVersion.Build,
					ExchangeSetupContext.InstalledVersion.Revision
				});
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00031631 File Offset: 0x0002F831
		public MonadConnectionInfo(Uri server, PSCredential credentials) : this(server, credentials, "http://schemas.microsoft.com/powershell/Microsoft.Exchange")
		{
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00031640 File Offset: 0x0002F840
		public MonadConnectionInfo(Uri server, PSCredential credentials, string shellUri) : this(server, credentials, shellUri, null, AuthenticationMechanism.Kerberos)
		{
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0003164D File Offset: 0x0002F84D
		public MonadConnectionInfo(Uri server, PSCredential credentials, string shellUri, string typesFile, AuthenticationMechanism authenticationMechanism) : this(server, credentials, shellUri, typesFile, authenticationMechanism, ExchangeRunspaceConfigurationSettings.SerializationLevel.Partial)
		{
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003165D File Offset: 0x0002F85D
		public MonadConnectionInfo(Uri server, PSCredential credentials, string shellUri, string typesFile, AuthenticationMechanism authenticationMechanism, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel) : this(server, credentials, shellUri, typesFile, authenticationMechanism, serializationLevel, ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown)
		{
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00031670 File Offset: 0x0002F870
		public MonadConnectionInfo(Uri server, PSCredential credentials, string shellUri, string typesFile, AuthenticationMechanism authenticationMechanism, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication) : this(server, credentials, shellUri, typesFile, authenticationMechanism, serializationLevel, clientApplication, string.Empty)
		{
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00031694 File Offset: 0x0002F894
		public MonadConnectionInfo(Uri server, PSCredential credentials, string shellUri, string typesFile, AuthenticationMechanism authenticationMechanism, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string clientVersion) : this(server, credentials, shellUri, typesFile, authenticationMechanism, serializationLevel, clientApplication, clientVersion, 0, true)
		{
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000316B8 File Offset: 0x0002F8B8
		public MonadConnectionInfo(Uri server, PSCredential credentials, string shellUri, string typesFile, AuthenticationMechanism authenticationMechanism, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string clientVersion, int maxRedirectionCount, bool skipCertificateCheck) : base(server, credentials, shellUri, typesFile, authenticationMechanism, skipCertificateCheck, maxRedirectionCount)
		{
			Uri uri = server;
			if (serializationLevel != ExchangeRunspaceConfigurationSettings.SerializationLevel.Partial)
			{
				uri = MonadConnectionInfo.AppendUriProperty(uri, "serializationLevel".ToString(), serializationLevel.ToString());
			}
			if (clientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown)
			{
				uri = MonadConnectionInfo.AppendUriProperty(uri, "clientApplication".ToString(), clientApplication.ToString());
			}
			if (MonadConnectionInfo.exchangeClientVersion != null)
			{
				uri = MonadConnectionInfo.AppendUriProperty(uri, "ExchClientVer", MonadConnectionInfo.exchangeClientVersion);
			}
			this.serverUri = uri;
			this.clientApplication = clientApplication;
			this.serializationLevel = serializationLevel;
			this.clientVersion = clientVersion;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00031751 File Offset: 0x0002F951
		public MonadConnectionInfo(Uri server, string certificateThumbprint, string shellUri) : this(server, certificateThumbprint, shellUri, false)
		{
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003175D File Offset: 0x0002F95D
		internal MonadConnectionInfo(Uri server, string certificateThumbprint, string shellUri, bool skipCerificateChecks) : base(server, certificateThumbprint, shellUri, null, AuthenticationMechanism.Default, true, 0)
		{
			this.serverUri = server;
			if (MonadConnectionInfo.exchangeClientVersion != null)
			{
				this.serverUri = MonadConnectionInfo.AppendUriProperty(this.serverUri, "ExchClientVer", MonadConnectionInfo.exchangeClientVersion);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00031798 File Offset: 0x0002F998
		private static Uri AppendUriProperty(Uri serverUri, string propertyName, string propertyValue)
		{
			if (serverUri.Query == null || !serverUri.Query.Contains(propertyName))
			{
				StringBuilder stringBuilder = new StringBuilder(serverUri.OriginalString);
				if (serverUri.OriginalString.Contains("?"))
				{
					stringBuilder.Append(";");
				}
				else
				{
					stringBuilder.Append("?");
				}
				stringBuilder.Append(propertyName);
				stringBuilder.Append("=");
				stringBuilder.Append(propertyValue);
				serverUri = new Uri(stringBuilder.ToString());
			}
			return serverUri;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003181D File Offset: 0x0002FA1D
		public override Uri ServerUri
		{
			get
			{
				return this.serverUri;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00031825 File Offset: 0x0002FA25
		public ExchangeRunspaceConfigurationSettings.SerializationLevel SerializationLevel
		{
			get
			{
				return this.serializationLevel;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003182D File Offset: 0x0002FA2D
		public ExchangeRunspaceConfigurationSettings.ExchangeApplication ClientApplication
		{
			get
			{
				return this.clientApplication;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00031835 File Offset: 0x0002FA35
		public string ClientVersion
		{
			get
			{
				return this.clientVersion;
			}
		}

		// Token: 0x04000377 RID: 887
		public const string DefaultShellUri = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";

		// Token: 0x04000378 RID: 888
		private ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel;

		// Token: 0x04000379 RID: 889
		private ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication;

		// Token: 0x0400037A RID: 890
		private Uri serverUri;

		// Token: 0x0400037B RID: 891
		private string clientVersion;

		// Token: 0x0400037C RID: 892
		private static string exchangeClientVersion;
	}
}
