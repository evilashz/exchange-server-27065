using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C37 RID: 3127
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveContactFromImListJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F8E RID: 12174
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveContactFromImListRequest Body;
	}
}
