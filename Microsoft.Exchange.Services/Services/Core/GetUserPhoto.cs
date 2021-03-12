using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.InfoWorker.Common.UserPhotos;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200033D RID: 829
	internal class GetUserPhoto : SingleStepServiceCommand<GetUserPhotoRequest, Stream>
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x0007BBAC File Offset: 0x00079DAC
		static GetUserPhoto()
		{
			CertificateValidationManager.RegisterCallback("GetUserPhoto", new RemoteCertificateValidationCallback(CommonCertificateValidationCallbacks.InternalServerToServer));
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0007BC14 File Offset: 0x00079E14
		public GetUserPhoto(CallContext callContext, GetUserPhotoRequest request, ITracer upstreamTracer) : base(callContext, request)
		{
			this.InitializeRequestTrackingInformation();
			this.InitializeTracers(upstreamTracer);
			this.perfLogger = new PhotoRequestPerformanceLogger(base.CallContext.ProtocolLog, this.requestTracer);
			OwsLogRegistry.Register("GetUserPhoto", typeof(PhotosLoggingMetadata), new Type[0]);
			base.Request.CacheId = base.CallContext.HttpContext.Request.Headers["If-None-Match"];
			if (base.Request.IsPostRequest)
			{
				this.OutgoingResponse = base.CallContext.CreateWebResponseContext();
				return;
			}
			this.OutgoingResponse = request.OutgoingResponse;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0007BCE2 File Offset: 0x00079EE2
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x0007BCEA File Offset: 0x00079EEA
		private IOutgoingWebResponseContext OutgoingResponse { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0007BCF3 File Offset: 0x00079EF3
		internal override Offer ExpectedOffer
		{
			get
			{
				return Offer.SharingCalendarFreeBusy;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0007BCFA File Offset: 0x00079EFA
		internal override bool SupportsExternalUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0007BD00 File Offset: 0x00079F00
		internal override IExchangeWebMethodResponse GetResponse()
		{
			if (base.Request.IsPostRequest)
			{
				return new GetUserPhotoResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value, this.hasChanged, this.photoContentType);
			}
			GetUserPhotoResponse getUserPhotoResponse = new GetUserPhotoResponse();
			getUserPhotoResponse.AddResponse(new GetUserPhotoResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value, this.hasChanged, this.photoContentType));
			return getUserPhotoResponse;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0007BD8C File Offset: 0x00079F8C
		internal override bool InternalPreExecute()
		{
			if (base.Request.IsPostRequest)
			{
				base.Request.Validate();
			}
			return base.InternalPreExecute();
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0007BDAC File Offset: 0x00079FAC
		internal override ServiceResult<Stream> Execute()
		{
			ServiceResult<Stream> result;
			try
			{
				this.tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "GetUserPhoto.Execute: executing on host {0}.  Request-Id: {1}.  Client-Request-Id: {2}", this.GetIncomingRequestHost(), this.requestId, this.clientRequestId);
				using (new StopwatchPerformanceTracker("GetUserPhotoTotal", this.perfLogger))
				{
					using (new ADPerformanceTracker("GetUserPhotoTotal", this.perfLogger))
					{
						using (new StorePerformanceTracker("GetUserPhotoTotal", this.perfLogger))
						{
							ServiceResult<Stream> serviceResult = this.ExecuteGetUserPhoto();
							if (base.Request.FallbackToClearImage && this.OutgoingResponse.StatusCode == HttpStatusCode.NotFound)
							{
								this.tracer.TraceDebug((long)this.GetHashCode(), "Since client is requesting to fallback on clear image and a photo not found, returning a clear 1x1 GIF image.");
								if (serviceResult.Value != null)
								{
									serviceResult.Value.Dispose();
								}
								result = this.GetClearImage();
							}
							else
							{
								result = serviceResult;
							}
						}
					}
				}
			}
			catch (IOException caughtException)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException);
			}
			catch (Win32Exception caughtException2)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException2);
			}
			catch (UnauthorizedAccessException caughtException3)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException3);
			}
			catch (TimeoutException caughtException4)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException4);
			}
			catch (StorageTransientException caughtException5)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException5);
			}
			catch (StoragePermanentException caughtException6)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException6);
			}
			catch (TransientException caughtException7)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException7);
			}
			catch (ADOperationException caughtException8)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException8);
			}
			catch (ServicePermanentException caughtException9)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException9);
			}
			catch (Exception caughtException10)
			{
				this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException10);
				throw;
			}
			finally
			{
				this.LogPhotoRequestTraces();
			}
			return result;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0007C0D0 File Offset: 0x0007A2D0
		private ServiceResult<Stream> GetClearImage()
		{
			this.OutgoingResponse.ContentType = (this.photoContentType = "image/gif");
			this.OutgoingResponse.StatusCode = HttpStatusCode.OK;
			return new ServiceResult<Stream>(new MemoryStream(GetUserPhoto.Clear1x1GIF));
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0007C115 File Offset: 0x0007A315
		private static ICollection<string> GetEmailAddressesOfRequestor(ExchangePrincipal requestor, ClientContext requestorContext)
		{
			return GetUserPhoto.GetEmailAddressesFromExchangePrincipal(requestor).Union(GetUserPhoto.GetEmailAddressesFromClientContext(requestorContext));
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0007C128 File Offset: 0x0007A328
		private static ICollection<string> GetEmailAddressesFromExchangePrincipal(ExchangePrincipal requestor)
		{
			if (requestor == null)
			{
				return Array<string>.Empty;
			}
			return requestor.GetAllEmailAddresses();
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0007C13C File Offset: 0x0007A33C
		private static ICollection<string> GetEmailAddressesFromClientContext(ClientContext clientContext)
		{
			if (clientContext == null)
			{
				return Array<string>.Empty;
			}
			if (clientContext is PersonalClientContext)
			{
				return new string[]
				{
					((PersonalClientContext)clientContext).EmailAddress.ToString()
				};
			}
			if (clientContext is OrganizationalClientContext)
			{
				return new string[]
				{
					((OrganizationalClientContext)clientContext).EmailAddress.ToString()
				};
			}
			return Array<string>.Empty;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0007C1B4 File Offset: 0x0007A3B4
		private ServiceResult<Stream> ExecuteGetUserPhoto()
		{
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.PhotoSize, base.Request.SizeRequested);
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.TargetEmailAddress, base.Request.Email);
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.ExecutedV2Implementation, true);
			this.request = this.AssemblePhotoRequest();
			MemoryStream memoryStream = new MemoryStream();
			ServiceResult<Stream> result;
			using (DisposeGuard disposeGuard = memoryStream.Guard())
			{
				IRecipientSession recipientSession = this.CreateRecipientSession(this.request.Requestor.OrganizationId);
				PhotoResponse photoResponse = new OrganizationalPhotoRetrievalPipeline(GetUserPhoto.PhotosConfiguration, "GetUserPhoto", this.GetMailboxSessionClientInfo(), recipientSession, this.GetProxyProviderForOutboundRequest(), this.GetRemoteForestPipelineFactory(recipientSession), XSOFactory.Default, this.downstreamTracer).Retrieve(this.request, memoryStream);
				this.photoContentType = photoResponse.ContentType;
				if (string.IsNullOrEmpty(this.photoContentType))
				{
					this.tracer.TraceDebug((long)this.GetHashCode(), "GetUserPhoto: ContentType of response is null or empty");
				}
				base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.ResponseContentType, this.photoContentType);
				this.OutgoingResponse.StatusCode = photoResponse.Status;
				this.OutgoingResponse.ETag = photoResponse.ETag;
				if (base.Request.IsPostRequest)
				{
					this.OutgoingResponse.ContentType = "text/xml; charset=utf-8";
				}
				else
				{
					this.OutgoingResponse.ContentType = photoResponse.ContentType;
				}
				string text = string.IsNullOrEmpty(photoResponse.HttpExpiresHeader) ? UserAgentPhotoExpiresHeader.Default.ComputeExpiresHeader(DateTime.UtcNow, photoResponse.Status, GetUserPhoto.PhotosConfiguration) : photoResponse.HttpExpiresHeader;
				this.OutgoingResponse.Expires = text;
				this.ComputeHasChangedNodeOfSoapResponseAndResetStatusCode();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				this.tracer.TraceDebug((long)this.GetHashCode(), "GetUserPhoto: request completed.  Status: {0};  ETag: {1};  Content-Type: {2};  Content-Length: {3};  Expires: {4}", new object[]
				{
					photoResponse.Status,
					photoResponse.ETag,
					photoResponse.ContentType,
					photoResponse.ContentLength,
					text
				});
				disposeGuard.Success();
				result = new ServiceResult<Stream>(memoryStream);
			}
			return result;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0007C41C File Offset: 0x0007A61C
		private ClientContext CreateClientContext()
		{
			ClientContext clientContext = this.CreateContextBasedOnClientLocation();
			clientContext.RequestSchemaVersion = Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.ExchangeVersionType.Exchange2012;
			clientContext.RequestId = this.clientRequestId;
			return clientContext;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0007C444 File Offset: 0x0007A644
		private ClientContext CreateContextBasedOnClientLocation()
		{
			ClientContext result;
			using (new StopwatchPerformanceTracker("CreateClientContext", this.perfLogger))
			{
				using (new ADPerformanceTracker("CreateClientContext", this.perfLogger))
				{
					base.CallerBudget.EndLocal();
					try
					{
						if (base.CallContext.IsExternalUser)
						{
							ExternalCallContext externalCallContext = (ExternalCallContext)base.CallContext;
							result = ClientContext.Create(externalCallContext.EmailAddress, externalCallContext.ExternalId, externalCallContext.WSSecurityHeader, externalCallContext.SharingSecurityHeader, externalCallContext.Budget, EWSSettings.RequestTimeZone, EWSSettings.ClientCulture);
						}
						else
						{
							ADUser adUser;
							ADIdentityInformationCache.Singleton.TryGetADUser(base.CallContext.EffectiveCallerSid, base.CallContext.ADRecipientSessionContext, out adUser);
							result = ClientContext.Create(base.CallContext.EffectiveCaller.ClientSecurityContext, base.CallerBudget, EWSSettings.RequestTimeZone, EWSSettings.ClientCulture, this.GetMessageIdForRequestTracking(), adUser);
						}
					}
					catch (AuthzException ex)
					{
						this.tracer.TraceError<AuthzException>((long)this.GetHashCode(), "Unable to retrieve client context due to authorization exception: {0}", ex);
						throw new ServiceAccessDeniedException(ex);
					}
					finally
					{
						base.CallerBudget.StartLocal("GetUserPhoto.CreateClientContext", default(TimeSpan));
					}
				}
			}
			return result;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0007C5DC File Offset: 0x0007A7DC
		private PhotoRequest CreatePhotoRequestFromContext(HttpContext context)
		{
			return new HttpPhotoRequestBuilder(GetUserPhoto.PhotosConfiguration, this.downstreamTracer).Parse(context.Request, this.perfLogger);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0007C600 File Offset: 0x0007A800
		private void InitializeTracers(ITracer upstreamTracer)
		{
			this.requestTracer = new InMemoryTracer(ExTraceGlobals.UserPhotosTracer.Category, ExTraceGlobals.UserPhotosTracer.TraceTag);
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(this.requestTracer).Compose(upstreamTracer);
			this.downstreamTracer = this.requestTracer.Compose(upstreamTracer);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0007C65C File Offset: 0x0007A85C
		private void LogPhotoRequestTraces()
		{
			if (!this.ShouldTraceRequest())
			{
				return;
			}
			PhotosDiagnostics.Instance.StampTracesOnGetUserPhotosResponse(this.requestTracer, base.CallContext.HttpContext.Response);
			if (this.requestTracer != null && !NullTracer.Instance.Equals(this.requestTracer))
			{
				((InMemoryTracer)this.requestTracer).Dump(new PhotoRequestLogWriter(GetUserPhoto.RequestLog, this.requestId));
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0007C6CC File Offset: 0x0007A8CC
		private bool ShouldTraceRequest()
		{
			return (this.request != null && this.request.Trace) || PhotosDiagnostics.Instance.ShouldTraceGetUserPhotoRequest(base.CallContext.HttpContext.Request);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0007C700 File Offset: 0x0007A900
		private bool IsSelfPhotoRequest()
		{
			if (string.IsNullOrEmpty(base.Request.Email) && base.Request.AdObjectId == null)
			{
				return true;
			}
			if (this.RequestWasProxied())
			{
				return false;
			}
			if (base.CallContext.AccessingPrincipal == null)
			{
				return false;
			}
			PhotoPrincipal photoPrincipal = new PhotoPrincipal
			{
				EmailAddresses = GetUserPhoto.GetEmailAddressesOfRequestor(base.CallContext.AccessingPrincipal, null)
			};
			PhotoPrincipal other = new PhotoPrincipal
			{
				EmailAddresses = new string[]
				{
					base.Request.Email
				}
			};
			return photoPrincipal.IsSame(other);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0007C794 File Offset: 0x0007A994
		private string GetIncomingRequestHost()
		{
			if (base.CallContext == null || base.CallContext.HttpContext == null || base.CallContext.HttpContext.Request == null || base.CallContext.HttpContext.Request.Headers == null)
			{
				return string.Empty;
			}
			return base.CallContext.HttpContext.Request.Headers["Host"];
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0007C804 File Offset: 0x0007AA04
		private bool RequestWasProxied()
		{
			return base.CallContext.AvailabilityProxyRequestType != null;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0007C824 File Offset: 0x0007AA24
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

		// Token: 0x0600174C RID: 5964 RVA: 0x0007C874 File Offset: 0x0007AA74
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

		// Token: 0x0600174D RID: 5965 RVA: 0x0007C908 File Offset: 0x0007AB08
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

		// Token: 0x0600174E RID: 5966 RVA: 0x0007C975 File Offset: 0x0007AB75
		private void InitializeRequestTrackingInformation()
		{
			this.requestId = this.ComputeRequestId();
			this.clientRequestId = this.ComputeClientRequestId();
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0007C990 File Offset: 0x0007AB90
		private void ComputeHasChangedNodeOfSoapResponseAndResetStatusCode()
		{
			if (!base.Request.IsPostRequest)
			{
				return;
			}
			if (this.OutgoingResponse.StatusCode != HttpStatusCode.NotModified)
			{
				this.hasChanged = true;
				return;
			}
			this.hasChanged = false;
			this.OutgoingResponse.StatusCode = HttpStatusCode.OK;
			this.tracer.TraceDebug((long)this.GetHashCode(), "Photo has NOT changed.  Setting HTTP status code to 200 and <HasChanged> node to FALSE in POST response.");
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0007C9F4 File Offset: 0x0007ABF4
		private bool IsRequestViaHttpGetAndKnownToBeLocalToThisBackend()
		{
			return !base.Request.IsPostRequest && base.CallContext.AvailabilityProxyRequestType != null && base.CallContext.AvailabilityProxyRequestType.Value.Equals(ProxyRequestType.CrossSite);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0007CA48 File Offset: 0x0007AC48
		private ServiceResult<Stream> TraceExceptionAndReturnInternalServerError(IOutgoingWebResponseContext webContext, Exception caughtException)
		{
			this.tracer.TraceError<Exception>((long)this.GetHashCode(), "Request failed with exception: {0}", caughtException);
			webContext.StatusCode = HttpStatusCode.InternalServerError;
			base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.GetUserPhotoFailed, true);
			return new ServiceResult<Stream>(new MemoryStream(Array<byte>.Empty));
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0007CAA5 File Offset: 0x0007ACA5
		private string GetMailboxSessionClientInfo()
		{
			return StoreSessionCacheBase.BuildMapiApplicationId(base.CallContext, "Action=GetUserPhoto");
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0007CAB8 File Offset: 0x0007ACB8
		private IMailboxSession GetMailboxSessionOfAccessingPrincipal(ExchangePrincipal target)
		{
			return base.GetMailboxSession(base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0007CAEE File Offset: 0x0007ACEE
		private IMailboxSession GetMailboxSessionOfUserKnownToBeLocalToThisBackend(ExchangePrincipal otherUser)
		{
			return null;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0007CAF1 File Offset: 0x0007ACF1
		private static IMailboxSession NoHostOwnedMailboxSession(ExchangePrincipal target)
		{
			return null;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0007CAF4 File Offset: 0x0007ACF4
		private PhotoHandlers GetHandlersToSkip()
		{
			return PhotosDiagnostics.Instance.GetHandlersToSkip(base.CallContext.HttpContext.Request);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0007CB10 File Offset: 0x0007AD10
		private PhotoRequest AssemblePhotoRequest()
		{
			if (this.IsSelfPhotoRequest())
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Assembled request for SELF-PHOTO.");
				base.CallContext.ProtocolLog.Set(PhotosLoggingMetadata.IsSelfPhoto, true);
				return new PhotoRequest
				{
					Requestor = new PhotoPrincipal
					{
						EmailAddresses = GetUserPhoto.GetEmailAddressesOfRequestor(base.CallContext.AccessingPrincipal, null),
						OrganizationId = base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId
					},
					RequestorFromExternalOrganization = false,
					ETag = base.Request.CacheId,
					Preview = base.Request.IsPreview,
					Size = base.Request.GetConvertedSizeRequested(),
					TargetSmtpAddress = base.Request.Email,
					TargetAdObjectId = base.Request.AdObjectId,
					TargetPrincipal = base.CallContext.AccessingPrincipal,
					PerformanceLogger = this.perfLogger,
					HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(this.GetMailboxSessionOfAccessingPrincipal),
					HandlersToSkip = this.GetHandlersToSkip(),
					Trace = this.ShouldTraceRequest(),
					Self = new bool?(true),
					IsTargetKnownToBeLocalToThisServer = new bool?(true),
					IsTargetMailboxLikelyOnThisServer = new bool?(true),
					ClientRequestId = this.clientRequestId
				};
			}
			if (this.IsRequestViaHttpGetAndKnownToBeLocalToThisBackend())
			{
				PhotoRequest photoRequest = this.CreatePhotoRequestFromContext(base.CallContext.HttpContext);
				photoRequest.HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(this.GetMailboxSessionOfUserKnownToBeLocalToThisBackend);
				photoRequest.Self = new bool?(false);
				photoRequest.IsTargetKnownToBeLocalToThisServer = new bool?(true);
				photoRequest.IsTargetMailboxLikelyOnThisServer = new bool?(true);
				photoRequest.RequestorFromExternalOrganization = base.CallContext.IsExternalUser;
				photoRequest.ClientRequestId = this.clientRequestId;
				this.tracer.TraceDebug((long)this.GetHashCode(), "Assembled request for TARGET KNOWN TO BE LOCAL TO THIS SERVER.");
				return photoRequest;
			}
			ClientContext clientContext = this.CreateClientContext();
			this.tracer.TraceDebug((long)this.GetHashCode(), "Assembled request for ARBITRARY photo.");
			return new PhotoRequest
			{
				Requestor = new PhotoPrincipal
				{
					EmailAddresses = GetUserPhoto.GetEmailAddressesOfRequestor(base.CallContext.AccessingPrincipal, clientContext),
					OrganizationId = clientContext.OrganizationId
				},
				RequestorFromExternalOrganization = base.CallContext.IsExternalUser,
				ETag = base.Request.CacheId,
				Preview = base.Request.IsPreview,
				Size = base.Request.GetConvertedSizeRequested(),
				TargetSmtpAddress = base.Request.Email,
				TargetAdObjectId = base.Request.AdObjectId,
				PerformanceLogger = this.perfLogger,
				HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(GetUserPhoto.NoHostOwnedMailboxSession),
				HandlersToSkip = this.GetHandlersToSkip(),
				Trace = this.ShouldTraceRequest(),
				ClientRequestId = this.clientRequestId,
				IsTargetMailboxLikelyOnThisServer = new bool?(this.LikelyRoutedToTargetMailboxByFrontend()),
				ClientContextForRemoteForestRequests = clientContext
			};
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0007CE25 File Offset: 0x0007B025
		private IRecipientSession CreateRecipientSession(OrganizationId organizationId)
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 982, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\GetUserPhoto.cs");
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0007CE48 File Offset: 0x0007B048
		private IPhotoRequestOutboundWebProxyProvider GetProxyProviderForOutboundRequest()
		{
			return new PhotoRequestOutboundWebProxyProviderUsingLocalServerConfiguration(this.downstreamTracer);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0007CE58 File Offset: 0x0007B058
		private bool LikelyRoutedToTargetMailboxByFrontend()
		{
			if (base.CallContext == null || base.CallContext.HttpContext == null || base.CallContext.HttpContext.Request == null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Cannot determine whether request was likely routed by frontend because context has not been initialized.");
				return false;
			}
			return !base.CallContext.HttpContext.Request.Path.EndsWith("/GetPersonaPhoto", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0007CEC8 File Offset: 0x0007B0C8
		private IRemoteForestPhotoRetrievalPipelineFactory GetRemoteForestPipelineFactory(IRecipientSession recipientSession)
		{
			return new RemoteForestPhotoRetrievalPipelineUsingAvailabilityServiceFactory(GetUserPhoto.PhotosConfiguration, recipientSession, this.downstreamTracer);
		}

		// Token: 0x04000FB7 RID: 4023
		private const string GetUserPhotoActionName = "GetUserPhoto";

		// Token: 0x04000FB8 RID: 4024
		private const string HttpHostHeader = "Host";

		// Token: 0x04000FB9 RID: 4025
		private const string CertificateValidationComponentId = "GetUserPhoto";

		// Token: 0x04000FBA RID: 4026
		private const string GetPersonaPhotoRequestPathSuffix = "/GetPersonaPhoto";

		// Token: 0x04000FBB RID: 4027
		private const string HttpPostContentType = "text/xml; charset=utf-8";

		// Token: 0x04000FBC RID: 4028
		private static readonly byte[] Clear1x1GIF = new byte[]
		{
			71,
			73,
			70,
			56,
			57,
			97,
			1,
			0,
			1,
			0,
			128,
			0,
			0,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			33,
			249,
			4,
			1,
			0,
			0,
			1,
			0,
			44,
			0,
			0,
			0,
			0,
			1,
			0,
			1,
			0,
			0,
			2,
			1,
			76,
			0,
			59
		};

		// Token: 0x04000FBD RID: 4029
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04000FBE RID: 4030
		private static readonly PhotoRequestLog RequestLog = new PhotoRequestLogFactory(GetUserPhoto.PhotosConfiguration, ExchangeSetupContext.InstalledVersion.ToString()).Create();

		// Token: 0x04000FBF RID: 4031
		private readonly IPerformanceDataLogger perfLogger = NullPerformanceDataLogger.Instance;

		// Token: 0x04000FC0 RID: 4032
		private string requestId;

		// Token: 0x04000FC1 RID: 4033
		private string clientRequestId;

		// Token: 0x04000FC2 RID: 4034
		private bool hasChanged;

		// Token: 0x04000FC3 RID: 4035
		private ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04000FC4 RID: 4036
		private ITracer requestTracer = NullTracer.Instance;

		// Token: 0x04000FC5 RID: 4037
		private ITracer downstreamTracer;

		// Token: 0x04000FC6 RID: 4038
		private PhotoRequest request;

		// Token: 0x04000FC7 RID: 4039
		private string photoContentType;
	}
}
