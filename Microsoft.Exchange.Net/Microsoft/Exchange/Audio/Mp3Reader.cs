using System;
using System.IO;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200061C RID: 1564
	internal class Mp3Reader : SoundReader
	{
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00033475 File Offset: 0x00031675
		protected override int MinimumLength
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00033478 File Offset: 0x00031678
		internal static bool TryCreate(string fileName, out SoundReader soundReader)
		{
			soundReader = null;
			if (AudioFile.IsMp3(fileName))
			{
				Mp3Reader mp3Reader = new Mp3Reader();
				if (!mp3Reader.Initialize(fileName))
				{
					mp3Reader.Dispose();
					mp3Reader = null;
				}
				soundReader = mp3Reader;
			}
			return soundReader != null;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000334B4 File Offset: 0x000316B4
		protected override bool ReadHeader(BinaryReader reader)
		{
			while (base.WaveStream.Position + 4L <= base.WaveStream.Length)
			{
				byte b = reader.ReadByte();
				if (b == 255)
				{
					b = reader.ReadByte();
					if ((b & 224) == 224)
					{
						int num = (b & 24) >> 3;
						Mp3Reader.ThrowIfFalse(num == 2);
						int num2 = (b & 6) >> 1;
						Mp3Reader.ThrowIfFalse(num2 == 1);
						b = reader.ReadByte();
						int num3 = (b & 240) >> 4;
						Mp3Reader.ThrowIfFalse(num3 == 2 || num3 == 4);
						int num4 = (b & 12) >> 2;
						Mp3Reader.ThrowIfFalse(num4 == 2);
						int num5 = (b & 2) >> 1;
						Mp3Reader.ThrowIfFalse(num5 == 0);
						b = reader.ReadByte();
						int num6 = (b & 192) >> 6;
						Mp3Reader.ThrowIfFalse(num6 == 3);
						base.WaveFormat = ((num3 == 4) ? MPEGLAYER3WAVEFORMAT.WideBandFormat : MPEGLAYER3WAVEFORMAT.NarrowBandFormat);
						base.WaveDataLength = (int)base.WaveStream.Length;
						base.WaveDataPosition = 0L;
						break;
					}
				}
			}
			base.WaveStream.Seek(0L, SeekOrigin.Begin);
			return base.WaveFormat != null;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000335DA File Offset: 0x000317DA
		private static void ThrowIfFalse(bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x04001CE1 RID: 7393
		private const int Mpeg16Kbps = 2;

		// Token: 0x04001CE2 RID: 7394
		private const int Mpeg32Kbps = 4;

		// Token: 0x04001CE3 RID: 7395
		private const int MpegLayer3 = 1;

		// Token: 0x04001CE4 RID: 7396
		private const int MpegVersion20 = 2;

		// Token: 0x04001CE5 RID: 7397
		private const int MpegSingleChannel = 3;

		// Token: 0x04001CE6 RID: 7398
		private const int MpegBitrate16Kbps = 1;

		// Token: 0x04001CE7 RID: 7399
		private const int MpegV2SamplingRate16Khz = 2;
	}
}
