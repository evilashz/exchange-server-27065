using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F3 RID: 243
	internal class WnsResponse
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x000181E9 File Offset: 0x000163E9
		public WnsResponse(HttpStatusCode responseCode, string messageId, string debugTrace, string errorDescription)
		{
			this.ResponseCode = responseCode;
			this.MessageId = messageId;
			this.DebugTrace = debugTrace;
			this.ErrorDescription = errorDescription;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001820E File Offset: 0x0001640E
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00018216 File Offset: 0x00016416
		public HttpStatusCode ResponseCode { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001821F File Offset: 0x0001641F
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00018227 File Offset: 0x00016427
		public string MessageId { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00018230 File Offset: 0x00016430
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00018238 File Offset: 0x00016438
		public string DebugTrace { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00018241 File Offset: 0x00016441
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00018249 File Offset: 0x00016449
		public string ErrorDescription { get; private set; }

		// Token: 0x060007D6 RID: 2006 RVA: 0x00018254 File Offset: 0x00016454
		public static WnsResponse Create(HttpWebResponse response)
		{
			ArgumentValidator.ThrowIfNull("response", response);
			return new WnsResponse(response.StatusCode, response.Headers["X-WNS-Msg-ID"], response.Headers["X-WNS-Debug-Trace"], response.Headers["X-WNS-Error-Description"]);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000182A8 File Offset: 0x000164A8
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("responseCode={0}; messageId={1}; error={2}; debugTrace={3}", new object[]
				{
					this.ResponseCode.ToString(),
					this.MessageId.ToNullableString(),
					this.ErrorDescription.ToNullableString(),
					this.DebugTrace.ToNullableString()
				});
			}
			return this.toString;
		}

		// Token: 0x0400044B RID: 1099
		private const string WnsHeaderMessageId = "X-WNS-Msg-ID";

		// Token: 0x0400044C RID: 1100
		private const string WnsHeaderDebugTrace = "X-WNS-Debug-Trace";

		// Token: 0x0400044D RID: 1101
		private const string WnsHeaderErrorDescription = "X-WNS-Error-Description";

		// Token: 0x0400044E RID: 1102
		private const string WnsHeaderNotificationStatus = "X-WNS-NotificationStatus";

		// Token: 0x0400044F RID: 1103
		private const string WnsHeaderDeviceStatus = "X-WNS-DeviceConnectionStatus";

		// Token: 0x04000450 RID: 1104
		public static readonly WnsResponse Succeeded = new WnsResponse(HttpStatusCode.OK, null, null, null);

		// Token: 0x04000451 RID: 1105
		private string toString;
	}
}
