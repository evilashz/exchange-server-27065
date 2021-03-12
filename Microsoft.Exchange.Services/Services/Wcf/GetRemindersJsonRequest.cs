using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BEF RID: 3055
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRemindersJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F46 RID: 12102
		[DataMember(IsRequired = true, Order = 0)]
		public GetRemindersRequest Body;
	}
}
