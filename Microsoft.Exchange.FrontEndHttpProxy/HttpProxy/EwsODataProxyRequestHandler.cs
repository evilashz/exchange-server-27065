using System;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B6 RID: 182
	internal sealed class EwsODataProxyRequestHandler : EwsProxyRequestHandler
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00029493 File Offset: 0x00027693
		internal static bool IsODataRequest(HttpRequest request)
		{
			return ProtocolHelper.IsEwsODataRequest(request.Path);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000294A0 File Offset: 0x000276A0
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = this.TryResolveTargetMailbox();
			if (!string.IsNullOrEmpty(text))
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "TargetMailbox-SMTP");
				return new SmtpAnchorMailbox(text, this);
			}
			return base.ResolveAnchorMailbox();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000294E4 File Offset: 0x000276E4
		private string TryResolveTargetMailbox()
		{
			Match match = Constants.UsersEntityRegex.Match(base.ClientRequest.Url.PathAndQuery);
			if (match.Success)
			{
				string text = match.Result("${address}");
				if (SmtpAddress.IsValidSmtpAddress(text))
				{
					return text;
				}
			}
			return null;
		}
	}
}
