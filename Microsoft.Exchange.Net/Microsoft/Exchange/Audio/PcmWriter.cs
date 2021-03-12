using System;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200061F RID: 1567
	internal class PcmWriter : WaveWriter
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x0003372C File Offset: 0x0003192C
		internal PcmWriter(string fileName, WaveFormat waveFormat)
		{
			base.Create(fileName, waveFormat);
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0003373C File Offset: 0x0003193C
		protected override int WaveHeaderSize
		{
			get
			{
				return 38;
			}
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00033740 File Offset: 0x00031940
		protected override void WriteAdditionalChunks()
		{
		}
	}
}
