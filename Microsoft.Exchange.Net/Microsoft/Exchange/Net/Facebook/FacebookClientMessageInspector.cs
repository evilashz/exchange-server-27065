using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200071B RID: 1819
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookClientMessageInspector : IClientMessageInspector
	{
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x0600227A RID: 8826 RVA: 0x00047238 File Offset: 0x00045438
		// (remove) Token: 0x0600227B RID: 8827 RVA: 0x00047270 File Offset: 0x00045470
		public event EventHandler<FacebookMessageEventArgs> MessageDownloaded;

		// Token: 0x0600227C RID: 8828 RVA: 0x000472A5 File Offset: 0x000454A5
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			return null;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000472A8 File Offset: 0x000454A8
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
			EventHandler<FacebookMessageEventArgs> messageDownloaded = this.MessageDownloaded;
			if (messageDownloaded != null && reply != null)
			{
				messageDownloaded(this, new FacebookMessageEventArgs(reply));
			}
		}
	}
}
