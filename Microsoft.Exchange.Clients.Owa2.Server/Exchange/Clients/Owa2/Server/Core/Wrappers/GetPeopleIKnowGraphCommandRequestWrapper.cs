using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200028B RID: 651
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPeopleIKnowGraphCommandRequestWrapper
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00053C9A File Offset: 0x00051E9A
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00053CA2 File Offset: 0x00051EA2
		[DataMember(Name = "request")]
		public GetPeopleIKnowGraphRequest Request { get; set; }
	}
}
