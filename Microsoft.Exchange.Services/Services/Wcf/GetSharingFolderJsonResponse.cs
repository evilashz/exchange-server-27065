using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BEE RID: 3054
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSharingFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F45 RID: 12101
		[DataMember(IsRequired = true, Order = 0)]
		public GetSharingFolderResponseMessage Body;
	}
}
