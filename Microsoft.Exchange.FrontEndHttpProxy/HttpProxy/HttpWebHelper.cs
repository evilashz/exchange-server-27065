using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200008E RID: 142
	internal static class HttpWebHelper
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0001990C File Offset: 0x00017B0C
		public static void SetRange(HttpWebRequest destination, string value)
		{
			HttpRangeSpecifier httpRangeSpecifier = HttpRangeSpecifier.Parse(value);
			foreach (HttpRange httpRange in httpRangeSpecifier.RangeCollection)
			{
				if (httpRange.HasFirstBytePosition && httpRange.HasLastBytePosition)
				{
					destination.AddRange(httpRangeSpecifier.RangeUnitSpecifier, httpRange.FirstBytePosition, httpRange.LastBytePosition);
				}
				else if (httpRange.HasFirstBytePosition)
				{
					destination.AddRange(httpRangeSpecifier.RangeUnitSpecifier, httpRange.FirstBytePosition);
				}
				else if (httpRange.HasSuffixLength)
				{
					destination.AddRange(httpRangeSpecifier.RangeUnitSpecifier, -httpRange.SuffixLength);
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000199BC File Offset: 0x00017BBC
		public static void SetIfModifiedSince(HttpWebRequest destination, string value)
		{
			DateTime ifModifiedSince;
			if (DateTime.TryParse(value, out ifModifiedSince))
			{
				destination.IfModifiedSince = ifModifiedSince;
				return;
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[HttpWebHelper::SetIfModifiedSince] Parse failure for IfModifiedSince header {0}", value);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000199ED File Offset: 0x00017BED
		public static void SetConnectionHeader(HttpWebRequest destination, string source)
		{
			if (source.IndexOf("keep-alive", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				destination.KeepAlive = true;
				return;
			}
			if (source.IndexOf("close", StringComparison.OrdinalIgnoreCase) == -1)
			{
				destination.Connection = source;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00019A1C File Offset: 0x00017C1C
		public static HttpWebHelper.ConnectivityError CheckConnectivityError(WebException e)
		{
			WebExceptionStatus status = e.Status;
			switch (status)
			{
			case WebExceptionStatus.NameResolutionFailure:
				return HttpWebHelper.ConnectivityError.NonRetryable;
			case WebExceptionStatus.ConnectFailure:
			case WebExceptionStatus.SendFailure:
				break;
			case WebExceptionStatus.ReceiveFailure:
				goto IL_42;
			default:
				if (status != WebExceptionStatus.ConnectionClosed)
				{
					switch (status)
					{
					case WebExceptionStatus.KeepAliveFailure:
						break;
					case WebExceptionStatus.Pending:
						goto IL_42;
					case WebExceptionStatus.Timeout:
					case WebExceptionStatus.ProxyNameResolutionFailure:
						return HttpWebHelper.ConnectivityError.NonRetryable;
					default:
						goto IL_42;
					}
				}
				break;
			}
			return HttpWebHelper.ConnectivityError.Retryable;
			IL_42:
			HttpWebResponse httpWebResponse = (HttpWebResponse)e.Response;
			if (httpWebResponse != null && (httpWebResponse.StatusCode == HttpStatusCode.ServiceUnavailable || httpWebResponse.StatusCode == HttpStatusCode.BadGateway || httpWebResponse.StatusCode == HttpStatusCode.GatewayTimeout))
			{
				return HttpWebHelper.ConnectivityError.NonRetryable;
			}
			return HttpWebHelper.ConnectivityError.None;
		}

		// Token: 0x0200008F RID: 143
		public enum ConnectivityError
		{
			// Token: 0x04000374 RID: 884
			None,
			// Token: 0x04000375 RID: 885
			Retryable,
			// Token: 0x04000376 RID: 886
			NonRetryable
		}
	}
}
