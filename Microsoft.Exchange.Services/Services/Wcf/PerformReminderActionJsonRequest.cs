using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF1 RID: 3057
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PerformReminderActionJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F48 RID: 12104
		[DataMember(IsRequired = true, Order = 0)]
		public PerformReminderActionRequest Body;
	}
}
