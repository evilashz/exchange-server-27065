using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200061D RID: 1565
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal class MPEGLAYER3WAVEFORMAT : WaveFormat
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x000335F0 File Offset: 0x000317F0
		internal MPEGLAYER3WAVEFORMAT(int bitRate, int samplingRate)
		{
			int num = 144 * bitRate / samplingRate;
			base.FormatTag = 85;
			base.Channels = 1;
			base.SamplesPerSec = samplingRate;
			base.AvgBytesPerSec = bitRate / 8;
			base.BlockAlign = 1;
			base.BitsPerSample = 0;
			base.Size = 12;
			this.ID = 1;
			this.Flags = 2;
			this.FramesPerBlock = 1;
			this.BlockSize = (short)num;
			this.CodecDelay = 1393;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0003366A File Offset: 0x0003186A
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x00033672 File Offset: 0x00031872
		internal short ID { get; set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0003367B File Offset: 0x0003187B
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00033683 File Offset: 0x00031883
		internal int Flags { get; set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0003368C File Offset: 0x0003188C
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x00033694 File Offset: 0x00031894
		internal short BlockSize { get; set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0003369D File Offset: 0x0003189D
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x000336A5 File Offset: 0x000318A5
		internal short FramesPerBlock { get; set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x000336AE File Offset: 0x000318AE
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x000336B6 File Offset: 0x000318B6
		internal short CodecDelay { get; set; }

		// Token: 0x04001CE8 RID: 7400
		internal const short MP3FormatTag = 85;

		// Token: 0x04001CE9 RID: 7401
		internal static readonly MPEGLAYER3WAVEFORMAT WideBandFormat = new MPEGLAYER3WAVEFORMAT(32000, 16000);

		// Token: 0x04001CEA RID: 7402
		internal static readonly MPEGLAYER3WAVEFORMAT NarrowBandFormat = new MPEGLAYER3WAVEFORMAT(16000, 16000);
	}
}
