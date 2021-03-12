using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B8 RID: 440
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class SubscriptionResponseData
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x0003CA63 File Offset: 0x0003AC63
		public SubscriptionResponseData(string subscrptionId, bool successfullyCreated)
		{
			this.SubscriptionId = subscrptionId;
			this.SuccessfullyCreated = successfullyCreated;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003CA79 File Offset: 0x0003AC79
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0003CA81 File Offset: 0x0003AC81
		[DataMember]
		public string SubscriptionId { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0003CA8A File Offset: 0x0003AC8A
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x0003CA92 File Offset: 0x0003AC92
		[DataMember]
		public bool SuccessfullyCreated { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0003CA9B File Offset: 0x0003AC9B
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x0003CAA3 File Offset: 0x0003ACA3
		[DataMember]
		public string ErrorInfo { get; set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003CAAC File Offset: 0x0003ACAC
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0003CAB4 File Offset: 0x0003ACB4
		[DataMember]
		public bool SubscriptionExists { get; set; }
	}
}
