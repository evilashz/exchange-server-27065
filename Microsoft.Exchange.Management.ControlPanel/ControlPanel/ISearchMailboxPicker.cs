using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000350 RID: 848
	[ServiceContract(Namespace = "ECP", Name = "SearchMailboxPicker")]
	public interface ISearchMailboxPicker : IGetListService<SearchMailboxPickerFilter, RecipientPickerObject>
	{
	}
}
