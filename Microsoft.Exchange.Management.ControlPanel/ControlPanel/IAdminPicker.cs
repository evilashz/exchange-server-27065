using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000316 RID: 790
	[ServiceContract(Namespace = "ECP", Name = "AdminPicker")]
	public interface IAdminPicker : IGetListService<AdminPickerFilter, RecipientPickerObject>
	{
	}
}
