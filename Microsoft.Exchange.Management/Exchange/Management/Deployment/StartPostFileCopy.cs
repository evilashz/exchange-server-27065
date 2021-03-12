using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000262 RID: 610
	[Cmdlet("Start", "PostFileCopy", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class StartPostFileCopy : ManageSetupBindingTasks
	{
		// Token: 0x060016C2 RID: 5826 RVA: 0x00061220 File Offset: 0x0005F420
		public StartPostFileCopy()
		{
			base.ImplementsResume = false;
			string[] fileSearchPath = new string[]
			{
				ExchangeSetupContext.BinPath,
				Path.Combine(ExchangeSetupContext.BinPath, "FIP-FS\\Bin")
			};
			base.SetFileSearchPath(fileSearchPath);
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00061264 File Offset: 0x0005F464
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartPostFileCopyDescription;
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0006126B File Offset: 0x0005F46B
		protected override void PopulateComponentInfoFileNames()
		{
			base.ComponentInfoFileNames.Add("setup\\data\\AllRolesPostFileCopyComponent.xml");
		}
	}
}
