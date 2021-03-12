using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE6 RID: 3046
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserOofSettingsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F3D RID: 12093
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserOofSettingsResponse Body;
	}
}
