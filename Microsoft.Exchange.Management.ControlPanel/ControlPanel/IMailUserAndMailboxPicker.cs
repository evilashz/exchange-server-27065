using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000334 RID: 820
	[ServiceContract(Namespace = "ECP", Name = "MailUserAndMailboxPicker")]
	public interface IMailUserAndMailboxPicker : IGetListService<MailUserAndMailboxPickerFilter, RecipientPickerObject>
	{
	}
}
