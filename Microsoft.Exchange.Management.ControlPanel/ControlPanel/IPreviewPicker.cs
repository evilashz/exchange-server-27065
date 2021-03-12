using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D0 RID: 464
	[ServiceContract(Namespace = "ECP", Name = "PreviewPicker")]
	public interface IPreviewPicker : IGetListService<PreviewPickerFilter, RecipientPickerObject>
	{
	}
}
