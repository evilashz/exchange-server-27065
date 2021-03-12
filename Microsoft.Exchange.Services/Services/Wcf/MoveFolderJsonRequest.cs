using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B99 RID: 2969
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MoveFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EF0 RID: 12016
		[DataMember(IsRequired = true, Order = 0)]
		public MoveFolderRequest Body;
	}
}
