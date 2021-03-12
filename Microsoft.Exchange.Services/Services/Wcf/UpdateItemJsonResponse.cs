using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BAA RID: 2986
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F01 RID: 12033
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateItemResponse Body;
	}
}
