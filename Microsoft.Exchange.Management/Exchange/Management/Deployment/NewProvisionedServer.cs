using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000212 RID: 530
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("New", "ProvisionedServer", SupportsShouldProcess = true)]
	public sealed class NewProvisionedServer : ManageProvisionedServer
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x000502A8 File Offset: 0x0004E4A8
		public NewProvisionedServer()
		{
			base.Fields["InstallationMode"] = InstallationModes.Install;
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x000502C6 File Offset: 0x0004E4C6
		protected override LocalizedString Description
		{
			get
			{
				return Strings.ProvisionServerDescription;
			}
		}
	}
}
