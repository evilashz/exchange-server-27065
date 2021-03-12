using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A8 RID: 168
	internal class GcmResponse
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x00013118 File Offset: 0x00011318
		public GcmResponse(string responseBody)
		{
			this.TransportStatus = GcmTransportStatusCode.Success;
			this.OriginalStatusCode = new HttpStatusCode?(HttpStatusCode.OK);
			this.ResponseStatus = GcmResponseStatusCode.InvalidResponse;
			this.OriginalBody = responseBody;
			GcmPayloadReader gcmPayloadReader = new GcmPayloadReader();
			Dictionary<string, string> dictionary = gcmPayloadReader.Read(responseBody);
			if (dictionary.ContainsKey("id"))
			{
				this.ResponseStatus = GcmResponseStatusCode.Success;
				this.Id = dictionary["id"].ToNullableString();
				return;
			}
			GcmResponseStatusCode responseStatus;
			if (dictionary.ContainsKey("Error") && Enum.TryParse<GcmResponseStatusCode>(dictionary["Error"], true, out responseStatus))
			{
				this.ResponseStatus = responseStatus;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000131B4 File Offset: 0x000113B4
		public GcmResponse(Exception exception)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			if (exception is DownloadCanceledException || exception is DownloadTimeoutException)
			{
				this.TransportStatus = GcmTransportStatusCode.Timeout;
			}
			else
			{
				this.TransportStatus = GcmTransportStatusCode.Unknown;
			}
			this.ResponseStatus = GcmResponseStatusCode.Undefined;
			this.Exception = exception;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00013200 File Offset: 0x00011400
		public GcmResponse(WebException webException)
		{
			ArgumentValidator.ThrowIfNull("webException", webException);
			this.TransportStatus = GcmTransportStatusCode.Unknown;
			this.ResponseStatus = GcmResponseStatusCode.Undefined;
			this.Exception = webException;
			HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				this.OriginalStatusCode = new HttpStatusCode?(httpWebResponse.StatusCode);
				if (this.OriginalStatusCode == HttpStatusCode.Unauthorized)
				{
					this.TransportStatus = GcmTransportStatusCode.Unauthorized;
					return;
				}
				if (this.OriginalStatusCode >= HttpStatusCode.InternalServerError && this.OriginalStatusCode.Value < (HttpStatusCode)600)
				{
					this.TransportStatus = ((this.OriginalStatusCode == HttpStatusCode.InternalServerError) ? GcmTransportStatusCode.InternalServerError : GcmTransportStatusCode.ServerUnavailable);
					this.OriginalRetryAfter = httpWebResponse.GetResponseHeader("Retry-After");
					if (!string.IsNullOrEmpty(this.OriginalRetryAfter))
					{
						int num = 0;
						if (int.TryParse(this.OriginalRetryAfter, out num))
						{
							this.BackOffEndTime = new ExDateTime?(ExDateTime.UtcNow.AddSeconds((double)num));
							return;
						}
						ExDateTime value;
						if (ExDateTime.TryParse(this.OriginalRetryAfter, out value))
						{
							this.BackOffEndTime = new ExDateTime?(value);
						}
					}
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001334F File Offset: 0x0001154F
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00013357 File Offset: 0x00011557
		public GcmTransportStatusCode TransportStatus { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00013360 File Offset: 0x00011560
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00013368 File Offset: 0x00011568
		public GcmResponseStatusCode ResponseStatus { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00013371 File Offset: 0x00011571
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00013379 File Offset: 0x00011579
		public string Id { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00013382 File Offset: 0x00011582
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0001338A File Offset: 0x0001158A
		public Exception Exception { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00013393 File Offset: 0x00011593
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0001339B File Offset: 0x0001159B
		public ExDateTime? BackOffEndTime { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000133A4 File Offset: 0x000115A4
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000133AC File Offset: 0x000115AC
		private HttpStatusCode? OriginalStatusCode { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000133B5 File Offset: 0x000115B5
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x000133BD File Offset: 0x000115BD
		private string OriginalRetryAfter { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000133C6 File Offset: 0x000115C6
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000133CE File Offset: 0x000115CE
		private string OriginalBody { get; set; }

		// Token: 0x060005E0 RID: 1504 RVA: 0x000133D8 File Offset: 0x000115D8
		public string ToFullString()
		{
			if (this.toFullString == null)
			{
				this.toFullString = string.Format("{{ transportStatus:{0}; responseStatus:{1}; id:{2}; backOffEndTime:{3}; originalStatus:{4}; originalRetryAfter:{5}; originalBody:{6}; exception:{7} }}", new object[]
				{
					this.TransportStatus,
					this.ResponseStatus,
					this.Id.ToNullableString(),
					this.BackOffEndTime.ToNullableString<ExDateTime>(),
					this.OriginalStatusCode.ToNullableString<HttpStatusCode>(),
					this.OriginalRetryAfter.ToNullableString(),
					this.OriginalBody.ToNullableString(),
					(this.Exception != null) ? this.Exception.ToTraceString() : "null"
				});
			}
			return this.toFullString;
		}

		// Token: 0x040002D5 RID: 725
		public const string IdKey = "id";

		// Token: 0x040002D6 RID: 726
		public const string ErrorKey = "Error";

		// Token: 0x040002D7 RID: 727
		public const string RetryAfterHeaderKey = "Retry-After";

		// Token: 0x040002D8 RID: 728
		public static readonly char[] KeyValueSeparator = new char[]
		{
			'='
		};

		// Token: 0x040002D9 RID: 729
		private string toFullString;
	}
}
