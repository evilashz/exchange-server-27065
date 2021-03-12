using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200002A RID: 42
	public sealed class MessagePropValueGetter
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00006EE3 File Offset: 0x000050E3
		public MessagePropValueGetter(Context context, int mailboxPartitionNumber, ExchangeId folderId)
		{
			this.context = context;
			this.mailboxPartitionNumber = mailboxPartitionNumber;
			this.folderId = folderId;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006F00 File Offset: 0x00005100
		public ErrorCode Execute(Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			return this.Execute(true, columnsToFetch, accessor, shouldContinue);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006F0C File Offset: 0x0000510C
		public ErrorCode Execute(bool opportunedPreread, Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			ErrorCode errorCode = this.Execute(false, opportunedPreread, null, columnsToFetch, accessor, shouldContinue);
			if (errorCode == ErrorCode.NoError)
			{
				errorCode = this.Execute(true, opportunedPreread, null, columnsToFetch, accessor, shouldContinue);
			}
			return errorCode;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006F43 File Offset: 0x00005143
		public ErrorCode Execute(bool associated, byte[] mid, Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			return this.Execute(associated, true, mid, columnsToFetch, accessor, shouldContinue);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006F54 File Offset: 0x00005154
		public ErrorCode Execute(bool associated, bool opportunedPreread, byte[] mid, Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(this.context.Database);
			KeyRange keyRange;
			if (mid == null)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					this.mailboxPartitionNumber,
					this.folderId.To26ByteArray(),
					associated
				});
				keyRange = new KeyRange(startStopKey, startStopKey);
			}
			else
			{
				StartStopKey startStopKey2 = new StartStopKey(true, new object[]
				{
					this.mailboxPartitionNumber,
					this.folderId.To26ByteArray(),
					associated,
					mid
				});
				keyRange = new KeyRange(startStopKey2, startStopKey2);
			}
			using (TableOperator tableOperator = Factory.CreateTableOperator(this.context.Culture, this.context, messageTable.Table, messageTable.MessageUnique, columnsToFetch, null, null, null, 0, 0, new KeyRange[]
			{
				keyRange
			}, false, opportunedPreread, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						errorCode = accessor(reader);
						if (errorCode != ErrorCode.NoError)
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceError<ErrorCode>(0L, "MessagePropValueGetter.Execute failed with error {0}", errorCode);
							}
							return errorCode.Propagate((LID)59880U);
						}
						if (!shouldContinue())
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
							}
							return ErrorCode.CreateExiting((LID)43496U);
						}
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x040000A1 RID: 161
		private readonly Context context;

		// Token: 0x040000A2 RID: 162
		private readonly int mailboxPartitionNumber;

		// Token: 0x040000A3 RID: 163
		private readonly ExchangeId folderId;
	}
}
