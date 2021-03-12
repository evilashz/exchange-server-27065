using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000054 RID: 84
	public class AuthMetadataHttpHandler : IHttpHandler
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000105C1 File Offset: 0x0000E7C1
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000105C4 File Offset: 0x0000E7C4
		private Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AuthMetadataTracer;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000105CC File Offset: 0x0000E7CC
		public void ProcessRequest(HttpContext context)
		{
			this.logger = RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(context);
			try
			{
				this.InternalProcessRequest(context);
			}
			catch (Exception ex)
			{
				this.logger.AppendGenericError("AuthMetadataHttpHandler_UnhandledException", ex.ToString());
				this.Tracer.TraceError<Exception>((long)this.GetHashCode(), "[AuthMetadataHttpHandler.ProcessRequest] Unhandled exception occurred. Error: {0}", ex);
				throw;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010630 File Offset: 0x0000E830
		private void InternalProcessRequest(HttpContext context)
		{
			this.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[AuthMetadataHttpHandler.InternalProcessRequest] Start processing request {0}.", this.logger.ActivityId);
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			this.logger.ActivityScope.Action = AuthMetadataHttpHandler.ProtocolLogAction;
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
				this.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[AuthMetadataHttpHandler.InternalProcessRequest] End processing request {0}.", this.logger.ActivityId);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010724 File Offset: 0x0000E924
		private void HandleBuilderExceptions(HttpResponse response, AuthMetadataBuilderException ex)
		{
			if (ex is AuthMetadataInternalException)
			{
				this.ReportBuilderException(response, ex, true, HttpStatusCode.InternalServerError, null);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00010750 File Offset: 0x0000E950
		private void ReportBuilderException(HttpResponse response, AuthMetadataBuilderException ex, bool logCallStack, HttpStatusCode httpStatusCode, LocalizedString? overridingError)
		{
			this.logger.Set(ServiceCommonMetadata.ErrorCode, ex.GetType().Name);
			string value = logCallStack ? ex.ToString() : ex.Message;
			this.logger.AppendGenericError(ex.GetType().Name, value);
			response.StatusCode = (int)httpStatusCode;
			response.TrySkipIisCustomErrors = true;
			LocalizedString? localizedString = new LocalizedString?(overridingError ?? ex.LocalizedString);
			LocalizedString? localizedString2 = localizedString;
			this.WriteResponse(response, (localizedString2 != null) ? localizedString2.GetValueOrDefault() : null, true);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000107F7 File Offset: 0x0000E9F7
		private void WriteResponse(HttpResponse response, string content, bool isError)
		{
			response.Clear();
			response.ContentType = (isError ? "text/plain" : "text/json");
			response.Charset = "utf-8";
			response.Output.Write(content);
		}

		// Token: 0x04000287 RID: 647
		private static readonly string ProtocolLogAction = "AuthMetadata";

		// Token: 0x04000288 RID: 648
		private static readonly char[] TrailingSlash = new char[]
		{
			'/'
		};

		// Token: 0x04000289 RID: 649
		private RequestDetailsLogger logger;
	}
}
