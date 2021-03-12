using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C25 RID: 3109
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MarkAsJunkJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F7C RID: 12156
		[DataMember(IsRequired = true, Order = 0)]
		public MarkAsJunkRequest Body;
	}
}
