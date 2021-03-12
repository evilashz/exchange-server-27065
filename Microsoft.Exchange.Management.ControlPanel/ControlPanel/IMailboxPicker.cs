using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000331 RID: 817
	[ServiceContract(Namespace = "ECP", Name = "MailboxPicker")]
	public interface IMailboxPicker : IGetListService<MailboxPickerFilter, RecipientPickerObject>
	{
	}
}
