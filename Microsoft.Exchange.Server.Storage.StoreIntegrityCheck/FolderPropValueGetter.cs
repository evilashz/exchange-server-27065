using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200000C RID: 12
	public sealed class FolderPropValueGetter
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00004351 File Offset: 0x00002551
		public FolderPropValueGetter(Context context, int mailboxPartitionNumber)
		{
			this.context = context;
			this.mailboxPartitionNumber = mailboxPartitionNumber;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004367 File Offset: 0x00002567
		public ErrorCode Execute(Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			return this.Execute(null, columnsToFetch, accessor, shouldContinue);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004374 File Offset: 0x00002574
		public ErrorCode Execute(byte[] folderId, Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(this.context.Database);
			KeyRange keyRange;
			if (folderId == null)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					this.mailboxPartitionNumber
				});
				keyRange = new KeyRange(startStopKey, startStopKey);
			}
			else
			{
				StartStopKey startStopKey2 = new StartStopKey(true, new object[]
				{
					this.mailboxPartitionNumber,
					folderId
				});
				keyRange = new KeyRange(startStopKey2, startStopKey2);
			}
			using (TableOperator tableOperator = Factory.CreateTableOperator(this.context.Culture, this.context, folderTable.Table, folderTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 0, keyRange, false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						ErrorCode errorCode = accessor(reader);
						if (errorCode != ErrorCode.NoError)
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceError<ErrorCode>(0L, "FolderPropValueGetter.Execute failed with error {0}", errorCode);
							}
							return errorCode.Propagate((LID)51688U);
						}
						if (!shouldContinue())
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
							}
							return ErrorCode.CreateExiting((LID)55784U);
						}
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x04000013 RID: 19
		private readonly Context context;

		// Token: 0x04000014 RID: 20
		private readonly int mailboxPartitionNumber;
	}
}
