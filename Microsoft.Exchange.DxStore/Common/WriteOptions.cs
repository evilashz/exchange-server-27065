using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000049 RID: 73
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class WriteOptions
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000045F3 File Offset: 0x000027F3
		// (set) Token: 0x06000266 RID: 614 RVA: 0x000045FB File Offset: 0x000027FB
		[DataMember]
		public bool IsPerformTestUpdate { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00004604 File Offset: 0x00002804
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000460C File Offset: 0x0000280C
		[DataMember]
		public double PercentageOfNodesToSucceed { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00004615 File Offset: 0x00002815
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000461D File Offset: 0x0000281D
		[DataMember]
		public string[] WaitForNodes { get; set; }

		// Token: 0x0600026B RID: 619 RVA: 0x00004626 File Offset: 0x00002826
		public bool IsWaitRequired()
		{
			return this.PercentageOfNodesToSucceed > 0.0 || (this.WaitForNodes != null && this.WaitForNodes.Length > 0);
		}
	}
}
