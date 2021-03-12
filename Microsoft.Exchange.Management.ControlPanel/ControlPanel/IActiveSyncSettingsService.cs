using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000300 RID: 768
	[ServiceContract(Namespace = "ECP", Name = "ActiveSyncSettings")]
	public interface IActiveSyncSettingsService : IEditObjectService<ActiveSyncSettings, SetActiveSyncSettings>, IGetObjectService<ActiveSyncSettings>
	{
	}
}
