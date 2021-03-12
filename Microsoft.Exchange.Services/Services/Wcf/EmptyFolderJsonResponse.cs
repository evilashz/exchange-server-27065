using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B94 RID: 2964
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EmptyFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EEB RID: 12011
		[DataMember(IsRequired = true, Order = 0)]
		public EmptyFolderResponse Body;
	}
}
