using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B8F RID: 2959
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EE6 RID: 12006
		[DataMember(IsRequired = true, Order = 0)]
		public GetFolderRequest Body;
	}
}
