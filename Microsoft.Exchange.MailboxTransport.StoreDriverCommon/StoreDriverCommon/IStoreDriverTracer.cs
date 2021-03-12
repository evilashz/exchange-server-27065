using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200001C RID: 28
	internal interface IStoreDriverTracer
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175
		bool IsMessageAMapiSubmitLAMProbe { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B0 RID: 176
		bool IsMessageAMapiSubmitSystemProbe { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B1 RID: 177
		ISystemProbeTrace GeneralTracer { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B2 RID: 178
		ISystemProbeTrace MapiStoreDriverSubmissionTracer { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B3 RID: 179
		// (set) Token: 0x060000B4 RID: 180
		Guid MessageProbeActivityId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		TraceHelper.LamNotificationIdParts? MessageProbeLamNotificationIdParts { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B7 RID: 183
		ISystemProbeTrace ServiceTracer { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B8 RID: 184
		ISystemProbeTrace StoreDriverCommonTracer { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B9 RID: 185
		ISystemProbeTrace StoreDriverSubmissionTracer { get; }

		// Token: 0x060000BA RID: 186
		TraceHelper.LamNotificationIdParts Parse(string lamNotificationId);

		// Token: 0x060000BB RID: 187
		void TraceFail(Trace tracer, long etlTraceId, string formatString, params object[] args);

		// Token: 0x060000BC RID: 188
		void TraceFail(Trace tracer, long etlTraceId, string message);

		// Token: 0x060000BD RID: 189
		void TracePass(Trace tracer, long etlTraceId, string formatString, params object[] args);
	}
}
