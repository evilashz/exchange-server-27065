using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Clients.Owa2.Server.Core.attachment;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000030 RID: 48
	internal class AttachmentHandler
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00004177 File Offset: 0x00002377
		public AttachmentHandler(string id, IAttachmentWebOperationContext webOperationContext, CallContext callContext, ConfigurationContextBase configurationContext)
		{
			this.id = id;
			this.callContext = callContext;
			this.webOperationContext = webOperationContext;
			this.configurationContext = configurationContext;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000419C File Offset: 0x0000239C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000041A4 File Offset: 0x000023A4
		public bool IsImagePreview { get; set; }

		// Token: 0x06000107 RID: 263 RVA: 0x000041B0 File Offset: 0x000023B0
		public Stream GetAttachmentStream(AttachmentHandler.IAttachmentRetriever attachmentRetriever, AttachmentHandler.IAttachmentPolicyChecker policyChecker, bool asDataUri)
		{
			ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "AttachmentHandler.GetAttachmentStream: Getting attachment stream for id={0}", this.id);
			if (string.IsNullOrEmpty(this.id))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug((long)this.GetHashCode(), "Attachment id is empty or null. returning null stream.");
				throw new FaultException("Id cannot be null or empty.");
			}
			Stream stream = null;
			ByteEncoder byteEncoder = null;
			Stream stream2 = null;
			Stream result;
			try
			{
				Attachment attachment = attachmentRetriever.Attachment;
				AttachmentPolicyLevel policy = policyChecker.GetPolicy(attachment, this.webOperationContext.IsPublicLogon);
				if (AudioFile.IsProtectedVoiceAttachment(attachment.FileName))
				{
					attachment.ContentType = Utils.GetUnProtectedVoiceAttachmentContentType(attachment.FileName);
					stream = DRMUtils.OpenProtectedAttachment(attachment, this.callContext.AccessingADUser.OrganizationId);
					string fileName;
					if (AudioFile.TryGetNonDRMFileNameFromDRM(attachment.FileName, out fileName))
					{
						attachment.FileName = fileName;
					}
				}
				string text = AttachmentUtilities.GetContentType(attachment);
				string fileExtension = attachment.FileExtension;
				OleAttachment oleAttachment = attachment as OleAttachment;
				if (oleAttachment != null)
				{
					stream = oleAttachment.ConvertToImage(ImageFormat.Jpeg);
					if (stream != null)
					{
						text = "image/jpeg";
					}
				}
				if (this.IsBlocked(policy) && !attachment.IsInline && !this.IsImagePreview)
				{
					ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "Attachment is blocked. Returning null stream. id is {0}", this.id);
					result = null;
				}
				else
				{
					this.WriteResponseHeaders(text, policy, attachment);
					if (stream == null)
					{
						StreamAttachment streamAttachment = attachment as StreamAttachment;
						if (streamAttachment != null)
						{
							if (string.Equals(text, "audio/mpeg", StringComparison.OrdinalIgnoreCase))
							{
								stream = streamAttachment.GetContentStream(PropertyOpenMode.Modify);
							}
							else
							{
								stream = streamAttachment.GetContentStream(PropertyOpenMode.ReadOnly);
							}
						}
						else
						{
							ItemAttachment itemAttachment = attachment as ItemAttachment;
							if (itemAttachment != null)
							{
								using (Item item = itemAttachment.GetItem(StoreObjectSchema.ContentConversionProperties))
								{
									IRecipientSession adrecipientSession = item.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
									OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(this.callContext.DefaultDomain.DomainName.Domain);
									outboundConversionOptions.ClearCategories = false;
									outboundConversionOptions.UserADSession = adrecipientSession;
									outboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(adrecipientSession.SessionSettings.CurrentOrganizationId);
									stream = new MemoryStream();
									ItemConversion.ConvertItemToMime(item, stream, outboundConversionOptions);
									stream.Seek(0L, SeekOrigin.Begin);
								}
							}
						}
					}
					long num = 0L;
					long num2 = 0L;
					if (AttachmentUtilities.NeedToFilterHtml(text, fileExtension, policy, this.configurationContext))
					{
						stream = AttachmentUtilities.GetFilteredStream(this.configurationContext, stream, attachment.TextCharset, attachmentRetriever.BlockStatus);
					}
					else if (this.NeedToSendPartialContent(stream, out num, out num2))
					{
						string value = string.Format(CultureInfo.InvariantCulture, "bytes {0}-{1}/{2}", new object[]
						{
							num,
							num2,
							stream.Length
						});
						this.webOperationContext.Headers["Accept-Ranges"] = "bytes";
						this.webOperationContext.Headers["Content-Range"] = value;
						this.webOperationContext.ETag = this.id;
						this.webOperationContext.StatusCode = HttpStatusCode.PartialContent;
						long num3 = num2 - num + 1L;
						if (num3 < stream.Length)
						{
							ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<long, long, long>((long)this.GetHashCode(), "RangeBytes:{0} - Seek:{1} - SetLength:{2}", num3, num, num2 + 1L);
							stream.Seek(num, SeekOrigin.Begin);
							return new BoundedStream(stream, true, num, num2);
						}
					}
					if (asDataUri)
					{
						byteEncoder = new Base64Encoder();
						stream2 = new EncoderStream(stream, byteEncoder, EncoderStreamAccess.Read);
						stream = new DataUriStream(stream2, text);
					}
					result = stream;
				}
			}
			catch (Exception ex)
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				if (stream2 != null)
				{
					stream2.Dispose();
				}
				if (byteEncoder != null)
				{
					byteEncoder.Dispose();
				}
				string formatString = string.Empty;
				if (ex is ExchangeDataException)
				{
					formatString = "Fail to sanitize HTML getting attachment. id is {0}, Exception: {1}";
				}
				else if (ex is StoragePermanentException)
				{
					formatString = "StoragePermanentException when getting attachment. id is {0}, Exception: {1}";
				}
				else
				{
					if (!(ex is StorageTransientException))
					{
						throw;
					}
					formatString = "StorageTransientException when getting attachment. id is {0}, Exception: {1}";
				}
				ExTraceGlobals.AttachmentHandlingTracer.TraceError<string, Exception>((long)this.GetHashCode(), formatString, this.id, ex);
				throw new CannotOpenFileAttachmentException(ex);
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000045C0 File Offset: 0x000027C0
		private static string GetSubject(Item item)
		{
			string text = item.TryGetProperty(ItemSchema.Subject) as string;
			if (string.IsNullOrEmpty(text))
			{
				text = Strings.GetLocalizedString(730745110);
			}
			return text;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000045F4 File Offset: 0x000027F4
		private static BlockStatus GetItemBlockStatus(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			BlockStatus result = BlockStatus.DontKnow;
			object obj = item.TryGetProperty(ItemSchema.BlockStatus);
			if (obj is BlockStatus)
			{
				result = (BlockStatus)obj;
			}
			return result;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004630 File Offset: 0x00002830
		private void WriteResponseHeaders(string contentType, AttachmentPolicyLevel policyLevel, Attachment attachment)
		{
			if (policyLevel == AttachmentPolicyLevel.ForceSave)
			{
				this.webOperationContext.Headers["X-Download-Options"] = "noopen";
			}
			if (string.Compare(contentType, "text/html", StringComparison.OrdinalIgnoreCase) == 0)
			{
				attachment.IsInline = false;
			}
			this.webOperationContext.ContentType = contentType + "; authoritative=true;";
			this.WriteContentDispositionResponseHeader(attachment.FileName, attachment.IsInline);
			if (attachment.IsInline || this.IsImagePreview)
			{
				this.webOperationContext.SetNoCacheNoStore();
				return;
			}
			DateHeader dateHeader = new DateHeader("Date", DateTime.UtcNow.AddDays(-1.0));
			this.webOperationContext.Headers["Expires"] = dateHeader.Value;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000046F0 File Offset: 0x000028F0
		private void WriteContentDispositionResponseHeader(string fileName, bool isInline)
		{
			bool chrome = this.webOperationContext.UserAgent.IsBrowserChrome();
			string text = AttachmentUtilities.ToHexString(fileName, chrome);
			string value = string.Empty;
			if (this.webOperationContext.UserAgent.IsBrowserFirefox())
			{
				value = string.Format(CultureInfo.InvariantCulture, (this.webOperationContext.UserAgent.BrowserVersion.Build >= 8) ? "{0}; filename*=UTF-8''{1}" : "{0}; filename*=\"{1}\"", new object[]
				{
					isInline ? "inline" : "attachment",
					text
				});
			}
			else
			{
				value = string.Format(CultureInfo.InvariantCulture, "{0}; filename=\"{1}\"", new object[]
				{
					isInline ? "inline" : "attachment",
					text
				});
			}
			this.webOperationContext.Headers["Content-Disposition"] = value;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000047C5 File Offset: 0x000029C5
		private bool IsBlocked(AttachmentPolicyLevel policyLevel)
		{
			if (policyLevel == AttachmentPolicyLevel.Block)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "Attachment is blocked because of policy level: id={0}", this.id);
				return true;
			}
			return false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000047EC File Offset: 0x000029EC
		private bool NeedToSendPartialContent(Stream stream, out long startByte, out long endByte)
		{
			startByte = (endByte = 0L);
			string requestHeader = this.webOperationContext.GetRequestHeader("Range");
			if (string.IsNullOrEmpty(requestHeader))
			{
				return false;
			}
			if (!stream.CanSeek)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string>((long)this.GetHashCode(), "NeedToSendPartialContent: Range requested but stream does not support seek. Range: {0}.", requestHeader);
				return false;
			}
			string[] array = requestHeader.Replace("bytes=", string.Empty).Split(",".ToCharArray(), 2);
			if (array.Length != 1)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string>((long)this.GetHashCode(), "NeedToSendPartialContent: Multipart range requests not supported. Range: {0}.", requestHeader);
				return false;
			}
			string[] array2 = array[0].Split("-".ToCharArray(), 3);
			if (array2.Length != 2)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string>((long)this.GetHashCode(), "NeedToSendPartialContent: Invalid Range: {0}.", requestHeader);
				return false;
			}
			if (string.IsNullOrEmpty(array2[1]))
			{
				endByte = stream.Length - 1L;
			}
			else if (!long.TryParse(array2[1], out endByte))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string, string>((long)this.GetHashCode(), "NeedToSendPartialContent: Invalid Range: {0}. Could not parse {1} into a long.", requestHeader, array2[1]);
				return false;
			}
			if (string.IsNullOrEmpty(array2[0]))
			{
				startByte = stream.Length - 1L - endByte;
				endByte = stream.Length - 1L;
			}
			else if (!long.TryParse(array2[0], out startByte))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string, string>((long)this.GetHashCode(), "NeedToSendPartialContent: Invalid Range: {0}. Could not parse {1} into a long.", requestHeader, array2[0]);
				return false;
			}
			if (endByte >= stream.Length || endByte < startByte)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string, long, long>((long)this.GetHashCode(), "NeedToSendPartialContent: Invalid Range {0}. StartByte:{1} EndByte:{2}", requestHeader, startByte, endByte);
				return false;
			}
			ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string, long, long>((long)this.GetHashCode(), "NeedToSendPartialContent: Valid Range {0}. StartByte:{1} EndByte:{2}", requestHeader, startByte, endByte);
			return true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004984 File Offset: 0x00002B84
		public Stream GetAllAttachmentsAsZipStream(AttachmentHandler.IAttachmentRetriever attachmentRetriever)
		{
			ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "AttachmentHandler.GetAttachmentStream: Getting attachment stream for id={0}", this.id);
			if (string.IsNullOrEmpty(this.id))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug((long)this.GetHashCode(), "Item id is empty or null. returning null stream.");
				throw new FaultException("Id cannot be null or empty.");
			}
			Item rootItem = attachmentRetriever.RootItem;
			if (rootItem is ReportMessage || ObjectClass.IsSmsMessage(rootItem.ClassName))
			{
				return null;
			}
			Stream stream = null;
			Stream result;
			try
			{
				AttachmentHandler.IAttachmentPolicyChecker attachmentPolicyChecker = AttachmentPolicyChecker.CreateInstance(this.configurationContext.AttachmentPolicy);
				AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(rootItem);
				BlockStatus itemBlockStatus = AttachmentHandler.GetItemBlockStatus(rootItem);
				string subject = AttachmentHandler.GetSubject(rootItem);
				ZipFileAttachments zipFileAttachments = new ZipFileAttachments(itemBlockStatus, subject);
				foreach (AttachmentHandle handle in attachmentCollection)
				{
					using (Attachment attachment = attachmentCollection.Open(handle))
					{
						if (attachment is OleAttachment || attachment.IsInline || attachment is ReferenceAttachment)
						{
							ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "Attachment is inline, an ole image, or a reference attachment. Do not add to zip file.  id is {0}", attachment.Id.ToString());
						}
						else
						{
							AttachmentPolicyLevel policy = attachmentPolicyChecker.GetPolicy(attachment, this.webOperationContext.IsPublicLogon);
							if (this.IsBlocked(policy))
							{
								ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "Attachment is blocked. Do not add to zip file.  id is {0}", attachment.Id.ToString());
							}
							else
							{
								zipFileAttachments.AddAttachmentToZip(attachment, policy, this.configurationContext);
							}
						}
					}
				}
				if (zipFileAttachments.Count == 0)
				{
					ExTraceGlobals.AttachmentHandlingTracer.TraceDebug((long)this.GetHashCode(), "AttachmentHandler.GetAllAttachmentsAsZipStream: No attachments returned for item");
					result = null;
				}
				else
				{
					IRecipientSession adrecipientSession = rootItem.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
					OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(this.callContext.DefaultDomain.DomainName.Domain);
					outboundConversionOptions.ClearCategories = false;
					outboundConversionOptions.UserADSession = adrecipientSession;
					outboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(adrecipientSession.SessionSettings.CurrentOrganizationId);
					stream = zipFileAttachments.WriteArchive(this.configurationContext, this.webOperationContext, outboundConversionOptions, attachmentCollection);
					stream.Seek(0L, SeekOrigin.Begin);
					result = stream;
				}
			}
			catch (Exception ex)
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				string formatString = string.Empty;
				if (ex is ExchangeDataException)
				{
					formatString = "Fail to sanitize HTML getting attachment. id is {0}, Exception: {1}";
				}
				else if (ex is StoragePermanentException)
				{
					formatString = "StoragePermanentException when getting attachment. id is {0}, Exception: {1}";
				}
				else
				{
					if (!(ex is StorageTransientException))
					{
						throw;
					}
					formatString = "StorageTransientException when getting attachment. id is {0}, Exception: {1}";
				}
				ExTraceGlobals.AttachmentHandlingTracer.TraceError<string, Exception>((long)this.GetHashCode(), formatString, this.id, ex);
				throw new CannotOpenFileAttachmentException(ex);
			}
			return result;
		}

		// Token: 0x04000063 RID: 99
		public const string AuthoritativeTrueHeader = "; authoritative=true;";

		// Token: 0x04000064 RID: 100
		public const string MpegContentType = "audio/mpeg";

		// Token: 0x04000065 RID: 101
		private const string AcceptRangesBytes = "bytes";

		// Token: 0x04000066 RID: 102
		private const string ContentRangeFormat = "bytes {0}-{1}/{2}";

		// Token: 0x04000067 RID: 103
		private static readonly EncodingOptions rfc2231EncodingOptions = new EncodingOptions(Encoding.UTF8.WebName, null, EncodingFlags.EnableRfc2231);

		// Token: 0x04000068 RID: 104
		private string id;

		// Token: 0x04000069 RID: 105
		private CallContext callContext;

		// Token: 0x0400006A RID: 106
		private IAttachmentWebOperationContext webOperationContext;

		// Token: 0x0400006B RID: 107
		private ConfigurationContextBase configurationContext;

		// Token: 0x02000031 RID: 49
		internal interface IAttachmentRetriever : IDisposable
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000110 RID: 272
			BlockStatus BlockStatus { get; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000111 RID: 273
			Attachment Attachment { get; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000112 RID: 274
			Item RootItem { get; }
		}

		// Token: 0x02000032 RID: 50
		internal interface IAttachmentPolicyChecker
		{
			// Token: 0x06000113 RID: 275
			AttachmentPolicyLevel GetPolicy(IAttachment attachment, bool isPublicLogin);
		}
	}
}
