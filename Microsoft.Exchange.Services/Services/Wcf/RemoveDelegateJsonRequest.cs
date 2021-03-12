using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD1 RID: 3025
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveDelegateJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F28 RID: 12072
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveDelegateRequest Body;
	}
}
