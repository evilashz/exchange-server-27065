using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000225 RID: 549
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Remove", "ProvisionedServer", SupportsShouldProcess = true)]
	public sealed class RemoveProvisionedServer : ManageProvisionedServer
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0005228C File Offset: 0x0005048C
		public RemoveProvisionedServer()
		{
			base.Fields["InstallationMode"] = InstallationModes.Uninstall;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000522AA File Offset: 0x000504AA
		protected override LocalizedString Description
		{
			get
			{
				return Strings.RemoveProvisionedServerDescription;
			}
		}
	}
}
