using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200000E RID: 14
	public class Trace
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000022D1 File Offset: 0x000004D1
		internal Trace(Trace traceImpl)
		{
			ArgumentValidator.ThrowIfNull("traceImpl", traceImpl);
			this.traceImpl = traceImpl;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022EB File Offset: 0x000004EB
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			this.traceImpl.Dump(writer, addHeader, verbose);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022FB File Offset: 0x000004FB
		public bool IsTraceEnabled(TraceType traceType)
		{
			return this.traceImpl.IsTraceEnabled((TraceType)traceType);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002309 File Offset: 0x00000509
		public void Information(long id, string message)
		{
			this.traceImpl.Information(id, message);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002318 File Offset: 0x00000518
		public void Information(long id, string formatString, params object[] args)
		{
			this.traceImpl.Information(id, formatString, args);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002328 File Offset: 0x00000528
		public void Information<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.Information<T0>(id, formatString, arg0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002338 File Offset: 0x00000538
		public void Information<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.Information<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000234A File Offset: 0x0000054A
		public void Information<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.Information<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000235E File Offset: 0x0000055E
		public void TraceDebug(long id, string message)
		{
			this.traceImpl.TraceDebug(id, message);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000236D File Offset: 0x0000056D
		public void TraceDebug(int lid, long id, string message)
		{
			this.traceImpl.TraceDebug(lid, id, message);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000237D File Offset: 0x0000057D
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceDebug(id, formatString, args);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000238D File Offset: 0x0000058D
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceDebug<T0>(id, formatString, arg0);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000239D File Offset: 0x0000059D
		public void TraceDebug(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceDebug(lid, id, formatString, args);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023AF File Offset: 0x000005AF
		public void TraceDebug<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceDebug<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023C1 File Offset: 0x000005C1
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceDebug<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023D3 File Offset: 0x000005D3
		public void TraceDebug<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceDebug<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023E7 File Offset: 0x000005E7
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceDebug<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023FB File Offset: 0x000005FB
		public void TraceDebug<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceDebug<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002411 File Offset: 0x00000611
		public void TraceError(long id, string message)
		{
			this.traceImpl.TraceError(id, message);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002420 File Offset: 0x00000620
		public void TraceError(int lid, long id, string message)
		{
			this.traceImpl.TraceError(lid, id, message);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002430 File Offset: 0x00000630
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceError(id, formatString, args);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002440 File Offset: 0x00000640
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceError<T0>(id, formatString, arg0);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002450 File Offset: 0x00000650
		public void TraceError(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceError(lid, id, formatString, args);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002462 File Offset: 0x00000662
		public void TraceError<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceError<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002474 File Offset: 0x00000674
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceError<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002486 File Offset: 0x00000686
		public void TraceError<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceError<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000249A File Offset: 0x0000069A
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceError<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000024AE File Offset: 0x000006AE
		public void TraceError<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceError<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000024C4 File Offset: 0x000006C4
		public void TraceFunction(long id, string message)
		{
			this.traceImpl.TraceFunction(id, message);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000024D3 File Offset: 0x000006D3
		public void TraceFunction(int lid, long id, string message)
		{
			this.traceImpl.TraceFunction(lid, id, message);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024E3 File Offset: 0x000006E3
		public void TraceFunction(long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceFunction(id, formatString, args);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024F3 File Offset: 0x000006F3
		public void TraceFunction<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceFunction<T0>(id, formatString, arg0);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002503 File Offset: 0x00000703
		public void TraceFunction(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceFunction(lid, id, formatString, args);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002515 File Offset: 0x00000715
		public void TraceFunction<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceFunction<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002527 File Offset: 0x00000727
		public void TraceFunction<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceFunction<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002539 File Offset: 0x00000739
		public void TraceFunction<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceFunction<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000254D File Offset: 0x0000074D
		public void TraceFunction<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceFunction<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002561 File Offset: 0x00000761
		public void TraceFunction<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceFunction<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002577 File Offset: 0x00000777
		public void TraceInformation(int lid, long id, string message)
		{
			this.traceImpl.TraceInformation(lid, id, message);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002587 File Offset: 0x00000787
		public void TraceInformation(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceInformation(lid, id, formatString, args);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002599 File Offset: 0x00000799
		public void TraceInformation<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceInformation<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000025AB File Offset: 0x000007AB
		public void TraceInformation<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceInformation<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000025BF File Offset: 0x000007BF
		public void TraceInformation<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceInformation<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000025D5 File Offset: 0x000007D5
		public void TracePerformance(long id, string message)
		{
			this.traceImpl.TracePerformance(id, message);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000025E4 File Offset: 0x000007E4
		public void TracePerformance(int lid, long id, string message)
		{
			this.traceImpl.TracePerformance(lid, id, message);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000025F4 File Offset: 0x000007F4
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.traceImpl.TracePerformance(id, formatString, args);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002604 File Offset: 0x00000804
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.TracePerformance<T0>(id, formatString, arg0);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002614 File Offset: 0x00000814
		public void TracePerformance(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TracePerformance(lid, id, formatString, args);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002626 File Offset: 0x00000826
		public void TracePerformance<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TracePerformance<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002638 File Offset: 0x00000838
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TracePerformance<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000264A File Offset: 0x0000084A
		public void TracePerformance<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TracePerformance<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000265E File Offset: 0x0000085E
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TracePerformance<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002672 File Offset: 0x00000872
		public void TracePerformance<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TracePerformance<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002688 File Offset: 0x00000888
		public void TracePfd(long id, string message)
		{
			this.traceImpl.TracePfd(id, message);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002697 File Offset: 0x00000897
		public void TracePfd(int lid, long id, string message)
		{
			this.traceImpl.TracePfd(lid, id, message);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000026A7 File Offset: 0x000008A7
		public void TracePfd(long id, string formatString, params object[] args)
		{
			this.traceImpl.TracePfd(id, formatString, args);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026B7 File Offset: 0x000008B7
		public void TracePfd<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.TracePfd<T0>(id, formatString, arg0);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026C7 File Offset: 0x000008C7
		public void TracePfd(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TracePfd(lid, id, formatString, args);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000026D9 File Offset: 0x000008D9
		public void TracePfd<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TracePfd<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000026EB File Offset: 0x000008EB
		public void TracePfd<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TracePfd<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000026FD File Offset: 0x000008FD
		public void TracePfd<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TracePfd<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002711 File Offset: 0x00000911
		public void TracePfd<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TracePfd<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002725 File Offset: 0x00000925
		public void TracePfd<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TracePfd<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000273B File Offset: 0x0000093B
		public void TraceWarning(long id, string message)
		{
			this.traceImpl.TraceWarning(id, message);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000274A File Offset: 0x0000094A
		public void TraceWarning(int lid, long id, string message)
		{
			this.traceImpl.TraceWarning(lid, id, message);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000275A File Offset: 0x0000095A
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceWarning(id, formatString, args);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000276A File Offset: 0x0000096A
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceWarning<T0>(id, formatString, arg0);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000277A File Offset: 0x0000097A
		public void TraceWarning(int lid, long id, string formatString, params object[] args)
		{
			this.traceImpl.TraceWarning(lid, id, formatString, args);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000278C File Offset: 0x0000098C
		public void TraceWarning<T0>(int lid, long id, string formatString, T0 arg0)
		{
			this.traceImpl.TraceWarning<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000279E File Offset: 0x0000099E
		public void TraceWarning<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceWarning<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000027B0 File Offset: 0x000009B0
		public void TraceWarning<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			this.traceImpl.TraceWarning<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000027C4 File Offset: 0x000009C4
		public void TraceWarning<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceWarning<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000027D8 File Offset: 0x000009D8
		public void TraceWarning<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.traceImpl.TraceWarning<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0400002A RID: 42
		private Trace traceImpl;
	}
}
