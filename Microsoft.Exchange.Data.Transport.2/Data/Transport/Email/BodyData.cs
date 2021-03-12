using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C6 RID: 198
	internal class BodyData
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000A2F1 File Offset: 0x000084F1
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000A2F9 File Offset: 0x000084F9
		internal BodyFormat BodyFormat
		{
			[DebuggerStepThrough]
			get
			{
				return this.format;
			}
			[DebuggerStepThrough]
			set
			{
				this.format = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000A302 File Offset: 0x00008502
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0000A30A File Offset: 0x0000850A
		internal InternalBodyFormat InternalBodyFormat
		{
			[DebuggerStepThrough]
			get
			{
				return this.internalFormat;
			}
			[DebuggerStepThrough]
			set
			{
				this.internalFormat = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000A313 File Offset: 0x00008513
		internal string CharsetName
		{
			get
			{
				if (this.charset == null || this.format == BodyFormat.None)
				{
					return string.Empty;
				}
				return this.charset.Name;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000A336 File Offset: 0x00008536
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000A33E File Offset: 0x0000853E
		internal Charset Charset
		{
			[DebuggerStepThrough]
			get
			{
				return this.charset;
			}
			[DebuggerStepThrough]
			set
			{
				this.charset = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000A348 File Offset: 0x00008548
		internal Encoding Encoding
		{
			get
			{
				Encoding result;
				if (this.charset != null && this.charset.TryGetEncoding(out result))
				{
					return result;
				}
				return Charset.DefaultMimeCharset.GetEncoding();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000A378 File Offset: 0x00008578
		internal bool HasContent
		{
			get
			{
				return null != this.storage;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000A386 File Offset: 0x00008586
		internal void Dispose()
		{
			if (this.storage != null)
			{
				this.storage.Release();
				this.storage = null;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000A3A2 File Offset: 0x000085A2
		internal void SetFormat(BodyFormat format, InternalBodyFormat internalFormat, Charset charset)
		{
			this.format = format;
			this.internalFormat = internalFormat;
			this.charset = charset;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000A3B9 File Offset: 0x000085B9
		internal void SetNewCharset(Charset charset)
		{
			this.charset = charset;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000A3C2 File Offset: 0x000085C2
		internal void SetStorage(DataStorage storage, long start, long end)
		{
			if (this.storage != null)
			{
				this.storage.Release();
			}
			if (storage != null)
			{
				storage.AddRef();
			}
			this.storage = storage;
			this.start = start;
			this.end = end;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000A3F8 File Offset: 0x000085F8
		internal void ValidateCharset(bool charsetWasDefaulted, Stream contentStream)
		{
			bool strongDefault = !charsetWasDefaulted && this.charset.CodePage != 20127;
			FeInboundCharsetDetector feInboundCharsetDetector = new FeInboundCharsetDetector(this.charset.CodePage, strongDefault, true, true, true);
			byte[] byteBuffer = ScratchPad.GetByteBuffer(1024);
			int num = 0;
			int num2 = 0;
			while (num2 < 4096 && (num = contentStream.Read(byteBuffer, 0, byteBuffer.Length)) != 0)
			{
				feInboundCharsetDetector.AddBytes(byteBuffer, 0, num, false);
				num2 += num;
			}
			if (num == 0)
			{
				feInboundCharsetDetector.AddBytes(byteBuffer, 0, 0, true);
			}
			int codePageChoice = feInboundCharsetDetector.GetCodePageChoice();
			if (codePageChoice != this.charset.CodePage)
			{
				this.charset = Charset.GetCharset(codePageChoice);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000A4A8 File Offset: 0x000086A8
		internal void GetStorage(InternalBodyFormat format, Charset charset, out DataStorage outStorage, out long outStart, out long outEnd)
		{
			if (format == this.internalFormat && (format == InternalBodyFormat.RtfCompressed || charset == null || charset == this.charset))
			{
				this.storage.AddRef();
				outStorage = this.storage;
				outStart = this.start;
				outEnd = this.end;
				return;
			}
			outStorage = new BodyFormatConversionStorage(this.storage, this.start, this.end, this.internalFormat, this.charset, format, charset);
			outStart = 0L;
			outEnd = long.MaxValue;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000A52C File Offset: 0x0000872C
		internal void ReleaseStorage()
		{
			this.SetStorage(null, 0L, 0L);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000A539 File Offset: 0x00008739
		internal Stream GetReadStream()
		{
			if (this.storage == null)
			{
				return DataStorage.NewEmptyReadStream();
			}
			return this.storage.OpenReadStream(this.start, this.end);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000A560 File Offset: 0x00008760
		internal Stream GetWriteStream()
		{
			TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
			this.SetStorage(temporaryDataStorage, 0L, long.MaxValue);
			return temporaryDataStorage.OpenWriteStream(true);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000A590 File Offset: 0x00008790
		internal Stream ConvertReadStreamFormat(Stream stream)
		{
			if (this.internalFormat == (InternalBodyFormat)this.format)
			{
				return stream;
			}
			TextConverter converter = null;
			if (this.internalFormat == InternalBodyFormat.RtfCompressed)
			{
				if (!(stream is RtfPreviewStream))
				{
					stream = new ConverterStream(stream, new RtfCompressedToRtf(), ConverterStreamAccess.Read);
				}
				if (BodyFormat.Rtf == this.BodyFormat)
				{
					return stream;
				}
				if (BodyFormat.Html == this.BodyFormat)
				{
					converter = new RtfToHtml
					{
						OutputEncoding = this.Encoding
					};
				}
				else
				{
					converter = new RtfToText
					{
						OutputEncoding = this.Encoding
					};
				}
			}
			else if (this.internalFormat == InternalBodyFormat.Enriched)
			{
				converter = new EnrichedToHtml
				{
					InputEncoding = this.Encoding,
					OutputEncoding = this.Encoding
				};
			}
			return new ConverterStream(stream, converter, ConverterStreamAccess.Read);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000A640 File Offset: 0x00008840
		internal Stream ConvertWriteStreamFormat(Stream stream, Charset writeStreamCharset)
		{
			if (this.internalFormat == (InternalBodyFormat)this.format && (writeStreamCharset == null || this.charset == writeStreamCharset))
			{
				return stream;
			}
			Charset charset = writeStreamCharset ?? this.charset;
			TextConverter converter;
			if (this.internalFormat == InternalBodyFormat.RtfCompressed)
			{
				stream = new ConverterStream(stream, new RtfToRtfCompressed(), ConverterStreamAccess.Write);
				if (BodyFormat.Rtf == this.format)
				{
					return stream;
				}
				converter = new TextToRtf
				{
					InputEncoding = charset.GetEncoding()
				};
			}
			else if (this.internalFormat == InternalBodyFormat.Enriched)
			{
				converter = new HtmlToEnriched
				{
					InputEncoding = charset.GetEncoding(),
					OutputEncoding = this.Encoding
				};
			}
			else if (this.internalFormat == InternalBodyFormat.Html)
			{
				converter = new HtmlToHtml
				{
					InputEncoding = charset.GetEncoding(),
					OutputEncoding = this.Encoding
				};
			}
			else
			{
				converter = new TextToText
				{
					InputEncoding = charset.GetEncoding(),
					OutputEncoding = this.Encoding
				};
			}
			return new ConverterStream(stream, converter, ConverterStreamAccess.Write);
		}

		// Token: 0x04000286 RID: 646
		private BodyFormat format;

		// Token: 0x04000287 RID: 647
		private InternalBodyFormat internalFormat;

		// Token: 0x04000288 RID: 648
		private Charset charset;

		// Token: 0x04000289 RID: 649
		private DataStorage storage;

		// Token: 0x0400028A RID: 650
		private long start;

		// Token: 0x0400028B RID: 651
		private long end;
	}
}
