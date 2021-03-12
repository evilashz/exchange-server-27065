using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A1 RID: 161
	public interface ITraceTestHook
	{
		// Token: 0x060003C9 RID: 969
		bool IsTraceEnabled(TraceType traceType);

		// Token: 0x060003CA RID: 970
		void TraceDebug(int lid, long id, string message);

		// Token: 0x060003CB RID: 971
		void TraceDebug(int lid, long id, string formatString, params object[] args);

		// Token: 0x060003CC RID: 972
		void TraceError(int lid, long id, string message);

		// Token: 0x060003CD RID: 973
		void TraceError(int lid, long id, string formatString, params object[] args);

		// Token: 0x060003CE RID: 974
		void TraceFunction(int lid, long id, string message);

		// Token: 0x060003CF RID: 975
		void TraceFunction(int lid, long id, string formatString, params object[] args);

		// Token: 0x060003D0 RID: 976
		void TraceInformation(int lid, long id, string message);

		// Token: 0x060003D1 RID: 977
		void TraceInformation(int lid, long id, string formatString, params object[] args);

		// Token: 0x060003D2 RID: 978
		void TracePerformance(int lid, long id, string message);

		// Token: 0x060003D3 RID: 979
		void TracePerformance(int lid, long id, string formatString, params object[] args);

		// Token: 0x060003D4 RID: 980
		void TracePfd(int lid, long id, string message);

		// Token: 0x060003D5 RID: 981
		void TracePfd(int lid, long id, string formatString, params object[] args);

		// Token: 0x060003D6 RID: 982
		void TraceWarning(int lid, long id, string message);

		// Token: 0x060003D7 RID: 983
		void TraceWarning(int lid, long id, string formatString, params object[] args);
	}
}
