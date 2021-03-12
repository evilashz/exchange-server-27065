using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C0 RID: 192
	[ServiceContract(Namespace = "ECP", Name = "MailboxCalendarFolder")]
	public interface IMailboxCalendarFolder : IEditObjectService<MailboxCalendarFolderRow, SetMailboxCalendarFolder>, IGetObjectService<MailboxCalendarFolderRow>
	{
		// Token: 0x06001CD2 RID: 7378
		[OperationContract]
		PowerShellResults<MailboxCalendarFolderRow> StartPublishing(Identity identity, SetMailboxCalendarFolder properties);

		// Token: 0x06001CD3 RID: 7379
		[OperationContract]
		PowerShellResults<MailboxCalendarFolderRow> StopPublishing(Identity identity, SetMailboxCalendarFolder properties);
	}
}
