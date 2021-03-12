using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001F6 RID: 502
	internal class SafeHtmlCallback : HtmlCallbackBase
	{
		// Token: 0x060013A8 RID: 5032 RVA: 0x000710C7 File Offset: 0x0006F2C7
		public SafeHtmlCallback(Item item) : base(item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.itemId = ((item.Id != null) ? item.Id.ObjectId : StoreObjectId.DummyId);
			base.InitializeAttachmentLinks(null);
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00071106 File Offset: 0x0006F306
		public static bool HasBlockedImages
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00071109 File Offset: 0x0006F309
		public bool HasBlockedInlineAttachments
		{
			get
			{
				return this.hasBlockedInlineAttachments;
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00071114 File Offset: 0x0006F314
		public override void ProcessTag(HtmlTagContext context, HtmlWriter writer)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			if (context.TagId == HtmlTagId.Link)
			{
				bool writeTag = SafeHtmlCallback.WriteTagWithMicroData(context);
				SafeHtmlCallback.ProcessMicrodataTag(writeTag, context, SafeHtmlCallback.linkTagAttributes);
				return;
			}
			if (context.TagId == HtmlTagId.Head)
			{
				context.WriteTag(true);
				return;
			}
			if (context.TagId == HtmlTagId.Meta)
			{
				bool writeTag2 = SafeHtmlCallback.WriteTagWithMicroData(context);
				SafeHtmlCallback.ProcessMicrodataTag(writeTag2, context, SafeHtmlCallback.metaTagAttributes);
				return;
			}
			if (context.TagId == HtmlTagId.Base)
			{
				foreach (HtmlTagContextAttribute attribute in context.Attributes)
				{
					if (SafeHtmlCallback.IsBaseTag(context.TagId, attribute))
					{
						string value = attribute.Value;
						if (!Uri.TryCreate(value, UriKind.Absolute, out this.baseRef))
						{
							this.baseRef = null;
							break;
						}
						break;
					}
				}
			}
			context.WriteTag();
			bool flag5 = false;
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in context.Attributes)
			{
				if (context.TagId == HtmlTagId.Form || context.TagId == HtmlTagId.Input)
				{
					if (htmlTagContextAttribute.Id != HtmlAttributeId.Src && htmlTagContextAttribute.Id != HtmlAttributeId.Action && htmlTagContextAttribute.Id != HtmlAttributeId.Method && htmlTagContextAttribute.Id != HtmlAttributeId.Target)
					{
						htmlTagContextAttribute.Write();
					}
				}
				else if (htmlTagContextAttribute.Id != HtmlAttributeId.UseMap)
				{
					if (SafeHtmlCallback.IsUrlTag(context.TagId, htmlTagContextAttribute))
					{
						if (!flag)
						{
							this.ProcessHtmlUrlTag(htmlTagContextAttribute, writer);
							flag = true;
						}
					}
					else if (SafeHtmlCallback.IsImageTag(context.TagId, htmlTagContextAttribute))
					{
						if ((htmlTagContextAttribute.Id == HtmlAttributeId.Src && !flag2) || (htmlTagContextAttribute.Id == HtmlAttributeId.DynSrc && !flag3) || (htmlTagContextAttribute.Id == HtmlAttributeId.LowSrc && !flag4))
						{
							this.ProcessImageTag(htmlTagContextAttribute, context, writer);
							if (htmlTagContextAttribute.Value == "rtfimage://")
							{
								flag5 = true;
							}
							if (htmlTagContextAttribute.Id == HtmlAttributeId.Src)
							{
								flag2 = true;
							}
							else if (htmlTagContextAttribute.Id == HtmlAttributeId.DynSrc)
							{
								flag3 = true;
							}
							else if (htmlTagContextAttribute.Id == HtmlAttributeId.LowSrc)
							{
								flag4 = true;
							}
						}
					}
					else if (SafeHtmlCallback.IsBackgroundAttribute(htmlTagContextAttribute))
					{
						this.ProcessImageTag(htmlTagContextAttribute, context, writer);
					}
					else if (!SafeHtmlCallback.IsTargetTagInAnchor(context.TagId, htmlTagContextAttribute))
					{
						if (SafeHtmlCallback.IsSanitizingAttribute(htmlTagContextAttribute))
						{
							if (htmlTagContextAttribute.Id == HtmlAttributeId.Border)
							{
								text = htmlTagContextAttribute.Value;
							}
							else if (htmlTagContextAttribute.Id == HtmlAttributeId.Height)
							{
								text2 = htmlTagContextAttribute.Value;
							}
							else if (htmlTagContextAttribute.Id == HtmlAttributeId.Width)
							{
								text3 = htmlTagContextAttribute.Value;
							}
						}
						else
						{
							htmlTagContextAttribute.Write();
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3) && this.hasBlockedImagesInCurrentPass)
			{
				SafeHtmlCallback.SanitizeImage(writer, text, text2, text3);
			}
			else if (!this.hasBlockedImagesInCurrentPass)
			{
				if (flag5)
				{
					writer.WriteAttribute(HtmlAttributeId.Height, "0");
					writer.WriteAttribute(HtmlAttributeId.Width, "0");
				}
				else
				{
					if (!string.IsNullOrEmpty(text2))
					{
						writer.WriteAttribute(HtmlAttributeId.Height, text2);
					}
					if (!string.IsNullOrEmpty(text3))
					{
						writer.WriteAttribute(HtmlAttributeId.Width, text3);
					}
					if (!string.IsNullOrEmpty(text))
					{
						writer.WriteAttribute(HtmlAttributeId.Border, text);
					}
				}
			}
			if (this.hasFoundRedirUrlInCurrentPass)
			{
				writer.WriteAttribute(HtmlAttributeId.Target, "_BLANK");
			}
			this.hasBlockedImagesInCurrentPass = false;
			this.hasFoundRedirUrlInCurrentPass = false;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000714BC File Offset: 0x0006F6BC
		protected static bool WriteTagWithMicroData(HtmlTagContext context)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in context.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.ItemProp || htmlTagContextAttribute.Id == HtmlAttributeId.ItemRef || htmlTagContextAttribute.Id == HtmlAttributeId.ItemScope || htmlTagContextAttribute.Id == HtmlAttributeId.ItemId || htmlTagContextAttribute.Id == HtmlAttributeId.ItemType)
				{
					context.WriteTag();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00071550 File Offset: 0x0006F750
		protected static void ProcessMicrodataTag(bool writeTag, HtmlTagContext context, List<HtmlAttributeId> safeAttributes)
		{
			if (!writeTag)
			{
				context.DeleteTag();
				return;
			}
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in context.Attributes)
			{
				if (safeAttributes.Contains(htmlTagContextAttribute.Id))
				{
					bool flag = true;
					if (htmlTagContextAttribute.Id == HtmlAttributeId.Href && !SafeHtmlCallback.IsSafeUrl(htmlTagContextAttribute.Value, htmlTagContextAttribute.Id))
					{
						flag = false;
					}
					if (flag)
					{
						htmlTagContextAttribute.Write();
					}
				}
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000715E8 File Offset: 0x0006F7E8
		protected static bool IsBackgroundAttribute(HtmlTagContextAttribute attribute)
		{
			return attribute.Id == HtmlAttributeId.Background;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00071604 File Offset: 0x0006F804
		protected static bool IsBaseTag(HtmlTagId tagId, HtmlTagContextAttribute attribute)
		{
			return tagId == HtmlTagId.Base && attribute.Id == HtmlAttributeId.Href;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00071628 File Offset: 0x0006F828
		protected static bool IsImageTag(HtmlTagId tagId, HtmlTagContextAttribute attribute)
		{
			return tagId == HtmlTagId.Img && (attribute.Id == HtmlAttributeId.Src || attribute.Id == HtmlAttributeId.DynSrc || attribute.Id == HtmlAttributeId.LowSrc);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00071664 File Offset: 0x0006F864
		protected static bool IsSanitizingAttribute(HtmlTagContextAttribute attribute)
		{
			return attribute.Id == HtmlAttributeId.Border || attribute.Id == HtmlAttributeId.Width || attribute.Id == HtmlAttributeId.Height;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00071698 File Offset: 0x0006F898
		protected static bool IsTargetTagInAnchor(HtmlTagId tagId, HtmlTagContextAttribute attr)
		{
			return (tagId == HtmlTagId.A || tagId == HtmlTagId.Area) && attr.Id == HtmlAttributeId.Target;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000716C0 File Offset: 0x0006F8C0
		protected static bool IsUrlTag(HtmlTagId tagId, HtmlTagContextAttribute attribute)
		{
			return (tagId == HtmlTagId.A || tagId == HtmlTagId.Area) && attribute.Id == HtmlAttributeId.Href;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000716E8 File Offset: 0x0006F8E8
		protected static SafeHtmlCallback.TypeOfUrl GetTypeOfUrl(string urlString, HtmlAttributeId htmlAttr)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return SafeHtmlCallback.TypeOfUrl.Unknown;
			}
			if (urlString.StartsWith("#", StringComparison.Ordinal))
			{
				return SafeHtmlCallback.TypeOfUrl.Local;
			}
			Uri uri;
			if (!Uri.TryCreate(urlString, UriKind.Absolute, out uri))
			{
				return SafeHtmlCallback.TypeOfUrl.Unknown;
			}
			string scheme = uri.Scheme;
			if (string.IsNullOrEmpty(scheme))
			{
				return SafeHtmlCallback.TypeOfUrl.Unknown;
			}
			if (string.Equals(scheme, "file", StringComparison.OrdinalIgnoreCase) && htmlAttr == HtmlAttributeId.Href)
			{
				return SafeHtmlCallback.TypeOfUrl.Trusted;
			}
			for (int i = 0; i < SafeHtmlCallback.redirProtocols.Length; i++)
			{
				if (string.Equals(scheme, SafeHtmlCallback.redirProtocols[i], StringComparison.OrdinalIgnoreCase))
				{
					return SafeHtmlCallback.TypeOfUrl.Redirection;
				}
			}
			return SafeHtmlCallback.TypeOfUrl.Trusted;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00071768 File Offset: 0x0006F968
		protected static bool IsSafeUrl(string urlString, HtmlAttributeId htmlAttr)
		{
			SafeHtmlCallback.TypeOfUrl typeOfUrl = SafeHtmlCallback.GetTypeOfUrl(urlString, htmlAttr);
			return typeOfUrl != SafeHtmlCallback.TypeOfUrl.Unknown;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00071784 File Offset: 0x0006F984
		protected static void SanitizeImage(HtmlWriter writer, string border, string height, string width)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag = true;
			if (border != null)
			{
				flag = int.TryParse(border, NumberStyles.Number, CultureInfo.InvariantCulture, out num);
			}
			if (flag)
			{
				flag = int.TryParse(height, NumberStyles.Number, CultureInfo.InvariantCulture, out num2);
			}
			if (flag)
			{
				flag = int.TryParse(width, NumberStyles.Number, CultureInfo.InvariantCulture, out num3);
			}
			if (num < 0 || num2 < 0 || num3 < 0)
			{
				return;
			}
			if (flag)
			{
				if (num > 1)
				{
					num3 = num3 + num - 1;
					num2 = num2 + num - 1;
				}
				writer.WriteAttribute(HtmlAttributeId.Border, "1");
				if (num3 >= 0)
				{
					writer.WriteAttribute(HtmlAttributeId.Width, num3.ToString(CultureInfo.InvariantCulture));
				}
				if (num2 >= 0)
				{
					writer.WriteAttribute(HtmlAttributeId.Height, num2.ToString(CultureInfo.InvariantCulture));
					return;
				}
			}
			else
			{
				if (border != null)
				{
					writer.WriteAttribute(HtmlAttributeId.Border, border);
				}
				writer.WriteAttribute(HtmlAttributeId.Width, width);
				writer.WriteAttribute(HtmlAttributeId.Height, height);
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0007184E File Offset: 0x0006FA4E
		protected AttachmentLink IsInlineReference(string bodyRef)
		{
			if (this.baseRef != null)
			{
				return base.FindAttachmentByBodyReference(bodyRef, this.baseRef);
			}
			return base.FindAttachmentByBodyReference(bodyRef);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00071873 File Offset: 0x0006FA73
		protected AttachmentPolicy.Level GetAttachmentLevel(AttachmentLink link)
		{
			if (link.AttachmentType == AttachmentType.Stream || link.AttachmentType == AttachmentType.Ole)
			{
				this.isembeddedItem = true;
				return AttachmentPolicy.Level.Allow;
			}
			return AttachmentPolicy.Level.Block;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00071894 File Offset: 0x0006FA94
		protected void OutputInlineReference(HtmlTagContextAttribute filterAttribute, AttachmentLink link, HtmlWriter writer)
		{
			AttachmentPolicy.Level attachmentLevel = this.GetAttachmentLevel(link);
			if (AttachmentPolicy.Level.Allow != attachmentLevel)
			{
				this.hasBlockedImagesInCurrentPass = true;
				this.hasBlockedInlineAttachments = true;
				writer.WriteAttribute(filterAttribute.Id, "  ");
				return;
			}
			if (!this.isembeddedItem)
			{
				StringBuilder stringBuilder = new StringBuilder("/Microsoft-Server-ActiveSync?Cmd=GetAttachment&AttachmentName=");
				int num = 0;
				foreach (AttachmentLink attachmentLink in base.AttachmentLinks)
				{
					if (link.AttachmentId == attachmentLink.AttachmentId)
					{
						break;
					}
					num++;
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					this.itemId.ToBase64String(),
					num
				});
				writer.WriteAttribute(filterAttribute.Id, stringBuilder.ToString());
				return;
			}
			filterAttribute.WriteName();
			writer.WriteAttributeValue("cid:" + this.GetOrGenerateAttachContentId(link));
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000719A0 File Offset: 0x0006FBA0
		protected void ProcessHtmlUrlTag(HtmlTagContextAttribute filterAttribute, HtmlWriter writer)
		{
			string value = filterAttribute.Value;
			AttachmentLink attachmentLink = this.IsInlineReference(value);
			if (attachmentLink != null)
			{
				this.OutputInlineReference(filterAttribute, attachmentLink, writer);
				return;
			}
			SafeHtmlCallback.TypeOfUrl typeOfUrl = SafeHtmlCallback.GetTypeOfUrl(filterAttribute.Value, filterAttribute.Id);
			if (typeOfUrl == SafeHtmlCallback.TypeOfUrl.Redirection)
			{
				filterAttribute.Write();
				this.hasFoundRedirUrlInCurrentPass = true;
				return;
			}
			if (typeOfUrl == SafeHtmlCallback.TypeOfUrl.Trusted || typeOfUrl == SafeHtmlCallback.TypeOfUrl.Local)
			{
				filterAttribute.Write();
				return;
			}
			if (typeOfUrl == SafeHtmlCallback.TypeOfUrl.Unknown)
			{
				writer.WriteAttribute(filterAttribute.Id, "  ");
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00071A18 File Offset: 0x0006FC18
		protected void ProcessImageTag(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
			AirSyncDiagnostics.Assert(context != null);
			string value = filterAttribute.Value;
			AttachmentLink attachmentLink = this.IsInlineReference(value);
			if (attachmentLink != null)
			{
				this.OutputInlineReference(filterAttribute, attachmentLink, writer);
				return;
			}
			if (SafeHtmlCallback.IsSafeUrl(filterAttribute.Value, filterAttribute.Id))
			{
				filterAttribute.Write();
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00071A6C File Offset: 0x0006FC6C
		private string GetOrGenerateAttachContentId(AttachmentLink attachmentLink)
		{
			StoreObjectId key = this.itemId;
			if (this.existingContentIds == null && !Command.CurrentCommand.InlineAttachmentContentIdLookUp.TryGetValue(key, out this.existingContentIds))
			{
				this.existingContentIds = new Dictionary<AttachmentId, string>(1);
				Command.CurrentCommand.InlineAttachmentContentIdLookUp[key] = this.existingContentIds;
			}
			string text;
			if (this.existingContentIds.TryGetValue(attachmentLink.AttachmentId, out text) && !string.IsNullOrEmpty(text))
			{
				attachmentLink.ContentId = text;
			}
			else
			{
				if (string.IsNullOrEmpty(attachmentLink.ContentId))
				{
					attachmentLink.ContentId = Guid.NewGuid().ToString();
				}
				this.existingContentIds[attachmentLink.AttachmentId] = attachmentLink.ContentId;
			}
			attachmentLink.MarkInline(true);
			return attachmentLink.ContentId;
		}

		// Token: 0x04000C2F RID: 3119
		private const string AttachmentBaseUrl = "/Microsoft-Server-ActiveSync?Cmd=GetAttachment&AttachmentName=";

		// Token: 0x04000C30 RID: 3120
		private const string DoubleBlank = "  ";

		// Token: 0x04000C31 RID: 3121
		private const string LocalUrlPrefix = "#";

		// Token: 0x04000C32 RID: 3122
		private const string TargetValue = "_BLANK";

		// Token: 0x04000C33 RID: 3123
		private static readonly string[] redirProtocols = new string[]
		{
			"http",
			"https",
			"mhtml"
		};

		// Token: 0x04000C34 RID: 3124
		private static List<HtmlAttributeId> metaTagAttributes = new List<HtmlAttributeId>
		{
			HtmlAttributeId.ItemProp,
			HtmlAttributeId.Content
		};

		// Token: 0x04000C35 RID: 3125
		private static List<HtmlAttributeId> linkTagAttributes = new List<HtmlAttributeId>
		{
			HtmlAttributeId.ItemProp,
			HtmlAttributeId.Href
		};

		// Token: 0x04000C36 RID: 3126
		private Uri baseRef;

		// Token: 0x04000C37 RID: 3127
		private bool hasBlockedImagesInCurrentPass;

		// Token: 0x04000C38 RID: 3128
		private bool hasBlockedInlineAttachments;

		// Token: 0x04000C39 RID: 3129
		private bool hasFoundRedirUrlInCurrentPass;

		// Token: 0x04000C3A RID: 3130
		private bool isembeddedItem;

		// Token: 0x04000C3B RID: 3131
		private Dictionary<AttachmentId, string> existingContentIds;

		// Token: 0x04000C3C RID: 3132
		private StoreObjectId itemId;

		// Token: 0x020001F7 RID: 503
		protected enum TypeOfUrl
		{
			// Token: 0x04000C3E RID: 3134
			Local = 1,
			// Token: 0x04000C3F RID: 3135
			Trusted,
			// Token: 0x04000C40 RID: 3136
			Redirection,
			// Token: 0x04000C41 RID: 3137
			Unknown
		}
	}
}
