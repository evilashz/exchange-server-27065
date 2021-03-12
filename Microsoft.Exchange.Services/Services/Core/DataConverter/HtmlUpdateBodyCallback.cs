using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200015B RID: 347
	internal class HtmlUpdateBodyCallback : HtmlCallbackBase
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0002EEDC File Offset: 0x0002D0DC
		public HtmlUpdateBodyCallback(Item item) : base(HtmlUpdateBodyCallback.GetAttachmentCollection(item), item.Body)
		{
			this.item = item;
			base.RemoveUnlinkedAttachments = true;
			base.ClearInlineOnUnmarkedAttachments = true;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002EF05 File Offset: 0x0002D105
		public override CoreAttachmentCollection AttachmentCollection
		{
			get
			{
				return this.item.AttachmentCollection.CoreAttachmentCollection;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002EF18 File Offset: 0x0002D118
		public override void ProcessTag(HtmlTagContext htmlTagContext, HtmlWriter htmlWriter)
		{
			HtmlTagId tagId = htmlTagContext.TagId;
			if (tagId == HtmlTagId.Img)
			{
				this.ProcessImgTag(htmlTagContext, htmlWriter);
				return;
			}
			htmlTagContext.WriteTag(true);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002EF41 File Offset: 0x0002D141
		private static AttachmentCollection GetAttachmentCollection(Item item)
		{
			return item.AttachmentCollection;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002EF4C File Offset: 0x0002D14C
		private void ProcessImgTag(HtmlTagContext htmlTagContext, HtmlWriter htmlWriter)
		{
			string text = null;
			string text2 = null;
			htmlTagContext.WriteTag(false);
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in htmlTagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Src)
				{
					text = htmlTagContextAttribute.Value;
				}
				else if (htmlTagContextAttribute.Name.Equals("originalSrc", StringComparison.OrdinalIgnoreCase) || htmlTagContextAttribute.Name.Equals("blockedImageSrc", StringComparison.OrdinalIgnoreCase))
				{
					text2 = htmlTagContextAttribute.Value;
				}
				else
				{
					htmlTagContextAttribute.Write();
				}
			}
			if (text2 != null)
			{
				text = text2;
			}
			if (text != null)
			{
				this.MarkInlineAttachment(text);
				htmlWriter.WriteAttribute(HtmlAttributeId.Src, text);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002F010 File Offset: 0x0002D210
		private void MarkInlineAttachment(string srcValue)
		{
			string contentId = this.GetContentId(srcValue);
			if (contentId == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "HtmlUpdateBodyCallback.MarkInlineAttachment. Content ID is empty for {0}", srcValue);
				return;
			}
			AttachmentId attachmentId = this.FindAttachmentId(contentId);
			if (attachmentId == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "HtmlUpdateBodyCallback.MarkInlineAttachment. Attachment ID not found for {0}", srcValue);
				return;
			}
			AttachmentLink attachmentLink = base.FindAttachmentByIdOrContentId(attachmentId, contentId);
			if (attachmentLink == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "HtmlUpdateBodyCallback.MarkInlineAttachment. No attachment link found for {0}", srcValue);
				return;
			}
			attachmentLink.MarkInline(true);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002F090 File Offset: 0x0002D290
		private string GetContentId(string srcValue)
		{
			string result = null;
			if (srcValue.StartsWith("cid:", StringComparison.OrdinalIgnoreCase) && srcValue.Length > "cid:".Length)
			{
				result = srcValue.Substring("cid:".Length);
			}
			return result;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002F0D4 File Offset: 0x0002D2D4
		private AttachmentId FindAttachmentId(string contentId)
		{
			AttachmentId result = null;
			foreach (AttachmentHandle handle in this.item.AttachmentCollection)
			{
				using (Attachment attachment = this.item.AttachmentCollection.Open(handle))
				{
					if (attachment.ContentId.Equals(contentId, StringComparison.OrdinalIgnoreCase))
					{
						result = attachment.Id;
					}
				}
			}
			return result;
		}

		// Token: 0x040007A6 RID: 1958
		private Item item;
	}
}
