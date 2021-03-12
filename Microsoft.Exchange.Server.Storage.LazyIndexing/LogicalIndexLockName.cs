using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000023 RID: 35
	public class LogicalIndexLockName : LogicalIndexComponentBase
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00010152 File Offset: 0x0000E352
		public override Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0001015A File Offset: 0x0000E35A
		public override int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00010162 File Offset: 0x0000E362
		public override int LogicalIndexNumber
		{
			get
			{
				return this.logicalIndexNumber;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0001016A File Offset: 0x0000E36A
		public LogicalIndexLockName(Guid databaseGuid, int mailboxPartitionNumber, int logicalIndexNumber)
		{
			this.databaseGuid = databaseGuid;
			this.mailboxPartitionNumber = mailboxPartitionNumber;
			this.logicalIndexNumber = logicalIndexNumber;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00010187 File Offset: 0x0000E387
		public override ILockName GetLockNameToCache()
		{
			return this;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0001018A File Offset: 0x0000E38A
		public override bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			return false;
		}

		// Token: 0x040000FF RID: 255
		private readonly Guid databaseGuid;

		// Token: 0x04000100 RID: 256
		private readonly int mailboxPartitionNumber;

		// Token: 0x04000101 RID: 257
		private readonly int logicalIndexNumber;
	}
}
