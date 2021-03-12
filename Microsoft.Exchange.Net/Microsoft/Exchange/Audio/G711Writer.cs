using System;
using System.Text;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000618 RID: 1560
	internal class G711Writer : WaveWriter
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x0003310F File Offset: 0x0003130F
		internal G711Writer(string fileName, G711Format format)
		{
			base.Create(fileName, new G711WAVEFORMAT(format));
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00033124 File Offset: 0x00031324
		protected override int WaveHeaderSize
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00033128 File Offset: 0x00031328
		protected override void WriteFmtChunk()
		{
			G711WAVEFORMAT g711WAVEFORMAT = (G711WAVEFORMAT)base.WaveFormat;
			base.WriteFmtChunk();
			base.Writer.Write(g711WAVEFORMAT.Size);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00033158 File Offset: 0x00031358
		protected override void WriteAdditionalChunks()
		{
			base.Writer.Write(Encoding.ASCII.GetBytes("fact"));
			base.Writer.Write(4);
			base.Writer.Write(base.NumBytesWritten);
		}
	}
}
