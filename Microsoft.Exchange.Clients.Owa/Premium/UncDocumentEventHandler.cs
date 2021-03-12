using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.DocumentLibrary;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004E7 RID: 1255
	[OwaEventNamespace("UncDocument")]
	internal sealed class UncDocumentEventHandler : DocumentEventHandler
	{
		// Token: 0x06002FB4 RID: 12212 RVA: 0x00115470 File Offset: 0x00113670
		protected override void PreDataBind()
		{
			if (!DocumentLibraryUtilities.IsNavigationToUNCAllowed(base.UserContext))
			{
				throw new OwaSegmentationException("Access to Unc documents is disabled");
			}
			this.contentTypePropertyDefinition = UncDocumentSchema.FileType;
		}

		// Token: 0x04002181 RID: 8577
		public const string EventNamespace = "UncDocument";
	}
}
