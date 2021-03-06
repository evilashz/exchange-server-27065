using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001FD RID: 509
	internal sealed class OwaSafeHtmlConversationsCallbacks : OwaSafeHtmlOutboundCallbacks
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x00066E78 File Offset: 0x00065078
		public OwaSafeHtmlConversationsCallbacks(Item item, bool userLogon, bool isJunkOrPhishing, OwaContext owaContext) : base(item, userLogon, false, null, isJunkOrPhishing, owaContext, false)
		{
			this.openMailtoInNewWindow = true;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00066E90 File Offset: 0x00065090
		protected override void ProcessImageTag(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
			AttachmentLink attachmentLink = base.IsInlineImage(filterAttribute);
			if (attachmentLink != null)
			{
				base.OutputInlineReference(filterAttribute, context, attachmentLink, writer);
				return;
			}
			if (base.IsSafeUrl(filterAttribute.Value, filterAttribute.Id))
			{
				this.hasBlockedImagesInCurrentPass = true;
				this.hasBlockedImages = true;
				string value = this.owaContext.UserContext.GetBlankPage(Utilities.PremiumScriptPath) + "#" + OwaSafeHtmlConversationsCallbacks.UrlDelimiter + filterAttribute.Value;
				writer.WriteAttribute(HtmlAttributeId.Src, value);
			}
		}

		// Token: 0x04000B71 RID: 2929
		private static readonly string UrlDelimiter = "__OWA_FLT000__";
	}
}
