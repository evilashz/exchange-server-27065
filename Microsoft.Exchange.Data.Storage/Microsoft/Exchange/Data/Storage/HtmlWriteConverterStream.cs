using System;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005DA RID: 1498
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class HtmlWriteConverterStream : StreamBase
	{
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000FEBF2 File Offset: 0x000FCDF2
		internal HtmlWriteConverterStream(Stream outputStream, TextToHtml textToHtml) : this(outputStream, textToHtml, false)
		{
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000FEBFD File Offset: 0x000FCDFD
		internal HtmlWriteConverterStream(Stream outputStream, RtfToHtml rtfToHtml) : this(outputStream, rtfToHtml, false)
		{
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x000FEC08 File Offset: 0x000FCE08
		internal HtmlWriteConverterStream(Stream outputStream, HtmlToHtml toHtmlConverter, bool canSkipConversionOnMatchingCharset) : this(outputStream, toHtmlConverter, canSkipConversionOnMatchingCharset)
		{
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000FEC13 File Offset: 0x000FCE13
		private HtmlWriteConverterStream(Stream outputStream, TextConverter toHtmlConverter, bool canSkipConversionOnMatchingCharset) : base(StreamBase.Capabilities.Writable)
		{
			this.outputStream = outputStream;
			this.toHtmlConverter = toHtmlConverter;
			this.cachedContentStream = new MemoryStream();
			this.skipConversionOnMatchingCharset = canSkipConversionOnMatchingCharset;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000FEC3C File Offset: 0x000FCE3C
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<HtmlWriteConverterStream>(this);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000FEC44 File Offset: 0x000FCE44
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.cachedContentStream != null)
			{
				this.cachedContentStream.Write(buffer, offset, count);
				return;
			}
			this.writeStream.Write(buffer, offset, count);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x000FEC6C File Offset: 0x000FCE6C
		public void SetCharset(Charset detectedCharset)
		{
			if (this.toHtmlConverter == null)
			{
				throw new InvalidOperationException("HtmlWriteConverterStream.SetCharset() should only be called once.");
			}
			HtmlToHtml htmlToHtml = this.toHtmlConverter as HtmlToHtml;
			if (htmlToHtml != null)
			{
				if (CodePageMap.GetCodePage(htmlToHtml.InputEncoding) == detectedCharset.CodePage && this.skipConversionOnMatchingCharset)
				{
					this.writeStream = this.outputStream;
				}
				else
				{
					htmlToHtml.OutputEncoding = detectedCharset.GetEncoding();
					this.writeStream = new ConverterStream(this.outputStream, htmlToHtml, ConverterStreamAccess.Write);
				}
			}
			else
			{
				RtfToHtml rtfToHtml = this.toHtmlConverter as RtfToHtml;
				if (rtfToHtml != null)
				{
					rtfToHtml.OutputEncoding = detectedCharset.GetEncoding();
				}
				else
				{
					TextToHtml textToHtml = this.toHtmlConverter as TextToHtml;
					if (textToHtml != null)
					{
						textToHtml.OutputEncoding = detectedCharset.GetEncoding();
					}
				}
				this.writeStream = new ConverterStream(this.outputStream, this.toHtmlConverter, ConverterStreamAccess.Write);
			}
			this.toHtmlConverter = null;
			this.cachedContentStream.Position = 0L;
			Util.StreamHandler.CopyStreamData(this.cachedContentStream, this.writeStream);
			this.cachedContentStream.Dispose();
			this.cachedContentStream = null;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000FED6D File Offset: 0x000FCF6D
		public override void Flush()
		{
			this.writeStream.Flush();
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000FED7C File Offset: 0x000FCF7C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.writeStream != null)
				{
					this.writeStream.Dispose();
					this.writeStream = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0400211C RID: 8476
		private Stream outputStream;

		// Token: 0x0400211D RID: 8477
		private Stream writeStream;

		// Token: 0x0400211E RID: 8478
		private MemoryStream cachedContentStream;

		// Token: 0x0400211F RID: 8479
		private TextConverter toHtmlConverter;

		// Token: 0x04002120 RID: 8480
		private bool skipConversionOnMatchingCharset;
	}
}
