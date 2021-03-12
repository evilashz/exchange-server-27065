using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA9 RID: 2985
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F00 RID: 12032
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateItemRequest Body;
	}
}
