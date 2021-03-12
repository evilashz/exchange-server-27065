using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000016 RID: 22
	internal class TaskExMonLogger : ExMonLogger
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00004518 File Offset: 0x00002718
		internal TaskExMonLogger(bool enableTestMode) : base(enableTestMode, string.Empty, string.Empty, new ExMonLogger.CreateExmonRpcInstanceId(ETWTrace.CreateExmonTaskInstanceId), new ExMonLogger.ExmonRpcTraceEventInstance(ETWTrace.ExmonTaskTraceEventInstance))
		{
			this.end = new TaskExMonLogger.TaskEnd(this);
			this.start = new TaskExMonLogger.TaskStart(this);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004566 File Offset: 0x00002766
		internal TaskExMonLogger.TaskStart Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000456E File Offset: 0x0000276E
		internal TaskExMonLogger.TaskEnd End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004576 File Offset: 0x00002776
		public void BeginTaskProcessing(JET_THREADSTATS threadStats)
		{
			if (base.IsTracingEnabled)
			{
				base.GetNewInstanceId();
				this.start.Clear();
				this.start.ThreadStats = threadStats;
				base.Submit();
				this.end.Clear();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000045AF File Offset: 0x000027AF
		public void SetMdbGuid(Guid mdbGuid)
		{
			if (base.IsTracingEnabled)
			{
				this.end.MdbGuid = mdbGuid;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000045C5 File Offset: 0x000027C5
		public void SetMailboxGuid(Guid mailboxGuid)
		{
			if (base.IsTracingEnabled)
			{
				this.end.MailboxGuid = mailboxGuid;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000045DC File Offset: 0x000027DC
		public void EndTaskProcessing(uint taskId, JET_THREADSTATS threadStats)
		{
			if (base.IsTracingEnabled)
			{
				this.end.SetStrings(base.UserName, base.ClientAddress, base.ServiceName);
				this.end.TaskId = taskId;
				this.end.ThreadStats = threadStats;
				base.Submit();
			}
			base.ReleaseBuffer();
		}

		// Token: 0x04000079 RID: 121
		private const byte ExMonStart = 203;

		// Token: 0x0400007A RID: 122
		private const byte ExMonEnd = 204;

		// Token: 0x0400007B RID: 123
		private TaskExMonLogger.TaskEnd end;

		// Token: 0x0400007C RID: 124
		private TaskExMonLogger.TaskStart start;

		// Token: 0x02000017 RID: 23
		internal struct TaskEnd
		{
			// Token: 0x06000121 RID: 289 RVA: 0x00004633 File Offset: 0x00002833
			internal TaskEnd(TaskExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
				this.statsLogger = new ExMonLogger.JetThreadStats(exmonLogger, 92);
			}

			// Token: 0x1700003D RID: 61
			// (set) Token: 0x06000122 RID: 290 RVA: 0x0000464A File Offset: 0x0000284A
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.statsLogger.ThreadStats = value;
				}
			}

			// Token: 0x1700003E RID: 62
			// (set) Token: 0x06000123 RID: 291 RVA: 0x00004658 File Offset: 0x00002858
			public Guid MdbGuid
			{
				set
				{
					this.exmonLogger.WriteGuid(60, value);
				}
			}

			// Token: 0x1700003F RID: 63
			// (set) Token: 0x06000124 RID: 292 RVA: 0x00004668 File Offset: 0x00002868
			public Guid MailboxGuid
			{
				set
				{
					this.exmonLogger.WriteGuid(76, value);
				}
			}

			// Token: 0x17000040 RID: 64
			// (set) Token: 0x06000125 RID: 293 RVA: 0x00004678 File Offset: 0x00002878
			public uint TaskId
			{
				set
				{
					this.exmonLogger.WriteUInt32(56, value);
				}
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00004688 File Offset: 0x00002888
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(129);
				this.exmonLogger.SetClassType(204);
			}

			// Token: 0x06000127 RID: 295 RVA: 0x000046AA File Offset: 0x000028AA
			public void SetStrings(string user, string address, string application)
			{
				this.exmonLogger.WriteUserAddressApplication(124, user, address, application);
			}

			// Token: 0x0400007D RID: 125
			internal const int StructSize = 129;

			// Token: 0x0400007E RID: 126
			private TaskExMonLogger exmonLogger;

			// Token: 0x0400007F RID: 127
			private ExMonLogger.JetThreadStats statsLogger;

			// Token: 0x02000018 RID: 24
			private enum Offsets
			{
				// Token: 0x04000081 RID: 129
				TaskId = 56,
				// Token: 0x04000082 RID: 130
				MdbGuid = 60,
				// Token: 0x04000083 RID: 131
				MailboxGuid = 76,
				// Token: 0x04000084 RID: 132
				JetStats = 92,
				// Token: 0x04000085 RID: 133
				StringsBuffer = 124,
				// Token: 0x04000086 RID: 134
				MaxOffset = 129
			}
		}

		// Token: 0x02000019 RID: 25
		internal struct TaskStart
		{
			// Token: 0x06000128 RID: 296 RVA: 0x000046BC File Offset: 0x000028BC
			internal TaskStart(TaskExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
				this.statsLogger = new ExMonLogger.JetThreadStats(exmonLogger, 56);
			}

			// Token: 0x17000041 RID: 65
			// (set) Token: 0x06000129 RID: 297 RVA: 0x000046D3 File Offset: 0x000028D3
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.statsLogger.ThreadStats = value;
				}
			}

			// Token: 0x0600012A RID: 298 RVA: 0x000046E1 File Offset: 0x000028E1
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(88);
				this.exmonLogger.SetClassType(203);
			}

			// Token: 0x04000087 RID: 135
			internal const int StructSize = 88;

			// Token: 0x04000088 RID: 136
			private TaskExMonLogger exmonLogger;

			// Token: 0x04000089 RID: 137
			private ExMonLogger.JetThreadStats statsLogger;

			// Token: 0x0200001A RID: 26
			private enum Offsets
			{
				// Token: 0x0400008B RID: 139
				JetStats = 56,
				// Token: 0x0400008C RID: 140
				MaxOffset = 88
			}
		}
	}
}
