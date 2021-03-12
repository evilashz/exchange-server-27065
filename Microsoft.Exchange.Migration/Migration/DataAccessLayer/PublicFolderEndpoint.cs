using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x02000093 RID: 147
	internal class PublicFolderEndpoint : MigrationEndpointBase
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x00025962 File Offset: 0x00023B62
		public PublicFolderEndpoint(MigrationEndpoint presentationObject) : base(presentationObject)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002596B File Offset: 0x00023B6B
		public PublicFolderEndpoint() : base(MigrationType.PublicFolder)
		{
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00025978 File Offset: 0x00023B78
		public override ConnectionSettingsBase ConnectionSettings
		{
			get
			{
				return ExchangeConnectionSettings.Create(base.Username, base.Domain, base.EncryptedPassword, this.RpcProxyServer, this.PublicFolderDatabaseServerLegacyDN, this.SourceMailboxLegacyDN, base.AuthenticationMethod);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000259AE File Offset: 0x00023BAE
		public override MigrationType PreferredMigrationType
		{
			get
			{
				return MigrationType.PublicFolder;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000259B5 File Offset: 0x00023BB5
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x000259BD File Offset: 0x00023BBD
		public Fqdn RpcProxyServer
		{
			get
			{
				return this.RemoteServer;
			}
			set
			{
				this.RemoteServer = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x000259C6 File Offset: 0x00023BC6
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x000259D8 File Offset: 0x00023BD8
		public string SourceMailboxLegacyDN
		{
			get
			{
				return base.ExtendedProperties.Get<string>("SourceMailboxLegacyDN");
			}
			set
			{
				base.ExtendedProperties.Set<string>("SourceMailboxLegacyDN", value);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x000259EB File Offset: 0x00023BEB
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x000259FD File Offset: 0x00023BFD
		public string PublicFolderDatabaseServerLegacyDN
		{
			get
			{
				return base.ExtendedProperties.Get<string>("PublicFolderDatabaseServerLegacyDN");
			}
			set
			{
				base.ExtendedProperties.Set<string>("PublicFolderDatabaseServerLegacyDN", value);
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00025A10 File Offset: 0x00023C10
		public static IMailbox ConnectToLocalSourceDatabase(Guid databaseGuid)
		{
			IMailbox result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MapiSourceMailbox mapiSourceMailbox = disposeGuard.Add<MapiSourceMailbox>(new MapiSourceMailbox(LocalMailboxFlags.LegacyPublicFolders | LocalMailboxFlags.ParallelPublicFolderMigration));
				((IMailbox)mapiSourceMailbox).Config(null, databaseGuid, databaseGuid, CommonUtils.GetPartitionHint(OrganizationId.ForestWideOrgId), databaseGuid, MailboxType.SourceMailbox, null);
				((IMailbox)mapiSourceMailbox).Connect(MailboxConnectFlags.None);
				PublicFolderEndpoint.ThrowIfMinimumRequiredVersionNotInstalled(mapiSourceMailbox.ServerVersion);
				disposeGuard.Success();
				result = mapiSourceMailbox;
			}
			return result;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00025A94 File Offset: 0x00023C94
		public IMailbox ConnectToSourceDatabase()
		{
			IMailbox result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MapiSourceMailbox mapiSourceMailbox = disposeGuard.Add<MapiSourceMailbox>(new MapiSourceMailbox(LocalMailboxFlags.PureMAPI | LocalMailboxFlags.LegacyPublicFolders | LocalMailboxFlags.ParallelPublicFolderMigration));
				mapiSourceMailbox.ConfigRPCHTTP(this.SourceMailboxLegacyDN, null, this.PublicFolderDatabaseServerLegacyDN, this.RpcProxyServer, base.NetworkCredentials, true, base.AuthenticationMethod == AuthenticationMethod.Ntlm);
				((IMailbox)mapiSourceMailbox).Connect(MailboxConnectFlags.None);
				PublicFolderEndpoint.ThrowIfMinimumRequiredVersionNotInstalled(mapiSourceMailbox.ServerVersion);
				disposeGuard.Success();
				result = mapiSourceMailbox;
			}
			return result;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00025B28 File Offset: 0x00023D28
		public override void VerifyConnectivity()
		{
			try
			{
				using (IMailbox mailbox = this.ConnectToSourceDatabase())
				{
					mailbox.Disconnect();
				}
			}
			catch (MigrationTransientException)
			{
				throw;
			}
			catch (LocalizedException innerException)
			{
				throw new MigrationServerConnectionFailedException(this.RpcProxyServer.ToString(), innerException);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00025B90 File Offset: 0x00023D90
		protected override void ApplyAdditionalProperties(MigrationEndpoint presentationObject)
		{
			presentationObject.Authentication = new AuthenticationMethod?(base.AuthenticationMethod);
			presentationObject.Credentials = this.Credentials;
			presentationObject.RpcProxyServer = this.RpcProxyServer;
			presentationObject.PublicFolderDatabaseServerLegacyDN = this.PublicFolderDatabaseServerLegacyDN;
			presentationObject.SourceMailboxLegacyDN = this.SourceMailboxLegacyDN;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00025BDE File Offset: 0x00023DDE
		protected override void InitializeFromPresentationObject(MigrationEndpoint endpoint)
		{
			base.InitializeFromPresentationObject(endpoint);
			this.RpcProxyServer = endpoint.RpcProxyServer;
			this.PublicFolderDatabaseServerLegacyDN = endpoint.PublicFolderDatabaseServerLegacyDN;
			this.SourceMailboxLegacyDN = endpoint.SourceMailboxLegacyDN;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00025C0C File Offset: 0x00023E0C
		private static void ThrowIfMinimumRequiredVersionNotInstalled(int sourceServerVersion)
		{
			LocalizedString? localizedString = ParallelPublicFolderMigrationVersionChecker.CheckForMinimumRequiredVersion(sourceServerVersion);
			if (localizedString != null)
			{
				throw new MigrationTransientException(localizedString.Value);
			}
		}
	}
}
