using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000158 RID: 344
	internal class HtmlBodyCallback : HtmlCallbackBase
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x0002E283 File Offset: 0x0002C483
		static HtmlBodyCallback()
		{
			HtmlBodyCallback.WellKnownInlineImageOnLoadTemplates.Add("InlineImageLoader.GetLoader().Load(this)");
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public HtmlBodyCallback(Item item, IdAndSession idAndSession = null, bool getAttachmentCollectionWhenClientSmimeInstalled = false) : base(Util.GetEffectiveAttachmentCollection(item, getAttachmentCollectionWhenClientSmimeInstalled), Util.GetEffectiveBody(item))
		{
			if (idAndSession == null)
			{
				if (PropertyCommand.InMemoryProcessOnly)
				{
					this.cachedItem = item;
				}
				else
				{
					this.idAndSession = IdAndSession.CreateFromItem(item);
				}
			}
			else
			{
				this.idAndSession = idAndSession;
			}
			this.BlockedUrlPageValue = string.Empty;
			this.BlockUnsafeImages = true;
			base.InitializeAttachmentLinks(null);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002E304 File Offset: 0x0002C504
		public override void ProcessTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			HtmlTagId tagId = tagContext.TagId;
			if (tagId <= HtmlTagId.Base)
			{
				if (tagId == HtmlTagId.A)
				{
					this.ProcessAnchorTag(tagContext, htmlWriter);
					return;
				}
				switch (tagId)
				{
				case HtmlTagId.Area:
					this.ProcessAreaTag(tagContext, htmlWriter);
					return;
				case HtmlTagId.Base:
					this.ProcessBaseTag(tagContext, htmlWriter);
					return;
				}
			}
			else
			{
				if (tagId == HtmlTagId.Form)
				{
					this.ProcessFormTag(tagContext, htmlWriter);
					return;
				}
				switch (tagId)
				{
				case HtmlTagId.Img:
					this.ProcessImageTag(tagContext, htmlWriter);
					return;
				case HtmlTagId.Input:
					this.ProcessInputTag(tagContext, htmlWriter);
					return;
				}
			}
			tagContext.WriteTag(true);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002E38C File Offset: 0x0002C58C
		private static void WriteAllAttributesExcept(HtmlTagContext tagContext, HtmlAttributeId attrToSkip)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id != attrToSkip)
				{
					htmlTagContextAttribute.Write();
				}
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002E3EC File Offset: 0x0002C5EC
		private static void WriteAllAttributesExcept(HtmlTagContext tagContext, Func<HtmlAttributeId, bool> shouldSkipFunc)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (!shouldSkipFunc(htmlTagContextAttribute.Id))
				{
					htmlTagContextAttribute.Write();
				}
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002E454 File Offset: 0x0002C654
		private void ProcessAnchorTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			if (this.AddBlankTargetToLinks)
			{
				this.AddBlankTarget(tagContext, htmlWriter, HtmlAttributeId.Href);
				return;
			}
			tagContext.WriteTag(true);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002E48C File Offset: 0x0002C68C
		private void AddBlankTarget(HtmlTagContext tagContext, HtmlWriter htmlWriter, HtmlAttributeId anchorAttribute = HtmlAttributeId.Href)
		{
			string text;
			this.GetAnchorReference(tagContext, anchorAttribute, out text);
			string value = text;
			HtmlBodyCallback.TypeOfUrl typeOfUrl = HtmlBodyCallback.TypeOfUrl.Unknown;
			if (text != null)
			{
				value = this.GetAbsoluteUrl(text, this.BlockedUrlPageValue, HtmlAttributeId.Href, out typeOfUrl);
			}
			if (string.IsNullOrEmpty(value))
			{
				tagContext.DeleteTag();
				if (!string.IsNullOrEmpty(text))
				{
					htmlWriter.WriteText(string.Format("[{0}]", text));
					return;
				}
			}
			else
			{
				tagContext.WriteTag(false);
				htmlWriter.WriteAttribute(anchorAttribute, value);
				if (typeOfUrl != HtmlBodyCallback.TypeOfUrl.Local)
				{
					htmlWriter.WriteAttribute(HtmlAttributeId.Target, "_blank");
				}
				HtmlBodyCallback.WriteAllAttributesExcept(tagContext, (HtmlAttributeId id) => id == anchorAttribute || id == HtmlAttributeId.Target);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002E53C File Offset: 0x0002C73C
		private string GetAbsoluteUrl(string hrefValue, string blockedUrlValue, HtmlAttributeId attrId)
		{
			HtmlBodyCallback.TypeOfUrl typeOfUrl;
			return this.GetAbsoluteUrl(hrefValue, blockedUrlValue, attrId, out typeOfUrl);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002E554 File Offset: 0x0002C754
		private string GetAbsoluteUrl(string hrefValue, string blockedUrlValue, HtmlAttributeId attrId, out HtmlBodyCallback.TypeOfUrl urlType)
		{
			string text = hrefValue;
			urlType = HtmlBodyCallback.GetTypeOfUrl(hrefValue, attrId);
			if (urlType == HtmlBodyCallback.TypeOfUrl.Unknown && !string.IsNullOrEmpty(hrefValue))
			{
				if (this.baseRef == null && !this.triedToLoadBaseHref && this.IsBodyFragment)
				{
					this.TryToDetermineBaseRef();
				}
				text = HtmlBodyCallback.GetAbsoluteUrl(hrefValue, HtmlAttributeId.Href, this.baseRef);
				urlType = HtmlBodyCallback.GetTypeOfUrl(text, HtmlAttributeId.Href);
			}
			if (urlType == HtmlBodyCallback.TypeOfUrl.Unknown)
			{
				text = blockedUrlValue;
			}
			return text;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		private void TryToDetermineBaseRef()
		{
			HtmlBodyCallbackForBaseTag htmlBodyCallbackForBaseTag = new HtmlBodyCallbackForBaseTag();
			using (Item item = this.cachedItem ?? ServiceCommandBase.GetXsoItem(this.idAndSession.Session, this.idAndSession.Id, new PropertyDefinition[0]))
			{
				BodyReadConfiguration bodyReadConfiguration = new BodyReadConfiguration(BodyFormat.TextHtml, "utf-8");
				bodyReadConfiguration.SetHtmlOptions(HtmlStreamingFlags.FilterHtml, htmlBodyCallbackForBaseTag);
				Body effectiveBody = Util.GetEffectiveBody(item);
				using (TextReader textReader = effectiveBody.OpenTextReader(bodyReadConfiguration))
				{
					int num = 5000;
					char[] buffer = new char[num];
					textReader.Read(buffer, 0, num);
				}
			}
			this.baseRef = htmlBodyCallbackForBaseTag.BaseHref;
			this.triedToLoadBaseHref = true;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002E690 File Offset: 0x0002C890
		private void ProcessImageTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			AttachmentLink attachmentLink;
			string srcValue;
			this.GetAttachmentLinkAndLinkSource(tagContext, out attachmentLink, out srcValue);
			if (attachmentLink == null)
			{
				this.ProcessExternalImageRef(tagContext, htmlWriter, srcValue, HtmlAttributeId.Src);
				return;
			}
			this.ProcessInlineImageRef(tagContext, htmlWriter, attachmentLink, srcValue);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002E6C4 File Offset: 0x0002C8C4
		private void ProcessInlineImageRef(HtmlTagContext tagContext, HtmlWriter htmlWriter, AttachmentLink link, string srcValue)
		{
			tagContext.WriteTag(false);
			if (link.IsOriginallyInline)
			{
				link.MarkInline(true);
			}
			else
			{
				if (srcValue != null && srcValue.StartsWith("objattph://"))
				{
					link.MarkInline(false);
					return;
				}
				link.MarkInline(true);
			}
			if (string.IsNullOrEmpty(link.ContentId))
			{
				link.ContentId = string.Format("{0}@1", HexConverter.ByteArrayToHexString(Guid.NewGuid().ToByteArray()));
			}
			string text = "cid:" + link.ContentId;
			string value = text;
			if (!string.IsNullOrEmpty(this.InlineImageUrlTemplate))
			{
				value = this.ApplyInlineImageIdToTemplate(link, this.InlineImageUrlTemplate);
				if (!string.IsNullOrEmpty(value))
				{
					htmlWriter.WriteAttribute("originalSrc", text);
					this.DoInlineAttachmentIdAction(link);
				}
				else
				{
					value = text;
				}
			}
			else if (this.CalculateAttachmentInlineProps)
			{
				this.DoInlineAttachmentIdAction(link);
			}
			if (this.IsWellKnownInlineImageOnLoadTemplate(this.InlineImageUrlOnLoadTemplate))
			{
				string inlineImageUrlOnLoadTemplate = this.InlineImageUrlOnLoadTemplate;
				htmlWriter.WriteAttribute("onload", inlineImageUrlOnLoadTemplate);
			}
			if (!string.IsNullOrEmpty(this.InlineImageCustomDataTemplate))
			{
				string value2 = this.ApplyInlineImageIdToTemplate(link, this.InlineImageCustomDataTemplate);
				if (!string.IsNullOrEmpty(value2))
				{
					htmlWriter.WriteAttribute("data-custom", value2);
				}
			}
			htmlWriter.WriteAttribute(HtmlAttributeId.Src, value);
			HtmlBodyCallback.WriteAllAttributesExcept(tagContext, HtmlAttributeId.Src);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002E7F9 File Offset: 0x0002C9F9
		private bool IsWellKnownInlineImageOnLoadTemplate(string template)
		{
			return HtmlBodyCallback.WellKnownInlineImageOnLoadTemplates.Contains(template);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002E808 File Offset: 0x0002CA08
		private void ProcessExternalImageRef(HtmlTagContext tagContext, HtmlWriter htmlWriter, string srcValue, HtmlAttributeId imgSrcAttributeId)
		{
			if (HtmlBodyCallback.IsEmptyLink(srcValue))
			{
				if (this.IncludeEmptyLinks)
				{
					tagContext.WriteTag(true);
				}
				return;
			}
			string text = this.GetAbsoluteUrl(srcValue, this.BlockedUrlImageValue, imgSrcAttributeId);
			if (this.BlockUnsafeImages)
			{
				if (HtmlBodyCallback.IsSafeUrl(text, imgSrcAttributeId))
				{
					if (this.BlockExternalImages)
					{
						tagContext.WriteTag(false);
						htmlWriter.WriteAttribute("blockedImageSrc", text);
						HtmlBodyCallback.WriteAllAttributesExcept(tagContext, imgSrcAttributeId);
						if (this.hasBlockedImagesAction != null)
						{
							this.hasBlockedImagesAction(true);
						}
						return;
					}
				}
				else
				{
					text = this.BlockedUrlImageValue;
				}
			}
			tagContext.WriteTag(false);
			htmlWriter.WriteAttribute(imgSrcAttributeId, text);
			HtmlBodyCallback.WriteAllAttributesExcept(tagContext, imgSrcAttributeId);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002E8A8 File Offset: 0x0002CAA8
		private void ProcessBaseTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Href)
				{
					string value = htmlTagContextAttribute.Value;
					this.baseRef = HtmlBodyCallback.TryParseUri(value, UriKind.Absolute);
					break;
				}
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002E91C File Offset: 0x0002CB1C
		private void ProcessAreaTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			if (this.AddBlankTargetToLinks)
			{
				this.AddBlankTarget(tagContext, htmlWriter, HtmlAttributeId.Href);
				return;
			}
			tagContext.WriteTag(true);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002E938 File Offset: 0x0002CB38
		private void ProcessFormTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			if (this.AddBlankTargetToLinks)
			{
				this.AddBlankTarget(tagContext, htmlWriter, HtmlAttributeId.Action);
				return;
			}
			tagContext.WriteTag(true);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002E954 File Offset: 0x0002CB54
		private void ProcessInputTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Src)
				{
					flag = true;
				}
				else if (htmlTagContextAttribute.Id == HtmlAttributeId.Type && htmlTagContextAttribute.Value.ToLower() == "image")
				{
					flag2 = true;
				}
			}
			if (flag && flag2)
			{
				AttachmentLink attachmentLink;
				string srcValue;
				this.GetAttachmentLinkAndLinkSource(tagContext, out attachmentLink, out srcValue);
				if (attachmentLink == null)
				{
					this.ProcessExternalImageRef(tagContext, htmlWriter, srcValue, HtmlAttributeId.Src);
					return;
				}
			}
			tagContext.WriteTag(true);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002EA08 File Offset: 0x0002CC08
		private string ApplyInlineImageIdToTemplate(AttachmentLink link, string srcTemplate)
		{
			if (link.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				return null;
			}
			if (this.idAndSession != null)
			{
				IdAndSession idAndSession = this.idAndSession.Clone();
				idAndSession.AttachmentIds.Add(link.AttachmentId);
				string id = idAndSession.GetConcatenatedId().Id;
				string newValue = Uri.EscapeDataString(id);
				return srcTemplate.Replace("{id}", newValue);
			}
			if (!string.IsNullOrEmpty(link.ContentId))
			{
				string stringToEscape = Convert.ToBase64String(Encoding.UTF8.GetBytes(link.ContentId));
				string newValue2 = Uri.EscapeDataString(stringToEscape);
				return srcTemplate.Replace("{id}", newValue2);
			}
			return null;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002EAA4 File Offset: 0x0002CCA4
		private void GetAttachmentLinkAndLinkSource(HtmlTagContext tagContext, out AttachmentLink link, out string srcValue)
		{
			link = null;
			srcValue = null;
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Src)
				{
					srcValue = htmlTagContextAttribute.Value;
					break;
				}
			}
			if (!string.IsNullOrEmpty(srcValue))
			{
				link = base.FindAttachmentByBodyReference(srcValue);
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002EB24 File Offset: 0x0002CD24
		private void GetAnchorReference(HtmlTagContext tagContext, HtmlAttributeId anchorAttribute, out string hrefValue)
		{
			hrefValue = null;
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == anchorAttribute)
				{
					hrefValue = htmlTagContextAttribute.Value;
					break;
				}
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002EB8C File Offset: 0x0002CD8C
		private string GetContentType(AttachmentLink link)
		{
			if (link.AttachmentType == AttachmentType.Ole)
			{
				link.ConvertToImage();
				return "image/jpeg";
			}
			return link.ContentType;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		private void DoInlineAttachmentIdAction(AttachmentLink link)
		{
			if (this.inlineAttachmentIdAction != null)
			{
				if (this.idAndSession != null)
				{
					IdAndSession idAndSession = this.idAndSession.Clone();
					idAndSession.AttachmentIds.Add(link.AttachmentId);
					this.inlineAttachmentIdAction(idAndSession.GetConcatenatedId().Id);
					return;
				}
				if (!string.IsNullOrEmpty(link.ContentId))
				{
					this.inlineAttachmentIdAction(Convert.ToBase64String(Encoding.UTF8.GetBytes(link.ContentId)));
				}
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002EC2D File Offset: 0x0002CE2D
		internal static bool IsEmptyLink(string src)
		{
			return string.Compare(src, "objattph://", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(src, "rtfimage://", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(src, "cid:", StringComparison.CurrentCultureIgnoreCase) == 0;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002EC5C File Offset: 0x0002CE5C
		internal static Uri TryParseUri(string uriString, UriKind uriKind)
		{
			Uri result = null;
			if (!Uri.TryCreate(uriString, uriKind, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002EC7C File Offset: 0x0002CE7C
		internal static HtmlBodyCallback.TypeOfUrl GetTypeOfUrl(string urlString, HtmlAttributeId htmlAttrId)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return HtmlBodyCallback.TypeOfUrl.Unknown;
			}
			if (urlString.StartsWith("#", StringComparison.Ordinal))
			{
				return HtmlBodyCallback.TypeOfUrl.Local;
			}
			Uri uri;
			if (!Uri.TryCreate(urlString, UriKind.Absolute, out uri))
			{
				return HtmlBodyCallback.TypeOfUrl.Unknown;
			}
			string scheme = uri.Scheme;
			if (string.IsNullOrEmpty(scheme))
			{
				return HtmlBodyCallback.TypeOfUrl.Unknown;
			}
			if (CultureInfo.InvariantCulture.CompareInfo.Compare(scheme, "file", CompareOptions.IgnoreCase) != 0)
			{
				return HtmlBodyCallback.TypeOfUrl.Trusted;
			}
			if (htmlAttrId != HtmlAttributeId.Href)
			{
				return HtmlBodyCallback.TypeOfUrl.Unknown;
			}
			return HtmlBodyCallback.TypeOfUrl.Trusted;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002ECE4 File Offset: 0x0002CEE4
		internal static string GetAbsoluteUrl(string urlString, HtmlAttributeId htmlAttributeId, Uri baseRef)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return null;
			}
			Uri uri;
			if (!Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out uri))
			{
				return null;
			}
			if (!uri.IsAbsoluteUri && baseRef != null)
			{
				Uri uri2;
				if (Uri.TryCreate(baseRef, uri, out uri2))
				{
					return uri2.AbsoluteUri;
				}
			}
			else if (uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return null;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		internal static bool IsSafeUrl(string urlString, HtmlAttributeId htmlAttr)
		{
			HtmlBodyCallback.TypeOfUrl typeOfUrl = HtmlBodyCallback.GetTypeOfUrl(urlString, htmlAttr);
			return typeOfUrl != HtmlBodyCallback.TypeOfUrl.Unknown;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0002ED58 File Offset: 0x0002CF58
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0002ED60 File Offset: 0x0002CF60
		internal bool IncludeEmptyLinks { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0002ED69 File Offset: 0x0002CF69
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0002ED71 File Offset: 0x0002CF71
		internal bool RemoveLinksToNonImageAttachments { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002ED7A File Offset: 0x0002CF7A
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0002ED82 File Offset: 0x0002CF82
		internal string InlineImageUrlTemplate { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002ED8B File Offset: 0x0002CF8B
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0002ED93 File Offset: 0x0002CF93
		internal string InlineImageUrlOnLoadTemplate { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
		internal string InlineImageCustomDataTemplate { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002EDAD File Offset: 0x0002CFAD
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0002EDB5 File Offset: 0x0002CFB5
		internal bool AddBlankTargetToLinks { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002EDBE File Offset: 0x0002CFBE
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0002EDC6 File Offset: 0x0002CFC6
		internal bool BlockExternalImages { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002EDCF File Offset: 0x0002CFCF
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0002EDD7 File Offset: 0x0002CFD7
		internal string BlockedUrlPageValue { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0002EDE8 File Offset: 0x0002CFE8
		internal string BlockedUrlImageValue { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002EDF1 File Offset: 0x0002CFF1
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0002EDF9 File Offset: 0x0002CFF9
		internal bool BlockUnsafeImages { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002EE02 File Offset: 0x0002D002
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0002EE0A File Offset: 0x0002D00A
		internal bool IsBodyFragment { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002EE13 File Offset: 0x0002D013
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0002EE1B File Offset: 0x0002D01B
		internal bool CalculateAttachmentInlineProps { get; set; }

		// Token: 0x1700015A RID: 346
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0002EE24 File Offset: 0x0002D024
		internal Action<bool> HasBlockedImagesAction
		{
			set
			{
				this.hasBlockedImagesAction = value;
				if (value == null)
				{
					this.hasBlockedImagesAction(false);
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0002EE3C File Offset: 0x0002D03C
		internal Action<string> InlineAttachmentIdAction
		{
			set
			{
				this.inlineAttachmentIdAction = value;
			}
		}

		// Token: 0x04000781 RID: 1921
		private const string DefaultAnchorTarget = "_blank";

		// Token: 0x04000782 RID: 1922
		private const string LocalUrlPrefix = "#";

		// Token: 0x04000783 RID: 1923
		public const string OriginalSrcAttributeName = "originalSrc";

		// Token: 0x04000784 RID: 1924
		public const string OnLoadAttributeName = "onload";

		// Token: 0x04000785 RID: 1925
		public const string CustomDataAttributeName = "data-custom";

		// Token: 0x04000786 RID: 1926
		public const string BlockedImageSrcAttributeName = "blockedImageSrc";

		// Token: 0x04000787 RID: 1927
		private const string InlineRTFattachmentScheme = "objattph://";

		// Token: 0x04000788 RID: 1928
		private const string EmbeddedRTFImage = "rtfimage://";

		// Token: 0x04000789 RID: 1929
		public const string ContentIdUrlScheme = "cid:";

		// Token: 0x0400078A RID: 1930
		private const string DataUrlContentEncoding = "base64";

		// Token: 0x0400078B RID: 1931
		private const string AttachmentIdParameter = "{id}";

		// Token: 0x0400078C RID: 1932
		private const string BlockedUrlFormatString = "[{0}]";

		// Token: 0x0400078D RID: 1933
		private const string ImageTypeAttributeValue = "image";

		// Token: 0x0400078E RID: 1934
		private static readonly HashSet<string> WellKnownInlineImageOnLoadTemplates = new HashSet<string>();

		// Token: 0x0400078F RID: 1935
		private Action<bool> hasBlockedImagesAction;

		// Token: 0x04000790 RID: 1936
		private Action<string> inlineAttachmentIdAction;

		// Token: 0x04000791 RID: 1937
		private Uri baseRef;

		// Token: 0x04000792 RID: 1938
		private bool triedToLoadBaseHref;

		// Token: 0x04000793 RID: 1939
		private IdAndSession idAndSession;

		// Token: 0x04000794 RID: 1940
		private Item cachedItem;

		// Token: 0x02000159 RID: 345
		internal enum TypeOfUrl
		{
			// Token: 0x040007A2 RID: 1954
			Trusted,
			// Token: 0x040007A3 RID: 1955
			Local,
			// Token: 0x040007A4 RID: 1956
			Unknown
		}
	}
}
