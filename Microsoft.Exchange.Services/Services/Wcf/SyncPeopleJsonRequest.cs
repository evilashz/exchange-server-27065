using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C13 RID: 3091
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncPeopleJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F6A RID: 12138
		[DataMember(IsRequired = true, Order = 0)]
		public SyncPeopleRequest Body;
	}
}
