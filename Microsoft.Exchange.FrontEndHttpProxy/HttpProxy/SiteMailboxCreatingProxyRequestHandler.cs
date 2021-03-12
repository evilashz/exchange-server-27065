using System;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D1 RID: 209
	internal class SiteMailboxCreatingProxyRequestHandler : EcpProxyRequestHandler
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
		internal static bool IsSiteMailboxCreatingProxyRequest(HttpRequest request)
		{
			if (request != null)
			{
				if (request.GetHttpMethod() == HttpMethod.Get)
				{
					string value = request.QueryString["ftr"];
					if ("TeamMailboxCreating".Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
					if (request.Url.AbsolutePath.EndsWith("TeamMailbox/NewSharePointTeamMailbox.aspx", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				else if (request.GetHttpMethod() == HttpMethod.Post && request.Url.AbsolutePath.EndsWith("DDI/DDIService.svc/NewObject", StringComparison.OrdinalIgnoreCase) && "TeamMailboxProperties".Equals(request.QueryString["schema"], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0002DC88 File Offset: 0x0002BE88
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			ExTraceGlobals.VerboseTracer.TraceDebug<string, Uri>((long)this.GetHashCode(), "[SiteMailboxCreatingProxyRequestHandler::ResolveAnchorMailbox]: Method {0}; Url {1};", base.ClientRequest.HttpMethod, base.ClientRequest.Url);
			if (!Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "SiteMailboxCreating-ServerVersion");
				return new ServerVersionAnchorMailbox<EcpService>(new ServerVersion(Server.E15MinVersion), ClientAccessType.Internal, this);
			}
			AnchorMailbox anchorMailbox = AnchorMailboxFactory.CreateFromCaller(this);
			if (anchorMailbox is AnonymousAnchorMailbox)
			{
				return anchorMailbox;
			}
			if (anchorMailbox is DomainAnchorMailbox || anchorMailbox is OrganizationAnchorMailbox)
			{
				return anchorMailbox;
			}
			SidAnchorMailbox sidAnchorMailbox = anchorMailbox as SidAnchorMailbox;
			if (sidAnchorMailbox != null)
			{
				if (sidAnchorMailbox.OrganizationId == null)
				{
					throw new InvalidOperationException(string.Format("OrganizationId is null for site mailbox proxy {0}.", anchorMailbox.ToString()));
				}
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "SiteMailboxCreating-Organization");
				return new OrganizationAnchorMailbox(sidAnchorMailbox.OrganizationId, this);
			}
			else
			{
				UserBasedAnchorMailbox userBasedAnchorMailbox = anchorMailbox as UserBasedAnchorMailbox;
				if (userBasedAnchorMailbox == null)
				{
					throw new InvalidOperationException(string.Format("Unknown site mailbox proxy {0}.", anchorMailbox.ToString()));
				}
				ADRawEntry adrawEntry = userBasedAnchorMailbox.GetADRawEntry();
				OrganizationId organizationId = (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
				if (organizationId == null)
				{
					throw new InvalidOperationException(string.Format("OrganizationId is null for site mailbox proxy {0}.", anchorMailbox.ToString()));
				}
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "SiteMailboxCreating-Organization");
				return new OrganizationAnchorMailbox(organizationId, this);
			}
		}
	}
}
