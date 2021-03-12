using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD0 RID: 3024
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateDelegateJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F27 RID: 12071
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateDelegateResponseMessage Body;
	}
}
