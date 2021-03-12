using System;
using System.IO;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002B4 RID: 692
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeedProgressReporter : ChangePoller
	{
		// Token: 0x06001B1E RID: 6942 RVA: 0x00074D68 File Offset: 0x00072F68
		public SeedProgressReporter(Guid dbGuid, string databaseName, SeederClient client, Task.TaskErrorLoggingDelegate writeError, Task.TaskWarningLoggingDelegate writeWarning, SeedProgressReporter.ProgressReportDelegate progressDelegate, SeedProgressReporter.ProgressCompletedDelegate progressCompleted, SeedProgressReporter.ProgressFailedDelegate progressFailed) : base(true)
		{
			this.m_guid = dbGuid;
			this.m_databaseName = databaseName;
			this.m_client = client;
			this.m_writeError = writeError;
			this.m_writeWarning = writeWarning;
			this.m_progressDelegate = progressDelegate;
			this.m_progressCompleted = progressCompleted;
			this.m_progressFailed = progressFailed;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00074DC8 File Offset: 0x00072FC8
		private string GetFileName(string fullFilePath)
		{
			string result;
			try
			{
				result = Path.GetFileName(fullFilePath);
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.SeederClientTracer.TraceError<ArgumentException>((long)this.GetHashCode(), "SeedProgressReporter: MonitorProgress caught exception in GetFileName : {0}", arg);
				result = fullFilePath;
			}
			return result;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x00074E0C File Offset: 0x0007300C
		public bool ErrorOccurred
		{
			get
			{
				bool result;
				lock (this)
				{
					result = (this.m_lastException != null || (this.m_status != null && this.m_status.ErrorInfo.IsFailed()));
				}
				return result;
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00074E6C File Offset: 0x0007306C
		public void MonitorProgress()
		{
			ManualResetEvent firstRpcCompletedEvent;
			lock (this)
			{
				firstRpcCompletedEvent = this.m_firstRpcCompletedEvent;
			}
			if (firstRpcCompletedEvent != null)
			{
				firstRpcCompletedEvent.WaitOne();
			}
			lock (this)
			{
				if (this.ErrorOccurred)
				{
					this.HandleError();
					return;
				}
				goto IL_F8;
			}
			IL_5D:
			RpcSeederStatus rpcSeederStatus = null;
			lock (this)
			{
				if (this.m_status.State == SeederState.SeedSuccessful)
				{
					this.m_progressCompleted();
					return;
				}
				if (this.m_status.IsSeederStatusDataAvailable())
				{
					rpcSeederStatus = new RpcSeederStatus(this.m_status);
				}
			}
			if (rpcSeederStatus != null)
			{
				this.m_progressDelegate(this.GetFileName(rpcSeederStatus.FileFullPath), rpcSeederStatus.AddressForData, rpcSeederStatus.PercentComplete, rpcSeederStatus.BytesRead, rpcSeederStatus.BytesWritten, rpcSeederStatus.BytesRemaining, this.m_databaseName);
			}
			Thread.Sleep(1000);
			IL_F8:
			if (!this.ErrorOccurred)
			{
				goto IL_5D;
			}
			lock (this)
			{
				this.HandleError();
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00074FD4 File Offset: 0x000731D4
		protected override void PollerThread()
		{
			while (!this.m_fShutdown)
			{
				try
				{
					ExTraceGlobals.SeederClientTracer.TraceDebug((long)this.GetHashCode(), "SeedProgressReporter: PollerThread now making GetDbSeedStatus RPC.");
					RpcSeederStatus databaseSeedStatus = this.m_client.GetDatabaseSeedStatus(this.m_guid);
					lock (this)
					{
						this.m_status = databaseSeedStatus;
					}
				}
				catch (SeederServerTransientException ex)
				{
					this.m_lastException = ex;
					ExTraceGlobals.SeederClientTracer.TraceError<SeederServerTransientException>((long)this.GetHashCode(), "SeedProgressReporter: PollerThread caught exception in GetDbSeedStatus RPC: {0}", ex);
				}
				catch (SeederServerException ex2)
				{
					this.m_lastException = ex2;
					ExTraceGlobals.SeederClientTracer.TraceError<SeederServerException>((long)this.GetHashCode(), "SeedProgressReporter: PollerThread caught exception in GetDbSeedStatus RPC: {0}", ex2);
				}
				finally
				{
					lock (this)
					{
						ManualResetEvent firstRpcCompletedEvent = this.m_firstRpcCompletedEvent;
						if (this.m_firstRpcCompletedEvent != null)
						{
							this.m_firstRpcCompletedEvent = null;
							firstRpcCompletedEvent.Set();
							ExTraceGlobals.SeederClientTracer.TraceDebug((long)this.GetHashCode(), "SeedProgressReporter: Setting m_firstRpcCompletedEvent.");
						}
					}
				}
				if (this.ErrorOccurred)
				{
					ExTraceGlobals.SeederClientTracer.TraceDebug((long)this.GetHashCode(), "SeedProgressReporter: PollerThread exiting due to an error.");
					break;
				}
				if (this.m_status.State == SeederState.SeedSuccessful)
				{
					ExTraceGlobals.SeederClientTracer.TraceDebug((long)this.GetHashCode(), "SeedProgressReporter: PollerThread exiting because seeding was successful.");
					break;
				}
				if (this.m_shutdownEvent.WaitOne(1000, false))
				{
					break;
				}
			}
			if (this.m_fShutdown)
			{
				ExTraceGlobals.SeederClientTracer.TraceDebug((long)this.GetHashCode(), "SeedProgressReporter: PollerThread exiting due to shutdown.");
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0007518C File Offset: 0x0007338C
		private void HandleError()
		{
			this.m_progressFailed();
			Exception exception = this.GetException();
			string error;
			if (SeedHelper.IsPerformingFastOperationException(exception as SeederServerException, out error))
			{
				this.m_writeWarning(ReplayStrings.WarningPerformingFastOperationException(this.m_databaseName, error));
				return;
			}
			this.m_writeError(exception, ErrorCategory.InvalidOperation, null);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000751E0 File Offset: 0x000733E0
		private Exception GetException()
		{
			if (this.m_lastException != null)
			{
				return this.m_lastException;
			}
			try
			{
				SeederRpcExceptionWrapper.Instance.ClientRethrowIfFailed(this.m_databaseName, this.m_client.ServerName, this.m_status.ErrorInfo);
			}
			catch (SeederServerException result)
			{
				return result;
			}
			catch (SeederServerTransientException result2)
			{
				return result2;
			}
			DiagCore.RetailAssert(false, "No exception was thrown in GetException()!", new object[0]);
			return null;
		}

		// Token: 0x04000ADB RID: 2779
		private const int StatusQueryIntervalSecs = 1;

		// Token: 0x04000ADC RID: 2780
		private Guid m_guid;

		// Token: 0x04000ADD RID: 2781
		private string m_databaseName;

		// Token: 0x04000ADE RID: 2782
		private SeederClient m_client;

		// Token: 0x04000ADF RID: 2783
		private Task.TaskErrorLoggingDelegate m_writeError;

		// Token: 0x04000AE0 RID: 2784
		private Task.TaskWarningLoggingDelegate m_writeWarning;

		// Token: 0x04000AE1 RID: 2785
		private RpcSeederStatus m_status;

		// Token: 0x04000AE2 RID: 2786
		private SeedProgressReporter.ProgressReportDelegate m_progressDelegate;

		// Token: 0x04000AE3 RID: 2787
		private SeedProgressReporter.ProgressCompletedDelegate m_progressCompleted;

		// Token: 0x04000AE4 RID: 2788
		private SeedProgressReporter.ProgressFailedDelegate m_progressFailed;

		// Token: 0x04000AE5 RID: 2789
		private Exception m_lastException;

		// Token: 0x04000AE6 RID: 2790
		private ManualResetEvent m_firstRpcCompletedEvent = new ManualResetEvent(false);

		// Token: 0x020002B5 RID: 693
		// (Invoke) Token: 0x06001B26 RID: 6950
		public delegate void ProgressReportDelegate(string edbFileName, string addressForData, int percentComplete, long bytesRead, long bytesWritten, long bytesRemaining, string databaseName);

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x06001B2A RID: 6954
		public delegate void ProgressCompletedDelegate();

		// Token: 0x020002B7 RID: 695
		// (Invoke) Token: 0x06001B2E RID: 6958
		public delegate void ProgressFailedDelegate();
	}
}
