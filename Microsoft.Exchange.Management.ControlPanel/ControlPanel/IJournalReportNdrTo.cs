using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200040C RID: 1036
	[ServiceContract(Namespace = "ECP", Name = "JournalReportNdrTo")]
	public interface IJournalReportNdrTo : IEditObjectService<JournalReportNdrTo, SetJournalReportNdrTo>, IGetObjectService<JournalReportNdrTo>
	{
	}
}
