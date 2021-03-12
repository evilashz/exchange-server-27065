using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Enriched;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000166 RID: 358
	public class HtmlToEnriched : TextConverter
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00074EA5 File Offset: 0x000730A5
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x00074EAD File Offset: 0x000730AD
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00074EBC File Offset: 0x000730BC
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x00074EC4 File Offset: 0x000730C4
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00074ED3 File Offset: 0x000730D3
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x00074EDB File Offset: 0x000730DB
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00074EEA File Offset: 0x000730EA
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00074EF2 File Offset: 0x000730F2
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00074F0B File Offset: 0x0007310B
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00074F13 File Offset: 0x00073113
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00074F22 File Offset: 0x00073122
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00074F2A File Offset: 0x0007312A
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

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00074F39 File Offset: 0x00073139
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00074F41 File Offset: 0x00073141
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

		// Token: 0x06000F8E RID: 3982 RVA: 0x00074F50 File Offset: 0x00073150
		internal HtmlToEnriched SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00074F5A File Offset: 0x0007315A
		internal HtmlToEnriched SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00074F64 File Offset: 0x00073164
		internal HtmlToEnriched SetDetectEncodingFromMetaTag(bool value)
		{
			this.DetectEncodingFromMetaTag = value;
			return this;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00074F6E File Offset: 0x0007316E
		internal HtmlToEnriched SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00074F78 File Offset: 0x00073178
		internal HtmlToEnriched SetUseFallbacks(bool value)
		{
			this.fallbacks = value;
			return this;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00074F82 File Offset: 0x00073182
		internal HtmlToEnriched SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00074F8C File Offset: 0x0007318C
		internal HtmlToEnriched SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00074F96 File Offset: 0x00073196
		internal HtmlToEnriched SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00074FA0 File Offset: 0x000731A0
		internal HtmlToEnriched SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00074FAA File Offset: 0x000731AA
		internal HtmlToEnriched SetTestBoundaryConditions(bool value)
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

		// Token: 0x06000F98 RID: 3992 RVA: 0x00074FCE File Offset: 0x000731CE
		internal HtmlToEnriched SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00074FD8 File Offset: 0x000731D8
		internal HtmlToEnriched SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00074FE2 File Offset: 0x000731E2
		internal HtmlToEnriched SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00074FEC File Offset: 0x000731EC
		internal HtmlToEnriched SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00074FF6 File Offset: 0x000731F6
		internal HtmlToEnriched SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00075000 File Offset: 0x00073200
		internal HtmlToEnriched SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0007500A File Offset: 0x0007320A
		internal HtmlToEnriched SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00075014 File Offset: 0x00073214
		internal HtmlToEnriched SetTestMaxHtmlTagSize(int value)
		{
			this.testMaxHtmlTagSize = value;
			return this;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0007501E File Offset: 0x0007321E
		internal HtmlToEnriched SetTestMaxHtmlTagAttributes(int value)
		{
			this.testMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00075028 File Offset: 0x00073228
		internal HtmlToEnriched SetTestMaxHtmlRestartOffset(int value)
		{
			this.testMaxHtmlRestartOffset = value;
			return this;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00075032 File Offset: 0x00073232
		internal HtmlToEnriched SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.testMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0007503C File Offset: 0x0007323C
		internal HtmlToEnriched SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00075046 File Offset: 0x00073246
		internal HtmlToEnriched SetTestFormatOutputTraceStream(Stream value)
		{
			this.testFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00075050 File Offset: 0x00073250
		internal HtmlToEnriched SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0007505C File Offset: 0x0007325C
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

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000750E0 File Offset: 0x000732E0
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

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0007514C File Offset: 0x0007334C
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.testMaxHtmlTagSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000751B0 File Offset: 0x000733B0
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.testMaxHtmlTagSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000751FC File Offset: 0x000733FC
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

		// Token: 0x06000FAB RID: 4011 RVA: 0x00075280 File Offset: 0x00073480
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.testMaxHtmlTagSize, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000752E4 File Offset: 0x000734E4
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

		// Token: 0x06000FAD RID: 4013 RVA: 0x00075350 File Offset: 0x00073550
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.testMaxHtmlTagSize, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0007539C File Offset: 0x0007359C
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlInjection injection = null;
			HtmlParser parser = new HtmlParser(input, this.detectEncodingFromMetaTag, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, this.testBoundaryConditions);
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor);
			}
			HtmlNormalizingParser parser2 = new HtmlNormalizingParser(parser, injection, false, this.testMaxHtmlNormalizerNesting, this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
			EnrichedFormatOutput output2 = new EnrichedFormatOutput(output, null, this.fallbacks, this.testFormatTraceStream, this.testFormatOutputTraceStream);
			return new HtmlFormatConverter(parser2, output2, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream, progressMonitor);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00075468 File Offset: 0x00073668
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

		// Token: 0x04001083 RID: 4227
		private Encoding inputEncoding;

		// Token: 0x04001084 RID: 4228
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04001085 RID: 4229
		private bool detectEncodingFromMetaTag = true;

		// Token: 0x04001086 RID: 4230
		private Encoding outputEncoding;

		// Token: 0x04001087 RID: 4231
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04001088 RID: 4232
		private bool fallbacks = true;

		// Token: 0x04001089 RID: 4233
		private HeaderFooterFormat injectionFormat;

		// Token: 0x0400108A RID: 4234
		private string injectHead;

		// Token: 0x0400108B RID: 4235
		private string injectTail;

		// Token: 0x0400108C RID: 4236
		private int testMaxTokenRuns = 512;

		// Token: 0x0400108D RID: 4237
		private Stream testTraceStream;

		// Token: 0x0400108E RID: 4238
		private bool testTraceShowTokenNum = true;

		// Token: 0x0400108F RID: 4239
		private int testTraceStopOnTokenNum;

		// Token: 0x04001090 RID: 4240
		private Stream testNormalizerTraceStream;

		// Token: 0x04001091 RID: 4241
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x04001092 RID: 4242
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04001093 RID: 4243
		private int testMaxHtmlTagSize = 4096;

		// Token: 0x04001094 RID: 4244
		private int testMaxHtmlTagAttributes = 64;

		// Token: 0x04001095 RID: 4245
		private int testMaxHtmlRestartOffset = 4096;

		// Token: 0x04001096 RID: 4246
		private int testMaxHtmlNormalizerNesting = 4096;

		// Token: 0x04001097 RID: 4247
		private Stream testFormatTraceStream;

		// Token: 0x04001098 RID: 4248
		private Stream testFormatOutputTraceStream;

		// Token: 0x04001099 RID: 4249
		private Stream testFormatConverterTraceStream;
	}
}
