using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B96 RID: 2966
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EED RID: 12013
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteFolderResponse Body;
	}
}
