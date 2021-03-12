using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF2 RID: 3058
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PerformReminderActionJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F49 RID: 12105
		[DataMember(IsRequired = true, Order = 0)]
		public PerformReminderActionResponse Body;
	}
}
