using System;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200061E RID: 1566
	internal class Mp3Writer : SoundWriter
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x000336E9 File Offset: 0x000318E9
		internal Mp3Writer(string fileName, MPEGLAYER3WAVEFORMAT format)
		{
			base.Create(fileName, format);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000336F9 File Offset: 0x000318F9
		internal static Mp3Writer Create(string outputFile, int inputSamplingRate)
		{
			if (inputSamplingRate == 8000)
			{
				return new Mp3Writer(outputFile, MPEGLAYER3WAVEFORMAT.NarrowBandFormat);
			}
			if (inputSamplingRate == 16000)
			{
				return new Mp3Writer(outputFile, MPEGLAYER3WAVEFORMAT.WideBandFormat);
			}
			throw new UnsupportedAudioFormat(outputFile);
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00033729 File Offset: 0x00031929
		protected override int DataOffset
		{
			get
			{
				return 0;
			}
		}
	}
}
