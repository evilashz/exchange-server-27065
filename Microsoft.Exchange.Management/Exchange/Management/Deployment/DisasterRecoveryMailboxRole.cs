using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001BD RID: 445
	[Cmdlet("DisasterRecovery", "MailboxRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class DisasterRecoveryMailboxRole : ManageMailboxRole
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000443D3 File Offset: 0x000425D3
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryMailboxRoleDescription;
			}
		}
	}
}
