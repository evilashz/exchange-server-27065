using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SyncTransientException : TransientException, ISyncException
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00007841 File Offset: 0x00005A41
		private SyncTransientException(bool isItemException, DetailedAggregationStatus detailedAggregationStatus, EventLogEntry eventLogEntry, bool needsBackoff, Exception innerException) : base(LocalizedString.Empty, innerException)
		{
			SyncUtilities.ThrowIfArgumentNull("innerException", innerException);
			this.isItemException = isItemException;
			this.detailedAggregationStatus = detailedAggregationStatus;
			this.eventLogEntry = eventLogEntry;
			this.needsBackoff = needsBackoff;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00007879 File Offset: 0x00005A79
		public DetailedAggregationStatus DetailedAggregationStatus
		{
			get
			{
				return this.detailedAggregationStatus;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007881 File Offset: 0x00005A81
		public bool NeedsBackoff
		{
			get
			{
				return this.needsBackoff;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00007889 File Offset: 0x00005A89
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00007891 File Offset: 0x00005A91
		public EventLogEntry EventLogEntry
		{
			get
			{
				return this.eventLogEntry;
			}
			set
			{
				this.eventLogEntry = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000789A File Offset: 0x00005A9A
		public bool IsItemException
		{
			get
			{
				return this.isItemException;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000078A2 File Offset: 0x00005AA2
		public static SyncTransientException CreateOperationLevelException(DetailedAggregationStatus detailedAggregationStatus, Exception innerException, bool needsBackoff)
		{
			return new SyncTransientException(false, detailedAggregationStatus, null, needsBackoff, innerException);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000078AE File Offset: 0x00005AAE
		public static SyncTransientException CreateOperationLevelException(DetailedAggregationStatus detailedAggregationStatus, Exception innerException)
		{
			return SyncTransientException.CreateOperationLevelException(detailedAggregationStatus, innerException, SyncTransientException.DefaultBackoff);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000078BC File Offset: 0x00005ABC
		public static SyncTransientException CreateItemLevelException(Exception innerException)
		{
			return new SyncTransientException(true, DetailedAggregationStatus.None, null, false, innerException);
		}

		// Token: 0x0400011F RID: 287
		private static readonly bool DefaultBackoff;

		// Token: 0x04000120 RID: 288
		private readonly DetailedAggregationStatus detailedAggregationStatus;

		// Token: 0x04000121 RID: 289
		private readonly bool needsBackoff;

		// Token: 0x04000122 RID: 290
		private bool isItemException;

		// Token: 0x04000123 RID: 291
		private EventLogEntry eventLogEntry;
	}
}
