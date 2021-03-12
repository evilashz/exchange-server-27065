using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x0200005C RID: 92
	internal class HtmlUpdateBodyCallback : HtmlCallbackBase
	{
		// Token: 0x06000206 RID: 518 RVA: 0x000074E8 File Offset: 0x000056E8
		public HtmlUpdateBodyCallback(IItem item) : base(item.AttachmentCollection, item.Body)
		{
			this.item = item;
			base.RemoveUnlinkedAttachments = true;
			base.ClearInlineOnUnmarkedAttachments = true;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00007511 File Offset: 0x00005711
		public override CoreAttachmentCollection AttachmentCollection
		{
			get
			{
				return this.item.AttachmentCollection.CoreAttachmentCollection;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007524 File Offset: 0x00005724
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

		// Token: 0x06000209 RID: 521 RVA: 0x00007550 File Offset: 0x00005750
		private static string GetContentId(string srcValue)
		{
			string result = null;
			if (srcValue.StartsWith("cid:", StringComparison.OrdinalIgnoreCase) && srcValue.Length > "cid:".Length)
			{
				result = srcValue.Substring("cid:".Length);
			}
			return result;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007594 File Offset: 0x00005794
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

		// Token: 0x0600020B RID: 523 RVA: 0x00007624 File Offset: 0x00005824
		private void MarkInlineAttachment(string srcValue)
		{
			string contentId = HtmlUpdateBodyCallback.GetContentId(srcValue);
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

		// Token: 0x0600020C RID: 524 RVA: 0x000076A4 File Offset: 0x000058A4
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

		// Token: 0x040000A5 RID: 165
		private readonly IItem item;
	}
}
