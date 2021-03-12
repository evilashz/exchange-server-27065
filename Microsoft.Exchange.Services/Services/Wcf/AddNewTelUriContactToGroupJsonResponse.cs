using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C32 RID: 3122
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddNewTelUriContactToGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F89 RID: 12169
		[DataMember(IsRequired = true, Order = 0)]
		public AddNewTelUriContactToGroupResponseMessage Body;
	}
}
