using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000A6 RID: 166
	internal abstract class EwsAutodiscoverProxyRequestHandler : BEServerCookieProxyRequestHandler<WebServicesService>
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00024F91 File Offset: 0x00023191
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00024F99 File Offset: 0x00023199
		protected bool PreferAnchorMailboxHeader
		{
			get
			{
				return this.preferAnchorMailboxHeader;
			}
			set
			{
				this.preferAnchorMailboxHeader = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00024FA2 File Offset: 0x000231A2
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00024FAA File Offset: 0x000231AA
		protected bool SkipTargetBackEndCalculation
		{
			get
			{
				return this.skipTargetBackEndCalculation;
			}
			set
			{
				this.skipTargetBackEndCalculation = value;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00024FB4 File Offset: 0x000231B4
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			if (this.skipTargetBackEndCalculation)
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "OrgRelationship-Anonymous");
				return new AnonymousAnchorMailbox(this);
			}
			string text;
			if (ProtocolHelper.IsAutodiscoverV2PreviewRequest(base.ClientRequest.Url.AbsolutePath))
			{
				text = base.ClientRequest.Params["Email"];
			}
			else if (ProtocolHelper.IsAutodiscoverV2Version1Request(base.ClientRequest.Url.AbsolutePath))
			{
				int num = base.ClientRequest.Url.AbsolutePath.LastIndexOf('/');
				text = base.ClientRequest.Url.AbsolutePath.Substring(num + 1);
			}
			else
			{
				text = this.TryGetExplicitLogonNode(ExplicitLogonNode.Second);
			}
			string text2;
			if (ProtocolHelper.TryGetValidNormalizedExplicitLogonAddress(text, out text2))
			{
				this.isExplicitLogonRequest = true;
				this.explicitLogonAddress = text;
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "ExplicitLogon-SMTP");
				SmtpAnchorMailbox smtpAnchorMailbox = new SmtpAnchorMailbox(text2, this);
				if (this.preferAnchorMailboxHeader)
				{
					string text3 = base.ClientRequest.Headers[Constants.AnchorMailboxHeaderName];
					if (!string.IsNullOrEmpty(text3) && !StringComparer.OrdinalIgnoreCase.Equals(text3, text2) && SmtpAddress.IsValidSmtpAddress(text3))
					{
						base.Logger.Set(HttpProxyMetadata.RoutingHint, "AnchorMailboxHeader-SMTP");
						smtpAnchorMailbox = new SmtpAnchorMailbox(text3, this);
					}
				}
				if (ProtocolHelper.IsAutodiscoverV2Request(base.ClientRequest.Url.AbsolutePath))
				{
					smtpAnchorMailbox.FailOnDomainNotFound = false;
				}
				return smtpAnchorMailbox;
			}
			return base.ResolveAnchorMailbox();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00025128 File Offset: 0x00023328
		protected override bool ShouldExcludeFromExplicitLogonParsing()
		{
			return false;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002512B File Offset: 0x0002332B
		protected override bool IsValidExplicitLogonNode(string node, bool nodeIsLast)
		{
			if (nodeIsLast)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[AutodiscoverProxyRequestHandler::IsValidExplicitLogonNode]: Context {0}; rejected explicit logon node: {1}", base.TraceContext, node);
				return false;
			}
			return true;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00025150 File Offset: 0x00023350
		protected override UriBuilder GetClientUrlForProxy()
		{
			string text = base.ClientRequest.Url.ToString();
			string uri = text;
			if (this.isExplicitLogonRequest && !ProtocolHelper.IsAutodiscoverV2Request(base.ClientRequest.Url.AbsoluteUri))
			{
				string text2 = "/" + this.explicitLogonAddress;
				int num = text.IndexOf(text2);
				if (num != -1)
				{
					uri = text.Substring(0, num) + text.Substring(num + text2.Length);
				}
			}
			return new UriBuilder(uri);
		}

		// Token: 0x04000412 RID: 1042
		private bool preferAnchorMailboxHeader;

		// Token: 0x04000413 RID: 1043
		private bool skipTargetBackEndCalculation;

		// Token: 0x04000414 RID: 1044
		private bool isExplicitLogonRequest;

		// Token: 0x04000415 RID: 1045
		private string explicitLogonAddress;
	}
}
