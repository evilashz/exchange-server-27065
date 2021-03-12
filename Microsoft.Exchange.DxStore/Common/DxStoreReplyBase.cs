using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000015 RID: 21
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class DxStoreReplyBase
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000025CF File Offset: 0x000007CF
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000025D7 File Offset: 0x000007D7
		[DataMember]
		public int Version { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000025E0 File Offset: 0x000007E0
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000025E8 File Offset: 0x000007E8
		[DataMember]
		public DateTimeOffset TimeReceived { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000025F1 File Offset: 0x000007F1
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000025F9 File Offset: 0x000007F9
		[DataMember]
		public TimeSpan Duration { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002602 File Offset: 0x00000802
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000260A File Offset: 0x0000080A
		[DataMember]
		public string Responder { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002613 File Offset: 0x00000813
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000261B File Offset: 0x0000081B
		[DataMember]
		public ProcessBasicInfo ProcessInfo { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000262C File Offset: 0x0000082C
		[DataMember]
		public int MostRecentUpdateNumber { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002635 File Offset: 0x00000835
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000263D File Offset: 0x0000083D
		[DataMember]
		public string DebugString { get; set; }
	}
}
