using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000297 RID: 663
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessageSignOutRequestWrapper
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x00053DF9 File Offset: 0x00051FF9
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x00053E01 File Offset: 0x00052001
		[DataMember(Name = "reserved")]
		public bool Reserved { get; set; }
	}
}
