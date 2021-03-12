using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000368 RID: 872
	[ServiceContract(Namespace = "ECP", Name = "UMMailboxPicker")]
	public interface IUMMailboxPicker : IGetListService<UMMailboxPickerFilter, UMMailboxPickerObject>
	{
	}
}
