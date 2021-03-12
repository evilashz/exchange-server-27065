using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C11 RID: 3089
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindPeopleJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F68 RID: 12136
		[DataMember(IsRequired = true, Order = 0)]
		public FindPeopleRequest Body;
	}
}
