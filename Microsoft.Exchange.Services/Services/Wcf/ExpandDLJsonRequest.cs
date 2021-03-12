using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BBB RID: 3003
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExpandDLJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F12 RID: 12050
		[DataMember(IsRequired = true, Order = 0)]
		public ExpandDLRequest Body;
	}
}
