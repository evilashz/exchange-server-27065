using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BEA RID: 3050
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSharingMetadataJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F41 RID: 12097
		[DataMember(IsRequired = true, Order = 0)]
		public GetSharingMetadataResponseMessage Body;
	}
}
