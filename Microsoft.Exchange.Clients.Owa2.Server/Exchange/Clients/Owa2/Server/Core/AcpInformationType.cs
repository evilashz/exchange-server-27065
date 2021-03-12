using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A0 RID: 928
	[KnownType(typeof(JsonFaultResponse))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AcpInformationType
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x00076172 File Offset: 0x00074372
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x0007617A File Offset: 0x0007437A
		[DataMember]
		public string ParticipantPassCode { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00076183 File Offset: 0x00074383
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0007618B File Offset: 0x0007438B
		[DataMember(IsRequired = false)]
		public string[] TollFreeNumbers { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00076194 File Offset: 0x00074394
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x0007619C File Offset: 0x0007439C
		[DataMember(IsRequired = false)]
		public string TollNumber { get; set; }
	}
}
