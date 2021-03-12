using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;

namespace Microsoft.Exchange.Management
{
	// Token: 0x020007C0 RID: 1984
	internal class TextConverterHelper
	{
		// Token: 0x060045B1 RID: 17841 RVA: 0x0011E6E0 File Offset: 0x0011C8E0
		public static string SanitizeHtml(string unsafeHtml)
		{
			if (string.IsNullOrEmpty(unsafeHtml))
			{
				return unsafeHtml;
			}
			string result;
			using (StringReader stringReader = new StringReader(unsafeHtml))
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					HtmlToHtml htmlToHtml = new HtmlToHtml();
					TextConvertersInternalHelpers.SetPreserveDisplayNoneStyle(htmlToHtml, true);
					htmlToHtml.InputEncoding = Encoding.UTF8;
					htmlToHtml.OutputEncoding = Encoding.UTF8;
					htmlToHtml.FilterHtml = true;
					htmlToHtml.Convert(stringReader, stringWriter);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x0011E778 File Offset: 0x0011C978
		public static string HtmlToText(string html, bool shouldUseNarrowGapForPTagHtmlToTextConversion)
		{
			if (string.IsNullOrEmpty(html))
			{
				return html;
			}
			html = TextConverterHelper.RemoveHtmlLink(html);
			string result;
			using (StringReader stringReader = new StringReader(html))
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					HtmlToText htmlToText = new HtmlToText();
					htmlToText.InputEncoding = Encoding.UTF8;
					htmlToText.OutputEncoding = Encoding.UTF8;
					htmlToText.ShouldUseNarrowGapForPTagHtmlToTextConversion = shouldUseNarrowGapForPTagHtmlToTextConversion;
					TextConvertersInternalHelpers.SetImageRenderingCallback(htmlToText, new ImageRenderingCallback(TextConverterHelper.RemoveImageCallback));
					htmlToText.Convert(stringReader, stringWriter);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x0011E820 File Offset: 0x0011CA20
		public static string TextToHtml(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			string result;
			using (StringReader stringReader = new StringReader(text))
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					new TextToHtml
					{
						InputEncoding = Encoding.UTF8,
						OutputEncoding = Encoding.UTF8,
						HtmlTagCallback = new HtmlTagCallback(TextConverterHelper.RemoveLinkCallback),
						OutputHtmlFragment = true
					}.Convert(stringReader, stringWriter);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x0011E8C0 File Offset: 0x0011CAC0
		internal static string RemoveHtmlLink(string html)
		{
			string result;
			using (StringReader stringReader = new StringReader(html))
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					new HtmlToHtml
					{
						InputEncoding = Encoding.UTF8,
						OutputEncoding = Encoding.UTF8,
						HtmlTagCallback = new HtmlTagCallback(TextConverterHelper.RemoveLinkCallback)
					}.Convert(stringReader, stringWriter);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x0011E950 File Offset: 0x0011CB50
		internal static void RemoveLinkCallback(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			if (tagContext.TagId == HtmlTagId.A)
			{
				tagContext.DeleteTag();
				return;
			}
			tagContext.WriteTag();
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				htmlTagContextAttribute.Write();
			}
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0011E9BC File Offset: 0x0011CBBC
		internal static bool RemoveImageCallback(string imageUrl, int approximateRenderingPosition)
		{
			return true;
		}
	}
}
