using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000616 RID: 1558
	internal abstract class SoundWriter : IDisposable
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00032E91 File Offset: 0x00031091
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x00032E99 File Offset: 0x00031099
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
			protected set
			{
				this.waveFormat = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00032EA2 File Offset: 0x000310A2
		protected BinaryWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001C11 RID: 7185
		protected abstract int DataOffset { get; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00032EAA File Offset: 0x000310AA
		protected int NumBytesWritten
		{
			get
			{
				return this.numBytesWritten;
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00032EB4 File Offset: 0x000310B4
		public void Dispose()
		{
			if (!this.closed)
			{
				this.waveStream.Seek(0L, SeekOrigin.Begin);
				this.WriteFileHeader();
				if (this.waveStream != null)
				{
					this.waveStream.Close();
					this.waveStream = null;
				}
				if (this.writer != null)
				{
					this.writer.Close();
					this.writer = null;
				}
				this.closed = true;
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00032F19 File Offset: 0x00031119
		internal void Write(byte[] buffer, int count)
		{
			this.writer.Write(buffer, 0, count);
			this.numBytesWritten += count;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00032F37 File Offset: 0x00031137
		protected void Create(string fileName, WaveFormat waveFormat)
		{
			this.waveStream = new FileStream(fileName, FileMode.Create);
			this.waveStream.Seek((long)this.DataOffset, SeekOrigin.Begin);
			this.writer = new BinaryWriter(this.waveStream, Encoding.ASCII);
			this.waveFormat = waveFormat;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00032F77 File Offset: 0x00031177
		protected virtual void WriteFileHeader()
		{
		}

		// Token: 0x04001CD5 RID: 7381
		private Stream waveStream;

		// Token: 0x04001CD6 RID: 7382
		private WaveFormat waveFormat;

		// Token: 0x04001CD7 RID: 7383
		private bool closed;

		// Token: 0x04001CD8 RID: 7384
		private int numBytesWritten;

		// Token: 0x04001CD9 RID: 7385
		private BinaryWriter writer;
	}
}
