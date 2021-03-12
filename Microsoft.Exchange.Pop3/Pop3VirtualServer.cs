using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200001D RID: 29
	internal sealed class Pop3VirtualServer : VirtualServer
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005D1B File Offset: 0x00003F1B
		public override int RpcLatencyCounterIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005D1E File Offset: 0x00003F1E
		public override int LdapLatencyCounterIndex
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005D21 File Offset: 0x00003F21
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005D29 File Offset: 0x00003F29
		internal ExPerformanceCounter[] ExceptionPerfCounters { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005D32 File Offset: 0x00003F32
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005D3A File Offset: 0x00003F3A
		internal ExPerformanceCounter[] LatencyPerfCounters { get; private set; }

		// Token: 0x060000E4 RID: 228 RVA: 0x00005D44 File Offset: 0x00003F44
		public Pop3VirtualServer(ProtocolBaseServices server, PopImapAdConfiguration configuration) : base(server, configuration)
		{
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe)
			{
				this.pop3CountersInstance = Pop3Counters.GetInstance(Strings.CAS);
			}
			else
			{
				this.pop3CountersInstance = Pop3Counters.GetInstance(Strings.MBX);
			}
			Pop3Counters.ResetInstance(this.pop3CountersInstance.Name);
			this.ExceptionPerfCounters = new ExPerformanceCounter[]
			{
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures,
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfMailboxOfflineErrors,
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfTransientStorageErrors,
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfPermanentStorageErrors,
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors,
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors,
				this.pop3CountersInstance.PerfCounter_RatePerMinuteOfTransientErrors
			};
			this.LatencyPerfCounters = new ExPerformanceCounter[]
			{
				this.pop3CountersInstance.PerfCounter_AverageRpcLatency,
				this.pop3CountersInstance.PerfCounter_AverageLdapLatency
			};
			if (!RatePerfCounters.Initialize(this.ExceptionPerfCounters, this.LatencyPerfCounters))
			{
				ProtocolBaseServices.LogEvent(base.Server.NoPerfCounterTimerEventTuple, null, new string[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005E58 File Offset: 0x00004058
		public override ExPerformanceCounter Connections_Current
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Connections_Current;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005E65 File Offset: 0x00004065
		public override ExPerformanceCounter Connections_Failed
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Connections_Failed;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005E72 File Offset: 0x00004072
		public override ExPerformanceCounter Connections_Rejected
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Connections_Rejected;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005E7F File Offset: 0x0000407F
		public override ExPerformanceCounter Connections_Total
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Connections_Total;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005E8C File Offset: 0x0000408C
		public override ExPerformanceCounter UnAuth_Connections
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_UnAuth_Connections_Current;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005E99 File Offset: 0x00004099
		public override ExPerformanceCounter SSLConnections_Current
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_SSLConnections_Current;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005EA6 File Offset: 0x000040A6
		public override ExPerformanceCounter SSLConnections_Total
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_SSLConnections_Total;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005EB3 File Offset: 0x000040B3
		public override ExPerformanceCounter InvalidCommands
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Invalid_Commands;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005EC0 File Offset: 0x000040C0
		public override ExPerformanceCounter AverageCommandProcessingTime
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Average_Command_Processing_Time;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005ECD File Offset: 0x000040CD
		public override ExPerformanceCounter Proxy_Connections_Current
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Proxy_Connections_Current;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005EDA File Offset: 0x000040DA
		public override ExPerformanceCounter Proxy_Connections_Failed
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Proxy_Connections_Failed;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005EE7 File Offset: 0x000040E7
		public override ExPerformanceCounter Proxy_Connections_Total
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Proxy_Connections_Total;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005EF4 File Offset: 0x000040F4
		public override ExPerformanceCounter Requests_Total
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Request_Total;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005F01 File Offset: 0x00004101
		public override ExPerformanceCounter Requests_Failure
		{
			get
			{
				return this.pop3CountersInstance.PerfCounter_Request_Failures;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005F0E File Offset: 0x0000410E
		internal Pop3CountersInstance Pop3CountersInstance
		{
			get
			{
				return this.pop3CountersInstance;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005F16 File Offset: 0x00004116
		public override ProtocolSession CreateNewSession(NetworkConnection connection)
		{
			return new Pop3Session(connection, this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005F1F File Offset: 0x0000411F
		public override void ClosePerfCounters()
		{
			if (this.pop3CountersInstance != null)
			{
				Pop3Counters.ResetInstance(this.pop3CountersInstance.Name);
				Pop3Counters.CloseInstance(this.pop3CountersInstance.Name);
			}
			this.pop3CountersInstance = null;
		}

		// Token: 0x0400007D RID: 125
		private Pop3CountersInstance pop3CountersInstance;

		// Token: 0x0200001E RID: 30
		internal enum ExceptionPerfCountersType
		{
			// Token: 0x04000081 RID: 129
			ConnectionFailedTransientExceptionRate,
			// Token: 0x04000082 RID: 130
			MailboxOfflineExceptionRate,
			// Token: 0x04000083 RID: 131
			StorageTransientExceptionRate,
			// Token: 0x04000084 RID: 132
			StoragePermanentExceptionRate,
			// Token: 0x04000085 RID: 133
			AdTransientExceptionRate,
			// Token: 0x04000086 RID: 134
			AdPermanentExceptionRate,
			// Token: 0x04000087 RID: 135
			TransientErrorRate,
			// Token: 0x04000088 RID: 136
			MaxExceptionPerfCounters
		}
	}
}
