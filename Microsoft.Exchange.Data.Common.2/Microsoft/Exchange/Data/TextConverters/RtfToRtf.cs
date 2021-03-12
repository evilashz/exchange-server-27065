using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000163 RID: 355
	public class RtfToRtf : TextConverter
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x000740B3 File Offset: 0x000722B3
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x000740BB File Offset: 0x000722BB
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000740CA File Offset: 0x000722CA
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x000740D2 File Offset: 0x000722D2
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x000740E1 File Offset: 0x000722E1
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x000740E9 File Offset: 0x000722E9
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

		// Token: 0x06000F11 RID: 3857 RVA: 0x000740F8 File Offset: 0x000722F8
		internal RtfToRtf SetHeaderFooterFormat(HeaderFooterFormat value)
		{
			this.HeaderFooterFormat = value;
			return this;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00074102 File Offset: 0x00072302
		internal RtfToRtf SetHeader(string value)
		{
			this.Header = value;
			return this;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0007410C File Offset: 0x0007230C
		internal RtfToRtf SetFooter(string value)
		{
			this.Footer = value;
			return this;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00074116 File Offset: 0x00072316
		internal RtfToRtf SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00074120 File Offset: 0x00072320
		internal RtfToRtf SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0007412C File Offset: 0x0007232C
		internal RtfToRtf SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00074136 File Offset: 0x00072336
		internal RtfToRtf SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00074140 File Offset: 0x00072340
		internal RtfToRtf SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0007414A File Offset: 0x0007234A
		internal RtfToRtf SetTestInjectionTraceStream(Stream value)
		{
			this.testInjectionTraceStream = value;
			return this;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00074154 File Offset: 0x00072354
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			return this.CreateChain(converterStream, output, true, converterStream);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00074160 File Offset: 0x00072360
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0007416C File Offset: 0x0007236C
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00074178 File Offset: 0x00072378
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00074184 File Offset: 0x00072384
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			return this.CreateChain(input, converterStream, false, converterStream);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00074190 File Offset: 0x00072390
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0007419C File Offset: 0x0007239C
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000741A8 File Offset: 0x000723A8
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000741B4 File Offset: 0x000723B4
		private RtfToRtfConverter CreateChain(Stream input, Stream output, bool push, IProgressMonitor progressMonitor)
		{
			this.locked = true;
			RtfParser parser = new RtfParser(input, push, base.InputStreamBufferSize, this.testBoundaryConditions, push ? null : progressMonitor, null);
			Injection injection = null;
			if (this.injectHead != null || this.injectTail != null)
			{
				injection = ((this.injectionFormat == HeaderFooterFormat.Html) ? new HtmlInjection(this.injectHead, this.injectTail, this.injectionFormat, false, null, this.testBoundaryConditions, this.testInjectionTraceStream, progressMonitor) : new TextInjection(this.injectHead, this.injectTail, this.testBoundaryConditions, this.testInjectionTraceStream, progressMonitor));
			}
			return new RtfToRtfConverter(parser, output, push, injection, this.testTraceStream, this.testTraceShowTokenNum, this.testTraceStopOnTokenNum);
		}

		// Token: 0x04001059 RID: 4185
		private HeaderFooterFormat injectionFormat;

		// Token: 0x0400105A RID: 4186
		private string injectHead;

		// Token: 0x0400105B RID: 4187
		private string injectTail;

		// Token: 0x0400105C RID: 4188
		private Stream testTraceStream;

		// Token: 0x0400105D RID: 4189
		private bool testTraceShowTokenNum = true;

		// Token: 0x0400105E RID: 4190
		private int testTraceStopOnTokenNum;

		// Token: 0x0400105F RID: 4191
		private Stream testInjectionTraceStream;
	}
}
