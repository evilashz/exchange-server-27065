using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA4 RID: 3236
	[Cmdlet("Set", "MailboxRestoreRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxRestoreRequest : SetRequest<MailboxRestoreRequestIdParameter>
	{
		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x06007C44 RID: 31812 RVA: 0x001FD28D File Offset: 0x001FB48D
		// (set) Token: 0x06007C45 RID: 31813 RVA: 0x001FD295 File Offset: 0x001FB495
		[Parameter(Mandatory = false)]
		public new Fqdn RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x17002688 RID: 9864
		// (get) Token: 0x06007C46 RID: 31814 RVA: 0x001FD29E File Offset: 0x001FB49E
		// (set) Token: 0x06007C47 RID: 31815 RVA: 0x001FD2A6 File Offset: 0x001FB4A6
		[Parameter(Mandatory = false)]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}
	}
}
