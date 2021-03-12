using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003CE RID: 974
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PolicyTipCustomizedStrings
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000773C0 File Offset: 0x000755C0
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x000773C8 File Offset: 0x000755C8
		[DataMember]
		public string ComplianceURL { get; set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000773D1 File Offset: 0x000755D1
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x000773D9 File Offset: 0x000755D9
		[DataMember]
		public string PolicyTipMessageNotifyString { get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x000773E2 File Offset: 0x000755E2
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x000773EA File Offset: 0x000755EA
		[DataMember]
		public string PolicyTipMessageOverrideString { get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000773F3 File Offset: 0x000755F3
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x000773FB File Offset: 0x000755FB
		[DataMember]
		public string PolicyTipMessageBlockString { get; set; }
	}
}
