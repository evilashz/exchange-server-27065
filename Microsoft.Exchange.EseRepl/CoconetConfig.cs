using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class CoconetConfig
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003042 File Offset: 0x00001242
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000304A File Offset: 0x0000124A
		public long DictionarySize { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003053 File Offset: 0x00001253
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000305B File Offset: 0x0000125B
		public int SampleRate { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003064 File Offset: 0x00001264
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000306C File Offset: 0x0000126C
		public int LzOption { get; set; }

		// Token: 0x06000076 RID: 118 RVA: 0x00003075 File Offset: 0x00001275
		internal CoconetConfig(long dictionarySize, int sampleRate, int lzOpt)
		{
			this.DictionarySize = dictionarySize;
			this.SampleRate = sampleRate;
			this.LzOption = lzOpt;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003092 File Offset: 0x00001292
		public CoconetConfig() : this(16777216L, 8, 3)
		{
		}
	}
}
