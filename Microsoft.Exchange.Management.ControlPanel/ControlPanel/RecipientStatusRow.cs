using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.Tracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C8 RID: 712
	[DataContract]
	public class RecipientStatusRow : BaseRow
	{
		// Token: 0x06002C3C RID: 11324 RVA: 0x00088F82 File Offset: 0x00087182
		public RecipientStatusRow(Identity messageTrackingReportIdentity, RecipientTrackingEvent trackingEvent) : base(RecipientStatusRow.CreateRecipientStatusRowIdentity(messageTrackingReportIdentity, trackingEvent), trackingEvent)
		{
			this.RecipientTrackingEvent = trackingEvent;
		}

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x00088F99 File Offset: 0x00087199
		// (set) Token: 0x06002C3E RID: 11326 RVA: 0x00088FA1 File Offset: 0x000871A1
		public RecipientTrackingEvent RecipientTrackingEvent { get; private set; }

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x00088FAA File Offset: 0x000871AA
		// (set) Token: 0x06002C40 RID: 11328 RVA: 0x00088FB7 File Offset: 0x000871B7
		[DataMember]
		public string RecipientDisplayName
		{
			get
			{
				return this.RecipientTrackingEvent.RecipientDisplayName;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x00088FC0 File Offset: 0x000871C0
		// (set) Token: 0x06002C42 RID: 11330 RVA: 0x00088FE6 File Offset: 0x000871E6
		[DataMember]
		public string RecipientEmail
		{
			get
			{
				return this.RecipientTrackingEvent.RecipientAddress.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x00088FED File Offset: 0x000871ED
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x0008900E File Offset: 0x0008720E
		[DataMember]
		public string DeliveryStatus
		{
			get
			{
				return LocalizedDescriptionAttribute.FromEnumForOwaOption(typeof(_DeliveryStatus), this.RecipientTrackingEvent.Status);
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x00089015 File Offset: 0x00087215
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x00089027 File Offset: 0x00087227
		[DataMember]
		public string Date
		{
			get
			{
				return this.RecipientTrackingEvent.Date.UtcToUserDateTimeString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x0008902E File Offset: 0x0008722E
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x0008903B File Offset: 0x0008723B
		public DateTime UTCDate
		{
			get
			{
				return this.RecipientTrackingEvent.Date;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x00089042 File Offset: 0x00087242
		// (set) Token: 0x06002C4A RID: 11338 RVA: 0x0008904F File Offset: 0x0008724F
		public RecipientDeliveryStatus RecipientDeliveryStatus
		{
			get
			{
				return (RecipientDeliveryStatus)this.RecipientTrackingEvent.Status;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x00089058 File Offset: 0x00087258
		private static Identity CreateRecipientStatusRowIdentity(Identity messageTrackingReportIdentity, RecipientTrackingEvent trackingEvent)
		{
			string recipient = trackingEvent.RecipientAddress.ToString();
			string displayName = trackingEvent.RecipientAddress.ToString();
			RecipientMessageTrackingReportId recipientMessageTrackingReportId = new RecipientMessageTrackingReportId(messageTrackingReportIdentity.RawIdentity, recipient);
			return new Identity(recipientMessageTrackingReportId.RawIdentity, displayName);
		}
	}
}
