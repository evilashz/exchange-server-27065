using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B9A RID: 2970
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MoveFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EF1 RID: 12017
		[DataMember(IsRequired = true, Order = 0)]
		public MoveFolderResponse Body;
	}
}
