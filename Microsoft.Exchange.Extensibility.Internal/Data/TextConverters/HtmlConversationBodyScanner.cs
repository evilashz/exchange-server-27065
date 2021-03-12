using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200002B RID: 43
	internal class HtmlConversationBodyScanner : ConversationBodyScanner
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00009132 File Offset: 0x00007332
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000913A File Offset: 0x0000733A
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00009149 File Offset: 0x00007349
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00009151 File Offset: 0x00007351
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00009160 File Offset: 0x00007360
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00009168 File Offset: 0x00007368
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

		// Token: 0x06000152 RID: 338 RVA: 0x00009177 File Offset: 0x00007377
		internal HtmlConversationBodyScanner SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009181 File Offset: 0x00007381
		internal HtmlConversationBodyScanner SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000918B File Offset: 0x0000738B
		internal HtmlConversationBodyScanner SetDetectEncodingFromMetaTag(bool value)
		{
			this.DetectEncodingFromMetaTag = value;
			return this;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009195 File Offset: 0x00007395
		internal HtmlConversationBodyScanner SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000919F File Offset: 0x0000739F
		internal HtmlConversationBodyScanner SetFilterHtml(bool value)
		{
			base.FilterHtml = value;
			return this;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000091A9 File Offset: 0x000073A9
		internal HtmlConversationBodyScanner SetHtmlTagCallback(HtmlTagCallback value)
		{
			base.HtmlTagCallback = value;
			return this;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000091B3 File Offset: 0x000073B3
		internal HtmlConversationBodyScanner SetTestBoundaryConditions(bool value)
		{
			base.TestBoundaryConditions = value;
			if (value)
			{
				this.testMaxHtmlTagSize = 123;
				this.testMaxHtmlTagAttributes = 5;
				this.testMaxHtmlNormalizerNesting = 10;
			}
			return this;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000091D7 File Offset: 0x000073D7
		internal HtmlConversationBodyScanner SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000091E1 File Offset: 0x000073E1
		internal HtmlConversationBodyScanner SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000091EB File Offset: 0x000073EB
		internal HtmlConversationBodyScanner SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000091F5 File Offset: 0x000073F5
		internal HtmlConversationBodyScanner SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000091FF File Offset: 0x000073FF
		internal HtmlConversationBodyScanner SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009209 File Offset: 0x00007409
		internal HtmlConversationBodyScanner SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009213 File Offset: 0x00007413
		internal HtmlConversationBodyScanner SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000921D File Offset: 0x0000741D
		internal HtmlConversationBodyScanner SetTestFormatTraceStream(Stream value)
		{
			this.TestFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009227 File Offset: 0x00007427
		internal HtmlConversationBodyScanner SetTestFormatConverterTraceStream(Stream value)
		{
			this.TestFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009231 File Offset: 0x00007431
		internal HtmlConversationBodyScanner SetTestFormatOutputTraceStream(Stream value)
		{
			this.TestFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000923B File Offset: 0x0000743B
		internal HtmlConversationBodyScanner SetTestMaxHtmlTagSize(int value)
		{
			this.testMaxHtmlTagSize = value;
			return this;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009245 File Offset: 0x00007445
		internal HtmlConversationBodyScanner SetTestMaxHtmlTagAttributes(int value)
		{
			this.testMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000924F File Offset: 0x0000744F
		internal HtmlConversationBodyScanner SetTestMaxHtmlRestartOffset(int value)
		{
			this.testMaxHtmlRestartOffset = value;
			return this;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009259 File Offset: 0x00007459
		internal HtmlConversationBodyScanner SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.testMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009264 File Offset: 0x00007464
		internal override FormatConverter CreatePullChain(Stream sourceStream, IProgressMonitor progressMonitor)
		{
			ConverterInput input = new ConverterDecodingInput(sourceStream, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.testMaxHtmlTagSize, this.testMaxHtmlRestartOffset, base.InputStreamBufferSize, base.TestBoundaryConditions, this as IResultsFeedback, progressMonitor);
			return this.CreateChain(input, progressMonitor);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000092AC File Offset: 0x000074AC
		internal override FormatConverter CreatePullChain(TextReader sourceReader, IProgressMonitor progressMonitor)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(sourceReader, false, this.testMaxHtmlTagSize, base.TestBoundaryConditions, progressMonitor);
			return this.CreateChain(input, progressMonitor);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000092E4 File Offset: 0x000074E4
		private FormatConverter CreateChain(ConverterInput input, IProgressMonitor progressMonitor)
		{
			this.Locked = true;
			HtmlParser parser = new HtmlParser(input, this.detectEncodingFromMetaTag, false, this.testMaxTokenRuns, this.testMaxHtmlTagAttributes, base.TestBoundaryConditions);
			HtmlNormalizingParser parser2 = new HtmlNormalizingParser(parser, null, false, this.testMaxHtmlNormalizerNesting, base.TestBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
			FormatStore formatStore = new FormatStore();
			return new HtmlFormatConverter(parser2, formatStore, false, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.TestFormatConverterTraceStream, progressMonitor);
		}

		// Token: 0x0400016B RID: 363
		private Encoding inputEncoding;

		// Token: 0x0400016C RID: 364
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x0400016D RID: 365
		private bool detectEncodingFromMetaTag = true;

		// Token: 0x0400016E RID: 366
		private int testMaxTokenRuns = 512;

		// Token: 0x0400016F RID: 367
		private Stream testTraceStream;

		// Token: 0x04000170 RID: 368
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000171 RID: 369
		private int testTraceStopOnTokenNum;

		// Token: 0x04000172 RID: 370
		internal Stream TestFormatConverterTraceStream;

		// Token: 0x04000173 RID: 371
		private Stream testNormalizerTraceStream;

		// Token: 0x04000174 RID: 372
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x04000175 RID: 373
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04000176 RID: 374
		private int testMaxHtmlTagSize = 4096;

		// Token: 0x04000177 RID: 375
		private int testMaxHtmlTagAttributes = 64;

		// Token: 0x04000178 RID: 376
		private int testMaxHtmlRestartOffset = 4096;

		// Token: 0x04000179 RID: 377
		private int testMaxHtmlNormalizerNesting = 4096;
	}
}
