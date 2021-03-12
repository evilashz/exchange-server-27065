﻿using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000C3 RID: 195
	public class AttachmentHandler : IHttpHandler
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x000377E8 File Offset: 0x000359E8
		internal static int SendDocumentContentToHttpStream(HttpContext httpContext, Stream stream, string fileName, string fileExtension, string contentType)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = LocalizedStrings.GetNonEncoded(1797976510);
			}
			if (fileExtension == null)
			{
				fileExtension = string.Empty;
			}
			if (contentType == null)
			{
				contentType = string.Empty;
			}
			bool flag = AttachmentUtility.DoNeedToFilterHtml(contentType, fileExtension, AttachmentPolicy.Level.Unknown, OwaContext.Get(httpContext).UserContext);
			if (flag)
			{
				AttachmentUtility.UpdateContentTypeForNeedToFilter(out contentType, null);
			}
			return AttachmentHandler.SendDocumentContentToHttpStream(httpContext, stream, fileName, fileExtension, contentType, false, null, BlockStatus.DontKnow, AttachmentPolicy.Level.Unknown, flag);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0003786C File Offset: 0x00035A6C
		private static int SendDocumentContentToHttpStream(HttpContext httpContext, Stream stream, string fileName, string fileExtension, string contentType, bool isInline, Charset charset, BlockStatus blockStatus, AttachmentPolicy.Level level, bool doNeedToFilterHtml)
		{
			if (AttachmentUtility.IsMhtmlAttachment(contentType, fileExtension))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>(0L, "AttachmentHandler.SendDocumentContentToHttpStream: Explicitly blocking MHTML attachment {0}", fileName);
				return 0;
			}
			AttachmentHandler.SetAttachmentResponseHeaders(httpContext, fileName, contentType, isInline, level);
			uint result;
			if (doNeedToFilterHtml)
			{
				result = AttachmentUtility.WriteFilteredResponse(httpContext, stream, charset, blockStatus);
			}
			else
			{
				bool isNotHtmlandNotXml = !AttachmentUtility.GetIsHtmlOrXml(contentType, fileExtension);
				bool doNotSniff = AttachmentUtility.GetDoNotSniff(level, OwaContext.Get(httpContext).UserContext);
				result = AttachmentUtility.WriteUnfilteredResponse(httpContext, stream, fileName, isNotHtmlandNotXml, doNotSniff);
			}
			return (int)result;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000378E4 File Offset: 0x00035AE4
		private static void SetAttachmentResponseHeaders(HttpContext httpContext, string fileName, string contentType, bool isInline, AttachmentPolicy.Level level)
		{
			if (level == AttachmentPolicy.Level.ForceSave)
			{
				httpContext.Response.AppendHeader("X-Download-Options", "noopen");
			}
			AttachmentUtility.SetContentDispositionResponseHeader(httpContext, fileName, isInline);
			httpContext.Response.AppendHeader("Content-Type", contentType + "; authoritative=true;");
			if (isInline && OwaContext.Current.UserContext.IsPublicLogon)
			{
				Utilities.MakePageNoCacheNoStore(httpContext.Response);
				return;
			}
			httpContext.Response.Cache.SetExpires(AttachmentUtility.GetAttachmentExpiryDate());
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00037963 File Offset: 0x00035B63
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00037968 File Offset: 0x00035B68
		public void ProcessRequest(HttpContext httpContext)
		{
			ExTraceGlobals.AttachmentHandlingTracer.TraceDebug((long)this.GetHashCode(), "AttachmentHandler.ProcessRequest");
			try
			{
				AttachmentUtility.UpdateAcceptEncodingHeader(httpContext);
				OwaContext owaContext = OwaContext.Get(httpContext);
				UserContext userContext = owaContext.UserContext;
				string text = Utilities.GetQueryStringParameter(httpContext.Request, "id");
				string queryStringParameter = Utilities.GetQueryStringParameter(httpContext.Request, "dla", false);
				bool flag = queryStringParameter == null && this.IsVoiceMailAttachment(httpContext);
				if (flag)
				{
					text = Utilities.GetStringfromBase64String(text);
				}
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(text);
				using (Item item = Utilities.GetItem<Item>(userContext, owaStoreObjectId, new PropertyDefinition[0]))
				{
					if (item == null)
					{
						ExTraceGlobals.AttachmentHandlingTracer.TraceError((long)this.GetHashCode(), "AttachmentHandler.ProcessRequest: Unable to retrieve parent item for attachment");
					}
					else
					{
						if (userContext.IsIrmEnabled && !userContext.IsBasicExperience)
						{
							Utilities.IrmDecryptIfRestricted(item, userContext, true);
						}
						if (queryStringParameter == null)
						{
							this.DownloadOneAttachment(item, userContext, httpContext, flag);
						}
						else
						{
							string queryStringParameter2 = Utilities.GetQueryStringParameter(httpContext.Request, "femb", false);
							if (queryStringParameter2 != null)
							{
								using (Item embeddedItem = AttachmentUtility.GetEmbeddedItem(item, httpContext.Request, userContext))
								{
									if (embeddedItem != null)
									{
										this.DownloadAllAttachments(embeddedItem, httpContext, userContext);
									}
									else
									{
										ExTraceGlobals.AttachmentHandlingTracer.TraceError((long)this.GetHashCode(), "AttachmentHandler.ProcessRequest: Failure to obtain reference to embedded item containing attachment");
									}
									goto IL_12E;
								}
							}
							this.DownloadAllAttachments(item, httpContext, userContext);
						}
						IL_12E:;
					}
				}
			}
			catch (ThreadAbortException)
			{
				OwaContext.Get(httpContext).UnlockMinResourcesOnCriticalError();
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00037B10 File Offset: 0x00035D10
		private void DownloadOneAttachment(Item item, UserContext userContext, HttpContext httpContext, bool isVoiceMailAttachment)
		{
			Attachment attachment = null;
			try
			{
				if (!isVoiceMailAttachment)
				{
					attachment = Utilities.GetAttachment(item, httpContext.Request, userContext);
				}
				else
				{
					if (!userContext.IsFeatureEnabled(Feature.UMIntegration))
					{
						throw new OwaSegmentationException("Access to Voice mail is disabled");
					}
					attachment = Utilities.GetLatestVoiceMailAttachment(item, userContext);
				}
				if (attachment == null)
				{
					ExTraceGlobals.AttachmentHandlingTracer.TraceError((long)this.GetHashCode(), "AttachmentHandler.DownloadOneAttachment: Unable to retrieve attachment from store");
				}
				else
				{
					BlockStatus itemBlockStatus = AttachmentUtility.GetItemBlockStatus(item);
					this.ProcessAttachment(attachment, httpContext, itemBlockStatus);
				}
			}
			finally
			{
				if (attachment != null)
				{
					attachment.Dispose();
				}
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00037B9C File Offset: 0x00035D9C
		private void ProcessAttachment(Attachment attachment, HttpContext httpContext, BlockStatus blockStatus)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}
			OwaContext owaContext = OwaContext.Get(httpContext);
			UserContext userContext = owaContext.UserContext;
			Stream stream = null;
			try
			{
				StreamAttachmentBase streamAttachment = AttachmentUtility.GetStreamAttachment(attachment);
				if (streamAttachment == null)
				{
					ExTraceGlobals.AttachmentHandlingTracer.TraceError((long)this.GetHashCode(), "AttachmentHandler.ProcessAttachment: attachment is not derived from AttachmentStream");
				}
				else
				{
					AttachmentPolicy.Level attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(streamAttachment, userContext);
					if (attachmentLevel == AttachmentPolicy.Level.Block)
					{
						Utilities.TransferToErrorPage(owaContext, LocalizedStrings.GetNonEncoded(-2000404449), LocalizedStrings.GetNonEncoded(-615885395));
					}
					else
					{
						string fileName = AttachmentUtility.CalculateAttachmentName(streamAttachment.DisplayName, streamAttachment.FileName);
						string text = streamAttachment.FileExtension;
						if (text == null)
						{
							text = string.Empty;
						}
						string empty = string.Empty;
						bool contentType = this.GetContentType(httpContext, streamAttachment, attachmentLevel, out empty);
						bool isInline = this.GetIsInline(streamAttachment, attachmentLevel);
						stream = AttachmentUtility.GetStream(streamAttachment);
						if (stream == null)
						{
							ExTraceGlobals.AttachmentHandlingTracer.TraceError((long)this.GetHashCode(), "AttachmentHandler.ProcessAttachment: Image conversion of OLE attachment failure");
						}
						else
						{
							AttachmentHandler.SendDocumentContentToHttpStream(httpContext, stream, fileName, text, empty, isInline, streamAttachment.TextCharset, blockStatus, attachmentLevel, contentType);
						}
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00037CBC File Offset: 0x00035EBC
		private void DownloadAllAttachments(Item item, HttpContext httpContext, UserContext userContext)
		{
			BlockStatus itemBlockStatus = AttachmentUtility.GetItemBlockStatus(item);
			string subject = this.GetSubject(item);
			ArrayList attachmentListForZip = AttachmentUtility.GetAttachmentListForZip(item, true, userContext);
			if (attachmentListForZip == null || attachmentListForZip.Count == 0)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug((long)this.GetHashCode(), "AttachmentHandler.DownloadAllAttachments: No attachments returned for item");
				return;
			}
			ZipFileAttachments zipFileAttachments = new ZipFileAttachments(itemBlockStatus, subject);
			foreach (object obj in attachmentListForZip)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
				zipFileAttachments.AddAttachmentToZip(attachmentWellInfo, userContext, httpContext);
			}
			zipFileAttachments.WriteArchive(httpContext);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00037D64 File Offset: 0x00035F64
		private string GetSubject(Item item)
		{
			string text = item.TryGetProperty(ItemSchema.Subject) as string;
			if (string.IsNullOrEmpty(text))
			{
				text = LocalizedStrings.GetNonEncoded(730745110);
			}
			return text;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00037D98 File Offset: 0x00035F98
		private bool GetContentType(HttpContext httpContext, StreamAttachmentBase streamAttachment, AttachmentPolicy.Level level, out string contentType)
		{
			contentType = AttachmentUtility.CalculateContentType(streamAttachment);
			bool flag = AttachmentUtility.DoNeedToFilterHtml(contentType, streamAttachment.FileExtension, level, OwaContext.Get(httpContext).UserContext);
			if (string.IsNullOrEmpty(contentType) && this.IsVoiceMailAttachment(httpContext))
			{
				contentType = "audio/x-ms-wma";
			}
			else if (flag)
			{
				AttachmentUtility.UpdateContentTypeForNeedToFilter(out contentType, streamAttachment.TextCharset);
			}
			return flag;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00037DF7 File Offset: 0x00035FF7
		private bool IsVoiceMailAttachment(HttpContext httpContext)
		{
			return !string.IsNullOrEmpty(Utilities.GetQueryStringParameter(httpContext.Request, "MSWMExt", false));
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00037E14 File Offset: 0x00036014
		private bool GetIsInline(StreamAttachmentBase streamAttachment, AttachmentPolicy.Level level)
		{
			OleAttachment oleAttachment = streamAttachment as OleAttachment;
			return level == AttachmentPolicy.Level.Allow && (streamAttachment.IsInline || null != oleAttachment);
		}

		// Token: 0x040004E1 RID: 1249
		private const string VoicemailQueryStringParam = "MSWMExt";
	}
}
