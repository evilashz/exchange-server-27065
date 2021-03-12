using System;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000013 RID: 19
	[SnapInAbout("Microsoft.Exchange.Management.NativeResources.dll", ApplicationBaseRelative = true, VendorId = 101, VersionId = 102, DisplayNameId = 1013, DescriptionId = 1014, IconId = 1010, LargeFolderBitmapId = 1011, SmallFolderBitmapId = 1012, SmallFolderSelectedBitmapId = 1012, FolderBitmapsColorMask = 16711935)]
	[SnapInSettings("8AC8AAAE-D130-48f2-9CD4-9375DA3F9BAE", DisplayName = "Details Templates Editor", Description = "Allows management of Exchange Server 2007 Details Templates. © 2006 Microsoft Corporation. All rights reserved.", UseCustomHelp = true)]
	public class DetailsTemplatesSnapIn : ExchangeDynamicServerSnapIn
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00005058 File Offset: 0x00003258
		public DetailsTemplatesSnapIn()
		{
			base.RootNode = new DetailsTemplatesRootNode();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000506B File Offset: 0x0000326B
		public override string SnapInGuidString
		{
			get
			{
				return "8AC8AAAE-D130-48f2-9CD4-9375DA3F9BAE";
			}
		}

		// Token: 0x0400003F RID: 63
		public const string SnapInGuid = "8AC8AAAE-D130-48f2-9CD4-9375DA3F9BAE";

		// Token: 0x04000040 RID: 64
		public const string SnapInDisplayName = "Details Templates Editor";

		// Token: 0x04000041 RID: 65
		public const string SnapInDescription = "Allows management of Exchange Server 2007 Details Templates. © 2006 Microsoft Corporation. All rights reserved.";
	}
}
