using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200006F RID: 111
	[ServiceContract(Namespace = "ECP", Name = "CalendarDiagnosticLog")]
	public interface ICalendarDiagnosticLogService
	{
		// Token: 0x06001B05 RID: 6917
		[OperationContract]
		PowerShellResults SendLog(CalendarDiagnosticLog properties);
	}
}
