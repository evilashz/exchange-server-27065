using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.ReportingWebService;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000037 RID: 55
	internal class ReportingVersion
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000703B File Offset: 0x0000523B
		public static string LatestVersion
		{
			get
			{
				return ReportingVersion.V1;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007042 File Offset: 0x00005242
		public static void EnableVersionZero()
		{
			if (!ReportingVersion.SupportedVersion.Contains(ReportingVersion.V0, StringComparer.OrdinalIgnoreCase))
			{
				ReportingVersion.SupportedVersion.Add(ReportingVersion.V0);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000706C File Offset: 0x0000526C
		public static string GetCurrentReportingVersion(HttpContext httpContext)
		{
			string text = (string)httpContext.Items["Exchange_RWS_Version"];
			if (string.IsNullOrEmpty(text))
			{
				bool flag = httpContext.Request.QueryString.AllKeys.Contains(ReportingVersion.QueryStringParameterName);
				bool flag2 = httpContext.Request.Headers.AllKeys.Contains(ReportingVersion.HttpHeaderName);
				if (flag && flag2)
				{
					ServiceDiagnostics.ThrowError(ReportingErrorCode.ErrorVersionAmbiguous, Strings.ErrorVersionAmbiguous);
				}
				else if (flag || flag2)
				{
					string version;
					if (flag)
					{
						version = httpContext.Request.QueryString[ReportingVersion.QueryStringParameterName];
						ExTraceGlobals.ReportingWebServiceTracer.TraceDebug<string>(0L, "[ReportingVersion::GetVersion] Version in query string: {0}", text);
					}
					else
					{
						version = httpContext.Request.Headers[ReportingVersion.HttpHeaderName];
						ExTraceGlobals.ReportingWebServiceTracer.TraceDebug<string>(0L, "[ReportingVersion::GetVersion] Version in header: {0}", text);
					}
					if (!ReportingVersion.IsVersionSupported(version, out text))
					{
						ServiceDiagnostics.ThrowError(ReportingErrorCode.ErrorInvalidVersion, Strings.ErrorInvalidVersion);
					}
				}
				else
				{
					ExTraceGlobals.ReportingWebServiceTracer.TraceDebug<string>(0L, "[ReportingVersion::GetVersion] Use the latest version: {0}", ReportingVersion.LatestVersion);
					text = ReportingVersion.LatestVersion;
				}
			}
			ExTraceGlobals.ReportingWebServiceTracer.Information<string>(0L, "[ReportingVersion::GetVersion] Version: {0}", text);
			httpContext.Items["Exchange_RWS_Version"] = text;
			return text;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000071A4 File Offset: 0x000053A4
		public static void WriteVersionInfoInResponse(HttpContext httpContext)
		{
			string value = (string)httpContext.Items["Exchange_RWS_Version"];
			if (string.IsNullOrEmpty(value))
			{
				value = ReportingVersion.LatestVersion;
			}
			httpContext.Response.Headers[ReportingVersion.HttpHeaderName] = value;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007204 File Offset: 0x00005404
		private static bool IsVersionSupported(string version, out string supportedVersion)
		{
			version = version.Trim();
			bool flag = ReportingVersion.SupportedVersion.Any((string v) => v.Equals(version, StringComparison.OrdinalIgnoreCase));
			supportedVersion = (flag ? version.ToUpper() : null);
			return flag;
		}

		// Token: 0x040000A7 RID: 167
		private const string HttpContextKey = "Exchange_RWS_Version";

		// Token: 0x040000A8 RID: 168
		public static readonly string HttpHeaderName = "X-RWS-Version";

		// Token: 0x040000A9 RID: 169
		public static readonly string QueryStringParameterName = "rws-version";

		// Token: 0x040000AA RID: 170
		public static readonly string V0 = "V0";

		// Token: 0x040000AB RID: 171
		public static readonly string V1 = "2013-V1";

		// Token: 0x040000AC RID: 172
		private static readonly List<string> SupportedVersion = new List<string>(new string[]
		{
			ReportingVersion.V1
		});
	}
}
