using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000159 RID: 345
	public class TextToRtf : TextConverter
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000718DE File Offset: 0x0006FADE
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000718E6 File Offset: 0x0006FAE6
		public Encoding InputEncoding
		{
			get
			{
				return this.inputEncoding;
			}
			set
			{
				base.AssertNotLocked();
				this.inputEncoding = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000718F5 File Offset: 0x0006FAF5
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x000718FD File Offset: 0x0006FAFD
		public bool DetectEncodingFromByteOrderMark
		{
			get
			{
				return this.detectEncodingFromByteOrderMark;
			}
			set
			{
				base.AssertNotLocked();
				this.detectEncodingFromByteOrderMark = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0007190C File Offset: 0x0006FB0C
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00071914 File Offset: 0x0006FB14
		public bool Unwrap
		{
			get
			{
				return this.unwrapFlowed;
			}
			set
			{
				base.AssertNotLocked();
				this.unwrapFlowed = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00071923 File Offset: 0x0006FB23
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0007192B File Offset: 0x0006FB2B
		internal bool UnwrapDeleteSpace
		{
			get
			{
				return this.unwrapDelSp;
			}
			set
			{
				base.AssertNotLocked();
				this.unwrapDelSp = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0007193A File Offset: 0x0006FB3A
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00071942 File Offset: 0x0006FB42
		public HeaderFooterFormat HeaderFooterFormat
		{
			get
			{
				return this.injectionFormat;
			}
			set
			{
				base.AssertNotLocked();
				this.injectionFormat = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00071951 File Offset: 0x0006FB51
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00071959 File Offset: 0x0006FB59
		public string Header
		{
			get
			{
				return this.injectHead;
			}
			set
			{
				base.AssertNotLocked();
				this.injectHead = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00071968 File Offset: 0x0006FB68
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00071970 File Offset: 0x0006FB70
		public string Footer
		{
			get
			{
				return this.injectTail;
			}
			set
			{
				base.AssertNotLocked();
				this.injectTail = value;
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0007197F File Offset: 0x0006FB7F
		internal TextToRtf SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00071989 File Offset: 0x0006FB89
		internal TextToRtf SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00071993 File Offset: 0x0006FB93
		internal TextToRtf SetUnwrap(bool value)
		{
			this.Unwrap = value;
			return this;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0007199D File Offset: 0x0006FB9D
		internal TextToRtf SetUnwrapDeleteSpace(bool value)
		{
			this.UnwrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000719A7 File Offset: 0x0006FBA7
		internal TextToRtf SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000719B1 File Offset: 0x0006FBB1
		internal TextToRtf SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000719BB File Offset: 0x0006FBBB
		internal TextToRtf SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000719C5 File Offset: 0x0006FBC5
		internal TextToRtf SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000719CF File Offset: 0x0006FBCF
		internal TextToRtf SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000719DB File Offset: 0x0006FBDB
		internal TextToRtf SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000719E5 File Offset: 0x0006FBE5
		internal TextToRtf SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000719EF File Offset: 0x0006FBEF
		internal TextToRtf SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000719F9 File Offset: 0x0006FBF9
		internal TextToRtf SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00071A03 File Offset: 0x0006FC03
		internal TextToRtf SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00071A0D File Offset: 0x0006FC0D
		internal TextToRtf SetTestFormatOutputTraceStream(Stream value)
		{
			this.testFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00071A17 File Offset: 0x0006FC17
		internal TextToRtf SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00071A24 File Offset: 0x0006FC24
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			FormatOutput output2 = new RtfFormatOutput(output, true, false, this.testBoundaryConditions, this, null, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.inputEncoding);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00071A85 File Offset: 0x0006FC85
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00071A94 File Offset: 0x0006FC94
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			FormatOutput output2 = new RtfFormatOutput(output, true, false, this.testBoundaryConditions, this, null, this.testFormatTraceStream, this.testFormatOutputTraceStream, null);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00071AE7 File Offset: 0x0006FCE7
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00071AF4 File Offset: 0x0006FCF4
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			FormatOutput output = new RtfFormatOutput(converterStream, false, false, this.testBoundaryConditions, this, null, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.inputEncoding);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00071B58 File Offset: 0x0006FD58
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterStream);
			FormatOutput output = new RtfFormatOutput(converterStream, false, false, this.testBoundaryConditions, this, null, this.testFormatTraceStream, this.testFormatOutputTraceStream, null);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00071BAB File Offset: 0x0006FDAB
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00071BB7 File Offset: 0x0006FDB7
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00071BC4 File Offset: 0x0006FDC4
		private IProducerConsumer CreateChain(ConverterInput input, FormatOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			Injection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = ((this.injectionFormat == HeaderFooterFormat.Html) ? new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor) : new TextInjection(this.injectHead, this.injectTail, this.testBoundaryConditions, null, progressMonitor));
			}
			TextParser parser = new TextParser(input, this.unwrapFlowed, this.unwrapDelSp, this.testMaxTokenRuns, this.testBoundaryConditions);
			return new TextFormatConverter(parser, output, injection, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00071C70 File Offset: 0x0006FE70
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			if (parameterId == ConfigParameter.InputEncoding)
			{
				this.inputEncoding = (Encoding)val;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x04000FBC RID: 4028
		private Encoding inputEncoding;

		// Token: 0x04000FBD RID: 4029
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04000FBE RID: 4030
		private bool unwrapFlowed;

		// Token: 0x04000FBF RID: 4031
		private bool unwrapDelSp;

		// Token: 0x04000FC0 RID: 4032
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04000FC1 RID: 4033
		private string injectHead;

		// Token: 0x04000FC2 RID: 4034
		private string injectTail;

		// Token: 0x04000FC3 RID: 4035
		private int testMaxTokenRuns = 512;

		// Token: 0x04000FC4 RID: 4036
		private Stream testFormatTraceStream;

		// Token: 0x04000FC5 RID: 4037
		private Stream testFormatOutputTraceStream;

		// Token: 0x04000FC6 RID: 4038
		private Stream testFormatConverterTraceStream;

		// Token: 0x04000FC7 RID: 4039
		private Stream testTraceStream;

		// Token: 0x04000FC8 RID: 4040
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000FC9 RID: 4041
		private int testTraceStopOnTokenNum;
	}
}
