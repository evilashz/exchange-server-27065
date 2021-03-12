using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200034A RID: 842
	[ServiceContract(Namespace = "ECP", Name = "RecipientPicker")]
	public interface IRecipientPicker : IGetListService<RecipientPickerFilter, RecipientPickerObject>
	{
	}
}
