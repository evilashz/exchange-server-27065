using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x02000140 RID: 320
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RemoteForestPhotoHandler : IPhotoHandler
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x00025A95 File Offset: 0x00023C95
		public RemoteForestPhotoHandler(PhotosConfiguration configuration, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.configuration = configuration;
			this.tracer = upstreamTracer;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00025AC4 File Offset: 0x00023CC4
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("response", response);
			PhotoResponse result;
			using (new StopwatchPerformanceTracker("RemoteForestHandlerTotal", request.PerformanceLogger))
			{
				using (new ADPerformanceTracker("RemoteForestHandlerTotal", request.PerformanceLogger))
				{
					if (request.ShouldSkipHandlers(PhotoHandlers.RemoteForest))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "REMOTE FOREST HANDLER: skipped by request.");
						result = response;
					}
					else if (response.Served)
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "REMOTE FOREST HANDLER: skipped because photo has already been served by an upstream handler.");
						result = response;
					}
					else
					{
						response.RemoteForestHandlerProcessed = true;
						request.PerformanceLogger.Log("RemoteForestHandlerProcessed", string.Empty, 1U);
						GetUserPhotoQuery getUserPhotoQuery;
						using (new StopwatchPerformanceTracker("QueryCreation", request.PerformanceLogger))
						{
							using (new ADPerformanceTracker("QueryCreation", request.PerformanceLogger))
							{
								getUserPhotoQuery = new GetUserPhotoQuery((ClientContext)request.ClientContextForRemoteForestRequests, request, null, request.RequestorFromExternalOrganization, this.configuration, this.tracer);
							}
						}
						try
						{
							byte[] array = getUserPhotoQuery.Execute();
							if (RemoteForestPhotoHandler.IsInvalidHttpStatusCode(getUserPhotoQuery.StatusCode))
							{
								result = this.RespondWithErrorBecauseQueryReturnedInvalidStatusCode(request, response);
							}
							else
							{
								response.Served = true;
								response.Status = getUserPhotoQuery.StatusCode;
								response.ETag = getUserPhotoQuery.ETag;
								response.HttpExpiresHeader = getUserPhotoQuery.Expires;
								response.ContentType = getUserPhotoQuery.ContentType;
								this.tracer.TraceDebug((long)this.GetHashCode(), "REMOTE FOREST HANDLER:  query completed.  Result is empty? {0}; HTTP status: {1}; ETag: {2}; Content-Type: {3}; HTTP Expires: {4}", new object[]
								{
									array == null || array.Length == 0,
									response.Status,
									getUserPhotoQuery.ETag,
									getUserPhotoQuery.ContentType,
									getUserPhotoQuery.Expires
								});
								using (MemoryStream memoryStream = new MemoryStream((array == null) ? Array<byte>.Empty : array))
								{
									memoryStream.CopyTo(response.OutputPhotoStream);
								}
								request.PerformanceLogger.Log("RemoteForestHandlerServed", string.Empty, 1U);
								result = response;
							}
						}
						catch (ClientDisconnectedException arg)
						{
							this.tracer.TraceDebug<ClientDisconnectedException>((long)this.GetHashCode(), "REMOTE FOREST HANDLER: client disconnected.  Exception: {0}", arg);
							result = this.ServePhotoWhenClientDisconnected(request, response);
						}
						catch (AccessDeniedException arg2)
						{
							this.tracer.TraceDebug<AccessDeniedException>((long)this.GetHashCode(), "REMOTE FOREST HANDLER: access denied.  Requestor does NOT have permission to retrieve this photo.  Exception: {0}", arg2);
							result = this.RespondWithAccessDenied(request, response);
						}
						finally
						{
							if (getUserPhotoQuery != null && getUserPhotoQuery.RequestLogger != null && getUserPhotoQuery.RequestLogger.LogData != null)
							{
								string text = getUserPhotoQuery.RequestLogger.LogData.ToString();
								if (!string.IsNullOrEmpty(text))
								{
									this.tracer.TracePerformance<string>((long)this.GetHashCode(), "REMOTE FOREST HANDLER: {0}", text);
									request.PerformanceLogger.Log("MiscRoutingAndDiscovery", string.Empty, text);
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00025E80 File Offset: 0x00024080
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00025E89 File Offset: 0x00024089
		private static bool IsInvalidHttpStatusCode(HttpStatusCode code)
		{
			return code == (HttpStatusCode)0;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00025E8F File Offset: 0x0002408F
		private PhotoResponse ServePhotoWhenClientDisconnected(PhotoRequest request, PhotoResponse response)
		{
			response.Served = true;
			response.Status = HttpStatusCode.InternalServerError;
			request.PerformanceLogger.Log("RemoteForestHandlerServed", string.Empty, 1U);
			return response;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00025EBA File Offset: 0x000240BA
		private PhotoResponse RespondWithAccessDenied(PhotoRequest request, PhotoResponse response)
		{
			response.Served = true;
			response.Status = HttpStatusCode.Forbidden;
			request.PerformanceLogger.Log("RemoteForestHandlerServed", string.Empty, 1U);
			return response;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00025EE8 File Offset: 0x000240E8
		private PhotoResponse RespondWithErrorBecauseQueryReturnedInvalidStatusCode(PhotoRequest request, PhotoResponse response)
		{
			this.tracer.TraceError((long)this.GetHashCode(), "REMOTE FOREST HANDLER: HTTP status code in query is invalid.  Overwriting with InternalServerError.");
			request.PerformanceLogger.Log("RemoteForestHandlerError", string.Empty, 1U);
			response.Served = false;
			response.Status = HttpStatusCode.InternalServerError;
			return response;
		}

		// Token: 0x040006D2 RID: 1746
		private readonly PhotosConfiguration configuration;

		// Token: 0x040006D3 RID: 1747
		private readonly ITracer tracer;
	}
}
