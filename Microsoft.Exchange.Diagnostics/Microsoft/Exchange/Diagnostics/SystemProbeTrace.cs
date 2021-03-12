using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001E3 RID: 483
	internal class SystemProbeTrace : ISystemProbeTrace
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x00039112 File Offset: 0x00037312
		public SystemProbeTrace(Trace etlTracer, string componentName)
		{
			if (etlTracer == null)
			{
				throw new ArgumentNullException("etlTracer");
			}
			if (string.IsNullOrWhiteSpace(componentName))
			{
				throw new ArgumentNullException("componentName");
			}
			this.etlTracer = etlTracer;
			this.componentName = componentName;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00039149 File Offset: 0x00037349
		public void TracePass(Guid activityId, long etlTraceId, string message)
		{
			SystemProbe.TracePass(activityId, this.componentName, message);
			this.etlTracer.TraceDebug(etlTraceId, message);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00039165 File Offset: 0x00037365
		public void TracePass<T0>(Guid activityId, long etlTraceId, string formatString, T0 arg0)
		{
			SystemProbe.TracePass<T0>(activityId, this.componentName, formatString, arg0);
			this.etlTracer.TraceDebug<T0>(etlTraceId, formatString, arg0);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00039185 File Offset: 0x00037385
		public void TracePass<T0, T1>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			SystemProbe.TracePass<T0, T1>(activityId, this.componentName, formatString, arg0, arg1);
			this.etlTracer.TraceDebug<T0, T1>(etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000391A9 File Offset: 0x000373A9
		public void TracePass<T0, T1, T2>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			SystemProbe.TracePass<T0, T1, T2>(activityId, this.componentName, formatString, arg0, arg1, arg2);
			this.etlTracer.TraceDebug<T0, T1, T2>(etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000391D1 File Offset: 0x000373D1
		public void TracePass(Guid activityId, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbe.TracePass(activityId, this.componentName, formatString, args);
			this.etlTracer.TraceDebug(etlTraceId, formatString, args);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000391F1 File Offset: 0x000373F1
		public void TracePfdPass(Guid activityId, long etlTraceId, string message)
		{
			SystemProbe.TracePass(activityId, this.componentName, message);
			this.etlTracer.TracePfd(etlTraceId, message);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003920D File Offset: 0x0003740D
		public void TracePfdPass<T0>(Guid activityId, long etlTraceId, string formatString, T0 arg0)
		{
			SystemProbe.TracePass<T0>(activityId, this.componentName, formatString, arg0);
			this.etlTracer.TracePfd<T0>(etlTraceId, formatString, arg0);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003922D File Offset: 0x0003742D
		public void TracePfdPass<T0, T1>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			SystemProbe.TracePass<T0, T1>(activityId, this.componentName, formatString, arg0, arg1);
			this.etlTracer.TracePfd<T0, T1>(etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00039251 File Offset: 0x00037451
		public void TracePfdPass<T0, T1, T2>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			SystemProbe.TracePass<T0, T1, T2>(activityId, this.componentName, formatString, arg0, arg1, arg2);
			this.etlTracer.TracePfd<T0, T1, T2>(etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00039279 File Offset: 0x00037479
		public void TracePfdPass(Guid activityId, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbe.TracePass(activityId, this.componentName, formatString, args);
			this.etlTracer.TracePfd(etlTraceId, formatString, args);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00039299 File Offset: 0x00037499
		public void TraceFail(Guid activityId, long etlTraceId, string message)
		{
			SystemProbe.TraceFail(activityId, this.componentName, message);
			this.etlTracer.TraceError(etlTraceId, message);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000392B5 File Offset: 0x000374B5
		public void TraceFail<T0>(Guid activityId, long etlTraceId, string formatString, T0 arg0)
		{
			SystemProbe.TraceFail<T0>(activityId, this.componentName, formatString, arg0);
			this.etlTracer.TraceError<T0>(etlTraceId, formatString, arg0);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000392D5 File Offset: 0x000374D5
		public void TraceFail<T0, T1>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			SystemProbe.TraceFail<T0, T1>(activityId, this.componentName, formatString, arg0, arg1);
			this.etlTracer.TraceError<T0, T1>(etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000392F9 File Offset: 0x000374F9
		public void TraceFail<T0, T1, T2>(Guid activityId, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			SystemProbe.TraceFail<T0, T1, T2>(activityId, this.componentName, formatString, arg0, arg1, arg2);
			this.etlTracer.TraceError<T0, T1, T2>(etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00039321 File Offset: 0x00037521
		public void TraceFail(Guid activityId, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbe.TraceFail(activityId, this.componentName, formatString, args);
			this.etlTracer.TraceError(etlTraceId, formatString, args);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00039341 File Offset: 0x00037541
		public void TracePass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string message)
		{
			SystemProbe.TracePass(activityIdHolder, this.componentName, message);
			this.etlTracer.TraceDebug(etlTraceId, message);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003935D File Offset: 0x0003755D
		public void TracePass<T0>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0)
		{
			SystemProbe.TracePass<T0>(activityIdHolder, this.componentName, formatString, arg0);
			this.etlTracer.TraceDebug<T0>(etlTraceId, formatString, arg0);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003937D File Offset: 0x0003757D
		public void TracePass<T0, T1>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			SystemProbe.TracePass<T0, T1>(activityIdHolder, this.componentName, formatString, arg0, arg1);
			this.etlTracer.TraceDebug<T0, T1>(etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000393A1 File Offset: 0x000375A1
		public void TracePass<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			SystemProbe.TracePass<T0, T1, T2>(activityIdHolder, this.componentName, formatString, arg0, arg1, arg2);
			this.etlTracer.TraceDebug<T0, T1, T2>(etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000393C9 File Offset: 0x000375C9
		public void TracePass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbe.TracePass(activityIdHolder, this.componentName, formatString, args);
			this.etlTracer.TraceDebug(etlTraceId, formatString, args);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000393E9 File Offset: 0x000375E9
		public void TracePfdPass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string message)
		{
			SystemProbe.TracePass(activityIdHolder, this.componentName, message);
			this.etlTracer.TracePfd(etlTraceId, message);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00039405 File Offset: 0x00037605
		public void TracePfdPass<T0>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0)
		{
			SystemProbe.TracePass<T0>(activityIdHolder, this.componentName, formatString, arg0);
			this.etlTracer.TracePfd<T0>(etlTraceId, formatString, arg0);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00039425 File Offset: 0x00037625
		public void TracePfdPass<T0, T1>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			SystemProbe.TracePass<T0, T1>(activityIdHolder, this.componentName, formatString, arg0, arg1);
			this.etlTracer.TracePfd<T0, T1>(etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00039449 File Offset: 0x00037649
		public void TracePfdPass<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			SystemProbe.TracePass<T0, T1, T2>(activityIdHolder, this.componentName, formatString, arg0, arg1, arg2);
			this.etlTracer.TracePfd<T0, T1, T2>(etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00039471 File Offset: 0x00037671
		public void TracePfdPass(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbe.TracePass(activityIdHolder, this.componentName, formatString, args);
			this.etlTracer.TracePfd(etlTraceId, formatString, args);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00039491 File Offset: 0x00037691
		public void TraceFail(ISystemProbeTraceable activityIdHolder, long etlTraceId, string message)
		{
			SystemProbe.TraceFail(activityIdHolder, this.componentName, message);
			this.etlTracer.TraceError(etlTraceId, message);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000394AD File Offset: 0x000376AD
		public void TraceFail<T0>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0)
		{
			SystemProbe.TraceFail<T0>(activityIdHolder, this.componentName, formatString, arg0);
			this.etlTracer.TraceError<T0>(etlTraceId, formatString, arg0);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000394CD File Offset: 0x000376CD
		public void TraceFail<T0, T1>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1)
		{
			SystemProbe.TraceFail<T0, T1>(activityIdHolder, this.componentName, formatString, arg0, arg1);
			this.etlTracer.TraceError<T0, T1>(etlTraceId, formatString, arg0, arg1);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000394F1 File Offset: 0x000376F1
		public void TraceFail<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			SystemProbe.TraceFail<T0, T1, T2>(activityIdHolder, this.componentName, formatString, arg0, arg1, arg2);
			this.etlTracer.TraceError<T0, T1, T2>(etlTraceId, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00039519 File Offset: 0x00037719
		public void TraceFail(ISystemProbeTraceable activityIdHolder, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbe.TraceFail(activityIdHolder, this.componentName, formatString, args);
			this.etlTracer.TraceError(etlTraceId, formatString, args);
		}

		// Token: 0x040009FD RID: 2557
		private readonly Trace etlTracer;

		// Token: 0x040009FE RID: 2558
		private readonly string componentName;
	}
}
