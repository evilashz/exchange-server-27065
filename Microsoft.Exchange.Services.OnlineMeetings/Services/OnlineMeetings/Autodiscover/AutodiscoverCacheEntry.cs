using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x0200002C RID: 44
	internal class AutodiscoverCacheEntry
	{
		// Token: 0x0600019D RID: 413 RVA: 0x0000624E File Offset: 0x0000444E
		internal AutodiscoverCacheEntry(string sipUri, string ucwaUrl, ExDateTime? expirationDate)
		{
			this.sipUri = sipUri;
			this.ucwaUrl = ucwaUrl;
			this.Expiration = expirationDate;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006272 File Offset: 0x00004472
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000627A File Offset: 0x0000447A
		public string UcwaUrl
		{
			get
			{
				return this.ucwaUrl;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006282 File Offset: 0x00004482
		public bool IsUcwaEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.UcwaUrl);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006292 File Offset: 0x00004492
		public int MaxFailureThreshold
		{
			get
			{
				if (this.maxFailureThreshold < 0)
				{
					this.maxFailureThreshold = AppConfigLoader.GetConfigIntValue("OnlineMeetingAutodiscoverRetryThreshold", 0, int.MaxValue, 5);
				}
				return this.maxFailureThreshold;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000062BA File Offset: 0x000044BA
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000062C2 File Offset: 0x000044C2
		public ExDateTime? Expiration { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000062CC File Offset: 0x000044CC
		public bool IsValid
		{
			get
			{
				return (this.Expiration == null || ExDateTime.Now.CompareTo(this.Expiration.Value) < 0) && this.FailureCount < this.MaxFailureThreshold;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006317 File Offset: 0x00004517
		internal int FailureCount
		{
			get
			{
				return this.failureCount;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000631F File Offset: 0x0000451F
		public void IncrementFailureCount()
		{
			ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, int>(0, 0L, "[AutodiscoverCacheEntry.IncrementFailureCount] Pre-incremented failure count of entry for user '{0}': {1}", this.SipUri, this.failureCount);
			this.failureCount++;
		}

		// Token: 0x04000127 RID: 295
		internal const int MaxFailureThresholdDefault = 5;

		// Token: 0x04000128 RID: 296
		private readonly string sipUri;

		// Token: 0x04000129 RID: 297
		private readonly string ucwaUrl;

		// Token: 0x0400012A RID: 298
		private int failureCount;

		// Token: 0x0400012B RID: 299
		private int maxFailureThreshold = -1;
	}
}
