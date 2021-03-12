using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C14 RID: 3092
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncPeopleJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F6B RID: 12139
		[DataMember(IsRequired = true, Order = 0)]
		public SyncPeopleResponseMessage Body;
	}
}
