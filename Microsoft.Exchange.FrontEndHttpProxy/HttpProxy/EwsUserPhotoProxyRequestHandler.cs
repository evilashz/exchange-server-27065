using System;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B4 RID: 180
	internal sealed class EwsUserPhotoProxyRequestHandler : EwsProxyRequestHandler
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00029314 File Offset: 0x00027514
		protected override bool UseBackEndCacheForDownLevelServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00029317 File Offset: 0x00027517
		internal static bool IsUserPhotoRequest(HttpRequest request)
		{
			return ProtocolHelper.IsEwsGetUserPhotoRequest(request.Path);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00029324 File Offset: 0x00027524
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

		// Token: 0x0600066C RID: 1644 RVA: 0x0002936E File Offset: 0x0002756E
		protected override string TryGetExplicitLogonNode(ExplicitLogonNode node)
		{
			return base.ClientRequest.QueryString["email"];
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00029388 File Offset: 0x00027588
		protected override BackEndServer GetDownLevelClientAccessServer(AnchorMailbox anchorMailbox, BackEndServer mailboxServer)
		{
			BackEndServer deterministicBackEndServer = HttpProxyBackEndHelper.GetDeterministicBackEndServer<WebServicesService>(mailboxServer, anchorMailbox.ToCookieKey(), this.ClientAccessType);
			ExTraceGlobals.VerboseTracer.TraceDebug<int, BackEndServer, BackEndServer>((long)this.GetHashCode(), "[EwsUserPhotoProxyRequestHandler::GetDownLevelClientAccessServer] Context {0}; Overriding down level target {0} with latest version backend {1}.", base.TraceContext, mailboxServer, deterministicBackEndServer);
			return deterministicBackEndServer;
		}
	}
}
