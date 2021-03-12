using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000296 RID: 662
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessageSignInRequestWrapper
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x00053DE0 File Offset: 0x00051FE0
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x00053DE8 File Offset: 0x00051FE8
		[DataMember(Name = "signedInManually")]
		public bool SignedInManually { get; set; }
	}
}
