using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000079 RID: 121
	internal interface IPhysicalMailbox
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000414 RID: 1044
		ByteQuantifiedSize AttachmentTableTotalSize { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000415 RID: 1045
		// (set) Token: 0x06000416 RID: 1046
		string DatabaseName { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000417 RID: 1047
		ulong DeletedItemCount { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000418 RID: 1048
		DateTime? DisconnectDate { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000419 RID: 1049
		Guid Guid { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600041A RID: 1050
		DirectoryIdentity Identity { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600041B RID: 1051
		bool IsArchive { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600041C RID: 1052
		bool IsDisabled { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600041D RID: 1053
		bool IsMoveDestination { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600041E RID: 1054
		bool IsQuarantined { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600041F RID: 1055
		bool IsSoftDeleted { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000420 RID: 1056
		bool IsConsumer { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000421 RID: 1057
		ulong ItemCount { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000422 RID: 1058
		TimeSpan LastLogonAge { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000423 RID: 1059
		DateTime? LastLogonTimestamp { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000424 RID: 1060
		StoreMailboxType MailboxType { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000425 RID: 1061
		ByteQuantifiedSize MessageTableTotalSize { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000426 RID: 1062
		string Name { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000427 RID: 1063
		Guid OrganizationId { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000428 RID: 1064
		ByteQuantifiedSize OtherTablesTotalSize { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000429 RID: 1065
		ByteQuantifiedSize TotalDeletedItemSize { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600042A RID: 1066
		ByteQuantifiedSize TotalItemSize { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600042B RID: 1067
		ByteQuantifiedSize TotalLogicalSize { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600042C RID: 1068
		ByteQuantifiedSize TotalPhysicalSize { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600042D RID: 1069
		DateTime CreationTimestamp { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600042E RID: 1070
		int ItemsPendingUpgrade { get; }

		// Token: 0x0600042F RID: 1071
		void PopulateLogEntry(MailboxStatisticsLogEntry logEntry);
	}
}
