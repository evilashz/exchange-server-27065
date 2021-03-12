using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000266 RID: 614
	[Cmdlet("Start", "PreSetup", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class StartPreSetup : ManageSetupBindingTasks
	{
		// Token: 0x060016DA RID: 5850 RVA: 0x00061648 File Offset: 0x0005F848
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
			base.Fields["MsiInstallPath"] = text;
			base.Fields["MsiInstallPathBin"] = ((text == null) ? null : Path.Combine(text, "Bin"));
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000616A3 File Offset: 0x0005F8A3
		protected override void PopulateComponentInfoFileNames()
		{
			base.PopulateComponentInfoFileNames();
			base.ComponentInfoFileNames.Add("setup\\data\\AllRolesPreSetupLastComponent.xml");
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000616BB File Offset: 0x0005F8BB
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartPreSetupDescription;
			}
		}
	}
}
