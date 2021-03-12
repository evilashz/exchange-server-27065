using System;
using Microsoft.Exchange.Management.SnapIn.Esm.Toolbox;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SnapIn.Esm
{
	// Token: 0x02000002 RID: 2
	[SnapInAbout("Microsoft.Exchange.Management.NativeResources.dll", ApplicationBaseRelative = true, VendorId = 101, VersionId = 102, DisplayNameId = 903, DescriptionId = 904, IconId = 910, LargeFolderBitmapId = 911, SmallFolderBitmapId = 912, SmallFolderSelectedBitmapId = 912, FolderBitmapsColorMask = 16711935)]
	[PublishesNodeType("714FA079-DC14-470f-851C-B7EAAA4177C1", Description = "Microsoft Exchange")]
	[SnapInSettings("714FA079-DC14-470f-851C-B7EAAA4177C1", DisplayName = "Microsoft Exchange", Description = "Microsoft Exchange.", UseCustomHelp = true)]
	public class MicrosoftExchangeSnapIn : ExchangeSnapIn
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MicrosoftExchangeSnapIn()
		{
			GC.KeepAlive(ManagementGuiSqmSession.Instance);
			base.RootNode = new ToolboxNode(EnvironmentAnalyzer.IsWorkGroup());
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F2 File Offset: 0x000002F2
		public override string SnapInGuidString
		{
			get
			{
				return "714FA079-DC14-470f-851C-B7EAAA4177C1";
			}
		}

		// Token: 0x04000001 RID: 1
		public const string NodeGuid = "714FA079-DC14-470f-851C-B7EAAA4177C1";

		// Token: 0x04000002 RID: 2
		public const string SnapInDisplayName = "Microsoft Exchange";

		// Token: 0x04000003 RID: 3
		public const string SnapInDescription = "Microsoft Exchange.";
	}
}
