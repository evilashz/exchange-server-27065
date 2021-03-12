using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.RtfCompressed;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000169 RID: 361
	public class RtfCompressedToRtf : TextConverter
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0007557F File Offset: 0x0007377F
		public RtfCompressionMode CompressionMode
		{
			get
			{
				return this.compressionMode;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00075587 File Offset: 0x00073787
		internal RtfCompressedToRtf SetInputStreamBufferSize(int value)
		{
			base.InputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00075591 File Offset: 0x00073791
		internal RtfCompressedToRtf SetOutputStreamBufferSize(int value)
		{
			base.OutputStreamBufferSize = value;
			return this;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0007559B File Offset: 0x0007379B
		internal RtfCompressedToRtf SetTestDisableFastLoop(bool value)
		{
			this.testDisableFastLoop = value;
			return this;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000755A5 File Offset: 0x000737A5
		internal RtfCompressedToRtf SetTestBoundaryConditions(bool value)
		{
			base.TestBoundaryConditions = value;
			return this;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000755AF File Offset: 0x000737AF
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output)
		{
			return new RtfDecompressConverter(converterStream, true, output, this.testDisableFastLoop, this, base.InputStreamBufferSize, base.OutputStreamBufferSize);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000755CC File Offset: 0x000737CC
		internal override IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.TextWriterUnsupported);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000755D8 File Offset: 0x000737D8
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000755E4 File Offset: 0x000737E4
		internal override IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterWriter);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000755F0 File Offset: 0x000737F0
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream)
		{
			return new RtfDecompressConverter(input, false, converterStream, this.testDisableFastLoop, this, base.InputStreamBufferSize, base.OutputStreamBufferSize);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0007560D File Offset: 0x0007380D
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00075619 File Offset: 0x00073819
		internal override IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.CannotUseConverterReader);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00075625 File Offset: 0x00073825
		internal override IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader)
		{
			throw new NotSupportedException(TextConvertersStrings.TextReaderUnsupported);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00075634 File Offset: 0x00073834
		internal override void SetResult(ConfigParameter parameterId, object val)
		{
			if (parameterId == ConfigParameter.RtfCompressionMode)
			{
				this.compressionMode = (RtfCompressionMode)val;
			}
			base.SetResult(parameterId, val);
		}

		// Token: 0x0400109E RID: 4254
		private RtfCompressionMode compressionMode = RtfCompressionMode.Uncompressed;

		// Token: 0x0400109F RID: 4255
		private bool testDisableFastLoop;
	}
}
