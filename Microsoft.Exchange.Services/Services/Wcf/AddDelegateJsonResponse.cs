using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BCE RID: 3022
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddDelegateJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F25 RID: 12069
		[DataMember(IsRequired = true, Order = 0)]
		public AddDelegateResponseMessage Body;
	}
}
