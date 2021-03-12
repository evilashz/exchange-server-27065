using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.ItemOperations
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemOperationsCommand : EasServerCommand<ItemOperationsRequest, ItemOperationsResponse, ItemOperationsStatus>
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00004D2D File Offset: 0x00002F2D
		internal ItemOperationsCommand(EasConnectionSettings easConnectionSettings) : base(Command.ItemOperations, easConnectionSettings)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004D38 File Offset: 0x00002F38
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
