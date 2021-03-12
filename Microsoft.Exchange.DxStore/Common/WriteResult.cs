using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200004A RID: 74
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class WriteResult
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00004658 File Offset: 0x00002858
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00004660 File Offset: 0x00002860
		[DataMember]
		public bool IsTestUpdate { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00004669 File Offset: 0x00002869
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00004671 File Offset: 0x00002871
		[DataMember]
		public bool IsConstraintPassed { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000467A File Offset: 0x0000287A
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00004682 File Offset: 0x00002882
		[DataMember]
		public WriteResult.ResponseInfo[] Responses { get; set; }

		// Token: 0x0200004B RID: 75
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class ResponseInfo
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000274 RID: 628 RVA: 0x00004693 File Offset: 0x00002893
			// (set) Token: 0x06000275 RID: 629 RVA: 0x0000469B File Offset: 0x0000289B
			[DataMember]
			public string Name { get; set; }

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000276 RID: 630 RVA: 0x000046A4 File Offset: 0x000028A4
			// (set) Token: 0x06000277 RID: 631 RVA: 0x000046AC File Offset: 0x000028AC
			[DataMember]
			public int LatencyInMs { get; set; }
		}
	}
}
