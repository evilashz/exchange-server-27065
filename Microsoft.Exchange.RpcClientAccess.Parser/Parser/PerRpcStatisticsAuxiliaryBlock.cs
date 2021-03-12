using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000032 RID: 50
	internal class PerRpcStatisticsAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000042A0 File Offset: 0x000024A0
		private PerRpcStatisticsAuxiliaryBlock(byte blockVersion, uint millisecondsInServer, uint millisecondsInCPU, uint pagesRead, uint pagesPreRead, uint logRecords, uint bytesLogRecord, ulong ldapReads, ulong ldapSearches, uint averageDbLatency, uint averageServerLatency, uint currentThreads, uint totalDbOperations, uint currentDbThreads, uint currentSCTThreads, uint currentSCTSessions, uint processId, uint dataProtectionHealth, uint dataAvailabilityHealth, uint currentCpuUsage) : base(blockVersion, AuxiliaryBlockTypes.PerRpcStatistics)
		{
			this.millisecondsInServer = millisecondsInServer;
			this.millisecondsInCPU = millisecondsInCPU;
			this.pagesRead = pagesRead;
			this.pagesPreRead = pagesPreRead;
			this.logRecords = logRecords;
			this.bytesLogRecord = bytesLogRecord;
			this.ldapReads = ldapReads;
			this.ldapSearches = ldapSearches;
			this.averageDbLatency = averageDbLatency;
			this.averageServerLatency = averageServerLatency;
			this.currentThreads = currentThreads;
			this.totalDbOperations = totalDbOperations;
			this.currentDbThreads = currentDbThreads;
			this.currentSCTThreads = currentSCTThreads;
			this.currentSCTSessions = currentSCTSessions;
			this.processId = processId;
			this.dataProtectionHealth = dataProtectionHealth;
			this.dataAvailabilityHealth = dataAvailabilityHealth;
			this.currentCpuUsage = currentCpuUsage;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000434C File Offset: 0x0000254C
		internal PerRpcStatisticsAuxiliaryBlock(uint millisecondsInServer, uint millisecondsInCPU, uint pagesRead, uint pagesPreRead, uint logRecords, uint bytesLogRecord, ulong ldapReads, ulong ldapSearches, uint averageDbLatency, uint averageServerLatency, uint currentThreads) : this(1, millisecondsInServer, millisecondsInCPU, pagesRead, pagesPreRead, logRecords, bytesLogRecord, ldapReads, ldapSearches, averageDbLatency, averageServerLatency, currentThreads, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000437C File Offset: 0x0000257C
		internal PerRpcStatisticsAuxiliaryBlock(uint millisecondsInServer, uint millisecondsInCPU, uint pagesRead, uint pagesPreRead, uint logRecords, uint bytesLogRecord, ulong ldapReads, ulong ldapSearches, uint averageDbLatency, uint averageServerLatency, uint currentThreads, uint totalDbOperations) : this(2, millisecondsInServer, millisecondsInCPU, pagesRead, pagesPreRead, logRecords, bytesLogRecord, ldapReads, ldapSearches, averageDbLatency, averageServerLatency, currentThreads, totalDbOperations, 0U, 0U, 0U, 0U, 0U, 0U, 0U)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000043AC File Offset: 0x000025AC
		internal PerRpcStatisticsAuxiliaryBlock(uint millisecondsInServer, uint millisecondsInCPU, uint pagesRead, uint pagesPreRead, uint logRecords, uint bytesLogRecord, ulong ldapReads, ulong ldapSearches, uint averageDbLatency, uint averageServerLatency, uint currentThreads, uint totalDbOperations, uint currentDbThreads, uint currentSCTThreads, uint currentSCTSessions, uint processId) : this(3, millisecondsInServer, millisecondsInCPU, pagesRead, pagesPreRead, logRecords, bytesLogRecord, ldapReads, ldapSearches, averageDbLatency, averageServerLatency, currentThreads, totalDbOperations, currentDbThreads, currentSCTThreads, currentSCTSessions, processId, 0U, 0U, 0U)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000043E0 File Offset: 0x000025E0
		internal PerRpcStatisticsAuxiliaryBlock(uint millisecondsInServer, uint millisecondsInCPU, uint pagesRead, uint pagesPreRead, uint logRecords, uint bytesLogRecord, ulong ldapReads, ulong ldapSearches, uint averageDbLatency, uint averageServerLatency, uint currentThreads, uint totalDbOperations, uint currentDbThreads, uint currentSCTThreads, uint currentSCTSessions, uint processId, uint dataProtectionHealth, uint dataAvailabilityHealth) : this(4, millisecondsInServer, millisecondsInCPU, pagesRead, pagesPreRead, logRecords, bytesLogRecord, ldapReads, ldapSearches, averageDbLatency, averageServerLatency, currentThreads, totalDbOperations, currentDbThreads, currentSCTThreads, currentSCTSessions, processId, dataProtectionHealth, dataAvailabilityHealth, 0U)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004418 File Offset: 0x00002618
		internal PerRpcStatisticsAuxiliaryBlock(uint millisecondsInServer, uint millisecondsInCPU, uint pagesRead, uint pagesPreRead, uint logRecords, uint bytesLogRecord, ulong ldapReads, ulong ldapSearches, uint averageDbLatency, uint averageServerLatency, uint currentThreads, uint totalDbOperations, uint currentDbThreads, uint currentSCTThreads, uint currentSCTSessions, uint processId, uint dataProtectionHealth, uint dataAvailabilityHealth, uint currentCpuUsage) : this(5, millisecondsInServer, millisecondsInCPU, pagesRead, pagesPreRead, logRecords, bytesLogRecord, ldapReads, ldapSearches, averageDbLatency, averageServerLatency, currentThreads, totalDbOperations, currentDbThreads, currentSCTThreads, currentSCTSessions, processId, dataProtectionHealth, dataAvailabilityHealth, currentCpuUsage)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004450 File Offset: 0x00002650
		internal PerRpcStatisticsAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.millisecondsInServer = reader.ReadUInt32();
			this.millisecondsInCPU = reader.ReadUInt32();
			this.pagesRead = reader.ReadUInt32();
			this.pagesPreRead = reader.ReadUInt32();
			this.logRecords = reader.ReadUInt32();
			this.bytesLogRecord = reader.ReadUInt32();
			this.ldapReads = reader.ReadUInt64();
			this.ldapSearches = reader.ReadUInt64();
			this.averageDbLatency = reader.ReadUInt32();
			this.averageServerLatency = reader.ReadUInt32();
			this.currentThreads = reader.ReadUInt32();
			if (base.Version >= 2)
			{
				this.totalDbOperations = reader.ReadUInt32();
			}
			if (base.Version >= 3)
			{
				this.currentDbThreads = reader.ReadUInt32();
				this.currentSCTThreads = reader.ReadUInt32();
				this.currentSCTSessions = reader.ReadUInt32();
				this.processId = reader.ReadUInt32();
			}
			if (base.Version >= 4)
			{
				this.dataProtectionHealth = reader.ReadUInt32();
				this.dataAvailabilityHealth = reader.ReadUInt32();
			}
			if (base.Version >= 5)
			{
				this.currentCpuUsage = reader.ReadUInt32();
				reader.ReadUInt32();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004573 File Offset: 0x00002773
		public uint MillisecondsInServer
		{
			get
			{
				return this.millisecondsInServer;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000457B File Offset: 0x0000277B
		public uint MillisecondsInCPU
		{
			get
			{
				return this.millisecondsInCPU;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004583 File Offset: 0x00002783
		public uint PagesRead
		{
			get
			{
				return this.pagesRead;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000458B File Offset: 0x0000278B
		public uint PagesPreRead
		{
			get
			{
				return this.pagesPreRead;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004593 File Offset: 0x00002793
		public uint LogRecords
		{
			get
			{
				return this.logRecords;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000459B File Offset: 0x0000279B
		public uint BytesLogRecord
		{
			get
			{
				return this.bytesLogRecord;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000045A3 File Offset: 0x000027A3
		public ulong LdapReads
		{
			get
			{
				return this.ldapReads;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000045AB File Offset: 0x000027AB
		public ulong LdapSearches
		{
			get
			{
				return this.ldapSearches;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000045B3 File Offset: 0x000027B3
		public uint AverageDbLatency
		{
			get
			{
				return this.averageDbLatency;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000045BB File Offset: 0x000027BB
		public uint AverageServerLatency
		{
			get
			{
				return this.averageServerLatency;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000045C3 File Offset: 0x000027C3
		public uint CurrentThreads
		{
			get
			{
				return this.currentThreads;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000045CB File Offset: 0x000027CB
		public uint TotalDbOperations
		{
			get
			{
				return this.totalDbOperations;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000045D3 File Offset: 0x000027D3
		public uint CurrentDbThreads
		{
			get
			{
				return this.currentDbThreads;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000045DB File Offset: 0x000027DB
		public uint CurrentSCTThreads
		{
			get
			{
				return this.currentSCTThreads;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000045E3 File Offset: 0x000027E3
		public uint CurrentSCTSessions
		{
			get
			{
				return this.currentSCTSessions;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000045EB File Offset: 0x000027EB
		public uint ProcessId
		{
			get
			{
				return this.processId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000045F3 File Offset: 0x000027F3
		public uint DataProtectionHealth
		{
			get
			{
				return this.dataProtectionHealth;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000045FB File Offset: 0x000027FB
		public uint DataAvailabilityHealth
		{
			get
			{
				return this.dataAvailabilityHealth;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004603 File Offset: 0x00002803
		public uint CurrentCpuUsage
		{
			get
			{
				return this.currentCpuUsage;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000460C File Offset: 0x0000280C
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.millisecondsInServer);
			writer.WriteUInt32(this.millisecondsInCPU);
			writer.WriteUInt32(this.pagesRead);
			writer.WriteUInt32(this.pagesPreRead);
			writer.WriteUInt32(this.logRecords);
			writer.WriteUInt32(this.bytesLogRecord);
			writer.WriteUInt64(this.ldapReads);
			writer.WriteUInt64(this.ldapSearches);
			writer.WriteUInt32(this.averageDbLatency);
			writer.WriteUInt32(this.averageServerLatency);
			writer.WriteUInt32(this.currentThreads);
			if (base.Version >= 2)
			{
				writer.WriteUInt32(this.totalDbOperations);
			}
			if (base.Version >= 3)
			{
				writer.WriteUInt32(this.currentDbThreads);
				writer.WriteUInt32(this.currentSCTThreads);
				writer.WriteUInt32(this.currentSCTSessions);
				writer.WriteUInt32(this.processId);
			}
			if (base.Version >= 4)
			{
				writer.WriteUInt32(this.dataProtectionHealth);
				writer.WriteUInt32(this.dataAvailabilityHealth);
			}
			if (base.Version >= 5)
			{
				writer.WriteUInt32(this.currentCpuUsage);
				writer.WriteUInt32(0U);
			}
		}

		// Token: 0x0400009B RID: 155
		private readonly uint millisecondsInServer;

		// Token: 0x0400009C RID: 156
		private readonly uint millisecondsInCPU;

		// Token: 0x0400009D RID: 157
		private readonly uint pagesRead;

		// Token: 0x0400009E RID: 158
		private readonly uint pagesPreRead;

		// Token: 0x0400009F RID: 159
		private readonly uint logRecords;

		// Token: 0x040000A0 RID: 160
		private readonly uint bytesLogRecord;

		// Token: 0x040000A1 RID: 161
		private readonly ulong ldapReads;

		// Token: 0x040000A2 RID: 162
		private readonly ulong ldapSearches;

		// Token: 0x040000A3 RID: 163
		private readonly uint averageDbLatency;

		// Token: 0x040000A4 RID: 164
		private readonly uint averageServerLatency;

		// Token: 0x040000A5 RID: 165
		private readonly uint currentThreads;

		// Token: 0x040000A6 RID: 166
		private readonly uint totalDbOperations;

		// Token: 0x040000A7 RID: 167
		private readonly uint currentDbThreads;

		// Token: 0x040000A8 RID: 168
		private readonly uint currentSCTThreads;

		// Token: 0x040000A9 RID: 169
		private readonly uint currentSCTSessions;

		// Token: 0x040000AA RID: 170
		private readonly uint processId;

		// Token: 0x040000AB RID: 171
		private readonly uint dataProtectionHealth;

		// Token: 0x040000AC RID: 172
		private readonly uint dataAvailabilityHealth;

		// Token: 0x040000AD RID: 173
		private readonly uint currentCpuUsage;
	}
}
