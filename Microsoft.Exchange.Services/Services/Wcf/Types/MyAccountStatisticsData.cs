using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A88 RID: 2696
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MyAccountStatisticsData : OptionsPropertyChangeTracker
	{
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06004C4F RID: 19535 RVA: 0x0010630B File Offset: 0x0010450B
		// (set) Token: 0x06004C50 RID: 19536 RVA: 0x00106313 File Offset: 0x00104513
		[DataMember]
		public UnlimitedUnsignedInteger DatabaseIssueWarningQuota { get; set; }

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06004C51 RID: 19537 RVA: 0x0010631C File Offset: 0x0010451C
		// (set) Token: 0x06004C52 RID: 19538 RVA: 0x00106324 File Offset: 0x00104524
		[DataMember]
		public UnlimitedUnsignedInteger DatabaseProhibitSendQuota { get; set; }

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06004C53 RID: 19539 RVA: 0x0010632D File Offset: 0x0010452D
		// (set) Token: 0x06004C54 RID: 19540 RVA: 0x00106335 File Offset: 0x00104535
		[DataMember]
		public UnlimitedUnsignedInteger DatabaseProhibitSendReceiveQuota { get; set; }

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x0010633E File Offset: 0x0010453E
		// (set) Token: 0x06004C56 RID: 19542 RVA: 0x00106346 File Offset: 0x00104546
		[DataMember]
		public NullableStorageLimitStatus StorageLimitStatus { get; set; }

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x0010634F File Offset: 0x0010454F
		// (set) Token: 0x06004C58 RID: 19544 RVA: 0x00106357 File Offset: 0x00104557
		[DataMember]
		public UnlimitedUnsignedInteger TotalItemSize { get; set; }
	}
}
