using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A5 RID: 677
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveSharedFoldersRequestWrapper
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00053FCE File Offset: 0x000521CE
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x00053FD6 File Offset: 0x000521D6
		[DataMember(Name = "primarySMTPAddress")]
		public string PrimarySMTPAddress { get; set; }
	}
}
