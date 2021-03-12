using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D70 RID: 3440
	[MessageContract(IsWrapped = false)]
	public class RemoveImGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003102 RID: 12546
		[MessageBodyMember(Name = "RemoveImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveImGroupRequest Body;
	}
}
