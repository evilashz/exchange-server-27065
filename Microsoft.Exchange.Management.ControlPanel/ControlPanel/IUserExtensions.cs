using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000470 RID: 1136
	[ServiceContract(Namespace = "ECP", Name = "UserExtensions")]
	public interface IUserExtensions : IGetListService<UserExtensionsFilter, UMMailboxExtension>
	{
	}
}
