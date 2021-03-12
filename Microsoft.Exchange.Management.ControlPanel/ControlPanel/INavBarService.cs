using System;
using System.ServiceModel;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000244 RID: 580
	[ServiceContract(Namespace = "ECP", Name = "NavBar")]
	public interface INavBarService
	{
		// Token: 0x06002851 RID: 10321
		[OperationContract]
		PowerShellResults<NavBarPack> GetObject(Identity identity);
	}
}
