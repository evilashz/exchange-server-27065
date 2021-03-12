using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF0 RID: 3056
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRemindersJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F47 RID: 12103
		[DataMember(IsRequired = true, Order = 0)]
		public GetRemindersResponse Body;
	}
}
