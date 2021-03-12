using System;
using System.ComponentModel;
using System.IO;
using System.Net;
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
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000350 RID: 848
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestHandler : IHttpHandler
	{
		// Token: 0x060017D9 RID: 6105 RVA: 0x0008028C File Offset: 0x0007E48C
		public PhotoRequestHandler(RequestDetailsLogger protocolLogger)
		{
			ArgumentValidator.ThrowIfNull("protocolLogger", protocolLogger);
			this.protocolLogger = protocolLogger;
			this.InitializeProtocolLogger();
			OwsLogRegistry.Register("GetUserPhoto", typeof(PhotosLoggingMetadata), new Type[0]);
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000802F2 File Offset: 0x0007E4F2
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000802F8 File Offset: 0x0007E4F8
		public void ProcessRequest(HttpContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			context.Response.TrySkipIisCustomErrors = true;
			this.LogPreExecution();
			this.InitializeRequestTrackingInformation(context);
			this.InitializeTracers(context);
			this.perfLogger = new PhotoRequestPerformanceLogger(this.protocolLogger, this.requestTracer);
			try
			{
				this.tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "PHOTO REQUEST HANDLER: executing on host {0}.  Request-Id: {1}.  Client-Request-Id: {2}", this.GetIncomingRequestHost(context), this.requestId, this.clientRequestId);
				using (new StopwatchPerformanceTracker("GetUserPhotoTotal", this.perfLogger))
				{
					using (new ADPerformanceTracker("GetUserPhotoTotal", this.perfLogger))
					{
						using (new StorePerformanceTracker("GetUserPhotoTotal", this.perfLogger))
						{
							this.request = this.AssemblePhotoRequest(context);
							using (MemoryStream memoryStream = new MemoryStream())
							{
								PhotoResponse photoResponse = new OrganizationalPhotoRetrievalPipeline(PhotoRequestHandler.PhotosConfiguration, "GetUserPhoto", this.GetMailboxSessionClientInfo(context), this.CreateRecipientSession(this.request.Requestor.OrganizationId), this.GetProxyProviderForOutboundRequest(), this.GetRemoteForestPipelineFactory(), XSOFactory.Default, this.tracer).Then(new TransparentImagePhotoHandler(PhotoRequestHandler.PhotosConfiguration, this.tracer)).Retrieve(this.request, new PhotoResponse(memoryStream));
								context.Response.StatusCode = (int)photoResponse.Status;
								context.Response.Headers["ETag"] = photoResponse.ETag;
								context.Response.ContentType = photoResponse.ContentType;
								string text = string.IsNullOrEmpty(photoResponse.HttpExpiresHeader) ? UserAgentPhotoExpiresHeader.Default.ComputeExpiresHeader(DateTime.UtcNow, photoResponse.Status, PhotoRequestHandler.PhotosConfiguration) : photoResponse.HttpExpiresHeader;
								context.Response.Headers["Expires"] = text;
								memoryStream.Seek(0L, SeekOrigin.Begin);
								this.tracer.TraceDebug((long)this.GetHashCode(), "PHOTO REQUEST HANDLER: request completed.  Status: {0};  ETag: {1};  Content-Type: {2};  Content-Length: {3};  Expires: {4}", new object[]
								{
									photoResponse.Status,
									photoResponse.ETag,
									photoResponse.ContentType,
									photoResponse.ContentLength,
									text
								});
								memoryStream.WriteTo(context.Response.OutputStream);
								this.protocolLogger.Set(PhotosLoggingMetadata.ServedByPhotoRequestHandler, 1);
							}
						}
					}
				}
			}
			catch (TooComplexPhotoRequestException arg)
			{
				this.tracer.TraceDebug<TooComplexPhotoRequestException>((long)this.GetHashCode(), "PHOTO REQUEST HANDLER: request too complex.  Exception: {0}", arg);
				throw;
			}
			catch (IOException caughtException)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException);
			}
			catch (Win32Exception caughtException2)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException2);
			}
			catch (UnauthorizedAccessException caughtException3)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException3);
			}
			catch (TimeoutException caughtException4)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException4);
			}
			catch (StorageTransientException caughtException5)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException5);
			}
			catch (StoragePermanentException caughtException6)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException6);
			}
			catch (TransientException caughtException7)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException7);
			}
			catch (ADOperationException caughtException8)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException8);
			}
			catch (ArgumentException caughtException9)
			{
				this.TraceErrorAndReturnCode(context, caughtException9, HttpStatusCode.BadRequest);
			}
			catch (Exception caughtException10)
			{
				this.TraceErrorAndReturnInternalServerError(context, caughtException10);
				throw;
			}
			finally
			{
				this.LogRequestTraces(context);
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000807AC File Offset: 0x0007E9AC
		private PhotoRequest AssemblePhotoRequest(HttpContext context)
		{
			PhotoRequest photoRequest = this.CreatePhotoRequestFromContext(context);
			if (this.IsSelfPhotoRequest(photoRequest))
			{
				throw new TooComplexPhotoRequestException();
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "PHOTO REQUEST HANDLER: assembled request for ARBITRARY photo.");
			photoRequest.RequestorFromExternalOrganization = false;
			photoRequest.HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(PhotoRequestHandler.NoHostOwnedMailboxSession);
			photoRequest.ClientRequestId = this.clientRequestId;
			photoRequest.IsTargetMailboxLikelyOnThisServer = new bool?(this.LikelyRoutedToTargetMailboxByFrontend(context));
			return photoRequest;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0008081F File Offset: 0x0007EA1F
		private void InitializeTracers(HttpContext context)
		{
			this.requestTracer = new InMemoryTracer(ExTraceGlobals.UserPhotosTracer.Category, ExTraceGlobals.UserPhotosTracer.TraceTag);
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(this.requestTracer);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00080858 File Offset: 0x0007EA58
		private void LogRequestTraces(HttpContext context)
		{
			if (!this.ShouldTraceRequest(context))
			{
				return;
			}
			PhotosDiagnostics.Instance.StampTracesOnGetUserPhotosResponse(this.requestTracer, context.Response);
			if (this.requestTracer != null && !NullTracer.Instance.Equals(this.requestTracer))
			{
				((InMemoryTracer)this.requestTracer).Dump(new PhotoRequestLogWriter(PhotoRequestHandler.RequestLog, this.requestId));
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000808BF File Offset: 0x0007EABF
		private bool ShouldTraceRequest(HttpContext context)
		{
			return (this.request != null && this.request.Trace) || PhotosDiagnostics.Instance.ShouldTraceGetUserPhotoRequest(context.Request);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000808E8 File Offset: 0x0007EAE8
		private bool LikelyRoutedToTargetMailboxByFrontend(HttpContext context)
		{
			if (context.Request == null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "PHOTO REQUEST HANDLER: cannot determine whether request was likely routed by frontend because context has not been initialized.");
				return false;
			}
			return !context.Request.Path.EndsWith("/GetPersonaPhoto", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00080924 File Offset: 0x0007EB24
		private static IMailboxSession NoHostOwnedMailboxSession(ExchangePrincipal target)
		{
			return null;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00080928 File Offset: 0x0007EB28
		private string ComputeRequestId(HttpContext context)
		{
			if (!string.IsNullOrEmpty(this.requestId))
			{
				return this.requestId;
			}
			if (this.protocolLogger == null)
			{
				return Guid.NewGuid().ToString();
			}
			if (!Guid.Empty.Equals(this.protocolLogger.ActivityId))
			{
				return this.protocolLogger.ActivityId.ToString();
			}
			return Guid.NewGuid().ToString();
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000809AC File Offset: 0x0007EBAC
		private string ComputeClientRequestId(HttpContext context)
		{
			if (!string.IsNullOrEmpty(this.clientRequestId))
			{
				return this.clientRequestId;
			}
			if (this.protocolLogger == null || this.protocolLogger.ActivityScope == null)
			{
				return this.ComputeRequestId(context);
			}
			string text = this.protocolLogger.ActivityScope.ClientRequestId;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return this.ComputeRequestId(context);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00080A0C File Offset: 0x0007EC0C
		private void InitializeRequestTrackingInformation(HttpContext context)
		{
			this.requestId = this.ComputeRequestId(context);
			this.clientRequestId = this.ComputeClientRequestId(context);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00080A28 File Offset: 0x0007EC28
		private string GetMailboxSessionClientInfo(HttpContext context)
		{
			return string.Format("{0};{1};Action=GetUserPhoto", Global.DefaultMapiClientType, string.IsNullOrEmpty(context.Request.UserAgent) ? "UserAgent=[NoUserAgent]" : context.Request.UserAgent);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00080A5D File Offset: 0x0007EC5D
		private IPhotoRequestOutboundWebProxyProvider GetProxyProviderForOutboundRequest()
		{
			return new PhotoRequestOutboundWebProxyProviderUsingLocalServerConfiguration(this.tracer);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00080A6A File Offset: 0x0007EC6A
		private IRecipientSession CreateRecipientSession(OrganizationId organizationId)
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 525, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\PhotoRequestHandler.cs");
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00080A90 File Offset: 0x0007EC90
		private PhotoRequest CreatePhotoRequestFromContext(HttpContext context)
		{
			PhotoRequest photoRequest = new HttpPhotoRequestBuilder(PhotoRequestHandler.PhotosConfiguration, this.tracer).Parse(context.Request, this.perfLogger);
			photoRequest.Requestor = this.ReadRequestorFromContext(context);
			return photoRequest;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00080AD0 File Offset: 0x0007ECD0
		private bool IsSelfPhotoRequest(PhotoRequest request)
		{
			PhotoPrincipal other = new PhotoPrincipal
			{
				EmailAddresses = new string[]
				{
					request.TargetSmtpAddress,
					request.TargetPrimarySmtpAddress
				}
			};
			return request.Requestor.IsSame(other);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00080B14 File Offset: 0x0007ED14
		private PhotoPrincipal ReadRequestorFromContext(HttpContext context)
		{
			PhotoPrincipal photoPrincipal = new PhotoRequestorReader().Read(context);
			if (photoPrincipal == null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "PHOTO REQUEST HANDLER:  bad request:  requestor MISSING.");
				throw new ArgumentException();
			}
			return photoPrincipal;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00080B4E File Offset: 0x0007ED4E
		private void TraceErrorAndReturnInternalServerError(HttpContext context, Exception caughtException)
		{
			this.TraceErrorAndReturnCode(context, caughtException, HttpStatusCode.InternalServerError);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00080B5D File Offset: 0x0007ED5D
		private void TraceErrorAndReturnCode(HttpContext context, Exception caughtException, HttpStatusCode code)
		{
			this.tracer.TraceError<Exception>((long)this.GetHashCode(), "PHOTO REQUEST HANDLER: request failed with exception: {0}", caughtException);
			context.Response.StatusCode = (int)code;
			this.protocolLogger.Set(PhotosLoggingMetadata.GetUserPhotoFailed, true);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00080B9C File Offset: 0x0007ED9C
		private string GetIncomingRequestHost(HttpContext context)
		{
			if (context.Request.Headers == null)
			{
				return string.Empty;
			}
			return context.Request.Headers["Host"];
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00080BC6 File Offset: 0x0007EDC6
		private void InitializeProtocolLogger()
		{
			if (this.protocolLogger == null || this.protocolLogger.ActivityScope == null)
			{
				return;
			}
			this.protocolLogger.ActivityScope.SetProperty(ExtensibleLoggerMetadata.EventId, "GetUserPhoto");
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00080BF9 File Offset: 0x0007EDF9
		private void LogPreExecution()
		{
			if (this.protocolLogger == null || this.protocolLogger.ActivityScope == null)
			{
				return;
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.protocolLogger, ServiceLatencyMetadata.PreExecutionLatency, this.protocolLogger.ActivityScope.TotalMilliseconds);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00080C37 File Offset: 0x0007EE37
		private IRemoteForestPhotoRetrievalPipelineFactory GetRemoteForestPipelineFactory()
		{
			return new RemoteForestPhotoRetrievalPipelineTooComplex(this.tracer);
		}

		// Token: 0x04001004 RID: 4100
		private const string ExpiresHeaderName = "Expires";

		// Token: 0x04001005 RID: 4101
		private const string ETagHeaderName = "ETag";

		// Token: 0x04001006 RID: 4102
		private const string HttpHostHeader = "Host";

		// Token: 0x04001007 RID: 4103
		private const string CertificateValidationComponentId = "GetUserPhoto";

		// Token: 0x04001008 RID: 4104
		private const string GetPersonaPhotoRequestPathSuffix = "/GetPersonaPhoto";

		// Token: 0x04001009 RID: 4105
		private const string GetUserPhotoActionName = "GetUserPhoto";

		// Token: 0x0400100A RID: 4106
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x0400100B RID: 4107
		private static readonly PhotoRequestLog RequestLog = new PhotoRequestLogFactory(PhotoRequestHandler.PhotosConfiguration, ExchangeSetupContext.InstalledVersion.ToString()).Create();

		// Token: 0x0400100C RID: 4108
		private readonly RequestDetailsLogger protocolLogger;

		// Token: 0x0400100D RID: 4109
		private IPerformanceDataLogger perfLogger = NullPerformanceDataLogger.Instance;

		// Token: 0x0400100E RID: 4110
		private string requestId;

		// Token: 0x0400100F RID: 4111
		private string clientRequestId;

		// Token: 0x04001010 RID: 4112
		private ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04001011 RID: 4113
		private ITracer requestTracer = NullTracer.Instance;

		// Token: 0x04001012 RID: 4114
		private PhotoRequest request;
	}
}
