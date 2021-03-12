using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C21 RID: 3105
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F78 RID: 12152
		[DataMember(IsRequired = true, Order = 0)]
		public GetPersonaRequest Body;
	}
}
