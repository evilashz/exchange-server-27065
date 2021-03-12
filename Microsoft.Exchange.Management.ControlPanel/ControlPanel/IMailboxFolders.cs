using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200060E RID: 1550
	[ServiceContract(Namespace = "ECP", Name = "MailboxFolders")]
	public interface IMailboxFolders : IGetListService<MailboxFolderFilter, MailboxFolder>, INewObjectService<MailboxFolder, NewMailboxFolder>
	{
	}
}
