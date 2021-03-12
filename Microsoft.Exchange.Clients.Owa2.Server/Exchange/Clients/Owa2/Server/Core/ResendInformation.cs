using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F9 RID: 1017
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(JsonFaultResponse))]
	public class ResendInformation
	{
		// Token: 0x06002101 RID: 8449 RVA: 0x00079426 File Offset: 0x00077626
		public ResendInformation()
		{
			this.ResendDraftId = string.Empty;
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x00079439 File Offset: 0x00077639
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x00079441 File Offset: 0x00077641
		[DataMember]
		public string ResendDraftId { get; set; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0007944A File Offset: 0x0007764A
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x00079452 File Offset: 0x00077652
		[DataMember]
		public string[] HiddenRecipientsInTo { get; set; }
	}
}
