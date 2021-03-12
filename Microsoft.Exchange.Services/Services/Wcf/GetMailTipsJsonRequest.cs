using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BDD RID: 3037
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailTipsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F34 RID: 12084
		[DataMember(IsRequired = true, Order = 0)]
		public GetMailTipsRequest Body;
	}
}
