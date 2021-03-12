using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Security.RightsManagement.Protectors;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000329 RID: 809
	internal class GetWacAttachmentInfo : ServiceCommand<WacAttachmentType>
	{
		// Token: 0x06001ACD RID: 6861 RVA: 0x000655A3 File Offset: 0x000637A3
		public GetWacAttachmentInfo(CallContext callContext, string attachmentId, bool isEdit, string draftId) : base(callContext)
		{
			this.attachmentId = attachmentId;
			this.isEdit = isEdit;
			this.draftId = draftId;
			OwsLogRegistry.Register("GetWacAttachmentInfo", typeof(GetWacAttachmentInfoMetadata), new Type[0]);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000655DC File Offset: 0x000637DC
		protected override WacAttachmentType InternalExecute()
		{
			if (string.IsNullOrEmpty(this.attachmentId))
			{
				throw new OwaInvalidRequestException("You must provide an attachmentId when calling GetWacAttachmentInfo.");
			}
			WacAttachmentType result;
			using (AttachmentHandler.IAttachmentRetriever attachmentRetriever = AttachmentRetriever.CreateInstance(this.attachmentId, base.CallContext))
			{
				result = GetWacAttachmentInfo.Execute(base.CallContext, attachmentRetriever.RootItem.Session, attachmentRetriever.RootItem, attachmentRetriever.Attachment, this.draftId, this.attachmentId, this.isEdit);
			}
			return result;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0006569C File Offset: 0x0006389C
		public static WacAttachmentType Execute(CallContext callContext, IStoreSession originalAttachmentSession, IItem originalAttachmentItem, IAttachment originalAttachment, string draftId, string ewsAttachmentId, bool isEdit)
		{
			MdbCache.GetInstance().BeginAsyncUpdate();
			UserContext userContext = UserContextManager.GetUserContext(callContext.HttpContext, callContext.EffectiveCaller, true);
			if (userContext == null)
			{
				throw new OwaInvalidRequestException("Unable to determine user context.");
			}
			if (!userContext.IsWacEditingEnabled)
			{
				isEdit = false;
			}
			ConfigurationContext configurationContext = new ConfigurationContext(userContext);
			AttachmentPolicy attachmentPolicy = configurationContext.AttachmentPolicy;
			bool isPublicLogon = userContext.IsPublicLogon;
			if (!attachmentPolicy.GetWacViewingEnabled(isPublicLogon))
			{
				throw new OwaOperationNotSupportedException("WAC viewing not enabled for the current user");
			}
			MailboxSession mailboxSession = null;
			StoreObjectId draftObjectId = null;
			if (draftId != null)
			{
				IdAndSession idAndSession = GetWacAttachmentInfo.GetIdAndSession(callContext, draftId, false);
				mailboxSession = (idAndSession.Session as MailboxSession);
				draftObjectId = StoreId.EwsIdToStoreObjectId(draftId);
				if (mailboxSession == null)
				{
					throw new OwaOperationNotSupportedException("We need a MailboxSession to create the draft, but this a " + idAndSession.Session.GetType().Name);
				}
			}
			string text = originalAttachmentSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			string primarySmtpAddress = callContext.EffectiveCaller.PrimarySmtpAddress;
			RequestDetailsLogger protocolLog = callContext.ProtocolLog;
			protocolLog.Set(GetWacAttachmentInfoMetadata.LogonSmtpAddress, primarySmtpAddress);
			protocolLog.Set(GetWacAttachmentInfoMetadata.MailboxSmtpAddress, text);
			protocolLog.Set(GetWacAttachmentInfoMetadata.Edit, isEdit);
			protocolLog.Set(GetWacAttachmentInfoMetadata.Extension, originalAttachment.FileExtension);
			protocolLog.Set(GetWacAttachmentInfoMetadata.DraftProvided, draftId != null);
			string displayName = callContext.AccessingPrincipal.MailboxInfo.DisplayName;
			XSOFactory factory = new XSOFactory();
			AttachmentDataProvider defaultUploadDataProvider = new AttachmentDataProviderManager().GetDefaultUploadDataProvider(callContext);
			IReferenceAttachment referenceAttachment = originalAttachment as IReferenceAttachment;
			if (referenceAttachment != null)
			{
				GetWacAttachmentInfo.LogReferenceAttachmentProperties(protocolLog, referenceAttachment.ProviderEndpointUrl, GetWacAttachmentInfoMetadata.OriginalReferenceAttachmentServiceUrl, referenceAttachment.AttachLongPathName, GetWacAttachmentInfoMetadata.OriginalReferenceAttachmentUrl);
			}
			if (defaultUploadDataProvider != null)
			{
				protocolLog.Set(GetWacAttachmentInfoMetadata.AttachmentDataProvider, defaultUploadDataProvider.ToString());
			}
			WacAttachmentType wacAttachmentType;
			try
			{
				using (GetWacAttachmentInfo.Implementation implementation = new GetWacAttachmentInfo.Implementation(defaultUploadDataProvider, factory, originalAttachmentSession, originalAttachmentSession.MailboxOwner.ModernGroupType, originalAttachmentItem, originalAttachment, ewsAttachmentId, mailboxSession, draftObjectId, isEdit, displayName, (IStoreSession session, StoreId itemId, AttachmentId attachmentId) => new IdAndSession(itemId, (StoreSession)session)
				{
					AttachmentIds = 
					{
						attachmentId
					}
				}.GetConcatenatedId().Id))
				{
					implementation.Execute();
					protocolLog.Set(GetWacAttachmentInfoMetadata.OriginalAttachmentType, implementation.OriginalAttachmentType);
					protocolLog.Set(GetWacAttachmentInfoMetadata.ResultAttachmentType, implementation.ResultAttachmentType);
					protocolLog.Set(GetWacAttachmentInfoMetadata.ResultAttachmentCreation, implementation.ResultAttachmentCreation);
					if (implementation.ResultAttachmentType == AttachmentType.Reference)
					{
						IMailboxInfo mailboxInfo = originalAttachmentSession.MailboxOwner.MailboxInfo;
						string mailboxAddress = mailboxInfo.PrimarySmtpAddress.ToString();
						StoreId id = originalAttachmentItem.Id;
						BaseItemId itemIdFromStoreId = IdConverter.GetItemIdFromStoreId(id, new MailboxId(mailboxInfo.MailboxGuid));
						string exchangeSessionId = WacUtilities.GetExchangeSessionId(default(Guid).ToString());
						protocolLog.Set(GetWacAttachmentInfoMetadata.ExchangeSessionId, exchangeSessionId);
						wacAttachmentType = GetWacAttachmentInfo.GetResultForReferenceAttachment(callContext, userContext, implementation, defaultUploadDataProvider, mailboxAddress, itemIdFromStoreId, originalAttachment.FileName, isEdit, displayName, exchangeSessionId, protocolLog);
					}
					else
					{
						if (implementation.ResultAttachmentType != AttachmentType.Stream)
						{
							throw new OwaNotSupportedException("Unsupported attachment type " + implementation.ResultAttachmentType);
						}
						AttachmentIdType ewsAttachmentIdType = GetWacAttachmentInfo.GetEwsAttachmentIdType(callContext, implementation.ResultItemId, implementation.ResultAttachmentId);
						wacAttachmentType = GetWacAttachmentInfo.GetResultForStreamAttachment(callContext, userContext, configurationContext, attachmentPolicy, isPublicLogon, Thread.CurrentThread.CurrentCulture.Name, isEdit, (IStreamAttachment)implementation.ResultAttachment, implementation.ResultAttachmentExtension, ewsAttachmentIdType, implementation.ResultIsInDraft, implementation.ResultStoreSession, text, originalAttachmentSession.MailboxOwner.MailboxInfo.IsArchive);
					}
				}
			}
			catch (ServerException exception)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception, WacAttachmentStatus.UploadFailed);
			}
			catch (GetWacAttachmentInfo.AttachmentUploadException exception2)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception2, WacAttachmentStatus.UploadFailed);
			}
			catch (RightsManagementPermanentException exception3)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception3, WacAttachmentStatus.ProtectedByUnsupportedIrm);
			}
			catch (AttachmentProtectionException exception4)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception4, WacAttachmentStatus.ProtectedByUnsupportedIrm);
			}
			catch (ObjectNotFoundException exception5)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception5, WacAttachmentStatus.NotFound);
			}
			catch (OwaInvalidRequestException exception6)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception6, WacAttachmentStatus.InvalidRequest);
			}
			catch (WacDiscoveryFailureException exception7)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception7, WacAttachmentStatus.WacDiscoveryFailed);
			}
			catch (WebException exception8)
			{
				wacAttachmentType = GetWacAttachmentInfo.HandleException(protocolLog, isEdit, exception8, WacAttachmentStatus.AttachmentDataProviderError);
			}
			if (wacAttachmentType == null)
			{
				throw new OwaInvalidOperationException("There is no reason known for code to reach here without throwing an unhandled exception elsewhere");
			}
			protocolLog.Set(GetWacAttachmentInfoMetadata.Status, wacAttachmentType.Status.ToString());
			return wacAttachmentType;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00065BDC File Offset: 0x00063DDC
		private static WacAttachmentType HandleException(RequestDetailsLogger logger, bool isEdit, Exception exception, WacAttachmentStatus status)
		{
			logger.Set(GetWacAttachmentInfoMetadata.HandledException, exception);
			return new WacAttachmentType
			{
				AttachmentId = null,
				IsEdit = isEdit,
				IsInDraft = false,
				WacUrl = string.Empty,
				Status = status
			};
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00065C27 File Offset: 0x00063E27
		private static void LogReferenceAttachmentProperties(RequestDetailsLogger logger, string webServiceUrl, Enum webServiceUrlKey, string contentUrl, Enum contentUrlKey)
		{
			if (string.IsNullOrEmpty(webServiceUrl))
			{
				webServiceUrl = "(none)";
			}
			logger.Set(webServiceUrlKey, webServiceUrl);
			if (string.IsNullOrEmpty(contentUrl))
			{
				contentUrl = "(none)";
			}
			logger.Set(contentUrlKey, contentUrl);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00065C5C File Offset: 0x00063E5C
		private static void PostUploadMessage(string groupAddress, string userAddress, string userDisplayName, BaseItemId referenceItemId, string fileName, string contentUrl, string providerType, string endpointUrl, string sessionId)
		{
			BodyContentType bodyContentType = new BodyContentType();
			bodyContentType.Value = string.Format(Strings.ModernGroupAttachmentUploadNoticeBody, fileName, userDisplayName);
			ReferenceAttachmentType referenceAttachmentType = new ReferenceAttachmentType();
			referenceAttachmentType.AttachLongPathName = contentUrl;
			referenceAttachmentType.ProviderEndpointUrl = endpointUrl;
			referenceAttachmentType.ProviderType = providerType;
			referenceAttachmentType.Name = fileName;
			ReplyToItemType replyToItemType = new ReplyToItemType();
			replyToItemType.NewBodyContent = bodyContentType;
			replyToItemType.Attachments = new AttachmentType[1];
			replyToItemType.Attachments[0] = referenceAttachmentType;
			replyToItemType.ReferenceItemId = referenceItemId;
			PostModernGroupItemJsonRequest postModernGroupItemJsonRequest = new PostModernGroupItemJsonRequest();
			postModernGroupItemJsonRequest.Body = new PostModernGroupItemRequest();
			postModernGroupItemJsonRequest.Body.Items = new NonEmptyArrayOfAllItemsType();
			postModernGroupItemJsonRequest.Body.Items.Add(replyToItemType);
			postModernGroupItemJsonRequest.Body.ModernGroupEmailAddress = new EmailAddressWrapper();
			postModernGroupItemJsonRequest.Body.ModernGroupEmailAddress.EmailAddress = groupAddress;
			postModernGroupItemJsonRequest.Body.ModernGroupEmailAddress.MailboxType = MailboxHelper.MailboxTypeType.GroupMailbox.ToString();
			OWAService owaservice = new OWAService();
			GetWacAttachmentInfo.PostUploadMessageAsyncState postUploadMessageAsyncState = new GetWacAttachmentInfo.PostUploadMessageAsyncState();
			postUploadMessageAsyncState.MailboxSmtpAddress = groupAddress;
			postUploadMessageAsyncState.LogonSmtpAddress = userAddress;
			postUploadMessageAsyncState.OwaService = owaservice;
			postUploadMessageAsyncState.SessionId = sessionId;
			IAsyncResult asyncResult = owaservice.BeginPostModernGroupItem(postModernGroupItemJsonRequest, null, null);
			asyncResult.AsyncWaitHandle.WaitOne();
			PostModernGroupItemResponse body = owaservice.EndPostModernGroupItem(asyncResult).Body;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00065D98 File Offset: 0x00063F98
		private static IdAndSession GetIdAndSession(CallContext callContext, string id, bool isAttachmentId)
		{
			IdAndSession idAndSession;
			try
			{
				if (isAttachmentId)
				{
					idAndSession = new IdConverter(callContext).ConvertAttachmentIdToIdAndSessionReadOnly(id);
				}
				else
				{
					idAndSession = new IdConverter(callContext).ConvertItemIdToIdAndSessionReadOnly(id);
				}
				if (idAndSession == null)
				{
					List<AttachmentId> attachmentIds = new List<AttachmentId>();
					IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(id, BasicTypes.Attachment, attachmentIds, false);
					throw new OwaInvalidRequestException("Unsupported Attachment ID. Storage type: " + idHeaderInformation.IdStorageType);
				}
			}
			catch (InvalidStoreIdException)
			{
				throw new OwaInvalidRequestException("Invalid attachment ID: " + id);
			}
			catch (AccessDeniedException)
			{
				throw new OwaInvalidRequestException("You do not have permission to access this mailbox.");
			}
			if (idAndSession.Session == null)
			{
				throw new OwaInvalidRequestException("The mailbox was not specified.");
			}
			return idAndSession;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00065E44 File Offset: 0x00064044
		internal static AttachmentIdType GetEwsAttachmentIdType(CallContext callContext, VersionedId itemId, string ewsAttachmentId)
		{
			IdAndSession idAndSession = GetWacAttachmentInfo.GetIdAndSession(callContext, ewsAttachmentId, true);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(itemId, idAndSession, null);
			return new AttachmentIdType
			{
				Id = ewsAttachmentId,
				RootItemId = concatenatedId.Id
			};
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00065E80 File Offset: 0x00064080
		private static WacAttachmentType GetResultForReferenceAttachment(CallContext callContext, UserContext userContext, GetWacAttachmentInfo.Implementation implementation, AttachmentDataProvider provider, string mailboxAddress, BaseItemId referenceItemId, string fileName, bool isEdit, string userDisplayName, string sessionId, RequestDetailsLogger logger)
		{
			GetWacAttachmentInfo.LogReferenceAttachmentProperties(logger, implementation.ResultAttachmentWebServiceUrl, GetWacAttachmentInfoMetadata.ResultReferenceAttachmentServiceUrl, implementation.ResultAttachmentContentUrl, GetWacAttachmentInfoMetadata.ResultReferenceAttachmentUrl);
			AttachmentIdType ewsAttachmentIdType = GetWacAttachmentInfo.GetEwsAttachmentIdType(callContext, implementation.ResultItemId, implementation.ResultAttachmentId);
			WacAttachmentType result = GetWacAttachmentInfo.CreateWacAttachmentType(userContext.LogonIdentity, ewsAttachmentIdType, implementation.ResultAttachmentWebServiceUrl, implementation.ResultAttachmentContentUrl, isEdit, implementation.ResultIsInDraft);
			if (implementation.ResultAttachmentCreation == WacAttachmentCreationType.Upload)
			{
				try
				{
					GetWacAttachmentInfo.PostUploadMessage(mailboxAddress, userContext.LogonIdentity.PrimarySmtpAddress.ToString(), userDisplayName, referenceItemId, fileName, implementation.ResultAttachmentContentUrl, implementation.ResultAttachmentProviderType, implementation.ResultAttachmentWebServiceUrl, sessionId);
				}
				catch (Exception value)
				{
					logger.Set(GetWacAttachmentInfoMetadata.HandledException, value);
				}
			}
			return result;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00065F4C File Offset: 0x0006414C
		private static WacAttachmentType CreateWacAttachmentType(OwaIdentity identity, AttachmentIdType attachmentIdType, string webServiceUrl, string contentUrl, bool isEdit, bool isInDraft)
		{
			string wacUrl;
			try
			{
				wacUrl = OneDriveProUtilities.GetWacUrl(identity, webServiceUrl, contentUrl, isEdit);
			}
			catch (Exception ex)
			{
				ex.ToString();
				throw;
			}
			return new WacAttachmentType
			{
				AttachmentId = attachmentIdType,
				IsEdit = isEdit,
				IsInDraft = isInDraft,
				WacUrl = wacUrl,
				Status = WacAttachmentStatus.Success
			};
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00065FAC File Offset: 0x000641AC
		private static WacAttachmentType GetResultForStreamAttachment(CallContext callContext, UserContext userContext, ConfigurationContext configurationContext, AttachmentPolicy attachmentPolicy, bool isPublicLogon, string cultureName, bool isEdit, IStreamAttachment attachment, string attachmentExtension, AttachmentIdType attachmentIdType, bool isInDraft, IStoreSession storeSession, string mailboxSmtpAddress, bool isArchive)
		{
			WacFileRep wacFileRep = GetWacAttachmentInfo.CreateWacFileRep(callContext, configurationContext, attachmentPolicy, isPublicLogon, isEdit, isArchive);
			HttpRequest request = callContext.HttpContext.Request;
			string text;
			string arg;
			GetWacAttachmentInfo.GenerateWopiSrcUrl(request, wacFileRep, mailboxSmtpAddress, out text, out arg);
			if (text == null)
			{
				throw new OwaInvalidOperationException("WOPI URL is null.");
			}
			string id = attachmentIdType.Id;
			TokenResult oauthToken = GetWacAttachmentInfo.GetOAuthToken(id, userContext, mailboxSmtpAddress, text);
			string exchangeSessionId = WacUtilities.GetExchangeSessionId(oauthToken.TokenString);
			callContext.ProtocolLog.Set(GetWacAttachmentInfoMetadata.ExchangeSessionId, exchangeSessionId);
			SecurityIdentifier effectiveCallerSid = callContext.EffectiveCallerSid;
			CachedAttachmentInfo.GetInstance(mailboxSmtpAddress, id, exchangeSessionId, effectiveCallerSid, cultureName);
			string wacUrl = GetWacAttachmentInfo.GetWacUrl(isEdit, cultureName, attachmentExtension);
			if (string.IsNullOrEmpty(wacUrl))
			{
				throw new OwaInvalidRequestException(string.Format("Wac Base Url is null for this given extension {0} and culture {1}", attachmentExtension, cultureName));
			}
			new Uri(wacUrl);
			string format = "{0}WOPISrc={1}&access_token={2}";
			string arg2 = HttpUtility.UrlEncode(oauthToken.TokenString);
			string text2 = string.Format(format, wacUrl, HttpUtility.UrlEncode(text), arg2);
			string value = string.Format(format, wacUrl, arg, arg2);
			callContext.ProtocolLog.Set(GetWacAttachmentInfoMetadata.WacUrl, value);
			if (!Uri.IsWellFormedUriString(text2, UriKind.Absolute))
			{
				throw new OwaInvalidOperationException("The WAC Iframe URL that was generated is not a well formed URI: " + text2);
			}
			return new WacAttachmentType
			{
				AttachmentId = attachmentIdType,
				IsEdit = isEdit,
				IsInDraft = isInDraft,
				WacUrl = text2,
				Status = WacAttachmentStatus.Success
			};
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00066110 File Offset: 0x00064310
		private static WacFileRep CreateWacFileRep(CallContext callContext, ConfigurationContext configurationContext, AttachmentPolicy attachmentPolicy, bool isPublicLogon, bool isEdit, bool isArchive)
		{
			bool directFileAccessEnabled = attachmentPolicy.GetDirectFileAccessEnabled(isPublicLogon);
			bool externalServicesEnabled = configurationContext.IsFeatureEnabled(Feature.WacExternalServicesEnabled);
			bool wacOMEXEnabled = configurationContext.IsFeatureEnabled(Feature.WacOMEXEnabled);
			return new WacFileRep(callContext.EffectiveCallerSid, directFileAccessEnabled, externalServicesEnabled, wacOMEXEnabled, isEdit, isArchive);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0006615C File Offset: 0x0006435C
		private static TokenResult GetOAuthToken(string ewsAttachmentId, UserContext userContext, string mailboxSmtpAddress, string wopiSrcUrl)
		{
			LocalTokenIssuer localTokenIssuer = new LocalTokenIssuer(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			TokenResult wacCallbackToken = localTokenIssuer.GetWacCallbackToken(new Uri(wopiSrcUrl, UriKind.Absolute), mailboxSmtpAddress, ewsAttachmentId);
			if (wacCallbackToken == null)
			{
				throw new InvalidOperationException("OAuth TokenResult is null.");
			}
			return wacCallbackToken;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000661A0 File Offset: 0x000643A0
		private static void GenerateWopiSrcUrl(HttpRequest request, WacFileRep wacFileRep, string emailAddress, out string wopiSrcUrl, out string wopiSrcUrlForLogging)
		{
			string text = string.Format("owa/{0}/wopi/files/@/owaatt", HttpUtility.UrlEncode(emailAddress));
			string text2 = string.Format("owa/{0}/wopi/files/@/owaatt", ExtensibleLogger.FormatPIIValue(emailAddress));
			Uri requestUrlEvenIfProxied = request.GetRequestUrlEvenIfProxied();
			wopiSrcUrl = string.Format("{0}://{1}:{2}/{3}?{4}={5}", new object[]
			{
				requestUrlEvenIfProxied.Scheme,
				requestUrlEvenIfProxied.Host,
				requestUrlEvenIfProxied.Port,
				text,
				"owaatt",
				HttpUtility.UrlEncode(wacFileRep.Serialize())
			});
			wopiSrcUrlForLogging = string.Format("{0}://{1}:{2}/{3}?{4}={5}", new object[]
			{
				requestUrlEvenIfProxied.Scheme,
				requestUrlEvenIfProxied.Host,
				requestUrlEvenIfProxied.Port,
				text2,
				"owaatt",
				HttpUtility.UrlEncode(wacFileRep.Serialize())
			});
			if (Globals.IsPreCheckinApp && request.Cookies["X-DFPOWA-Vdir"] != null)
			{
				wopiSrcUrl = string.Format("{0}&vdir={1}", wopiSrcUrl, request.Cookies["X-DFPOWA-Vdir"].Value);
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000662B8 File Offset: 0x000644B8
		private static string GetWacUrl(bool isEdit, string cultureName, string fileExtension)
		{
			string result = null;
			if (WacDiscoveryManager.Instance.WacDiscoveryResult != null)
			{
				if (isEdit)
				{
					WacDiscoveryManager.Instance.WacDiscoveryResult.TryGetEditUrlForFileExtension(fileExtension, cultureName, out result);
				}
				else
				{
					WacDiscoveryManager.Instance.WacDiscoveryResult.TryGetViewUrlForFileExtension(fileExtension, cultureName, out result);
				}
				return result;
			}
			throw new OwaInvalidOperationException("WacDiscoveryResult cannot be null, if it is, we have a bug in our code");
		}

		// Token: 0x04000EDC RID: 3804
		private const string AttachmentTypeReference = "Reference";

		// Token: 0x04000EDD RID: 3805
		private const string AttachmentTypeStream = "Stream";

		// Token: 0x04000EDE RID: 3806
		private const string ActionName = "GetWacAttachmentInfo";

		// Token: 0x04000EDF RID: 3807
		private const string DFPOWAVdirCookie = "X-DFPOWA-Vdir";

		// Token: 0x04000EE0 RID: 3808
		private readonly string attachmentId;

		// Token: 0x04000EE1 RID: 3809
		private readonly bool isEdit;

		// Token: 0x04000EE2 RID: 3810
		private readonly string draftId;

		// Token: 0x0200032A RID: 810
		public class Implementation : DisposeTrackableBase
		{
			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0006630E File Offset: 0x0006450E
			// (set) Token: 0x06001ADE RID: 6878 RVA: 0x00066316 File Offset: 0x00064516
			public IStoreSession ResultStoreSession { get; private set; }

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x06001ADF RID: 6879 RVA: 0x0006631F File Offset: 0x0006451F
			// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x00066327 File Offset: 0x00064527
			public IItem ResultItem { get; private set; }

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00066330 File Offset: 0x00064530
			// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x00066338 File Offset: 0x00064538
			public VersionedId ResultItemId { get; private set; }

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00066341 File Offset: 0x00064541
			// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x00066349 File Offset: 0x00064549
			public IAttachment ResultAttachment { get; private set; }

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00066352 File Offset: 0x00064552
			// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x0006635A File Offset: 0x0006455A
			public WacAttachmentCreationType ResultAttachmentCreation { get; private set; }

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00066363 File Offset: 0x00064563
			// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x0006636B File Offset: 0x0006456B
			public string ResultAttachmentId { get; private set; }

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00066374 File Offset: 0x00064574
			// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0006637C File Offset: 0x0006457C
			public string ResultAttachmentContentUrl { get; private set; }

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00066385 File Offset: 0x00064585
			// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0006638D File Offset: 0x0006458D
			public string ResultAttachmentWebServiceUrl { get; private set; }

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06001AED RID: 6893 RVA: 0x00066396 File Offset: 0x00064596
			// (set) Token: 0x06001AEE RID: 6894 RVA: 0x0006639E File Offset: 0x0006459E
			public string ResultAttachmentProviderType { get; private set; }

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000663A7 File Offset: 0x000645A7
			// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x000663AF File Offset: 0x000645AF
			public AttachmentType OriginalAttachmentType { get; private set; }

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000663B8 File Offset: 0x000645B8
			// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x000663C0 File Offset: 0x000645C0
			public AttachmentType ResultAttachmentType { get; private set; }

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000663C9 File Offset: 0x000645C9
			// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x000663D1 File Offset: 0x000645D1
			public bool ResultIsInDraft { get; private set; }

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000663DA File Offset: 0x000645DA
			// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x000663E2 File Offset: 0x000645E2
			public string ResultAttachmentExtension { get; private set; }

			// Token: 0x06001AF7 RID: 6903 RVA: 0x000663EC File Offset: 0x000645EC
			public Implementation(AttachmentDataProvider provider, IXSOFactory factory, IStoreSession attachmentSession, ModernGroupObjectType attachmentSessionModernGroupType, IItem rootItem, IAttachment attachment, string attachmentId, IMailboxSession draftAttachmentSession, StoreObjectId draftObjectId, bool isEdit, string userDisplayName, Func<IStoreSession, StoreId, AttachmentId, string> idConverter)
			{
				this.provider = provider;
				this.factory = factory;
				this.originalAttachmentSession = attachmentSession;
				this.originalSessionModernGroupType = attachmentSessionModernGroupType;
				this.originalAttachmentRootItem = rootItem;
				this.originalAttachment = attachment;
				this.originalAttachmentId = attachmentId;
				this.draftAttachmentSession = draftAttachmentSession;
				this.draftObjectId = draftObjectId;
				this.isEdit = isEdit;
				this.userDisplayName = userDisplayName;
				this.idConverter = idConverter;
			}

			// Token: 0x06001AF8 RID: 6904 RVA: 0x0006645C File Offset: 0x0006465C
			protected override void InternalDispose(bool isDisposing)
			{
				if (isDisposing)
				{
					if (this.ResultAttachment != null)
					{
						this.ResultAttachment.Dispose();
						this.ResultAttachment = null;
					}
					if (this.draftItem != null)
					{
						this.draftItem.Dispose();
						this.draftItem = null;
					}
				}
			}

			// Token: 0x06001AF9 RID: 6905 RVA: 0x00066495 File Offset: 0x00064695
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<GetWacAttachmentInfo.Implementation>(this);
			}

			// Token: 0x06001AFA RID: 6906 RVA: 0x000664A0 File Offset: 0x000646A0
			public void Execute()
			{
				if (this.originalAttachment is IReferenceAttachment)
				{
					this.OriginalAttachmentType = AttachmentType.Reference;
				}
				else
				{
					if (!(this.originalAttachment is IStreamAttachment))
					{
						throw new OwaOperationNotSupportedException("Only StreamAttachment and ReferenceAttachment are supported.");
					}
					this.OriginalAttachmentType = AttachmentType.Stream;
				}
				if (this.isEdit)
				{
					if (this.draftObjectId != null)
					{
						this.draftItem = this.factory.BindToItem(this.draftAttachmentSession, this.draftObjectId, new PropertyDefinition[0]);
						this.ThrowIfNotEditable(this.draftItem);
						this.draftItem.OpenAsReadWrite();
						this.ResultStoreSession = this.draftAttachmentSession;
						this.ResultItem = this.draftItem;
						this.ResultItemId = this.ResultItem.Id;
						IAttachment resultAttachment;
						AttachmentId arg;
						if (this.ShouldUploadAttachment())
						{
							this.ResultAttachmentType = AttachmentType.Reference;
							this.ResultAttachmentCreation = WacAttachmentCreationType.Upload;
							this.UploadAttachment(this.originalAttachment, this.draftItem, out resultAttachment, out arg);
						}
						else
						{
							this.ResultAttachmentType = this.OriginalAttachmentType;
							this.ResultAttachmentCreation = WacAttachmentCreationType.Copy;
							this.CopyAttachment(this.originalAttachment, this.draftItem, out resultAttachment, out arg);
						}
						this.ResultAttachment = resultAttachment;
						this.ResultAttachmentId = this.idConverter(this.ResultStoreSession, this.ResultItemId, arg);
					}
					else
					{
						this.ThrowIfNotEditable(this.originalAttachmentRootItem);
						this.SetResultPropertiesFromOriginalAttachment();
					}
					this.ResultIsInDraft = true;
					return;
				}
				this.SetResultPropertiesFromOriginalAttachment();
				this.ResultIsInDraft = this.IsEditable(this.ResultItem);
			}

			// Token: 0x06001AFB RID: 6907 RVA: 0x00066610 File Offset: 0x00064810
			private void SetResultPropertiesFromOriginalAttachment()
			{
				this.ResultAttachmentType = this.OriginalAttachmentType;
				this.ResultAttachmentCreation = WacAttachmentCreationType.UseExisting;
				this.ResultStoreSession = this.originalAttachmentSession;
				this.ResultItem = this.originalAttachmentRootItem;
				this.ResultItemId = this.ResultItem.Id;
				this.ResultAttachment = this.originalAttachment;
				this.ResultAttachmentId = this.originalAttachmentId;
				this.ResultAttachmentExtension = this.originalAttachment.FileExtension;
				IReferenceAttachment referenceAttachment = this.originalAttachment as IReferenceAttachment;
				if (referenceAttachment != null)
				{
					this.ResultAttachmentContentUrl = referenceAttachment.AttachLongPathName;
					this.ResultAttachmentWebServiceUrl = referenceAttachment.ProviderEndpointUrl;
					this.ResultAttachmentProviderType = referenceAttachment.ProviderType;
				}
			}

			// Token: 0x06001AFC RID: 6908 RVA: 0x000666B5 File Offset: 0x000648B5
			private void ThrowIfNotEditable(IItem item)
			{
				if (!this.IsEditable(item))
				{
					throw new OwaInvalidOperationException("The given item should be a draft or task. It is a " + item.GetType().Name);
				}
			}

			// Token: 0x06001AFD RID: 6909 RVA: 0x000666DC File Offset: 0x000648DC
			private bool IsEditable(IItem item)
			{
				if (item is Task)
				{
					return true;
				}
				IMessageItem messageItem = item as IMessageItem;
				return messageItem != null && messageItem.IsDraft;
			}

			// Token: 0x06001AFE RID: 6910 RVA: 0x00066708 File Offset: 0x00064908
			private bool ShouldUploadAttachment()
			{
				bool flag = this.originalSessionModernGroupType != ModernGroupObjectType.None;
				bool flag2 = this.OriginalAttachmentType == AttachmentType.Stream;
				return flag2 && this.isEdit && flag;
			}

			// Token: 0x06001AFF RID: 6911 RVA: 0x0006673C File Offset: 0x0006493C
			private void UploadAttachment(IAttachment attachment, IItem draftItem, out IAttachment newAttachment, out AttachmentId newAttachmentId)
			{
				IStreamAttachment streamAttachment = attachment as IStreamAttachment;
				if (streamAttachment == null)
				{
					throw new InvalidOperationException("UploadAttachment requires a stream attachment, but was given a " + attachment.GetType().Name);
				}
				UploadItemAsyncResult uploadItemAsyncResult;
				using (Stream contentStream = streamAttachment.GetContentStream())
				{
					byte[] array = new byte[contentStream.Length];
					while (contentStream.Position != contentStream.Length)
					{
						int count = (int)Math.Min(4000L, contentStream.Length - contentStream.Position);
						contentStream.Read(array, (int)contentStream.Position, count);
					}
					CancellationToken cancellationToken = default(CancellationToken);
					uploadItemAsyncResult = this.provider.UploadItemSync(array, attachment.FileName, cancellationToken);
				}
				if (uploadItemAsyncResult.ResultCode != AttachmentResultCode.Success)
				{
					throw new GetWacAttachmentInfo.AttachmentUploadException(uploadItemAsyncResult.ResultCode.ToString());
				}
				UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
				this.ResultAttachmentWebServiceUrl = uploadItemAsyncResult.Item.ProviderEndpointUrl;
				this.ResultAttachmentProviderType = uploadItemAsyncResult.Item.ProviderType.ToString();
				this.ResultAttachmentContentUrl = this.provider.GetItemAbsoulteUrl(userContext, uploadItemAsyncResult.Item.Location, uploadItemAsyncResult.Item.ProviderEndpointUrl, null, null);
				this.ResultAttachmentExtension = attachment.FileExtension;
				IReferenceAttachment referenceAttachment = (IReferenceAttachment)this.factory.CreateAttachment(draftItem, AttachmentType.Reference);
				newAttachment = referenceAttachment;
				newAttachmentId = newAttachment.Id;
				referenceAttachment.AttachLongPathName = this.ResultAttachmentContentUrl;
				referenceAttachment.ProviderEndpointUrl = this.ResultAttachmentWebServiceUrl;
				referenceAttachment.ProviderType = this.ResultAttachmentProviderType;
				referenceAttachment.FileName = attachment.FileName;
				newAttachment.IsInline = false;
				newAttachment.Save();
				draftItem.Save(SaveMode.NoConflictResolutionForceSave);
			}

			// Token: 0x06001B00 RID: 6912 RVA: 0x00066904 File Offset: 0x00064B04
			private void CopyAttachment(IAttachment attachment, IItem draftItem, out IAttachment newAttachment, out AttachmentId newAttachmentId)
			{
				newAttachment = this.factory.CloneAttachment(attachment, draftItem);
				if (newAttachment is IStreamAttachment)
				{
					newAttachment.FileName = GetWacAttachmentInfo.Implementation.GenerateDocumentNameForEditing(attachment.FileName, this.userDisplayName);
				}
				else
				{
					newAttachment.FileName = attachment.FileName;
				}
				newAttachmentId = newAttachment.Id;
				this.ResultAttachmentExtension = newAttachment.FileExtension;
				IReferenceAttachment referenceAttachment = newAttachment as IReferenceAttachment;
				if (referenceAttachment != null)
				{
					this.ResultAttachmentContentUrl = referenceAttachment.AttachLongPathName;
					this.ResultAttachmentWebServiceUrl = referenceAttachment.ProviderEndpointUrl;
					this.ResultAttachmentProviderType = referenceAttachment.ProviderType;
				}
				newAttachment.IsInline = false;
				newAttachment.Save();
				draftItem.Save(SaveMode.NoConflictResolutionForceSave);
			}

			// Token: 0x06001B01 RID: 6913 RVA: 0x000669B0 File Offset: 0x00064BB0
			internal static string GenerateDocumentNameForEditing(string fileName, string userDisplayName)
			{
				string extension = Path.GetExtension(fileName);
				string text = Path.GetFileNameWithoutExtension(fileName);
				string text2 = GetWacAttachmentInfo.Implementation.GenerateDocumentNameForEditing(text, userDisplayName, extension);
				int length = text2.Length;
				if (length > 150)
				{
					string ellipsis = Strings.Ellipsis;
					int num = length - 150;
					if (userDisplayName.Length > 75)
					{
						num += ellipsis.Length * 2;
						if (num % 2 == 1)
						{
							num++;
						}
						text = text.Substring(0, text.Length - num / 2);
						text += ellipsis;
						userDisplayName = userDisplayName.Substring(0, userDisplayName.Length - num / 2);
						userDisplayName += ellipsis;
					}
					else
					{
						num += ellipsis.Length;
						text = text.Substring(0, text.Length - num);
						text += ellipsis;
					}
					text2 = GetWacAttachmentInfo.Implementation.GenerateDocumentNameForEditing(text, userDisplayName, extension);
				}
				return text2;
			}

			// Token: 0x06001B02 RID: 6914 RVA: 0x00066A87 File Offset: 0x00064C87
			private static string GenerateDocumentNameForEditing(string baseName, string displayName, string extension)
			{
				return string.Format(Strings.DocumentEditFormat, baseName, displayName) + extension;
			}

			// Token: 0x04000EE4 RID: 3812
			private readonly AttachmentDataProvider provider;

			// Token: 0x04000EE5 RID: 3813
			private readonly IXSOFactory factory;

			// Token: 0x04000EE6 RID: 3814
			private readonly IStoreSession originalAttachmentSession;

			// Token: 0x04000EE7 RID: 3815
			private readonly ModernGroupObjectType originalSessionModernGroupType;

			// Token: 0x04000EE8 RID: 3816
			private readonly IMailboxSession draftAttachmentSession;

			// Token: 0x04000EE9 RID: 3817
			private readonly StoreObjectId draftObjectId;

			// Token: 0x04000EEA RID: 3818
			private readonly IItem originalAttachmentRootItem;

			// Token: 0x04000EEB RID: 3819
			private readonly IAttachment originalAttachment;

			// Token: 0x04000EEC RID: 3820
			private readonly string originalAttachmentId;

			// Token: 0x04000EED RID: 3821
			private readonly bool isEdit;

			// Token: 0x04000EEE RID: 3822
			private readonly string userDisplayName;

			// Token: 0x04000EEF RID: 3823
			private readonly Func<IStoreSession, StoreId, AttachmentId, string> idConverter;

			// Token: 0x04000EF0 RID: 3824
			private IItem draftItem;
		}

		// Token: 0x0200032B RID: 811
		private class PostUploadMessageAsyncState
		{
			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00066A9B File Offset: 0x00064C9B
			// (set) Token: 0x06001B04 RID: 6916 RVA: 0x00066AA3 File Offset: 0x00064CA3
			public string MailboxSmtpAddress { get; set; }

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00066AAC File Offset: 0x00064CAC
			// (set) Token: 0x06001B06 RID: 6918 RVA: 0x00066AB4 File Offset: 0x00064CB4
			public string LogonSmtpAddress { get; set; }

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00066ABD File Offset: 0x00064CBD
			// (set) Token: 0x06001B08 RID: 6920 RVA: 0x00066AC5 File Offset: 0x00064CC5
			public OWAService OwaService { get; set; }

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00066ACE File Offset: 0x00064CCE
			// (set) Token: 0x06001B0A RID: 6922 RVA: 0x00066AD6 File Offset: 0x00064CD6
			public string SessionId { get; set; }
		}

		// Token: 0x0200032C RID: 812
		public class AttachmentUploadException : Exception
		{
			// Token: 0x06001B0C RID: 6924 RVA: 0x00066AE7 File Offset: 0x00064CE7
			public AttachmentUploadException(string message) : base(message)
			{
			}
		}
	}
}
