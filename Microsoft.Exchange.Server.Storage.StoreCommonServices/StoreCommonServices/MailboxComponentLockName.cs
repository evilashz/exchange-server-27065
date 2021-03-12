using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000A7 RID: 167
	public class MailboxComponentLockName : LockableMailboxComponent
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00023BE8 File Offset: 0x00021DE8
		public MailboxComponentLockName(MailboxComponentId componentId, int mailboxPartitionNumber, Guid databaseGuid, Connection.OperationType operationType, Table table) : this(LockManager.LockType.MailboxComponentsShared, componentId, mailboxPartitionNumber, databaseGuid, operationType, table)
		{
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00023BF9 File Offset: 0x00021DF9
		public MailboxComponentLockName(LockManager.LockType readerLockType, MailboxComponentId componentId, int mailboxPartitionNumber, Guid databaseGuid) : this(readerLockType, componentId, mailboxPartitionNumber, databaseGuid, Connection.OperationType.Query, null)
		{
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00023C08 File Offset: 0x00021E08
		private MailboxComponentLockName(LockManager.LockType readerLockType, MailboxComponentId componentId, int mailboxPartitionNumber, Guid databaseGuid, Connection.OperationType operationType, Table table)
		{
			this.readerLockType = readerLockType;
			this.componentId = componentId;
			this.mailboxPartitionNumber = mailboxPartitionNumber;
			this.databaseGuid = databaseGuid;
			this.operationType = operationType;
			this.table = table;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00023C3D File Offset: 0x00021E3D
		public override MailboxComponentId MailboxComponentId
		{
			get
			{
				return this.componentId;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00023C45 File Offset: 0x00021E45
		public override Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00023C4D File Offset: 0x00021E4D
		public override int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00023C55 File Offset: 0x00021E55
		public override LockManager.LockType ReaderLockType
		{
			get
			{
				return this.readerLockType;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00023C5D File Offset: 0x00021E5D
		public override bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (operationType != this.operationType || !table.Equals(this.table))
			{
				return false;
			}
			if (operationType == Connection.OperationType.Query)
			{
				return this.TestSharedLock() || this.TestExclusiveLock();
			}
			return this.TestExclusiveLock();
		}

		// Token: 0x04000429 RID: 1065
		private readonly MailboxComponentId componentId;

		// Token: 0x0400042A RID: 1066
		private readonly int mailboxPartitionNumber;

		// Token: 0x0400042B RID: 1067
		private readonly Guid databaseGuid;

		// Token: 0x0400042C RID: 1068
		private Connection.OperationType operationType;

		// Token: 0x0400042D RID: 1069
		private Table table;

		// Token: 0x0400042E RID: 1070
		private LockManager.LockType readerLockType;
	}
}
