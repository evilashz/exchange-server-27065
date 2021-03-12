using System;
using System.Runtime.Serialization;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000010 RID: 16
	[DataContract(Name = "AutoDiscover", Namespace = "http://microsoft.com/exoanalytics")]
	internal class AutoDiscoverRawData : InsightRawData
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004050 File Offset: 0x00002250
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00004058 File Offset: 0x00002258
		[DataMember(Name = "RequestTime")]
		public string RequestTime { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004061 File Offset: 0x00002261
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004069 File Offset: 0x00002269
		[DataMember(Name = "UserAgent")]
		public string UserAgent { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004072 File Offset: 0x00002272
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000407A File Offset: 0x0000227A
		[DataMember(Name = "SoapAction")]
		public string SoapAction { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004083 File Offset: 0x00002283
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000408B File Offset: 0x0000228B
		[DataMember(Name = "HttpStatus")]
		public string HttpStatus { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004094 File Offset: 0x00002294
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000409C File Offset: 0x0000229C
		[DataMember(Name = "ErrorCode")]
		public string ErrorCode { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000040A5 File Offset: 0x000022A5
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000040AD File Offset: 0x000022AD
		[DataMember(Name = "GenericErrors")]
		public string GenericErrors { get; set; }
	}
}
