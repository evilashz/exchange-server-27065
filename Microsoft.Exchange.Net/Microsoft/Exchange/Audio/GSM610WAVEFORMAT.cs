using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000619 RID: 1561
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal class GSM610WAVEFORMAT : WaveFormat
	{
		// Token: 0x06001C24 RID: 7204 RVA: 0x00033194 File Offset: 0x00031394
		internal GSM610WAVEFORMAT(bool useDefaults)
		{
			if (useDefaults)
			{
				base.FormatTag = 49;
				base.Channels = 1;
				base.SamplesPerSec = 8000;
				base.AvgBytesPerSec = 1625;
				base.BlockAlign = 65;
				base.BitsPerSample = 0;
				base.Size = 2;
				this.samplesPerBlock = 320;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000331F0 File Offset: 0x000313F0
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x000331F8 File Offset: 0x000313F8
		internal short SamplesPerBlock
		{
			get
			{
				return this.samplesPerBlock;
			}
			set
			{
				this.samplesPerBlock = value;
			}
		}

		// Token: 0x04001CDF RID: 7391
		private short samplesPerBlock;
	}
}
