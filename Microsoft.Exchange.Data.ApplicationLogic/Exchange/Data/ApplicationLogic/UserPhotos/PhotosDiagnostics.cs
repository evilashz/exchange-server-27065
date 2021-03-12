using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200020E RID: 526
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotosDiagnostics
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x0004F143 File Offset: 0x0004D343
		private PhotosDiagnostics()
		{
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004F14B File Offset: 0x0004D34B
		public bool ShouldTraceGetPersonaPhotoRequest(HttpRequest request)
		{
			return request != null && request.QueryString.AllKeys.Contains("trace", StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004F16C File Offset: 0x0004D36C
		public bool ShouldTraceGetUserPhotoRequest(HttpRequest request)
		{
			return request != null && (request.QueryString.AllKeys.Contains("trace", StringComparer.OrdinalIgnoreCase) || request.Headers.AllKeys.Contains("X-Exchange-GetUserPhoto-TraceEnabled", StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004F1AB File Offset: 0x0004D3AB
		public void StampTracesOnGetPersonaPhotosResponse(ITracer tracer, OutgoingWebResponseContext response)
		{
			if (tracer == null || response == null)
			{
				return;
			}
			this.StampTracesOnPhotosResponse(tracer, response.Headers, "X-Exchange-GetPersonaPhoto-Traces");
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004F1C6 File Offset: 0x0004D3C6
		public void StampTracesOnGetUserPhotosResponse(ITracer tracer, OutgoingWebResponseContext response)
		{
			if (tracer == null || response == null)
			{
				return;
			}
			this.StampTracesOnPhotosResponse(tracer, response.Headers, "X-Exchange-GetUserPhoto-Traces");
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004F1E1 File Offset: 0x0004D3E1
		public void StampTracesOnGetUserPhotosResponse(ITracer tracer, HttpResponse response)
		{
			if (tracer == null || response == null)
			{
				return;
			}
			this.StampTracesOnPhotosResponse(tracer, response.Headers, "X-Exchange-GetUserPhoto-Traces");
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004F1FC File Offset: 0x0004D3FC
		public void StampGetUserPhotoTraceEnabledHeaders(Dictionary<string, string> headers)
		{
			if (headers == null)
			{
				return;
			}
			headers.Add("X-Exchange-GetUserPhoto-TraceEnabled", "1");
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004F212 File Offset: 0x0004D412
		public void StampGetUserPhotoTraceEnabledHeaders(WebRequest request)
		{
			if (request == null)
			{
				return;
			}
			request.Headers.Set("X-Exchange-GetUserPhoto-TraceEnabled", "1");
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004F230 File Offset: 0x0004D430
		public string ReadGetUserPhotoTracesFromResponseHeaders(Dictionary<string, string> headers)
		{
			if (headers == null)
			{
				return string.Empty;
			}
			string text;
			if (!headers.TryGetValue("X-Exchange-GetUserPhoto-Traces", out text) || string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004F264 File Offset: 0x0004D464
		public string ReadGetUserPhotoTracesFromResponse(WebResponse response)
		{
			if (response == null || response.Headers == null || response.Headers.Count == 0)
			{
				return string.Empty;
			}
			return response.Headers["X-Exchange-GetUserPhoto-Traces"] ?? string.Empty;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004F2A0 File Offset: 0x0004D4A0
		public PhotoHandlers GetHandlersToSkip(HttpRequest request)
		{
			if (request == null)
			{
				return PhotoHandlers.None;
			}
			PhotoHandlers photoHandlers = PhotoHandlers.None;
			photoHandlers |= PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.FileSystem, "skipfs");
			photoHandlers |= PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.Mailbox, "skipmbx");
			photoHandlers |= PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.ActiveDirectory, "skipad");
			photoHandlers |= PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.Caching, "skipcaching");
			photoHandlers |= PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.Http, "skiphttp");
			photoHandlers |= PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.Private, "skipprv");
			return photoHandlers | PhotosDiagnostics.IsHandlerToBeSkipped(request, PhotoHandlers.RemoteForest, "skiprf");
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0004F324 File Offset: 0x0004D524
		public string GetHandlersToSkipQueryStringParametersWithLeadingAmpersand(PhotoRequest request)
		{
			if (request == null || request.HandlersToSkip == PhotoHandlers.None)
			{
				return string.Empty;
			}
			PhotoHandlers handlersToSkip = request.HandlersToSkip;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.FileSystem, "&skipfs=1", handlersToSkip));
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.Mailbox, "&skipmbx=1", handlersToSkip));
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.ActiveDirectory, "&skipad=1", handlersToSkip));
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.Caching, "&skipcaching=1", handlersToSkip));
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.Http, "&skiphttp=1", handlersToSkip));
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.Private, "&skipprv=1", handlersToSkip));
			stringBuilder.Append(PhotosDiagnostics.GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers.RemoteForest, "&skiprf=1", handlersToSkip));
			return stringBuilder.ToString();
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004F3DD File Offset: 0x0004D5DD
		private static PhotoHandlers IsHandlerToBeSkipped(HttpRequest request, PhotoHandlers handler, string skipHandlerQueryStringParameter)
		{
			if (!request.QueryString.AllKeys.Contains(skipHandlerQueryStringParameter, StringComparer.OrdinalIgnoreCase))
			{
				return PhotoHandlers.None;
			}
			return handler;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004F3FA File Offset: 0x0004D5FA
		private static string GetSkipHandlerQueryParameterWithLeadingAmpersand(PhotoHandlers handlerToTest, string skipHandlerQueryStringParameterWithLeadingAmpersand, PhotoHandlers handlersToSkip)
		{
			if ((handlersToSkip & handlerToTest) != PhotoHandlers.None)
			{
				return skipHandlerQueryStringParameterWithLeadingAmpersand;
			}
			return string.Empty;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004F408 File Offset: 0x0004D608
		private void StampTracesOnPhotosResponse(ITracer tracer, NameValueCollection responseHeaders, string header)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				tracer.Dump(stringWriter, true, true);
			}
			responseHeaders[header] = this.SanitizeHttpHeaderValue(stringBuilder.ToString());
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004F460 File Offset: 0x0004D660
		private string SanitizeHttpHeaderValue(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			foreach (char c in s)
			{
				if (c == '\r')
				{
					stringBuilder.Append("\\r");
				}
				else if (c == '\n')
				{
					stringBuilder.Append("\\n");
				}
				else if (c >= ' ' && c < '\u007f')
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('.');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000AA9 RID: 2729
		private const char Space = ' ';

		// Token: 0x04000AAA RID: 2730
		private const char Delete = '\u007f';

		// Token: 0x04000AAB RID: 2731
		private const string TraceEnabledQueryStringParameter = "trace";

		// Token: 0x04000AAC RID: 2732
		private const string GetUserPhotoTraceEnabledHeaderName = "X-Exchange-GetUserPhoto-TraceEnabled";

		// Token: 0x04000AAD RID: 2733
		private const string GetUserPhotoTraceEnabledHeaderValue = "1";

		// Token: 0x04000AAE RID: 2734
		private const string GetPersonaPhotoTracesHeaderName = "X-Exchange-GetPersonaPhoto-Traces";

		// Token: 0x04000AAF RID: 2735
		private const string GetUserPhotoTracesHeaderName = "X-Exchange-GetUserPhoto-Traces";

		// Token: 0x04000AB0 RID: 2736
		public static readonly PhotosDiagnostics Instance = new PhotosDiagnostics();
	}
}
