using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BBA RID: 3002
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ResolveNamesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F11 RID: 12049
		[DataMember(IsRequired = true, Order = 0)]
		public ResolveNamesResponse Body;
	}
}
