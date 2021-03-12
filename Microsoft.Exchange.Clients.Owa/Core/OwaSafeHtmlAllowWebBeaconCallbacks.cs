using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001FC RID: 508
	internal sealed class OwaSafeHtmlAllowWebBeaconCallbacks : OwaSafeHtmlOutboundCallbacks
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x00066DF6 File Offset: 0x00064FF6
		public OwaSafeHtmlAllowWebBeaconCallbacks(OwaContext owaContext, bool isEditableContent) : base(owaContext, isEditableContent)
		{
			this.allowForms = true;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00066E07 File Offset: 0x00065007
		public OwaSafeHtmlAllowWebBeaconCallbacks(Item item, bool userLogon, OwaContext owaContext, bool isEditableContent) : base(item, userLogon, false, owaContext, isEditableContent)
		{
			this.allowForms = true;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00066E1C File Offset: 0x0006501C
		public OwaSafeHtmlAllowWebBeaconCallbacks(Item item, bool userLogon, bool isEmbedded, string itemUrl, OwaContext owaContext, bool isEditableContent) : base(item, userLogon, isEmbedded, itemUrl, false, owaContext, isEditableContent)
		{
			this.allowForms = true;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00066E35 File Offset: 0x00065035
		public override bool HasBlockedImages
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00066E38 File Offset: 0x00065038
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
				filterAttribute.Write();
			}
		}
	}
}
