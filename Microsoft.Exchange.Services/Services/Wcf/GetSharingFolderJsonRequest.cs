using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BED RID: 3053
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSharingFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F44 RID: 12100
		[DataMember(IsRequired = true, Order = 0)]
		public GetSharingFolderRequest Body;
	}
}
