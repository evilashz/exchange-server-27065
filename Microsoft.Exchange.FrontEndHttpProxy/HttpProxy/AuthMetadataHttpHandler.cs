using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000A5 RID: 165
	internal class AuthMetadataHttpHandler : IHttpHandler
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x00024CF3 File Offset: 0x00022EF3
		public AuthMetadataHttpHandler(HttpContext context)
		{
			this.httpContext = context;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00024D02 File Offset: 0x00022F02
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00024D05 File Offset: 0x00022F05
		private Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AuthMetadataTracer;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00024D0C File Offset: 0x00022F0C
		private RequestDetailsLogger Logger
		{
			get
			{
				return RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(this.httpContext);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00024D1C File Offset: 0x00022F1C
		public void ProcessRequest(HttpContext context)
		{
			try
			{
				this.InternalProcessRequest(context);
			}
			catch (Exception ex)
			{
				this.Logger.AppendGenericError("AuthMetadataHttpHandler_UnhandledException", ex.ToString());
				this.Tracer.TraceError<Exception>((long)this.GetHashCode(), "[AuthMetadataHttpHandler.ProcessRequest] Unhandled exception occurred. Error: {0}", ex);
				throw;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00024D74 File Offset: 0x00022F74
		private void InternalProcessRequest(HttpContext context)
		{
			this.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[AuthMetadataHttpHandler.InternalProcessRequest] Start processing request {0}.", this.Logger.ActivityId);
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			this.Logger.ActivityScope.Action = AuthMetadataHttpHandler.ProtocolLogAction;
			try
			{
				string content = null;
				bool isError = true;
				if (request.Url.ToString().TrimEnd(AuthMetadataHttpHandler.TrailingSlash).EndsWith("metadata/json/1", StringComparison.OrdinalIgnoreCase))
				{
					isError = false;
					content = AuthMetadataBuilder.Singleton.Build(request.Url);
				}
				else
				{
					response.StatusCode = 404;
				}
				this.WriteResponse(response, content, isError);
			}
			catch (AuthMetadataBuilderException ex)
			{
				this.HandleBuilderExceptions(response, ex);
			}
			finally
			{
				this.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[AuthMetadataHttpHandler.InternalProcessRequest] End processing request {0}.", this.Logger.ActivityId);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00024E68 File Offset: 0x00023068
		private void HandleBuilderExceptions(HttpResponse response, AuthMetadataBuilderException ex)
		{
			if (ex is AuthMetadataInternalException)
			{
				this.ReportBuilderException(response, ex, true, HttpStatusCode.InternalServerError, new LocalizedString?(HttpProxyStrings.ErrorInternalServerError));
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00024E8C File Offset: 0x0002308C
		private void ReportBuilderException(HttpResponse response, AuthMetadataBuilderException ex, bool logCallStack, HttpStatusCode httpStatusCode, LocalizedString? overridingError)
		{
			this.Logger.Set(ServiceCommonMetadata.ErrorCode, ex.GetType().Name);
			string value = logCallStack ? ex.ToString() : ex.Message;
			this.Logger.AppendGenericError(ex.GetType().Name, value);
			response.StatusCode = (int)httpStatusCode;
			response.TrySkipIisCustomErrors = true;
			LocalizedString? localizedString = new LocalizedString?(overridingError ?? ex.LocalizedString);
			LocalizedString? localizedString2 = localizedString;
			this.WriteResponse(response, (localizedString2 != null) ? localizedString2.GetValueOrDefault() : null, true);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00024F33 File Offset: 0x00023133
		private void WriteResponse(HttpResponse response, string content, bool isError)
		{
			response.Clear();
			response.ContentType = (isError ? "text/plain" : "text/json");
			response.Charset = "utf-8";
			response.Output.Write(content);
		}

		// Token: 0x0400040F RID: 1039
		private static readonly string ProtocolLogAction = "AuthMetadata";

		// Token: 0x04000410 RID: 1040
		private static readonly char[] TrailingSlash = new char[]
		{
			'/'
		};

		// Token: 0x04000411 RID: 1041
		private HttpContext httpContext;
	}
}
