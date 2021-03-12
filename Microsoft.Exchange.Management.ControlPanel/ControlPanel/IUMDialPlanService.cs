using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004BB RID: 1211
	[ServiceContract(Namespace = "ECP", Name = "UMDialPlan")]
	public interface IUMDialPlanService : IUploadHandler
	{
	}
}
