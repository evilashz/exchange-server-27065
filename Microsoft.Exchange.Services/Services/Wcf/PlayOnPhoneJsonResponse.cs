using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE0 RID: 3040
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PlayOnPhoneJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F37 RID: 12087
		[DataMember(IsRequired = true, Order = 0)]
		public PlayOnPhoneResponseMessage Body;
	}
}
