using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000011 RID: 17
	internal class AdminExMonLogger : ExMonLogger
	{
		// Token: 0x06000108 RID: 264 RVA: 0x0000431D File Offset: 0x0000251D
		internal AdminExMonLogger(bool enableTestMode, string clientAddress) : base(enableTestMode, clientAddress, string.Empty, new ExMonLogger.CreateExmonRpcInstanceId(ETWTrace.CreateExmonAdminRpcInstanceId), new ExMonLogger.ExmonRpcTraceEventInstance(ETWTrace.ExmonAdminRpcTraceEventInstance))
		{
			this.end = new AdminExMonLogger.AdminRpcEnd(this);
			this.start = new AdminExMonLogger.AdminRpcStart(this);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000435C File Offset: 0x0000255C
		internal AdminExMonLogger.AdminRpcStart RpcStart
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004364 File Offset: 0x00002564
		internal AdminExMonLogger.AdminRpcEnd RpcEnd
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000436C File Offset: 0x0000256C
		public void BeginRpcProcessing(JET_THREADSTATS threadStats)
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

		// Token: 0x0600010C RID: 268 RVA: 0x000043A5 File Offset: 0x000025A5
		public void SetMdbGuid(Guid mdbGuid)
		{
			if (base.IsTracingEnabled)
			{
				this.end.MdbGuid = mdbGuid;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000043BB File Offset: 0x000025BB
		public void SetMailboxGuid(Guid mailboxGuid)
		{
			if (base.IsTracingEnabled)
			{
				this.end.MailboxGuid = mailboxGuid;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000043D4 File Offset: 0x000025D4
		public void EndRpcProcessing(uint methodId, JET_THREADSTATS threadStats, uint rpcResult)
		{
			if (base.IsTracingEnabled)
			{
				this.end.SetStrings(base.UserName, base.ClientAddress, base.ServiceName);
				this.end.MethodId = methodId;
				this.end.ThreadStats = threadStats;
				this.end.RpcResult = rpcResult;
				base.Submit();
			}
			base.ReleaseBuffer();
		}

		// Token: 0x04000064 RID: 100
		private const byte ExMonStart = 201;

		// Token: 0x04000065 RID: 101
		private const byte ExMonEnd = 202;

		// Token: 0x04000066 RID: 102
		private AdminExMonLogger.AdminRpcEnd end;

		// Token: 0x04000067 RID: 103
		private AdminExMonLogger.AdminRpcStart start;

		// Token: 0x02000012 RID: 18
		internal struct AdminRpcEnd
		{
			// Token: 0x0600010F RID: 271 RVA: 0x00004437 File Offset: 0x00002637
			internal AdminRpcEnd(AdminExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
				this.statsLogger = new ExMonLogger.JetThreadStats(exmonLogger, 96);
			}

			// Token: 0x17000035 RID: 53
			// (set) Token: 0x06000110 RID: 272 RVA: 0x0000444E File Offset: 0x0000264E
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.statsLogger.ThreadStats = value;
				}
			}

			// Token: 0x17000036 RID: 54
			// (set) Token: 0x06000111 RID: 273 RVA: 0x0000445C File Offset: 0x0000265C
			public Guid MdbGuid
			{
				set
				{
					this.exmonLogger.WriteGuid(60, value);
				}
			}

			// Token: 0x17000037 RID: 55
			// (set) Token: 0x06000112 RID: 274 RVA: 0x0000446C File Offset: 0x0000266C
			public Guid MailboxGuid
			{
				set
				{
					this.exmonLogger.WriteGuid(76, value);
				}
			}

			// Token: 0x17000038 RID: 56
			// (set) Token: 0x06000113 RID: 275 RVA: 0x0000447C File Offset: 0x0000267C
			public uint RpcResult
			{
				set
				{
					this.exmonLogger.WriteUInt32(92, value);
				}
			}

			// Token: 0x17000039 RID: 57
			// (set) Token: 0x06000114 RID: 276 RVA: 0x0000448C File Offset: 0x0000268C
			public uint MethodId
			{
				set
				{
					this.exmonLogger.WriteUInt32(56, value);
				}
			}

			// Token: 0x06000115 RID: 277 RVA: 0x0000449C File Offset: 0x0000269C
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(133);
				this.exmonLogger.SetClassType(202);
			}

			// Token: 0x06000116 RID: 278 RVA: 0x000044BE File Offset: 0x000026BE
			public void SetStrings(string user, string address, string application)
			{
				this.exmonLogger.WriteUserAddressApplication(128, user, address, application);
			}

			// Token: 0x04000068 RID: 104
			internal const int StructSize = 133;

			// Token: 0x04000069 RID: 105
			private AdminExMonLogger exmonLogger;

			// Token: 0x0400006A RID: 106
			private ExMonLogger.JetThreadStats statsLogger;

			// Token: 0x02000013 RID: 19
			private enum Offsets
			{
				// Token: 0x0400006C RID: 108
				MethodId = 56,
				// Token: 0x0400006D RID: 109
				MdbGuid = 60,
				// Token: 0x0400006E RID: 110
				MailboxGuid = 76,
				// Token: 0x0400006F RID: 111
				Ec = 92,
				// Token: 0x04000070 RID: 112
				JetStats = 96,
				// Token: 0x04000071 RID: 113
				StringsBuffer = 128,
				// Token: 0x04000072 RID: 114
				MaxOffset = 133
			}
		}

		// Token: 0x02000014 RID: 20
		internal struct AdminRpcStart
		{
			// Token: 0x06000117 RID: 279 RVA: 0x000044D3 File Offset: 0x000026D3
			internal AdminRpcStart(AdminExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
				this.statsLogger = new ExMonLogger.JetThreadStats(exmonLogger, 56);
			}

			// Token: 0x1700003A RID: 58
			// (set) Token: 0x06000118 RID: 280 RVA: 0x000044EA File Offset: 0x000026EA
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.statsLogger.ThreadStats = value;
				}
			}

			// Token: 0x06000119 RID: 281 RVA: 0x000044F8 File Offset: 0x000026F8
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(88);
				this.exmonLogger.SetClassType(201);
			}

			// Token: 0x04000073 RID: 115
			internal const int StructSize = 88;

			// Token: 0x04000074 RID: 116
			private AdminExMonLogger exmonLogger;

			// Token: 0x04000075 RID: 117
			private ExMonLogger.JetThreadStats statsLogger;

			// Token: 0x02000015 RID: 21
			private enum Offsets
			{
				// Token: 0x04000077 RID: 119
				JetStats = 56,
				// Token: 0x04000078 RID: 120
				MaxOffset = 88
			}
		}
	}
}
