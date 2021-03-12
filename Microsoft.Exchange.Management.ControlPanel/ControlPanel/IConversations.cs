using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200008D RID: 141
	[ServiceContract(Namespace = "ECP", Name = "Conversations")]
	public interface IConversations : IMessagingBase<ConversationsConfiguration, SetConversationsConfiguration>, IEditObjectService<ConversationsConfiguration, SetConversationsConfiguration>, IGetObjectService<ConversationsConfiguration>
	{
	}
}
