using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200027D RID: 637
	[Cmdlet("Uninstall", "MailboxRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class UninstallMailboxRole : ManageMailboxRole
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x000636F9 File Offset: 0x000618F9
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallMailboxRoleDescription;
			}
		}
	}
}
