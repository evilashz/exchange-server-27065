using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200000C RID: 12
	internal sealed class Imap4VirtualServer : VirtualServer, IDisposable
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003A50 File Offset: 0x00001C50
		public override int RpcLatencyCounterIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003A53 File Offset: 0x00001C53
		public override int LdapLatencyCounterIndex
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003A56 File Offset: 0x00001C56
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003A5E File Offset: 0x00001C5E
		internal ExPerformanceCounter[] ExceptionPerfCounters { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003A67 File Offset: 0x00001C67
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003A6F File Offset: 0x00001C6F
		internal ExPerformanceCounter[] LatencyPerfCounters { get; private set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00003A78 File Offset: 0x00001C78
		public Imap4VirtualServer(ProtocolBaseServices server, PopImapAdConfiguration configuration) : base(server, configuration)
		{
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe)
			{
				this.imap4CountersInstance = Imap4Counters.GetInstance(Strings.CAS);
			}
			else
			{
				this.imap4CountersInstance = Imap4Counters.GetInstance(Strings.MBX);
			}
			Imap4Counters.ResetInstance(this.imap4CountersInstance.Name);
			this.ExceptionPerfCounters = new ExPerformanceCounter[]
			{
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures,
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfMailboxOfflineErrors,
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfTransientStorageErrors,
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfPermanentStorageErrors,
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors,
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors,
				this.imap4CountersInstance.PerfCounter_RatePerMinuteOfTransientErrors
			};
			this.LatencyPerfCounters = new ExPerformanceCounter[]
			{
				this.imap4CountersInstance.PerfCounter_AverageRpcLatency,
				this.imap4CountersInstance.PerfCounter_AverageLdapLatency
			};
			if (!RatePerfCounters.Initialize(this.ExceptionPerfCounters, this.LatencyPerfCounters))
			{
				ProtocolBaseServices.LogEvent(base.Server.NoPerfCounterTimerEventTuple, null, new string[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003B8C File Offset: 0x00001D8C
		public override ExPerformanceCounter Connections_Current
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Connections_Current;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003B99 File Offset: 0x00001D99
		public override ExPerformanceCounter Connections_Failed
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Connections_Failed;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public override ExPerformanceCounter Connections_Rejected
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Connections_Rejected;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003BB3 File Offset: 0x00001DB3
		public override ExPerformanceCounter Connections_Total
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Connections_Total;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public override ExPerformanceCounter UnAuth_Connections
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_UnAuth_Connections_Current;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003BCD File Offset: 0x00001DCD
		public override ExPerformanceCounter SSLConnections_Current
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_SSLConnections_Current;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003BDA File Offset: 0x00001DDA
		public override ExPerformanceCounter SSLConnections_Total
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_SSLConnections_Total;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public override ExPerformanceCounter InvalidCommands
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Invalid_Commands;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public override ExPerformanceCounter AverageCommandProcessingTime
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Average_Command_Processing_Time;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003C01 File Offset: 0x00001E01
		public override ExPerformanceCounter Proxy_Connections_Current
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Proxy_Connections_Current;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003C0E File Offset: 0x00001E0E
		public override ExPerformanceCounter Proxy_Connections_Failed
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Proxy_Connections_Failed;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003C1B File Offset: 0x00001E1B
		public override ExPerformanceCounter Proxy_Connections_Total
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Proxy_Connections_Total;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003C28 File Offset: 0x00001E28
		public override ExPerformanceCounter Requests_Total
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Request_Total;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003C35 File Offset: 0x00001E35
		public override ExPerformanceCounter Requests_Failure
		{
			get
			{
				return this.imap4CountersInstance.PerfCounter_Request_Failures;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003C42 File Offset: 0x00001E42
		internal Imap4CountersInstance Imap4CountersInstance
		{
			get
			{
				return this.imap4CountersInstance;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C4A File Offset: 0x00001E4A
		public override ProtocolSession CreateNewSession(NetworkConnection connection)
		{
			return new Imap4Session(connection, this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C53 File Offset: 0x00001E53
		public override void ClosePerfCounters()
		{
			if (this.imap4CountersInstance != null)
			{
				Imap4Counters.ResetInstance(this.imap4CountersInstance.Name);
				Imap4Counters.CloseInstance(this.imap4CountersInstance.Name);
			}
			this.imap4CountersInstance = null;
		}

		// Token: 0x04000028 RID: 40
		private Imap4CountersInstance imap4CountersInstance;

		// Token: 0x0200000D RID: 13
		internal enum ExceptionPerfCountersType
		{
			// Token: 0x0400002C RID: 44
			ConnectionFailedTransientExceptionRate,
			// Token: 0x0400002D RID: 45
			MailboxOfflineExceptionRate,
			// Token: 0x0400002E RID: 46
			StorageTransientExceptionRate,
			// Token: 0x0400002F RID: 47
			StoragePermanentExceptionRate,
			// Token: 0x04000030 RID: 48
			AdTransientExceptionRate,
			// Token: 0x04000031 RID: 49
			AdPermanentExceptionRate,
			// Token: 0x04000032 RID: 50
			TransientErrorRate,
			// Token: 0x04000033 RID: 51
			MaxExceptionPerfCounters
		}
	}
}
