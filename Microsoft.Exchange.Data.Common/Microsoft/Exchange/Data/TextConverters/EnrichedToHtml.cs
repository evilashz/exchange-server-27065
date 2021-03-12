using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Enriched;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000164 RID: 356
	public class EnrichedToHtml : TextConverter
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0007428C File Offset: 0x0007248C
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00074294 File Offset: 0x00072494
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

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x000742A3 File Offset: 0x000724A3
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x000742AB File Offset: 0x000724AB
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x000742BA File Offset: 0x000724BA
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x000742C2 File Offset: 0x000724C2
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

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x000742DB File Offset: 0x000724DB
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x000742E3 File Offset: 0x000724E3
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

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000742F2 File Offset: 0x000724F2
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x000742FA File Offset: 0x000724FA
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

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00074309 File Offset: 0x00072509
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x00074311 File Offset: 0x00072511
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00074320 File Offset: 0x00072520
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00074328 File Offset: 0x00072528
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00074337 File Offset: 0x00072537
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0007433F File Offset: 0x0007253F
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

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0007434E File Offset: 0x0007254E
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00074356 File Offset: 0x00072556
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

		// Token: 0x06000F36 RID: 3894 RVA: 0x00074365 File Offset: 0x00072565
		internal EnrichedToHtml SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0007436F File Offset: 0x0007256F
		internal EnrichedToHtml SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00074379 File Offset: 0x00072579
		internal EnrichedToHtml SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00074383 File Offset: 0x00072583
		internal EnrichedToHtml SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0007438D File Offset: 0x0007258D
		internal EnrichedToHtml SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00074397 File Offset: 0x00072597
		internal EnrichedToHtml SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000743A1 File Offset: 0x000725A1
		internal EnrichedToHtml SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000743AB File Offset: 0x000725AB
		internal EnrichedToHtml SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000743B7 File Offset: 0x000725B7
		internal EnrichedToHtml SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000743C1 File Offset: 0x000725C1
		internal EnrichedToHtml SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000743CB File Offset: 0x000725CB
		internal EnrichedToHtml SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000743D5 File Offset: 0x000725D5
		internal EnrichedToHtml SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000743DF File Offset: 0x000725DF
		internal EnrichedToHtml SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000743E9 File Offset: 0x000725E9
		internal EnrichedToHtml SetTestFormatOutputTraceStream(Stream value)
		{
			this.testFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000743F3 File Offset: 0x000725F3
		internal EnrichedToHtml SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00074400 File Offset: 0x00072600
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00074480 File Offset: 0x00072680
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, true);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000744E8 File Offset: 0x000726E8
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00074548 File Offset: 0x00072748
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00074594 File Offset: 0x00072794
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00074614 File Offset: 0x00072814
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00074674 File Offset: 0x00072874
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

		// Token: 0x06000F4C RID: 3916 RVA: 0x000746DC File Offset: 0x000728DC
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00074728 File Offset: 0x00072928
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlInjection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, this.filterHtml, this.htmlCallback, this.testBoundaryConditions, null, progressMonitor);
			}
			EnrichedParser parser = new EnrichedParser(input, this.testMaxTokenRuns, this.testBoundaryConditions);
			HtmlWriter writer = new HtmlWriter(output, this.filterHtml, false);
			FormatOutput output2 = new HtmlFormatOutput(writer, injection, this.outputFragment, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.filterHtml, this.htmlCallback, true);
			return new EnrichedFormatConverter(parser, output2, null, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000747E4 File Offset: 0x000729E4
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

		// Token: 0x04001060 RID: 4192
		private Encoding inputEncoding;

		// Token: 0x04001061 RID: 4193
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04001062 RID: 4194
		private Encoding outputEncoding;

		// Token: 0x04001063 RID: 4195
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04001064 RID: 4196
		private bool filterHtml;

		// Token: 0x04001065 RID: 4197
		private HtmlTagCallback htmlCallback;

		// Token: 0x04001066 RID: 4198
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04001067 RID: 4199
		private string injectHead;

		// Token: 0x04001068 RID: 4200
		private string injectTail;

		// Token: 0x04001069 RID: 4201
		private bool outputFragment;

		// Token: 0x0400106A RID: 4202
		private int testMaxTokenRuns = 512;

		// Token: 0x0400106B RID: 4203
		private Stream testTraceStream;

		// Token: 0x0400106C RID: 4204
		private bool testTraceShowTokenNum = true;

		// Token: 0x0400106D RID: 4205
		private int testTraceStopOnTokenNum;

		// Token: 0x0400106E RID: 4206
		private Stream testFormatTraceStream;

		// Token: 0x0400106F RID: 4207
		private Stream testFormatOutputTraceStream;

		// Token: 0x04001070 RID: 4208
		private Stream testFormatConverterTraceStream;
	}
}
