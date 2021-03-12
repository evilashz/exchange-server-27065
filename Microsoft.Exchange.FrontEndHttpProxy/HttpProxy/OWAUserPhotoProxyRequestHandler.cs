using System;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B5 RID: 181
	internal sealed class OWAUserPhotoProxyRequestHandler : OwaProxyRequestHandler
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000293CF File Offset: 0x000275CF
		protected override bool UseBackEndCacheForDownLevelServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000293D2 File Offset: 0x000275D2
		internal static bool IsUserPhotoRequest(HttpRequest request)
		{
			return request.Path.StartsWith("/owa/service.svc/s/GetUserPhoto", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000293E8 File Offset: 0x000275E8
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = this.TryGetExplicitLogonNode(ExplicitLogonNode.Second);
			if (!string.IsNullOrEmpty(text) && SmtpAddress.IsValidSmtpAddress(text))
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "ExplicitLogon-SMTP");
				return new SmtpAnchorMailbox(text, this);
			}
			return base.ResolveAnchorMailbox();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00029432 File Offset: 0x00027632
		protected override string TryGetExplicitLogonNode(ExplicitLogonNode node)
		{
			return base.ClientRequest.QueryString["email"];
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002944C File Offset: 0x0002764C
		protected override BackEndServer GetDownLevelClientAccessServer(AnchorMailbox anchorMailbox, BackEndServer mailboxServer)
		{
			BackEndServer deterministicBackEndServer = HttpProxyBackEndHelper.GetDeterministicBackEndServer<WebServicesService>(mailboxServer, anchorMailbox.ToCookieKey(), this.ClientAccessType);
			ExTraceGlobals.VerboseTracer.TraceDebug<int, BackEndServer, BackEndServer>((long)this.GetHashCode(), "[OWAUserPhotoProxyRequestHandler::GetDownLevelClientAccessServer] Context {0}; Overriding down level target {0} with latest version backend {1}.", base.TraceContext, mailboxServer, deterministicBackEndServer);
			return deterministicBackEndServer;
		}

		// Token: 0x04000485 RID: 1157
		private const string OwaGetUserPhotoPath = "/owa/service.svc/s/GetUserPhoto";
	}
}
