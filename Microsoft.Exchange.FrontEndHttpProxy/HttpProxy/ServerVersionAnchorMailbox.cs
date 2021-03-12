using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000015 RID: 21
	internal class ServerVersionAnchorMailbox<ServiceType> : AnchorMailbox where ServiceType : HttpService
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00004928 File Offset: 0x00002B28
		public ServerVersionAnchorMailbox(ServerVersion serverVersion, ClientAccessType clientAccessType, IRequestContext requestContext) : base(AnchorSource.ServerVersion, serverVersion, requestContext)
		{
			this.ClientAccessType = clientAccessType;
			base.NotFoundExceptionCreator = delegate()
			{
				string message = string.Format("Cannot find Mailbox server with {0}.", this.ServerVersion);
				return new ServerNotFoundException(message, this.ServerVersion.ToString());
			};
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000495F File Offset: 0x00002B5F
		public ServerVersionAnchorMailbox(ServerVersion serverVersion, ClientAccessType clientAccessType, bool exactVersionMatch, IRequestContext requestContext) : this(serverVersion, clientAccessType, requestContext)
		{
			this.ExactVersionMatch = exactVersionMatch;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004972 File Offset: 0x00002B72
		public ServerVersion ServerVersion
		{
			get
			{
				return (ServerVersion)base.SourceObject;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000497F File Offset: 0x00002B7F
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004987 File Offset: 0x00002B87
		public ClientAccessType ClientAccessType { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004990 File Offset: 0x00002B90
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004998 File Offset: 0x00002B98
		public bool ExactVersionMatch { get; private set; }

		// Token: 0x0600009F RID: 159 RVA: 0x000049A4 File Offset: 0x00002BA4
		public override BackEndServer TryDirectBackEndCalculation()
		{
			if (this.ServerVersion.Major == 15 && !this.ExactVersionMatch)
			{
				BackEndServer backEndServer = LocalSiteMailboxServerCache.Instance.TryGetRandomE15Server(base.RequestContext);
				if (backEndServer != null)
				{
					ServerVersion serverVersion = new ServerVersion(backEndServer.Version);
					if (serverVersion.Minor >= this.ServerVersion.Minor)
					{
						return backEndServer;
					}
				}
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.ServersCache.Enabled)
			{
				try
				{
					MiniServer miniServer;
					if (this.ExactVersionMatch)
					{
						miniServer = ServersCache.GetAnyBackEndServerWithExactVersion(this.ServerVersion.ToInt());
					}
					else
					{
						miniServer = ServersCache.GetAnyBackEndServerWithMinVersion(this.ServerVersion.ToInt());
					}
					return new BackEndServer(miniServer.Fqdn, miniServer.VersionNumber);
				}
				catch (ServerHasNotBeenFoundException)
				{
					return base.CheckForNullAndThrowIfApplicable<BackEndServer>(null);
				}
			}
			BackEndServer result;
			try
			{
				BackEndServer anyBackEndServerForVersion = HttpProxyBackEndHelper.GetAnyBackEndServerForVersion<ServiceType>(this.ServerVersion, this.ExactVersionMatch, this.ClientAccessType, false);
				result = anyBackEndServerForVersion;
			}
			catch (ServerNotFoundException)
			{
				result = base.CheckForNullAndThrowIfApplicable<BackEndServer>(null);
			}
			return result;
		}
	}
}
