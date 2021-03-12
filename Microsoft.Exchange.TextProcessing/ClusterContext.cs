using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000011 RID: 17
	internal class ClusterContext
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000A295 File Offset: 0x00008495
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000A29D File Offset: 0x0000849D
		public int LowSimilarityBoundInt { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000A2A6 File Offset: 0x000084A6
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000A2AE File Offset: 0x000084AE
		public int InternalStoreNumber { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000A2B7 File Offset: 0x000084B7
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000A2BF File Offset: 0x000084BF
		public int SwapTimeInMinutes { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000A2C8 File Offset: 0x000084C8
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public int MergeTimeInMinutes { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000A2D9 File Offset: 0x000084D9
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000A2E1 File Offset: 0x000084E1
		public int CleanTimeInMinutes { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000A2EA File Offset: 0x000084EA
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000A2F2 File Offset: 0x000084F2
		public int MaxHashSetSize { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000A2FB File Offset: 0x000084FB
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000A303 File Offset: 0x00008503
		public int FnFeedSizeAbove { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000A30C File Offset: 0x0000850C
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000A314 File Offset: 0x00008514
		public int HoneypotFeedSizeAbove { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000A31D File Offset: 0x0000851D
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000A325 File Offset: 0x00008525
		public int SenFeedSizeAbove { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000A32E File Offset: 0x0000852E
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000A336 File Offset: 0x00008536
		public int ThirdPartyFeedSizeAbove { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000A33F File Offset: 0x0000853F
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000A347 File Offset: 0x00008547
		public int SewrFeedSizeAbove { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000A350 File Offset: 0x00008550
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000A358 File Offset: 0x00008558
		public int SpamSizeAbove { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000A361 File Offset: 0x00008561
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000A369 File Offset: 0x00008569
		public int NearOneSourcePercentageAbove { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000A372 File Offset: 0x00008572
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000A37A File Offset: 0x0000857A
		public int NumberOfRecipientDomainAbove { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000A383 File Offset: 0x00008583
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000A38B File Offset: 0x0000858B
		public int NumberofSourcesMadeOfMajorSourcesAbove { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000A394 File Offset: 0x00008594
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000A39C File Offset: 0x0000859C
		public int SpamFeedClusterSizeAbove { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000A3A5 File Offset: 0x000085A5
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000A3AD File Offset: 0x000085AD
		public int SpamVerdictFeedClusterSizeAbove { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000A3B6 File Offset: 0x000085B6
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000A3BE File Offset: 0x000085BE
		public int AllOneSourceClusterSizeAbove { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000A3C7 File Offset: 0x000085C7
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000A3CF File Offset: 0x000085CF
		public int OneAndMultiSourceClusterSizeAbove { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000A3D8 File Offset: 0x000085D8
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public int AllMultiSourceClusterSizeAbove { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000A3E9 File Offset: 0x000085E9
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000A3F1 File Offset: 0x000085F1
		public bool UseBloomFilter { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000A3FA File Offset: 0x000085FA
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000A402 File Offset: 0x00008602
		public int PowerIndexOf2 { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000A40B File Offset: 0x0000860B
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000A413 File Offset: 0x00008613
		public int MaxCountValue { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000A41C File Offset: 0x0000861C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000A424 File Offset: 0x00008624
		public int HashNumbers { get; set; }
	}
}
