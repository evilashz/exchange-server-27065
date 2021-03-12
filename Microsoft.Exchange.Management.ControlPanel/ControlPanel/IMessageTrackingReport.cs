using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C6 RID: 710
	[ServiceContract(Namespace = "ECP", Name = "MessageTrackingReport")]
	public interface IMessageTrackingReport : IGetListService<RecipientTrackingEventsFilter, RecipientStatusRow>, IGetObjectService<MessageTrackingReportRow>
	{
	}
}
