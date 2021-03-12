using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public class CoconetConfig
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00028DF0 File Offset: 0x00026FF0
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00028DF8 File Offset: 0x00026FF8
		public long DictionarySize { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00028E01 File Offset: 0x00027001
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00028E09 File Offset: 0x00027009
		public int SampleRate { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00028E12 File Offset: 0x00027012
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00028E1A File Offset: 0x0002701A
		public int LzOption { get; set; }

		// Token: 0x06000893 RID: 2195 RVA: 0x00028E23 File Offset: 0x00027023
		internal CoconetConfig(long dictionarySize, int sampleRate, int lzOpt)
		{
			this.DictionarySize = dictionarySize;
			this.SampleRate = sampleRate;
			this.LzOption = lzOpt;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00028E40 File Offset: 0x00027040
		public CoconetConfig() : this(16777216L, 8, 3)
		{
		}
	}
}
