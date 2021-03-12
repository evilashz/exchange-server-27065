using System;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000599 RID: 1433
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyCharsetDetectionStream : StreamBase
	{
		// Token: 0x06003AAC RID: 15020 RVA: 0x000F1404 File Offset: 0x000EF604
		public BodyCharsetDetectionStream(Stream outputStream, BodyCharsetDetectionStream.DetectCharsetCallback callback, ICoreItem coreItem, BodyStreamFormat format, Charset contentCharset, Charset userCharset, BodyCharsetFlags charsetFlags, string extraData, bool trustHtmlMetaTag) : base(StreamBase.Capabilities.Writable)
		{
			this.outputStream = outputStream;
			this.callback = callback;
			this.coreItem = coreItem;
			this.userCharset = userCharset;
			this.charsetFlags = charsetFlags;
			Charset charset;
			this.isDetectingCharset = !this.coreItem.CharsetDetector.IsItemCharsetKnownWithoutDetection(charsetFlags, userCharset, out charset);
			if (this.isDetectingCharset)
			{
				this.CreateDetectionStream(format, contentCharset, extraData, trustHtmlMetaTag);
				return;
			}
			this.CalculateCharset();
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000F147C File Offset: 0x000EF67C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseDetectorConversionStream();
					if (this.outputStream != null)
					{
						this.outputStream.Dispose();
						this.outputStream = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000F14C8 File Offset: 0x000EF6C8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BodyCharsetDetectionStream>(this);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000F14D0 File Offset: 0x000EF6D0
		private void CreateDetectionStream(BodyStreamFormat format, Charset contentCharset, string extraData, bool trustHtmlMetaTag)
		{
			this.detectorCache = new PooledMemoryStream(8192);
			if (extraData != null)
			{
				byte[] bytes = ConvertUtils.UnicodeEncoding.GetBytes(extraData);
				this.detectorCache.Write(bytes, 0, bytes.Length);
			}
			switch (format)
			{
			case BodyStreamFormat.Text:
			{
				if (contentCharset.CodePage == 1200)
				{
					this.detectorConversionStream = new StreamWrapper(this.detectorCache, false);
					return;
				}
				TextToText textToText = new TextToText(TextToTextConversionMode.ConvertCodePageOnly);
				textToText.InputEncoding = contentCharset.GetEncoding();
				textToText.OutputEncoding = ConvertUtils.UnicodeEncoding;
				textToText.OutputStreamBufferSize = 1024;
				textToText.InputStreamBufferSize = 1024;
				this.detectorConversionStream = new ConverterStream(new StreamWrapper(this.detectorCache, false), textToText, ConverterStreamAccess.Write);
				return;
			}
			case BodyStreamFormat.Html:
			{
				HtmlToText htmlToText = new HtmlToText(TextExtractionMode.ExtractText);
				htmlToText.InputEncoding = contentCharset.GetEncoding();
				htmlToText.OutputEncoding = ConvertUtils.UnicodeEncoding;
				htmlToText.DetectEncodingFromMetaTag = trustHtmlMetaTag;
				htmlToText.OutputStreamBufferSize = 1024;
				htmlToText.InputStreamBufferSize = 1024;
				this.detectorConversionStream = new ConverterStream(new StreamWrapper(this.detectorCache, false), htmlToText, ConverterStreamAccess.Write);
				return;
			}
			case BodyStreamFormat.RtfCompressed:
			case BodyStreamFormat.RtfUncompressed:
			{
				RtfToText rtfToText = new RtfToText(TextExtractionMode.ExtractText);
				rtfToText.OutputEncoding = ConvertUtils.UnicodeEncoding;
				rtfToText.OutputStreamBufferSize = 1024;
				rtfToText.InputStreamBufferSize = 1024;
				this.detectorConversionStream = new ConverterStream(new StreamWrapper(this.detectorCache, false), rtfToText, ConverterStreamAccess.Write);
				if (format == BodyStreamFormat.RtfCompressed)
				{
					RtfCompressedToRtf rtfCompressedToRtf = new RtfCompressedToRtf();
					rtfCompressedToRtf.OutputStreamBufferSize = 1024;
					rtfCompressedToRtf.InputStreamBufferSize = 1024;
					this.detectorConversionStream = new ConverterStream(this.detectorConversionStream, rtfCompressedToRtf, ConverterStreamAccess.Write);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000F1664 File Offset: 0x000EF864
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.isDetectingCharset)
			{
				this.detectorConversionStream.Write(buffer, offset, count);
				if (this.detectorCache.Length >= 32768L)
				{
					this.OnBufferFull();
				}
			}
			if (this.outputStream != null)
			{
				this.outputStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000F16B6 File Offset: 0x000EF8B6
		public override void Flush()
		{
			this.CloseDetectorConversionStream();
			if (this.outputStream != null)
			{
				this.outputStream.Flush();
			}
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		private void CloseDetectorConversionStream()
		{
			if (this.isDetectingCharset)
			{
				this.OnBufferFull();
			}
			if (this.detectorConversionStream != null)
			{
				try
				{
					this.detectorConversionStream.Dispose();
					this.detectorConversionStream = null;
				}
				catch (TextConvertersException)
				{
				}
			}
			if (this.detectorCache != null)
			{
				this.detectorCache.Dispose();
				this.detectorCache = null;
			}
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000F1738 File Offset: 0x000EF938
		private void CalculateCharset()
		{
			char[] cachedBodyData = null;
			if (this.isDetectingCharset)
			{
				this.detectorCache.Position = 0L;
				cachedBodyData = ConvertUtils.UnicodeEncoding.GetChars(this.detectorCache.GetBuffer(), 0, (int)this.detectorCache.Length);
				this.isDetectingCharset = false;
			}
			Charset detectedCharset = this.coreItem.CharsetDetector.SetCachedBodyDataAndDetectCharset(cachedBodyData, this.userCharset, this.charsetFlags);
			if (this.callback != null)
			{
				this.callback(detectedCharset);
			}
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F17B8 File Offset: 0x000EF9B8
		private void OnBufferFull()
		{
			if (!this.isDetectingCharset)
			{
				return;
			}
			this.CalculateCharset();
			this.CloseDetectorConversionStream();
		}

		// Token: 0x04001F72 RID: 8050
		internal const int DetectionDataSize = 32768;

		// Token: 0x04001F73 RID: 8051
		private const int InitialDetectionBufferSize = 8192;

		// Token: 0x04001F74 RID: 8052
		private ICoreItem coreItem;

		// Token: 0x04001F75 RID: 8053
		private Stream detectorConversionStream;

		// Token: 0x04001F76 RID: 8054
		private PooledMemoryStream detectorCache;

		// Token: 0x04001F77 RID: 8055
		private Stream outputStream;

		// Token: 0x04001F78 RID: 8056
		private Charset userCharset;

		// Token: 0x04001F79 RID: 8057
		private BodyCharsetFlags charsetFlags;

		// Token: 0x04001F7A RID: 8058
		private BodyCharsetDetectionStream.DetectCharsetCallback callback;

		// Token: 0x04001F7B RID: 8059
		private bool isDetectingCharset;

		// Token: 0x0200059A RID: 1434
		// (Invoke) Token: 0x06003AB6 RID: 15030
		public delegate void DetectCharsetCallback(Charset detectedCharset);
	}
}
