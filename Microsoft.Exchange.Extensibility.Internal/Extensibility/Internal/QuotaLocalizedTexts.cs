using System;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000042 RID: 66
	internal sealed class QuotaLocalizedTexts
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000102C8 File Offset: 0x0000E4C8
		private QuotaLocalizedTexts(LocalizedString subject, LocalizedString topText, LocalizedString details)
		{
			this.subject = subject;
			this.topText = topText;
			this.details = details;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000102E5 File Offset: 0x0000E4E5
		public LocalizedString TopText
		{
			get
			{
				return this.topText;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000102ED File Offset: 0x0000E4ED
		public LocalizedString Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000102F5 File Offset: 0x0000E4F5
		public LocalizedString Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00010300 File Offset: 0x0000E500
		public static QuotaLocalizedTexts GetQuotaLocalizedTexts(QuotaMessageType quotaMessageType, string folderName, string currentSize, bool isPrimaryMailbox)
		{
			switch (quotaMessageType)
			{
			case QuotaMessageType.WarningMailboxUnlimitedSize:
				if (!isPrimaryMailbox)
				{
					return new QuotaLocalizedTexts(SystemMessages.ArchiveQuotaWarningNoLimitSubject, SystemMessages.ArchiveQuotaWarningNoLimitTopText(currentSize), SystemMessages.ArchiveQuotaWarningNoLimitDetails);
				}
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningNoLimitSubject, SystemMessages.QuotaWarningNoLimitTopText(currentSize), SystemMessages.QuotaWarningNoLimitDetails);
			case QuotaMessageType.WarningPublicFolderUnlimitedSize:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningNoLimitSubjectPF, SystemMessages.QuotaWarningNoLimitTopTextPF(folderName, currentSize), SystemMessages.QuotaWarningNoLimitDetailsPF);
			case QuotaMessageType.WarningMailbox:
				if (!isPrimaryMailbox)
				{
					return QuotaLocalizedTexts.archiveQuotaWarningTexts;
				}
				return QuotaLocalizedTexts.quotaWarningTexts;
			case QuotaMessageType.WarningPublicFolder:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningSubjectPF, SystemMessages.QuotaWarningTopTextPF(folderName), SystemMessages.QuotaWarningDetailsPF);
			case QuotaMessageType.ProhibitSendMailbox:
				return QuotaLocalizedTexts.quotaSendTexts;
			case QuotaMessageType.ProhibitPostPublicFolder:
				return new QuotaLocalizedTexts(SystemMessages.QuotaSendSubjectPF, SystemMessages.QuotaSendTopTextPF(folderName), SystemMessages.QuotaSendDetailsPF);
			case QuotaMessageType.ProhibitSendReceiveMailBox:
				if (!isPrimaryMailbox)
				{
					return QuotaLocalizedTexts.archiveQuotaFullTexts;
				}
				return QuotaLocalizedTexts.quotaSendReceiveTexts;
			case QuotaMessageType.WarningMailboxMessagesPerFolderCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningMailboxMessagesPerFolderCountSubject, SystemMessages.QuotaWarningMailboxMessagesPerFolderCountTopText(folderName, currentSize), SystemMessages.QuotaWarningMailboxMessagesPerFolderCountDetails);
			case QuotaMessageType.ProhibitReceiveMailboxMessagesPerFolderCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaProhibitReceiveMailboxMessagesPerFolderCountSubject, SystemMessages.QuotaProhibitReceiveMailboxMessagesPerFolderCountTopText(folderName, currentSize), SystemMessages.QuotaProhibitReceiveMailboxMessagesPerFolderCountDetails);
			case QuotaMessageType.WarningFolderHierarchyChildrenCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningFolderHierarchyChildrenCountSubject, SystemMessages.QuotaWarningFolderHierarchyChildrenCountTopText(folderName, currentSize), SystemMessages.QuotaWarningFolderHierarchyChildrenCountDetails);
			case QuotaMessageType.ProhibitReceiveFolderHierarchyChildrenCountCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaProhibitReceiveFolderHierarchyChildrenCountSubject, SystemMessages.QuotaProhibitReceiveFolderHierarchyChildrenCountTopText(folderName, currentSize), SystemMessages.QuotaProhibitReceiveFolderHierarchyChildrenCountDetails);
			case QuotaMessageType.WarningMailboxMessagesPerFolderUnlimitedCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningMailboxMessagesPerFolderNoLimitSubject, SystemMessages.QuotaWarningMailboxMessagesPerFolderNoLimitTopText(folderName, currentSize), SystemMessages.QuotaWarningMailboxMessagesPerFolderNoLimitDetails);
			case QuotaMessageType.WarningFolderHierarchyChildrenUnlimitedCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningFolderHierarchyChildrenNoLimitSubject, SystemMessages.QuotaWarningFolderHierarchyChildrenNoLimitTopText(folderName, currentSize), SystemMessages.QuotaWarningFolderHierarchyChildrenNoLimitDetails);
			case QuotaMessageType.WarningFolderHierarchyDepth:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningFolderHierarchyDepthSubject, SystemMessages.QuotaWarningFolderHierarchyDepthTopText(folderName, currentSize), SystemMessages.QuotaWarningFolderHierarchyDepthDetails);
			case QuotaMessageType.ProhibitReceiveFolderHierarchyDepth:
				return new QuotaLocalizedTexts(SystemMessages.QuotaProhibitReceiveFolderHierarchyDepthSubject, SystemMessages.QuotaProhibitReceiveFolderHierarchyDepthTopText(folderName, currentSize), SystemMessages.QuotaProhibitReceiveFolderHierarchyDepthDetails);
			case QuotaMessageType.WarningFolderHierarchyDepthUnlimited:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningFolderHierarchyDepthNoLimitSubject, SystemMessages.QuotaWarningFolderHierarchyDepthNoLimitTopText(folderName, currentSize), SystemMessages.QuotaWarningFolderHierarchyDepthNoLimitDetails);
			case QuotaMessageType.WarningFoldersCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningFoldersCountSubject, SystemMessages.QuotaWarningFoldersCountTopText(currentSize), SystemMessages.QuotaWarningFoldersCountDetails);
			case QuotaMessageType.ProhibitReceiveFoldersCount:
				return new QuotaLocalizedTexts(SystemMessages.QuotaProhibitReceiveFoldersCountSubject, SystemMessages.QuotaProhibitReceiveFoldersCountTopText(currentSize), SystemMessages.QuotaProhibitReceiveFoldersCountDetails);
			case QuotaMessageType.WarningFoldersCountUnlimited:
				return new QuotaLocalizedTexts(SystemMessages.QuotaWarningFoldersCountNoLimitSubject, SystemMessages.QuotaWarningFoldersCountNoLimitTopText(currentSize), SystemMessages.QuotaWarningFoldersCountNoLimitDetails);
			default:
				throw new NotSupportedException("quotaMessageType invalid");
			}
		}

		// Token: 0x04000324 RID: 804
		private static readonly QuotaLocalizedTexts quotaWarningTexts = new QuotaLocalizedTexts(SystemMessages.QuotaWarningSubject, SystemMessages.QuotaWarningTopText, SystemMessages.QuotaWarningDetails);

		// Token: 0x04000325 RID: 805
		private static readonly QuotaLocalizedTexts quotaSendTexts = new QuotaLocalizedTexts(SystemMessages.QuotaSendSubject, SystemMessages.QuotaSendTopText, SystemMessages.QuotaSendDetails);

		// Token: 0x04000326 RID: 806
		private static readonly QuotaLocalizedTexts quotaSendReceiveTexts = new QuotaLocalizedTexts(SystemMessages.QuotaSendReceiveSubject, SystemMessages.QuotaSendReceiveTopText, SystemMessages.QuotaSendReceiveDetails);

		// Token: 0x04000327 RID: 807
		private static readonly QuotaLocalizedTexts archiveQuotaWarningTexts = new QuotaLocalizedTexts(SystemMessages.ArchiveQuotaWarningSubject, SystemMessages.ArchiveQuotaWarningTopText, SystemMessages.ArchiveQuotaWarningDetails);

		// Token: 0x04000328 RID: 808
		private static readonly QuotaLocalizedTexts archiveQuotaFullTexts = new QuotaLocalizedTexts(SystemMessages.ArchiveQuotaFullSubject, SystemMessages.ArchiveQuotaFullTopText, SystemMessages.ArchiveQuotaFullDetails);

		// Token: 0x04000329 RID: 809
		private LocalizedString subject;

		// Token: 0x0400032A RID: 810
		private LocalizedString topText;

		// Token: 0x0400032B RID: 811
		private LocalizedString details;
	}
}
