using System;
using System.IO;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200031E RID: 798
	internal sealed class GetPeopleICommunicateWith : SingleStepServiceCommand<GetPeopleICommunicateWithRequest, Stream>
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x00076844 File Offset: 0x00074A44
		static GetPeopleICommunicateWith()
		{
			CertificateValidationManager.RegisterCallback("GetPeopleICommunicateWith", new RemoteCertificateValidationCallback(CommonCertificateValidationCallbacks.InternalServerToServer));
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0007685C File Offset: 0x00074A5C
		public GetPeopleICommunicateWith(CallContext callContext, GetPeopleICommunicateWithRequest request) : base(callContext, request)
		{
			this.requestId = this.ComputeRequestId();
			this.clientRequestId = this.ComputeClientRequestId();
			this.requestTracer = new InMemoryTracer(ExTraceGlobals.PeopleICommunicateWithTracer.Category, ExTraceGlobals.PeopleICommunicateWithTracer.TraceTag);
			this.tracer = ExTraceGlobals.PeopleICommunicateWithTracer.Compose(this.requestTracer);
			OwsLogRegistry.Register("GetPeopleICommunicateWith", typeof(GetPeopleICommunicateWithMetadata), new Type[0]);
			this.OutgoingResponse = request.OutgoingResponse;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x00076905 File Offset: 0x00074B05
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0007690D File Offset: 0x00074B0D
		private IOutgoingWebResponseContext OutgoingResponse { get; set; }

		// Token: 0x06001697 RID: 5783 RVA: 0x00076918 File Offset: 0x00074B18
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetPeopleICommunicateWithResponseMessage message = new GetPeopleICommunicateWithResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
			GetPeopleICommunicateWithResponse getPeopleICommunicateWithResponse = new GetPeopleICommunicateWithResponse();
			getPeopleICommunicateWithResponse.AddResponse(message);
			return getPeopleICommunicateWithResponse;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0007695C File Offset: 0x00074B5C
		internal override ServiceResult<Stream> Execute()
		{
			ServiceResult<Stream> result;
			try
			{
				this.tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "GetPeopleICommunicateWith.Execute: executing on host {0}.  Request-Id: {1}.  Client-Request-Id: {2}", this.GetIncomingRequestHost(), this.requestId, this.clientRequestId);
				using (new StopwatchPerformanceTracker("GetPeopleICommunicateWithTotal", this.perfLogger))
				{
					using (new ADPerformanceTracker("GetPeopleICommunicateWithTotal", this.perfLogger))
					{
						using (new StorePerformanceTracker("GetPeopleICommunicateWithTotal", this.perfLogger))
						{
							result = this.ExecuteGetPeopleICommunicateWith();
						}
					}
				}
			}
			catch (Exception caughtException)
			{
				result = this.TraceExceptionAndReturnInternalServerError(this.OutgoingResponse, caughtException);
			}
			return result;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00076A40 File Offset: 0x00074C40
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

		// Token: 0x0600169A RID: 5786 RVA: 0x00076AD4 File Offset: 0x00074CD4
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

		// Token: 0x0600169B RID: 5787 RVA: 0x00076B44 File Offset: 0x00074D44
		private ServiceResult<Stream> ExecuteGetPeopleICommunicateWith()
		{
			if (base.CallContext.AccessingPrincipal == null || base.CallContext.AccessingPrincipal.MailboxInfo == null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "CallContext.AccessingPrincipal=null, InboxRulesCommandBaseRequest.MailboxSmtpAddress not specified. NonExistentMailbox error returned.");
				throw new NonExistentMailboxException((CoreResources.IDs)2489326695U, (base.CallContext.EffectiveCaller == null) ? string.Empty : base.CallContext.EffectiveCaller.PrimarySmtpAddress);
			}
			base.CallContext.ProtocolLog.Set(GetPeopleICommunicateWithMetadata.TargetEmailAddress, base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
			MemoryStream memoryStream = new MemoryStream();
			ServiceResult<Stream> result;
			using (DisposeGuard disposeGuard = memoryStream.Guard())
			{
				byte[] array = new byte[]
				{
					1
				};
				memoryStream.Write(array, 0, array.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				string text = "application/octet-stream";
				this.OutgoingResponse.ContentType = text;
				base.CallContext.ProtocolLog.Set(GetPeopleICommunicateWithMetadata.ResponseContentType, text);
				this.OutgoingResponse.StatusCode = HttpStatusCode.OK;
				this.tracer.TraceDebug<HttpStatusCode, string, int>((long)this.GetHashCode(), "GetPeopleICommunicateWith: request completed.  Status: {0};  Content-Type: {1};  Content-Length: {2}", this.OutgoingResponse.StatusCode, text, array.Length);
				disposeGuard.Success();
				result = new ServiceResult<Stream>(memoryStream);
			}
			return result;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00076CB0 File Offset: 0x00074EB0
		private string GetIncomingRequestHost()
		{
			if (base.CallContext == null || base.CallContext.HttpContext == null || base.CallContext.HttpContext.Request == null || base.CallContext.HttpContext.Request.Headers == null)
			{
				return string.Empty;
			}
			return base.CallContext.HttpContext.Request.Headers["Host"];
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00076D20 File Offset: 0x00074F20
		private ServiceResult<Stream> TraceExceptionAndReturnInternalServerError(IOutgoingWebResponseContext webContext, Exception caughtException)
		{
			this.tracer.TraceError<Exception>((long)this.GetHashCode(), "Request failed with exception: {0}", caughtException);
			webContext.StatusCode = HttpStatusCode.InternalServerError;
			base.CallContext.ProtocolLog.Set(GetPeopleICommunicateWithMetadata.GetPeopleICommunicateWithFailed, true);
			return new ServiceResult<Stream>(new MemoryStream(Array<byte>.Empty));
		}

		// Token: 0x04000F29 RID: 3881
		private const string CertificateValidationComponentId = "GetPeopleICommunicateWith";

		// Token: 0x04000F2A RID: 3882
		private const string HttpGetContentType = "application/octet-stream";

		// Token: 0x04000F2B RID: 3883
		private const string ActionName = "GetPeopleICommunicateWith";

		// Token: 0x04000F2C RID: 3884
		private const string GetPeopleICommunicateWithTotalPerformanceMarker = "GetPeopleICommunicateWithTotal";

		// Token: 0x04000F2D RID: 3885
		private const string HttpHostHeader = "Host";

		// Token: 0x04000F2E RID: 3886
		private readonly IPerformanceDataLogger perfLogger = NullPerformanceDataLogger.Instance;

		// Token: 0x04000F2F RID: 3887
		private readonly string requestId;

		// Token: 0x04000F30 RID: 3888
		private readonly string clientRequestId;

		// Token: 0x04000F31 RID: 3889
		private readonly ITracer tracer = ExTraceGlobals.PeopleICommunicateWithTracer;

		// Token: 0x04000F32 RID: 3890
		private readonly ITracer requestTracer = NullTracer.Instance;
	}
}
