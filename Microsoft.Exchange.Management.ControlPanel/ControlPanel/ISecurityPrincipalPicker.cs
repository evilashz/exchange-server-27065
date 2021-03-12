using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000356 RID: 854
	[ServiceContract(Namespace = "ECP", Name = "SecurityPrincipalPicker")]
	public interface ISecurityPrincipalPicker : IGetListService<SecurityPrincipalPickerFilter, SecurityPrincipalPickerObject>
	{
	}
}
