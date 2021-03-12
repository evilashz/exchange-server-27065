using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F1 RID: 241
	internal class WnsRawNotification : WnsNotification
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00018048 File Offset: 0x00016248
		public WnsRawNotification(string appId, OrganizationId tenantId, string deviceUri, string data) : base(appId, tenantId, deviceUri)
		{
			this.Data = data;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001805B File Offset: 0x0001625B
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00018063 File Offset: 0x00016263
		public string Data { get; private set; }

		// Token: 0x060007BB RID: 1979 RVA: 0x0001806C File Offset: 0x0001626C
		protected override string GetSerializedPayload(List<LocalizedString> errors)
		{
			return this.Data;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00018074 File Offset: 0x00016274
		protected override void PrepareWnsRequest(WnsRequest wnsRequest)
		{
			wnsRequest.WnsType = "wns/raw";
			wnsRequest.ContentType = "application/octet-stream";
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001808C File Offset: 0x0001628C
		protected override string InternalToFullString()
		{
			return string.Format("{0}; data:{1};", base.InternalToFullString(), this.Data.ToNullableString());
		}
	}
}
