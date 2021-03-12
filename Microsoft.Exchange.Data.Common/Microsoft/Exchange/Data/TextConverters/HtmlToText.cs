using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200015A RID: 346
	public class HtmlToText : TextConverter
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00071C98 File Offset: 0x0006FE98
		public HtmlToText()
		{
			this.mode = TextExtractionMode.NormalConversion;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00071D20 File Offset: 0x0006FF20
		public HtmlToText(TextExtractionMode mode)
		{
			this.mode = mode;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00071DA6 File Offset: 0x0006FFA6
		public TextExtractionMode TextExtractionMode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00071DAE File Offset: 0x0006FFAE
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00071DB6 File Offset: 0x0006FFB6
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00071DC5 File Offset: 0x0006FFC5
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x00071DCD File Offset: 0x0006FFCD
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00071DDC File Offset: 0x0006FFDC
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x00071DE4 File Offset: 0x0006FFE4
		public bool DetectEncodingFromMetaTag
		{
			get
			{
				return this.detectEncodingFromMetaTag;
			}
			set
			{
				base.AssertNotLocked();
				this.detectEncodingFromMetaTag = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00071DF3 File Offset: 0x0006FFF3
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x00071DFB File Offset: 0x0006FFFB
		public bool NormalizeHtml
		{
			get
			{
				return this.normalizeInputHtml;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.normalizeInputHtml = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00071E0A File Offset: 0x0007000A
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x00071E12 File Offset: 0x00070012
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

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00071E2B File Offset: 0x0007002B
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00071E33 File Offset: 0x00070033
		public bool Wrap
		{
			get
			{
				return this.wrapFlowed;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.wrapFlowed = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00071E42 File Offset: 0x00070042
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x00071E4A File Offset: 0x0007004A
		internal bool WrapDeleteSpace
		{
			get
			{
				return this.wrapDelSp;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.wrapDelSp = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00071E59 File Offset: 0x00070059
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x00071E61 File Offset: 0x00070061
		public bool HtmlEscapeOutput
		{
			get
			{
				return this.htmlEscape;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.htmlEscape = value;
				if (value)
				{
					this.fallbacks = true;
				}
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00071E7A File Offset: 0x0007007A
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00071E82 File Offset: 0x00070082
		public HeaderFooterFormat HeaderFooterFormat
		{
			get
			{
				return this.injectionFormat;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.injectionFormat = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00071E91 File Offset: 0x00070091
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00071E99 File Offset: 0x00070099
		public string Header
		{
			get
			{
				return this.injectHead;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.injectHead = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00071EA8 File Offset: 0x000700A8
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00071EB0 File Offset: 0x000700B0
		public string Footer
		{
			get
			{
				return this.injectTail;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.injectTail = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00071EBF File Offset: 0x000700BF
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00071EC7 File Offset: 0x000700C7
		public bool ShouldUseNarrowGapForPTagHtmlToTextConversion { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00071ED0 File Offset: 0x000700D0
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00071ED8 File Offset: 0x000700D8
		public bool OutputAnchorLinks
		{
			get
			{
				return this.outputAnchorLinks;
			}
			set
			{
				this.outputAnchorLinks = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00071EE1 File Offset: 0x000700E1
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00071EE9 File Offset: 0x000700E9
		public bool OutputImageLinks
		{
			get
			{
				return this.outputImageLinks;
			}
			set
			{
				this.outputImageLinks = value;
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00071EF2 File Offset: 0x000700F2
		internal HtmlToText SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00071EFC File Offset: 0x000700FC
		internal HtmlToText SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00071F06 File Offset: 0x00070106
		internal HtmlToText SetDetectEncodingFromMetaTag(bool value)
		{
			this.DetectEncodingFromMetaTag = value;
			return this;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00071F10 File Offset: 0x00070110
		internal HtmlToText SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00071F1A File Offset: 0x0007011A
		internal HtmlToText SetNormalizeHtml(bool value)
		{
			this.NormalizeHtml = value;
			return this;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00071F24 File Offset: 0x00070124
		internal HtmlToText SetWrap(bool value)
		{
			this.Wrap = value;
			return this;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00071F2E File Offset: 0x0007012E
		internal HtmlToText SetWrapDeleteSpace(bool value)
		{
			this.WrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00071F38 File Offset: 0x00070138
		internal HtmlToText SetUseFallbacks(bool value)
		{
			this.fallbacks = value;
			return this;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00071F42 File Offset: 0x00070142
		internal HtmlToText SetHtmlEscapeOutput(bool value)
		{
			this.HtmlEscapeOutput = value;
			return this;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00071F4C File Offset: 0x0007014C
		internal HtmlToText SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00071F56 File Offset: 0x00070156
		internal HtmlToText SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00071F60 File Offset: 0x00070160
		internal HtmlToText SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00071F6A File Offset: 0x0007016A
		internal HtmlToText SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00071F74 File Offset: 0x00070174
		internal HtmlToText SetImageRenderingCallback(ImageRenderingCallbackInternal value)
		{
			this.imageRenderingCallback = value;
			return this;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00071F7E File Offset: 0x0007017E
		internal HtmlToText SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			if (value)
			{
				this.testMaxHtmlTagSize = 123;
				this.testMaxHtmlTagAttributes = 5;
				this.testMaxHtmlNormalizerNesting = 10;
			}
			return this;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00071FA2 File Offset: 0x000701A2
		internal HtmlToText SetTestPreserveTrailingSpaces(bool value)
		{
			this.testPreserveTrailingSpaces = value;
			return this;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00071FAC File Offset: 0x000701AC
		internal HtmlToText SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00071FB6 File Offset: 0x000701B6
		internal HtmlToText SetTestTreatNbspAsBreakable(bool value)
		{
			this.testTreatNbspAsBreakable = value;
			return this;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00071FC0 File Offset: 0x000701C0
		internal HtmlToText SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00071FCA File Offset: 0x000701CA
		internal HtmlToText SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00071FD4 File Offset: 0x000701D4
		internal HtmlToText SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00071FDE File Offset: 0x000701DE
		internal HtmlToText SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00071FE8 File Offset: 0x000701E8
		internal HtmlToText SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00071FF2 File Offset: 0x000701F2
		internal HtmlToText SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00071FFC File Offset: 0x000701FC
		internal HtmlToText SetTestMaxHtmlTagSize(int value)
		{
			this.testMaxHtmlTagSize = value;
			return this;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00072006 File Offset: 0x00070206
		internal HtmlToText SetTestMaxHtmlTagAttributes(int value)
		{
			this.testMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00072010 File Offset: 0x00070210
		internal HtmlToText SetTestMaxHtmlRestartOffset(int value)
		{
			this.testMaxHtmlRestartOffset = value;
			return this;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0007201A File Offset: 0x0007021A
		internal HtmlToText SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.testMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00072024 File Offset: 0x00070224
		internal HtmlToText SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00072030 File Offset: 0x00070230
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000720B4 File Offset: 0x000702B4
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, true);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00072120 File Offset: 0x00070320
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.testMaxHtmlTagSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00072184 File Offset: 0x00070384
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.testMaxHtmlTagSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000721D0 File Offset: 0x000703D0
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00072254 File Offset: 0x00070454
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.testMaxHtmlTagSize, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000722B8 File Offset: 0x000704B8
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, true);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00072324 File Offset: 0x00070524
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.testMaxHtmlTagSize, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00072370 File Offset: 0x00070570
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlParser htmlParser = new HtmlParser(input, this.detectEncodingFromMetaTag, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
			if (this.mode == TextExtractionMode.ExtractText)
			{
				return new HtmlTextExtractionConverter(htmlParser, output, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
			}
			Injection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = ((this.injectionFormat == HeaderFooterFormat.Html) ? new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor) : new TextInjection(this.injectHead, this.injectTail, this.testBoundaryConditions, null, progressMonitor));
			}
			IHtmlParser parser = htmlParser;
			if (this.normalizeInputHtml)
			{
				if (this.injectionFormat == HeaderFooterFormat.Html)
				{
					parser = new HtmlNormalizingParser(htmlParser, (HtmlInjection)injection, false, this.testMaxHtmlNormalizerNesting, this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
					injection = null;
				}
				else
				{
					parser = new HtmlNormalizingParser(htmlParser, null, false, this.testMaxHtmlNormalizerNesting, this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
				}
			}
			TextOutput output2 = new TextOutput(output, this.wrapFlowed, this.wrapFlowed, 72, 78, this.imageRenderingCallback, this.fallbacks, this.htmlEscape, this.testPreserveTrailingSpaces, this.testFormatTraceStream);
			return new HtmlToTextConverter(parser, output2, injection, false, false, this.testTreatNbspAsBreakable, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.ShouldUseNarrowGapForPTagHtmlToTextConversion, this.OutputAnchorLinks, this.OutputImageLinks);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000724F8 File Offset: 0x000706F8
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

		// Token: 0x06000E1A RID: 3610 RVA: 0x00072539 File Offset: 0x00070739
		private void AssertNotLockedAndNotTextExtraction()
		{
			base.AssertNotLocked();
			if (this.mode == TextExtractionMode.ExtractText)
			{
				throw new InvalidOperationException(TextConvertersStrings.PropertyNotValidForTextExtractionMode);
			}
		}

		// Token: 0x04000FCA RID: 4042
		private TextExtractionMode mode;

		// Token: 0x04000FCB RID: 4043
		private Encoding inputEncoding;

		// Token: 0x04000FCC RID: 4044
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04000FCD RID: 4045
		private bool detectEncodingFromMetaTag = true;

		// Token: 0x04000FCE RID: 4046
		private bool normalizeInputHtml;

		// Token: 0x04000FCF RID: 4047
		private Encoding outputEncoding;

		// Token: 0x04000FD0 RID: 4048
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04000FD1 RID: 4049
		private bool wrapFlowed;

		// Token: 0x04000FD2 RID: 4050
		private bool wrapDelSp;

		// Token: 0x04000FD3 RID: 4051
		private bool fallbacks = true;

		// Token: 0x04000FD4 RID: 4052
		private bool htmlEscape;

		// Token: 0x04000FD5 RID: 4053
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04000FD6 RID: 4054
		private string injectHead;

		// Token: 0x04000FD7 RID: 4055
		private string injectTail;

		// Token: 0x04000FD8 RID: 4056
		private ImageRenderingCallbackInternal imageRenderingCallback;

		// Token: 0x04000FD9 RID: 4057
		private bool testPreserveTrailingSpaces;

		// Token: 0x04000FDA RID: 4058
		private int testMaxTokenRuns = 512;

		// Token: 0x04000FDB RID: 4059
		private bool testTreatNbspAsBreakable;

		// Token: 0x04000FDC RID: 4060
		private Stream testTraceStream;

		// Token: 0x04000FDD RID: 4061
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000FDE RID: 4062
		private int testTraceStopOnTokenNum;

		// Token: 0x04000FDF RID: 4063
		private Stream testNormalizerTraceStream;

		// Token: 0x04000FE0 RID: 4064
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x04000FE1 RID: 4065
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04000FE2 RID: 4066
		private int testMaxHtmlTagSize = 4096;

		// Token: 0x04000FE3 RID: 4067
		private int testMaxHtmlTagAttributes = 64;

		// Token: 0x04000FE4 RID: 4068
		private int testMaxHtmlRestartOffset = 4096;

		// Token: 0x04000FE5 RID: 4069
		private int testMaxHtmlNormalizerNesting = 4096;

		// Token: 0x04000FE6 RID: 4070
		private Stream testFormatTraceStream;

		// Token: 0x04000FE7 RID: 4071
		private bool outputAnchorLinks = true;

		// Token: 0x04000FE8 RID: 4072
		private bool outputImageLinks = true;
	}
}
