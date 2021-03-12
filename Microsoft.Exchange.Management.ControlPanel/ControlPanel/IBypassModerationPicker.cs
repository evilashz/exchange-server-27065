using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200031C RID: 796
	[ServiceContract(Namespace = "ECP", Name = "BypassModerationPicker")]
	public interface IBypassModerationPicker : IGetListService<PersonOrGroupPickerFilter, RecipientPickerObject>
	{
	}
}
