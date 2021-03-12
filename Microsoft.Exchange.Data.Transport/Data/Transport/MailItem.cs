using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000073 RID: 115
	public abstract class MailItem
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00006901 File Offset: 0x00004B01
		internal MailItem()
		{
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000254 RID: 596
		// (set) Token: 0x06000255 RID: 597
		public abstract string OriginalAuthenticator { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000256 RID: 598
		public abstract DateTime DateTimeReceived { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000257 RID: 599
		// (set) Token: 0x06000258 RID: 600
		public abstract string EnvelopeId { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000259 RID: 601
		// (set) Token: 0x0600025A RID: 602
		public abstract RoutingAddress FromAddress { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600025B RID: 603
		public abstract string OriginatingDomain { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600025C RID: 604
		public abstract EmailMessage Message { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600025D RID: 605
		public abstract long MimeStreamLength { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025E RID: 606
		public abstract IDictionary<string, object> Properties { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025F RID: 607
		public abstract EnvelopeRecipientCollection Recipients { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000260 RID: 608
		// (set) Token: 0x06000261 RID: 609
		public abstract DsnFormatRequested DsnFormatRequested { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000262 RID: 610
		// (set) Token: 0x06000263 RID: 611
		public abstract DeliveryPriority DeliveryPriority { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000264 RID: 612
		public abstract DeliveryMethod InboundDeliveryMethod { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000265 RID: 613
		public abstract bool MustDeliver { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000266 RID: 614
		public abstract Guid TenantId { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000267 RID: 615
		// (set) Token: 0x06000268 RID: 616
		public abstract string OriginatorOrganization { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000269 RID: 617
		internal abstract long InternalMessageId { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600026A RID: 618
		internal abstract Guid NetworkMessageId { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026B RID: 619
		// (set) Token: 0x0600026C RID: 620
		internal abstract Guid SystemProbeId { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600026D RID: 621
		internal abstract MessageSnapshotWriter SnapshotWriter { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600026E RID: 622
		internal abstract bool PipelineTracingEnabled { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600026F RID: 623
		internal abstract string PipelineTracingPath { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000270 RID: 624
		internal abstract MimeDocument MimeDocument { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000271 RID: 625
		internal abstract string InternetMessageId { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000272 RID: 626
		internal abstract object RecipientCache { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000273 RID: 627
		internal abstract bool MimeWriteStreamOpen { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000274 RID: 628
		internal abstract bool HasBeenDeferred { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000275 RID: 629
		internal abstract bool HasBeenDeleted { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000276 RID: 630
		internal abstract long CachedMimeStreamLength { get; }

		// Token: 0x06000277 RID: 631
		public abstract void SetMustDeliver();

		// Token: 0x06000278 RID: 632
		public abstract Stream GetMimeReadStream();

		// Token: 0x06000279 RID: 633
		public abstract Stream GetMimeWriteStream();

		// Token: 0x0600027A RID: 634
		internal abstract void RestoreLastSavedMime(string agentName, string eventName);

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00006909 File Offset: 0x00004B09
		internal bool IsProbeMessage
		{
			get
			{
				return this.SystemProbeId != Guid.Empty;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000691B File Offset: 0x00004B1B
		internal bool HasRecipients
		{
			get
			{
				return this.Recipients == null || this.Recipients.Any<EnvelopeRecipient>();
			}
		}
	}
}
