using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200027A RID: 634
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "FfoWebServiceRole", SupportsShouldProcess = true)]
	public sealed class UninstallFfoWebServiceRoleRole : ManageFfoWebServiceRole
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x000636CC File Offset: 0x000618CC
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallFfoWebServiceRoleDescription;
			}
		}
	}
}
