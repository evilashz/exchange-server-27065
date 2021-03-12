using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x0200001C RID: 28
	internal class ProtocolContext
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004DC4 File Offset: 0x00002FC4
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004DCC File Offset: 0x00002FCC
		internal ProtocolContext.MessageDirection Direction { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004DD5 File Offset: 0x00002FD5
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00004DDD File Offset: 0x00002FDD
		internal DateTime? QueueStartTime { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004DE6 File Offset: 0x00002FE6
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00004DEE File Offset: 0x00002FEE
		internal DateTime? QueueEndTime { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004DF7 File Offset: 0x00002FF7
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00004DFF File Offset: 0x00002FFF
		internal DateTime? ProcessStartTime { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004E08 File Offset: 0x00003008
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004E10 File Offset: 0x00003010
		internal DateTime? ProcessEndTime { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004E19 File Offset: 0x00003019
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00004E21 File Offset: 0x00003021
		internal DateTime? DispatchStartTime { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004E2A File Offset: 0x0000302A
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004E32 File Offset: 0x00003032
		internal DateTime? DispatchEndTime { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004E3B File Offset: 0x0000303B
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004E43 File Offset: 0x00003043
		internal object DispatchData { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004E4C File Offset: 0x0000304C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004E54 File Offset: 0x00003054
		internal FaultDefinition FaultDefinition { get; set; }

		// Token: 0x0200001D RID: 29
		public enum MessageDirection
		{
			// Token: 0x04000057 RID: 87
			Unknown,
			// Token: 0x04000058 RID: 88
			Incoming,
			// Token: 0x04000059 RID: 89
			Outgoing,
			// Token: 0x0400005A RID: 90
			Return
		}
	}
}
