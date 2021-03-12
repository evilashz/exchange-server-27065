using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001FF RID: 511
	internal sealed class OwaSafeHtmlRemoveWebBeaconCallbacks : OwaSafeHtmlOutboundCallbacks
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x00067209 File Offset: 0x00065409
		public OwaSafeHtmlRemoveWebBeaconCallbacks(OwaContext owaContext, bool isEditableContent) : base(owaContext, isEditableContent)
		{
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00067213 File Offset: 0x00065413
		public OwaSafeHtmlRemoveWebBeaconCallbacks(Item item, bool userLogon, OwaContext owaContext, bool isEditableContent) : base(item, userLogon, false, owaContext, isEditableContent)
		{
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00067221 File Offset: 0x00065421
		public OwaSafeHtmlRemoveWebBeaconCallbacks(Item item, bool userLogon, bool isEmbedded, string itemUrl, OwaContext owaContext, bool isEditableContent) : base(item, userLogon, isEmbedded, itemUrl, false, owaContext, isEditableContent)
		{
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00067233 File Offset: 0x00065433
		public override bool HasBlockedImages
		{
			get
			{
				return this.hasBlockedImages;
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0006723C File Offset: 0x0006543C
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
				this.hasBlockedImages = true;
			}
		}
	}
}
