using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002BD RID: 701
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetOwaUserEmailSignatureRequestWrapper
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005428C File Offset: 0x0005248C
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x00054294 File Offset: 0x00052494
		[DataMember(Name = "userEmailSignature")]
		public EmailSignatureConfiguration UserEmailSignature { get; set; }
	}
}
