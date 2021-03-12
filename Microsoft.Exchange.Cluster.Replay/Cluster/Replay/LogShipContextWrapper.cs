using System;
using Microsoft.Exchange.Cluster.EseBack;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.HA.FailureItem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000119 RID: 281
	internal class LogShipContextWrapper : SafeRefCountedTimeoutWrapper
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00030174 File Offset: 0x0002E374
		public LogShipContextWrapper(string sourceMachineFqdn, string serverName, Guid identityGuid, string logFilePrefix, string destLogPath, bool circularLogging, TimeSpan timeout, ManualOneShotEvent cancelEvent) : base("LogShipContextWrapper", cancelEvent)
		{
			this.m_config = new LogShipContextWrapper.TruncationConfiguration
			{
				SourceMachine = sourceMachineFqdn,
				ServerName = serverName,
				IdentityGuid = identityGuid,
				LogFilePrefix = logFilePrefix,
				DestinationLogPath = destLogPath,
				CircularLoggingEnabled = circularLogging
			};
			this.m_timeout = timeout;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000301DB File Offset: 0x0002E3DB
		public LogShipContextWrapper(ITruncationConfiguration config, TimeSpan timeout, ManualOneShotEvent cancelEvent) : base("LogShipContextWrapper", cancelEvent)
		{
			this.m_config = config;
			this.m_timeout = timeout;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00030204 File Offset: 0x0002E404
		private CLogShipContext LogShipContext
		{
			get
			{
				CLogShipContext logShipContext;
				lock (this.m_lockObj)
				{
					if (this.m_logShipContext == null || this.m_logShipContext.IsInvalid)
					{
						this.Open();
					}
					logShipContext = this.m_logShipContext;
				}
				return logShipContext;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00030298 File Offset: 0x0002E498
		public void Truncate(long lgenReplicated, ref long lgenTruncatedLocally)
		{
			uint hr = 0U;
			uint tmpDwExtError = 0U;
			long tempLgenTruncatedLocally = 0L;
			try
			{
				base.ProtectedCall("LogShipTruncate", delegate
				{
					hr = this.LogShipContext.LogShipTruncate(lgenReplicated, ref tempLgenTruncatedLocally, out tmpDwExtError);
				});
			}
			catch (TimeoutException ex)
			{
				throw new FailedToTruncateLocallyException(258U, ReplayStrings.ReplayTestStoreConnectivityTimedoutException("LogShipTruncate", ex.Message), ex);
			}
			catch (OperationAbortedException ex2)
			{
				throw new FailedToTruncateLocallyException(1003U, ex2.Message, ex2);
			}
			if (hr == 0U)
			{
				lgenTruncatedLocally = tempLgenTruncatedLocally;
				return;
			}
			string optionalFriendlyError = SeedHelper.TranslateEsebackErrorCode((long)((ulong)hr), (long)((ulong)tmpDwExtError)) ?? ReplayStrings.SeederEcUndefined((int)hr);
			throw new FailedToTruncateLocallyException(hr, optionalFriendlyError);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000303C8 File Offset: 0x0002E5C8
		public void Notify(long lgenReplicated, ref long lgenLowestRequiredGlobally)
		{
			uint hr = 0U;
			uint tmpDwExtError = 0U;
			long tempLgenLowestRequiredGlobally = 0L;
			try
			{
				base.ProtectedCallWithTimeout("LogShipNotify", this.m_timeout, delegate
				{
					hr = this.LogShipContext.LogShipNotify(lgenReplicated, ref tempLgenLowestRequiredGlobally, out tmpDwExtError);
				});
			}
			catch (TimeoutException ex)
			{
				throw new FailedToNotifySourceLogTruncException(this.m_config.SourceMachine, 258U, ReplayStrings.ReplayTestStoreConnectivityTimedoutException("LogShipNotify", ex.Message), ex);
			}
			catch (OperationAbortedException ex2)
			{
				throw new FailedToNotifySourceLogTruncException(this.m_config.SourceMachine, 1003U, ex2.Message, ex2);
			}
			if (hr == 0U)
			{
				lgenLowestRequiredGlobally = tempLgenLowestRequiredGlobally;
				return;
			}
			string optionalFriendlyError = SeedHelper.TranslateEsebackErrorCode((long)((ulong)hr), (long)((ulong)tmpDwExtError)) ?? ReplayStrings.SeederEcUndefined((int)hr);
			throw new FailedToNotifySourceLogTruncException(this.m_config.SourceMachine, hr, optionalFriendlyError);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000304EC File Offset: 0x0002E6EC
		protected override void InternalProtectedDispose()
		{
			if (this.m_logShipContext != null && !this.m_logShipContext.IsInvalid)
			{
				this.m_logShipContext.Dispose();
				this.m_logShipContext = null;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000305BC File Offset: 0x0002E7BC
		private void Open()
		{
			if (this.m_logShipContext != null && !this.m_logShipContext.IsInvalid)
			{
				return;
			}
			uint hr = 0U;
			uint tmpDwExtError = 0U;
			CLogShipContext tmpLogShipContext = null;
			uint rpcTimeoutMsec = (uint)this.m_timeout.TotalMilliseconds;
			string guid = this.m_config.IdentityGuid.ToString();
			Action operation = delegate()
			{
				hr = CLogShipContext.Open(this.m_config.SourceMachine, guid, this.m_config.ServerName, this.m_config.LogFilePrefix, this.m_config.DestinationLogPath, this.m_config.CircularLoggingEnabled, ReplicaType.StandbyReplica, rpcTimeoutMsec, out tmpDwExtError, out tmpLogShipContext);
				if (hr == 0U)
				{
					this.m_logShipContext = tmpLogShipContext;
				}
			};
			try
			{
				base.ProtectedCallWithTimeout("LogShipOpenContext", this.m_timeout, operation);
				if (hr != 0U || tmpLogShipContext == null || tmpLogShipContext.IsInvalid)
				{
					if (hr == 3355379665U)
					{
						throw new CopyUnknownToActiveLogTruncationException(guid, this.m_config.SourceMachine, this.m_config.ServerName, hr);
					}
					string text;
					if (hr == 3355444321U)
					{
						text = ReplayStrings.FileSystemCorruptionDetected(this.m_config.DestinationLogPath);
						ReplayEventLogConstants.Tuple_FilesystemCorrupt.LogEvent(this.m_config.IdentityGuid.ToString(), new object[]
						{
							this.m_config.DestinationLogPath
						});
						FailureItemPublisherHelper.PublishAction(FailureTag.FileSystemCorruption, this.m_config.IdentityGuid, this.m_config.IdentityGuid.ToString());
					}
					else
					{
						text = SeedHelper.TranslateEsebackErrorCode((long)((ulong)hr), (long)((ulong)tmpDwExtError));
						if (text == null)
						{
							text = ReplayStrings.SeederEcUndefined((int)hr);
						}
					}
					throw new FailedToOpenLogTruncContextException(this.m_config.SourceMachine, hr, text);
				}
			}
			catch (TimeoutException ex)
			{
				throw new FailedToOpenLogTruncContextException(this.m_config.SourceMachine, 258U, ReplayStrings.ReplayTestStoreConnectivityTimedoutException("LogShipOpenContext", ex.Message), ex);
			}
			catch (OperationAbortedException ex2)
			{
				throw new FailedToOpenLogTruncContextException(this.m_config.SourceMachine, 1003U, ex2.Message, ex2);
			}
		}

		// Token: 0x04000487 RID: 1159
		private readonly object m_lockObj = new object();

		// Token: 0x04000488 RID: 1160
		private readonly ITruncationConfiguration m_config;

		// Token: 0x04000489 RID: 1161
		private readonly TimeSpan m_timeout;

		// Token: 0x0400048A RID: 1162
		private CLogShipContext m_logShipContext;

		// Token: 0x0200011B RID: 283
		private class TruncationConfiguration : ITruncationConfiguration
		{
			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00030814 File Offset: 0x0002EA14
			// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0003081C File Offset: 0x0002EA1C
			public string SourceMachine { get; set; }

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00030825 File Offset: 0x0002EA25
			// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0003082D File Offset: 0x0002EA2D
			public string ServerName { get; set; }

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00030836 File Offset: 0x0002EA36
			// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0003083E File Offset: 0x0002EA3E
			public Guid IdentityGuid { get; set; }

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00030847 File Offset: 0x0002EA47
			// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0003084F File Offset: 0x0002EA4F
			public string LogFilePrefix { get; set; }

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00030858 File Offset: 0x0002EA58
			// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x00030860 File Offset: 0x0002EA60
			public string DestinationLogPath { get; set; }

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00030869 File Offset: 0x0002EA69
			// (set) Token: 0x06000ACB RID: 2763 RVA: 0x00030871 File Offset: 0x0002EA71
			public bool CircularLoggingEnabled { get; set; }
		}
	}
}
