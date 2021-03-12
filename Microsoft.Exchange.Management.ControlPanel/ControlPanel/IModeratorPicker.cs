using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200033E RID: 830
	[ServiceContract(Namespace = "ECP", Name = "ModeratorPicker")]
	public interface IModeratorPicker : IGetListService<ModeratorPickerFilter, RecipientPickerObject>
	{
	}
}
