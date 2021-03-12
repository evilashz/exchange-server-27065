using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B90 RID: 2960
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EE7 RID: 12007
		[DataMember(IsRequired = true, Order = 0)]
		public GetFolderResponse Body;
	}
}
