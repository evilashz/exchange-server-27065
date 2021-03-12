using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C22 RID: 3106
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F79 RID: 12153
		[DataMember(IsRequired = true, Order = 0)]
		public GetPersonaResponseMessage Body;
	}
}
