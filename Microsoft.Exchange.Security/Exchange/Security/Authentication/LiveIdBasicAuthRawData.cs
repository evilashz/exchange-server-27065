using System;
using System.Runtime.Serialization;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000073 RID: 115
	[DataContract(Name = "LiveIdBasicAuth", Namespace = "http://microsoft.com/exoanalytics")]
	public class LiveIdBasicAuthRawData : InsightRawData
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00020299 File Offset: 0x0001E499
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x000202A1 File Offset: 0x0001E4A1
		[DataMember(Name = "RangeStart")]
		public DateTime RangeStart { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000202AA File Offset: 0x0001E4AA
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x000202B2 File Offset: 0x0001E4B2
		[DataMember(Name = "RangeEnd")]
		public DateTime RangeEnd { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000202BB File Offset: 0x0001E4BB
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x000202C3 File Offset: 0x0001E4C3
		[DataMember(Name = "ResultType")]
		public string ResultType { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000202CC File Offset: 0x0001E4CC
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x000202D4 File Offset: 0x0001E4D4
		[DataMember(Name = "ApplicationName")]
		public string ApplicationName { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000202DD File Offset: 0x0001E4DD
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000202E5 File Offset: 0x0001E4E5
		[DataMember(Name = "ServerName")]
		public string ServerName { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000202EE File Offset: 0x0001E4EE
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000202F6 File Offset: 0x0001E4F6
		[DataMember(Name = "CountDuringInterval")]
		public int CountDuringInterval
		{
			get
			{
				return this.countDuringInterval;
			}
			set
			{
				this.countDuringInterval = value;
			}
		}

		// Token: 0x0400043B RID: 1083
		private int countDuringInterval = 1;
	}
}
