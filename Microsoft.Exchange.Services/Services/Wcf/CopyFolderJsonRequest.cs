using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B9B RID: 2971
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CopyFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EF2 RID: 12018
		[DataMember(IsRequired = true, Order = 0)]
		public CopyFolderRequest Body;
	}
}
