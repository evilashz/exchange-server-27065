using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002D2 RID: 722
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ValidateModernGroupAliasRequestWrapper
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000544EE File Offset: 0x000526EE
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x000544F6 File Offset: 0x000526F6
		[DataMember(Name = "request")]
		public ValidateModernGroupAliasRequest Request { get; set; }
	}
}
