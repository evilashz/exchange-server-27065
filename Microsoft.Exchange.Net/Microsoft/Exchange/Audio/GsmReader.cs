using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200061A RID: 1562
	internal class GsmReader : WaveReader
	{
		// Token: 0x06001C27 RID: 7207 RVA: 0x00033201 File Offset: 0x00031401
		internal GsmReader(string fileName)
		{
			base.Create(fileName);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00033210 File Offset: 0x00031410
		protected GsmReader()
		{
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00033218 File Offset: 0x00031418
		protected override int MinimumLength
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0003321C File Offset: 0x0003141C
		internal static bool TryCreate(string fileName, out SoundReader soundReader)
		{
			soundReader = null;
			if (AudioFile.IsWav(fileName))
			{
				soundReader = new GsmReader();
				if (!((GsmReader)soundReader).Initialize(fileName))
				{
					soundReader.Dispose();
					soundReader = null;
				}
			}
			return soundReader != null;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00033251 File Offset: 0x00031451
		protected override bool ReadRiffHeader(BinaryReader reader)
		{
			return this.ReadWaveChunk(reader) && this.ReadFmtChunk(reader) && this.ReadFactChunk(reader) && this.ReadDataChunk(reader);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0003327C File Offset: 0x0003147C
		protected override bool ReadFmtChunk(BinaryReader reader)
		{
			base.WaveFormat = new GSM610WAVEFORMAT(false);
			WaveFormat waveFormat = new GSM610WAVEFORMAT(true);
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
			if (base.WaveFormat.FormatTag != waveFormat.FormatTag || base.WaveFormat.Size != waveFormat.Size)
			{
				return false;
			}
			((GSM610WAVEFORMAT)base.WaveFormat).SamplesPerBlock = reader.ReadInt16();
			return true;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00033389 File Offset: 0x00031589
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

		// Token: 0x04001CE0 RID: 7392
		internal const int GsmHeaderSize = 52;
	}
}
