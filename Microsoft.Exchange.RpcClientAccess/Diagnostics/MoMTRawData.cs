using System;
using System.Runtime.Serialization;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200003B RID: 59
	[DataContract(Name = "MoMT", Namespace = "http://microsoft.com/exoanalytics")]
	internal class MoMTRawData : InsightRawData
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00008801 File Offset: 0x00006A01
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00008809 File Offset: 0x00006A09
		[DataMember(Name = "DateTimeUtc")]
		public string DateTimeUtc { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00008812 File Offset: 0x00006A12
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000881A File Offset: 0x00006A1A
		[DataMember(Name = "ClientName")]
		public string ClientName { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008823 File Offset: 0x00006A23
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000882B File Offset: 0x00006A2B
		[DataMember(Name = "OrganizationInfo")]
		public string OrganizationInfo { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00008834 File Offset: 0x00006A34
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000883C File Offset: 0x00006A3C
		[DataMember(Name = "Failures")]
		public string Failures { get; set; }
	}
}
