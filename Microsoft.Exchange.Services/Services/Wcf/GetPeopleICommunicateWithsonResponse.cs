using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C4E RID: 3150
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPeopleICommunicateWithsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FA5 RID: 12197
		[DataMember(IsRequired = true, Order = 0)]
		public GetPeopleICommunicateWithResponse Body;
	}
}
