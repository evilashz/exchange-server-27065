using System;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200059F RID: 1439
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DefaultHtmlCallbacks : HtmlCallbackBase
	{
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000F2046 File Offset: 0x000F0246
		public DefaultHtmlCallbacks(IItem item, bool itemReadOnly) : this(item.CoreItem, itemReadOnly)
		{
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000F2055 File Offset: 0x000F0255
		internal DefaultHtmlCallbacks(ICoreItem coreItem, bool itemReadOnly) : base(coreItem)
		{
			this.readOnly = itemReadOnly;
			this.clearEmptyLinks = false;
			this.removeLinksToNonImageAttachments = false;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000F2073 File Offset: 0x000F0273
		internal DefaultHtmlCallbacks(CoreAttachmentCollection collection, Body itemBody, bool itemReadOnly) : base(collection, itemBody)
		{
			this.readOnly = itemReadOnly;
			this.clearEmptyLinks = false;
			this.removeLinksToNonImageAttachments = false;
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000F2092 File Offset: 0x000F0292
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x000F209A File Offset: 0x000F029A
		internal bool ClearingEmptyLinks
		{
			get
			{
				return this.clearEmptyLinks;
			}
			set
			{
				this.clearEmptyLinks = value;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000F20A3 File Offset: 0x000F02A3
		// (set) Token: 0x06003ADF RID: 15071 RVA: 0x000F20AB File Offset: 0x000F02AB
		internal bool RemoveLinksToNonImageAttachments
		{
			get
			{
				return this.removeLinksToNonImageAttachments;
			}
			set
			{
				this.removeLinksToNonImageAttachments = value;
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000F20B4 File Offset: 0x000F02B4
		public override void ProcessTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			if (tagContext.TagId != HtmlTagId.Img)
			{
				tagContext.WriteTag(true);
				return;
			}
			AttachmentLink attachmentLink = null;
			string text = null;
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Src)
				{
					text = htmlTagContextAttribute.Value;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				attachmentLink = base.FindAttachmentByBodyReference(text);
			}
			if (attachmentLink == null)
			{
				if (!this.clearEmptyLinks || !DefaultHtmlCallbacks.IsEmptyLink(text))
				{
					tagContext.WriteTag(true);
				}
				return;
			}
			string text2;
			string text3;
			if (attachmentLink.AttachmentType == AttachmentType.Ole)
			{
				text2 = "image/jpeg";
				text3 = "jpg";
				attachmentLink.ConvertToImage();
			}
			else
			{
				text2 = attachmentLink.ContentType;
				text3 = attachmentLink.FileExtension;
			}
			bool flag = text.StartsWith("cid:Microsoft-Infopath-", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text2) && text2.Equals("application/octet-stream");
			if (DefaultHtmlCallbacks.IsInlineImage(text2, text3) || flag)
			{
				tagContext.WriteTag(false);
				foreach (HtmlTagContextAttribute htmlTagContextAttribute2 in tagContext.Attributes)
				{
					if (htmlTagContextAttribute2.Id == HtmlAttributeId.Src)
					{
						string value = "cid:" + this.GetOrGenerateAttachContentId(attachmentLink);
						htmlWriter.WriteAttribute(HtmlAttributeId.Src, value);
					}
					else
					{
						htmlTagContextAttribute2.Write();
					}
				}
				attachmentLink.MarkInline(true);
				return;
			}
			if (!this.RemoveLinksToNonImageAttachments)
			{
				string text4 = attachmentLink.Filename;
				if (text4 == null)
				{
					text4 = ServerStrings.DefaultHtmlAttachmentHrefText;
				}
				htmlWriter.WriteStartTag(HtmlTagId.A);
				htmlWriter.WriteAttribute(HtmlAttributeId.Href, "cid:" + this.GetOrGenerateAttachContentId(attachmentLink));
				htmlWriter.WriteText(text4);
				htmlWriter.WriteEndTag(HtmlTagId.A);
				attachmentLink.MarkInline(false);
			}
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000F22A4 File Offset: 0x000F04A4
		public override bool SaveChanges()
		{
			return !this.readOnly && base.SaveChanges();
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000F22B8 File Offset: 0x000F04B8
		internal static bool IsEmptyLink(string src)
		{
			return string.Compare(src, "objattph://", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(src, "rtfimage://", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(src, "cid:", StringComparison.CurrentCultureIgnoreCase) == 0;
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000F22F5 File Offset: 0x000F04F5
		public static bool IsInlineImage(string contentType, string fileExtension)
		{
			if (contentType != null && DefaultHtmlCallbacks.IsInlineImage(contentType))
			{
				return true;
			}
			if (fileExtension != null)
			{
				contentType = Attachment.CalculateContentType(fileExtension);
				return DefaultHtmlCallbacks.IsInlineImage(contentType);
			}
			return false;
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000F2318 File Offset: 0x000F0518
		private static bool IsInlineImage(string contentType)
		{
			return contentType != null && (contentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/pjpeg", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/gif", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/bmp", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/png", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/x-png", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000F2380 File Offset: 0x000F0580
		private string GetOrGenerateAttachContentId(AttachmentLink link)
		{
			string text = link.ContentId;
			if (string.IsNullOrEmpty(text))
			{
				text = AttachmentLink.CreateContentId(this.AttachmentCollection.ContainerItem, link.AttachmentId, this.contentIdDomain);
				link.ContentId = text;
			}
			return text;
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x000F23C1 File Offset: 0x000F05C1
		public void SetContentIdDomain(string domain)
		{
			Util.ThrowOnNullOrEmptyArgument(domain, "domain");
			this.contentIdDomain = domain;
		}

		// Token: 0x04001F82 RID: 8066
		private bool readOnly;

		// Token: 0x04001F83 RID: 8067
		private bool clearEmptyLinks;

		// Token: 0x04001F84 RID: 8068
		private bool removeLinksToNonImageAttachments;

		// Token: 0x04001F85 RID: 8069
		private string contentIdDomain;
	}
}
