using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB9 RID: 3001
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ResolveNamesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F10 RID: 12048
		[DataMember(IsRequired = true, Order = 0)]
		public ResolveNamesRequest Body;
	}
}
