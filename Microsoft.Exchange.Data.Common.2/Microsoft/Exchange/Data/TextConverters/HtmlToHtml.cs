using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200015B RID: 347
	public class HtmlToHtml : TextConverter
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000725D0 File Offset: 0x000707D0
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x000725D8 File Offset: 0x000707D8
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000725E7 File Offset: 0x000707E7
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x000725EF File Offset: 0x000707EF
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

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x000725FE File Offset: 0x000707FE
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x00072606 File Offset: 0x00070806
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00072615 File Offset: 0x00070815
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0007261D File Offset: 0x0007081D
		public bool NormalizeHtml
		{
			get
			{
				return this.normalizeInputHtml;
			}
			set
			{
				base.AssertNotLocked();
				this.normalizeInputHtml = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0007262C File Offset: 0x0007082C
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00072634 File Offset: 0x00070834
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0007264D File Offset: 0x0007084D
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00072655 File Offset: 0x00070855
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

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00072664 File Offset: 0x00070864
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0007266C File Offset: 0x0007086C
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

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0007267B File Offset: 0x0007087B
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00072683 File Offset: 0x00070883
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00072692 File Offset: 0x00070892
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0007269A File Offset: 0x0007089A
		public bool OutputHtmlFragment
		{
			get
			{
				return this.outputFragment;
			}
			set
			{
				base.AssertNotLocked();
				this.outputFragment = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x000726A9 File Offset: 0x000708A9
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x000726B1 File Offset: 0x000708B1
		public bool FilterHtml
		{
			get
			{
				return this.filterHtml;
			}
			set
			{
				base.AssertNotLocked();
				this.filterHtml = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000726C0 File Offset: 0x000708C0
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x000726C8 File Offset: 0x000708C8
		public HtmlTagCallback HtmlTagCallback
		{
			get
			{
				return this.htmlCallback;
			}
			set
			{
				base.AssertNotLocked();
				this.htmlCallback = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x000726D7 File Offset: 0x000708D7
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x000726DF File Offset: 0x000708DF
		public int MaxCallbackTagLength
		{
			get
			{
				return this.maxHtmlTagSize;
			}
			set
			{
				base.AssertNotLocked();
				this.maxHtmlTagSize = value;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000726EE File Offset: 0x000708EE
		internal HtmlToHtml SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000726F8 File Offset: 0x000708F8
		internal HtmlToHtml SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00072702 File Offset: 0x00070902
		internal HtmlToHtml SetDetectEncodingFromMetaTag(bool value)
		{
			this.DetectEncodingFromMetaTag = value;
			return this;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0007270C File Offset: 0x0007090C
		internal HtmlToHtml SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00072716 File Offset: 0x00070916
		internal HtmlToHtml SetNormalizeHtml(bool value)
		{
			this.NormalizeHtml = value;
			return this;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00072720 File Offset: 0x00070920
		internal HtmlToHtml SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0007272A File Offset: 0x0007092A
		internal HtmlToHtml SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00072734 File Offset: 0x00070934
		internal HtmlToHtml SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0007273E File Offset: 0x0007093E
		internal HtmlToHtml SetFilterHtml(bool value)
		{
			this.FilterHtml = value;
			return this;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00072748 File Offset: 0x00070948
		internal HtmlToHtml SetHtmlTagCallback(HtmlTagCallback value)
		{
			this.HtmlTagCallback = value;
			return this;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00072752 File Offset: 0x00070952
		internal HtmlToHtml SetTestTruncateForCallback(bool value)
		{
			this.testTruncateForCallback = value;
			return this;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0007275C File Offset: 0x0007095C
		internal HtmlToHtml SetMaxCallbackTagLength(int value)
		{
			this.maxHtmlTagSize = value;
			return this;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00072766 File Offset: 0x00070966
		internal HtmlToHtml SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00072770 File Offset: 0x00070970
		internal HtmlToHtml SetOutputHtmlFragment(bool value)
		{
			this.OutputHtmlFragment = value;
			return this;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0007277A File Offset: 0x0007097A
		internal HtmlToHtml SetTestConvertHtmlFragment(bool value)
		{
			this.testConvertFragment = value;
			return this;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00072784 File Offset: 0x00070984
		internal HtmlToHtml SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			if (value)
			{
				this.maxHtmlTagSize = 123;
				this.testMaxHtmlTagAttributes = 5;
				this.testMaxHtmlNormalizerNesting = 10;
			}
			return this;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000727A8 File Offset: 0x000709A8
		internal HtmlToHtml SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000727B2 File Offset: 0x000709B2
		internal HtmlToHtml SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000727BC File Offset: 0x000709BC
		internal HtmlToHtml SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000727C6 File Offset: 0x000709C6
		internal HtmlToHtml SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000727D0 File Offset: 0x000709D0
		internal HtmlToHtml SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000727DA File Offset: 0x000709DA
		internal HtmlToHtml SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000727E4 File Offset: 0x000709E4
		internal HtmlToHtml SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000727EE File Offset: 0x000709EE
		internal HtmlToHtml SetTestMaxHtmlTagAttributes(int value)
		{
			this.testMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000727F8 File Offset: 0x000709F8
		internal HtmlToHtml SetTestMaxHtmlRestartOffset(int value)
		{
			this.testMaxHtmlRestartOffset = value;
			return this;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00072802 File Offset: 0x00070A02
		internal HtmlToHtml SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.testMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0007280C File Offset: 0x00070A0C
		internal HtmlToHtml SetTestNoNewLines(bool value)
		{
			this.testNoNewLines = value;
			return this;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00072816 File Offset: 0x00070A16
		internal HtmlToHtml SetSmallCssBlockThreshold(int value)
		{
			this.smallCssBlockThreshold = value;
			return this;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00072820 File Offset: 0x00070A20
		internal HtmlToHtml SetPreserveDisplayNoneStyle(bool value)
		{
			this.preserveDisplayNoneStyle = value;
			return this;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0007282C File Offset: 0x00070A2C
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, true);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0007291C File Offset: 0x00070B1C
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.maxHtmlTagSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00072980 File Offset: 0x00070B80
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.maxHtmlTagSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000729CC File Offset: 0x00070BCC
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00072A50 File Offset: 0x00070C50
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.maxHtmlTagSize, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00072AB4 File Offset: 0x00070CB4
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, true);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00072B20 File Offset: 0x00070D20
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.maxHtmlTagSize, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00072B6C File Offset: 0x00070D6C
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlInjection htmlInjection = null;
			HtmlInjection htmlInjection2 = null;
			IProducerConsumer result;
			try
			{
				if (this.injectHead != null || this.injectTail != null)
				{
					htmlInjection2 = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, this.filterHtml, this.htmlCallback, this.testBoundaryConditions, null, progressMonitor);
					htmlInjection = htmlInjection2;
					this.normalizeInputHtml = true;
				}
				if (this.filterHtml || this.outputFragment || this.htmlCallback != null)
				{
					this.normalizeInputHtml = true;
				}
				IHtmlParser parser2;
				if (this.normalizeInputHtml)
				{
					HtmlParser parser = new HtmlParser(input, this.detectEncodingFromMetaTag, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
					parser2 = new HtmlNormalizingParser(parser, htmlInjection, this.htmlCallback != null, this.testMaxHtmlNormalizerNesting, this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
					htmlInjection2 = null;
				}
				else
				{
					parser2 = new HtmlParser(input, this.detectEncodingFromMetaTag, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
				}
				HtmlWriter writer = new HtmlWriter(output, this.filterHtml, this.normalizeInputHtml && !this.testNoNewLines);
				result = new HtmlToHtmlConverter(parser2, writer, this.testConvertFragment, this.outputFragment, this.filterHtml, this.htmlCallback, this.testTruncateForCallback, htmlInjection != null && htmlInjection.HaveTail, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.smallCssBlockThreshold, this.preserveDisplayNoneStyle, progressMonitor);
			}
			finally
			{
				IDisposable disposable = htmlInjection2;
				if (disposable != null)
				{
					disposable.Dispose();
					htmlInjection2 = null;
				}
			}
			return result;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00072D0C File Offset: 0x00070F0C
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

		// Token: 0x04000FEA RID: 4074
		private Encoding inputEncoding;

		// Token: 0x04000FEB RID: 4075
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04000FEC RID: 4076
		private bool detectEncodingFromMetaTag = true;

		// Token: 0x04000FED RID: 4077
		private Encoding outputEncoding;

		// Token: 0x04000FEE RID: 4078
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04000FEF RID: 4079
		private bool normalizeInputHtml;

		// Token: 0x04000FF0 RID: 4080
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04000FF1 RID: 4081
		private string injectHead;

		// Token: 0x04000FF2 RID: 4082
		private string injectTail;

		// Token: 0x04000FF3 RID: 4083
		private bool filterHtml;

		// Token: 0x04000FF4 RID: 4084
		private HtmlTagCallback htmlCallback;

		// Token: 0x04000FF5 RID: 4085
		private bool testTruncateForCallback = true;

		// Token: 0x04000FF6 RID: 4086
		private bool testConvertFragment;

		// Token: 0x04000FF7 RID: 4087
		private bool outputFragment;

		// Token: 0x04000FF8 RID: 4088
		private int testMaxTokenRuns = 512;

		// Token: 0x04000FF9 RID: 4089
		private Stream testTraceStream;

		// Token: 0x04000FFA RID: 4090
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000FFB RID: 4091
		private int testTraceStopOnTokenNum;

		// Token: 0x04000FFC RID: 4092
		private Stream testNormalizerTraceStream;

		// Token: 0x04000FFD RID: 4093
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x04000FFE RID: 4094
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04000FFF RID: 4095
		private int maxHtmlTagSize = 32768;

		// Token: 0x04001000 RID: 4096
		private int testMaxHtmlTagAttributes = 64;

		// Token: 0x04001001 RID: 4097
		private int testMaxHtmlRestartOffset = 4096;

		// Token: 0x04001002 RID: 4098
		private int testMaxHtmlNormalizerNesting = 4096;

		// Token: 0x04001003 RID: 4099
		private int smallCssBlockThreshold = -1;

		// Token: 0x04001004 RID: 4100
		private bool preserveDisplayNoneStyle;

		// Token: 0x04001005 RID: 4101
		private bool testNoNewLines;
	}
}
