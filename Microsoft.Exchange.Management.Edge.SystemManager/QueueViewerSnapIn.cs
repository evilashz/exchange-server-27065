using System;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000015 RID: 21
	[SnapInSettings("7F96F1BD-0EDA-4cb0-8393-632B83BD54EE", DisplayName = "Exchange Queue Viewer", Description = "Allow management of Exchange transport queues", UseCustomHelp = true)]
	[SnapInAbout("Microsoft.Exchange.Management.NativeResources.dll", ApplicationBaseRelative = true, VendorId = 101, VersionId = 102, DisplayNameId = 103, DescriptionId = 104, IconId = 110, LargeFolderBitmapId = 111, SmallFolderBitmapId = 112, SmallFolderSelectedBitmapId = 112, FolderBitmapsColorMask = 16711935)]
	public sealed class QueueViewerSnapIn : ExchangeDynamicServerSnapIn
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00006638 File Offset: 0x00004838
		public QueueViewerSnapIn()
		{
			base.RootNode = new QueueViewerNode();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000664B File Offset: 0x0000484B
		public override string SnapInGuidString
		{
			get
			{
				return "7F96F1BD-0EDA-4cb0-8393-632B83BD54EE";
			}
		}

		// Token: 0x0400003B RID: 59
		public const string SnapInGuid = "7F96F1BD-0EDA-4cb0-8393-632B83BD54EE";

		// Token: 0x0400003C RID: 60
		public const string SnapInDisplayName = "Exchange Queue Viewer";

		// Token: 0x0400003D RID: 61
		public const string SnapInDescription = "Allow management of Exchange transport queues";
	}
}
