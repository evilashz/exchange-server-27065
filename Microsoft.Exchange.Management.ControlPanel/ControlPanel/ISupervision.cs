using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200048F RID: 1167
	[ServiceContract(Namespace = "ECP", Name = "Supervision")]
	public interface ISupervision : IEditObjectService<SupervisionStatus, SetSupervisionStatus>, IGetObjectService<SupervisionStatus>
	{
	}
}
