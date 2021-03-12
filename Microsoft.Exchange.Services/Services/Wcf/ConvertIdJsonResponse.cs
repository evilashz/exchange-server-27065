using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B8A RID: 2954
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConvertIdJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EE1 RID: 12001
		[DataMember(IsRequired = true, Order = 0)]
		public ConvertIdResponse Body;
	}
}
