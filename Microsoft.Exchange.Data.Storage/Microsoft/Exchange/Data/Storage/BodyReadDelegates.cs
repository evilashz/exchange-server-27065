using System;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A2 RID: 1442
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class BodyReadDelegates
	{
		// Token: 0x06003AEB RID: 15083 RVA: 0x000F243A File Offset: 0x000F063A
		private static object GetTextStreamOrReader(Stream stream, TextConverter converter, bool createReader)
		{
			if (createReader)
			{
				return new ConverterReader(stream, converter);
			}
			return new ConverterStream(stream, converter, ConverterStreamAccess.Read);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000F2450 File Offset: 0x000F0650
		private static object GetRtfStreamOrReader(Stream stream, TextConverter toRtfConverter, bool createReader)
		{
			object obj = null;
			Stream stream2 = null;
			try
			{
				stream2 = new ConverterStream(stream, toRtfConverter, ConverterStreamAccess.Read);
				if (createReader)
				{
					obj = new StreamReader(stream2, CTSGlobals.AsciiEncoding);
				}
				else
				{
					obj = new ConverterStream(stream2, new RtfToRtfCompressed(), ConverterStreamAccess.Read);
				}
			}
			finally
			{
				if (obj == null && stream2 != null)
				{
					stream2.Dispose();
					stream2 = null;
				}
			}
			return obj;
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000F24AC File Offset: 0x000F06AC
		private static object FromTextToText(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			TextToText textToText;
			if (string.IsNullOrEmpty(configuration.InjectPrefix) && string.IsNullOrEmpty(configuration.InjectSuffix))
			{
				textToText = new TextToText(TextToTextConversionMode.ConvertCodePageOnly);
			}
			else
			{
				textToText = new TextToText();
			}
			textToText.InputEncoding = ConvertUtils.UnicodeEncoding;
			textToText.OutputEncoding = configuration.Encoding;
			if (!string.IsNullOrEmpty(configuration.InjectPrefix) || !string.IsNullOrEmpty(configuration.InjectSuffix))
			{
				textToText.Header = configuration.InjectPrefix;
				textToText.Footer = configuration.InjectSuffix;
				textToText.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
			}
			return BodyReadDelegates.GetTextStreamOrReader(bodyStream, textToText, createReader);
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000F2544 File Offset: 0x000F0744
		private static object FromTextToHtml(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			return BodyReadDelegates.GetTextStreamOrReader(bodyStream, new TextToHtml
			{
				InputEncoding = ConvertUtils.UnicodeEncoding,
				OutputEncoding = configuration.Encoding,
				Header = configuration.InjectPrefix,
				Footer = configuration.InjectSuffix,
				HeaderFooterFormat = configuration.InjectionHeaderFooterFormat,
				HtmlTagCallback = configuration.InternalHtmlTagCallback,
				FilterHtml = configuration.FilterHtml,
				OutputHtmlFragment = configuration.IsHtmlFragment
			}, createReader);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000F25C0 File Offset: 0x000F07C0
		private static object FromTextToRtf(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			return BodyReadDelegates.GetRtfStreamOrReader(bodyStream, new TextToRtf
			{
				InputEncoding = ConvertUtils.UnicodeEncoding,
				Header = configuration.InjectPrefix,
				Footer = configuration.InjectSuffix,
				HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
			}, createReader);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000F260C File Offset: 0x000F080C
		private static object FromHtmlToText(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			HtmlToText htmlToText = new HtmlToText();
			htmlToText.InputEncoding = ConvertUtils.GetItemMimeCharset(coreItem.PropertyBag).GetEncoding();
			htmlToText.OutputEncoding = configuration.Encoding;
			htmlToText.DetectEncodingFromMetaTag = false;
			htmlToText.Header = configuration.InjectPrefix;
			htmlToText.Footer = configuration.InjectSuffix;
			htmlToText.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
			htmlToText.ShouldUseNarrowGapForPTagHtmlToTextConversion = configuration.ShouldUseNarrowGapForPTagHtmlToTextConversion;
			htmlToText.OutputAnchorLinks = configuration.ShouldOutputAnchorLinks;
			htmlToText.OutputImageLinks = configuration.ShouldOutputImageLinks;
			if (configuration.ImageRenderingCallback != null)
			{
				TextConvertersInternalHelpers.SetImageRenderingCallback(htmlToText, configuration.ImageRenderingCallback);
			}
			return BodyReadDelegates.GetTextStreamOrReader(bodyStream, htmlToText, createReader);
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000F26AC File Offset: 0x000F08AC
		private static object FromHtmlToHtml(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			Charset itemMimeCharset = ConvertUtils.GetItemMimeCharset(coreItem.PropertyBag);
			Charset charset = configuration.Charset;
			HtmlToHtml htmlToHtml = new HtmlToHtml();
			htmlToHtml.InputEncoding = itemMimeCharset.GetEncoding();
			htmlToHtml.OutputEncoding = charset.GetEncoding();
			htmlToHtml.Header = configuration.InjectPrefix;
			htmlToHtml.Footer = configuration.InjectSuffix;
			htmlToHtml.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
			htmlToHtml.DetectEncodingFromMetaTag = false;
			htmlToHtml.FilterHtml = configuration.FilterHtml;
			htmlToHtml.HtmlTagCallback = configuration.InternalHtmlTagCallback;
			htmlToHtml.OutputHtmlFragment = configuration.IsHtmlFragment;
			if (configuration.StyleSheetLimit != null)
			{
				TextConvertersInternalHelpers.SetSmallCssBlockThreshold(htmlToHtml, configuration.StyleSheetLimit.Value);
			}
			return BodyReadDelegates.GetTextStreamOrReader(bodyStream, htmlToHtml, createReader);
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000F2768 File Offset: 0x000F0968
		private static object FromHtmlToRtf(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			HtmlToRtf htmlToRtf = new HtmlToRtf();
			htmlToRtf.InputEncoding = ConvertUtils.GetItemMimeCharset(coreItem.PropertyBag).GetEncoding();
			htmlToRtf.DetectEncodingFromMetaTag = false;
			htmlToRtf.Header = configuration.InjectPrefix;
			htmlToRtf.Footer = configuration.InjectSuffix;
			htmlToRtf.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
			if (configuration.ImageRenderingCallback != null)
			{
				TextConvertersInternalHelpers.SetImageRenderingCallback(htmlToRtf, configuration.ImageRenderingCallback);
			}
			return BodyReadDelegates.GetRtfStreamOrReader(bodyStream, htmlToRtf, createReader);
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000F27D8 File Offset: 0x000F09D8
		private static object FromRtfToText(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			object obj = null;
			Stream stream = null;
			try
			{
				stream = new ConverterStream(bodyStream, new RtfCompressedToRtf(), ConverterStreamAccess.Read);
				obj = BodyReadDelegates.GetTextStreamOrReader(stream, new RtfToText
				{
					OutputEncoding = configuration.Encoding,
					Header = configuration.InjectPrefix,
					Footer = configuration.InjectSuffix,
					HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
				}, createReader);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			return obj;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000F2858 File Offset: 0x000F0A58
		private static object FromRtfToHtml(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			object obj = null;
			Stream stream = null;
			try
			{
				stream = new ConverterStream(bodyStream, new RtfCompressedToRtf(), ConverterStreamAccess.Read);
				RtfToHtml rtfToHtml = new RtfToHtml();
				rtfToHtml.OutputEncoding = configuration.Encoding;
				rtfToHtml.Header = configuration.InjectPrefix;
				rtfToHtml.Footer = configuration.InjectSuffix;
				rtfToHtml.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
				rtfToHtml.FilterHtml = configuration.FilterHtml;
				rtfToHtml.HtmlTagCallback = configuration.InternalHtmlTagCallback;
				rtfToHtml.OutputHtmlFragment = configuration.IsHtmlFragment;
				if (configuration.StyleSheetLimit != null)
				{
					TextConvertersInternalHelpers.SetSmallCssBlockThreshold(rtfToHtml, configuration.StyleSheetLimit.Value);
				}
				obj = BodyReadDelegates.GetTextStreamOrReader(stream, rtfToHtml, createReader);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			return obj;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F2920 File Offset: 0x000F0B20
		private static object FromRtfToRtf(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader)
		{
			object obj = null;
			Stream stream = null;
			try
			{
				stream = new ConverterStream(bodyStream, new RtfCompressedToRtf(), ConverterStreamAccess.Read);
				obj = BodyReadDelegates.GetRtfStreamOrReader(stream, new RtfToRtf
				{
					Header = configuration.InjectPrefix,
					Footer = configuration.InjectSuffix,
					HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
				}, createReader);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			return obj;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F2994 File Offset: 0x000F0B94
		private static int GetFormatIndex(BodyFormat format)
		{
			switch (format)
			{
			case BodyFormat.TextPlain:
				return 0;
			case BodyFormat.TextHtml:
				return 1;
			case BodyFormat.ApplicationRtf:
				return 2;
			default:
				throw new InvalidOperationException("BodyReadDelegates.GetFormatIndex");
			}
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000F29CC File Offset: 0x000F0BCC
		private static BodyReadDelegates.ConversionCreator GetConversionMethod(ICoreItem coreItem, BodyReadConfiguration configuration)
		{
			int formatIndex = BodyReadDelegates.GetFormatIndex(coreItem.Body.RawFormat);
			int formatIndex2 = BodyReadDelegates.GetFormatIndex(configuration.Format);
			return BodyReadDelegates.conversionCreatorsTable[formatIndex][formatIndex2];
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x000F2A00 File Offset: 0x000F0C00
		private static ConversionCallbackBase CreateConversionDelegateProvider(ICoreItem coreItem, BodyReadConfiguration configuration)
		{
			ConversionCallbackBase conversionCallbackBase;
			if (coreItem.Body.RawFormat == BodyFormat.ApplicationRtf && configuration.Format == BodyFormat.TextHtml)
			{
				if (configuration.HtmlCallback == null)
				{
					conversionCallbackBase = new DefaultHtmlCallbacks(coreItem, false)
					{
						ClearInlineOnUnmarkedAttachments = false
					};
					configuration.ConversionCallback = conversionCallbackBase;
				}
				else
				{
					conversionCallbackBase = configuration.ConversionCallback;
				}
			}
			else if (coreItem.Body.RawFormat == BodyFormat.TextHtml && configuration.Format == BodyFormat.ApplicationRtf)
			{
				if (configuration.ImageRenderingCallback == null)
				{
					conversionCallbackBase = new DefaultRtfCallbacks(coreItem, false)
					{
						ClearInlineOnUnmarkedAttachments = false
					};
					configuration.ConversionCallback = conversionCallbackBase;
				}
				else
				{
					conversionCallbackBase = configuration.ConversionCallback;
				}
			}
			else
			{
				conversionCallbackBase = null;
			}
			return conversionCallbackBase;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x000F2ADC File Offset: 0x000F0CDC
		internal static TextReader CreateReader(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, out ConversionCallbackBase provider)
		{
			BodyReadConfiguration readerConfiguration = new BodyReadConfiguration(configuration);
			readerConfiguration.Charset = ConvertUtils.UnicodeCharset;
			provider = BodyReadDelegates.CreateConversionDelegateProvider(coreItem, readerConfiguration);
			return ConvertUtils.CallCtsWithReturnValue<TextReader>(ExTraceGlobals.CcBodyTracer, "BodyReadDelegates::CreateReader", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				BodyReadDelegates.ConversionCreator conversionMethod = BodyReadDelegates.GetConversionMethod(coreItem, configuration);
				return (TextReader)conversionMethod(coreItem, readerConfiguration, bodyStream, true);
			});
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000F2BA0 File Offset: 0x000F0DA0
		internal static Stream CreateStream(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, out ConversionCallbackBase provider)
		{
			BodyReadConfiguration readConfiguration = new BodyReadConfiguration(configuration);
			provider = BodyReadDelegates.CreateConversionDelegateProvider(coreItem, readConfiguration);
			if (readConfiguration.Charset == null)
			{
				readConfiguration.Charset = ConvertUtils.GetItemMimeCharset(coreItem.PropertyBag);
			}
			if (!readConfiguration.IsBodyTransformationNeeded(coreItem.Body))
			{
				return bodyStream;
			}
			return ConvertUtils.CallCtsWithReturnValue<Stream>(ExTraceGlobals.CcBodyTracer, "BodyReadDelegates::CreateStream", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				BodyReadDelegates.ConversionCreator conversionMethod = BodyReadDelegates.GetConversionMethod(coreItem, configuration);
				return (Stream)conversionMethod(coreItem, readConfiguration, bodyStream, false);
			});
		}

		// Token: 0x04001F89 RID: 8073
		private static BodyReadDelegates.ConversionCreator[][] conversionCreatorsTable = new BodyReadDelegates.ConversionCreator[][]
		{
			new BodyReadDelegates.ConversionCreator[]
			{
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromTextToText),
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromTextToHtml),
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromTextToRtf)
			},
			new BodyReadDelegates.ConversionCreator[]
			{
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromHtmlToText),
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromHtmlToHtml),
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromHtmlToRtf)
			},
			new BodyReadDelegates.ConversionCreator[]
			{
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromRtfToText),
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromRtfToHtml),
				new BodyReadDelegates.ConversionCreator(BodyReadDelegates.FromRtfToRtf)
			}
		};

		// Token: 0x020005A3 RID: 1443
		// (Invoke) Token: 0x06003AFD RID: 15101
		private delegate object ConversionCreator(ICoreItem coreItem, BodyReadConfiguration configuration, Stream bodyStream, bool createReader);
	}
}
