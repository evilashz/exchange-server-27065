using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200001D RID: 29
	internal class StoreDriverTracer : IStoreDriverTracer
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006B35 File Offset: 0x00004D35
		public bool IsMessageAMapiSubmitLAMProbe
		{
			get
			{
				return TraceHelper.IsMessageAMapiSubmitLAMProbe;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00006B3C File Offset: 0x00004D3C
		public bool IsMessageAMapiSubmitSystemProbe
		{
			get
			{
				return TraceHelper.IsMessageAMapiSubmitSystemProbe;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006B43 File Offset: 0x00004D43
		public ISystemProbeTrace GeneralTracer
		{
			get
			{
				return TraceHelper.GeneralTracer;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00006B4A File Offset: 0x00004D4A
		public ISystemProbeTrace MapiStoreDriverSubmissionTracer
		{
			get
			{
				return TraceHelper.MapiStoreDriverSubmissionTracer;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00006B51 File Offset: 0x00004D51
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00006B58 File Offset: 0x00004D58
		public Guid MessageProbeActivityId
		{
			get
			{
				return TraceHelper.MessageProbeActivityId;
			}
			set
			{
				TraceHelper.MessageProbeActivityId = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006B60 File Offset: 0x00004D60
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00006B67 File Offset: 0x00004D67
		public TraceHelper.LamNotificationIdParts? MessageProbeLamNotificationIdParts
		{
			get
			{
				return TraceHelper.MessageProbeLamNotificationIdParts;
			}
			set
			{
				TraceHelper.MessageProbeLamNotificationIdParts = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006B6F File Offset: 0x00004D6F
		public ISystemProbeTrace ServiceTracer
		{
			get
			{
				return TraceHelper.ServiceTracer;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00006B76 File Offset: 0x00004D76
		public ISystemProbeTrace StoreDriverSubmissionTracer
		{
			get
			{
				return TraceHelper.StoreDriverSubmissionTracer;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006B7D File Offset: 0x00004D7D
		public ISystemProbeTrace StoreDriverCommonTracer
		{
			get
			{
				return TraceHelper.StoreDriverTracer;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006B84 File Offset: 0x00004D84
		public TraceHelper.LamNotificationIdParts Parse(string lamNotificationId)
		{
			return TraceHelper.LamNotificationIdParts.Parse(lamNotificationId);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006B8C File Offset: 0x00004D8C
		public void TraceFail(Trace tracer, long etlTraceId, string formatString, params object[] args)
		{
			TraceHelper.TraceFail(tracer, etlTraceId, formatString, args);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006B98 File Offset: 0x00004D98
		public void TraceFail(Trace tracer, long etlTraceId, string message)
		{
			TraceHelper.TraceFail(tracer, etlTraceId, message);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006BA2 File Offset: 0x00004DA2
		public void TracePass(Trace tracer, long etlTraceId, string formatString, params object[] args)
		{
			TraceHelper.TracePass(tracer, etlTraceId, formatString, args);
		}
	}
}
