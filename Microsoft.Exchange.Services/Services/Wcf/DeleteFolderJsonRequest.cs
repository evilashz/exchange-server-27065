using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B95 RID: 2965
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EEC RID: 12012
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteFolderRequest Body;
	}
}
