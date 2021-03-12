using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E38 RID: 3640
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TracingLogger : ISyncLogger
	{
		// Token: 0x06007E21 RID: 32289 RVA: 0x0022BCA9 File Offset: 0x00229EA9
		public void Information(Trace tracer, long id, string message)
		{
			tracer.Information(id, message);
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x0022BCB3 File Offset: 0x00229EB3
		public void Information(Trace tracer, long id, string formatString, params object[] args)
		{
			tracer.Information(id, formatString, args);
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x0022BCBF File Offset: 0x00229EBF
		public void Information<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			tracer.Information<T0>(id, formatString, arg0);
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x0022BCCB File Offset: 0x00229ECB
		public void Information<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.Information<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x0022BCD9 File Offset: 0x00229ED9
		public void Information<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.Information<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x0022BCE9 File Offset: 0x00229EE9
		public void TraceDebug(Trace tracer, long id, string message)
		{
			tracer.TraceDebug(id, message);
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x0022BCF3 File Offset: 0x00229EF3
		public void TraceDebug(Trace tracer, int lid, long id, string message)
		{
			tracer.TraceDebug(lid, id, message);
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x0022BCFF File Offset: 0x00229EFF
		public void TraceDebug(Trace tracer, long id, string formatString, params object[] args)
		{
			tracer.TraceDebug(id, formatString, args);
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x0022BD0B File Offset: 0x00229F0B
		public void TraceDebug<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			tracer.TraceDebug<T0>(id, formatString, arg0);
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x0022BD17 File Offset: 0x00229F17
		public void TraceDebug(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			tracer.TraceDebug(lid, id, formatString, args);
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x0022BD25 File Offset: 0x00229F25
		public void TraceDebug<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			tracer.TraceDebug<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x0022BD33 File Offset: 0x00229F33
		public void TraceDebug<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceDebug<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x0022BD41 File Offset: 0x00229F41
		public void TraceDebug<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceDebug<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x0022BD51 File Offset: 0x00229F51
		public void TraceDebug<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceDebug<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x0022BD61 File Offset: 0x00229F61
		public void TraceDebug<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceDebug<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x0022BD73 File Offset: 0x00229F73
		public void TraceError(Trace tracer, long id, string message)
		{
			tracer.TraceError(id, message);
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x0022BD7D File Offset: 0x00229F7D
		public void TraceError(Trace tracer, int lid, long id, string message)
		{
			tracer.TraceError(lid, id, message);
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x0022BD89 File Offset: 0x00229F89
		public void TraceError(Trace tracer, long id, string formatString, params object[] args)
		{
			tracer.TraceError(id, formatString, args);
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x0022BD95 File Offset: 0x00229F95
		public void TraceError<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			tracer.TraceError<T0>(id, formatString, arg0);
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x0022BDA1 File Offset: 0x00229FA1
		public void TraceError(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			tracer.TraceError(lid, id, formatString, args);
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x0022BDAF File Offset: 0x00229FAF
		public void TraceError<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			tracer.TraceError<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x0022BDBD File Offset: 0x00229FBD
		public void TraceError<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceError<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x0022BDCB File Offset: 0x00229FCB
		public void TraceError<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceError<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x0022BDDB File Offset: 0x00229FDB
		public void TraceError<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceError<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x0022BDEB File Offset: 0x00229FEB
		public void TraceError<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceError<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x0022BDFD File Offset: 0x00229FFD
		public void TraceFunction(Trace tracer, long id, string message)
		{
			tracer.TraceFunction(id, message);
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x0022BE07 File Offset: 0x0022A007
		public void TraceFunction(Trace tracer, int lid, long id, string message)
		{
			tracer.TraceFunction(lid, id, message);
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x0022BE13 File Offset: 0x0022A013
		public void TraceFunction(Trace tracer, long id, string formatString, params object[] args)
		{
			tracer.TraceFunction(id, formatString, args);
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x0022BE1F File Offset: 0x0022A01F
		public void TraceFunction<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			tracer.TraceFunction<T0>(id, formatString, arg0);
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x0022BE2B File Offset: 0x0022A02B
		public void TraceFunction(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			tracer.TraceFunction(lid, id, formatString, args);
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x0022BE39 File Offset: 0x0022A039
		public void TraceFunction<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			tracer.TraceFunction<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x0022BE47 File Offset: 0x0022A047
		public void TraceFunction<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceFunction<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x0022BE55 File Offset: 0x0022A055
		public void TraceFunction<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceFunction<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x0022BE65 File Offset: 0x0022A065
		public void TraceFunction<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceFunction<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x0022BE75 File Offset: 0x0022A075
		public void TraceFunction<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceFunction<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x0022BE87 File Offset: 0x0022A087
		public void TracePfd(Trace tracer, long id, string message)
		{
			tracer.TracePfd(id, message);
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x0022BE91 File Offset: 0x0022A091
		public void TracePfd(Trace tracer, int lid, long id, string message)
		{
			tracer.TracePfd(lid, id, message);
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x0022BE9D File Offset: 0x0022A09D
		public void TracePfd(Trace tracer, long id, string formatString, params object[] args)
		{
			tracer.TracePfd(id, formatString, args);
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x0022BEA9 File Offset: 0x0022A0A9
		public void TracePfd<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			tracer.TracePfd<T0>(id, formatString, arg0);
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x0022BEB5 File Offset: 0x0022A0B5
		public void TracePfd(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			tracer.TracePfd(lid, id, formatString, args);
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x0022BEC3 File Offset: 0x0022A0C3
		public void TracePfd<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			tracer.TracePfd<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x0022BED1 File Offset: 0x0022A0D1
		public void TracePfd<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TracePfd<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x0022BEDF File Offset: 0x0022A0DF
		public void TracePfd<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TracePfd<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x0022BEEF File Offset: 0x0022A0EF
		public void TracePfd<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TracePfd<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x0022BEFF File Offset: 0x0022A0FF
		public void TracePfd<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TracePfd<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x0022BF11 File Offset: 0x0022A111
		public void TraceWarning(Trace tracer, long id, string message)
		{
			tracer.TraceWarning(id, message);
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x0022BF1B File Offset: 0x0022A11B
		public void TraceWarning(Trace tracer, int lid, long id, string message)
		{
			tracer.TraceWarning(lid, id, message);
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x0022BF27 File Offset: 0x0022A127
		public void TraceWarning(Trace tracer, long id, string formatString, params object[] args)
		{
			tracer.TraceWarning(id, formatString, args);
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x0022BF33 File Offset: 0x0022A133
		public void TraceWarning<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			tracer.TraceWarning<T0>(id, formatString, arg0);
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x0022BF3F File Offset: 0x0022A13F
		public void TraceWarning(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			tracer.TraceWarning(lid, id, formatString, args);
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x0022BF4D File Offset: 0x0022A14D
		public void TraceWarning<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			tracer.TraceWarning<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x0022BF5B File Offset: 0x0022A15B
		public void TraceWarning<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceWarning<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x0022BF69 File Offset: 0x0022A169
		public void TraceWarning<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			tracer.TraceWarning<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x0022BF79 File Offset: 0x0022A179
		public void TraceWarning<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceWarning<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x0022BF89 File Offset: 0x0022A189
		public void TraceWarning<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			tracer.TraceWarning<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x040055E8 RID: 21992
		public static readonly TracingLogger Singleton = new TracingLogger();
	}
}
