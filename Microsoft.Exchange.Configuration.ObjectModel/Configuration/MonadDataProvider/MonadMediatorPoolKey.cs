using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D2 RID: 466
	internal class MonadMediatorPoolKey : IEquatable<MonadMediatorPoolKey>
	{
		// Token: 0x060010AD RID: 4269 RVA: 0x0003317D File Offset: 0x0003137D
		public MonadMediatorPoolKey(MonadConnectionInfo connectionInfo, RunspaceServerSettingsPresentationObject serverSettings)
		{
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			this.connectionInfo = connectionInfo;
			this.serverSettings = serverSettings;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000331A1 File Offset: 0x000313A1
		public MonadMediatorPoolKey(MonadConnectionInfo connectionInfo) : this(connectionInfo, null)
		{
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x000331AB File Offset: 0x000313AB
		public MonadConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connectionInfo;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x000331B3 File Offset: 0x000313B3
		public RunspaceServerSettingsPresentationObject ServerSettings
		{
			get
			{
				return this.serverSettings;
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000331BC File Offset: 0x000313BC
		public bool Equals(MonadMediatorPoolKey other)
		{
			return other != null && (other.ConnectionInfo.SerializationLevel == this.ConnectionInfo.SerializationLevel && other.ConnectionInfo.ServerUri != null && other.ConnectionInfo.ServerUri.Equals(this.ConnectionInfo.ServerUri) && other.ConnectionInfo.ShellUri != null && other.ConnectionInfo.ShellUri.Equals(this.ConnectionInfo.ShellUri) && MonadMediatorPoolKey.EqualCredentials(this.ConnectionInfo, other.ConnectionInfo)) && ((this.serverSettings == null && other.serverSettings == null) || (other.ServerSettings != null && other.serverSettings.Equals(this.serverSettings)));
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00033288 File Offset: 0x00031488
		private static bool EqualCredentials(MonadConnectionInfo connInfo, MonadConnectionInfo otherConnInfo)
		{
			if (!string.IsNullOrEmpty(connInfo.CertificateThumbprint) || !string.IsNullOrEmpty(otherConnInfo.CertificateThumbprint))
			{
				return string.Equals(connInfo.CertificateThumbprint, otherConnInfo.CertificateThumbprint);
			}
			if (connInfo.AuthenticationMechanism != otherConnInfo.AuthenticationMechanism)
			{
				return false;
			}
			if (connInfo.Credentials != null && otherConnInfo.Credentials != null)
			{
				return string.Equals(connInfo.Credentials.UserName, otherConnInfo.Credentials.UserName, StringComparison.OrdinalIgnoreCase);
			}
			return connInfo.Credentials == null && otherConnInfo.Credentials == null;
		}

		// Token: 0x040003A3 RID: 931
		private MonadConnectionInfo connectionInfo;

		// Token: 0x040003A4 RID: 932
		private RunspaceServerSettingsPresentationObject serverSettings;
	}
}
