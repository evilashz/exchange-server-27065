using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000131 RID: 305
	internal static class InternalDebug
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0006B6B8 File Offset: 0x000698B8
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0006B6BF File Offset: 0x000698BF
		internal static bool UseSystemDiagnostics
		{
			get
			{
				return InternalDebug.useSystemDiagnostics;
			}
			set
			{
				InternalDebug.useSystemDiagnostics = value;
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0006B6C7 File Offset: 0x000698C7
		[Conditional("DEBUG")]
		public static void Trace(long traceType, string format, params object[] traceObjects)
		{
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0006B6C9 File Offset: 0x000698C9
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString)
		{
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0006B6CB File Offset: 0x000698CB
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x04000EA6 RID: 3750
		private static bool useSystemDiagnostics;

		// Token: 0x02000132 RID: 306
		internal class DebugAssertionViolationException : Exception
		{
			// Token: 0x06000BFB RID: 3067 RVA: 0x0006B6CF File Offset: 0x000698CF
			public DebugAssertionViolationException()
			{
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x0006B6D7 File Offset: 0x000698D7
			public DebugAssertionViolationException(string message) : base(message)
			{
			}
		}
	}
}
