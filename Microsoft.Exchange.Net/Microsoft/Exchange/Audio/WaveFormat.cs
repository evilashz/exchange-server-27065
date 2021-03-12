using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000614 RID: 1556
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal class WaveFormat
	{
		// Token: 0x06001BFC RID: 7164 RVA: 0x00032D81 File Offset: 0x00030F81
		internal WaveFormat()
		{
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00032D8C File Offset: 0x00030F8C
		internal WaveFormat(int samplesPerSec, int bitsPerSample, int channels)
		{
			this.formatTag = 1;
			this.channels = (short)channels;
			this.samplesPerSec = samplesPerSec;
			this.bitsPerSample = (short)bitsPerSample;
			this.size = 0;
			this.blockAlign = (short)(channels * (bitsPerSample / 8));
			this.avgBytesPerSec = samplesPerSec * (int)this.blockAlign;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00032DDE File Offset: 0x00030FDE
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00032DE6 File Offset: 0x00030FE6
		internal short FormatTag
		{
			get
			{
				return this.formatTag;
			}
			set
			{
				this.formatTag = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00032DEF File Offset: 0x00030FEF
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x00032DF7 File Offset: 0x00030FF7
		internal short Channels
		{
			get
			{
				return this.channels;
			}
			set
			{
				this.channels = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00032E00 File Offset: 0x00031000
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x00032E08 File Offset: 0x00031008
		internal int SamplesPerSec
		{
			get
			{
				return this.samplesPerSec;
			}
			set
			{
				this.samplesPerSec = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x00032E11 File Offset: 0x00031011
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x00032E19 File Offset: 0x00031019
		internal int AvgBytesPerSec
		{
			get
			{
				return this.avgBytesPerSec;
			}
			set
			{
				this.avgBytesPerSec = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00032E22 File Offset: 0x00031022
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x00032E2A File Offset: 0x0003102A
		internal short BlockAlign
		{
			get
			{
				return this.blockAlign;
			}
			set
			{
				this.blockAlign = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00032E33 File Offset: 0x00031033
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x00032E3B File Offset: 0x0003103B
		internal short BitsPerSample
		{
			get
			{
				return this.bitsPerSample;
			}
			set
			{
				this.bitsPerSample = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00032E44 File Offset: 0x00031044
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x00032E4C File Offset: 0x0003104C
		internal short Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x04001CCC RID: 7372
		internal static readonly WaveFormat Pcm8WaveFormat = new WaveFormat(8000, 16, 1);

		// Token: 0x04001CCD RID: 7373
		internal static readonly WaveFormat Pcm16WaveFormat = new WaveFormat(16000, 16, 1);

		// Token: 0x04001CCE RID: 7374
		private short formatTag;

		// Token: 0x04001CCF RID: 7375
		private short channels;

		// Token: 0x04001CD0 RID: 7376
		private int samplesPerSec;

		// Token: 0x04001CD1 RID: 7377
		private int avgBytesPerSec;

		// Token: 0x04001CD2 RID: 7378
		private short blockAlign;

		// Token: 0x04001CD3 RID: 7379
		private short bitsPerSample;

		// Token: 0x04001CD4 RID: 7380
		private short size;
	}
}
