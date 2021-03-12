using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C4B RID: 3147
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserPhotoJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FA2 RID: 12194
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserPhotoRequest Body;
	}
}
