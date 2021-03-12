using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002CE RID: 718
	[ServiceContract(Namespace = "ECP", Name = "MessageTrackingSearch")]
	public interface IMessageTrackingSearch : IGetListService<MessageTrackingSearchFilter, MessageTrackingSearchResultRow>
	{
	}
}
