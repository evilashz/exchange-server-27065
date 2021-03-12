using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002EE RID: 750
	internal class FindParameters
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00067155 File Offset: 0x00065355
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x0006715D File Offset: 0x0006535D
		internal string MessageId { get; private set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00067166 File Offset: 0x00065366
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x0006716E File Offset: 0x0006536E
		internal RecipientTrackingEvent ReferralEvent { get; private set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00067177 File Offset: 0x00065377
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x0006717F File Offset: 0x0006537F
		internal TrackingAuthority Authority { get; private set; }

		// Token: 0x06001625 RID: 5669 RVA: 0x00067188 File Offset: 0x00065388
		internal FindParameters(string messageId, RecipientTrackingEvent referralEvent, TrackingAuthority authority)
		{
			this.MessageId = messageId;
			this.ReferralEvent = referralEvent;
			this.Authority = authority;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000671A8 File Offset: 0x000653A8
		public override string ToString()
		{
			SmtpAddress? smtpAddress = null;
			if (this.ReferralEvent.EventDescription == EventDescription.TransferredToPartnerOrg && this.ReferralEvent.EventData != null && this.ReferralEvent.EventData.Length > 0)
			{
				smtpAddress = new SmtpAddress?(new SmtpAddress(this.ReferralEvent.EventData[0]));
				if (!smtpAddress.Value.IsValidAddress)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Federated delivery email address invalid", new object[0]);
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "RecipientEvent Invalid: Federated Delivery Address incorrect {0}", new object[]
					{
						smtpAddress.ToString()
					});
				}
			}
			string text = null;
			string text2 = null;
			if (this.ReferralEvent.EventDescription == EventDescription.SmtpSendCrossSite)
			{
				string[] eventData = this.ReferralEvent.EventData;
				if (eventData == null || eventData.Length < 2)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "No server-data for XSite send", new object[0]);
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "RecipientEvent Invalid: No server-name for cross-site SMTP send event", new object[0]);
				}
				text = this.ReferralEvent.EventData[1];
			}
			if (this.ReferralEvent.EventDescription == EventDescription.PendingModeration)
			{
				text2 = this.ReferralEvent.ExtendedProperties.ArbitrationMailboxAddress;
			}
			return string.Format("Fed={0},Mid={1},Server={2},authority={3},ArbMbx={4}", new object[]
			{
				(smtpAddress == null) ? "<null>" : smtpAddress.Value.ToString(),
				this.MessageId,
				(text == null) ? "<null>" : text,
				this.Authority.ToString(),
				(text2 == null) ? "<null>" : text2
			});
		}
	}
}
