using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000028 RID: 40
	public class MailboxPropValueGetter
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public MailboxPropValueGetter(Context context)
		{
			this.context = context;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006CCF File Offset: 0x00004ECF
		public ErrorCode Execute(Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			return this.Execute(Guid.Empty, columnsToFetch, accessor, shouldContinue);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006CE0 File Offset: 0x00004EE0
		public ErrorCode Execute(Guid mailboxGuid, Column[] columnsToFetch, Func<Reader, ErrorCode> accessor, Func<bool> shouldContinue)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(this.context.Database);
			KeyRange allRowsRange;
			if (!mailboxGuid.Equals(Guid.Empty))
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailboxGuid
				});
				allRowsRange = new KeyRange(startStopKey, startStopKey);
			}
			else
			{
				allRowsRange = KeyRange.AllRowsRange;
			}
			using (TableOperator tableOperator = Factory.CreateTableOperator(this.context.Culture, this.context, mailboxTable.Table, mailboxTable.MailboxGuidIndex, columnsToFetch, Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
			{
				Factory.CreateSearchCriteriaCompare(mailboxTable.DeletedOn, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(null, mailboxTable.DeletedOn)),
				Factory.CreateSearchCriteriaCompare(mailboxTable.MailboxGuid, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(null, mailboxTable.MailboxGuid))
			}), null, 0, 0, allRowsRange, false, true))
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
								ExTraceGlobals.OnlineIsintegTracer.TraceError<ErrorCode>(0L, "MailboxPropValueGetter.Execute failed with error {0}", errorCode);
							}
							return errorCode.Propagate((LID)35304U);
						}
						if (!shouldContinue())
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
							}
							return ErrorCode.CreateExiting((LID)39400U);
						}
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0400009E RID: 158
		private readonly Context context;
	}
}
