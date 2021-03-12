using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE5 RID: 3045
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserOofSettingsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F3C RID: 12092
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserOofSettingsRequest Body;
	}
}
