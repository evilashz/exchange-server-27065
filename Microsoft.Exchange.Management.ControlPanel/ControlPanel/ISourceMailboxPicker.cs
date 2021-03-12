using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200035D RID: 861
	[ServiceContract(Namespace = "ECP", Name = "SourceMailboxPicker")]
	public interface ISourceMailboxPicker : IGetListService<SourceMailboxPickerFilter, RecipientPickerObject>
	{
	}
}
