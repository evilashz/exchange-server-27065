using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C6 RID: 1222
	[ServiceContract(Namespace = "ECP", Name = "UMMailboxResetPin")]
	public interface IUMMailboxResetPin : IEditObjectService<SetUMMailboxPinConfiguration, SetUMMailboxPinParameters>, IGetObjectService<SetUMMailboxPinConfiguration>
	{
	}
}
