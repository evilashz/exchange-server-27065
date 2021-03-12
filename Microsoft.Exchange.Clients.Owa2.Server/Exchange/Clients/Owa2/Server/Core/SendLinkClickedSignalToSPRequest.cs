using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003FC RID: 1020
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendLinkClickedSignalToSPRequest
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x0007949E File Offset: 0x0007769E
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x000794A6 File Offset: 0x000776A6
		[DataMember]
		public ItemId ID { get; set; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x000794AF File Offset: 0x000776AF
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x000794B7 File Offset: 0x000776B7
		[DataMember]
		public string Url { get; set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000794C0 File Offset: 0x000776C0
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x000794C8 File Offset: 0x000776C8
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000794D1 File Offset: 0x000776D1
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x000794D9 File Offset: 0x000776D9
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000794E2 File Offset: 0x000776E2
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x000794EA File Offset: 0x000776EA
		[DataMember]
		public string ImgURL { get; set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000794F3 File Offset: 0x000776F3
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x000794FB File Offset: 0x000776FB
		[DataMember]
		public string ImgDimensions { get; set; }
	}
}
