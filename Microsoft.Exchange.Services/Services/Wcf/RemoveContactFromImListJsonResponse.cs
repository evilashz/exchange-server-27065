using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C38 RID: 3128
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveContactFromImListJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F8F RID: 12175
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveContactFromImListResponseMessage Body;
	}
}
