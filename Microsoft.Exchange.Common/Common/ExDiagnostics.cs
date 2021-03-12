using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000007 RID: 7
	public class ExDiagnostics
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000230F File Offset: 0x0000050F
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002316 File Offset: 0x00000516
		public static FailFastMode FailFastMode
		{
			get
			{
				return ExDiagnostics.failFastMode;
			}
			set
			{
				ExDiagnostics.failFastMode = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000231E File Offset: 0x0000051E
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002325 File Offset: 0x00000525
		public static ExDiagnostics.FailFastEvent OnFailFast
		{
			get
			{
				return ExDiagnostics.failFastEventHandlers;
			}
			set
			{
				ExDiagnostics.failFastEventHandlers = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002330 File Offset: 0x00000530
		public static void FailFast(string message, bool alwaysTerminate)
		{
			bool flag = ExDiagnostics.FailFastMode != FailFastMode.Exception && ExDiagnostics.FailFastMode != FailFastMode.Test;
			flag = (flag || alwaysTerminate);
			try
			{
				ExDiagnostics.failFastEventHandlers(flag);
			}
			catch
			{
			}
			if (ExDiagnostics.FailFastMode == FailFastMode.Test && !flag)
			{
				throw new TestFailFastException(message);
			}
			FailFastException ex = new FailFastException(message, new StackTrace(1, true).ToString());
			ExWatson.SendReport(ex);
			if (flag)
			{
				Environment.Exit(1);
				return;
			}
			throw ex;
		}

		// Token: 0x0400000B RID: 11
		private static ExDiagnostics.FailFastEvent failFastEventHandlers;

		// Token: 0x0400000C RID: 12
		private static FailFastMode failFastMode = FailFastMode.Terminate;

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x06000020 RID: 32
		public delegate void FailFastEvent(bool terminating);
	}
}
