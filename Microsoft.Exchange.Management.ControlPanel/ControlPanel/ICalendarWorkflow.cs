using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000079 RID: 121
	[ServiceContract(Namespace = "ECP", Name = "CalendarWorkflow")]
	public interface ICalendarWorkflow : IEditObjectService<CalendarWorkflowConfiguration, SetCalendarWorkflowConfiguration>, IGetObjectService<CalendarWorkflowConfiguration>
	{
		// Token: 0x06001B3C RID: 6972
		[OperationContract]
		PowerShellResults<CalendarWorkflowConfiguration> UpdateObject(Identity identity);
	}
}
