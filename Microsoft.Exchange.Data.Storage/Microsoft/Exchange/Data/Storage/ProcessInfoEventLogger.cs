using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000298 RID: 664
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ProcessInfoEventLogger
	{
		// Token: 0x06001B88 RID: 7048 RVA: 0x0007F63C File Offset: 0x0007D83C
		static ProcessInfoEventLogger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				ProcessInfoEventLogger.ProcessName = currentProcess.ProcessName;
				ProcessInfoEventLogger.ProcessId = currentProcess.Id;
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0007F684 File Offset: 0x0007D884
		public static void Log(ExEventLog.EventTuple tuple, string periodicKey, params object[] arguments)
		{
			object[] array = new object[arguments.Length + 3];
			array[0] = ProcessInfoEventLogger.ProcessName;
			array[1] = ProcessInfoEventLogger.ProcessId;
			array[2] = Environment.CurrentManagedThreadId;
			arguments.CopyTo(array, 3);
			StorageGlobals.EventLogger.LogEvent(tuple, periodicKey, array);
		}

		// Token: 0x04001320 RID: 4896
		private static readonly string ProcessName;

		// Token: 0x04001321 RID: 4897
		private static readonly int ProcessId;
	}
}
