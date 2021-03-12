using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE8 RID: 3048
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserOofSettingsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F3F RID: 12095
		[DataMember(IsRequired = true, Order = 0)]
		public SetUserOofSettingsResponse Body;
	}
}
