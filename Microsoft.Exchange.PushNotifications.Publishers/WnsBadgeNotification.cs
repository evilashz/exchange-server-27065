using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000DA RID: 218
	internal class WnsBadgeNotification : WnsXmlNotification
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x000162C4 File Offset: 0x000144C4
		public WnsBadgeNotification(string appId, OrganizationId tenantId, string deviceUri, int numericValue, int? timeToLive = null, WnsCachePolicy? cachePolicy = 1) : this(appId, tenantId, deviceUri, new int?(numericValue), null, timeToLive, null)
		{
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000162F8 File Offset: 0x000144F8
		public WnsBadgeNotification(string appId, OrganizationId tenantId, string deviceUri, WnsGlyph glyphValue, int? timeToLive = null, WnsCachePolicy? cachePolicy = 1) : this(appId, tenantId, deviceUri, null, new WnsGlyph?(glyphValue), timeToLive, null)
		{
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00016329 File Offset: 0x00014529
		private WnsBadgeNotification(string appId, OrganizationId tenantId, string deviceUri, int? numericValue, WnsGlyph? glyphValue, int? timeToLive = null, WnsCachePolicy? cachePolicy = null) : base(appId, tenantId, deviceUri)
		{
			this.NumericValue = numericValue;
			this.GlyphValue = glyphValue;
			this.CachePolicy = cachePolicy;
			this.TimeToLive = timeToLive;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00016354 File Offset: 0x00014554
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001635C File Offset: 0x0001455C
		public int? NumericValue { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00016365 File Offset: 0x00014565
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001636D File Offset: 0x0001456D
		public WnsGlyph? GlyphValue { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00016376 File Offset: 0x00014576
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001637E File Offset: 0x0001457E
		public WnsCachePolicy? CachePolicy { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00016387 File Offset: 0x00014587
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001638F File Offset: 0x0001458F
		public int? TimeToLive { get; private set; }

		// Token: 0x0600071C RID: 1820 RVA: 0x00016398 File Offset: 0x00014598
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			base.ValidateTimeToLive(this.TimeToLive, errors);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000163B0 File Offset: 0x000145B0
		protected override void WriteWnsXmlPayload(WnsPayloadWriter wpw)
		{
			wpw.WriteElementStart("badge", false);
			if (this.GlyphValue != null)
			{
				wpw.WriteAttribute<WnsGlyph>("value", this.GlyphValue, false);
			}
			else
			{
				wpw.WriteAttribute<int>("value", this.NumericValue, false);
			}
			wpw.WriteElementEnd();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00016408 File Offset: 0x00014608
		protected override void PrepareWnsRequest(WnsRequest wnsRequest)
		{
			base.PrepareWnsRequest(wnsRequest);
			wnsRequest.WnsType = "wns/badge";
			if (this.CachePolicy != null)
			{
				wnsRequest.WnsCachePolicy = this.CachePolicy.Value.ToString();
			}
			if (this.TimeToLive != null)
			{
				wnsRequest.WnsTimeToLive = this.TimeToLive.ToString();
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00016480 File Offset: 0x00014680
		protected override string InternalToFullString()
		{
			return string.Format("{0}; num:{1}; glyph:{2}; cache:{3}; ttl:{4}", new object[]
			{
				base.InternalToFullString(),
				this.NumericValue.ToNullableString<int>(),
				this.TimeToLive.ToNullableString<int>(),
				this.CachePolicy.ToNullableString<WnsCachePolicy>(),
				this.TimeToLive.ToNullableString<int>()
			});
		}
	}
}
