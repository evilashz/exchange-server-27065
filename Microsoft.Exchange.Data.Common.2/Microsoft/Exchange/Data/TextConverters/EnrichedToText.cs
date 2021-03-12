using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Enriched;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000165 RID: 357
	public class EnrichedToText : TextConverter
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00074854 File Offset: 0x00072A54
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0007485C File Offset: 0x00072A5C
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

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0007486B File Offset: 0x00072A6B
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x00074873 File Offset: 0x00072A73
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

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00074882 File Offset: 0x00072A82
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0007488A File Offset: 0x00072A8A
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

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x000748A3 File Offset: 0x00072AA3
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x000748AB File Offset: 0x00072AAB
		public bool Wrap
		{
			get
			{
				return this.wrapFlowed;
			}
			set
			{
				base.AssertNotLocked();
				this.wrapFlowed = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x000748BA File Offset: 0x00072ABA
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x000748C2 File Offset: 0x00072AC2
		internal bool WrapDeleteSpace
		{
			get
			{
				return this.wrapDelSp;
			}
			set
			{
				base.AssertNotLocked();
				this.wrapDelSp = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x000748D1 File Offset: 0x00072AD1
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x000748D9 File Offset: 0x00072AD9
		public bool HtmlEscapeOutput
		{
			get
			{
				return this.htmlEscape;
			}
			set
			{
				base.AssertNotLocked();
				this.htmlEscape = value;
				if (value)
				{
					this.fallbacks = true;
				}
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000748F2 File Offset: 0x00072AF2
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x000748FA File Offset: 0x00072AFA
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

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00074909 File Offset: 0x00072B09
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x00074911 File Offset: 0x00072B11
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

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00074920 File Offset: 0x00072B20
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00074928 File Offset: 0x00072B28
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

		// Token: 0x06000F62 RID: 3938 RVA: 0x00074937 File Offset: 0x00072B37
		internal EnrichedToText SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00074941 File Offset: 0x00072B41
		internal EnrichedToText SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0007494B File Offset: 0x00072B4B
		internal EnrichedToText SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00074955 File Offset: 0x00072B55
		internal EnrichedToText SetWrap(bool value)
		{
			this.Wrap = value;
			return this;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0007495F File Offset: 0x00072B5F
		internal EnrichedToText SetWrapDeleteSpace(bool value)
		{
			this.WrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00074969 File Offset: 0x00072B69
		internal EnrichedToText SetUseFallbacks(bool value)
		{
			this.fallbacks = value;
			return this;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00074973 File Offset: 0x00072B73
		internal EnrichedToText SetHtmlEscapeOutput(bool value)
		{
			this.HtmlEscapeOutput = value;
			return this;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0007497D File Offset: 0x00072B7D
		internal EnrichedToText SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00074987 File Offset: 0x00072B87
		internal EnrichedToText SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00074991 File Offset: 0x00072B91
		internal EnrichedToText SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0007499B File Offset: 0x00072B9B
		internal EnrichedToText SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000749A5 File Offset: 0x00072BA5
		internal EnrichedToText SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000749B1 File Offset: 0x00072BB1
		internal EnrichedToText SetTestPreserveTrailingSpaces(bool value)
		{
			this.testPreserveTrailingSpaces = value;
			return this;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000749BB File Offset: 0x00072BBB
		internal EnrichedToText SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000749C5 File Offset: 0x00072BC5
		internal EnrichedToText SetTestTreatNbspAsBreakable(bool value)
		{
			this.testTreatNbspAsBreakable = value;
			return this;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000749CF File Offset: 0x00072BCF
		internal EnrichedToText SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000749D9 File Offset: 0x00072BD9
		internal EnrichedToText SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000749E3 File Offset: 0x00072BE3
		internal EnrichedToText SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000749ED File Offset: 0x00072BED
		internal EnrichedToText SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x000749F8 File Offset: 0x00072BF8
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

		// Token: 0x06000F76 RID: 3958 RVA: 0x00074A78 File Offset: 0x00072C78
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

		// Token: 0x06000F77 RID: 3959 RVA: 0x00074AE0 File Offset: 0x00072CE0
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00074B40 File Offset: 0x00072D40
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00074B8C File Offset: 0x00072D8C
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

		// Token: 0x06000F7A RID: 3962 RVA: 0x00074C0C File Offset: 0x00072E0C
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00074C6C File Offset: 0x00072E6C
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

		// Token: 0x06000F7C RID: 3964 RVA: 0x00074CD4 File Offset: 0x00072ED4
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00074D20 File Offset: 0x00072F20
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			Injection injection = null;
			EnrichedParser parser = new EnrichedParser(input, this.testMaxTokenRuns, this.testBoundaryConditions);
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = ((this.injectionFormat == HeaderFooterFormat.Html) ? new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor) : new TextInjection(this.injectHead, this.injectTail, this.testBoundaryConditions, null, progressMonitor));
			}
			TextOutput output2 = new TextOutput(output, this.wrapFlowed, this.wrapFlowed, 72, 78, null, this.fallbacks, this.htmlEscape, this.testPreserveTrailingSpaces, this.testFormatTraceStream);
			return new EnrichedToTextConverter(parser, output2, injection, this.testTreatNbspAsBreakable, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00074DF0 File Offset: 0x00072FF0
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

		// Token: 0x04001071 RID: 4209
		private Encoding inputEncoding;

		// Token: 0x04001072 RID: 4210
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04001073 RID: 4211
		private Encoding outputEncoding;

		// Token: 0x04001074 RID: 4212
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04001075 RID: 4213
		private bool wrapFlowed;

		// Token: 0x04001076 RID: 4214
		private bool wrapDelSp;

		// Token: 0x04001077 RID: 4215
		private bool fallbacks = true;

		// Token: 0x04001078 RID: 4216
		private bool htmlEscape;

		// Token: 0x04001079 RID: 4217
		private HeaderFooterFormat injectionFormat;

		// Token: 0x0400107A RID: 4218
		private string injectHead;

		// Token: 0x0400107B RID: 4219
		private string injectTail;

		// Token: 0x0400107C RID: 4220
		private bool testPreserveTrailingSpaces;

		// Token: 0x0400107D RID: 4221
		private int testMaxTokenRuns = 512;

		// Token: 0x0400107E RID: 4222
		private bool testTreatNbspAsBreakable;

		// Token: 0x0400107F RID: 4223
		private Stream testTraceStream;

		// Token: 0x04001080 RID: 4224
		private bool testTraceShowTokenNum = true;

		// Token: 0x04001081 RID: 4225
		private int testTraceStopOnTokenNum;

		// Token: 0x04001082 RID: 4226
		private Stream testFormatTraceStream;
	}
}
