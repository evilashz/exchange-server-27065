using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderSync
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderSyncCommand : EasServerCommand<FolderSyncRequest, FolderSyncResponse, FolderSyncStatus>
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000048F5 File Offset: 0x00002AF5
		internal FolderSyncCommand(EasConnectionSettings easConnectionSettings) : base(Command.FolderSync, easConnectionSettings)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004900 File Offset: 0x00002B00
		protected override void AddWebRequestHeaders(HttpWebRequest request)
		{
			base.AddWebRequestHeaders(request);
			EasExtensionCapabilities extensionCapabilities = base.EasConnectionSettings.ExtensionCapabilities;
			if (extensionCapabilities != null && extensionCapabilities.SupportsExtensions(EasExtensionsVersion1.FolderTypes))
			{
				request.Headers.Add("X-OLK-Extension", extensionCapabilities.RequestExtensions(EasExtensionsVersion1.FolderTypes));
			}
		}
	}
}
