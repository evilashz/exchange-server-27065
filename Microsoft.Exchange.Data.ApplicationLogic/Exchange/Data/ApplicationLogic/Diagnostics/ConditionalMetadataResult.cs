using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000DA RID: 218
	[XmlRoot("CompletedCondition")]
	[XmlType("CompletedCondition")]
	public class ConditionalMetadataResult
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00024F1E File Offset: 0x0002311E
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00024F26 File Offset: 0x00023126
		public string Cookie { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00024F2F File Offset: 0x0002312F
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00024F37 File Offset: 0x00023137
		public string User { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00024F40 File Offset: 0x00023140
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x00024F48 File Offset: 0x00023148
		[XmlArrayItem("HitFile")]
		public string[] Files { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00024F51 File Offset: 0x00023151
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00024F59 File Offset: 0x00023159
		public int CurrentHits { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00024F62 File Offset: 0x00023162
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00024F6A File Offset: 0x0002316A
		public DateTime Created { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00024F73 File Offset: 0x00023173
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00024F7B File Offset: 0x0002317B
		public int MaxHits { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00024F84 File Offset: 0x00023184
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00024F8C File Offset: 0x0002318C
		public string Description { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00024F95 File Offset: 0x00023195
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00024F9D File Offset: 0x0002319D
		public string SelectClause { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00024FA6 File Offset: 0x000231A6
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00024FAE File Offset: 0x000231AE
		public string WhereClause { get; set; }
	}
}
