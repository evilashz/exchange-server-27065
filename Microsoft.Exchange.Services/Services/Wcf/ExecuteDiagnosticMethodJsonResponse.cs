using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C0C RID: 3084
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExecuteDiagnosticMethodJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F63 RID: 12131
		[DataMember(IsRequired = true, Order = 0)]
		public ExecuteDiagnosticMethodResponse Body;
	}
}
