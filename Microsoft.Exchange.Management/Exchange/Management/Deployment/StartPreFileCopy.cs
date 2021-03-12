using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000265 RID: 613
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Start", "PreFileCopy", SupportsShouldProcess = true)]
	public sealed class StartPreFileCopy : ManageSetupBindingTasks
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00061624 File Offset: 0x0005F824
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartPreFileCopyDescription;
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0006162B File Offset: 0x0005F82B
		protected override void PopulateComponentInfoFileNames()
		{
			base.ComponentInfoFileNames.Add("setup\\data\\AllRolesPreFileCopyComponent.xml");
		}
	}
}
