using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B89 RID: 2953
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConvertIdJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EE0 RID: 12000
		[DataMember(IsRequired = true, Order = 0)]
		public ConvertIdRequest Body;
	}
}
