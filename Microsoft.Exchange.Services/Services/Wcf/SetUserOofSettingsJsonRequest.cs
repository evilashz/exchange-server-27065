using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE7 RID: 3047
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserOofSettingsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F3E RID: 12094
		[DataMember(IsRequired = true, Order = 0)]
		public SetUserOofSettingsRequest Body;
	}
}
