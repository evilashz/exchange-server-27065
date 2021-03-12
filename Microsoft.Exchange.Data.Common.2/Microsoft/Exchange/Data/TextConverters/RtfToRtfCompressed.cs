using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.RtfCompressed;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000168 RID: 360
	public class RtfToRtfCompressed : TextConverter
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000754B1 File Offset: 0x000736B1
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x000754B9 File Offset: 0x000736B9
		public RtfCompressionMode CompressionMode
		{
			get
			{
				return this.compressionMode;
			}
			set
			{
				base.AssertNotLocked();
				this.compressionMode = value;
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000754C8 File Offset: 0x000736C8
		internal RtfToRtfCompressed SetCompressionMode(RtfCompressionMode value)
		{
			this.CompressionMode = value;
			return this;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000754D2 File Offset: 0x000736D2
		internal RtfToRtfCompressed SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000754DC File Offset: 0x000736DC
		internal RtfToRtfCompressed SetOutputStreamBufferSize(int value)
		{
			base.OutputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000754E6 File Offset: 0x000736E6
		internal RtfToRtfCompressed SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000754F0 File Offset: 0x000736F0
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			return new RtfCompressConverter(converterStream, true, output, this.compressionMode, base.InputStreamBufferSize, base.OutputStreamBufferSize);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0007550C File Offset: 0x0007370C
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00075518 File Offset: 0x00073718
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00075524 File Offset: 0x00073724
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00075530 File Offset: 0x00073730
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			return new RtfCompressConverter(input, false, converterStream, this.compressionMode, base.InputStreamBufferSize, base.OutputStreamBufferSize);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0007554C File Offset: 0x0007374C
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00075558 File Offset: 0x00073758
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00075564 File Offset: 0x00073764
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x0400109D RID: 4253
		private RtfCompressionMode compressionMode;
	}
}
