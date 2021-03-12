using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003B4 RID: 948
	internal static class Assert
	{
		// Token: 0x060031CC RID: 12748 RVA: 0x000BFF59 File Offset: 0x000BE159
		internal static void Check(bool condition, string conditionString, string message)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, -2146232797);
			}
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000BFF6B File Offset: 0x000BE16B
		internal static void Check(bool condition, string conditionString, string message, int exitCode)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, exitCode);
			}
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000BFF79 File Offset: 0x000BE179
		internal static void Fail(string conditionString, string message)
		{
			Assert.Fail(conditionString, message, null, -2146232797);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000BFF88 File Offset: 0x000BE188
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode)
		{
			Assert.Fail(conditionString, message, windowTitle, exitCode, StackTrace.TraceFormat.Normal, 0);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000BFF95 File Offset: 0x000BE195
		internal static void Fail(string conditionString, string message, int exitCode, StackTrace.TraceFormat stackTraceFormat)
		{
			Assert.Fail(conditionString, message, null, exitCode, stackTraceFormat, 0);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000BFFA4 File Offset: 0x000BE1A4
		[SecuritySafeCritical]
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode, StackTrace.TraceFormat stackTraceFormat, int numStackFramesToSkip)
		{
			StackTrace location = new StackTrace(numStackFramesToSkip, true);
			AssertFilters assertFilters = Assert.Filter.AssertFailure(conditionString, message, location, stackTraceFormat, windowTitle);
			if (assertFilters == AssertFilters.FailDebug)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
					return;
				}
				if (!Debugger.Launch())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DebuggerLaunchFailed"));
				}
			}
			else if (assertFilters == AssertFilters.FailTerminate)
			{
				if (Debugger.IsAttached)
				{
					Environment._Exit(exitCode);
					return;
				}
				Environment.FailFast(message, (uint)exitCode);
			}
		}

		// Token: 0x060031D2 RID: 12754
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ShowDefaultAssertDialog(string conditionString, string message, string stackTrace, string windowTitle);

		// Token: 0x040015DD RID: 5597
		internal const int COR_E_FAILFAST = -2146232797;

		// Token: 0x040015DE RID: 5598
		private static AssertFilter Filter = new DefaultFilter();
	}
}
