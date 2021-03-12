using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A87 RID: 2695
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MyAccountMailboxData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06004C46 RID: 19526 RVA: 0x001062BF File Offset: 0x001044BF
		// (set) Token: 0x06004C47 RID: 19527 RVA: 0x001062C7 File Offset: 0x001044C7
		[DataMember]
		public UnlimitedUnsignedInteger IssueWarningQuota { get; set; }

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x001062D0 File Offset: 0x001044D0
		// (set) Token: 0x06004C49 RID: 19529 RVA: 0x001062D8 File Offset: 0x001044D8
		[DataMember]
		public UnlimitedUnsignedInteger ProhibitSendQuota { get; set; }

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06004C4A RID: 19530 RVA: 0x001062E1 File Offset: 0x001044E1
		// (set) Token: 0x06004C4B RID: 19531 RVA: 0x001062E9 File Offset: 0x001044E9
		[DataMember]
		public UnlimitedUnsignedInteger ProhibitSendReceiveQuota { get; set; }

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06004C4C RID: 19532 RVA: 0x001062F2 File Offset: 0x001044F2
		// (set) Token: 0x06004C4D RID: 19533 RVA: 0x001062FA File Offset: 0x001044FA
		[DataMember]
		public bool UseDatabaseQuotaDefaults { get; set; }
	}
}
