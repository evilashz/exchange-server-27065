using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200032F RID: 815
	internal class GetWacIframeUrl : ServiceCommand<string>
	{
		// Token: 0x06001B13 RID: 6931 RVA: 0x00066B83 File Offset: 0x00064D83
		public GetWacIframeUrl(CallContext callContext, string attachmentId) : base(callContext)
		{
			this.attachmentId = attachmentId;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00066B94 File Offset: 0x00064D94
		protected override string InternalExecute()
		{
			string wacUrl;
			using (AttachmentHandler.IAttachmentRetriever attachmentRetriever = AttachmentRetriever.CreateInstance(this.attachmentId, base.CallContext))
			{
				StoreSession session = attachmentRetriever.RootItem.Session;
				Item rootItem = attachmentRetriever.RootItem;
				Attachment attachment = attachmentRetriever.Attachment;
				WacAttachmentType wacAttachmentType = GetWacAttachmentInfo.Execute(base.CallContext, session, rootItem, attachment, null, this.attachmentId, false);
				if (wacAttachmentType == null)
				{
					throw new OwaInvalidOperationException("There is no reason known for code to reach here without throwing an unhandled exception elsewhere");
				}
				wacUrl = wacAttachmentType.WacUrl;
			}
			return wacUrl;
		}

		// Token: 0x04000F05 RID: 3845
		private readonly string attachmentId;
	}
}
