using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C68 RID: 3176
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NotificationSettingsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FBC RID: 12220
		[DataMember(IsRequired = true, Order = 0)]
		public NotificationSettingsRequest Body;
	}
}
