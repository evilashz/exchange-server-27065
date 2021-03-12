using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA6 RID: 2982
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EFD RID: 12029
		[DataMember(IsRequired = true, Order = 0)]
		public CreateItemResponse Body;
	}
}
