using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001C5 RID: 453
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADPhotoHandler : IPhotoHandler
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x00047864 File Offset: 0x00045A64
		public ADPhotoHandler(IADPhotoReader reader, IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("reader", reader);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.reader = reader;
			this.recipientSession = recipientSession;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000478C4 File Offset: 0x00045AC4
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			PhotoResponse result;
			using (new StopwatchPerformanceTracker("ADHandlerTotal", request.PerformanceLogger))
			{
				using (new ADPerformanceTracker("ADHandlerTotal", request.PerformanceLogger))
				{
					if (request.ShouldSkipHandlers(PhotoHandlers.ActiveDirectory))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "AD HANDLER: skipped by request.");
						result = response;
					}
					else if (response.Served)
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "AD HANDLER: skipped because photo has already been served by an upstream handler.");
						result = response;
					}
					else
					{
						response.ADHandlerProcessed = true;
						request.PerformanceLogger.Log("ADHandlerProcessed", string.Empty, 1U);
						try
						{
							result = this.FindTargetAndReadPhoto(request, response);
						}
						catch (ADNoSuchObjectException arg)
						{
							this.tracer.TraceDebug<ADNoSuchObjectException>((long)this.GetHashCode(), "AD HANDLER: no photo.  Exception: {0}", arg);
							request.PerformanceLogger.Log("ADHandlerPhotoAvailable", string.Empty, 0U);
							result = response;
						}
						catch (TransientException arg2)
						{
							this.tracer.TraceError<TransientException>((long)this.GetHashCode(), "AD HANDLER: transient exception at reading photo.  Exception: {0}", arg2);
							request.PerformanceLogger.Log("ADHandlerError", string.Empty, 1U);
							throw;
						}
						catch (ADOperationException arg3)
						{
							this.tracer.TraceError<ADOperationException>((long)this.GetHashCode(), "AD HANDLER: AD exception at reading photo.  Exception: {0}", arg3);
							request.PerformanceLogger.Log("ADHandlerError", string.Empty, 1U);
							throw;
						}
						catch (IOException arg4)
						{
							this.tracer.TraceError<IOException>((long)this.GetHashCode(), "AD HANDLER: I/O exception at reading photo.  Exception: {0}", arg4);
							request.PerformanceLogger.Log("ADHandlerError", string.Empty, 1U);
							throw;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00047AEC File Offset: 0x00045CEC
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00047AF5 File Offset: 0x00045CF5
		private PhotoResponse FindTargetAndReadPhoto(PhotoRequest request, PhotoResponse response)
		{
			this.ComputeTargetADObjectIdAndStampOntoRequest(request);
			if (request.TargetAdObjectId == null)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "AD HANDLER: cannot serve photo because target AD object ID has not been initialized.");
				return response;
			}
			return this.ReadPhotoOntoResponse(request, response);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00047B28 File Offset: 0x00045D28
		private void ComputeTargetADObjectIdAndStampOntoRequest(PhotoRequest request)
		{
			if (request.TargetAdObjectId != null)
			{
				return;
			}
			if (request.TargetPrincipal != null && request.TargetPrincipal.ObjectId != null)
			{
				request.TargetAdObjectId = request.TargetPrincipal.ObjectId;
				return;
			}
			if (string.IsNullOrEmpty(request.TargetSmtpAddress) || !SmtpAddress.IsValidSmtpAddress(request.TargetSmtpAddress))
			{
				return;
			}
			ADRecipient adrecipient = this.recipientSession.FindByProxyAddress(ProxyAddress.Parse(request.TargetSmtpAddress));
			if (adrecipient == null)
			{
				return;
			}
			request.TargetAdObjectId = adrecipient.Id;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00047BA8 File Offset: 0x00045DA8
		private PhotoResponse ReadPhotoOntoResponse(PhotoRequest request, PhotoResponse response)
		{
			using (new StopwatchPerformanceTracker("ADHandlerReadPhoto", request.PerformanceLogger))
			{
				using (new ADPerformanceTracker("ADHandlerReadPhoto", request.PerformanceLogger))
				{
					PhotoMetadata photoMetadata = this.reader.Read(this.recipientSession, request.TargetAdObjectId, response.OutputPhotoStream);
					response.Served = true;
					response.Status = HttpStatusCode.OK;
					response.ContentLength = photoMetadata.Length;
					response.ContentType = photoMetadata.ContentType;
					response.Thumbprint = null;
					request.PerformanceLogger.Log("ADHandlerPhotoAvailable", string.Empty, 1U);
					request.PerformanceLogger.Log("ADHandlerPhotoServed", string.Empty, 1U);
				}
			}
			return response;
		}

		// Token: 0x0400092B RID: 2347
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x0400092C RID: 2348
		private readonly IADPhotoReader reader;

		// Token: 0x0400092D RID: 2349
		private readonly IRecipientSession recipientSession;
	}
}
