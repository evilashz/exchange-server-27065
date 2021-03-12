using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000612 RID: 1554
	internal class G711Reader : PcmReader
	{
		// Token: 0x06001BF5 RID: 7157 RVA: 0x00032BC6 File Offset: 0x00030DC6
		internal G711Reader(string fileName)
		{
			base.Create(fileName);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00032BD5 File Offset: 0x00030DD5
		protected G711Reader()
		{
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00032BDD File Offset: 0x00030DDD
		protected override int MinimumLength
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00032BE1 File Offset: 0x00030DE1
		internal new static bool TryCreate(string fileName, out SoundReader soundReader)
		{
			soundReader = null;
			if (AudioFile.IsWav(fileName))
			{
				soundReader = new G711Reader();
				if (!((G711Reader)soundReader).Initialize(fileName))
				{
					soundReader.Dispose();
					soundReader = null;
				}
			}
			return soundReader != null;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00032C16 File Offset: 0x00030E16
		protected override bool ReadRiffHeader(BinaryReader reader)
		{
			return this.ReadWaveChunk(reader) && this.ReadFmtChunk(reader) && this.ReadFactChunk(reader) && this.ReadDataChunk(reader);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00032C40 File Offset: 0x00030E40
		protected override bool ReadFmtChunk(BinaryReader reader)
		{
			base.WaveFormat = new G711WAVEFORMAT(G711Format.ALAW);
			if (base.GetString(reader, 4) != "fmt ")
			{
				return false;
			}
			base.FormatLength = reader.ReadInt32();
			if (base.FormatLength != Marshal.SizeOf(base.WaveFormat))
			{
				return false;
			}
			base.WaveFormat.FormatTag = reader.ReadInt16();
			base.WaveFormat.Channels = reader.ReadInt16();
			base.WaveFormat.SamplesPerSec = reader.ReadInt32();
			base.WaveFormat.AvgBytesPerSec = reader.ReadInt32();
			base.WaveFormat.BlockAlign = reader.ReadInt16();
			base.WaveFormat.BitsPerSample = reader.ReadInt16();
			base.WaveFormat.Size = reader.ReadInt16();
			return (base.WaveFormat.FormatTag == 6 || base.WaveFormat.FormatTag == 7) && 1 == base.WaveFormat.Channels && 8000 == base.WaveFormat.SamplesPerSec && 8 == base.WaveFormat.BitsPerSample;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00032D56 File Offset: 0x00030F56
		private bool ReadFactChunk(BinaryReader reader)
		{
			if ("fact" != base.GetString(reader, 4))
			{
				return false;
			}
			if (4 != reader.ReadInt32())
			{
				return false;
			}
			reader.ReadInt32();
			return true;
		}

		// Token: 0x04001CC8 RID: 7368
		internal const int G711HeaderSize = 52;
	}
}
