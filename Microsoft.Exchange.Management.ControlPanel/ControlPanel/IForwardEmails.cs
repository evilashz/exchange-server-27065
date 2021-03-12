using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A5 RID: 677
	[ServiceContract(Namespace = "ECP", Name = "ForwardEmails")]
	public interface IForwardEmails : IEditObjectService<ForwardEmailMailbox, SetForwardEmailMailbox>, IGetObjectService<ForwardEmailMailbox>
	{
		// Token: 0x06002B96 RID: 11158
		[OperationContract]
		PowerShellResults<ForwardEmailMailbox> StopForward(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
