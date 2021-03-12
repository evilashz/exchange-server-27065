using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C4C RID: 3148
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserPhotoJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FA3 RID: 12195
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserPhotoResponse Body;
	}
}
