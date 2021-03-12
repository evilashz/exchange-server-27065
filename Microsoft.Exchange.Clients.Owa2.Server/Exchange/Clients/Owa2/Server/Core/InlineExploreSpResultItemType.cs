using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E0 RID: 992
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InlineExploreSpResultItemType
	{
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x000783CD File Offset: 0x000765CD
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x000783D5 File Offset: 0x000765D5
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000783DE File Offset: 0x000765DE
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x000783E6 File Offset: 0x000765E6
		[DataMember]
		public string Url { get; set; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x000783EF File Offset: 0x000765EF
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x000783F7 File Offset: 0x000765F7
		[DataMember]
		public string FileType { get; set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00078400 File Offset: 0x00076600
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x00078408 File Offset: 0x00076608
		[DataMember]
		public string LastModifiedTime { get; set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00078411 File Offset: 0x00076611
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x00078419 File Offset: 0x00076619
		[DataMember]
		public string Summary { get; set; }
	}
}
