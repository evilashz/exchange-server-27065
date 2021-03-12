using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000342 RID: 834
	[ServiceContract(Namespace = "ECP", Name = "OwnerPicker")]
	public interface IOwnerPicker : IGetListService<OwnerPickerFilter, RecipientPickerObject>
	{
	}
}
