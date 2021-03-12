using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002D2 RID: 722
	[Serializable]
	internal sealed class MessageTrackingSearchResult
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x0005FE8C File Offset: 0x0005E08C
		internal static MessageTrackingSearchResult Create(FindMessageTrackingSearchResultType wsResult, string targetInfoForDisplay)
		{
			if (wsResult.Sender == null)
			{
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: Sender is null in FindMessageTrackingReport response from {0}", new object[]
				{
					targetInfoForDisplay
				});
			}
			SmtpAddress smtpAddress = new SmtpAddress(wsResult.Sender.EmailAddress);
			if (!smtpAddress.IsValidAddress || string.IsNullOrEmpty(wsResult.Sender.Name))
			{
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: Sender {0} is invalid in FindMessageTrackingReport response from {1}", new object[]
				{
					smtpAddress.ToString(),
					targetInfoForDisplay
				});
			}
			smtpAddress = SmtpAddress.Parse(wsResult.Sender.EmailAddress);
			string name = wsResult.Sender.Name;
			EmailAddressType[] recipients = wsResult.Recipients;
			SmtpAddress[] array = new SmtpAddress[recipients.Length];
			if (recipients == null || recipients.Length == 0)
			{
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: No recipients in FindMessageTrackingReport response from {0}", new object[]
				{
					targetInfoForDisplay
				});
			}
			for (int i = 0; i < recipients.Length; i++)
			{
				if (recipients[i] == null)
				{
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: Null recipient in FindMessageTrackingReport response from {0}", new object[]
					{
						targetInfoForDisplay
					});
				}
				array[i] = new SmtpAddress(recipients[i].EmailAddress);
				if (!array[i].IsValidAddress)
				{
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: Invalid Recipient {0} in FindMessageTrackingReport response from {1}", new object[]
					{
						array[i].ToString(),
						targetInfoForDisplay
					});
				}
			}
			MessageTrackingReportId identity = null;
			if (!MessageTrackingReportId.TryParse(wsResult.MessageTrackingReportId, out identity))
			{
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: Invalid report ID {0} in FindMessageTrackingReport response from {1}", new object[]
				{
					wsResult.MessageTrackingReportId,
					targetInfoForDisplay
				});
			}
			return new MessageTrackingSearchResult(identity, smtpAddress, name, array, wsResult.Subject, wsResult.SubmittedTime, wsResult.PreviousHopServer, wsResult.FirstHopServer);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00060059 File Offset: 0x0005E259
		internal static int CompareSearchResultsByTimeStampDescending(MessageTrackingSearchResult leftValue, MessageTrackingSearchResult rightValue)
		{
			if (leftValue == null)
			{
				throw new ArgumentNullException("leftValue");
			}
			if (rightValue == null)
			{
				throw new ArgumentNullException("rightValue");
			}
			return rightValue.submittedDateTime.CompareTo(leftValue.submittedDateTime);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00060088 File Offset: 0x0005E288
		public MessageTrackingSearchResult()
		{
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00060090 File Offset: 0x0005E290
		public MessageTrackingSearchResult(MessageTrackingReportId identity, SmtpAddress fromAddress, string fromDisplayName, SmtpAddress[] recipientAddresses, string subject, DateTime submittedDateTime, string previousHopServer, string firstHopServer)
		{
			this.messageTrackingReportId = identity;
			this.fromAddress = fromAddress;
			this.fromDisplayName = fromDisplayName;
			this.recipientAddresses = recipientAddresses;
			this.submittedDateTime = submittedDateTime;
			this.subject = subject;
			this.previousHopServer = previousHopServer;
			this.firstHopServer = firstHopServer;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x000600E0 File Offset: 0x0005E2E0
		public MessageTrackingReportId MessageTrackingReportId
		{
			get
			{
				return this.messageTrackingReportId;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x000600E8 File Offset: 0x0005E2E8
		public string PreviousHopServer
		{
			get
			{
				return this.previousHopServer;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x000600F0 File Offset: 0x0005E2F0
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x000600F8 File Offset: 0x0005E2F8
		public DateTime SubmittedDateTime
		{
			get
			{
				return this.submittedDateTime;
			}
			internal set
			{
				this.submittedDateTime = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00060101 File Offset: 0x0005E301
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x00060109 File Offset: 0x0005E309
		public string Subject
		{
			get
			{
				return this.subject;
			}
			internal set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00060112 File Offset: 0x0005E312
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0006011A File Offset: 0x0005E31A
		public SmtpAddress FromAddress
		{
			get
			{
				return this.fromAddress;
			}
			set
			{
				this.fromAddress = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00060123 File Offset: 0x0005E323
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0006012B File Offset: 0x0005E32B
		public string FromDisplayName
		{
			get
			{
				return this.fromDisplayName;
			}
			set
			{
				this.fromDisplayName = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00060134 File Offset: 0x0005E334
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0006013C File Offset: 0x0005E33C
		public SmtpAddress[] RecipientAddresses
		{
			get
			{
				return this.recipientAddresses;
			}
			internal set
			{
				this.recipientAddresses = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00060145 File Offset: 0x0005E345
		public string FirstHopServer
		{
			get
			{
				return this.firstHopServer;
			}
		}

		// Token: 0x04000D70 RID: 3440
		private MessageTrackingReportId messageTrackingReportId;

		// Token: 0x04000D71 RID: 3441
		private string previousHopServer;

		// Token: 0x04000D72 RID: 3442
		private string firstHopServer;

		// Token: 0x04000D73 RID: 3443
		private string subject;

		// Token: 0x04000D74 RID: 3444
		private DateTime submittedDateTime;

		// Token: 0x04000D75 RID: 3445
		private SmtpAddress fromAddress;

		// Token: 0x04000D76 RID: 3446
		private string fromDisplayName;

		// Token: 0x04000D77 RID: 3447
		private SmtpAddress[] recipientAddresses;
	}
}
