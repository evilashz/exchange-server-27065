using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C63 RID: 3171
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdatePersonaJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FB7 RID: 12215
		[DataMember(IsRequired = true, Order = 0)]
		public UpdatePersonaRequest Body;
	}
}
