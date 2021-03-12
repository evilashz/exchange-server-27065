using System;

namespace System.Diagnostics
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	internal abstract class AssertFilter
	{
		// Token: 0x060031D3 RID: 12755
		public abstract AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle);
	}
}
