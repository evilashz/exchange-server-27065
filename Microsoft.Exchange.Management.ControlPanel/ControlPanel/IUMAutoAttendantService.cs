using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A9 RID: 1193
	[ServiceContract(Namespace = "ECP", Name = "UMAutoAttendant")]
	public interface IUMAutoAttendantService : IUploadHandler
	{
	}
}
