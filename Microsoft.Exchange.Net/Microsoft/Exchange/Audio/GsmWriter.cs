using System;
using System.Text;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200061B RID: 1563
	internal class GsmWriter : WaveWriter
	{
		// Token: 0x06001C2E RID: 7214 RVA: 0x000333B4 File Offset: 0x000315B4
		internal GsmWriter(string fileName)
		{
			base.Create(fileName, new GSM610WAVEFORMAT(true));
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x000333C9 File Offset: 0x000315C9
		protected override int WaveHeaderSize
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000333D0 File Offset: 0x000315D0
		protected override void WriteFmtChunk()
		{
			GSM610WAVEFORMAT gsm610WAVEFORMAT = (GSM610WAVEFORMAT)base.WaveFormat;
			base.WriteFmtChunk();
			base.Writer.Write(gsm610WAVEFORMAT.Size);
			base.Writer.Write(gsm610WAVEFORMAT.SamplesPerBlock);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00033414 File Offset: 0x00031614
		protected override void WriteAdditionalChunks()
		{
			base.Writer.Write(Encoding.ASCII.GetBytes("fact"));
			base.Writer.Write(4);
			base.Writer.Write(base.NumBytesWritten / (int)base.WaveFormat.BlockAlign * (int)((GSM610WAVEFORMAT)base.WaveFormat).SamplesPerBlock);
		}
	}
}
