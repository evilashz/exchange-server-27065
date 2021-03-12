using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200031A RID: 794
	[ServiceContract(Namespace = "ECP", Name = "AllowedSenderPicker")]
	public interface IAllowedSenderPicker : IGetListService<PersonOrGroupPickerFilter, RecipientPickerObject>
	{
	}
}
