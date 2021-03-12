using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000542 RID: 1346
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RequestDeviceRegistrationChallengeResponseMessage : ResponseMessage
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x000A63C2 File Offset: 0x000A45C2
		public RequestDeviceRegistrationChallengeResponseMessage()
		{
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000A63CA File Offset: 0x000A45CA
		internal RequestDeviceRegistrationChallengeResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000A63D4 File Offset: 0x000A45D4
		public override ResponseType GetResponseType()
		{
			return ResponseType.RequestDeviceRegistrationChallengeResponseMessage;
		}
	}
}
