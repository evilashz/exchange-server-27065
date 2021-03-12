using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C12 RID: 3090
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindPeopleJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F69 RID: 12137
		[DataMember(IsRequired = true, Order = 0)]
		public FindPeopleResponseMessage Body;
	}
}
