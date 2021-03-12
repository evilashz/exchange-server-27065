using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F8 RID: 248
	internal class WnsTileNotification : WnsXmlNotification
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x000189C6 File Offset: 0x00016BC6
		public WnsTileNotification(string appId, OrganizationId tenantId, string deviceUri, WnsTileVisual visual, int? timeToLive = null, string tag = null, WnsCachePolicy? cachePolicy = null) : base(appId, tenantId, deviceUri)
		{
			this.Visual = visual;
			this.CachePolicy = cachePolicy;
			this.TimeToLive = timeToLive;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x000189E9 File Offset: 0x00016BE9
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x000189F1 File Offset: 0x00016BF1
		public WnsTileVisual Visual { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000189FA File Offset: 0x00016BFA
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00018A02 File Offset: 0x00016C02
		public string Tag { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00018A0B File Offset: 0x00016C0B
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00018A13 File Offset: 0x00016C13
		public WnsCachePolicy? CachePolicy { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00018A1C File Offset: 0x00016C1C
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00018A24 File Offset: 0x00016C24
		public int? TimeToLive { get; private set; }

		// Token: 0x06000804 RID: 2052 RVA: 0x00018A2D File Offset: 0x00016C2D
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			base.ValidateTimeToLive(this.TimeToLive, errors);
			base.ValidateTemplate(this.Visual.Binding, errors);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00018A55 File Offset: 0x00016C55
		protected override void WriteWnsXmlPayload(WnsPayloadWriter wpw)
		{
			wpw.WriteElementStart("tile", true);
			wpw.WriteAttributesEnd();
			this.Visual.WriteWnsPayload(wpw);
			wpw.WriteElementEnd();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00018A7C File Offset: 0x00016C7C
		protected override void PrepareWnsRequest(WnsRequest wnsRequest)
		{
			base.PrepareWnsRequest(wnsRequest);
			wnsRequest.WnsType = "wns/tile";
			if (this.CachePolicy != null)
			{
				wnsRequest.WnsCachePolicy = this.CachePolicy.Value.ToString();
			}
			if (this.TimeToLive != null)
			{
				wnsRequest.WnsTimeToLive = this.TimeToLive.ToString();
			}
			if (string.IsNullOrWhiteSpace(this.Tag))
			{
				wnsRequest.WnsTag = this.Tag;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00018B0C File Offset: 0x00016D0C
		protected override string InternalToFullString()
		{
			return string.Format("{0}; tag: {1}; cache:{2}; ttl:{3}", new object[]
			{
				base.InternalToFullString(),
				this.Tag.ToNullableString(),
				this.CachePolicy.ToNullableString<WnsCachePolicy>(),
				this.TimeToLive.ToNullableString<int>()
			});
		}
	}
}
