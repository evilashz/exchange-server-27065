using System;
using Microsoft.Exchange.Clients.Owa.Core.Transcoding;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200028F RID: 655
	[OwaEventNamespace("WebReady")]
	internal sealed class WebReadyFileEventHandler : OwaEventHandlerBase
	{
		// Token: 0x0600192A RID: 6442 RVA: 0x00092B96 File Offset: 0x00090D96
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(WebReadyFileEventHandler));
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00092BA8 File Offset: 0x00090DA8
		[OwaEventParameter("d", typeof(string))]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("GetFile")]
		[OwaEventParameter("X-OWA-CANARY", typeof(string))]
		[OwaEventParameter("fileName", typeof(string))]
		public void GetFile()
		{
			string text = (string)base.GetParameter("d");
			string text2 = (string)base.GetParameter("fileName");
			if (text == null || text2 == null)
			{
				throw new OwaInvalidRequestException("DocumentId or fileName does not exist");
			}
			base.DontWriteHeaders = true;
			Utilities.MakePageNoCacheNoStore(this.HttpContext.Response);
			if (text2.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
			{
				this.HttpContext.Response.ContentType = Utilities.GetContentTypeString(OwaEventContentType.Css);
			}
			else
			{
				if (!text2.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
				{
					throw new OwaInvalidRequestException("Unsupported file type");
				}
				this.HttpContext.Response.ContentType = Utilities.GetContentTypeString(OwaEventContentType.Jpeg);
			}
			try
			{
				TranscodingTaskManager.TransmitFile(base.UserContext.Key.UserContextId, text, text2, this.HttpContext.Response);
			}
			catch (TranscodingFatalFaultException innerException)
			{
				throw new OwaInvalidRequestException("The TransmitFile function fails", innerException);
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00092C98 File Offset: 0x00090E98
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WebReadyFileEventHandler>(this);
		}

		// Token: 0x04001263 RID: 4707
		public const string EventNamespace = "WebReady";

		// Token: 0x04001264 RID: 4708
		public const string DocumentID = "d";

		// Token: 0x04001265 RID: 4709
		public const string FileName = "fileName";

		// Token: 0x04001266 RID: 4710
		public const string MethodGetFile = "GetFile";
	}
}
