using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C26 RID: 3110
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MarkAsJunkJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F7D RID: 12157
		[DataMember(IsRequired = true, Order = 0)]
		public MarkAsJunkResponse Body;
	}
}
