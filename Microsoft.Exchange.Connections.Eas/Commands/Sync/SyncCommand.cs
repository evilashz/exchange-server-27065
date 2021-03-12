using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Sync
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncCommand : EasServerCommand<SyncRequest, SyncResponse, SyncStatus>
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00005681 File Offset: 0x00003881
		internal SyncCommand(EasConnectionSettings easConnectionSettings) : base(Command.Sync, easConnectionSettings)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000568C File Offset: 0x0000388C
		protected override void AddWebRequestHeaders(HttpWebRequest request)
		{
			base.AddWebRequestHeaders(request);
			EasExtensionCapabilities extensionCapabilities = base.EasConnectionSettings.ExtensionCapabilities;
			if (extensionCapabilities != null && extensionCapabilities.SupportsExtensions(EasExtensionsVersion1.SystemCategories))
			{
				request.Headers.Add("X-OLK-Extension", extensionCapabilities.RequestExtensions(EasExtensionsVersion1.SystemCategories));
			}
		}
	}
}
