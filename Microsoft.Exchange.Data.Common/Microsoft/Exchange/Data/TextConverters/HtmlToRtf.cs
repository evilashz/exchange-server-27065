using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200015D RID: 349
	public class HtmlToRtf : TextConverter
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00072D50 File Offset: 0x00070F50
		public HtmlToRtf()
		{
			this.encapsulateMarkup = RegistryConfigManager.HtmlEncapsulationOverride;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00072DBE File Offset: 0x00070FBE
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00072DC6 File Offset: 0x00070FC6
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00072DD5 File Offset: 0x00070FD5
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00072DDD File Offset: 0x00070FDD
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00072DEC File Offset: 0x00070FEC
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00072DF4 File Offset: 0x00070FF4
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

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00072E03 File Offset: 0x00071003
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00072E0B File Offset: 0x0007100B
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

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00072E1A File Offset: 0x0007101A
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00072E22 File Offset: 0x00071022
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00072E31 File Offset: 0x00071031
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00072E39 File Offset: 0x00071039
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

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00072E48 File Offset: 0x00071048
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00072E50 File Offset: 0x00071050
		public bool EncapsulateHtmlMarkup
		{
			get
			{
				return this.encapsulateMarkup;
			}
			set
			{
				base.AssertNotLocked();
				this.encapsulateMarkup = value;
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00072E5F File Offset: 0x0007105F
		internal HtmlToRtf SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00072E69 File Offset: 0x00071069
		internal HtmlToRtf SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00072E73 File Offset: 0x00071073
		internal HtmlToRtf SetDetectEncodingFromMetaTag(bool value)
		{
			this.DetectEncodingFromMetaTag = value;
			return this;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00072E7D File Offset: 0x0007107D
		internal HtmlToRtf SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00072E87 File Offset: 0x00071087
		internal HtmlToRtf SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00072E91 File Offset: 0x00071091
		internal HtmlToRtf SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00072E9B File Offset: 0x0007109B
		internal HtmlToRtf SetImageRenderingCallback(ImageRenderingCallbackInternal value)
		{
			this.imageRenderingCallback = value;
			return this;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00072EA5 File Offset: 0x000710A5
		internal HtmlToRtf SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00072EAF File Offset: 0x000710AF
		internal HtmlToRtf SetTestBoundaryConditions(bool value)
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

		// Token: 0x06000E77 RID: 3703 RVA: 0x00072ED3 File Offset: 0x000710D3
		internal HtmlToRtf SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00072EDD File Offset: 0x000710DD
		internal HtmlToRtf SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00072EE7 File Offset: 0x000710E7
		internal HtmlToRtf SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00072EF1 File Offset: 0x000710F1
		internal HtmlToRtf SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00072EFB File Offset: 0x000710FB
		internal HtmlToRtf SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00072F05 File Offset: 0x00071105
		internal HtmlToRtf SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00072F0F File Offset: 0x0007110F
		internal HtmlToRtf SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00072F19 File Offset: 0x00071119
		internal HtmlToRtf SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00072F23 File Offset: 0x00071123
		internal HtmlToRtf SetTestFormatOutputTraceStream(Stream value)
		{
			this.testFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00072F2D File Offset: 0x0007112D
		internal HtmlToRtf SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00072F37 File Offset: 0x00071137
		internal HtmlToRtf SetTestMaxHtmlTagSize(int value)
		{
			this.testMaxHtmlTagSize = value;
			return this;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00072F41 File Offset: 0x00071141
		internal HtmlToRtf SetTestMaxHtmlTagAttributes(int value)
		{
			this.testMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00072F4B File Offset: 0x0007114B
		internal HtmlToRtf SetTestMaxHtmlRestartOffset(int value)
		{
			this.testMaxHtmlRestartOffset = value;
			return this;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00072F55 File Offset: 0x00071155
		internal HtmlToRtf SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.testMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00072F60 File Offset: 0x00071160
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			FormatOutput output2 = new RtfFormatOutput(output, true, true, this.testBoundaryConditions, this, this.imageRenderingCallback, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.inputEncoding);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00072FCC File Offset: 0x000711CC
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00072FD8 File Offset: 0x000711D8
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.testMaxHtmlTagSize, this.testBoundaryConditions, null);
			FormatOutput output2 = new RtfFormatOutput(output, true, false, this.testBoundaryConditions, this, this.imageRenderingCallback, this.testFormatTraceStream, this.testFormatOutputTraceStream, null);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00073031 File Offset: 0x00071231
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00073040 File Offset: 0x00071240
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			FormatOutput output = new RtfFormatOutput(converterStream, false, true, this.testBoundaryConditions, this, this.imageRenderingCallback, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.inputEncoding);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000730AC File Offset: 0x000712AC
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.testMaxHtmlTagSize, this.testBoundaryConditions, converterStream);
			FormatOutput output = new RtfFormatOutput(converterStream, false, false, this.testBoundaryConditions, this, this.imageRenderingCallback, this.testFormatTraceStream, this.testFormatOutputTraceStream, null);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00073105 File Offset: 0x00071305
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00073111 File Offset: 0x00071311
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00073120 File Offset: 0x00071320
		private IProducerConsumer CreateChain(ConverterInput input, FormatOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlInjection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor);
			}
			HtmlParser parser = new HtmlParser(input, this.detectEncodingFromMetaTag, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
			HtmlNormalizingParser parser2 = new HtmlNormalizingParser(parser, injection, false, this.testMaxHtmlNormalizerNesting, this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
			HtmlFormatConverter result;
			if (this.encapsulateMarkup)
			{
				result = new HtmlFormatConverterWithEncapsulation(parser2, output, this.encapsulateMarkup, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream, progressMonitor);
			}
			else
			{
				result = new HtmlFormatConverter(parser2, output, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream, progressMonitor);
			}
			return result;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00073204 File Offset: 0x00071404
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			if (parameterId == ConfigParameter.InputEncoding)
			{
				this.inputEncoding = (Encoding)val;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x04001006 RID: 4102
		private Encoding inputEncoding;

		// Token: 0x04001007 RID: 4103
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04001008 RID: 4104
		private bool detectEncodingFromMetaTag = true;

		// Token: 0x04001009 RID: 4105
		private HeaderFooterFormat injectionFormat;

		// Token: 0x0400100A RID: 4106
		private string injectHead;

		// Token: 0x0400100B RID: 4107
		private string injectTail;

		// Token: 0x0400100C RID: 4108
		private bool encapsulateMarkup;

		// Token: 0x0400100D RID: 4109
		private ImageRenderingCallbackInternal imageRenderingCallback;

		// Token: 0x0400100E RID: 4110
		private int testMaxTokenRuns = 512;

		// Token: 0x0400100F RID: 4111
		private Stream testTraceStream;

		// Token: 0x04001010 RID: 4112
		private bool testTraceShowTokenNum = true;

		// Token: 0x04001011 RID: 4113
		private int testTraceStopOnTokenNum;

		// Token: 0x04001012 RID: 4114
		private Stream testFormatTraceStream;

		// Token: 0x04001013 RID: 4115
		private Stream testFormatOutputTraceStream;

		// Token: 0x04001014 RID: 4116
		private Stream testFormatConverterTraceStream;

		// Token: 0x04001015 RID: 4117
		private Stream testNormalizerTraceStream;

		// Token: 0x04001016 RID: 4118
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x04001017 RID: 4119
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04001018 RID: 4120
		private int testMaxHtmlTagSize = 4096;

		// Token: 0x04001019 RID: 4121
		private int testMaxHtmlTagAttributes = 64;

		// Token: 0x0400101A RID: 4122
		private int testMaxHtmlRestartOffset = 4096;

		// Token: 0x0400101B RID: 4123
		private int testMaxHtmlNormalizerNesting = 4096;
	}
}
