using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C4D RID: 3149
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPeopleICommunicateWithJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FA4 RID: 12196
		[DataMember(IsRequired = true, Order = 0)]
		public GetPeopleICommunicateWithRequest Body;
	}
}
