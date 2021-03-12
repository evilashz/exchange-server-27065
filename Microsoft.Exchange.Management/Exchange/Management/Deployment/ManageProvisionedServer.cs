using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000202 RID: 514
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ManageProvisionedServer : ComponentInfoBasedTask
	{
		// Token: 0x0600117E RID: 4478 RVA: 0x0004CF74 File Offset: 0x0004B174
		public ManageProvisionedServer()
		{
			base.ImplementsResume = false;
			base.Fields["InstallationMode"] = InstallationModes.Unknown;
			base.Fields["IsProvisionServer"] = true;
			base.Fields["ServerName"] = base.GetFqdnOrNetbiosName();
			base.ComponentInfoFileNames = new List<string>();
			base.ComponentInfoFileNames.Add("setup\\data\\ProvisionServerComponent.xml");
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0004CFEB File Offset: 0x0004B1EB
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x0004D002 File Offset: 0x0004B202
		[Parameter(Mandatory = false)]
		public string ServerName
		{
			get
			{
				return (string)base.Fields["ServerName"];
			}
			set
			{
				base.Fields["ServerName"] = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0004D015 File Offset: 0x0004B215
		protected override LocalizedString Description
		{
			get
			{
				return LocalizedString.Empty;
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0004D01C File Offset: 0x0004B21C
		protected override void PopulateContextVariables()
		{
			base.Fields["NetBIOSName"] = base.GetNetBIOSName(this.ServerName);
			base.PopulateContextVariables();
		}
	}
}
