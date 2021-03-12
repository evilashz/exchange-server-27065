using System;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000209 RID: 521
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PhotoRequestRouter
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x0004E680 File Offset: 0x0004C880
		public PhotoRequestRouter(PhotosConfiguration configuration, string certificateValidationComponentId, string clientInfo, IRecipientSession recipientSession, IPhotoServiceLocatorFactory serviceLocatorFactory, IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider, IRemoteForestPhotoRetrievalPipelineFactory remoteForestPipelineFactory, IXSOFactory xsoFactory, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNullOrEmpty("certificateValidationComponentId", certificateValidationComponentId);
			ArgumentValidator.ThrowIfNullOrEmpty("clientInfo", clientInfo);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("serviceLocatorFactory", serviceLocatorFactory);
			ArgumentValidator.ThrowIfNull("outgoingRequestProxyProvider", outgoingRequestProxyProvider);
			ArgumentValidator.ThrowIfNull("remoteForestPipelineFactory", remoteForestPipelineFactory);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.configuration = configuration;
			this.certificateValidationComponentId = certificateValidationComponentId;
			this.clientInfo = clientInfo;
			this.recipientSession = recipientSession;
			this.serviceLocatorFactory = serviceLocatorFactory;
			this.outgoingRequestProxyProvider = outgoingRequestProxyProvider;
			this.remoteForestPipelineFactory = remoteForestPipelineFactory;
			this.xsoFactory = xsoFactory;
			this.tracer = upstreamTracer;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0004E744 File Offset: 0x0004C944
		public IPhotoHandler Route(PhotoRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			IPhotoHandler result;
			using (new StopwatchPerformanceTracker("RouterTotal", request.PerformanceLogger))
			{
				using (new ADPerformanceTracker("RouterTotal", request.PerformanceLogger))
				{
					if (this.IsSelfPhotoRequest(request))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "ROUTING: self-photo request.");
						result = new SelfPhotoRetrievalPipeline(this.configuration, this.clientInfo, this.recipientSession, this.xsoFactory, this.tracer);
					}
					else if (this.TargetKnownToBeLocalToThisServer(request))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "ROUTING: target known to be local to this server.");
						result = new LocalServerPhotoRetrievalPipeline(this.configuration, this.clientInfo, this.recipientSession, this.xsoFactory, this.tracer);
					}
					else if (request.RequestorFromExternalOrganization)
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "ROUTING: requestor is from EXTERNAL organization.");
						result = new ExternalRequestorPhotoRetrievalPipeline(this.recipientSession, this.tracer);
					}
					else
					{
						result = this.LookupTargetInDirectoryAndRoute(request);
					}
				}
			}
			return result;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0004E884 File Offset: 0x0004CA84
		private bool IsSelfPhotoRequest(PhotoRequest request)
		{
			return request.Self ?? false;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0004E8AC File Offset: 0x0004CAAC
		private bool TargetKnownToBeLocalToThisServer(PhotoRequest request)
		{
			return request.IsTargetKnownToBeLocalToThisServer ?? false;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
		private IPhotoHandler LookupTargetInDirectoryAndRoute(PhotoRequest request)
		{
			ADRecipient adrecipient = this.LookupTargetInDirectory(request);
			if (adrecipient == null)
			{
				return new TargetNotFoundPhotoHandler(this.configuration, this.tracer);
			}
			return this.RouteTarget(request, adrecipient);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0004E908 File Offset: 0x0004CB08
		private ADRecipient LookupTargetInDirectory(PhotoRequest request)
		{
			ADRecipient result;
			using (new StopwatchPerformanceTracker("RouterLookupTargetInDirectory", request.PerformanceLogger))
			{
				using (new ADPerformanceTracker("RouterLookupTargetInDirectory", request.PerformanceLogger))
				{
					if (!string.IsNullOrEmpty(request.TargetSmtpAddress) && SmtpAddress.IsValidSmtpAddress(request.TargetSmtpAddress))
					{
						ADRecipient adrecipient = this.recipientSession.FindByProxyAddress(ProxyAddress.Parse(request.TargetSmtpAddress));
						if (adrecipient != null)
						{
							return adrecipient;
						}
					}
					if (request.TargetAdObjectId != null)
					{
						ADRecipient adrecipient2 = this.recipientSession.Read(request.TargetAdObjectId);
						if (adrecipient2 != null)
						{
							return adrecipient2;
						}
					}
					this.tracer.TraceDebug<string, ADObjectId>((long)this.GetHashCode(), "ROUTING: target not found in directory.  Search params were SMTP-address='{0}' OR ADObjectId='{1}'", request.TargetSmtpAddress, request.TargetAdObjectId);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0004E9F4 File Offset: 0x0004CBF4
		private IPhotoHandler RouteTarget(PhotoRequest request, ADRecipient target)
		{
			this.PopulateTargetInformationIntoRequest(request, target);
			switch (target.RecipientType)
			{
			case RecipientType.UserMailbox:
				return this.RouteTargetWithMailboxInLocalForest(request, (ADUser)target);
			case RecipientType.MailUser:
				return this.RouteTargetInRemoteForest();
			default:
				this.tracer.TraceDebug<RecipientType>((long)this.GetHashCode(), "ROUTING: UNEXPECTED/TOO COMPLEX target.  Recipient type: {0}", target.RecipientType);
				return new TargetNotFoundPhotoHandler(this.configuration, this.tracer);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0004EA68 File Offset: 0x0004CC68
		private void PopulateTargetInformationIntoRequest(PhotoRequest request, ADRecipient target)
		{
			request.TargetRecipient = (request.TargetRecipient ?? target);
			request.TargetAdObjectId = (request.TargetAdObjectId ?? target.Id);
			SmtpAddress primarySmtpAddress = target.PrimarySmtpAddress;
			request.TargetSmtpAddress = (string.IsNullOrEmpty(request.TargetSmtpAddress) ? target.PrimarySmtpAddress.ToString() : request.TargetSmtpAddress);
			request.TargetPrimarySmtpAddress = (string.IsNullOrEmpty(request.TargetPrimarySmtpAddress) ? target.PrimarySmtpAddress.ToString() : request.TargetPrimarySmtpAddress);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0004EB04 File Offset: 0x0004CD04
		private IPhotoHandler RouteTargetWithMailboxInLocalForest(PhotoRequest request, ADUser target)
		{
			if (this.TargetLikelyOnThisServer(request))
			{
				return new LocalServerFallbackToOtherServerPhotoRetrievalPipeline(this.configuration, this.clientInfo, this.recipientSession, this.xsoFactory, this.certificateValidationComponentId, this.serviceLocatorFactory, this.outgoingRequestProxyProvider, this.tracer);
			}
			IPhotoServiceLocator serviceLocator = this.serviceLocatorFactory.CreateForLocalForest(request.PerformanceLogger);
			if (this.IsTargetMailboxOnThisServer(request, target, serviceLocator))
			{
				return this.RouteTargetOnThisServer();
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "ROUTING: target mailbox is in LOCAL forest but OTHER server.");
			return new LocalForestOtherServerPhotoRetrievalPipeline(this.configuration, this.certificateValidationComponentId, serviceLocator, this.recipientSession, this.outgoingRequestProxyProvider, this.tracer);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004EBAF File Offset: 0x0004CDAF
		private IPhotoHandler RouteTargetOnThisServer()
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "ROUTING: target mailbox is on this server.");
			return new LocalServerPhotoRetrievalPipeline(this.configuration, this.clientInfo, this.recipientSession, this.xsoFactory, this.tracer);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0004EBEC File Offset: 0x0004CDEC
		private bool IsTargetMailboxOnThisServer(PhotoRequest request, ADUser target, IPhotoServiceLocator serviceLocator)
		{
			bool result;
			using (new StopwatchPerformanceTracker("RouterCheckTargetMailboxOnThisServer", request.PerformanceLogger))
			{
				using (new ADPerformanceTracker("RouterCheckTargetMailboxOnThisServer", request.PerformanceLogger))
				{
					result = serviceLocator.IsServiceOnThisServer(serviceLocator.Locate(target));
				}
			}
			return result;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0004EC64 File Offset: 0x0004CE64
		private IPhotoHandler RouteTargetInRemoteForest()
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "ROUTING: target is in REMOTE forest.");
			return this.remoteForestPipelineFactory.Create();
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004EC88 File Offset: 0x0004CE88
		private bool TargetLikelyOnThisServer(PhotoRequest request)
		{
			return request.IsTargetMailboxLikelyOnThisServer ?? false;
		}

		// Token: 0x04000A62 RID: 2658
		private readonly PhotosConfiguration configuration;

		// Token: 0x04000A63 RID: 2659
		private readonly string certificateValidationComponentId;

		// Token: 0x04000A64 RID: 2660
		private readonly string clientInfo;

		// Token: 0x04000A65 RID: 2661
		private readonly IRecipientSession recipientSession;

		// Token: 0x04000A66 RID: 2662
		private readonly IPhotoServiceLocatorFactory serviceLocatorFactory;

		// Token: 0x04000A67 RID: 2663
		private readonly IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider;

		// Token: 0x04000A68 RID: 2664
		private readonly IRemoteForestPhotoRetrievalPipelineFactory remoteForestPipelineFactory;

		// Token: 0x04000A69 RID: 2665
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04000A6A RID: 2666
		private readonly ITracer tracer;
	}
}
