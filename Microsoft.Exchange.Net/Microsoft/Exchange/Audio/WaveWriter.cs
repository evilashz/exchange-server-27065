using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000617 RID: 1559
	internal abstract class WaveWriter : SoundWriter
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001C18 RID: 7192
		protected abstract int WaveHeaderSize { get; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00032F81 File Offset: 0x00031181
		protected override int DataOffset
		{
			get
			{
				return this.WaveHeaderSize + 8;
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00032F8B File Offset: 0x0003118B
		protected override void WriteFileHeader()
		{
			this.WriteRiffChunk();
			this.WriteFmtChunk();
			this.WriteAdditionalChunks();
			this.WriteDataChunk();
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00032FA8 File Offset: 0x000311A8
		protected virtual void WriteRiffChunk()
		{
			base.Writer.Write(Encoding.ASCII.GetBytes("RIFF"));
			int value = this.WaveHeaderSize + base.NumBytesWritten;
			base.Writer.Write(value);
			base.Writer.Write(Encoding.ASCII.GetBytes("WAVE"));
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00033004 File Offset: 0x00031204
		protected virtual void WriteFmtChunk()
		{
			base.Writer.Write(Encoding.ASCII.GetBytes("fmt "));
			int num = (1 == base.WaveFormat.FormatTag) ? 2 : 0;
			base.Writer.Write(Marshal.SizeOf(base.WaveFormat) - num);
			base.Writer.Write(base.WaveFormat.FormatTag);
			base.Writer.Write(base.WaveFormat.Channels);
			base.Writer.Write((uint)base.WaveFormat.SamplesPerSec);
			base.Writer.Write((uint)base.WaveFormat.AvgBytesPerSec);
			base.Writer.Write(base.WaveFormat.BlockAlign);
			base.Writer.Write(base.WaveFormat.BitsPerSample);
		}

		// Token: 0x06001C1D RID: 7197
		protected abstract void WriteAdditionalChunks();

		// Token: 0x06001C1E RID: 7198 RVA: 0x000330DA File Offset: 0x000312DA
		protected virtual void WriteDataChunk()
		{
			base.Writer.Write(Encoding.ASCII.GetBytes("data"));
			base.Writer.Write(base.NumBytesWritten);
		}

		// Token: 0x04001CDA RID: 7386
		protected const int DataChunkSize = 8;

		// Token: 0x04001CDB RID: 7387
		protected const string RIFF = "RIFF";

		// Token: 0x04001CDC RID: 7388
		protected const string WAVE = "WAVE";

		// Token: 0x04001CDD RID: 7389
		protected const string FMT = "fmt ";

		// Token: 0x04001CDE RID: 7390
		protected const string DATA = "data";
	}
}
