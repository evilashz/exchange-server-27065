using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200002C RID: 44
	public class HtmlToHtmlSafe : TextConverter
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00009401 File Offset: 0x00007601
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00009409 File Offset: 0x00007609
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00009418 File Offset: 0x00007618
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00009420 File Offset: 0x00007620
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000942F File Offset: 0x0000762F
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00009437 File Offset: 0x00007637
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00009446 File Offset: 0x00007646
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000944E File Offset: 0x0000764E
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00009467 File Offset: 0x00007667
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000946F File Offset: 0x0000766F
		public bool OutputHtmlFragment
		{
			get
			{
				return this.OutputFragment;
			}
			set
			{
				base.AssertNotLocked();
				this.OutputFragment = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000947E File Offset: 0x0000767E
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00009486 File Offset: 0x00007686
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00009495 File Offset: 0x00007695
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000949D File Offset: 0x0000769D
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000094AC File Offset: 0x000076AC
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000094B4 File Offset: 0x000076B4
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000094C3 File Offset: 0x000076C3
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000094CB File Offset: 0x000076CB
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000094DA File Offset: 0x000076DA
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000094E2 File Offset: 0x000076E2
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

		// Token: 0x06000180 RID: 384 RVA: 0x000094F1 File Offset: 0x000076F1
		internal HtmlToHtmlSafe SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			base.AssertNotLocked();
			return this;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009501 File Offset: 0x00007701
		internal HtmlToHtmlSafe SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			base.AssertNotLocked();
			return this;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009511 File Offset: 0x00007711
		internal HtmlToHtmlSafe SetDetectEncodingFromMetaTag(bool value)
		{
			this.DetectEncodingFromMetaTag = value;
			base.AssertNotLocked();
			return this;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009521 File Offset: 0x00007721
		internal HtmlToHtmlSafe SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			base.AssertNotLocked();
			return this;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009531 File Offset: 0x00007731
		internal HtmlToHtmlSafe SetHeader(string value)
		{
			base.AssertNotLocked();
			this.Header = value;
			return this;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009541 File Offset: 0x00007741
		internal HtmlToHtmlSafe SetFooter(string value)
		{
			base.AssertNotLocked();
			this.Footer = value;
			return this;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009551 File Offset: 0x00007751
		internal HtmlToHtmlSafe SetFilterHtml(bool value)
		{
			base.AssertNotLocked();
			this.FilterHtml = value;
			return this;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009561 File Offset: 0x00007761
		internal HtmlToHtmlSafe SetHtmlTagCallback(HtmlTagCallback value)
		{
			base.AssertNotLocked();
			this.HtmlTagCallback = value;
			return this;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009571 File Offset: 0x00007771
		internal HtmlToHtmlSafe SetInputStreamBufferSize(int value)
		{
			base.AssertNotLocked();
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009581 File Offset: 0x00007781
		internal HtmlToHtmlSafe SetTestBoundaryConditions(bool value)
		{
			base.AssertNotLocked();
			this.testBoundaryConditions = value;
			this.maxTokenSize = (value ? TextConvertersDefaults.MaxTokenSize(value) : 32768);
			return this;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000095A7 File Offset: 0x000077A7
		internal HtmlToHtmlSafe SetTestTraceStream(Stream value)
		{
			base.AssertNotLocked();
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000095B7 File Offset: 0x000077B7
		internal HtmlToHtmlSafe SetTestTraceShowTokenNum(bool value)
		{
			base.AssertNotLocked();
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000095C7 File Offset: 0x000077C7
		internal HtmlToHtmlSafe SetTestTraceStopOnTokenNum(int value)
		{
			base.AssertNotLocked();
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000095D7 File Offset: 0x000077D7
		internal HtmlToHtmlSafe SetTestNormalizerTraceStream(Stream value)
		{
			base.AssertNotLocked();
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000095E7 File Offset: 0x000077E7
		internal HtmlToHtmlSafe SetTestNormalizerTraceShowTokenNum(bool value)
		{
			base.AssertNotLocked();
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000095F7 File Offset: 0x000077F7
		internal HtmlToHtmlSafe SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			base.AssertNotLocked();
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009607 File Offset: 0x00007807
		internal HtmlToHtmlSafe SetTestFormatTraceStream(Stream value)
		{
			base.AssertNotLocked();
			this.TestFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009617 File Offset: 0x00007817
		internal HtmlToHtmlSafe SetTestFormatOutputTraceStream(Stream value)
		{
			base.AssertNotLocked();
			this.TestFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009627 File Offset: 0x00007827
		internal HtmlToHtmlSafe SetTestFormatConverterTraceStream(Stream value)
		{
			base.AssertNotLocked();
			this.TestFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009638 File Offset: 0x00007838
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxTokenSize, TextConvertersDefaults.MaxHtmlMetaRestartOffset(this.testBoundaryConditions), base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000096C0 File Offset: 0x000078C0
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterDecodingInput(converterStream, true, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxTokenSize, TextConvertersDefaults.MaxHtmlMetaRestartOffset(this.testBoundaryConditions), base.InputStreamBufferSize, this.testBoundaryConditions, this, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, true);
			return this.CreateChain(input, output2, converterStream);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009730 File Offset: 0x00007930
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.maxTokenSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009794 File Offset: 0x00007994
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, this.maxTokenSize, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000097E0 File Offset: 0x000079E0
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxTokenSize, TextConvertersDefaults.MaxHtmlMetaRestartOffset(this.testBoundaryConditions), base.InputStreamBufferSize, this.testBoundaryConditions, this, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, true, this.outputEncodingSameAsInput ? this.inputEncoding : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009868 File Offset: 0x00007A68
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.maxTokenSize, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000098CC File Offset: 0x00007ACC
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterDecodingInput(input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, this.maxTokenSize, TextConvertersDefaults.MaxHtmlMetaRestartOffset(this.testBoundaryConditions), base.InputStreamBufferSize, this.testBoundaryConditions, this, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, true);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000993C File Offset: 0x00007B3C
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, this.maxTokenSize, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, true);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009988 File Offset: 0x00007B88
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlInjection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, this.filterHtml, this.htmlCallback, this.testBoundaryConditions, null, progressMonitor);
			}
			HtmlParser parser = new HtmlParser(input, this.detectEncodingFromMetaTag, false, TextConvertersDefaults.MaxTokenRuns(this.testBoundaryConditions), TextConvertersDefaults.MaxHtmlAttributes(this.testBoundaryConditions), this.testBoundaryConditions);
			HtmlNormalizingParser parser2 = new HtmlNormalizingParser(parser, injection, false, TextConvertersDefaults.MaxHtmlNormalizerNesting(this.testBoundaryConditions), this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
			HtmlWriter writer = new HtmlWriter(output, this.filterHtml, true);
			FormatOutput output2 = new HtmlFormatOutput(writer, null, this.OutputFragment, this.TestFormatTraceStream, this.TestFormatOutputTraceStream, this.filterHtml, this.htmlCallback, false);
			return new HtmlFormatConverter(parser2, output2, false, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.TestFormatConverterTraceStream, progressMonitor);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009A88 File Offset: 0x00007C88
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			if (parameterId == ConfigParameter.InputEncoding)
			{
				this.inputEncoding = (Encoding)val;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x0400017A RID: 378
		private Encoding inputEncoding;

		// Token: 0x0400017B RID: 379
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x0400017C RID: 380
		private bool detectEncodingFromMetaTag = true;

		// Token: 0x0400017D RID: 381
		private Encoding outputEncoding;

		// Token: 0x0400017E RID: 382
		private bool outputEncodingSameAsInput = true;

		// Token: 0x0400017F RID: 383
		internal bool OutputFragment;

		// Token: 0x04000180 RID: 384
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04000181 RID: 385
		private string injectHead;

		// Token: 0x04000182 RID: 386
		private string injectTail;

		// Token: 0x04000183 RID: 387
		private Stream testTraceStream;

		// Token: 0x04000184 RID: 388
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000185 RID: 389
		private int testTraceStopOnTokenNum;

		// Token: 0x04000186 RID: 390
		internal Stream TestFormatTraceStream;

		// Token: 0x04000187 RID: 391
		internal Stream TestFormatOutputTraceStream;

		// Token: 0x04000188 RID: 392
		internal Stream TestFormatConverterTraceStream;

		// Token: 0x04000189 RID: 393
		private bool filterHtml;

		// Token: 0x0400018A RID: 394
		private HtmlTagCallback htmlCallback;

		// Token: 0x0400018B RID: 395
		private Stream testNormalizerTraceStream;

		// Token: 0x0400018C RID: 396
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x0400018D RID: 397
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x0400018E RID: 398
		private int maxTokenSize = 32768;
	}
}
