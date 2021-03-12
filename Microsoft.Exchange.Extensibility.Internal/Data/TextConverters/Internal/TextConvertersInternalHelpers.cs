using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal
{
	// Token: 0x0200001D RID: 29
	internal sealed class TextConvertersInternalHelpers
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000472B File Offset: 0x0000292B
		public static void SetImageRenderingCallback(HtmlToText conversion, ImageRenderingCallback callback)
		{
			conversion.SetImageRenderingCallback(new ImageRenderingCallbackInternal(callback.Invoke));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004740 File Offset: 0x00002940
		public static void SetImageRenderingCallback(HtmlToRtf conversion, ImageRenderingCallback callback)
		{
			conversion.SetImageRenderingCallback(new ImageRenderingCallbackInternal(callback.Invoke));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004755 File Offset: 0x00002955
		public static void SetSmallCssBlockThreshold(HtmlToHtml conversion, int threshold)
		{
			conversion.SetSmallCssBlockThreshold(threshold);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000475F File Offset: 0x0000295F
		public static void SetSmallCssBlockThreshold(RtfToHtml conversion, int threshold)
		{
			conversion.SetSmallCssBlockThreshold(threshold);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004769 File Offset: 0x00002969
		public static void SetPreserveDisplayNoneStyle(HtmlToHtml conversion, bool preserve)
		{
			conversion.SetPreserveDisplayNoneStyle(preserve);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004773 File Offset: 0x00002973
		public static void SetPreserveDisplayNoneStyle(RtfToHtml conversion, bool preserve)
		{
			conversion.SetPreserveDisplayNoneStyle(preserve);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000477D File Offset: 0x0000297D
		public static Stream CreateRtfPreviewStream(Stream input, int inputBufferLength)
		{
			return new RtfPreviewStream(input, inputBufferLength);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004788 File Offset: 0x00002988
		public static bool RtfHasEncapsulatedHtml(Stream previewStream)
		{
			RtfPreviewStream rtfPreviewStream = previewStream as RtfPreviewStream;
			if (rtfPreviewStream == null)
			{
				throw new ArgumentException("previewStream must be created with CreateRtfPreviewStream");
			}
			return rtfPreviewStream.Encapsulation == RtfEncapsulation.Html;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000047B4 File Offset: 0x000029B4
		public static bool RtfHasEncapsulatedText(Stream previewStream)
		{
			RtfPreviewStream rtfPreviewStream = previewStream as RtfPreviewStream;
			if (rtfPreviewStream == null)
			{
				throw new ArgumentException("previewStream must be created with CreateRtfPreviewStream");
			}
			return rtfPreviewStream.Encapsulation == RtfEncapsulation.Text;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000047DF File Offset: 0x000029DF
		public static bool IsUrlSchemaSafe(string schema)
		{
			return HtmlToHtmlConverter.TestSafeUrlSchema(schema);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000047E7 File Offset: 0x000029E7
		public static void ReuseConverter(ConverterStream converter, object newSourceOrSink)
		{
			converter.Reuse(newSourceOrSink);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000047F0 File Offset: 0x000029F0
		public static void ReuseConverter(ConverterReader converter, object newSource)
		{
			converter.Reuse(newSource);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000047F9 File Offset: 0x000029F9
		public static void ReuseConverter(ConverterWriter converter, object newSink)
		{
			converter.Reuse(newSink);
		}
	}
}
