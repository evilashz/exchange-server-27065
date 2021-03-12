using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200032C RID: 812
	[ServiceContract(Namespace = "ECP", Name = "ExtendedMailboxPicker")]
	public interface IExtendedMailboxPicker : IGetListService<ExtendedMailboxPickerFilter, RecipientPickerObject>
	{
	}
}
