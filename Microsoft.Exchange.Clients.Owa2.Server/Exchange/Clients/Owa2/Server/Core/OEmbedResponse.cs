using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002ED RID: 749
	[DataContract]
	public class OEmbedResponse
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00057DE0 File Offset: 0x00055FE0
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x00057DE8 File Offset: 0x00055FE8
		[DataMember(Name = "type")]
		public string Type { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00057DF1 File Offset: 0x00055FF1
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x00057DF9 File Offset: 0x00055FF9
		[DataMember(Name = "version")]
		public string Version { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00057E02 File Offset: 0x00056002
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x00057E0A File Offset: 0x0005600A
		[DataMember(Name = "title")]
		public string Title { get; set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00057E13 File Offset: 0x00056013
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x00057E1B File Offset: 0x0005601B
		[DataMember(Name = "author_name")]
		public string AuthorName { get; set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00057E24 File Offset: 0x00056024
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x00057E2C File Offset: 0x0005602C
		[DataMember(Name = "author_url")]
		public string AuthorUrl { get; set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00057E35 File Offset: 0x00056035
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x00057E3D File Offset: 0x0005603D
		[DataMember(Name = "provider_name")]
		public string ProviderName { get; set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00057E46 File Offset: 0x00056046
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x00057E4E File Offset: 0x0005604E
		[DataMember(Name = "provider_url")]
		public string ProviderUrl { get; set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00057E57 File Offset: 0x00056057
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x00057E5F File Offset: 0x0005605F
		[DataMember(Name = "cache_age")]
		public string CacheAge { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00057E68 File Offset: 0x00056068
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x00057E70 File Offset: 0x00056070
		[DataMember(Name = "thumbnail_url")]
		public string ThumbnailUrl { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00057E79 File Offset: 0x00056079
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x00057E81 File Offset: 0x00056081
		[DataMember(Name = "thumbnail_width")]
		public string ThumbnailWidth { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00057E8A File Offset: 0x0005608A
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x00057E92 File Offset: 0x00056092
		[DataMember(Name = "thumbnail_height")]
		public string ThumbnailHeight { get; set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00057E9B File Offset: 0x0005609B
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x00057EA3 File Offset: 0x000560A3
		[DataMember(Name = "url")]
		public string Url { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00057EAC File Offset: 0x000560AC
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x00057EB4 File Offset: 0x000560B4
		[DataMember(Name = "html")]
		public string Html { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00057EBD File Offset: 0x000560BD
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x00057EC5 File Offset: 0x000560C5
		[DataMember(Name = "width")]
		public int Width { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00057ECE File Offset: 0x000560CE
		// (set) Token: 0x06001947 RID: 6471 RVA: 0x00057ED6 File Offset: 0x000560D6
		[DataMember(Name = "height")]
		public int Height { get; set; }
	}
}
