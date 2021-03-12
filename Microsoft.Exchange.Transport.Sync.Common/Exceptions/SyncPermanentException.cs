using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SyncPermanentException : LocalizedException, ISyncException
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000048F7 File Offset: 0x00002AF7
		public SyncPermanentException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004900 File Offset: 0x00002B00
		public SyncPermanentException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000490A File Offset: 0x00002B0A
		protected SyncPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004914 File Offset: 0x00002B14
		private SyncPermanentException(bool isItemException, DetailedAggregationStatus detailedAggregationStatus, EventLogEntry eventLogEntry, Exception innerException, bool isPromotedFromTransientException) : base(LocalizedString.Empty, innerException)
		{
			SyncUtilities.ThrowIfArgumentNull("innerException", innerException);
			this.isItemException = isItemException;
			this.detailedAggregationStatus = detailedAggregationStatus;
			this.eventLogEntry = eventLogEntry;
			this.isPromotedFromTransientException = isPromotedFromTransientException;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000494C File Offset: 0x00002B4C
		public DetailedAggregationStatus DetailedAggregationStatus
		{
			get
			{
				return this.detailedAggregationStatus;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004954 File Offset: 0x00002B54
		public bool IsItemException
		{
			get
			{
				return this.isItemException;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000495C File Offset: 0x00002B5C
		public bool IsPromotedFromTransientException
		{
			get
			{
				return this.isPromotedFromTransientException;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004964 File Offset: 0x00002B64
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000496C File Offset: 0x00002B6C
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

		// Token: 0x060000DC RID: 220 RVA: 0x00004975 File Offset: 0x00002B75
		public static SyncPermanentException CreateOperationLevelException(DetailedAggregationStatus detailedAggregationStatus, Exception innerException)
		{
			return new SyncPermanentException(false, detailedAggregationStatus, null, innerException, false);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004981 File Offset: 0x00002B81
		public static SyncPermanentException CreateItemLevelException(Exception innerException)
		{
			return new SyncPermanentException(true, DetailedAggregationStatus.None, null, innerException, false);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000498D File Offset: 0x00002B8D
		public static SyncPermanentException CreateItemLevelExceptionPromotedFromTransientException(Exception innerException)
		{
			return new SyncPermanentException(true, DetailedAggregationStatus.None, null, innerException, true);
		}

		// Token: 0x040000C9 RID: 201
		private readonly DetailedAggregationStatus detailedAggregationStatus;

		// Token: 0x040000CA RID: 202
		private EventLogEntry eventLogEntry;

		// Token: 0x040000CB RID: 203
		private bool isItemException;

		// Token: 0x040000CC RID: 204
		private bool isPromotedFromTransientException;
	}
}
