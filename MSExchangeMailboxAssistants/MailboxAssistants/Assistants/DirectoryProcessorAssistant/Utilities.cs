using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001B4 RID: 436
	internal class Utilities
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x0006310B File Offset: 0x0006130B
		public static void DebugTrace(Trace tracer, string formatString, params object[] formatObjects)
		{
			tracer.TraceDebug(0L, formatString, formatObjects);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00063117 File Offset: 0x00061317
		public static void ErrorTrace(Trace tracer, string formatString, params object[] formatObjects)
		{
			tracer.TraceError(0L, formatString, formatObjects);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00063124 File Offset: 0x00061324
		public static Exception RunSafeADOperation(Trace tracer, ADOperation adOperation, string details)
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(adOperation, 5);
			if (!adoperationResult.Succeeded)
			{
				Utilities.ErrorTrace(tracer, "RunSafeADOperation failed with ErrorCode: {0}, Exception: {1}. Operation Details: {2}", new object[]
				{
					adoperationResult.ErrorCode,
					adoperationResult.Exception,
					string.IsNullOrEmpty(details) ? "null" : details
				});
				return adoperationResult.Exception;
			}
			return null;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00063186 File Offset: 0x00061386
		public static string GetFileName(string baseName, string extension)
		{
			return string.Format("{0}{1}", baseName, extension);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00063194 File Offset: 0x00061394
		public static string GetEntriesFilePath(string runFolderPath, string ADEntriesFileName, string ADEntriesFileNameExt)
		{
			string fileName = Utilities.GetFileName(ADEntriesFileName, ADEntriesFileNameExt);
			return Path.Combine(runFolderPath, fileName);
		}

		// Token: 0x04000AB0 RID: 2736
		public const int MaxTransientExceptionRetries = 5;

		// Token: 0x04000AB1 RID: 2737
		public static TestFlag TestFlag;

		// Token: 0x04000AB2 RID: 2738
		public static bool TestForceYieldChunk;
	}
}
