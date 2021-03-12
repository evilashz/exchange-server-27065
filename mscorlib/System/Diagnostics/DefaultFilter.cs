using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003B6 RID: 950
	internal class DefaultFilter : AssertFilter
	{
		// Token: 0x060031D5 RID: 12757 RVA: 0x000C0013 File Offset: 0x000BE213
		internal DefaultFilter()
		{
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000C001B File Offset: 0x000BE21B
		[SecuritySafeCritical]
		public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
		{
			return (AssertFilters)Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
		}
	}
}
