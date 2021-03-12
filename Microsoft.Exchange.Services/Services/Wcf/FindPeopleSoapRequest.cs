using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D24 RID: 3364
	[MessageContract(IsWrapped = false)]
	public class FindPeopleSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030B6 RID: 12470
		[MessageBodyMember(Name = "FindPeople", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindPeopleRequest Body;
	}
}
