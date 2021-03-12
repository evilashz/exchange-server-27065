using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000328 RID: 808
	[ServiceContract(Namespace = "ECP", Name = "DiscoveryMailboxPicker")]
	public interface IDiscoveryMailboxPicker : IGetListService<DiscoveryMailboxPickerFilter, RecipientPickerObject>
	{
	}
}
