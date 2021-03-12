using System;
using System.IO;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000611 RID: 1553
	internal class PcmReader : WaveReader
	{
		// Token: 0x06001BED RID: 7149 RVA: 0x000329B3 File Offset: 0x00030BB3
		internal PcmReader(string fileName)
		{
			base.Create(fileName);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000329C2 File Offset: 0x00030BC2
		protected PcmReader()
		{
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000329CA File Offset: 0x00030BCA
		protected override int MinimumLength
		{
			get
			{
				return 38;
			}
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000329D0 File Offset: 0x00030BD0
		internal static bool TryCreate(string fileName, out SoundReader soundReader)
		{
			soundReader = null;
			if (AudioFile.IsWav(fileName))
			{
				soundReader = new PcmReader();
				if (!((PcmReader)soundReader).Initialize(fileName) || !((PcmReader)soundReader).IsSupportedInput())
				{
					soundReader.Dispose();
					soundReader = null;
				}
			}
			return soundReader != null;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00032A1E File Offset: 0x00030C1E
		protected override bool ReadRiffHeader(BinaryReader reader)
		{
			return this.ReadWaveChunk(reader) && this.ReadFmtChunk(reader) && this.ReadDataChunk(reader);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00032A40 File Offset: 0x00030C40
		protected override bool ReadFmtChunk(BinaryReader reader)
		{
			if (base.GetString(reader, 4) != "fmt ")
			{
				return false;
			}
			base.FormatLength = reader.ReadInt32();
			if (base.FormatLength < 16)
			{
				return false;
			}
			base.WaveFormat = new WaveFormat();
			base.WaveFormat.FormatTag = reader.ReadInt16();
			base.WaveFormat.Channels = reader.ReadInt16();
			base.WaveFormat.SamplesPerSec = reader.ReadInt32();
			base.WaveFormat.AvgBytesPerSec = reader.ReadInt32();
			base.WaveFormat.BlockAlign = reader.ReadInt16();
			base.WaveFormat.BitsPerSample = reader.ReadInt16();
			if (base.WaveFormat.FormatTag != 1)
			{
				return false;
			}
			if (base.FormatLength > 16)
			{
				base.WaveStream.Position += (long)(base.FormatLength - 16);
			}
			return true;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00032B24 File Offset: 0x00030D24
		private bool IsSupportedInput()
		{
			foreach (WaveFormat waveFormat in PcmReader.SupportedInputFormats)
			{
				if (base.WaveFormat.Channels == waveFormat.Channels && base.WaveFormat.FormatTag == waveFormat.FormatTag && base.WaveFormat.SamplesPerSec == waveFormat.SamplesPerSec && base.WaveFormat.BitsPerSample == waveFormat.BitsPerSample)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001CC5 RID: 7365
		internal const int WaveHeaderSize = 38;

		// Token: 0x04001CC6 RID: 7366
		private const int ExpectedWaveFormatSize = 16;

		// Token: 0x04001CC7 RID: 7367
		private static readonly WaveFormat[] SupportedInputFormats = new WaveFormat[]
		{
			WaveFormat.Pcm8WaveFormat,
			WaveFormat.Pcm16WaveFormat
		};
	}
}
