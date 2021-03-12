using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B92 RID: 2962
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EE9 RID: 12009
		[DataMember(IsRequired = true, Order = 0)]
		public CreateFolderResponse Body;
	}
}
