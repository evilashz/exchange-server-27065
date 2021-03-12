using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000157 RID: 343
	public class TextToText : TextConverter
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x00070BEF File Offset: 0x0006EDEF
		public TextToText()
		{
			this.mode = TextToTextConversionMode.Normal;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00070C25 File Offset: 0x0006EE25
		public TextToText(TextToTextConversionMode mode)
		{
			this.mode = mode;
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00070C5B File Offset: 0x0006EE5B
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00070C63 File Offset: 0x0006EE63
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00070C72 File Offset: 0x0006EE72
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x00070C7A File Offset: 0x0006EE7A
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00070C89 File Offset: 0x0006EE89
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00070C91 File Offset: 0x0006EE91
		public bool Unwrap
		{
			get
			{
				return this.unwrapFlowed;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.unwrapFlowed = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00070CA8 File Offset: 0x0006EEA8
		internal bool UnwrapDeleteSpace
		{
			get
			{
				return this.unwrapDelSp;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.unwrapDelSp = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00070CB7 File Offset: 0x0006EEB7
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00070CBF File Offset: 0x0006EEBF
		public Encoding OutputEncoding
		{
			get
			{
				return this.outputEncoding;
			}
			set
			{
				base.AssertNotLocked();
				this.outputEncoding = value;
				this.outputEncodingSameAsInput = (value == null);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00070CD8 File Offset: 0x0006EED8
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x00070CE0 File Offset: 0x0006EEE0
		public bool Wrap
		{
			get
			{
				return this.wrapFlowed;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.wrapFlowed = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00070CEF File Offset: 0x0006EEEF
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00070CF7 File Offset: 0x0006EEF7
		internal bool WrapDeleteSpace
		{
			get
			{
				return this.wrapDelSp;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.wrapDelSp = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00070D06 File Offset: 0x0006EF06
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x00070D0E File Offset: 0x0006EF0E
		public bool HtmlEscapeOutput
		{
			get
			{
				return this.htmlEscape;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.htmlEscape = value;
				if (value)
				{
					this.fallbacks = true;
				}
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00070D27 File Offset: 0x0006EF27
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00070D2F File Offset: 0x0006EF2F
		public HeaderFooterFormat HeaderFooterFormat
		{
			get
			{
				return this.injectionFormat;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.injectionFormat = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00070D3E File Offset: 0x0006EF3E
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00070D46 File Offset: 0x0006EF46
		public string Header
		{
			get
			{
				return this.injectHead;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.injectHead = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00070D55 File Offset: 0x0006EF55
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00070D5D File Offset: 0x0006EF5D
		public string Footer
		{
			get
			{
				return this.injectTail;
			}
			set
			{
				this.AssertNotLockedAndNotSimpleCodepageConversion();
				this.injectTail = value;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00070D6C File Offset: 0x0006EF6C
		internal TextToText SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00070D76 File Offset: 0x0006EF76
		internal TextToText SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00070D80 File Offset: 0x0006EF80
		internal TextToText SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00070D8A File Offset: 0x0006EF8A
		internal TextToText SetUnwrap(bool value)
		{
			this.Unwrap = value;
			return this;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00070D94 File Offset: 0x0006EF94
		internal TextToText SetUnwrapDeleteSpace(bool value)
		{
			this.UnwrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00070D9E File Offset: 0x0006EF9E
		internal TextToText SetWrap(bool value)
		{
			this.Wrap = value;
			return this;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00070DA8 File Offset: 0x0006EFA8
		internal TextToText SetWrapDeleteSpace(bool value)
		{
			this.WrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00070DB2 File Offset: 0x0006EFB2
		internal TextToText SetUseFallbacks(bool value)
		{
			this.fallbacks = value;
			return this;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00070DBC File Offset: 0x0006EFBC
		internal TextToText SetHtmlEscapeOutput(bool value)
		{
			this.HtmlEscapeOutput = value;
			return this;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00070DC6 File Offset: 0x0006EFC6
		internal TextToText SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		internal TextToText SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00070DDA File Offset: 0x0006EFDA
		internal TextToText SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		internal TextToText SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00070DEE File Offset: 0x0006EFEE
		internal TextToText SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00070DFA File Offset: 0x0006EFFA
		internal TextToText SetTestPreserveTrailingSpaces(bool value)
		{
			this.testPreserveTrailingSpaces = value;
			return this;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00070E04 File Offset: 0x0006F004
		internal TextToText SetTestTreatNbspAsBreakable(bool value)
		{
			this.testTreatNbspAsBreakable = value;
			return this;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00070E0E File Offset: 0x0006F00E
		internal TextToText SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00070E18 File Offset: 0x0006F018
		internal TextToText SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00070E22 File Offset: 0x0006F022
		internal TextToText SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00070E2C File Offset: 0x0006F02C
		internal TextToText SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00070E36 File Offset: 0x0006F036
		internal TextToText SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00070E40 File Offset: 0x0006F040
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00070EC0 File Offset: 0x0006F0C0
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00070F28 File Offset: 0x0006F128
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00070F88 File Offset: 0x0006F188
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00070FD4 File Offset: 0x0006F1D4
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00071054 File Offset: 0x0006F254
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000710B4 File Offset: 0x0006F2B4
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, true);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0007111C File Offset: 0x0006F31C
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00071168 File Offset: 0x0006F368
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			if (this.mode == TextToTextConversionMode.ConvertCodePageOnly)
			{
				return new TextCodePageConverter(input, output);
			}
			Injection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = ((this.injectionFormat == HeaderFooterFormat.Html) ? new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor) : new TextInjection(this.injectHead, this.injectTail, this.testBoundaryConditions, null, progressMonitor));
			}
			TextParser parser = new TextParser(input, this.unwrapFlowed, this.unwrapDelSp, this.testMaxTokenRuns, this.testBoundaryConditions);
			TextOutput output2 = new TextOutput(output, this.wrapFlowed, this.wrapFlowed, 72, 78, null, this.fallbacks, this.htmlEscape, this.testPreserveTrailingSpaces, this.testFormatTraceStream);
			return new TextToTextConverter(parser, output2, injection, false, this.testTreatNbspAsBreakable, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00071254 File Offset: 0x0006F454
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			switch (parameterId)
			{
			case ConfigParameter.InputEncoding:
				this.inputEncoding = (Encoding)val;
				break;
			case ConfigParameter.OutputEncoding:
				this.outputEncoding = (Encoding)val;
				break;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00071295 File Offset: 0x0006F495
		private void AssertNotLockedAndNotSimpleCodepageConversion()
		{
			base.AssertNotLocked();
			if (this.mode == TextToTextConversionMode.ConvertCodePageOnly)
			{
				throw new InvalidOperationException(TextConvertersStrings.PropertyNotValidForCodepageConversionMode);
			}
		}

		// Token: 0x04000F94 RID: 3988
		private TextToTextConversionMode mode;

		// Token: 0x04000F95 RID: 3989
		private Encoding inputEncoding;

		// Token: 0x04000F96 RID: 3990
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04000F97 RID: 3991
		private bool unwrapFlowed;

		// Token: 0x04000F98 RID: 3992
		private bool unwrapDelSp;

		// Token: 0x04000F99 RID: 3993
		private Encoding outputEncoding;

		// Token: 0x04000F9A RID: 3994
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04000F9B RID: 3995
		private bool wrapFlowed;

		// Token: 0x04000F9C RID: 3996
		private bool wrapDelSp;

		// Token: 0x04000F9D RID: 3997
		private bool fallbacks = true;

		// Token: 0x04000F9E RID: 3998
		private bool htmlEscape;

		// Token: 0x04000F9F RID: 3999
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04000FA0 RID: 4000
		private string injectHead;

		// Token: 0x04000FA1 RID: 4001
		private string injectTail;

		// Token: 0x04000FA2 RID: 4002
		private bool testPreserveTrailingSpaces;

		// Token: 0x04000FA3 RID: 4003
		private int testMaxTokenRuns = 512;

		// Token: 0x04000FA4 RID: 4004
		private bool testTreatNbspAsBreakable;

		// Token: 0x04000FA5 RID: 4005
		private Stream testTraceStream;

		// Token: 0x04000FA6 RID: 4006
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000FA7 RID: 4007
		private int testTraceStopOnTokenNum;

		// Token: 0x04000FA8 RID: 4008
		private Stream testFormatTraceStream;
	}
}
