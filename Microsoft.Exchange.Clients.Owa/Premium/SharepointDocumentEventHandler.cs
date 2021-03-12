using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.DocumentLibrary;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004E3 RID: 1251
	[OwaEventNamespace("SharepointDocument")]
	internal sealed class SharepointDocumentEventHandler : DocumentEventHandler
	{
		// Token: 0x06002F71 RID: 12145 RVA: 0x00112A6C File Offset: 0x00110C6C
		protected override void PreDataBind()
		{
			if (!DocumentLibraryUtilities.IsNavigationToWSSAllowed(base.UserContext))
			{
				throw new OwaSegmentationException("Access to Sharepoint documents is disabled");
			}
			this.contentTypePropertyDefinition = SharepointDocumentSchema.FileType;
		}

		// Token: 0x04002149 RID: 8521
		public const string EventNamespace = "SharepointDocument";
	}
}
