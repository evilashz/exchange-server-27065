using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public class MessageTrackingSearchResult : MessageTrackingConfigurableObject
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00016CA2 File Offset: 0x00014EA2
		public MessageTrackingSearchResult()
		{
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00016CAA File Offset: 0x00014EAA
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MessageTrackingSearchResult.schema;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00016CB1 File Offset: 0x00014EB1
		public MessageTrackingReportId MessageTrackingReportId
		{
			get
			{
				return (MessageTrackingReportId)this[MessageTrackingSharedResultSchema.MessageTrackingReportId];
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00016CC3 File Offset: 0x00014EC3
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00016CD5 File Offset: 0x00014ED5
		public DateTime SubmittedDateTime
		{
			get
			{
				return (DateTime)this[MessageTrackingSharedResultSchema.SubmittedDateTime];
			}
			internal set
			{
				this[MessageTrackingSharedResultSchema.SubmittedDateTime] = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00016CE8 File Offset: 0x00014EE8
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00016CFA File Offset: 0x00014EFA
		public string Subject
		{
			get
			{
				return (string)this[MessageTrackingSharedResultSchema.Subject];
			}
			internal set
			{
				this[MessageTrackingSharedResultSchema.Subject] = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00016D08 File Offset: 0x00014F08
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00016D1A File Offset: 0x00014F1A
		public SmtpAddress FromAddress
		{
			get
			{
				return (SmtpAddress)this[MessageTrackingSharedResultSchema.FromAddress];
			}
			set
			{
				this[MessageTrackingSharedResultSchema.FromAddress] = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00016D2D File Offset: 0x00014F2D
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00016D3F File Offset: 0x00014F3F
		public string FromDisplayName
		{
			get
			{
				return (string)this[MessageTrackingSharedResultSchema.FromDisplayName];
			}
			set
			{
				this[MessageTrackingSharedResultSchema.FromDisplayName] = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00016D4D File Offset: 0x00014F4D
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00016D5F File Offset: 0x00014F5F
		public SmtpAddress[] RecipientAddresses
		{
			get
			{
				return (SmtpAddress[])this[MessageTrackingSharedResultSchema.RecipientAddresses];
			}
			internal set
			{
				this[MessageTrackingSharedResultSchema.RecipientAddresses] = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00016D6D File Offset: 0x00014F6D
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00016D7F File Offset: 0x00014F7F
		public string[] RecipientDisplayNames
		{
			get
			{
				return (string[])this[MessageTrackingSharedResultSchema.RecipientDisplayNames];
			}
			set
			{
				this[MessageTrackingSharedResultSchema.RecipientDisplayNames] = value;
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00016D90 File Offset: 0x00014F90
		internal MessageTrackingSearchResult(MessageTrackingSearchResult internalMessageTrackingSearchResult)
		{
			this[MessageTrackingSharedResultSchema.MessageTrackingReportId] = new MessageTrackingReportId(internalMessageTrackingSearchResult.MessageTrackingReportId);
			this[MessageTrackingSharedResultSchema.SubmittedDateTime] = internalMessageTrackingSearchResult.SubmittedDateTime;
			this[MessageTrackingSharedResultSchema.Subject] = internalMessageTrackingSearchResult.Subject;
			this[MessageTrackingSharedResultSchema.FromAddress] = internalMessageTrackingSearchResult.FromAddress;
			this[MessageTrackingSharedResultSchema.FromDisplayName] = internalMessageTrackingSearchResult.FromDisplayName;
			this[MessageTrackingSharedResultSchema.RecipientAddresses] = internalMessageTrackingSearchResult.RecipientAddresses;
			this[MessageTrackingSharedResultSchema.RecipientDisplayNames] = null;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00016E34 File Offset: 0x00015034
		internal static void FillDisplayNames(List<MessageTrackingSearchResult> results, IRecipientSession recipSession)
		{
			BulkRecipientLookupCache bulkRecipientLookupCache = new BulkRecipientLookupCache(100);
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in results)
			{
				IEnumerable<string> addresses = from address in messageTrackingSearchResult.RecipientAddresses
				select address.ToString();
				messageTrackingSearchResult.RecipientDisplayNames = bulkRecipientLookupCache.Resolve(addresses, recipSession).ToArray<string>();
			}
		}

		// Token: 0x0400020F RID: 527
		private static MessageTrackingSharedResultSchema schema = ObjectSchema.GetInstance<MessageTrackingSharedResultSchema>();
	}
}
