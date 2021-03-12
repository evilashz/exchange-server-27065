using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x0200005A RID: 90
	internal abstract class HtmlBodyCallback : HtmlCallbackBase
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000702C File Offset: 0x0000522C
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00007034 File Offset: 0x00005234
		internal bool AddBlankTargetToLinks { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000703D File Offset: 0x0000523D
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00007045 File Offset: 0x00005245
		internal bool BlockExternalImages { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000704E File Offset: 0x0000524E
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00007056 File Offset: 0x00005256
		internal bool BlockUnsafeImages { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000705F File Offset: 0x0000525F
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00007067 File Offset: 0x00005267
		internal string BlockedUrlImageValue { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007070 File Offset: 0x00005270
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007078 File Offset: 0x00005278
		internal string BlockedUrlPageValue { get; set; }

		// Token: 0x17000051 RID: 81
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00007081 File Offset: 0x00005281
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007099 File Offset: 0x00005299
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000070A1 File Offset: 0x000052A1
		internal bool IncludeEmptyLinks { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000070AA File Offset: 0x000052AA
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000070B2 File Offset: 0x000052B2
		internal string InlineImageCustomDataTemplate { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000070BB File Offset: 0x000052BB
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000070C3 File Offset: 0x000052C3
		internal string InlineImageUrlOnLoadTemplate { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000070CC File Offset: 0x000052CC
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000070D4 File Offset: 0x000052D4
		internal string InlineImageUrlTemplate { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000070DD File Offset: 0x000052DD
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000070E5 File Offset: 0x000052E5
		internal bool IsBodyFragment { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000070EE File Offset: 0x000052EE
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000070F6 File Offset: 0x000052F6
		internal bool RemoveLinksToNonImageAttachments { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000070FF File Offset: 0x000052FF
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00007107 File Offset: 0x00005307
		private protected Uri BaseRef { protected get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007110 File Offset: 0x00005310
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00007118 File Offset: 0x00005318
		protected Action<string> InlineAttachmentIdAction { get; set; }

		// Token: 0x060001F5 RID: 501 RVA: 0x00007124 File Offset: 0x00005324
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

		// Token: 0x060001F6 RID: 502 RVA: 0x0000717C File Offset: 0x0000537C
		internal static HtmlBodyCallback.TypeOfUrl GetTypeOfUrl(string urlString, HtmlAttributeId htmlAttrId)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return HtmlBodyCallback.TypeOfUrl.Unknown;
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

		// Token: 0x060001F7 RID: 503 RVA: 0x000071D4 File Offset: 0x000053D4
		internal static bool IsSafeUrl(string urlString, HtmlAttributeId htmlAttr)
		{
			HtmlBodyCallback.TypeOfUrl typeOfUrl = HtmlBodyCallback.GetTypeOfUrl(urlString, htmlAttr);
			return typeOfUrl != HtmlBodyCallback.TypeOfUrl.Unknown;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000071F0 File Offset: 0x000053F0
		internal static Uri TryParseUri(string uriString, UriKind uriKind)
		{
			Uri result;
			if (Uri.TryCreate(uriString, uriKind, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007228 File Offset: 0x00005428
		protected static void WriteAllAttributesExcept(HtmlTagContext tagContext, HtmlAttributeId attrToSkip)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in from attribute in tagContext.Attributes
			where attribute.Id != attrToSkip
			select attribute)
			{
				htmlTagContextAttribute.Write();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007298 File Offset: 0x00005498
		protected static void WriteAllAttributesExcept(HtmlTagContext tagContext, Func<HtmlAttributeId, bool> shouldSkipFunc)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (!shouldSkipFunc(htmlTagContextAttribute.Id))
				{
					htmlTagContextAttribute.Write();
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007318 File Offset: 0x00005518
		protected void GetAnchorReference(HtmlTagContext tagContext, out string hrefValue)
		{
			hrefValue = (from attr in tagContext.Attributes
			where attr.Id == HtmlAttributeId.Href
			select attr.Value).FirstOrDefault<string>();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007394 File Offset: 0x00005594
		protected void GetAttachmentLinkAndLinkSource(HtmlTagContext tagContext, out AttachmentLink link, out string srcValue)
		{
			link = null;
			srcValue = (from attr in tagContext.Attributes
			where attr.Id == HtmlAttributeId.Src
			select attr.Value).FirstOrDefault<string>();
			if (!string.IsNullOrEmpty(srcValue))
			{
				link = base.FindAttachmentByBodyReference(srcValue);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000740D File Offset: 0x0000560D
		protected string GetContentType(AttachmentLink link)
		{
			if (link.AttachmentType == AttachmentType.Ole)
			{
				link.ConvertToImage();
				return "image/jpeg";
			}
			return link.ContentType;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007440 File Offset: 0x00005640
		protected void ProcessBaseTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			using (IEnumerator<string> enumerator = (from filterAttribute in tagContext.Attributes
			where filterAttribute.Id == HtmlAttributeId.Href
			select filterAttribute.Value).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string uriString = enumerator.Current;
					this.BaseRef = HtmlBodyCallback.TryParseUri(uriString, UriKind.Absolute);
				}
			}
		}

		// Token: 0x04000089 RID: 137
		public const string BlockedImageSrcAttributeName = "blockedImageSrc";

		// Token: 0x0400008A RID: 138
		public const string ContentIdUrlScheme = "cid:";

		// Token: 0x0400008B RID: 139
		public const string CustomDataAttributeName = "data-custom";

		// Token: 0x0400008C RID: 140
		public const string OnLoadAttributeName = "onload";

		// Token: 0x0400008D RID: 141
		public const string OriginalSrcAttributeName = "originalSrc";

		// Token: 0x0400008E RID: 142
		private Action<bool> hasBlockedImagesAction;

		// Token: 0x0200005B RID: 91
		internal enum TypeOfUrl
		{
			// Token: 0x040000A3 RID: 163
			Trusted,
			// Token: 0x040000A4 RID: 164
			Unknown
		}
	}
}
