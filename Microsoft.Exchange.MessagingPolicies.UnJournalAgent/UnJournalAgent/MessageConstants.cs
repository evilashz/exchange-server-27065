using System;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x0200000C RID: 12
	internal static class MessageConstants
	{
		// Token: 0x0200000D RID: 13
		public static class MimeHeader
		{
			// Token: 0x04000064 RID: 100
			public const string ContentTransferEncodingSevenBit = "7bit";

			// Token: 0x04000065 RID: 101
			public const string ContentTypePlainText = "plaintext";

			// Token: 0x04000066 RID: 102
			public const string E12EnvelopeJournal = "X-MS-Journal-Report";

			// Token: 0x04000067 RID: 103
			public const string ContentIdentifier = "Content-Identifier";

			// Token: 0x04000068 RID: 104
			public const string ContentIdentifierTypo = "Content-Identifer";

			// Token: 0x04000069 RID: 105
			public const string ExchangeJournalReport = "exjournalreport";

			// Token: 0x0400006A RID: 106
			public const string OriginalMessageDate = "X-MS-EHA-MessageDate";

			// Token: 0x0400006B RID: 107
			public const string EHAConfirmBatchSize = "X-MS-EHA-ConfirmBatchSize";

			// Token: 0x0400006C RID: 108
			public const string EHAConfirmTimeout = "X-MS-EHA-ConfirmTimeout";

			// Token: 0x0400006D RID: 109
			public const string EHAMessageRetainUntil = "X-MS-EHA-MessageExpiryDate";

			// Token: 0x0400006E RID: 110
			public const string EHAMessageID = "X-MS-EHAMessageID";

			// Token: 0x0400006F RID: 111
			public const string ProcessedByUnjournal = "X-MS-Exchange-Organization-Unjournal-Processed";

			// Token: 0x04000070 RID: 112
			public const string SenderIsRecipient = "X-MS-Exchange-Organization-Unjournal-SenderIsRecipient";

			// Token: 0x04000071 RID: 113
			public const string SenderAddress = "X-MS-Exchange-Organization-Unjournal-SenderAddress";

			// Token: 0x04000072 RID: 114
			public const string ProcessedByUnjournalForNdr = "X-MS-Exchange-Organization-Unjournal-ProcessedNdr";

			// Token: 0x04000073 RID: 115
			public const string MessageOriginalDate = "X-MS-Exchange-Organization-Unjournal-OriginalReceiveDate";

			// Token: 0x04000074 RID: 116
			public const string MessageExpiryDate = "X-MS-Exchange-Organization-Unjournal-OriginalExpiryDate";

			// Token: 0x04000075 RID: 117
			public const string InternalJournalReport = "X-MS-InternalJournal";
		}

		// Token: 0x0200000E RID: 14
		public static class AddressType
		{
			// Token: 0x04000076 RID: 118
			public const string SMTP = "smtp";
		}

		// Token: 0x0200000F RID: 15
		public static class JournalReportField
		{
			// Token: 0x04000077 RID: 119
			public const string Recipient = "Recipient";

			// Token: 0x04000078 RID: 120
			public const string To = "To";

			// Token: 0x04000079 RID: 121
			public const string Cc = "Cc";

			// Token: 0x0400007A RID: 122
			public const string Bcc = "Bcc";

			// Token: 0x0400007B RID: 123
			public const string OnBehalfOf = "On-Behalf-Of";

			// Token: 0x0400007C RID: 124
			public const string Sender = "Sender:";

			// Token: 0x0400007D RID: 125
			public const string MessageId = "Message-ID:";

			// Token: 0x0400007E RID: 126
			public const string Recipients = "Recipients:";
		}

		// Token: 0x02000010 RID: 16
		public static class ContentType
		{
			// Token: 0x0400007F RID: 127
			public const string MSTnef = "application/ms-tnef";

			// Token: 0x04000080 RID: 128
			public const string TextPlain = "text/plain";

			// Token: 0x04000081 RID: 129
			public const string TextHtml = "text/html";

			// Token: 0x04000082 RID: 130
			public const string MessageRfc822 = "message/rfc822";
		}

		// Token: 0x02000011 RID: 17
		public static class EHAJournalHeaderDefaults
		{
			// Token: 0x04000083 RID: 131
			public const int DefaultConfirmBatchSize = 1000;

			// Token: 0x04000084 RID: 132
			public const int DefaultConfirmTimeout = 3600;

			// Token: 0x04000085 RID: 133
			public static readonly DateTime DefaultRetainUntilDate = DateTime.MaxValue;
		}

		// Token: 0x02000012 RID: 18
		public static class EHAConfirmationMessage
		{
			// Token: 0x04000086 RID: 134
			public const string EHAJournalLogSegmentStatus = ",status=<";

			// Token: 0x04000087 RID: 135
			public const string EHAJournalLogSegmentDefectivePermanentError = ",<permanenterror>";

			// Token: 0x04000088 RID: 136
			public const string EHAJournalLogSegmentDefectiveUnprovisionedUsers = ",<unprovisionedusers>";

			// Token: 0x04000089 RID: 137
			public const string EHAJournalLogSegmentDefectiveNoUserResolved = ",<nousersresolved>";

			// Token: 0x0400008A RID: 138
			public const string EHAJournalLogSegmentDefectiveDistributionGroup = ",<distributiongroup>";

			// Token: 0x0400008B RID: 139
			public const string EHAJournalLogSegmentDefectiveUnprovisionedUsersAndDistributionGroups = ",<unprovisionedanddistributionlistusers>";

			// Token: 0x0400008C RID: 140
			public const string EHAJournalLogSegmentSender = ",SND=<";

			// Token: 0x0400008D RID: 141
			public const string EHAJournalLogSegmentRecipient = ",RCP=<";

			// Token: 0x0400008E RID: 142
			public const string EHAJournalLogSegmentBatchSize = ",RBS=<";

			// Token: 0x0400008F RID: 143
			public const string EHAJournalLogSegmentTimeout = ",RTO=<";

			// Token: 0x04000090 RID: 144
			public const string EHAJournalLogSegmentMessageID = ",RID=<";

			// Token: 0x04000091 RID: 145
			public const string EHAJournalLogSegmentExpirationDate = ",RXD=<";

			// Token: 0x04000092 RID: 146
			public const string EHAJournalLogSegmentExternalOrganizationId = ",ExtOrgId=<";

			// Token: 0x04000093 RID: 147
			public const string EHAJournalLogSegmentClose = ">";

			// Token: 0x04000094 RID: 148
			public const string EHAJournalLogTitle = "EHAJournal";
		}
	}
}
