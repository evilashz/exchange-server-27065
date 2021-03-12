using System;
using System.Diagnostics;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200003C RID: 60
	public static class Globals
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000C84A File Offset: 0x0000AA4A
		public static ExEventLog EventLog
		{
			get
			{
				return Globals.eventLog;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000C851 File Offset: 0x0000AA51
		public static bool IsMultiProcess
		{
			get
			{
				return Globals.isMultiProcess;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000C858 File Offset: 0x0000AA58
		public static void Initialize(Guid? databaseGuid, Guid? dagOrServerGuid)
		{
			LockManager.Initialize();
			WatsonOnUnhandledException.Initialize();
			ErrorHelper.Initialize(databaseGuid);
			Globals.isMultiProcess = (databaseGuid != null);
			Globals.databaseGuid = databaseGuid;
			ConfigurationSchema.SetDatabaseContext(databaseGuid, dagOrServerGuid);
			ThreadManager.Initialize();
			TempStream.Configure(null);
			Statistics.Initialize();
			NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(null);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000C8AE File Offset: 0x0000AAAE
		public static void Terminate()
		{
			Statistics.Terminate();
			LoggerManager.Terminate();
			ThreadManager.Terminate();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000C8BF File Offset: 0x0000AABF
		[Conditional("DEBUG")]
		internal static void Assert(bool assertCondition, string message)
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000C8C1 File Offset: 0x0000AAC1
		[Conditional("INTERNAL")]
		internal static void AssertInternal(bool assertCondition, string message)
		{
			ErrorHelper.AssertRetail(assertCondition, message);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000C8CA File Offset: 0x0000AACA
		internal static void AssertRetail(bool assertCondition, string message)
		{
			ErrorHelper.AssertRetail(assertCondition, message);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		internal static void LogEvent(ExEventLog.EventTuple tuple, params object[] messageArgs)
		{
			if (!Globals.testOnlyEvents.Contains(tuple.EventId) || StoreEnvironment.IsTestEnvironment)
			{
				Globals.TracePIDAndDatabaseGuid(tuple.EventId);
				Globals.LogPeriodicEvent(null, tuple, messageArgs);
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000C904 File Offset: 0x0000AB04
		private static void TracePIDAndDatabaseGuid(uint eventId)
		{
			if (Globals.tracedForThisEventId != eventId)
			{
				Globals.tracedForThisEventId = eventId;
				if (Globals.databaseGuid != null)
				{
					DiagnosticContext.TraceGuid((LID)41344U, Globals.databaseGuid.GetValueOrDefault());
				}
				DiagnosticContext.TraceDword((LID)35200U, (uint)DiagnosticsNativeMethods.GetCurrentProcessId());
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000C958 File Offset: 0x0000AB58
		internal static void LogPeriodicEvent(string periodicKey, ExEventLog.EventTuple tuple, params object[] messageArgs)
		{
			byte[] array = DiagnosticContext.PackInfo();
			byte[] array2 = new byte["[DIAG_CTX]".Length + 2 + 4 + array.Length];
			int num = 0;
			num += ExBitConverter.Write("[DIAG_CTX]", "[DIAG_CTX]".Length, false, array2, num) - 1;
			num += ExBitConverter.Write(0, array2, num);
			num += ExBitConverter.Write(DiagnosticContext.Size + 14, array2, num);
			Buffer.BlockCopy(array, 0, array2, num, array.Length);
			Globals.eventLog.LogEventWithExtraData(tuple, periodicKey, array2, messageArgs);
		}

		// Token: 0x040004D3 RID: 1235
		public static readonly Guid EventLogGuid = new Guid("e1cda82c-0202-4901-8dfb-7b1993298b60");

		// Token: 0x040004D4 RID: 1236
		private static readonly ExEventLog eventLog = new ExEventLog(Globals.EventLogGuid, "MSExchangeIS");

		// Token: 0x040004D5 RID: 1237
		private static bool isMultiProcess;

		// Token: 0x040004D6 RID: 1238
		private static Guid? databaseGuid = null;

		// Token: 0x040004D7 RID: 1239
		private static uint tracedForThisEventId = 0U;

		// Token: 0x040004D8 RID: 1240
		private static HashSet<uint> testOnlyEvents = new HashSet<uint>
		{
			MSExchangeISEventLogConstants.Tuple_InvalidMailboxGlobcntAllocation.EventId,
			MSExchangeISEventLogConstants.Tuple_LeakedException.EventId,
			MSExchangeISEventLogConstants.Tuple_PropertyPromotion.EventId
		};
	}
}
