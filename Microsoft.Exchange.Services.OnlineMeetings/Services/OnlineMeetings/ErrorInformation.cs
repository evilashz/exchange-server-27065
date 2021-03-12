using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000010 RID: 16
	internal class ErrorInformation
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000225D File Offset: 0x0000045D
		internal ErrorInformation()
		{
			this.Parameters = new Dictionary<string, string>();
			this.DebugInfo = new Dictionary<string, string>();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000227B File Offset: 0x0000047B
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002283 File Offset: 0x00000483
		public ErrorCode Code { get; internal set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002294 File Offset: 0x00000494
		public ErrorSubcode Subcode { get; internal set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000229D File Offset: 0x0000049D
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000022A5 File Offset: 0x000004A5
		public string Message { get; internal set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000022AE File Offset: 0x000004AE
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000022B6 File Offset: 0x000004B6
		public IDictionary<string, string> Parameters { get; internal set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000022BF File Offset: 0x000004BF
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000022C7 File Offset: 0x000004C7
		public IDictionary<string, string> DebugInfo { get; internal set; }

		// Token: 0x0600003B RID: 59 RVA: 0x000022D0 File Offset: 0x000004D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Code:{0};Subcode:{1};Message:{2};", this.Code, this.Subcode, this.Message);
			if (this.Parameters != null)
			{
				stringBuilder.Append("Parameters:");
				foreach (string text in this.Parameters.Keys)
				{
					stringBuilder.AppendFormat("{0}-{1}.", text, this.Parameters[text]);
				}
				stringBuilder.Append(";");
			}
			if (this.DebugInfo != null)
			{
				stringBuilder.Append("DebugInfo:");
				foreach (string text2 in this.DebugInfo.Keys)
				{
					stringBuilder.AppendFormat("{0}-{1}.", text2, this.DebugInfo[text2]);
				}
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002400 File Offset: 0x00000600
		public static ErrorCode TryGetErrorFromHttpStatusCode(HttpStatusCode httpResponseCode)
		{
			ErrorCode result = ErrorCode.Unknown;
			switch (httpResponseCode)
			{
			case HttpStatusCode.BadRequest:
				result = ErrorCode.BadRequest;
				break;
			case HttpStatusCode.Unauthorized:
			case HttpStatusCode.PaymentRequired:
			case HttpStatusCode.NotAcceptable:
			case HttpStatusCode.ProxyAuthenticationRequired:
			case HttpStatusCode.RequestTimeout:
			case HttpStatusCode.LengthRequired:
				break;
			case HttpStatusCode.Forbidden:
				result = ErrorCode.Forbidden;
				break;
			case HttpStatusCode.NotFound:
				result = ErrorCode.NotFound;
				break;
			case HttpStatusCode.MethodNotAllowed:
				result = ErrorCode.MethodNotAllowed;
				break;
			case HttpStatusCode.Conflict:
				result = ErrorCode.Conflict;
				break;
			case HttpStatusCode.Gone:
				result = ErrorCode.Gone;
				break;
			case HttpStatusCode.PreconditionFailed:
				result = ErrorCode.PreconditionFailed;
				break;
			default:
				if (httpResponseCode != HttpStatusCode.UnsupportedMediaType)
				{
					switch (httpResponseCode)
					{
					case HttpStatusCode.InternalServerError:
						result = ErrorCode.ServiceFailure;
						break;
					case HttpStatusCode.BadGateway:
						result = ErrorCode.BadGateway;
						break;
					case HttpStatusCode.ServiceUnavailable:
						result = ErrorCode.ServiceUnavailable;
						break;
					case HttpStatusCode.GatewayTimeout:
						result = ErrorCode.Timeout;
						break;
					}
				}
				else
				{
					result = ErrorCode.UnsupportedMediaType;
				}
				break;
			}
			return result;
		}
	}
}
