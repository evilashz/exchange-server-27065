using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200002E RID: 46
	internal class TextConversationBodyScanner : ConversationBodyScanner
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009D70 File Offset: 0x00007F70
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00009D78 File Offset: 0x00007F78
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009D87 File Offset: 0x00007F87
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009D8F File Offset: 0x00007F8F
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009D9E File Offset: 0x00007F9E
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009DA6 File Offset: 0x00007FA6
		public bool Unwrap
		{
			get
			{
				return this.unwrapFlowed;
			}
			set
			{
				base.AssertNotLocked();
				this.unwrapFlowed = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009DB5 File Offset: 0x00007FB5
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009DBD File Offset: 0x00007FBD
		internal bool UnwrapDeleteSpace
		{
			get
			{
				return this.unwrapDelSp;
			}
			set
			{
				base.AssertNotLocked();
				this.unwrapDelSp = value;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009DCC File Offset: 0x00007FCC
		internal TextConversationBodyScanner SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009DD6 File Offset: 0x00007FD6
		internal TextConversationBodyScanner SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009DE0 File Offset: 0x00007FE0
		internal TextConversationBodyScanner SetUnwrap(bool value)
		{
			this.Unwrap = value;
			return this;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009DEA File Offset: 0x00007FEA
		internal TextConversationBodyScanner SetUnwrapDeleteSpace(bool value)
		{
			this.UnwrapDeleteSpace = value;
			return this;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009DF4 File Offset: 0x00007FF4
		internal TextConversationBodyScanner SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009DFE File Offset: 0x00007FFE
		internal TextConversationBodyScanner SetFilterHtml(bool value)
		{
			base.FilterHtml = value;
			return this;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009E08 File Offset: 0x00008008
		internal TextConversationBodyScanner SetHtmlTagCallback(HtmlTagCallback value)
		{
			base.HtmlTagCallback = value;
			return this;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009E12 File Offset: 0x00008012
		internal TextConversationBodyScanner SetTestBoundaryConditions(bool value)
		{
			base.TestBoundaryConditions = value;
			return this;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009E1E File Offset: 0x0000801E
		internal TextConversationBodyScanner SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009E28 File Offset: 0x00008028
		internal TextConversationBodyScanner SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009E32 File Offset: 0x00008032
		internal TextConversationBodyScanner SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009E3C File Offset: 0x0000803C
		internal TextConversationBodyScanner SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009E46 File Offset: 0x00008046
		internal TextConversationBodyScanner SetTestFormatTraceStream(Stream value)
		{
			this.TestFormatTraceStream = value;
			return this;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009E50 File Offset: 0x00008050
		internal TextConversationBodyScanner SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009E5A File Offset: 0x0000805A
		internal TextConversationBodyScanner SetTestFormatOutputTraceStream(Stream value)
		{
			this.TestFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009E64 File Offset: 0x00008064
		internal override FormatConverter CreatePullChain(Stream sourceStream, IProgressMonitor progressMonitor)
		{
			if (this.inputEncoding == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
			}
			ConverterInput input = new ConverterDecodingInput(sourceStream, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, 4096, 0, base.InputStreamBufferSize, base.TestBoundaryConditions, this as IResultsFeedback, progressMonitor);
			return this.CreateChain(input, progressMonitor);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009EBC File Offset: 0x000080BC
		internal override FormatConverter CreatePullChain(TextReader sourceReader, IProgressMonitor progressMonitor)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(sourceReader, false, 4096, base.TestBoundaryConditions, progressMonitor);
			return this.CreateChain(input, progressMonitor);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009EF0 File Offset: 0x000080F0
		private FormatConverter CreateChain(ConverterInput input, IProgressMonitor progressMonitor)
		{
			this.RecognizeHyperlinks = true;
			this.Locked = true;
			TextParser parser = new TextParser(input, this.unwrapFlowed, this.unwrapDelSp, this.testMaxTokenRuns, base.TestBoundaryConditions);
			FormatStore store = new FormatStore();
			return new TextFormatConverter(parser, store, null, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream);
		}

		// Token: 0x0400019F RID: 415
		private Encoding inputEncoding;

		// Token: 0x040001A0 RID: 416
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x040001A1 RID: 417
		private bool unwrapFlowed;

		// Token: 0x040001A2 RID: 418
		private bool unwrapDelSp;

		// Token: 0x040001A3 RID: 419
		private int testMaxTokenRuns = 512;

		// Token: 0x040001A4 RID: 420
		private Stream testTraceStream;

		// Token: 0x040001A5 RID: 421
		private bool testTraceShowTokenNum = true;

		// Token: 0x040001A6 RID: 422
		private int testTraceStopOnTokenNum;

		// Token: 0x040001A7 RID: 423
		private Stream testFormatConverterTraceStream;
	}
}
