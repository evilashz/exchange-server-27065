using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000065 RID: 101
	[ServiceContract]
	public interface ILegacyAutodiscover
	{
		// Token: 0x060002CF RID: 719
		[WebGet]
		[OperationContract(Action = "GET", ReplyAction = "*")]
		Message LegacyGetAction(Message input);

		// Token: 0x060002D0 RID: 720
		[OperationContract(Action = "*", ReplyAction = "*")]
		[WebInvoke]
		Message LegacyAction(Message input);
	}
}
