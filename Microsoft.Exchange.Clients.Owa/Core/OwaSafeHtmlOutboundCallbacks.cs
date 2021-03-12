using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001FA RID: 506
	internal class OwaSafeHtmlOutboundCallbacks : OwaSafeHtmlCallbackBase
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x00065D5A File Offset: 0x00063F5A
		public OwaSafeHtmlOutboundCallbacks(OwaContext owaContext, bool isEditableContent)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			this.owaContext = owaContext;
			this.allowForms = isEditableContent;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00065D85 File Offset: 0x00063F85
		public OwaSafeHtmlOutboundCallbacks(OwaContext owaContext, bool openMailtoInNewWindow, bool isEditableContent)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			this.owaContext = owaContext;
			this.openMailtoInNewWindow = openMailtoInNewWindow;
			this.allowForms = isEditableContent;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00065DB7 File Offset: 0x00063FB7
		public OwaSafeHtmlOutboundCallbacks(Item item, bool userLogon, bool isJunkOrPhishing, OwaContext owaContext, bool isEditableContent) : this(item, userLogon, false, null, isJunkOrPhishing, owaContext, isEditableContent)
		{
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00065DC8 File Offset: 0x00063FC8
		public OwaSafeHtmlOutboundCallbacks(Item item, bool userLogon, bool isEmbedded, string itemUrl, bool isJunkOrPhishing, OwaContext owaContext, bool isEditableContent) : base(OwaSafeHtmlOutboundCallbacks.GetAttachmentCollection(item, owaContext.UserContext), item.Body)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			this.owaContext = owaContext;
			this.isLoggedOnFromPublicComputer = userLogon;
			this.isEmbeddedItem = isEmbedded;
			this.embeddedItemUrl = itemUrl;
			this.isJunkOrPhishing = isJunkOrPhishing;
			this.isOutputFragment = owaContext.UserContext.IsBasicExperience;
			this.allowForms = isEditableContent;
			if (item.Id != null)
			{
				this.itemId = item.Id.ObjectId;
			}
			this.parentId = (item.TryGetProperty(StoreObjectSchema.ParentItemId) as StoreObjectId);
			this.objectClass = item.ClassName;
			if (base.ItemBody != null)
			{
				this.charSet = base.ItemBody.Charset;
			}
			if (Utilities.IsOtherMailbox(item) || Utilities.IsInArchiveMailbox(item))
			{
				this.legacyDN = Utilities.GetMailboxSessionLegacyDN(item);
			}
			else
			{
				this.legacyDN = owaContext.UserContext.MailboxOwnerLegacyDN;
			}
			if (Utilities.IsOtherMailbox(item))
			{
				this.owaStoreObjectIdType = OwaStoreObjectIdType.OtherUserMailboxObject;
			}
			else if (Utilities.IsInArchiveMailbox(item))
			{
				this.owaStoreObjectIdType = OwaStoreObjectIdType.ArchiveMailboxObject;
			}
			else if (Utilities.IsPublic(item))
			{
				this.owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreItem;
			}
			this.bodyFormat = base.ItemBody.Format;
			string queryStringParameter = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "t", false);
			this.isConversations = (!string.IsNullOrEmpty(queryStringParameter) && string.Equals("IPM.Conversation", queryStringParameter, StringComparison.Ordinal));
			this.isConversationsOrUnknownType = (this.isConversations || queryStringParameter == null);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00065F54 File Offset: 0x00064154
		private static AttachmentCollection GetAttachmentCollection(Item item, UserContext userContext)
		{
			AttachmentCollection attachmentCollection = null;
			if (Utilities.IsSMimeButNotSecureSign(item))
			{
				Item item2 = Utilities.OpenSMimeContent(item);
				if (item2 != null)
				{
					attachmentCollection = item2.AttachmentCollection;
				}
			}
			else if (userContext.IsIrmEnabled && !userContext.IsBasicExperience && Utilities.IsIrmDecrypted(item))
			{
				attachmentCollection = ((RightsManagedMessageItem)item).ProtectedAttachmentCollection;
			}
			if (attachmentCollection != null)
			{
				return attachmentCollection;
			}
			return item.AttachmentCollection;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00065FAD File Offset: 0x000641AD
		public virtual bool HasBlockedForms
		{
			get
			{
				return this.hasBlockedForms;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00065FB5 File Offset: 0x000641B5
		public virtual bool HasInlineImages
		{
			get
			{
				return this.hasInlineImages;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00065FC0 File Offset: 0x000641C0
		public override void ProcessTag(HtmlTagContext context, HtmlWriter writer)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			AttachmentLink attachmentLink = null;
			if (context.TagId == HtmlTagId.Link)
			{
				context.DeleteTag();
				return;
			}
			if (context.TagId == HtmlTagId.Head)
			{
				if (this.owaContext.UserContext.IsBasicExperience || this.bodyFormat != BodyFormat.TextPlain)
				{
					context.WriteTag(true);
					return;
				}
				if (!context.IsEndTag)
				{
					context.WriteTag(true);
					context.InvokeCallbackForEndTag();
					return;
				}
				writer.WriteStartTag(HtmlTagId.Style);
				writer.WriteText("div.PlainText ");
				if (ObjectClass.IsSmsMessage(this.objectClass))
				{
					writer.WriteText(OwaPlainTextStyle.GetStyleFromUserOption(this.owaContext.UserContext.UserOptions));
				}
				else
				{
					writer.WriteText(OwaPlainTextStyle.GetStyleFromCharset(this.charSet));
				}
				writer.WriteEndTag(HtmlTagId.Style);
				context.WriteTag(true);
				return;
			}
			else
			{
				if (this.isOutputFragment && context.TagId == HtmlTagId.Form)
				{
					context.DeleteTag();
					return;
				}
				if (context.TagId == HtmlTagId.Form || (!this.isOutputFragment && OwaSafeHtmlOutboundCallbacks.IsFormElementTag(context.TagId)))
				{
					this.ProcessUnfragFormTagContext(context, writer);
					return;
				}
				if (context.TagId == HtmlTagId.Base)
				{
					foreach (HtmlTagContextAttribute attribute in context.Attributes)
					{
						if (OwaSafeHtmlCallbackBase.IsBaseTag(context.TagId, attribute))
						{
							string value = attribute.Value;
							this.baseRef = Utilities.TryParseUri(value);
							break;
						}
					}
					return;
				}
				foreach (HtmlTagContextAttribute filterAttribute in context.Attributes)
				{
					if (filterAttribute.Id == HtmlAttributeId.Src || filterAttribute.Id == HtmlAttributeId.Href)
					{
						if (context.TagId == HtmlTagId.Img && string.IsNullOrEmpty(filterAttribute.Value))
						{
							return;
						}
						if (string.CompareOrdinal(filterAttribute.Value, this.inlineRTFattachmentScheme) == 0 || filterAttribute.Value.StartsWith(this.inlineHTMLAttachmentScheme, StringComparison.OrdinalIgnoreCase))
						{
							attachmentLink = this.IsInlineImage(filterAttribute);
							if (attachmentLink == null)
							{
								return;
							}
							if (context.TagId != HtmlTagId.Img)
							{
								writer.WriteEmptyElementTag(HtmlTagId.Img);
								this.OutputInlineReference(filterAttribute, context, attachmentLink, writer);
								context.DeleteTag(false);
								context.DeleteInnerContent();
								return;
							}
							flag2 = true;
							break;
						}
						else
						{
							if (string.CompareOrdinal(filterAttribute.Value, this.embeddedRTFImage) == 0)
							{
								this.hasRtfEmbeddedImages = true;
								break;
							}
							break;
						}
					}
				}
				context.WriteTag();
				foreach (HtmlTagContextAttribute htmlTagContextAttribute in context.Attributes)
				{
					if (!this.isOutputFragment || htmlTagContextAttribute.Id != HtmlAttributeId.Name || !OwaSafeHtmlOutboundCallbacks.IsFormElementTag(context.TagId))
					{
						if (htmlTagContextAttribute.Id == HtmlAttributeId.UseMap)
						{
							this.ProcessUseMapAttribute(htmlTagContextAttribute, context, writer);
						}
						else if (OwaSafeHtmlCallbackBase.IsUrlTag(context.TagId, htmlTagContextAttribute))
						{
							if (!flag)
							{
								this.ProcessHtmlUrlTag(htmlTagContextAttribute, context, writer);
								flag = true;
							}
						}
						else if (OwaSafeHtmlCallbackBase.IsImageTag(context.TagId, htmlTagContextAttribute))
						{
							if (htmlTagContextAttribute.Id == HtmlAttributeId.Src && attachmentLink != null)
							{
								this.OutputInlineReference(htmlTagContextAttribute, context, attachmentLink, writer);
							}
							else
							{
								if ((htmlTagContextAttribute.Id != HtmlAttributeId.Src || flag2) && (htmlTagContextAttribute.Id != HtmlAttributeId.DynSrc || flag3) && (htmlTagContextAttribute.Id != HtmlAttributeId.LowSrc || flag4))
								{
									continue;
								}
								this.ProcessImageTag(htmlTagContextAttribute, context, writer);
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
						else if (OwaSafeHtmlCallbackBase.IsBackgroundAttribute(htmlTagContextAttribute))
						{
							if (this.isOutputFragment && context.TagId == HtmlTagId.Div)
							{
								attachmentLink = this.IsInlineImage(htmlTagContextAttribute);
								if (attachmentLink != null)
								{
									AttachmentPolicy.Level attachmentLevel = this.GetAttachmentLevel(attachmentLink);
									if (AttachmentPolicy.Level.Allow == attachmentLevel)
									{
										writer.WriteAttribute(HtmlAttributeId.Style, "background:url('" + this.GetInlineReferenceUrl(attachmentLevel, attachmentLink, writer) + "');");
									}
								}
							}
							else
							{
								this.ProcessImageTag(htmlTagContextAttribute, context, writer);
							}
						}
						else if (!OwaSafeHtmlOutboundCallbacks.IsTargetTagInAnchor(context.TagId, htmlTagContextAttribute))
						{
							if (OwaSafeHtmlCallbackBase.IsSanitizingAttribute(htmlTagContextAttribute))
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
					OwaSafeHtmlOutboundCallbacks.SanitizeImage(writer, text, text2, text3);
				}
				else if (!this.hasBlockedImagesInCurrentPass)
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
				if (this.hasFoundNonLocalUrlInCurrentPass)
				{
					if (this.owaContext.UserContext.IsBasicExperience)
					{
						if (!this.hasFoundMailToUrlInCurrentPass || this.openMailtoInNewWindow)
						{
							this.WriteSafeTargetBlank(writer);
						}
					}
					else
					{
						this.WriteSafeTargetBlank(writer);
					}
				}
				this.hasBlockedImagesInCurrentPass = false;
				this.hasFoundNonLocalUrlInCurrentPass = false;
				this.hasFoundMailToUrlInCurrentPass = false;
				return;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00066554 File Offset: 0x00064754
		protected AttachmentLink IsInlineImage(HtmlTagContextAttribute filterAttribute)
		{
			AttachmentLink attachmentLink = this.IsInlineReference(filterAttribute.Value);
			if (attachmentLink != null)
			{
				string text;
				if (attachmentLink.AttachmentType == AttachmentType.Ole)
				{
					text = "image/jpeg";
				}
				else
				{
					text = attachmentLink.ContentType;
					if (string.Compare(text, "image/tiff", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return null;
					}
				}
				if (text.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
				{
					this.hasInlineImages = true;
					return attachmentLink;
				}
			}
			return null;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000665B4 File Offset: 0x000647B4
		protected virtual void ProcessImageTag(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
			AttachmentLink attachmentLink = this.IsInlineImage(filterAttribute);
			if (attachmentLink != null)
			{
				this.OutputInlineReference(filterAttribute, context, attachmentLink, writer);
				return;
			}
			if (this.IsSafeUrl(filterAttribute.Value, filterAttribute.Id))
			{
				this.hasBlockedImagesInCurrentPass = true;
				this.hasBlockedImages = true;
				writer.WriteAttribute(filterAttribute.Id, OwaSafeHtmlCallbackBase.blankImageFileName);
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00066610 File Offset: 0x00064810
		protected virtual void ProcessUnfragFormTagContext(HtmlTagContext context, HtmlWriter writer)
		{
			if (this.allowForms)
			{
				context.WriteTag();
				foreach (HtmlTagContextAttribute htmlTagContextAttribute in context.Attributes)
				{
					if ((htmlTagContextAttribute.Id == HtmlAttributeId.Src || htmlTagContextAttribute.Id == HtmlAttributeId.Action) && (!this.IsSafeUrl(htmlTagContextAttribute.Value, htmlTagContextAttribute.Id) || !Redir.IsSafeUrl(htmlTagContextAttribute.Value, this.owaContext.HttpContext.Request)))
					{
						writer.WriteAttribute(htmlTagContextAttribute.Id, OwaSafeHtmlOutboundCallbacks.BlockedUrlPageValue);
					}
					else if (htmlTagContextAttribute.Id != HtmlAttributeId.Target)
					{
						htmlTagContextAttribute.Write();
					}
				}
				this.WriteSafeTargetBlank(writer);
				return;
			}
			this.hasBlockedForms = true;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000666F0 File Offset: 0x000648F0
		private void WriteSafeTargetBlank(HtmlWriter writer)
		{
			writer.WriteAttribute(HtmlAttributeId.Target, "_blank");
			writer.WriteAttribute(HtmlAttributeId.Rel, "noopener noreferrer");
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0006670C File Offset: 0x0006490C
		protected void ProcessHtmlUrlTag(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
			OwaSafeHtmlOutboundCallbacks.TypeOfUrl typeOfUrl = this.GetTypeOfUrl(filterAttribute.Value, filterAttribute.Id);
			string text;
			if (typeOfUrl == OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Unknown || typeOfUrl == OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Local)
			{
				if (this.baseRef == null && this.isConversationsOrUnknownType && !this.triedLoadingBaseHref)
				{
					OwaLightweightHtmlCallback owaLightweightHtmlCallback = new OwaLightweightHtmlCallback();
					using (Item item = Utilities.GetItem<Item>(this.owaContext.UserContext, this.itemId, new PropertyDefinition[0]))
					{
						BodyReadConfiguration bodyReadConfiguration = new BodyReadConfiguration(BodyFormat.TextHtml, "utf-8");
						bodyReadConfiguration.SetHtmlOptions(HtmlStreamingFlags.FilterHtml, owaLightweightHtmlCallback);
						Body body = item.Body;
						if (this.owaContext.UserContext.IsIrmEnabled)
						{
							Utilities.IrmDecryptIfRestricted(item, this.owaContext.UserContext, true);
							if (Utilities.IsIrmRestrictedAndDecrypted(item))
							{
								body = ((RightsManagedMessageItem)item).ProtectedBody;
							}
						}
						using (TextReader textReader = body.OpenTextReader(bodyReadConfiguration))
						{
							int num = 5000;
							char[] buffer = new char[num];
							textReader.Read(buffer, 0, num);
						}
					}
					this.baseRef = owaLightweightHtmlCallback.BaseRef;
					this.triedLoadingBaseHref = true;
				}
				text = this.GetAbsoluteUrl(filterAttribute.Value, filterAttribute.Id);
				typeOfUrl = this.GetTypeOfUrl(text, filterAttribute.Id);
			}
			else
			{
				text = filterAttribute.Value;
			}
			switch (typeOfUrl)
			{
			case OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Local:
				if (this.owaContext.UserContext.BrowserType != BrowserType.Safari && !this.owaContext.UserContext.IsBasicExperience && !this.isConversations)
				{
					writer.WriteAttribute(filterAttribute.Id, OwaSafeHtmlCallbackBase.JSLocalLink + OwaSafeHtmlCallbackBase.JSMethodPrefix + filterAttribute.Value.Substring(1) + OwaSafeHtmlCallbackBase.JSMethodSuffix);
					return;
				}
				filterAttribute.Write();
				return;
			case OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Trusted:
				filterAttribute.Write();
				this.hasFoundNonLocalUrlInCurrentPass = true;
				return;
			case OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Redirection:
				filterAttribute.WriteName();
				writer.WriteAttributeValue(Redir.BuildRedirUrl(this.owaContext.UserContext, text));
				this.hasFoundNonLocalUrlInCurrentPass = true;
				return;
			case OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Unknown:
				writer.WriteAttribute(filterAttribute.Id, OwaSafeHtmlOutboundCallbacks.BlockedUrlPageValue);
				this.hasFoundNonLocalUrlInCurrentPass = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0006694C File Offset: 0x00064B4C
		protected virtual void ProcessUseMapAttribute(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00066950 File Offset: 0x00064B50
		protected void OutputInlineReference(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, AttachmentLink imageAttachmentLink, HtmlWriter writer)
		{
			AttachmentPolicy.Level attachmentLevel = this.GetAttachmentLevel(imageAttachmentLink);
			if (AttachmentPolicy.Level.Allow == attachmentLevel && filterAttribute.Id == HtmlAttributeId.Href)
			{
				writer.WriteAttribute(HtmlAttributeId.Src, this.GetInlineReferenceUrl(attachmentLevel, imageAttachmentLink, writer));
				return;
			}
			string value;
			if (this.owaContext.ShouldDeferInlineImages)
			{
				value = this.owaContext.UserContext.GetThemeFileUrl(ThemeFileId.Clear1x1) + "#" + OwaSafeHtmlOutboundCallbacks.DeferImageUrlDelimiter + this.GetInlineReferenceUrl(attachmentLevel, imageAttachmentLink, writer);
			}
			else
			{
				value = this.GetInlineReferenceUrl(attachmentLevel, imageAttachmentLink, writer);
			}
			writer.WriteAttribute(filterAttribute.Id, value);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000669E0 File Offset: 0x00064BE0
		private string GetInlineReferenceUrl(AttachmentPolicy.Level level, AttachmentLink imageAttachmentLink, HtmlWriter writer)
		{
			imageAttachmentLink.MarkInline(true);
			if (AttachmentPolicy.Level.Allow == level)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.isEmbeddedItem)
				{
					stringBuilder.Append(this.embeddedItemUrl);
				}
				else
				{
					stringBuilder.Append(OwaSafeHtmlCallbackBase.AttachmentBaseUrl);
					bool flag = this.owaStoreObjectIdType == OwaStoreObjectIdType.OtherUserMailboxObject || this.owaStoreObjectIdType == OwaStoreObjectIdType.ArchiveMailboxObject;
					OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromItemId(this.itemId, flag ? null : this.parentId, this.owaStoreObjectIdType, this.legacyDN);
					stringBuilder.Append(Utilities.UrlEncode(owaStoreObjectId.ToString()));
					stringBuilder.Append("&attcnt=1&attid0=");
				}
				stringBuilder.Append(Utilities.UrlEncode(imageAttachmentLink.AttachmentId.ToBase64String()));
				if (!this.isEmbeddedItem && !string.IsNullOrEmpty(imageAttachmentLink.ContentId))
				{
					stringBuilder.Append("&attcid0=");
					stringBuilder.Append(Utilities.UrlEncode(imageAttachmentLink.ContentId));
				}
				return stringBuilder.ToString();
			}
			this.hasBlockedImagesInCurrentPass = true;
			this.hasBlockedInlineAttachments = true;
			return OwaSafeHtmlOutboundCallbacks.BlockedUrlPageValue;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00066AE0 File Offset: 0x00064CE0
		protected AttachmentLink IsInlineReference(string bodyRef)
		{
			if (this.baseRef != null)
			{
				return base.FindAttachmentByBodyReference(bodyRef, this.baseRef);
			}
			return base.FindAttachmentByBodyReference(bodyRef);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00066B05 File Offset: 0x00064D05
		protected AttachmentPolicy.Level GetAttachmentLevel(AttachmentLink attachmentLink)
		{
			if (this.isJunkOrPhishing)
			{
				return AttachmentPolicy.Level.Block;
			}
			if (attachmentLink.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				return AttachmentPolicy.Level.Block;
			}
			return AttachmentLevelLookup.GetAttachmentLevel(attachmentLink, this.owaContext.UserContext);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00066B30 File Offset: 0x00064D30
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
					writer.WriteAttribute(HtmlAttributeId.Width, num3.ToString());
				}
				if (num2 >= 0)
				{
					writer.WriteAttribute(HtmlAttributeId.Height, num2.ToString());
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

		// Token: 0x060010BA RID: 4282 RVA: 0x00066BF0 File Offset: 0x00064DF0
		protected bool IsSafeUrl(string urlString, HtmlAttributeId htmlAttrId)
		{
			string absoluteUrl = this.GetAbsoluteUrl(urlString, htmlAttrId);
			OwaSafeHtmlOutboundCallbacks.TypeOfUrl typeOfUrl = this.GetTypeOfUrl(absoluteUrl, htmlAttrId);
			return typeOfUrl != OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Unknown;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00066C18 File Offset: 0x00064E18
		protected OwaSafeHtmlOutboundCallbacks.TypeOfUrl GetTypeOfUrl(string urlString, HtmlAttributeId htmlAttrId)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Unknown;
			}
			if (urlString.StartsWith(OwaSafeHtmlCallbackBase.LocalUrlPrefix, StringComparison.Ordinal))
			{
				return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Local;
			}
			Uri uri;
			if (null == (uri = Utilities.TryParseUri(urlString)))
			{
				return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Unknown;
			}
			string scheme = uri.Scheme;
			if (string.IsNullOrEmpty(scheme))
			{
				return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Unknown;
			}
			if (CultureInfo.InvariantCulture.CompareInfo.Compare(scheme, "file", CompareOptions.IgnoreCase) == 0 && htmlAttrId == HtmlAttributeId.Href)
			{
				return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Redirection;
			}
			for (int i = 0; i < OwaSafeHtmlOutboundCallbacks.RedirProtocols.Length; i++)
			{
				if (CultureInfo.InvariantCulture.CompareInfo.Compare(scheme, OwaSafeHtmlOutboundCallbacks.RedirProtocols[i], CompareOptions.IgnoreCase) == 0)
				{
					if (CultureInfo.InvariantCulture.CompareInfo.Compare(scheme, "mailto", CompareOptions.IgnoreCase) == 0 && htmlAttrId == HtmlAttributeId.Href)
					{
						this.hasFoundMailToUrlInCurrentPass = true;
					}
					return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Redirection;
				}
			}
			return OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Trusted;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00066CD8 File Offset: 0x00064ED8
		protected string GetAbsoluteUrl(string urlString, HtmlAttributeId htmlAttributeId)
		{
			if (urlString == null)
			{
				return null;
			}
			Uri uri = Utilities.TryParseUri(urlString, UriKind.RelativeOrAbsolute);
			if (uri == null)
			{
				return null;
			}
			if (!uri.IsAbsoluteUri && this.baseRef != null)
			{
				Uri uri2;
				Uri.TryCreate(this.baseRef, uri, out uri2);
				if (uri2 != null)
				{
					return uri2.AbsoluteUri;
				}
			}
			else if (uri.IsAbsoluteUri || this.GetTypeOfUrl(urlString, htmlAttributeId) == OwaSafeHtmlOutboundCallbacks.TypeOfUrl.Local)
			{
				return uri.OriginalString;
			}
			return null;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00066D4D File Offset: 0x00064F4D
		protected static bool IsTargetTagInAnchor(HtmlTagId tagId, HtmlTagContextAttribute attr)
		{
			return (tagId == HtmlTagId.A && attr.Id == HtmlAttributeId.Target) || (tagId == HtmlTagId.Area && attr.Id == HtmlAttributeId.Target);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00066D71 File Offset: 0x00064F71
		protected static bool IsFormElementTag(HtmlTagId tagId)
		{
			return tagId == HtmlTagId.Input || tagId == HtmlTagId.Button || tagId == HtmlTagId.Select || tagId == HtmlTagId.OptGroup || tagId == HtmlTagId.Option || tagId == HtmlTagId.FieldSet || tagId == HtmlTagId.TextArea || tagId == HtmlTagId.IsIndex || tagId == HtmlTagId.Label || tagId == HtmlTagId.Legend;
		}

		// Token: 0x04000B51 RID: 2897
		private static readonly string DeferImageUrlDelimiter = "__OWA_DEFERIMG000__";

		// Token: 0x04000B52 RID: 2898
		private static readonly string BlockedUrlPageValue = "UrlBlockedError.aspx";

		// Token: 0x04000B53 RID: 2899
		protected bool hasBlockedForms;

		// Token: 0x04000B54 RID: 2900
		protected bool hasInlineImages;

		// Token: 0x04000B55 RID: 2901
		protected bool allowForms = true;

		// Token: 0x04000B56 RID: 2902
		protected bool hasBlockedImagesInCurrentPass;

		// Token: 0x04000B57 RID: 2903
		protected bool hasFoundNonLocalUrlInCurrentPass;

		// Token: 0x04000B58 RID: 2904
		protected bool hasFoundMailToUrlInCurrentPass;

		// Token: 0x04000B59 RID: 2905
		protected bool isLoggedOnFromPublicComputer;

		// Token: 0x04000B5A RID: 2906
		protected bool openMailtoInNewWindow;

		// Token: 0x04000B5B RID: 2907
		protected bool isOutputFragment;

		// Token: 0x04000B5C RID: 2908
		protected OwaContext owaContext;

		// Token: 0x04000B5D RID: 2909
		protected bool isEmbeddedItem;

		// Token: 0x04000B5E RID: 2910
		protected string embeddedItemUrl;

		// Token: 0x04000B5F RID: 2911
		protected Uri baseRef;

		// Token: 0x04000B60 RID: 2912
		public static readonly string[] RedirProtocols = new string[]
		{
			"http",
			"https",
			"mailto",
			"mhtml"
		};

		// Token: 0x04000B61 RID: 2913
		private bool isJunkOrPhishing;

		// Token: 0x04000B62 RID: 2914
		private StoreObjectId itemId;

		// Token: 0x04000B63 RID: 2915
		private StoreObjectId parentId;

		// Token: 0x04000B64 RID: 2916
		private string objectClass;

		// Token: 0x04000B65 RID: 2917
		private string charSet;

		// Token: 0x04000B66 RID: 2918
		private BodyFormat bodyFormat;

		// Token: 0x04000B67 RID: 2919
		private OwaStoreObjectIdType owaStoreObjectIdType;

		// Token: 0x04000B68 RID: 2920
		private string legacyDN;

		// Token: 0x04000B69 RID: 2921
		private bool isConversations;

		// Token: 0x04000B6A RID: 2922
		private bool isConversationsOrUnknownType;

		// Token: 0x04000B6B RID: 2923
		private bool triedLoadingBaseHref;

		// Token: 0x020001FB RID: 507
		protected enum TypeOfUrl
		{
			// Token: 0x04000B6D RID: 2925
			Local = 1,
			// Token: 0x04000B6E RID: 2926
			Trusted,
			// Token: 0x04000B6F RID: 2927
			Redirection,
			// Token: 0x04000B70 RID: 2928
			Unknown
		}
	}
}
