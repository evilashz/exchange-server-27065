using System;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000BA RID: 186
	internal class MrsProxyRequestHandler : BEServerCookieProxyRequestHandler<WebServicesService>
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0002AAA7 File Offset: 0x00028CA7
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.InternalNLBBypass;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002AAAA File Offset: 0x00028CAA
		protected override bool UseBackEndCacheForDownLevelServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0002AAB0 File Offset: 0x00028CB0
		internal static bool IsMrsRequest(HttpRequest request)
		{
			string[] segments = request.Url.Segments;
			if (segments == null || segments.Length != 3)
			{
				return false;
			}
			string text = segments[2].TrimEnd(new char[]
			{
				'/'
			});
			if (!text.Equals("MRSProxy.svc", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (!MrsProxyRequestHandler.IsMrsProxyEnabled())
			{
				throw new HttpException(403, "MRS proxy service is disabled");
			}
			return true;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0002AB14 File Offset: 0x00028D14
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = base.ClientRequest.Headers[Constants.TargetDatabaseHeaderName];
			if (!string.IsNullOrEmpty(text))
			{
				Guid databaseGuid;
				if (Guid.TryParse(text, out databaseGuid))
				{
					base.Logger.Set(HttpProxyMetadata.RoutingHint, "TargetDatabase-GUID");
					return new DatabaseGuidAnchorMailbox(databaseGuid, this);
				}
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "TargetDatabase-Name");
				return new DatabaseNameAnchorMailbox(text, this);
			}
			else
			{
				AnchorMailbox anchorMailbox = base.CreateAnchorMailboxFromRoutingHint();
				if (anchorMailbox != null)
				{
					return anchorMailbox;
				}
				if (Utilities.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
				{
					base.Logger.Set(HttpProxyMetadata.RoutingHint, "ClientVersionHeader");
					return base.GetServerVersionAnchorMailbox(base.ClientRequest.Headers[Constants.ClientVersionHeaderName]);
				}
				string text2 = base.ClientRequest.Headers[WellKnownHeader.GenericAnchorHint];
				if (!string.IsNullOrEmpty(text2))
				{
					return new PstProviderAnchorMailbox(text2, this);
				}
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "ForestWideOrganization");
				return new OrganizationAnchorMailbox(OrganizationId.ForestWideOrgId, this);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0002AC34 File Offset: 0x00028E34
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			UriBuilder uriBuilder = new UriBuilder(targetBackEndServerUrl);
			if (targetBackEndServerUrl.Port == 444)
			{
				uriBuilder.Port = 443;
			}
			uriBuilder.Path = "/Microsoft.Exchange.MailboxReplicationService.ProxyService";
			return uriBuilder.Uri;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002AC78 File Offset: 0x00028E78
		protected override BackEndServer GetDownLevelClientAccessServer(AnchorMailbox anchorMailbox, BackEndServer mailboxServer)
		{
			BackEndServer deterministicBackEndServer = HttpProxyBackEndHelper.GetDeterministicBackEndServer<WebServicesService>(mailboxServer, anchorMailbox.ToCookieKey(), this.ClientAccessType);
			ExTraceGlobals.VerboseTracer.TraceDebug<int, BackEndServer, BackEndServer>((long)this.GetHashCode(), "[MrsProxyRequestHandler::GetDownLevelClientAccessServer] Context {0}; Overriding down level target {0} with latest version backend {1}.", base.TraceContext, mailboxServer, deterministicBackEndServer);
			return deterministicBackEndServer;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		private static bool IsMrsProxyEnabled()
		{
			bool? flag = null;
			ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = (ADWebServicesVirtualDirectory)HttpProxyGlobals.VdirObject.Member;
			flag = new bool?(adwebServicesVirtualDirectory.MRSProxyEnabled);
			if (flag == null)
			{
				ExTraceGlobals.VerboseTracer.TraceError(0L, "[MrsProxyRequestHandler::IsMrsProxyEnabled] Can not find vdir.");
			}
			return flag != null && flag.Value;
		}

		// Token: 0x0400049C RID: 1180
		private const string BackEndMrsProxyPath = "/Microsoft.Exchange.MailboxReplicationService.ProxyService";

		// Token: 0x0400049D RID: 1181
		private const string FrontEndMrsProxyPath = "MRSProxy.svc";
	}
}
