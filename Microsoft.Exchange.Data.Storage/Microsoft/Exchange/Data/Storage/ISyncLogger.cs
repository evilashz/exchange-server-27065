using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E24 RID: 3620
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncLogger
	{
		// Token: 0x06007D26 RID: 32038
		void Information(Trace tracer, long id, string message);

		// Token: 0x06007D27 RID: 32039
		void Information(Trace tracer, long id, string formatString, params object[] args);

		// Token: 0x06007D28 RID: 32040
		void Information<T0>(Trace tracer, long id, string formatString, T0 arg0);

		// Token: 0x06007D29 RID: 32041
		void Information<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D2A RID: 32042
		void Information<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D2B RID: 32043
		void TraceDebug(Trace tracer, long id, string message);

		// Token: 0x06007D2C RID: 32044
		void TraceDebug(Trace tracer, int lid, long id, string message);

		// Token: 0x06007D2D RID: 32045
		void TraceDebug(Trace tracer, long id, string formatString, params object[] args);

		// Token: 0x06007D2E RID: 32046
		void TraceDebug<T0>(Trace tracer, long id, string formatString, T0 arg0);

		// Token: 0x06007D2F RID: 32047
		void TraceDebug(Trace tracer, int lid, long id, string formatString, params object[] args);

		// Token: 0x06007D30 RID: 32048
		void TraceDebug<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0);

		// Token: 0x06007D31 RID: 32049
		void TraceDebug<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D32 RID: 32050
		void TraceDebug<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D33 RID: 32051
		void TraceDebug<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D34 RID: 32052
		void TraceDebug<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D35 RID: 32053
		void TraceError(Trace tracer, long id, string message);

		// Token: 0x06007D36 RID: 32054
		void TraceError(Trace tracer, int lid, long id, string message);

		// Token: 0x06007D37 RID: 32055
		void TraceError(Trace tracer, long id, string formatString, params object[] args);

		// Token: 0x06007D38 RID: 32056
		void TraceError<T0>(Trace tracer, long id, string formatString, T0 arg0);

		// Token: 0x06007D39 RID: 32057
		void TraceError(Trace tracer, int lid, long id, string formatString, params object[] args);

		// Token: 0x06007D3A RID: 32058
		void TraceError<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0);

		// Token: 0x06007D3B RID: 32059
		void TraceError<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D3C RID: 32060
		void TraceError<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D3D RID: 32061
		void TraceError<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D3E RID: 32062
		void TraceError<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D3F RID: 32063
		void TraceFunction(Trace tracer, long id, string message);

		// Token: 0x06007D40 RID: 32064
		void TraceFunction(Trace tracer, int lid, long id, string message);

		// Token: 0x06007D41 RID: 32065
		void TraceFunction(Trace tracer, long id, string formatString, params object[] args);

		// Token: 0x06007D42 RID: 32066
		void TraceFunction<T0>(Trace tracer, long id, string formatString, T0 arg0);

		// Token: 0x06007D43 RID: 32067
		void TraceFunction(Trace tracer, int lid, long id, string formatString, params object[] args);

		// Token: 0x06007D44 RID: 32068
		void TraceFunction<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0);

		// Token: 0x06007D45 RID: 32069
		void TraceFunction<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D46 RID: 32070
		void TraceFunction<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D47 RID: 32071
		void TraceFunction<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D48 RID: 32072
		void TraceFunction<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D49 RID: 32073
		void TracePfd(Trace tracer, long id, string message);

		// Token: 0x06007D4A RID: 32074
		void TracePfd(Trace tracer, int lid, long id, string message);

		// Token: 0x06007D4B RID: 32075
		void TracePfd(Trace tracer, long id, string formatString, params object[] args);

		// Token: 0x06007D4C RID: 32076
		void TracePfd<T0>(Trace tracer, long id, string formatString, T0 arg0);

		// Token: 0x06007D4D RID: 32077
		void TracePfd(Trace tracer, int lid, long id, string formatString, params object[] args);

		// Token: 0x06007D4E RID: 32078
		void TracePfd<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0);

		// Token: 0x06007D4F RID: 32079
		void TracePfd<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D50 RID: 32080
		void TracePfd<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D51 RID: 32081
		void TracePfd<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D52 RID: 32082
		void TracePfd<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D53 RID: 32083
		void TraceWarning(Trace tracer, long id, string message);

		// Token: 0x06007D54 RID: 32084
		void TraceWarning(Trace tracer, int lid, long id, string message);

		// Token: 0x06007D55 RID: 32085
		void TraceWarning(Trace tracer, long id, string formatString, params object[] args);

		// Token: 0x06007D56 RID: 32086
		void TraceWarning<T0>(Trace tracer, long id, string formatString, T0 arg0);

		// Token: 0x06007D57 RID: 32087
		void TraceWarning(Trace tracer, int lid, long id, string formatString, params object[] args);

		// Token: 0x06007D58 RID: 32088
		void TraceWarning<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0);

		// Token: 0x06007D59 RID: 32089
		void TraceWarning<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D5A RID: 32090
		void TraceWarning<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06007D5B RID: 32091
		void TraceWarning<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06007D5C RID: 32092
		void TraceWarning<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2);
	}
}
