using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000014 RID: 20
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class DxStoreRequestBase
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002561 File Offset: 0x00000761
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002569 File Offset: 0x00000769
		[DataMember]
		public int Version { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002572 File Offset: 0x00000772
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000257A File Offset: 0x0000077A
		[DataMember]
		public Guid Id { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002583 File Offset: 0x00000783
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000258B File Offset: 0x0000078B
		[DataMember]
		public DateTimeOffset TimeRequested { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002594 File Offset: 0x00000794
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000259C File Offset: 0x0000079C
		[DataMember]
		public string Requester { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000025A5 File Offset: 0x000007A5
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000025AD File Offset: 0x000007AD
		[DataMember]
		public ProcessBasicInfo ProcessInfo { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000025B6 File Offset: 0x000007B6
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000025BE File Offset: 0x000007BE
		[DataMember]
		public string DebugString { get; set; }
	}
}
