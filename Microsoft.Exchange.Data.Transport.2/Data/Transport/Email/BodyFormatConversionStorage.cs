using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C7 RID: 199
	internal class BodyFormatConversionStorage : DataStorage
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0000A740 File Offset: 0x00008940
		public BodyFormatConversionStorage(DataStorage originalStorage, long originalStart, long originalEnd, InternalBodyFormat originalFormat, Charset originalCharset, InternalBodyFormat targetFormat, Charset targetCharset)
		{
			originalStorage.AddRef();
			this.originalStorage = originalStorage;
			this.originalStart = originalStart;
			this.originalEnd = originalEnd;
			this.originalFormat = originalFormat;
			this.originalCharset = originalCharset;
			this.targetFormat = targetFormat;
			this.targetCharset = targetCharset;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000A790 File Offset: 0x00008990
		public override Stream OpenReadStream(long start, long end)
		{
			base.ThrowIfDisposed();
			BodyFormatConversionStorage.BodyFormatConversion conversion = BodyFormatConversionStorage.GetConversion(this.originalFormat, this.targetFormat);
			TextConverter converter = null;
			Encoding inputEncoding = null;
			if (this.originalCharset != null)
			{
				inputEncoding = this.originalCharset.GetEncoding();
			}
			Encoding outputEncoding = null;
			if (this.targetCharset != null)
			{
				outputEncoding = this.targetCharset.GetEncoding();
			}
			Stream stream = this.originalStorage.OpenReadStream(this.originalStart, this.originalEnd);
			switch (conversion)
			{
			case BodyFormatConversionStorage.BodyFormatConversion.HtmlToText:
				converter = new HtmlToText
				{
					InputEncoding = inputEncoding,
					OutputEncoding = outputEncoding
				};
				break;
			case BodyFormatConversionStorage.BodyFormatConversion.HtmlToEnriched:
				converter = new HtmlToEnriched
				{
					InputEncoding = inputEncoding,
					OutputEncoding = outputEncoding
				};
				break;
			case BodyFormatConversionStorage.BodyFormatConversion.RtfCompressedToText:
				stream = new ConverterStream(stream, new RtfCompressedToRtf(), ConverterStreamAccess.Read);
				converter = new RtfToText
				{
					OutputEncoding = outputEncoding
				};
				break;
			case BodyFormatConversionStorage.BodyFormatConversion.TextToText:
				converter = new TextToText
				{
					InputEncoding = inputEncoding,
					OutputEncoding = outputEncoding
				};
				break;
			case BodyFormatConversionStorage.BodyFormatConversion.HtmlToHtml:
				converter = new HtmlToHtml
				{
					InputEncoding = inputEncoding,
					OutputEncoding = outputEncoding
				};
				break;
			case BodyFormatConversionStorage.BodyFormatConversion.EnrichedToText:
				converter = new EnrichedToText
				{
					InputEncoding = inputEncoding,
					OutputEncoding = outputEncoding
				};
				break;
			}
			return new ConverterStream(stream, converter, ConverterStreamAccess.Read);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000A8DE File Offset: 0x00008ADE
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				if (disposing && this.originalStorage != null)
				{
					this.originalStorage.Release();
					this.originalStorage = null;
				}
				this.originalCharset = null;
				this.targetCharset = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000A91A File Offset: 0x00008B1A
		private static BodyFormatConversionStorage.BodyFormatConversion GetConversion(InternalBodyFormat sourceFormat, InternalBodyFormat targetFormat)
		{
			if (sourceFormat == InternalBodyFormat.Html && targetFormat == InternalBodyFormat.Text)
			{
				return BodyFormatConversionStorage.BodyFormatConversion.HtmlToText;
			}
			if (sourceFormat == InternalBodyFormat.Html && targetFormat == InternalBodyFormat.Enriched)
			{
				return BodyFormatConversionStorage.BodyFormatConversion.HtmlToEnriched;
			}
			if (sourceFormat == InternalBodyFormat.Enriched && targetFormat == InternalBodyFormat.Text)
			{
				return BodyFormatConversionStorage.BodyFormatConversion.EnrichedToText;
			}
			if (sourceFormat == InternalBodyFormat.RtfCompressed && targetFormat == InternalBodyFormat.Text)
			{
				return BodyFormatConversionStorage.BodyFormatConversion.RtfCompressedToText;
			}
			if (sourceFormat == InternalBodyFormat.Text && targetFormat == InternalBodyFormat.Text)
			{
				return BodyFormatConversionStorage.BodyFormatConversion.TextToText;
			}
			if (sourceFormat == InternalBodyFormat.Html && targetFormat == InternalBodyFormat.Html)
			{
				return BodyFormatConversionStorage.BodyFormatConversion.HtmlToHtml;
			}
			return BodyFormatConversionStorage.BodyFormatConversion.None;
		}

		// Token: 0x0400028C RID: 652
		private DataStorage originalStorage;

		// Token: 0x0400028D RID: 653
		private long originalStart;

		// Token: 0x0400028E RID: 654
		private long originalEnd;

		// Token: 0x0400028F RID: 655
		private InternalBodyFormat originalFormat;

		// Token: 0x04000290 RID: 656
		private Charset originalCharset;

		// Token: 0x04000291 RID: 657
		private InternalBodyFormat targetFormat;

		// Token: 0x04000292 RID: 658
		private Charset targetCharset;

		// Token: 0x020000C8 RID: 200
		internal enum BodyFormatConversion
		{
			// Token: 0x04000294 RID: 660
			None,
			// Token: 0x04000295 RID: 661
			HtmlToText,
			// Token: 0x04000296 RID: 662
			HtmlToEnriched,
			// Token: 0x04000297 RID: 663
			RtfCompressedToText,
			// Token: 0x04000298 RID: 664
			TextToText,
			// Token: 0x04000299 RID: 665
			HtmlToHtml,
			// Token: 0x0400029A RID: 666
			EnrichedToText
		}
	}
}
