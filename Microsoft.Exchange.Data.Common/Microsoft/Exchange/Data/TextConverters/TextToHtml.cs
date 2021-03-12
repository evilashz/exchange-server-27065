using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000158 RID: 344
	public class TextToHtml : TextConverter
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x000712D8 File Offset: 0x0006F4D8
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x000712E0 File Offset: 0x0006F4E0
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000712EF File Offset: 0x0006F4EF
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000712F7 File Offset: 0x0006F4F7
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

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00071306 File Offset: 0x0006F506
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0007130E File Offset: 0x0006F50E
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0007131D File Offset: 0x0006F51D
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x00071325 File Offset: 0x0006F525
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00071334 File Offset: 0x0006F534
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x0007133C File Offset: 0x0006F53C
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00071355 File Offset: 0x0006F555
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x0007135D File Offset: 0x0006F55D
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0007136C File Offset: 0x0006F56C
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00071374 File Offset: 0x0006F574
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00071383 File Offset: 0x0006F583
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0007138B File Offset: 0x0006F58B
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

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0007139A File Offset: 0x0006F59A
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x000713A2 File Offset: 0x0006F5A2
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

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000713B1 File Offset: 0x0006F5B1
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x000713B9 File Offset: 0x0006F5B9
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000713C8 File Offset: 0x0006F5C8
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x000713D0 File Offset: 0x0006F5D0
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

		// Token: 0x06000D90 RID: 3472 RVA: 0x000713DF File Offset: 0x0006F5DF
		internal TextToHtml SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000713E9 File Offset: 0x0006F5E9
		internal TextToHtml SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000713F3 File Offset: 0x0006F5F3
		internal TextToHtml SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000713FD File Offset: 0x0006F5FD
		internal TextToHtml SetUnwrap(bool value)
		{
			this.Unwrap = value;
			return this;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00071407 File Offset: 0x0006F607
		internal TextToHtml SetUnwrapDeleteSpace(bool value)
		{
			this.UnwrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00071411 File Offset: 0x0006F611
		internal TextToHtml SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0007141B File Offset: 0x0006F61B
		internal TextToHtml SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00071425 File Offset: 0x0006F625
		internal TextToHtml SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0007142F File Offset: 0x0006F62F
		internal TextToHtml SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00071439 File Offset: 0x0006F639
		internal TextToHtml SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00071445 File Offset: 0x0006F645
		internal TextToHtml SetTestMaxTokenRuns(int value)
		{
			this.testMaxTokenRuns = value;
			return this;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0007144F File Offset: 0x0006F64F
		internal TextToHtml SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00071459 File Offset: 0x0006F659
		internal TextToHtml SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00071463 File Offset: 0x0006F663
		internal TextToHtml SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0007146D File Offset: 0x0006F66D
		internal TextToHtml SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00071477 File Offset: 0x0006F677
		internal TextToHtml SetTestFormatOutputTraceStream(Stream value)
		{
			this.testFormatOutputTraceStream = value;
			return this;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00071481 File Offset: 0x0006F681
		internal TextToHtml SetTestFormatConverterTraceStream(Stream value)
		{
			this.testFormatConverterTraceStream = value;
			return this;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0007148C File Offset: 0x0006F68C
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

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0007150C File Offset: 0x0006F70C
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

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00071574 File Offset: 0x0006F774
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000715D4 File Offset: 0x0006F7D4
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input = new ConverterUnicodeInput(converterWriter, true, 4096, this.testBoundaryConditions, null);
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(input, output2, converterWriter);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00071620 File Offset: 0x0006F820
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

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000716A0 File Offset: 0x0006F8A0
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			this.inputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterStream);
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.UTF8 : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input2, output, converterStream);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00071700 File Offset: 0x0006F900
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

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00071768 File Offset: 0x0006F968
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			this.inputEncoding = Encoding.Unicode;
			this.outputEncoding = Encoding.Unicode;
			ConverterInput input2 = new ConverterUnicodeInput(input, false, 4096, this.testBoundaryConditions, converterReader);
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input2, output, converterReader);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000717B4 File Offset: 0x0006F9B4
		private IProducerConsumer CreateChain(ConverterInput input, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			HtmlInjection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, this.filterHtml, this.htmlCallback, this.testBoundaryConditions, null, progressMonitor);
			}
			TextParser parser = new TextParser(input, this.unwrapFlowed, this.unwrapDelSp, this.testMaxTokenRuns, this.testBoundaryConditions);
			HtmlWriter writer = new HtmlWriter(output, this.filterHtml, false);
			FormatOutput output2 = new HtmlFormatOutput(writer, injection, this.outputFragment, this.testFormatTraceStream, this.testFormatOutputTraceStream, this.filterHtml, this.htmlCallback, true);
			return new TextFormatConverter(parser, output2, null, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum, this.testFormatConverterTraceStream);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0007187C File Offset: 0x0006FA7C
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

		// Token: 0x04000FA9 RID: 4009
		private Encoding inputEncoding;

		// Token: 0x04000FAA RID: 4010
		private bool detectEncodingFromByteOrderMark = true;

		// Token: 0x04000FAB RID: 4011
		private bool unwrapFlowed;

		// Token: 0x04000FAC RID: 4012
		private bool unwrapDelSp;

		// Token: 0x04000FAD RID: 4013
		private Encoding outputEncoding;

		// Token: 0x04000FAE RID: 4014
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04000FAF RID: 4015
		private bool filterHtml;

		// Token: 0x04000FB0 RID: 4016
		private HtmlTagCallback htmlCallback;

		// Token: 0x04000FB1 RID: 4017
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04000FB2 RID: 4018
		private string injectHead;

		// Token: 0x04000FB3 RID: 4019
		private string injectTail;

		// Token: 0x04000FB4 RID: 4020
		private bool outputFragment;

		// Token: 0x04000FB5 RID: 4021
		private int testMaxTokenRuns = 512;

		// Token: 0x04000FB6 RID: 4022
		private Stream testTraceStream;

		// Token: 0x04000FB7 RID: 4023
		private bool testTraceShowTokenNum = true;

		// Token: 0x04000FB8 RID: 4024
		private int testTraceStopOnTokenNum;

		// Token: 0x04000FB9 RID: 4025
		private Stream testFormatTraceStream;

		// Token: 0x04000FBA RID: 4026
		private Stream testFormatOutputTraceStream;

		// Token: 0x04000FBB RID: 4027
		private Stream testFormatConverterTraceStream;
	}
}
