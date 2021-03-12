using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000494 RID: 1172
	[ServiceContract(Namespace = "ECP", Name = "SupervisionListEntry")]
	public interface ISupervisionListEntryService : IGetListService<SupervisionListEntryFilter, SupervisionListEntryRow>
	{
	}
}
