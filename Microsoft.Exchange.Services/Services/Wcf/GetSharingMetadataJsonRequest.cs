using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE9 RID: 3049
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSharingMetadataJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F40 RID: 12096
		[DataMember(IsRequired = true, Order = 0)]
		public GetSharingMetadataRequest Body;
	}
}
