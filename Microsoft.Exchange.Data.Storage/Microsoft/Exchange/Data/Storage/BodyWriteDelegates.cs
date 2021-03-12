using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005AB RID: 1451
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class BodyWriteDelegates
	{
		// Token: 0x06003B87 RID: 15239 RVA: 0x000F4F1F File Offset: 0x000F311F
		private static object GetConverterStreamOrWriter(Stream targetStream, TextConverter converter, bool createWriter)
		{
			if (createWriter)
			{
				return new ConverterWriter(targetStream, converter);
			}
			return new ConverterStream(targetStream, converter, ConverterStreamAccess.Write);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000F4F34 File Offset: 0x000F3134
		private static object GetStreamOrUnicodeWriter(Stream targetStream, bool createWriter)
		{
			if (createWriter)
			{
				return new StreamWriter(targetStream, ConvertUtils.UnicodeEncoding);
			}
			return targetStream;
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000F4F46 File Offset: 0x000F3146
		private static object GetRtfDecompressorOrUnicodeWriter(Stream uncompressedRtfStream, bool createWriter)
		{
			if (createWriter)
			{
				return new StreamWriter(uncompressedRtfStream, CTSGlobals.AsciiEncoding);
			}
			return new ConverterStream(uncompressedRtfStream, new RtfCompressedToRtf(), ConverterStreamAccess.Write);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000F4F64 File Offset: 0x000F3164
		private static void SafeDisposeStream(Stream tempStream)
		{
			if (tempStream != null)
			{
				try
				{
					tempStream.Dispose();
				}
				catch (ExchangeDataException arg)
				{
					ExTraceGlobals.CcBodyTracer.TraceError<Stream, ExchangeDataException>((long)tempStream.GetHashCode(), "BodyWriteDelegates.SafeDisposeStream({0}) exception {1}", tempStream, arg);
				}
			}
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000F4FA8 File Offset: 0x000F31A8
		private static object FromTextToText(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				stream = new BodyCharsetDetectionStream(bodyStream, null, coreItem, BodyStreamFormat.Text, ConvertUtils.UnicodeCharset, configuration.TargetCharset, configuration.TargetCharsetFlags, null, false);
				if (!createWriter && !configuration.IsContentTransformationNeeded(coreItem))
				{
					obj = stream;
				}
				else
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
					textToText.InputEncoding = configuration.SourceEncoding;
					textToText.OutputEncoding = ConvertUtils.UnicodeEncoding;
					if (!string.IsNullOrEmpty(configuration.InjectPrefix) || !string.IsNullOrEmpty(configuration.InjectSuffix))
					{
						textToText.Header = configuration.InjectPrefix;
						textToText.Footer = configuration.InjectSuffix;
						textToText.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
					}
					obj = BodyWriteDelegates.GetConverterStreamOrWriter(stream, textToText, createWriter);
				}
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000F5094 File Offset: 0x000F3294
		private static object FromTextToHtml(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				HtmlWriteConverterStream htmlWriteConverterStream = new HtmlWriteConverterStream(bodyStream, new TextToHtml
				{
					InputEncoding = configuration.SourceEncoding,
					Header = configuration.InjectPrefix,
					Footer = configuration.InjectSuffix,
					HeaderFooterFormat = configuration.InjectionHeaderFooterFormat,
					HtmlTagCallback = configuration.InternalHtmlTagCallback,
					FilterHtml = configuration.FilterHtml
				});
				stream = htmlWriteConverterStream;
				stream = new BodyCharsetDetectionStream(stream, new BodyCharsetDetectionStream.DetectCharsetCallback(htmlWriteConverterStream.SetCharset), coreItem, BodyStreamFormat.Text, configuration.SourceCharset, configuration.TargetCharset, configuration.TargetCharsetFlags, BodyWriteDelegates.GetExtraData(configuration), false);
				obj = BodyWriteDelegates.GetStreamOrUnicodeWriter(stream, createWriter);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000F5154 File Offset: 0x000F3354
		private static object FromTextToRtf(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				stream = new ConverterStream(bodyStream, new RtfToRtfCompressed(), ConverterStreamAccess.Write);
				stream = new ConverterStream(stream, new TextToRtf
				{
					InputEncoding = configuration.SourceEncoding,
					Header = configuration.InjectPrefix,
					Footer = configuration.InjectSuffix,
					HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
				}, ConverterStreamAccess.Write);
				stream = new BodyCharsetDetectionStream(stream, null, coreItem, BodyStreamFormat.Text, configuration.SourceCharset, configuration.TargetCharset, configuration.TargetCharsetFlags, BodyWriteDelegates.GetExtraData(configuration), false);
				obj = BodyWriteDelegates.GetStreamOrUnicodeWriter(stream, createWriter);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000F51FC File Offset: 0x000F33FC
		private static object FromHtmlToText(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				stream = new BodyCharsetDetectionStream(bodyStream, null, coreItem, BodyStreamFormat.Text, ConvertUtils.UnicodeCharset, configuration.TargetCharset, configuration.TargetCharsetFlags, null, false);
				HtmlToText htmlToText = new HtmlToText();
				htmlToText.InputEncoding = configuration.SourceEncoding;
				htmlToText.OutputEncoding = ConvertUtils.UnicodeEncoding;
				htmlToText.DetectEncodingFromMetaTag = false;
				htmlToText.Header = configuration.InjectPrefix;
				htmlToText.Footer = configuration.InjectSuffix;
				htmlToText.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
				if (configuration.ImageRenderingCallback != null)
				{
					TextConvertersInternalHelpers.SetImageRenderingCallback(htmlToText, configuration.ImageRenderingCallback);
				}
				TextConverter converter = htmlToText;
				if (configuration.FilterHtml || configuration.InternalHtmlTagCallback != null)
				{
					stream = new ConverterStream(stream, htmlToText, ConverterStreamAccess.Write);
					converter = new HtmlToHtml
					{
						InputEncoding = configuration.SourceEncoding,
						OutputEncoding = configuration.SourceEncoding,
						DetectEncodingFromMetaTag = false,
						FilterHtml = configuration.FilterHtml,
						HtmlTagCallback = configuration.InternalHtmlTagCallback
					};
				}
				obj = BodyWriteDelegates.GetConverterStreamOrWriter(stream, converter, createWriter);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000F5314 File Offset: 0x000F3514
		private static object FromHtmlToHtml(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				if (!createWriter && configuration.SourceCharset == null)
				{
					obj = bodyStream;
				}
				else
				{
					HtmlToHtml htmlToHtml = new HtmlToHtml();
					htmlToHtml.InputEncoding = configuration.SourceEncoding;
					htmlToHtml.DetectEncodingFromMetaTag = configuration.TrustHtmlMetaTag;
					htmlToHtml.FilterHtml = configuration.FilterHtml;
					htmlToHtml.HtmlTagCallback = configuration.InternalHtmlTagCallback;
					htmlToHtml.Header = configuration.InjectPrefix;
					htmlToHtml.Footer = configuration.InjectSuffix;
					htmlToHtml.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
					bool canSkipConversionOnMatchingCharset = !configuration.IsContentTransformationNeeded(coreItem);
					HtmlWriteConverterStream htmlWriteConverterStream = new HtmlWriteConverterStream(bodyStream, htmlToHtml, canSkipConversionOnMatchingCharset);
					stream = htmlWriteConverterStream;
					stream = new BodyCharsetDetectionStream(htmlWriteConverterStream, new BodyCharsetDetectionStream.DetectCharsetCallback(htmlWriteConverterStream.SetCharset), coreItem, BodyStreamFormat.Html, configuration.SourceCharset, configuration.TargetCharset, configuration.TargetCharsetFlags, BodyWriteDelegates.GetExtraData(configuration), configuration.TrustHtmlMetaTag);
					obj = BodyWriteDelegates.GetStreamOrUnicodeWriter(stream, createWriter);
				}
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000F5408 File Offset: 0x000F3608
		private static object FromHtmlToRtf(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				stream = new ConverterStream(bodyStream, new RtfToRtfCompressed(), ConverterStreamAccess.Write);
				stream = new BodyCharsetDetectionStream(stream, null, coreItem, BodyStreamFormat.RtfUncompressed, null, configuration.TargetCharset, configuration.TargetCharsetFlags, null, false);
				HtmlToRtf htmlToRtf = new HtmlToRtf();
				htmlToRtf.InputEncoding = configuration.SourceEncoding;
				htmlToRtf.DetectEncodingFromMetaTag = false;
				htmlToRtf.Header = configuration.InjectPrefix;
				htmlToRtf.Footer = configuration.InjectSuffix;
				htmlToRtf.HeaderFooterFormat = configuration.InjectionHeaderFooterFormat;
				if (configuration.ImageRenderingCallback != null)
				{
					TextConvertersInternalHelpers.SetImageRenderingCallback(htmlToRtf, configuration.ImageRenderingCallback);
				}
				TextConverter converter = htmlToRtf;
				if (configuration.FilterHtml || configuration.InternalHtmlTagCallback != null)
				{
					stream = new ConverterStream(stream, htmlToRtf, ConverterStreamAccess.Write);
					converter = new HtmlToHtml
					{
						InputEncoding = configuration.SourceEncoding,
						OutputEncoding = configuration.SourceEncoding,
						DetectEncodingFromMetaTag = false,
						FilterHtml = configuration.FilterHtml,
						HtmlTagCallback = configuration.InternalHtmlTagCallback
					};
				}
				obj = BodyWriteDelegates.GetConverterStreamOrWriter(stream, converter, createWriter);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000F551C File Offset: 0x000F371C
		private static object FromRtfToText(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				stream = new BodyCharsetDetectionStream(bodyStream, null, coreItem, BodyStreamFormat.Text, ConvertUtils.UnicodeCharset, configuration.TargetCharset, configuration.TargetCharsetFlags, null, false);
				stream = new ConverterStream(stream, new RtfToText
				{
					OutputEncoding = ConvertUtils.UnicodeEncoding,
					Header = configuration.InjectPrefix,
					Footer = configuration.InjectSuffix,
					HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
				}, ConverterStreamAccess.Write);
				obj = BodyWriteDelegates.GetRtfDecompressorOrUnicodeWriter(stream, createWriter);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000F55B0 File Offset: 0x000F37B0
		private static object FromRtfToHtml(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				HtmlWriteConverterStream htmlWriteConverterStream = new HtmlWriteConverterStream(bodyStream, new RtfToHtml
				{
					FilterHtml = configuration.FilterHtml,
					HtmlTagCallback = configuration.InternalHtmlTagCallback,
					Header = configuration.InjectPrefix,
					Footer = configuration.InjectSuffix,
					HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
				});
				stream = htmlWriteConverterStream;
				stream = new BodyCharsetDetectionStream(stream, new BodyCharsetDetectionStream.DetectCharsetCallback(htmlWriteConverterStream.SetCharset), coreItem, BodyStreamFormat.RtfUncompressed, null, configuration.TargetCharset, configuration.TargetCharsetFlags, BodyWriteDelegates.GetExtraData(configuration), false);
				obj = BodyWriteDelegates.GetRtfDecompressorOrUnicodeWriter(stream, createWriter);
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000F5660 File Offset: 0x000F3860
		private static object FromRtfToRtf(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter)
		{
			Stream stream = null;
			object obj = null;
			try
			{
				if (!createWriter && !configuration.IsContentTransformationNeeded(coreItem))
				{
					obj = new BodyCharsetDetectionStream(bodyStream, null, coreItem, BodyStreamFormat.RtfCompressed, null, configuration.TargetCharset, configuration.TargetCharsetFlags, null, false);
				}
				else
				{
					stream = new ConverterStream(bodyStream, new RtfToRtfCompressed(), ConverterStreamAccess.Write);
					stream = new BodyCharsetDetectionStream(stream, null, coreItem, BodyStreamFormat.RtfUncompressed, null, configuration.TargetCharset, configuration.TargetCharsetFlags, null, false);
					if (configuration.InjectPrefix != null || configuration.InjectSuffix != null)
					{
						stream = new ConverterStream(stream, new RtfToRtf
						{
							Header = configuration.InjectPrefix,
							Footer = configuration.InjectSuffix,
							HeaderFooterFormat = configuration.InjectionHeaderFooterFormat
						}, ConverterStreamAccess.Write);
					}
					obj = BodyWriteDelegates.GetRtfDecompressorOrUnicodeWriter(stream, createWriter);
				}
			}
			finally
			{
				if (obj == null && stream != null)
				{
					BodyWriteDelegates.SafeDisposeStream(stream);
				}
			}
			return obj;
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000F572C File Offset: 0x000F392C
		private static string GetExtraData(BodyWriteConfiguration configuration)
		{
			return configuration.InjectPrefix + configuration.InjectSuffix;
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000F5740 File Offset: 0x000F3940
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
				throw new InvalidOperationException("BodyWriteDelegates.GetFormatIndex");
			}
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000F5778 File Offset: 0x000F3978
		private static BodyWriteDelegates.ConversionCreator GetConversionMethod(BodyWriteConfiguration configuration)
		{
			int formatIndex = BodyWriteDelegates.GetFormatIndex(configuration.SourceFormat);
			int formatIndex2 = BodyWriteDelegates.GetFormatIndex(configuration.TargetFormat);
			return BodyWriteDelegates.conversionCreatorsTable[formatIndex][formatIndex2];
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000F57A8 File Offset: 0x000F39A8
		private static ConversionCallbackBase CreateConversionDelegateProvider(ICoreItem coreItem, BodyWriteConfiguration configuration)
		{
			ConversionCallbackBase result = null;
			if (configuration.SourceFormat == BodyFormat.ApplicationRtf && configuration.TargetFormat == BodyFormat.TextHtml && configuration.HtmlCallback == null)
			{
				configuration.HtmlCallback = new DefaultHtmlCallbacks(coreItem, false)
				{
					ClearInlineOnUnmarkedAttachments = false
				};
				result = configuration.HtmlCallback;
			}
			if (configuration.SourceFormat == BodyFormat.TextHtml && configuration.TargetFormat == BodyFormat.ApplicationRtf && configuration.ImageRenderingCallback == null)
			{
				configuration.RtfCallback = new DefaultRtfCallbacks(coreItem, false)
				{
					ClearInlineOnUnmarkedAttachments = false
				};
				result = configuration.RtfCallback;
			}
			return result;
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000F5868 File Offset: 0x000F3A68
		internal static TextWriter CreateWriter(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, out ConversionCallbackBase provider)
		{
			BodyWriteConfiguration writerConfiguration = new BodyWriteConfiguration(configuration);
			writerConfiguration.SourceCharset = ConvertUtils.UnicodeCharset;
			provider = BodyWriteDelegates.CreateConversionDelegateProvider(coreItem, writerConfiguration);
			return ConvertUtils.CallCtsWithReturnValue<TextWriter>(ExTraceGlobals.CcBodyTracer, "BodyWriteDelegates::CreateWriter", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				BodyWriteDelegates.ConversionCreator conversionMethod = BodyWriteDelegates.GetConversionMethod(writerConfiguration);
				return (TextWriter)conversionMethod(coreItem, writerConfiguration, bodyStream, true);
			});
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000F5918 File Offset: 0x000F3B18
		internal static Stream CreateStream(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, out ConversionCallbackBase provider)
		{
			BodyWriteConfiguration writerConfiguration = new BodyWriteConfiguration(configuration);
			provider = BodyWriteDelegates.CreateConversionDelegateProvider(coreItem, writerConfiguration);
			return ConvertUtils.CallCtsWithReturnValue<Stream>(ExTraceGlobals.CcBodyTracer, "BodyWriteDelegates::CreateStream", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				BodyWriteDelegates.ConversionCreator conversionMethod = BodyWriteDelegates.GetConversionMethod(configuration);
				return (Stream)conversionMethod(coreItem, writerConfiguration, bodyStream, false);
			});
		}

		// Token: 0x04001FBD RID: 8125
		private static BodyWriteDelegates.ConversionCreator[][] conversionCreatorsTable = new BodyWriteDelegates.ConversionCreator[][]
		{
			new BodyWriteDelegates.ConversionCreator[]
			{
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromTextToText),
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromTextToHtml),
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromTextToRtf)
			},
			new BodyWriteDelegates.ConversionCreator[]
			{
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromHtmlToText),
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromHtmlToHtml),
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromHtmlToRtf)
			},
			new BodyWriteDelegates.ConversionCreator[]
			{
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromRtfToText),
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromRtfToHtml),
				new BodyWriteDelegates.ConversionCreator(BodyWriteDelegates.FromRtfToRtf)
			}
		};

		// Token: 0x020005AC RID: 1452
		// (Invoke) Token: 0x06003B9C RID: 15260
		private delegate object ConversionCreator(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream bodyStream, bool createWriter);
	}
}
