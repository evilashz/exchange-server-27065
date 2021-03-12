using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C62 RID: 3170
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreatePersonaJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FB6 RID: 12214
		[DataMember(IsRequired = true, Order = 0)]
		public CreatePersonaRequest Body;
	}
}
