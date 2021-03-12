using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D25 RID: 3365
	[MessageContract(IsWrapped = false)]
	public class FindPeopleSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030B7 RID: 12471
		[MessageBodyMember(Name = "FindPeopleResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindPeopleResponseMessage Body;
	}
}
