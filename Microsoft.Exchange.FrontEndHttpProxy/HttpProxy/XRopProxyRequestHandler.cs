using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D3 RID: 211
	internal class XRopProxyRequestHandler : BEServerCookieProxyRequestHandler<RpcHttpService>
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0002DF90 File Offset: 0x0002C190
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.External;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0002DF94 File Offset: 0x0002C194
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = base.ClientRequest.QueryString[Constants.AnchorMailboxHeaderName];
			if (!string.IsNullOrEmpty(text) && SmtpAddress.IsValidSmtpAddress(text))
			{
				SmtpAnchorMailbox smtpAnchorMailbox = new SmtpAnchorMailbox(text, this);
				string value = "AnchorMailboxQuery-SMTP";
				if (!base.ClientRequest.GetHttpRequestBase().IsProxyTestProbeRequest())
				{
					smtpAnchorMailbox.IsArchive = new bool?(true);
					value = "AnchorMailboxQuery-Archive-SMTP";
				}
				base.Logger.Set(HttpProxyMetadata.RoutingHint, value);
				return smtpAnchorMailbox;
			}
			return new OrganizationAnchorMailbox(OrganizationId.ForestWideOrgId, this);
		}
	}
}
