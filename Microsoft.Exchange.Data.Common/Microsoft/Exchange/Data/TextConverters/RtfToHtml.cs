using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000160 RID: 352
	public class RtfToHtml : TextConverter
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00073685 File Offset: 0x00071885
		public bool EncapsulatedHtml
		{
			get
			{
				return this.rtfEncapsulation == RtfEncapsulation.Html;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00073690 File Offset: 0x00071890
		public bool ConvertedFromText
		{
			get
			{
				return this.rtfEncapsulation == RtfEncapsulation.Text;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0007369B File Offset: 0x0007189B
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x000736A3 File Offset: 0x000718A3
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

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x000736BC File Offset: 0x000718BC
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x000736C4 File Offset: 0x000718C4
		public bool EnableHtmlDeencapsulation
		{
			get
			{
				return this.enableHtmlDeencapsulation;
			}
			set
			{
				base.AssertNotLocked();
				this.enableHtmlDeencapsulation = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000736D3 File Offset: 0x000718D3
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x000736DB File Offset: 0x000718DB
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

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000736EA File Offset: 0x000718EA
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x000736F2 File Offset: 0x000718F2
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00073701 File Offset: 0x00071901
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00073709 File Offset: 0x00071909
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

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00073718 File Offset: 0x00071918
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00073720 File Offset: 0x00071920
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

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0007372F File Offset: 0x0007192F
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00073737 File Offset: 0x00071937
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00073746 File Offset: 0x00071946
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x0007374E File Offset: 0x0007194E
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0007375D File Offset: 0x0007195D
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00073765 File Offset: 0x00071965
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00073774 File Offset: 0x00071974
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x0007377C File Offset: 0x0007197C
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

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0007378B File Offset: 0x0007198B
		internal RtfToHtml SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00073795 File Offset: 0x00071995
		internal RtfToHtml SetNormalizeHtml(bool value)
		{
			this.NormalizeHtml = value;
			return this;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0007379F File Offset: 0x0007199F
		internal RtfToHtml SetFilterHtml(bool value)
		{
			this.FilterHtml = value;
			return this;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000737A9 File Offset: 0x000719A9
		internal RtfToHtml SetHtmlTagCallback(HtmlTagCallback value)
		{
			this.HtmlTagCallback = value;
			return this;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000737B3 File Offset: 0x000719B3
		internal RtfToHtml SetTestTruncateForCallback(bool value)
		{
			this.testTruncateForCallback = value;
			return this;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000737BD File Offset: 0x000719BD
		internal RtfToHtml SetMaxCallbackTagLength(int value)
		{
			this.maxHtmlTagSize = value;
			return this;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000737C7 File Offset: 0x000719C7
		internal RtfToHtml SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000737D1 File Offset: 0x000719D1
		internal RtfToHtml SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000737DB File Offset: 0x000719DB
		internal RtfToHtml SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000737E5 File Offset: 0x000719E5
		internal RtfToHtml SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000737EF File Offset: 0x000719EF
		internal RtfToHtml SetTestBoundaryConditions(bool value)
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

		// Token: 0x06000EDD RID: 3805 RVA: 0x00073813 File Offset: 0x00071A13
		internal RtfToHtml SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0007381D File Offset: 0x00071A1D
		internal RtfToHtml SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00073827 File Offset: 0x00071A27
		internal RtfToHtml SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00073831 File Offset: 0x00071A31
		internal RtfToHtml SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0007383B File Offset: 0x00071A3B
		internal RtfToHtml SetTestFormatOutputTraceStream(Stream value)
		{
			this.testFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00073845 File Offset: 0x00071A45
		internal RtfToHtml SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0007384F File Offset: 0x00071A4F
		internal RtfToHtml SetTestHtmlTraceStream(Stream value)
		{
			this.testHtmlTraceStream = value;
			return this;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00073859 File Offset: 0x00071A59
		internal RtfToHtml SetTestHtmlTraceShowTokenNum(bool value)
		{
			this.testHtmlTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00073863 File Offset: 0x00071A63
		internal RtfToHtml SetTestHtmlTraceStopOnTokenNum(int value)
		{
			this.testHtmlTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0007386D File Offset: 0x00071A6D
		internal RtfToHtml SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00073877 File Offset: 0x00071A77
		internal RtfToHtml SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00073881 File Offset: 0x00071A81
		internal RtfToHtml SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0007388B File Offset: 0x00071A8B
		internal RtfToHtml SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00073895 File Offset: 0x00071A95
		internal RtfToHtml SetTestMaxHtmlTagAttributes(int value)
		{
			this.testMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0007389F File Offset: 0x00071A9F
		internal RtfToHtml SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.testMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000738A9 File Offset: 0x00071AA9
		internal RtfToHtml SetTestNoNewLines(bool value)
		{
			this.testNoNewLines = value;
			return this;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000738B3 File Offset: 0x00071AB3
		internal RtfToHtml SetSmallCssBlockThreshold(int value)
		{
			this.smallCssBlockThreshold = value;
			return this;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000738BD File Offset: 0x00071ABD
		internal RtfToHtml SetPreserveDisplayNoneStyle(bool value)
		{
			this.preserveDisplayNoneStyle = value;
			return this;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000738C7 File Offset: 0x00071AC7
		internal RtfToHtml SetExpansionSizeLimit(int value)
		{
			this.expansionSizeLimit = value;
			return this;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000738D1 File Offset: 0x00071AD1
		internal RtfToHtml SetExpansionSizeMultiple(int value)
		{
			this.expansionSizeMultiple = value;
			return this;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000738DC File Offset: 0x00071ADC
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.GetEncoding("Windows-1252") : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(converterStream, true, output2, converterStream);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00073924 File Offset: 0x00071B24
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(converterStream, true, output2, converterStream);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00073944 File Offset: 0x00071B44
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00073950 File Offset: 0x00071B50
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0007395C File Offset: 0x00071B5C
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.GetEncoding("Windows-1252") : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, false, output, converterStream);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000739A3 File Offset: 0x00071BA3
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000739B0 File Offset: 0x00071BB0
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input, false, output, converterReader);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000739D0 File Offset: 0x00071BD0
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000739DC File Offset: 0x00071BDC
		private IProducerConsumer CreateChain(Stream input, bool push, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			this.reportBytes = new ReportBytes(this.expansionSizeLimit, this.expansionSizeMultiple);
			output.ReportBytes = this.reportBytes;
			RtfParser parser;
			if (!this.enableHtmlDeencapsulation)
			{
				parser = new RtfParser(input, push, base.InputStreamBufferSize, this.testBoundaryConditions, push ? null : progressMonitor, this.reportBytes);
				HtmlInjection injection = null;
				if (this.injectHead != null || this.injectTail != null)
				{
					injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, this.filterHtml, this.htmlCallback, this.testBoundaryConditions, null, progressMonitor);
				}
				HtmlWriter writer = new HtmlWriter(output, this.filterHtml, !this.testNoNewLines);
				FormatOutput output2 = new HtmlFormatOutput(writer, injection, this.outputFragment, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.filterHtml, this.htmlCallback, true);
				return new RtfFormatConverter(parser, output2, null, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream);
			}
			RtfPreviewStream rtfPreviewStream = input as RtfPreviewStream;
			if (!push && rtfPreviewStream != null && rtfPreviewStream.Parser != null && rtfPreviewStream.InternalPosition == 0 && rtfPreviewStream.InputRtfStream != null)
			{
				rtfPreviewStream.InternalPosition = int.MaxValue;
				parser = new RtfParser(rtfPreviewStream.InputRtfStream, base.InputStreamBufferSize, this.testBoundaryConditions, push ? null : progressMonitor, rtfPreviewStream.Parser, this.reportBytes);
				return this.CreateChain(rtfPreviewStream.Encapsulation, parser, output, progressMonitor);
			}
			parser = new RtfParser(input, push, base.InputStreamBufferSize, this.testBoundaryConditions, push ? null : progressMonitor, this.reportBytes);
			return new RtfToHtmlAdapter(parser, output, this, progressMonitor);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00073B80 File Offset: 0x00071D80
		internal IProducerConsumer CreateChain(RtfEncapsulation encapsulation, RtfParser parser, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.rtfEncapsulation = encapsulation;
			if (this.reportBytes == null)
			{
				throw new InvalidOperationException("I have an RtfParser but no ReportBytes.");
			}
			output.ReportBytes = this.reportBytes;
			IProducerConsumer result;
			if (encapsulation != RtfEncapsulation.Html)
			{
				HtmlInjection injection = null;
				if (this.injectHead != null || this.injectTail != null)
				{
					injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, this.filterHtml, this.htmlCallback, this.testBoundaryConditions, null, progressMonitor);
				}
				HtmlWriter writer = new HtmlWriter(output, this.filterHtml, !this.testNoNewLines);
				FormatOutput output2 = new HtmlFormatOutput(writer, injection, this.outputFragment, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.filterHtml, this.htmlCallback, true);
				RtfFormatConverter rtfFormatConverter = new RtfFormatConverter(parser, output2, null, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream);
				result = rtfFormatConverter;
			}
			else
			{
				HtmlInjection htmlInjection = null;
				HtmlInjection htmlInjection2 = null;
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
					HtmlInRtfExtractingInput input = new HtmlInRtfExtractingInput(parser, this.maxHtmlTagSize, this.testBoundaryConditions, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
					IHtmlParser parser3;
					if (this.normalizeInputHtml)
					{
						HtmlParser parser2 = new HtmlParser(input, false, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
						parser3 = new HtmlNormalizingParser(parser2, htmlInjection, this.htmlCallback != null, this.testMaxHtmlNormalizerNesting, this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
						htmlInjection2 = null;
					}
					else
					{
						parser3 = new HtmlParser(input, false, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
					}
					HtmlWriter writer2 = new HtmlWriter(output, this.filterHtml, this.normalizeInputHtml && !this.testNoNewLines);
					result = new HtmlToHtmlConverter(parser3, writer2, false, this.outputFragment, this.filterHtml, this.htmlCallback, this.testTruncateForCallback, htmlInjection != null && htmlInjection.HaveTail, this.testHtmlTraceStream, this.testHtmlTraceShowTokenNum, this.testHtmlTraceStopOnTokenNum, this.smallCssBlockThreshold, this.preserveDisplayNoneStyle, progressMonitor);
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
			}
			return result;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00073E18 File Offset: 0x00072018
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			switch (parameterId)
			{
			case ConfigParameter.OutputEncoding:
				this.outputEncoding = (Encoding)val;
				break;
			case ConfigParameter.RtfEncapsulation:
				this.rtfEncapsulation = (RtfEncapsulation)val;
				break;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x0400102F RID: 4143
		private Encoding outputEncoding;

		// Token: 0x04001030 RID: 4144
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04001031 RID: 4145
		private bool enableHtmlDeencapsulation = true;

		// Token: 0x04001032 RID: 4146
		private bool normalizeInputHtml;

		// Token: 0x04001033 RID: 4147
		private bool filterHtml;

		// Token: 0x04001034 RID: 4148
		private HtmlTagCallback htmlCallback;

		// Token: 0x04001035 RID: 4149
		private bool testTruncateForCallback = true;

		// Token: 0x04001036 RID: 4150
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04001037 RID: 4151
		private string injectHead;

		// Token: 0x04001038 RID: 4152
		private string injectTail;

		// Token: 0x04001039 RID: 4153
		private bool outputFragment;

		// Token: 0x0400103A RID: 4154
		private RtfEncapsulation rtfEncapsulation;

		// Token: 0x0400103B RID: 4155
		private Stream testTraceStream;

		// Token: 0x0400103C RID: 4156
		private bool testTraceShowTokenNum = true;

		// Token: 0x0400103D RID: 4157
		private int testTraceStopOnTokenNum;

		// Token: 0x0400103E RID: 4158
		private Stream testFormatTraceStream;

		// Token: 0x0400103F RID: 4159
		private Stream testFormatOutputTraceStream;

		// Token: 0x04001040 RID: 4160
		private Stream testFormatConverterTraceStream;

		// Token: 0x04001041 RID: 4161
		private Stream testHtmlTraceStream;

		// Token: 0x04001042 RID: 4162
		private bool testHtmlTraceShowTokenNum = true;

		// Token: 0x04001043 RID: 4163
		private int testHtmlTraceStopOnTokenNum;

		// Token: 0x04001044 RID: 4164
		private Stream testNormalizerTraceStream;

		// Token: 0x04001045 RID: 4165
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x04001046 RID: 4166
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04001047 RID: 4167
		private int testMaxTokenRuns = 512;

		// Token: 0x04001048 RID: 4168
		private int maxHtmlTagSize = 32768;

		// Token: 0x04001049 RID: 4169
		private int testMaxHtmlTagAttributes = 64;

		// Token: 0x0400104A RID: 4170
		private int testMaxHtmlNormalizerNesting = 4096;

		// Token: 0x0400104B RID: 4171
		private bool testNoNewLines;

		// Token: 0x0400104C RID: 4172
		private int smallCssBlockThreshold = -1;

		// Token: 0x0400104D RID: 4173
		private bool preserveDisplayNoneStyle;

		// Token: 0x0400104E RID: 4174
		private ReportBytes reportBytes;

		// Token: 0x0400104F RID: 4175
		private int expansionSizeLimit;

		// Token: 0x04001050 RID: 4176
		private int expansionSizeMultiple;
	}
}
