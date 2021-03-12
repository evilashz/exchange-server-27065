using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C0 RID: 704
	[ServiceContract(Namespace = "ECP", Name = "MailMessages")]
	public interface IMailMessages : INewObjectService<MailMessageRow, NewMailMessage>
	{
	}
}
