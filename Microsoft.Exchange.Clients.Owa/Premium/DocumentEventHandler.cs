using System;
using System.Globalization;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000498 RID: 1176
	internal abstract class DocumentEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002D57 RID: 11607 RVA: 0x000FE6E9 File Offset: 0x000FC8E9
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(UncDocumentEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(SharepointDocumentEventHandler));
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000FE70C File Offset: 0x000FC90C
		[OwaEvent("GetDoc")]
		[OwaEventParameter("TranslatedURL", typeof(string), false, true)]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("URL", typeof(string))]
		[OwaEventParameter("id", typeof(string), false, true)]
		[OwaEventParameter("allowLevel2", typeof(int), false, true)]
		public void GetDocument()
		{
			bool flag = false;
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "DocumentEventHandler.GetDocument");
			base.ShowErrorInPage = true;
			base.DontWriteHeaders = true;
			HttpContext httpContext = base.OwaContext.HttpContext;
			if (!DocumentLibraryUtilities.IsDocumentsAccessEnabled(base.UserContext))
			{
				throw new OwaSegmentationException("Access to this document library is disabled");
			}
			string text = (string)base.GetParameter("id");
			string s = (string)base.GetParameter("URL");
			DocumentLibraryObjectId documentLibraryObjectId = DocumentLibraryUtilities.CreateDocumentLibraryObjectId(base.OwaContext);
			if (documentLibraryObjectId == null)
			{
				return;
			}
			try
			{
				this.DataBind(documentLibraryObjectId);
			}
			finally
			{
				if (this.stream == null)
				{
					this.Dispose();
				}
			}
			if (this.stream == null)
			{
				return;
			}
			UserContext userContext = base.OwaContext.UserContext;
			AttachmentPolicy.Level levelForAttachment = AttachmentLevelLookup.GetLevelForAttachment(Path.GetExtension(this.fileName), this.contentType, userContext);
			if (base.IsParameterSet("allowLevel2"))
			{
				flag = true;
			}
			if (levelForAttachment == AttachmentPolicy.Level.Block)
			{
				string errorDescription = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(1280363351), new object[]
				{
					this.fileName
				});
				Utilities.TransferToErrorPage(base.OwaContext, errorDescription, null, ThemeFileId.ButtonDialogInfo, true);
				return;
			}
			if (levelForAttachment == AttachmentPolicy.Level.ForceSave && !flag)
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(httpContext.Request, "ns");
				string text2 = string.Concat(new string[]
				{
					"<br> <a onclick=\"return false;\" href=\"ev.owa?ns=",
					queryStringParameter,
					"&ev=GetDoc&allowLevel2=1&URL=",
					Utilities.UrlEncode(s),
					"&id=",
					Utilities.UrlEncode(documentLibraryObjectId.ToBase64String()),
					Utilities.GetCanaryRequestParameter(),
					"\">",
					Utilities.HtmlEncode(this.fileName),
					"</a>"
				});
				string errorDetailedDescription = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetHtmlEncoded(-625229753), new object[]
				{
					text2
				});
				Utilities.TransferToErrorPage(base.OwaContext, LocalizedStrings.GetHtmlEncoded(-226672911), errorDetailedDescription, ThemeFileId.ButtonDialogInfo, true, true);
				return;
			}
			int num = AttachmentHandler.SendDocumentContentToHttpStream(httpContext, this.stream, this.fileName, DocumentEventHandler.CalculateFileExtension(this.fileName), this.contentType);
			if (this.contentType != null && this.contentType.Equals("application/x-zip-compressed", StringComparison.OrdinalIgnoreCase))
			{
				Utilities.DisableContentEncodingForThisResponse(base.OwaContext.HttpContext.Response);
			}
			if (Globals.ArePerfCountersEnabled)
			{
				if ((documentLibraryObjectId.UriFlags & UriFlags.Sharepoint) != (UriFlags)0)
				{
					OwaSingleCounters.WssBytes.IncrementBy((long)num);
					OwaSingleCounters.WssRequests.Increment();
					return;
				}
				if ((documentLibraryObjectId.UriFlags & UriFlags.Unc) != (UriFlags)0)
				{
					OwaSingleCounters.UncBytes.IncrementBy((long)num);
					OwaSingleCounters.UncRequests.Increment();
				}
			}
		}

		// Token: 0x06002D59 RID: 11609
		protected abstract void PreDataBind();

		// Token: 0x06002D5A RID: 11610 RVA: 0x000FE9BC File Offset: 0x000FCBBC
		protected void DataBind(DocumentLibraryObjectId objectId)
		{
			this.PreDataBind();
			IDocument document;
			try
			{
				document = DocumentLibraryUtilities.LoadDocumentLibraryItem(objectId, base.UserContext);
			}
			catch (AccessDeniedException)
			{
				ExTraceGlobals.DocumentsTracer.TraceDebug(0L, "URI access is denied.");
				Utilities.TransferToErrorPage(base.OwaContext, LocalizedStrings.GetNonEncoded(234621291));
				return;
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.DocumentsTracer.TraceDebug(0L, "URI object not found.");
				Utilities.TransferToErrorPage(base.OwaContext, LocalizedStrings.GetNonEncoded(1599334062));
				return;
			}
			if (document == null)
			{
				throw new OwaInvalidRequestException("objectId is invalid for unc/wss document");
			}
			object obj = document.TryGetProperty(this.contentTypePropertyDefinition);
			if (!(obj is PropertyError))
			{
				this.contentType = (obj as string);
			}
			else
			{
				this.contentType = string.Empty;
			}
			this.stream = document.GetDocument();
			this.fileName = Path.GetFileName(document.Uri.ToString());
			if (!string.IsNullOrEmpty(this.fileName))
			{
				this.fileName = HttpUtility.UrlDecode(this.fileName);
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000FEACC File Offset: 0x000FCCCC
		private static string CalculateFileExtension(string fileName)
		{
			if (fileName == null)
			{
				return null;
			}
			int num = fileName.LastIndexOf('.');
			if (num >= 0 && num < fileName.Length - 1)
			{
				return fileName.Substring(num);
			}
			return string.Empty;
		}

		// Token: 0x04001E04 RID: 7684
		public const string MethodGetDocument = "GetDoc";

		// Token: 0x04001E05 RID: 7685
		public const string DocumentIdQueryParameter = "id";

		// Token: 0x04001E06 RID: 7686
		public const string DocumentLevel2AllowParameter = "allowLevel2";

		// Token: 0x04001E07 RID: 7687
		protected string contentType;

		// Token: 0x04001E08 RID: 7688
		protected string fileName;

		// Token: 0x04001E09 RID: 7689
		protected Stream stream;

		// Token: 0x04001E0A RID: 7690
		protected PropertyDefinition contentTypePropertyDefinition;
	}
}
