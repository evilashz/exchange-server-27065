using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200002D RID: 45
	internal class RtfConversationBodyScanner : ConversationBodyScanner
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009AAF File Offset: 0x00007CAF
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009AB7 File Offset: 0x00007CB7
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

		// Token: 0x0600019F RID: 415 RVA: 0x00009AC6 File Offset: 0x00007CC6
		internal RtfConversationBodyScanner SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009AD0 File Offset: 0x00007CD0
		internal RtfConversationBodyScanner SetFilterHtml(bool value)
		{
			base.FilterHtml = value;
			return this;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009ADA File Offset: 0x00007CDA
		internal RtfConversationBodyScanner SetHtmlTagCallback(HtmlTagCallback value)
		{
			base.HtmlTagCallback = value;
			return this;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009AE4 File Offset: 0x00007CE4
		internal RtfConversationBodyScanner SetTestBoundaryConditions(bool value)
		{
			base.TestBoundaryConditions = value;
			if (value)
			{
				this.TestMaxHtmlTagSize = 123;
				this.TestMaxHtmlTagAttributes = 5;
				this.TestMaxHtmlNormalizerNesting = 10;
			}
			return this;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009B08 File Offset: 0x00007D08
		internal RtfConversationBodyScanner SetTestTraceStream(Stream value)
		{
			this.TestTraceStream = value;
			return this;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009B12 File Offset: 0x00007D12
		internal RtfConversationBodyScanner SetTestTraceShowTokenNum(bool value)
		{
			this.TestTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009B1C File Offset: 0x00007D1C
		internal RtfConversationBodyScanner SetTestTraceStopOnTokenNum(int value)
		{
			this.TestTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009B26 File Offset: 0x00007D26
		internal RtfConversationBodyScanner SetTestFormatTraceStream(Stream value)
		{
			this.TestFormatTraceStream = value;
			return this;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009B30 File Offset: 0x00007D30
		internal RtfConversationBodyScanner SetTestFormatConverterTraceStream(Stream value)
		{
			this.TestFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009B3A File Offset: 0x00007D3A
		internal RtfConversationBodyScanner SetTestFormatOutputTraceStream(Stream value)
		{
			this.TestFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009B44 File Offset: 0x00007D44
		internal RtfConversationBodyScanner SetTestHtmlTraceStream(Stream value)
		{
			this.TestHtmlTraceStream = value;
			return this;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009B4E File Offset: 0x00007D4E
		internal RtfConversationBodyScanner SetTestHtmlTraceShowTokenNum(bool value)
		{
			this.TestHtmlTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009B58 File Offset: 0x00007D58
		internal RtfConversationBodyScanner SetTestHtmlTraceStopOnTokenNum(int value)
		{
			this.TestHtmlTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009B62 File Offset: 0x00007D62
		internal RtfConversationBodyScanner SetTestNormalizerTraceStream(Stream value)
		{
			this.TestNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009B6C File Offset: 0x00007D6C
		internal RtfConversationBodyScanner SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.TestNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009B76 File Offset: 0x00007D76
		internal RtfConversationBodyScanner SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.TestNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009B80 File Offset: 0x00007D80
		internal RtfConversationBodyScanner SetTestMaxTokenRuns(int value)
		{
			this.TestMaxTokenRuns = value;
			return this;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009B8A File Offset: 0x00007D8A
		internal RtfConversationBodyScanner SetTestMaxHtmlTagAttributes(int value)
		{
			this.TestMaxHtmlTagAttributes = value;
			return this;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009B94 File Offset: 0x00007D94
		internal RtfConversationBodyScanner SetTestMaxHtmlNormalizerNesting(int value)
		{
			this.TestMaxHtmlNormalizerNesting = value;
			return this;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009BA0 File Offset: 0x00007DA0
		internal override FormatConverter CreatePullChain(Stream sourceStream, IProgressMonitor progressMonitor)
		{
			this.Locked = true;
			this.reportBytes = new ReportBytes();
			RtfParser parser;
			if (this.enableHtmlDeencapsulation)
			{
				RtfPreviewStream rtfPreviewStream = sourceStream as RtfPreviewStream;
				if (rtfPreviewStream == null || rtfPreviewStream.Parser == null || rtfPreviewStream.InternalPosition != 0 || rtfPreviewStream.InputRtfStream == null)
				{
					rtfPreviewStream = new RtfPreviewStream(sourceStream, base.InputStreamBufferSize);
				}
				parser = new RtfParser(rtfPreviewStream.InputRtfStream, base.InputStreamBufferSize, base.TestBoundaryConditions, progressMonitor, rtfPreviewStream.Parser, this.reportBytes);
				if (rtfPreviewStream.Encapsulation == RtfEncapsulation.Html)
				{
					return this.CreateDeencapsulationChain(parser, progressMonitor);
				}
			}
			else
			{
				parser = new RtfParser(sourceStream, false, base.InputStreamBufferSize, base.TestBoundaryConditions, progressMonitor, this.reportBytes);
			}
			FormatStore formatStore = new FormatStore();
			return new RtfFormatConverter(parser, formatStore, false, this.TestTraceStream, this.TestTraceShowTokenNum, this.TestTraceStopOnTokenNum, this.TestFormatConverterTraceStream);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009C6E File Offset: 0x00007E6E
		internal override FormatConverter CreatePullChain(TextReader sourceReader, IProgressMonitor progressMonitor)
		{
			throw new NotSupportedException("RTF input must be a stream");
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009C7C File Offset: 0x00007E7C
		internal FormatConverter CreateDeencapsulationChain(RtfParser parser, IProgressMonitor progressMonitor)
		{
			HtmlInRtfExtractingInput input = new HtmlInRtfExtractingInput(parser, this.TestMaxHtmlTagSize, base.TestBoundaryConditions, this.TestTraceStream, this.TestTraceShowTokenNum, this.TestTraceStopOnTokenNum);
			HtmlParser parser2 = new HtmlParser(input, false, false, this.TestMaxTokenRuns, this.TestMaxHtmlTagAttributes, base.TestBoundaryConditions);
			HtmlNormalizingParser parser3 = new HtmlNormalizingParser(parser2, null, false, this.TestMaxHtmlNormalizerNesting, base.TestBoundaryConditions, this.TestNormalizerTraceStream, this.TestNormalizerTraceShowTokenNum, this.TestNormalizerTraceStopOnTokenNum);
			FormatStore formatStore = new FormatStore();
			return new HtmlFormatConverter(parser3, formatStore, false, false, this.TestHtmlTraceStream, this.TestHtmlTraceShowTokenNum, this.TestHtmlTraceStopOnTokenNum, this.TestFormatConverterTraceStream, progressMonitor);
		}

		// Token: 0x0400018F RID: 399
		private bool enableHtmlDeencapsulation = true;

		// Token: 0x04000190 RID: 400
		internal Stream TestTraceStream;

		// Token: 0x04000191 RID: 401
		internal bool TestTraceShowTokenNum = true;

		// Token: 0x04000192 RID: 402
		internal int TestTraceStopOnTokenNum;

		// Token: 0x04000193 RID: 403
		internal Stream TestFormatConverterTraceStream;

		// Token: 0x04000194 RID: 404
		internal Stream TestHtmlTraceStream;

		// Token: 0x04000195 RID: 405
		internal bool TestHtmlTraceShowTokenNum = true;

		// Token: 0x04000196 RID: 406
		internal int TestHtmlTraceStopOnTokenNum;

		// Token: 0x04000197 RID: 407
		internal Stream TestNormalizerTraceStream;

		// Token: 0x04000198 RID: 408
		internal bool TestNormalizerTraceShowTokenNum = true;

		// Token: 0x04000199 RID: 409
		internal int TestNormalizerTraceStopOnTokenNum;

		// Token: 0x0400019A RID: 410
		internal int TestMaxTokenRuns = 512;

		// Token: 0x0400019B RID: 411
		internal int TestMaxHtmlTagSize = 32768;

		// Token: 0x0400019C RID: 412
		internal int TestMaxHtmlTagAttributes = 64;

		// Token: 0x0400019D RID: 413
		internal int TestMaxHtmlNormalizerNesting = 4096;

		// Token: 0x0400019E RID: 414
		private ReportBytes reportBytes;
	}
}
