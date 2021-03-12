using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200015F RID: 351
	public class RtfToText : TextConverter
	{
		// Token: 0x06000E8F RID: 3727 RVA: 0x0007322B File Offset: 0x0007142B
		public RtfToText()
		{
			this.textExtractionMode = TextExtractionMode.NormalConversion;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0007324F File Offset: 0x0007144F
		public RtfToText(TextExtractionMode textExtractionMode)
		{
			this.textExtractionMode = textExtractionMode;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00073273 File Offset: 0x00071473
		public TextExtractionMode TextExtractionMode
		{
			get
			{
				return this.textExtractionMode;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0007327B File Offset: 0x0007147B
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x00073283 File Offset: 0x00071483
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0007329C File Offset: 0x0007149C
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x000732A4 File Offset: 0x000714A4
		public bool Wrap
		{
			get
			{
				return this.wrapFlowed;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.wrapFlowed = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x000732B3 File Offset: 0x000714B3
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x000732BB File Offset: 0x000714BB
		internal bool WrapDeleteSpace
		{
			get
			{
				return this.wrapDelSp;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.wrapDelSp = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000732CA File Offset: 0x000714CA
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x000732D2 File Offset: 0x000714D2
		public bool HtmlEscapeOutput
		{
			get
			{
				return this.htmlEscape;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.htmlEscape = value;
				if (value)
				{
					this.fallbacks = true;
				}
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000732EB File Offset: 0x000714EB
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x000732F3 File Offset: 0x000714F3
		public HeaderFooterFormat HeaderFooterFormat
		{
			get
			{
				return this.injectionFormat;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.injectionFormat = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00073302 File Offset: 0x00071502
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x0007330A File Offset: 0x0007150A
		public string Header
		{
			get
			{
				return this.injectHead;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.injectHead = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00073319 File Offset: 0x00071519
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00073321 File Offset: 0x00071521
		public string Footer
		{
			get
			{
				return this.injectTail;
			}
			set
			{
				this.AssertNotLockedAndNotTextExtraction();
				this.injectTail = value;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00073330 File Offset: 0x00071530
		internal RtfToText SetOutputEncoding(Encoding value)
		{
			this.OutputEncoding = value;
			return this;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0007333A File Offset: 0x0007153A
		internal RtfToText SetWrap(bool value)
		{
			this.Wrap = value;
			return this;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00073344 File Offset: 0x00071544
		internal RtfToText SetWrapDeleteSpace(bool value)
		{
			this.WrapDeleteSpace = value;
			return this;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0007334E File Offset: 0x0007154E
		internal RtfToText SetUseFallbacks(bool value)
		{
			this.fallbacks = value;
			return this;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00073358 File Offset: 0x00071558
		internal RtfToText SetHtmlEscapeOutput(bool value)
		{
			this.HtmlEscapeOutput = value;
			return this;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00073362 File Offset: 0x00071562
		internal RtfToText SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0007336C File Offset: 0x0007156C
		internal RtfToText SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00073376 File Offset: 0x00071576
		internal RtfToText SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00073380 File Offset: 0x00071580
		internal RtfToText SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0007338A File Offset: 0x0007158A
		internal RtfToText SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00073396 File Offset: 0x00071596
		internal RtfToText SetTestPreserveTrailingSpaces(bool value)
		{
			this.testPreserveTrailingSpaces = value;
			return this;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000733A0 File Offset: 0x000715A0
		internal RtfToText SetTestTreatNbspAsBreakable(bool value)
		{
			this.testTreatNbspAsBreakable = value;
			return this;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000733AA File Offset: 0x000715AA
		internal RtfToText SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000733B4 File Offset: 0x000715B4
		internal RtfToText SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x000733BE File Offset: 0x000715BE
		internal RtfToText SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000733C8 File Offset: 0x000715C8
		internal RtfToText SetTestFormatTraceStream(Stream value)
		{
			this.testFormatTraceStream = value;
			return this;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000733D4 File Offset: 0x000715D4
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			ConverterOutput output2 = new ConverterEncodingOutput(output, true, false, this.outputEncodingSameAsInput ? Encoding.GetEncoding("Windows-1252") : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(converterStream, true, output2, converterStream);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0007341C File Offset: 0x0007161C
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			ConverterOutput output2 = new ConverterUnicodeOutput(output, true, false);
			return this.CreateChain(converterStream, true, output2, converterStream);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0007343C File Offset: 0x0007163C
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00073448 File Offset: 0x00071648
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00073454 File Offset: 0x00071654
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			ConverterOutput output = new ConverterEncodingOutput(converterStream, false, false, this.outputEncodingSameAsInput ? Encoding.GetEncoding("Windows-1252") : this.outputEncoding, this.outputEncodingSameAsInput, this.testBoundaryConditions, this);
			return this.CreateChain(input, false, output, converterStream);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0007349B File Offset: 0x0007169B
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000734A8 File Offset: 0x000716A8
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			ConverterOutput output = new ConverterUnicodeOutput(converterReader, false, false);
			return this.CreateChain(input, false, output, converterReader);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000734C8 File Offset: 0x000716C8
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000734D4 File Offset: 0x000716D4
		private IProducerConsumer CreateChain(Stream input, bool push, ConverterOutput output, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			RtfParser parser = new RtfParser(input, push, base.InputStreamBufferSize, this.testBoundaryConditions, push ? null : progressMonitor, null);
			if (this.textExtractionMode == TextExtractionMode.ExtractText)
			{
				return new RtfTextExtractionConverter(parser, output, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
			}
			Injection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = ((this.injectionFormat == HeaderFooterFormat.Html) ? new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, null, progressMonitor) : new TextInjection(this.injectHead, this.injectTail, this.testBoundaryConditions, null, progressMonitor));
			}
			TextOutput output2 = new TextOutput(output, this.wrapFlowed, this.wrapFlowed, 72, 78, null, this.fallbacks, this.htmlEscape, this.testPreserveTrailingSpaces, this.testFormatTraceStream);
			return new RtfToTextConverter(parser, output2, injection, this.testTreatNbspAsBreakable, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000735D4 File Offset: 0x000717D4
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			if (parameterId == ConfigParameter.OutputEncoding)
			{
				this.outputEncoding = (Encoding)val;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000735FB File Offset: 0x000717FB
		private void AssertNotLockedAndNotTextExtraction()
		{
			base.AssertNotLocked();
			if (this.textExtractionMode == TextExtractionMode.ExtractText)
			{
				throw new InvalidOperationException(TextConvertersStrings.PropertyNotValidForTextExtractionMode);
			}
		}

		// Token: 0x0400101F RID: 4127
		private TextExtractionMode textExtractionMode;

		// Token: 0x04001020 RID: 4128
		private Encoding outputEncoding;

		// Token: 0x04001021 RID: 4129
		private bool outputEncodingSameAsInput = true;

		// Token: 0x04001022 RID: 4130
		private bool wrapFlowed;

		// Token: 0x04001023 RID: 4131
		private bool wrapDelSp;

		// Token: 0x04001024 RID: 4132
		private bool fallbacks = true;

		// Token: 0x04001025 RID: 4133
		private bool htmlEscape;

		// Token: 0x04001026 RID: 4134
		private HeaderFooterFormat injectionFormat;

		// Token: 0x04001027 RID: 4135
		private string injectHead;

		// Token: 0x04001028 RID: 4136
		private string injectTail;

		// Token: 0x04001029 RID: 4137
		private bool testPreserveTrailingSpaces;

		// Token: 0x0400102A RID: 4138
		private bool testTreatNbspAsBreakable;

		// Token: 0x0400102B RID: 4139
		private Stream testTraceStream;

		// Token: 0x0400102C RID: 4140
		private bool testTraceShowTokenNum = true;

		// Token: 0x0400102D RID: 4141
		private int testTraceStopOnTokenNum;

		// Token: 0x0400102E RID: 4142
		private Stream testFormatTraceStream;
	}
}
