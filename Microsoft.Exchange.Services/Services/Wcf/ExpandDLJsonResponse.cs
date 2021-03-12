using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BBC RID: 3004
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExpandDLJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F13 RID: 12051
		[DataMember(IsRequired = true, Order = 0)]
		public ExpandDLResponse Body;
	}
}
