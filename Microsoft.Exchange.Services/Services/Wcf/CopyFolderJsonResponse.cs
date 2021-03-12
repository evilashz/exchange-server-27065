using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B9C RID: 2972
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CopyFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EF3 RID: 12019
		[DataMember(IsRequired = true, Order = 0)]
		public CopyFolderResponse Body;
	}
}
