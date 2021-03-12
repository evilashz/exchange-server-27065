using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement.Protectors;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200005C RID: 92
	internal static class WacUtilities
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B56B File Offset: 0x0000976B
		public static string LocalServerName
		{
			get
			{
				WacUtilities.EnsureLocalServerNameAndVersion();
				return WacUtilities.localServerName;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B577 File Offset: 0x00009777
		public static string LocalServerVersion
		{
			get
			{
				WacUtilities.EnsureLocalServerNameAndVersion();
				return WacUtilities.localServerVersion;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B584 File Offset: 0x00009784
		public static string GetExchangeSessionId(string authToken)
		{
			return authToken.GetHashCode().ToString("X8");
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B5A4 File Offset: 0x000097A4
		public static ExchangePrincipal GetExchangePrincipal(WacRequest wacRequest, out ADSessionSettings adSessionSettings, bool isArchive)
		{
			return WacUtilities.GetExchangePrincipal(wacRequest.MailboxSmtpAddress.ToString(), out adSessionSettings, isArchive);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B5CC File Offset: 0x000097CC
		public static ExchangePrincipal GetExchangePrincipal(string smtpAddress, out ADSessionSettings adSessionSettings, bool isArchive)
		{
			adSessionSettings = Directory.SessionSettingsFromAddress(smtpAddress);
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromProxyAddress(adSessionSettings, ProxyAddressPrefix.Smtp.PrimaryPrefix + ":" + smtpAddress);
			if (exchangePrincipal == null)
			{
				throw new OwaADUserNotFoundException("PrimarySmtpAddress=" + smtpAddress);
			}
			if (isArchive)
			{
				return exchangePrincipal.GetArchiveExchangePrincipal();
			}
			return exchangePrincipal;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B620 File Offset: 0x00009820
		public static string GetOwnerIdForMailbox(string ewsAttachmentId)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsAttachmentId, BasicTypes.Attachment, null);
			return "ExchangeMbx_" + idHeaderInformation.MailboxId.MailboxGuid;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B64C File Offset: 0x0000984C
		public static Stream GetStreamForProtectedAttachment(StreamAttachment attachment, IExchangePrincipal exchangePrincipal)
		{
			UseLicenseAndUsageRights useLicenseAndUsageRights;
			bool flag;
			return StreamAttachment.OpenRestrictedAttachment(attachment, exchangePrincipal.MailboxInfo.OrganizationId, exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), exchangePrincipal.Sid, exchangePrincipal.RecipientTypeDetails, out useLicenseAndUsageRights, out flag);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B694 File Offset: 0x00009894
		public static void WriteStreamBody(HttpResponse response, Stream contentStream)
		{
			Stream outputStream = response.OutputStream;
			WacUtilities.WriteStreamBody(outputStream, contentStream);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public static void WriteStreamBody(Stream responseStream, Stream contentStream)
		{
			byte[] buffer = new byte[65536];
			contentStream.Position = 0L;
			int num;
			do
			{
				num = contentStream.Read(buffer, 0, 65536);
				responseStream.Write(buffer, 0, num);
			}
			while (num > 0);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public static bool IsIrmRestricted(Item item)
		{
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			return rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B710 File Offset: 0x00009910
		public static bool ShouldUpdateAttachment(string mailboxSmtpAddress, string ewsAttachmentId, out CobaltStoreSaver saver)
		{
			string cacheKey = CachedAttachmentInfo.GetCacheKey(mailboxSmtpAddress, ewsAttachmentId);
			CachedAttachmentInfo fromCache = CachedAttachmentInfo.GetFromCache(cacheKey);
			if (fromCache == null || fromCache.CobaltStore == null)
			{
				saver = null;
				return false;
			}
			saver = fromCache.CobaltStore.Saver;
			return saver != null;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B750 File Offset: 0x00009950
		public static void ProcessAttachment(IStoreSession session, string ewsAttachmentId, IExchangePrincipal exchangePrincipal, PropertyOpenMode openMode, WacUtilities.AttachmentProcessor attachmentProcessor)
		{
			IdConverterDependencies converterDependencies = new IdConverterDependencies.FromRawData(false, false, null, null, exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), session as MailboxSession, session as MailboxSession, session as PublicFolderSession);
			using (AttachmentHandler.IAttachmentRetriever attachmentRetriever = AttachmentRetriever.CreateInstance(ewsAttachmentId, converterDependencies))
			{
				bool flag = WacUtilities.IsIrmRestricted(attachmentRetriever.RootItem);
				if (openMode == PropertyOpenMode.Modify)
				{
					attachmentRetriever.RootItem.OpenAsReadWrite();
				}
				StreamAttachment streamAttachment = attachmentRetriever.Attachment as StreamAttachment;
				if (streamAttachment == null)
				{
					attachmentProcessor(exchangePrincipal, attachmentRetriever.Attachment, null, flag);
				}
				else
				{
					using (Stream contentStream = streamAttachment.GetContentStream(openMode))
					{
						bool flag2 = WacUtilities.IsContentProtected(attachmentRetriever.Attachment.FileName, contentStream);
						attachmentProcessor(exchangePrincipal, streamAttachment, contentStream, flag || flag2);
						if (openMode == PropertyOpenMode.Modify)
						{
							attachmentRetriever.RootItem.Save(SaveMode.NoConflictResolution);
						}
					}
				}
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B850 File Offset: 0x00009A50
		private static bool TryProcessUnprotectedAttachment(IExchangePrincipal exchangePrincipal, Item item, StreamAttachment streamAttachment, PropertyOpenMode openMode, WacUtilities.AttachmentProcessor attachmentProcessor)
		{
			if (openMode == PropertyOpenMode.Modify)
			{
				item.OpenAsReadWrite();
			}
			bool result;
			using (Stream contentStream = streamAttachment.GetContentStream(openMode))
			{
				bool flag = WacUtilities.IsContentProtected(streamAttachment.FileName, contentStream);
				if (flag)
				{
					result = false;
				}
				else
				{
					attachmentProcessor(exchangePrincipal, streamAttachment, contentStream, false);
					if (openMode == PropertyOpenMode.Modify)
					{
						item.Save(SaveMode.NoConflictResolution);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B8BC File Offset: 0x00009ABC
		internal static StoreObjectId GetStoreObjectId(string ewsAttachmentId)
		{
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsAttachmentId, BasicTypes.Attachment, attachmentIds);
			return idHeaderInformation.ToStoreObjectId();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		internal static string GenerateSHA256HashForStream(Stream stream)
		{
			string result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				stream.Position = 0L;
				byte[] inArray = sha256Cng.ComputeHash(stream);
				result = Convert.ToBase64String(inArray);
			}
			return result;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B928 File Offset: 0x00009B28
		internal static void SetEventId(RequestDetailsLogger logger, string eventId)
		{
			OwsLogRegistry.Register(eventId, typeof(WacRequestHandlerMetadata), new Type[0]);
			logger.ActivityScope.SetProperty(ExtensibleLoggerMetadata.EventId, eventId);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B954 File Offset: 0x00009B54
		internal static string GetCurrentTimeForFileName()
		{
			return ExDateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss.ffff");
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B9DC File Offset: 0x00009BDC
		internal static void ReplaceAttachmentContent(string smtpAddress, string cultureName, string ewsAttachmentID, bool isArchive, Stream source)
		{
			ADSessionSettings adsessionSettings;
			ExchangePrincipal exchangePrincipal = WacUtilities.GetExchangePrincipal(smtpAddress, out adsessionSettings, isArchive);
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(cultureName);
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsAttachmentID, BasicTypes.Attachment, attachmentIds);
			idHeaderInformation.ToStoreObjectId();
			using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(exchangePrincipal, cultureInfo, "Client=OWA;Action=WAC"))
			{
				WacUtilities.ProcessAttachment(mailboxSession, ewsAttachmentID, exchangePrincipal, PropertyOpenMode.Modify, delegate(IExchangePrincipal principal, Attachment attachment, Stream stream, bool anyContentProtected)
				{
					BinaryReader binaryReader = new BinaryReader(source);
					stream.Seek(0L, SeekOrigin.Begin);
					int num = 10000;
					int num2 = 0;
					byte[] buffer = new byte[num];
					for (;;)
					{
						int num3 = binaryReader.Read(buffer, 0, num);
						num2 += num3;
						if (num3 == 0)
						{
							break;
						}
						stream.Write(buffer, 0, num3);
					}
					stream.SetLength((long)num2);
					attachment.Save();
				});
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BA70 File Offset: 0x00009C70
		internal static bool ItemIsMessageDraft(Item item, Attachment attachment, out string referenceAttachmentWebServiceUrl, out string referenceAttachmentUrl)
		{
			ReferenceAttachment referenceAttachment = attachment as ReferenceAttachment;
			if (referenceAttachment != null)
			{
				referenceAttachmentWebServiceUrl = referenceAttachment.ProviderEndpointUrl;
				referenceAttachmentUrl = referenceAttachment.AttachLongPathName;
			}
			else
			{
				referenceAttachmentWebServiceUrl = null;
				referenceAttachmentUrl = null;
			}
			MessageItem messageItem = item as MessageItem;
			return messageItem != null && messageItem.IsDraft;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		internal static byte[] FromBase64String(string fileRepAsString)
		{
			byte[] result;
			try
			{
				result = Convert.FromBase64String(fileRepAsString);
			}
			catch (FormatException innerException)
			{
				throw new OwaInvalidRequestException("Cannot convert from base 64", innerException);
			}
			return result;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		private static void EnsureLocalServerNameAndVersion()
		{
			if (string.IsNullOrEmpty(WacUtilities.localServerName) || string.IsNullOrEmpty(WacUtilities.localServerVersion))
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 599, "EnsureLocalServerNameAndVersion", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\attachment\\WacUtilities.cs");
				Server server = topologyConfigurationSession.FindLocalServer();
				WacUtilities.localServerName = server.Fqdn;
				WacUtilities.localServerVersion = WacUtilities.ConvertVersionNumberToString(server.VersionNumber);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BB50 File Offset: 0x00009D50
		private static string ConvertVersionNumberToString(int versionNumber)
		{
			int value = versionNumber & 32767;
			int value2 = versionNumber >> 16 & 63;
			int value3 = versionNumber >> 22 & 63;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value3);
			stringBuilder.Append(".");
			stringBuilder.Append(value2);
			stringBuilder.Append(".");
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BBB1 File Offset: 0x00009DB1
		private static bool IsContentProtected(string attachmentName, Stream contentStream)
		{
			return ProtectorsManager.Instance.IsProtected(attachmentName, contentStream) == MsoIpiResult.Protected;
		}

		// Token: 0x04000156 RID: 342
		public const string WopiAccessToken = "access_token";

		// Token: 0x04000157 RID: 343
		public const string WopiClientVersion = "X-WOPI-InterfaceVersion";

		// Token: 0x04000158 RID: 344
		public const string WopiRequestMachineName = "X-WOPI-MachineName";

		// Token: 0x04000159 RID: 345
		public const string WopiCorrelationID = "X-WOPI-CorrelationID";

		// Token: 0x0400015A RID: 346
		public const string WopiPerfTraceRequested = "X-WOPI-PerfTraceRequested";

		// Token: 0x0400015B RID: 347
		public const string WopiServerMachineName = "X-WOPI-ServerMachineName";

		// Token: 0x0400015C RID: 348
		public const string WopiServerError = "X-WOPI-ServerError";

		// Token: 0x0400015D RID: 349
		public const string WopiServerVersion = "X-WOPI-ServerVersion";

		// Token: 0x0400015E RID: 350
		public const string WopiPerfTrace = "X-WOPI-PerfTrace";

		// Token: 0x0400015F RID: 351
		public const string WopiUIParameter = "ui";

		// Token: 0x04000160 RID: 352
		public const string MailboxSmtpAddress = "UserEmail";

		// Token: 0x04000161 RID: 353
		public const string FileNameSeparator = "-";

		// Token: 0x04000162 RID: 354
		private const string OwnerIdPrefix = "ExchangeMbx_";

		// Token: 0x04000163 RID: 355
		private static string localServerName;

		// Token: 0x04000164 RID: 356
		private static string localServerVersion;

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x06000304 RID: 772
		public delegate void AttachmentProcessor(IExchangePrincipal exchangePrincipal, Attachment attachment, Stream stream, bool contentProtected);
	}
}
