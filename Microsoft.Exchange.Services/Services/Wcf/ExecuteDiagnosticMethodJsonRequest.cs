using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C0B RID: 3083
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExecuteDiagnosticMethodJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F62 RID: 12130
		[DataMember(IsRequired = true, Order = 0)]
		public ExecuteDiagnosticMethodRequest Body;
	}
}
