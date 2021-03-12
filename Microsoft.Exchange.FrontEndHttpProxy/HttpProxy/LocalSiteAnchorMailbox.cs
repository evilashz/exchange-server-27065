using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200001D RID: 29
	internal class LocalSiteAnchorMailbox : AnchorMailbox
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000562A File Offset: 0x0000382A
		public LocalSiteAnchorMailbox(IRequestContext requestContext) : base(AnchorSource.Anonymous, LocalSiteAnchorMailbox.LocalSiteIdentifier, requestContext)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000563C File Offset: 0x0000383C
		public override BackEndServer TryDirectBackEndCalculation()
		{
			BackEndServer backEndServer = LocalSiteMailboxServerCache.Instance.TryGetRandomE15Server(base.RequestContext);
			if (backEndServer != null)
			{
				return backEndServer;
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.ServersCache.Enabled)
			{
				try
				{
					MiniServer anyBackEndServerFromLocalSite = ServersCache.GetAnyBackEndServerFromLocalSite(Server.E15MinVersion, false);
					return new BackEndServer(anyBackEndServerFromLocalSite.Fqdn, anyBackEndServerFromLocalSite.VersionNumber);
				}
				catch (ServerHasNotBeenFoundException)
				{
					return base.CheckForNullAndThrowIfApplicable<BackEndServer>(null);
				}
			}
			return HttpProxyBackEndHelper.GetAnyBackEndServerForVersion<WebServicesService>(new ServerVersion(Server.E15MinVersion), false, ClientAccessType.InternalNLBBypass, true);
		}

		// Token: 0x0400004F RID: 79
		internal static readonly string LocalSiteIdentifier = "LocalSite";
	}
}
