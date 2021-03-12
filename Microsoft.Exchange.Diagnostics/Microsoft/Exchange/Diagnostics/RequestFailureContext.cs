using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200014F RID: 335
	internal class RequestFailureContext
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x00023AC0 File Offset: 0x00021CC0
		public RequestFailureContext(RequestFailureContext.RequestFailurePoint failurePoint, int httpStatusCode, string error, string details, HttpProxySubErrorCode? httpProxySubErrorCode = null, WebExceptionStatus? webExceptionStatus = null, LiveIdAuthResult? liveIdAuthResult = null)
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			if (details == null)
			{
				throw new ArgumentNullException("details");
			}
			this.FailurePoint = failurePoint;
			this.HttpStatusCode = httpStatusCode;
			this.Error = error;
			this.Details = details;
			this.HttpProxySubErrorCode = httpProxySubErrorCode;
			this.WebExceptionStatus = webExceptionStatus;
			this.LiveIdAuthResult = liveIdAuthResult;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00023B25 File Offset: 0x00021D25
		private RequestFailureContext(string serializedFailureContext)
		{
			if (string.IsNullOrEmpty(serializedFailureContext))
			{
				throw new ArgumentException("serializedFailureContext is null or empty");
			}
			this.Deserialize(serializedFailureContext);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00023B47 File Offset: 0x00021D47
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00023B4F File Offset: 0x00021D4F
		public RequestFailureContext.RequestFailurePoint FailurePoint { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00023B58 File Offset: 0x00021D58
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00023B60 File Offset: 0x00021D60
		public int HttpStatusCode { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00023B69 File Offset: 0x00021D69
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00023B71 File Offset: 0x00021D71
		public HttpProxySubErrorCode? HttpProxySubErrorCode { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00023B7A File Offset: 0x00021D7A
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00023B82 File Offset: 0x00021D82
		public WebExceptionStatus? WebExceptionStatus { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00023B8B File Offset: 0x00021D8B
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00023B93 File Offset: 0x00021D93
		public LiveIdAuthResult? LiveIdAuthResult { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00023B9C File Offset: 0x00021D9C
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00023BA4 File Offset: 0x00021DA4
		public string Error { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00023BAD File Offset: 0x00021DAD
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00023BB5 File Offset: 0x00021DB5
		public string Details { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00023BBE File Offset: 0x00021DBE
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00023BC6 File Offset: 0x00021DC6
		public string UnrecognizedFailurePoint { get; private set; }

		// Token: 0x06000990 RID: 2448 RVA: 0x00023BCF File Offset: 0x00021DCF
		public static bool TryCreateFromResponse(HttpWebResponse cafeResponse, out RequestFailureContext requestFailureContext)
		{
			if (cafeResponse == null)
			{
				throw new ArgumentNullException("cafeResponse");
			}
			return RequestFailureContext.TryCreateFromResponseHeaders(cafeResponse.Headers, out requestFailureContext);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00023BEB File Offset: 0x00021DEB
		public static bool TryCreateFromResponseHeaders(WebHeaderCollection webHeaderCollection, out RequestFailureContext requestFailureContext)
		{
			if (webHeaderCollection == null)
			{
				throw new ArgumentNullException("webHeaderCollection");
			}
			return RequestFailureContext.TryDeserialize(webHeaderCollection[RequestFailureContext.HeaderKey], out requestFailureContext);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00023C0C File Offset: 0x00021E0C
		public static bool TryCreateFromResponseHeaders(IDictionary<string, string> headerDictionary, out RequestFailureContext requestFailureContext)
		{
			if (headerDictionary == null)
			{
				throw new ArgumentNullException("headerDictionary");
			}
			requestFailureContext = null;
			string headerValue;
			return headerDictionary.TryGetValue(RequestFailureContext.HeaderKey, out headerValue) && RequestFailureContext.TryDeserialize(headerValue, out requestFailureContext);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00023C44 File Offset: 0x00021E44
		public static bool TryDeserialize(string headerValue, out RequestFailureContext requestFailureContext)
		{
			requestFailureContext = null;
			if (!string.IsNullOrEmpty(headerValue))
			{
				try
				{
					requestFailureContext = new RequestFailureContext(headerValue);
					return true;
				}
				catch (FormatException)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00023C80 File Offset: 0x00021E80
		private static string ConvertStringFromBase64(string input)
		{
			string @string;
			try
			{
				byte[] bytes = Convert.FromBase64String(input);
				@string = Encoding.UTF8.GetString(bytes);
			}
			catch (ArgumentException innerException)
			{
				throw new FormatException(string.Format("Couldn't convert base64 string: {0}", input), innerException);
			}
			return @string;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00023CC8 File Offset: 0x00021EC8
		private static int ConvertStringToInt(string input)
		{
			int result;
			if (!int.TryParse(input, out result))
			{
				throw new FormatException(string.Format("Couldn't parse input string as an int: {0}", input));
			}
			return result;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00023CF4 File Offset: 0x00021EF4
		public string Serialize()
		{
			return string.Format("{0};{1};{2};{3};{4};{5};{6}", new object[]
			{
				this.FailurePoint.ToString(),
				this.HttpStatusCode,
				Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Error)),
				Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Details)),
				(this.HttpProxySubErrorCode != null) ? this.HttpProxySubErrorCode.ToString() : string.Empty,
				(this.WebExceptionStatus != null) ? this.WebExceptionStatus.ToString() : string.Empty,
				(this.LiveIdAuthResult != null) ? this.LiveIdAuthResult.ToString() : string.Empty
			});
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00023DF1 File Offset: 0x00021FF1
		public void UpdateResponse(HttpResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			response.Headers[RequestFailureContext.HeaderKey] = this.Serialize();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00023E18 File Offset: 0x00022018
		private void Deserialize(string serializedFailureContext)
		{
			string[] array = serializedFailureContext.Split(new char[]
			{
				';'
			});
			int num = 0;
			if (array.Length >= 4)
			{
				string text = array[num++];
				RequestFailureContext.RequestFailurePoint failurePoint;
				if (!Enum.TryParse<RequestFailureContext.RequestFailurePoint>(text, out failurePoint))
				{
					failurePoint = RequestFailureContext.RequestFailurePoint.Unrecognized;
					this.UnrecognizedFailurePoint = text;
				}
				this.FailurePoint = failurePoint;
				this.HttpStatusCode = RequestFailureContext.ConvertStringToInt(array[num++]);
				this.Error = RequestFailureContext.ConvertStringFromBase64(array[num++]);
				this.Details = RequestFailureContext.ConvertStringFromBase64(array[num++]);
				if (array.Length >= 6)
				{
					HttpProxySubErrorCode value;
					if (!Enum.TryParse<HttpProxySubErrorCode>(array[num++], out value))
					{
						this.HttpProxySubErrorCode = null;
					}
					else
					{
						this.HttpProxySubErrorCode = new HttpProxySubErrorCode?(value);
					}
					WebExceptionStatus value2;
					if (!Enum.TryParse<WebExceptionStatus>(array[num++], out value2))
					{
						this.WebExceptionStatus = null;
					}
					else
					{
						this.WebExceptionStatus = new WebExceptionStatus?(value2);
					}
				}
				if (array.Length >= 7)
				{
					LiveIdAuthResult value3;
					if (!Enum.TryParse<LiveIdAuthResult>(array[num++], out value3))
					{
						this.LiveIdAuthResult = null;
						return;
					}
					this.LiveIdAuthResult = new LiveIdAuthResult?(value3);
				}
				return;
			}
			throw new FormatException("Expected a minimum of 4 parameters.");
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00023F44 File Offset: 0x00022144
		public override string ToString()
		{
			string text = this.FailurePoint.ToString();
			if (this.FailurePoint == RequestFailureContext.RequestFailurePoint.Unrecognized)
			{
				text = string.Format("{0}({1})", text, this.UnrecognizedFailurePoint);
			}
			return string.Format("FailurePoint={0}, HttpStatusCode={1}, Error={2}, Details={3}, HttpProxySubErrorCode={4}, WebExceptionStatus={5}, LiveIdAuthResult={6}", new object[]
			{
				text,
				this.HttpStatusCode,
				this.Error,
				this.Details,
				this.HttpProxySubErrorCode,
				this.WebExceptionStatus,
				this.LiveIdAuthResult
			});
		}

		// Token: 0x0400066D RID: 1645
		private static readonly string HeaderKey = "X-FailureContext";

		// Token: 0x0400066E RID: 1646
		public static readonly string HttpContextKeyName = "RequestFailureContext";

		// Token: 0x02000150 RID: 336
		public enum RequestFailurePoint
		{
			// Token: 0x04000678 RID: 1656
			Unrecognized,
			// Token: 0x04000679 RID: 1657
			FrontEnd,
			// Token: 0x0400067A RID: 1658
			BackEnd
		}
	}
}
