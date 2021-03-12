using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001BC RID: 444
	internal class CommonFailureLog : FailureObjectLog
	{
		// Token: 0x060010CB RID: 4299 RVA: 0x0002730F File Offset: 0x0002550F
		private CommonFailureLog() : base(new SimpleObjectLogConfiguration("CommonFailure", "CommonFailureLogEnabled", "CommonFailureLogMaxDirSize", "CommonFailureLogMaxFileSize"))
		{
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00027330 File Offset: 0x00025530
		public static void LogCommonFailureEvent(IFailureObjectLoggable failureEvent, Exception failureException)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
				CommonFailureLog.instance.LogFailureEvent(failureEvent, failureException);
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000273A8 File Offset: 0x000255A8
		public static void LogCommonFailureEvent(string objectType, Exception failureException, Guid objectGuid = default(Guid), int failureFlags = 0, string failureContext = null)
		{
			FailureEvent failureEvent = new FailureEvent(objectGuid, objectType, failureFlags, failureContext);
			CommonFailureLog.LogCommonFailureEvent(failureEvent, failureException);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000273C7 File Offset: 0x000255C7
		public override string ComputeFailureHash(Exception failureException)
		{
			return CommonUtils.ComputeCallStackHash(failureException, 5);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000273D0 File Offset: 0x000255D0
		public override string ExtractExceptionString(Exception failureException)
		{
			return CommonUtils.FullFailureMessageWithCallStack(failureException, 5);
		}

		// Token: 0x04000972 RID: 2418
		private static CommonFailureLog instance = new CommonFailureLog();
	}
}
