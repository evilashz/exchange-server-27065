using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.InfoWorker.Common.UserPhotos;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000322 RID: 802
	internal sealed class GetPersonaPhoto : ServiceCommand<Stream>
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x00063D84 File Offset: 0x00061F84
		static GetPersonaPhoto()
		{
			CertificateValidationManager.RegisterCallback("GetPersonaPhoto", new RemoteCertificateValidationCallback(CommonCertificateValidationCallbacks.InternalServerToServer));
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00063DD4 File Offset: 0x00061FD4
		public GetPersonaPhoto(CallContext callContext, string personIdParam, string adObjectIdParam, string email, string singleSourceId, Microsoft.Exchange.Services.Core.Types.UserPhotoSize size) : base(callContext)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			this.InitializeRequestTrackingInformation();
			this.InitializeTracers();
			OwsLogRegistry.Register("GetPersonaPhoto", typeof(PhotosLoggingMetadata), new Type[0]);
			this.perfLogger = new PhotoRequestPerformanceLogger(base.CallContext.ProtocolLog, this.tracer);
			this.responseContext = callContext.CreateWebResponseContext();
			this.size = size;
			this.tracer.TraceDebug<Microsoft.Exchange.Services.Core.Types.UserPhotoSize>((long)this.GetHashCode(), "size = {0}", this.size);
			this.email = email;
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "email = {0}", this.email);
			if (!string.IsNullOrEmpty(personIdParam))
			{
				try
				{
					if (IdConverter.EwsIdIsConversationId(personIdParam))
					{
						this.personId = IdConverter.EwsIdToPersonId(personIdParam);
						this.tracer.TraceDebug<PersonId>((long)this.GetHashCode(), "personId = {0}", this.personId);
					}
				}
				catch (InvalidStoreIdException arg)
				{
					this.tracer.TraceError<string, InvalidStoreIdException>((long)this.GetHashCode(), "Exception: InvalidStoreIdException. personIdParam = {0}. Exception: {1}", personIdParam, arg);
				}
			}
			Guid empty = Guid.Empty;
			if (!string.IsNullOrEmpty(adObjectIdParam) && Guid.TryParse(adObjectIdParam, out empty))
			{
				this.adObjectId = new ADObjectId(empty);
				this.tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "adObjectId = {0}", this.adObjectId);
			}
			if (!string.IsNullOrEmpty(singleSourceId))
			{
				if (IdConverter.EwsIdIsActiveDirectoryObject(singleSourceId))
				{
					this.adObjectId = IdConverter.EwsIdToADObjectId(singleSourceId);
					this.tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "adObjectId = {0}", this.adObjectId);
					return;
				}
				this.contactId = IdConverter.EwsIdToMessageStoreObjectId(singleSourceId);
				this.tracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "contactId = {0}", this.contactId);
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00063FB4 File Offset: 0x000621B4
		protected override Stream InternalExecute()
		{
			Stream result;
			try
			{
				using (new StopwatchPerformanceTracker("GetPersonaPhotoTotal", this.perfLogger))
				{
					using (new ADPerformanceTracker("GetPersonaPhotoTotal", this.perfLogger))
					{
						using (new StorePerformanceTracker("GetPersonaPhotoTotal", this.perfLogger))
						{
							result = this.ExecuteGetPersonaPhoto();
						}
					}
				}
			}
			catch (Exception arg)
			{
				this.tracer.TraceError<Exception>((long)this.GetHashCode(), "GetPersonaPhoto.InternalExecute: exception: {0}", arg);
				base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.GetPersonaPhotoFailed, true);
				throw;
			}
			return result;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00064098 File Offset: 0x00062298
		protected override void LogTracesForCurrentRequest()
		{
			WcfServiceCommandBase.TraceLoggerFactory.Create(this.responseContext.Headers).LogTraces(this.requestTracer);
			if (!NullTracer.Instance.Equals(this.requestTracer))
			{
				((InMemoryTracer)this.requestTracer).Dump(new PhotoRequestLogWriter(GetPersonaPhoto.RequestLog, this.requestId));
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000640F7 File Offset: 0x000622F7
		private static string ComputeExpiresHeader(HttpStatusCode responseStatus)
		{
			return UserAgentPhotoExpiresHeader.Default.ComputeExpiresHeader(DateTime.UtcNow, responseStatus, GetPersonaPhoto.PhotosConfiguration);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00064110 File Offset: 0x00062310
		private void InitializeTracers()
		{
			ITracer tracer;
			if (!base.IsRequestTracingEnabled)
			{
				ITracer instance = NullTracer.Instance;
				tracer = instance;
			}
			else
			{
				tracer = new InMemoryTracer(ExTraceGlobals.GetPersonaPhotoTracer.Category, ExTraceGlobals.GetPersonaPhotoTracer.TraceTag);
			}
			this.requestTracer = tracer;
			this.tracer = ExTraceGlobals.GetPersonaPhotoTracer.Compose(this.requestTracer);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00064164 File Offset: 0x00062364
		private Stream ExecuteGetPersonaPhoto()
		{
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.ExecutedV2Implementation, true);
			Stream result;
			try
			{
				PhotoRequest request = this.AssembleRequest();
				MemoryStream memoryStream = new MemoryStream();
				using (DisposeGuard disposeGuard = memoryStream.Guard())
				{
					IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
					PhotoResponse photoResponse = new OwaPhotoRetrievalPipeline(GetPersonaPhoto.PhotosConfiguration, "GetPersonaPhoto", base.MailboxIdentityMailboxSession.ClientInfoString, adrecipientSession, this.GetProxyProviderForOutboundRequest(), this.GetRemoteForestPipelineFactory(adrecipientSession), XSOFactory.Default, this.requestTracer).Retrieve(request, memoryStream);
					this.responseContext.StatusCode = photoResponse.Status;
					this.responseContext.ETag = photoResponse.ETag;
					if (string.IsNullOrEmpty(photoResponse.ContentType))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "GetPersonaPhoto: ContentType of response is null or empty");
					}
					base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.ResponseContentType, photoResponse.ContentType);
					this.responseContext.ContentType = photoResponse.ContentType;
					this.responseContext.Headers["Location"] = (photoResponse.PhotoUrl ?? string.Empty);
					string text = string.IsNullOrEmpty(photoResponse.HttpExpiresHeader) ? GetPersonaPhoto.ComputeExpiresHeader(photoResponse.Status) : photoResponse.HttpExpiresHeader;
					this.responseContext.Headers["Expires"] = text;
					memoryStream.Seek(0L, SeekOrigin.Begin);
					this.tracer.TraceDebug((long)this.GetHashCode(), "GetPersonaPhoto: request completed.  Status: {0};  ETag: {1};  Content-Type: {2};  Expires: '{3}';  Redirect: '{4}'", new object[]
					{
						photoResponse.Status,
						photoResponse.ETag,
						photoResponse.ContentType,
						text,
						photoResponse.PhotoUrl
					});
					disposeGuard.Success();
					result = memoryStream;
				}
			}
			catch (IOException caughtException)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException);
			}
			catch (Win32Exception caughtException2)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException2);
			}
			catch (UnauthorizedAccessException caughtException3)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException3);
			}
			catch (TimeoutException caughtException4)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException4);
			}
			catch (StorageTransientException caughtException5)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException5);
			}
			catch (StoragePermanentException caughtException6)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException6);
			}
			catch (TransientException caughtException7)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException7);
			}
			catch (ADOperationException caughtException8)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException8);
			}
			catch (ServicePermanentException caughtException9)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.responseContext, caughtException9);
			}
			return result;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00064500 File Offset: 0x00062700
		private PhotoRequest AssembleRequest()
		{
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.PhotoSize, this.size);
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.TargetEmailAddress, this.email);
			PhotoRequest photoRequest = new PhotoRequest
			{
				Requestor = new PhotoPrincipal
				{
					EmailAddresses = base.CallContext.AccessingPrincipal.GetAllEmailAddresses(),
					OrganizationId = base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId
				},
				RequestorMailboxSession = base.MailboxIdentityMailboxSession,
				ETag = base.CallContext.HttpContext.Request.Headers["If-None-Match"],
				Preview = false,
				Size = ServicePhotoSizeToStoragePhotoSizeConverter.Convert(this.size),
				TargetSmtpAddress = this.email,
				TargetAdObjectId = this.adObjectId,
				TargetPersonId = this.personId,
				TargetContactId = this.contactId,
				PerformanceLogger = this.perfLogger,
				HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(GetPersonaPhoto.NoHostOwnedMailboxSession),
				HandlersToSkip = this.GetHandlersToSkip(),
				Trace = base.IsRequestTracingEnabled,
				ClientRequestId = this.clientRequestId,
				ClientContextForRemoteForestRequests = this.CreateClientContext()
			};
			if (this.IsSelfPhotoRequest())
			{
				base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.IsSelfPhoto, true);
				photoRequest.HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(this.GetMailboxSessionOfRequestor);
				photoRequest.TargetPrincipal = base.CallContext.AccessingPrincipal;
				photoRequest.Self = new bool?(true);
			}
			return photoRequest;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000646AE File Offset: 0x000628AE
		private IMailboxSession GetMailboxSessionOfRequestor(ExchangePrincipal target)
		{
			return base.MailboxIdentityMailboxSession;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000646B6 File Offset: 0x000628B6
		private static IMailboxSession NoHostOwnedMailboxSession(ExchangePrincipal target)
		{
			return null;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000646B9 File Offset: 0x000628B9
		private PhotoHandlers GetHandlersToSkip()
		{
			return PhotosDiagnostics.Instance.GetHandlersToSkip(base.CallContext.HttpContext.Request);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000646D5 File Offset: 0x000628D5
		private void InitializeRequestTrackingInformation()
		{
			this.requestId = this.ComputeRequestId();
			this.clientRequestId = this.ComputeClientRequestId();
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000646F0 File Offset: 0x000628F0
		private string ComputeClientRequestId()
		{
			if (!string.IsNullOrEmpty(this.clientRequestId))
			{
				return this.clientRequestId;
			}
			if (base.CallContext.ProtocolLog == null || base.CallContext.ProtocolLog.ActivityScope == null)
			{
				return this.ComputeRequestId();
			}
			string text = base.CallContext.ProtocolLog.ActivityScope.ClientRequestId;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return this.ComputeRequestId();
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00064760 File Offset: 0x00062960
		private string ComputeRequestId()
		{
			if (!string.IsNullOrEmpty(this.requestId))
			{
				return this.requestId;
			}
			if (base.CallContext.ProtocolLog == null)
			{
				return Guid.NewGuid().ToString();
			}
			if (!Guid.Empty.Equals(base.CallContext.ProtocolLog.ActivityId))
			{
				return base.CallContext.ProtocolLog.ActivityId.ToString();
			}
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000647F4 File Offset: 0x000629F4
		private bool IsSelfPhotoRequest()
		{
			if (base.CallContext.AccessingPrincipal == null)
			{
				return false;
			}
			if (base.CallContext.AccessingPrincipal.ObjectId.Equals(this.adObjectId))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Requestor is requesting his/her own photo: target AD object ID matches requestor's.");
				return true;
			}
			PhotoPrincipal photoPrincipal = new PhotoPrincipal
			{
				EmailAddresses = base.CallContext.AccessingPrincipal.GetAllEmailAddresses()
			};
			PhotoPrincipal other = new PhotoPrincipal
			{
				EmailAddresses = new string[]
				{
					this.email
				}
			};
			bool flag = photoPrincipal.IsSame(other);
			if (flag)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Requestor is requesting his/her own photo: target and requestor are same principal.");
			}
			return flag;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000648AC File Offset: 0x00062AAC
		private IPhotoRequestOutboundWebProxyProvider GetProxyProviderForOutboundRequest()
		{
			return new PhotoRequestOutboundWebProxyProviderUsingLocalServerConfiguration(this.requestTracer);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000648BC File Offset: 0x00062ABC
		private Stream TraceExceptionAndReturnInternalServerError(IOutgoingWebResponseContext webContext, Exception caughtException)
		{
			this.tracer.TraceError<Exception>((long)this.GetHashCode(), "Request failed with exception: {0}", caughtException);
			webContext.StatusCode = HttpStatusCode.InternalServerError;
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.GetPersonaPhotoFailed, true);
			return new MemoryStream(Array<byte>.Empty);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00064914 File Offset: 0x00062B14
		private IRemoteForestPhotoRetrievalPipelineFactory GetRemoteForestPipelineFactory(IRecipientSession recipientSession)
		{
			return new RemoteForestPhotoRetrievalPipelineUsingAvailabilityServiceFactory(GetPersonaPhoto.PhotosConfiguration, recipientSession, this.requestTracer);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00064928 File Offset: 0x00062B28
		private ClientContext CreateClientContext()
		{
			ClientContext result;
			using (new StopwatchPerformanceTracker("CreateClientContext", this.perfLogger))
			{
				using (new ADPerformanceTracker("CreateClientContext", this.perfLogger))
				{
					ADUser adUser;
					ADIdentityInformationCache.Singleton.TryGetADUser(base.CallContext.EffectiveCallerSid, base.CallContext.ADRecipientSessionContext, out adUser);
					ClientContext clientContext = ClientContext.Create(base.CallContext.EffectiveCaller.ClientSecurityContext, null, EWSSettings.RequestTimeZone, EWSSettings.ClientCulture, this.GetMessageIdForRequestTracking(), adUser);
					clientContext.RequestSchemaVersion = Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.ExchangeVersionType.Exchange2012;
					clientContext.RequestId = this.clientRequestId;
					result = clientContext;
				}
			}
			return result;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000649F4 File Offset: 0x00062BF4
		private string GetMessageIdForRequestTracking()
		{
			if (string.IsNullOrEmpty(base.CallContext.MessageId))
			{
				return string.Format(CultureInfo.InvariantCulture, "urn:uuid:{0}", new object[]
				{
					Guid.NewGuid()
				});
			}
			return base.CallContext.MessageId;
		}

		// Token: 0x04000EBD RID: 3773
		private const string GetPersonaPhotoActionName = "GetPersonaPhoto";

		// Token: 0x04000EBE RID: 3774
		private const string IfNoneMatchHeaderName = "If-None-Match";

		// Token: 0x04000EBF RID: 3775
		private const string CertificateValidationComponentId = "GetPersonaPhoto";

		// Token: 0x04000EC0 RID: 3776
		private const string ExpiresHeaderName = "Expires";

		// Token: 0x04000EC1 RID: 3777
		private const string LocationHeaderName = "Location";

		// Token: 0x04000EC2 RID: 3778
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04000EC3 RID: 3779
		private static readonly PhotoRequestLog RequestLog = new PhotoRequestLogFactory(GetPersonaPhoto.PhotosConfiguration, ExchangeSetupContext.InstalledVersion.ToString()).Create();

		// Token: 0x04000EC4 RID: 3780
		private readonly ADObjectId adObjectId;

		// Token: 0x04000EC5 RID: 3781
		private readonly StoreObjectId contactId;

		// Token: 0x04000EC6 RID: 3782
		private readonly string email;

		// Token: 0x04000EC7 RID: 3783
		private readonly Microsoft.Exchange.Services.Core.Types.UserPhotoSize size;

		// Token: 0x04000EC8 RID: 3784
		private readonly IPerformanceDataLogger perfLogger;

		// Token: 0x04000EC9 RID: 3785
		private readonly IOutgoingWebResponseContext responseContext;

		// Token: 0x04000ECA RID: 3786
		private PersonId personId;

		// Token: 0x04000ECB RID: 3787
		private ITracer tracer = ExTraceGlobals.GetPersonaPhotoTracer;

		// Token: 0x04000ECC RID: 3788
		private ITracer requestTracer = NullTracer.Instance;

		// Token: 0x04000ECD RID: 3789
		private string requestId;

		// Token: 0x04000ECE RID: 3790
		private string clientRequestId;
	}
}
