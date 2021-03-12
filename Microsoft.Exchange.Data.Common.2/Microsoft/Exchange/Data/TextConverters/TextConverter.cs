using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000155 RID: 341
	public abstract class TextConverter : IResultsFeedback
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x000709EC File Offset: 0x0006EBEC
		internal TextConverter()
		{
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00070A0A File Offset: 0x0006EC0A
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00070A12 File Offset: 0x0006EC12
		internal bool TestBoundaryConditions
		{
			get
			{
				return this.testBoundaryConditions;
			}
			set
			{
				this.AssertNotLocked();
				this.testBoundaryConditions = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00070A21 File Offset: 0x0006EC21
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00070A29 File Offset: 0x0006EC29
		public int InputStreamBufferSize
		{
			get
			{
				return this.inputBufferSize;
			}
			set
			{
				this.AssertNotLocked();
				if (value < 1024 || value > 81920)
				{
					throw new ArgumentOutOfRangeException("value", TextConvertersStrings.BufferSizeValueRange);
				}
				this.inputBufferSize = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00070A58 File Offset: 0x0006EC58
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00070A60 File Offset: 0x0006EC60
		public int OutputStreamBufferSize
		{
			get
			{
				return this.outputBufferSize;
			}
			set
			{
				this.AssertNotLocked();
				if (value < 1024 || value > 81920)
				{
					throw new ArgumentOutOfRangeException("value", TextConvertersStrings.BufferSizeValueRange);
				}
				this.outputBufferSize = value;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00070A90 File Offset: 0x0006EC90
		public void Convert(Stream sourceStream, Stream destinationStream)
		{
			if (destinationStream == null)
			{
				throw new ArgumentNullException("destinationStream");
			}
			Stream stream = new ConverterStream(sourceStream, this, ConverterStreamAccess.Read);
			byte[] array = new byte[this.outputBufferSize];
			for (;;)
			{
				int num = stream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				destinationStream.Write(array, 0, num);
			}
			destinationStream.Flush();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00070AE0 File Offset: 0x0006ECE0
		public void Convert(Stream sourceStream, TextWriter destinationWriter)
		{
			if (destinationWriter == null)
			{
				throw new ArgumentNullException("destinationWriter");
			}
			TextReader textReader = new ConverterReader(sourceStream, this);
			char[] array = new char[4096];
			for (;;)
			{
				int num = textReader.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				destinationWriter.Write(array, 0, num);
			}
			destinationWriter.Flush();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00070B30 File Offset: 0x0006ED30
		public void Convert(TextReader sourceReader, Stream destinationStream)
		{
			if (destinationStream == null)
			{
				throw new ArgumentNullException("destinationStream");
			}
			Stream stream = new ConverterStream(sourceReader, this);
			byte[] array = new byte[this.outputBufferSize];
			for (;;)
			{
				int num = stream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				destinationStream.Write(array, 0, num);
			}
			destinationStream.Flush();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00070B80 File Offset: 0x0006ED80
		public void Convert(TextReader sourceReader, TextWriter destinationWriter)
		{
			if (destinationWriter == null)
			{
				throw new ArgumentNullException("destinationWriter");
			}
			TextReader textReader = new ConverterReader(sourceReader, this);
			char[] array = new char[4096];
			for (;;)
			{
				int num = textReader.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				destinationWriter.Write(array, 0, num);
			}
			destinationWriter.Flush();
		}

		// Token: 0x06000D36 RID: 3382
		internal abstract IProducerConsumer CreatePushChain(ConverterStream converterStream, Stream output);

		// Token: 0x06000D37 RID: 3383
		internal abstract IProducerConsumer CreatePushChain(ConverterStream converterStream, TextWriter output);

		// Token: 0x06000D38 RID: 3384
		internal abstract IProducerConsumer CreatePushChain(ConverterWriter converterWriter, Stream output);

		// Token: 0x06000D39 RID: 3385
		internal abstract IProducerConsumer CreatePushChain(ConverterWriter converterWriter, TextWriter output);

		// Token: 0x06000D3A RID: 3386
		internal abstract IProducerConsumer CreatePullChain(Stream input, ConverterStream converterStream);

		// Token: 0x06000D3B RID: 3387
		internal abstract IProducerConsumer CreatePullChain(TextReader input, ConverterStream converterStream);

		// Token: 0x06000D3C RID: 3388
		internal abstract IProducerConsumer CreatePullChain(Stream input, ConverterReader converterReader);

		// Token: 0x06000D3D RID: 3389
		internal abstract IProducerConsumer CreatePullChain(TextReader input, ConverterReader converterReader);

		// Token: 0x06000D3E RID: 3390 RVA: 0x00070BCE File Offset: 0x0006EDCE
		internal virtual void SetResult(ConfigParameter parameterId, object val)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00070BD0 File Offset: 0x0006EDD0
		void IResultsFeedback.Set(ConfigParameter parameterId, object val)
		{
			this.SetResult(parameterId, val);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00070BDA File Offset: 0x0006EDDA
		internal void AssertNotLocked()
		{
			if (this.locked)
			{
				throw new InvalidOperationException(TextConvertersStrings.ParametersCannotBeChangedAfterConverterObjectIsUsed);
			}
		}

		// Token: 0x04000F8D RID: 3981
		protected bool testBoundaryConditions;

		// Token: 0x04000F8E RID: 3982
		private int inputBufferSize = 4096;

		// Token: 0x04000F8F RID: 3983
		private int outputBufferSize = 4096;

		// Token: 0x04000F90 RID: 3984
		protected bool locked;
	}
}
