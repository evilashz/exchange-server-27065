using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA2 RID: 2978
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EF9 RID: 12025
		[DataMember(IsRequired = true, Order = 0)]
		public FindFolderResponse Body;
	}
}
