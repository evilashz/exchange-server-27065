using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000996 RID: 2454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskSeeder : IDisposable
	{
		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x060057B6 RID: 22454 RVA: 0x0016E65F File Offset: 0x0016C85F
		// (set) Token: 0x060057B7 RID: 22455 RVA: 0x0016E667 File Offset: 0x0016C867
		public bool Force { get; set; }

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x060057B8 RID: 22456 RVA: 0x0016E670 File Offset: 0x0016C870
		// (set) Token: 0x060057B9 RID: 22457 RVA: 0x0016E678 File Offset: 0x0016C878
		public bool DeleteExistingFiles { get; set; }

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x060057BA RID: 22458 RVA: 0x0016E681 File Offset: 0x0016C881
		// (set) Token: 0x060057BB RID: 22459 RVA: 0x0016E689 File Offset: 0x0016C889
		public bool SafeDeleteExistingFiles { get; set; }

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x060057BC RID: 22460 RVA: 0x0016E692 File Offset: 0x0016C892
		// (set) Token: 0x060057BD RID: 22461 RVA: 0x0016E69A File Offset: 0x0016C89A
		public bool AutoSuspend { get; set; }

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x060057BE RID: 22462 RVA: 0x0016E6A3 File Offset: 0x0016C8A3
		// (set) Token: 0x060057BF RID: 22463 RVA: 0x0016E6AB File Offset: 0x0016C8AB
		public bool ManualResume { get; set; }

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x060057C0 RID: 22464 RVA: 0x0016E6B4 File Offset: 0x0016C8B4
		// (set) Token: 0x060057C1 RID: 22465 RVA: 0x0016E6BC File Offset: 0x0016C8BC
		public bool SeedDatabaseFiles { get; set; }

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x0016E6C5 File Offset: 0x0016C8C5
		// (set) Token: 0x060057C3 RID: 22467 RVA: 0x0016E6CD File Offset: 0x0016C8CD
		public bool SeedCiFiles { get; set; }

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x0016E6D6 File Offset: 0x0016C8D6
		// (set) Token: 0x060057C5 RID: 22469 RVA: 0x0016E6DE File Offset: 0x0016C8DE
		public DatabaseAvailabilityGroupNetworkIdParameter NetworkId { get; set; }

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x0016E6E7 File Offset: 0x0016C8E7
		// (set) Token: 0x060057C7 RID: 22471 RVA: 0x0016E6EF File Offset: 0x0016C8EF
		public bool? CompressOverride { get; set; }

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x060057C8 RID: 22472 RVA: 0x0016E6F8 File Offset: 0x0016C8F8
		// (set) Token: 0x060057C9 RID: 22473 RVA: 0x0016E700 File Offset: 0x0016C900
		public bool? EncryptOverride { get; set; }

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x0016E709 File Offset: 0x0016C909
		// (set) Token: 0x060057CB RID: 22475 RVA: 0x0016E711 File Offset: 0x0016C911
		public bool BeginSeed { get; set; }

		// Token: 0x060057CC RID: 22476 RVA: 0x0016E71C File Offset: 0x0016C91C
		public TaskSeeder(SeedingTask seedingTask, Server server, Database database, Server sourceServer, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning, Task.TaskErrorLoggingDelegate writeError, Task.TaskProgressLoggingDelegate writeProgress, Task.TaskShouldContinueDelegate shouldContinue, TaskSeeder.TaskIsStoppingDelegate stopping)
		{
			this.m_seedingTask = seedingTask;
			this.m_server = server;
			this.m_dbGuid = database.Guid;
			this.m_dbName = database.Name;
			this.m_isPublicDB = database.IsPublicFolderDatabase;
			this.m_sourceServer = sourceServer;
			this.m_writeVerbose = writeVerbose;
			this.m_writeWarning = writeWarning;
			this.m_writeError = writeError;
			this.m_writeProgress = writeProgress;
			this.m_shouldContinue = shouldContinue;
			this.m_stopping = stopping;
			this.m_taskName = this.GetTaskNameFromSeedingEnum(this.m_seedingTask);
			this.InitializeDefaultParameters();
			this.m_seeder = SeederClient.Create(this.m_server, this.m_dbName, this.m_sourceServer);
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x0016E7CE File Offset: 0x0016C9CE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0016E7DD File Offset: 0x0016C9DD
		public void Dispose(bool disposing)
		{
			if (!this.m_fDisposed)
			{
				if (disposing && this.m_seeder != null)
				{
					this.m_seeder.Dispose();
					this.m_seeder = null;
				}
				this.m_fDisposed = true;
			}
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0016E80C File Offset: 0x0016CA0C
		public void SeedDatabase()
		{
			bool flag = true;
			try
			{
				bool flag2 = this.TryPrepareDbSeedAndBegin(false);
				if (flag2)
				{
					this.PerformSeedCleanup();
					flag = !this.TryPrepareDbSeedAndBegin(true);
				}
				flag = (!this.BeginSeed && flag);
				if (flag)
				{
					SeedProgressReporter seedProgressReporter = new SeedProgressReporter(this.m_dbGuid, this.m_dbName, this.m_seeder, this.m_writeError, this.m_writeWarning, new SeedProgressReporter.ProgressReportDelegate(this.ProgressReportDatabaseSeeding), new SeedProgressReporter.ProgressCompletedDelegate(this.ProgressComplete), new SeedProgressReporter.ProgressFailedDelegate(this.ProgressFailed));
					seedProgressReporter.Start();
					try
					{
						seedProgressReporter.MonitorProgress();
					}
					catch (PipelineStoppedException arg)
					{
						ExTraceGlobals.CmdletsTracer.TraceDebug<string, PipelineStoppedException>((long)this.GetHashCode(), "{0} task caught PipelineStoppedException: {1}.", this.m_taskName, arg);
					}
					finally
					{
						seedProgressReporter.Stop();
					}
				}
			}
			finally
			{
				if (this.m_seedingTask == SeedingTask.UpdateDatabaseCopy && this.ManualResume && flag)
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: skipped to AutoResume {1} because -ManualResume is specified or it failed to seed.", this.m_taskName, this.m_dbName);
					if (!this.m_stopping())
					{
						this.m_writeWarning(Strings.WarningForStillSuspended(this.m_dbName, this.m_server.Name));
					}
				}
			}
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x0016E958 File Offset: 0x0016CB58
		public void CancelSeed()
		{
			ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: CancelSeed() called", this.m_taskName);
			this.m_writeVerbose(Strings.SeederCancelDbSeedRpcBegin(this.m_taskName, this.m_server.Name, this.m_dbName));
			try
			{
				this.m_seeder.CancelDbSeed(this.m_dbGuid);
			}
			catch (SeederServerTransientException ex)
			{
				this.m_writeError(new SeederCancelDbSeedRpcFailedException(this.m_dbName, this.m_server.Name, ex.Message, ex), ErrorCategory.InvalidOperation, null);
			}
			catch (SeederServerException ex2)
			{
				this.m_writeError(new SeederCancelDbSeedRpcFailedException(this.m_dbName, this.m_server.Name, ex2.Message, ex2), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x0016EA34 File Offset: 0x0016CC34
		private bool TryPrepareDbSeedAndBegin(bool fFailOnError)
		{
			bool result = false;
			try
			{
				this.m_writeVerbose(Strings.SeederPrepareDbSeedRpcBegin(this.m_taskName, this.m_server.Name));
				string text = string.Empty;
				if (this.NetworkId != null)
				{
					if (this.NetworkId.NetName != null)
					{
						text = this.NetworkId.NetName;
					}
					else
					{
						text = this.NetworkId.ToString();
					}
					ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Seeding over '{0}'. Full name provided was '{1}'", text, this.NetworkId.ToString());
				}
				this.m_seeder.PrepareDbSeedAndBegin(this.m_dbGuid, this.DeleteExistingFiles, this.SafeDeleteExistingFiles, this.AutoSuspend, this.ManualResume, this.SeedDatabaseFiles, this.SeedCiFiles, text, null, (this.m_sourceServer == null) ? string.Empty : this.m_sourceServer.Fqdn, this.CompressOverride, this.EncryptOverride, SeederRpcFlags.None);
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: succeeded to begin seeding {1}", this.m_taskName, this.m_dbName);
			}
			catch (SeederInstanceAlreadyInProgressException ex)
			{
				result = this.PromptOrWriteError(ex, fFailOnError, Strings.SeederAlreadyInProgressPrompt(this.m_dbName, this.m_server.Name, ex.SourceMachine));
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: found another seed instance in progress for {1}. Reporting on that instance now.", this.m_taskName, this.m_dbName);
			}
			catch (SeederInstanceAlreadyCompletedException ex2)
			{
				result = this.PromptOrWriteError(ex2, fFailOnError, Strings.SeederAlreadyCompletedPrompt(this.m_dbName, this.m_server.Name, ex2.SourceMachine));
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: found another completed seed instance for {1}. Now attempting to cleanup that instance.", this.m_taskName, this.m_dbName);
			}
			catch (SeederInstanceAlreadyFailedException ex3)
			{
				result = this.PromptOrWriteError(ex3, fFailOnError, Strings.SeederAlreadyFailedPrompt(this.m_dbName, this.m_server.Name, ex3.SourceMachine));
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: found another failed seed instance for {1}. Now attempting to cleanup that instance.", this.m_taskName, this.m_dbName);
			}
			catch (SeederInstanceAlreadyCancelledException ex4)
			{
				result = this.PromptOrWriteError(ex4, fFailOnError, Strings.SeederAlreadyCancelledPrompt(this.m_dbName, this.m_server.Name, ex4.SourceMachine));
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: found another cancelled seed instance for {1}. Now attempting to cleanup that instance.", this.m_taskName, this.m_dbName);
			}
			catch (SeederServerException ex5)
			{
				this.ProgressFailed();
				ExTraceGlobals.CmdletsTracer.TraceError<string, string, SeederServerException>((long)this.GetHashCode(), "{0}: failed to begin seeding {1}, reason: {2}", this.m_taskName, this.m_dbName, ex5);
				this.m_writeError(ex5, ErrorCategory.InvalidOperation, this.m_dbName);
			}
			catch (SeederServerTransientException ex6)
			{
				this.ProgressFailed();
				ExTraceGlobals.CmdletsTracer.TraceError<string, string, SeederServerTransientException>((long)this.GetHashCode(), "{0}: failed to begin seeding {1}, reason: {2}", this.m_taskName, this.m_dbName, ex6);
				this.m_writeError(ex6, ErrorCategory.InvalidOperation, this.m_dbName);
			}
			return result;
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x0016ED4C File Offset: 0x0016CF4C
		private bool PromptOrWriteError(SeederServerException ex, bool fFailOnError, LocalizedString promptMessage)
		{
			bool result = true;
			ExTraceGlobals.CmdletsTracer.TraceError<string, string, SeederServerException>((long)this.GetHashCode(), "{0}: failed to begin seeding {1}, reason: {2}", this.m_taskName, this.m_dbName, ex);
			if (fFailOnError || (!this.Force && !this.m_shouldContinue(promptMessage)))
			{
				if (fFailOnError)
				{
					result = false;
				}
				this.m_writeError(ex, ErrorCategory.InvalidOperation, null);
				return result;
			}
			if (ex is SeederInstanceAlreadyInProgressException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0016EDB7 File Offset: 0x0016CFB7
		private void ProgressFailed()
		{
			if (this.m_progressString != null)
			{
				this.m_writeProgress(Strings.ProgressStatusFailed, new LocalizedString(this.m_progressString), 100);
			}
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x0016EDDE File Offset: 0x0016CFDE
		private void ProgressComplete()
		{
			if (this.m_progressString != null)
			{
				this.m_writeProgress(Strings.ProgressStatusCompleted, new LocalizedString(this.m_progressString), 100);
			}
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0016EE08 File Offset: 0x0016D008
		private void ProgressReportDatabaseSeeding(string edbName, string addressForData, int percent, long bytesRead, long bytesWritten, long bytesRemaining, string databaseName)
		{
			if (!string.IsNullOrEmpty(addressForData))
			{
				this.m_progressString = Strings.SeederProgressMsgOverSpecifiedNetwork(edbName, this.m_server.Name, addressForData, bytesRead / 1024L, bytesWritten / 1024L, bytesRemaining / 1024L, databaseName);
			}
			else
			{
				this.m_progressString = Strings.SeederProgressMsgNoNetwork(edbName, this.m_server.Name, bytesRead / 1024L, bytesWritten / 1024L, bytesRemaining / 1024L, databaseName);
			}
			this.m_writeProgress(Strings.ProgressStatusInProgress, new LocalizedString(this.m_progressString), percent);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x0016EEB0 File Offset: 0x0016D0B0
		private void PerformSeedCleanup()
		{
			if (this.m_seeder != null)
			{
				try
				{
					this.m_writeVerbose(Strings.SeederEndDbSeedRpcBegin(this.m_taskName, this.m_server.Name, this.m_dbName));
					this.m_seeder.EndDbSeed(this.m_dbGuid);
				}
				catch (SeederServerTransientException ex)
				{
					ExTraceGlobals.CmdletsTracer.TraceError<string, SeederServerTransientException>((long)this.GetHashCode(), "{0}: TrySeedCleanup: Exception caught during Cleanup RPC: {1}", this.m_taskName, ex);
					this.m_writeError(new SeederEndDbSeedRpcFailedException(this.m_dbName, this.m_server.Name, ex.Message, ex), ErrorCategory.InvalidOperation, null);
				}
				catch (SeederServerException ex2)
				{
					ExTraceGlobals.CmdletsTracer.TraceError<string, SeederServerException>((long)this.GetHashCode(), "{0}: TrySeedCleanup: Exception caught during Cleanup RPC: {1}", this.m_taskName, ex2);
					this.m_writeError(new SeederEndDbSeedRpcFailedException(this.m_dbName, this.m_server.Name, ex2.Message, ex2), ErrorCategory.InvalidOperation, null);
				}
			}
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x0016EFB8 File Offset: 0x0016D1B8
		private string GetTaskNameFromSeedingEnum(SeedingTask seedingTask)
		{
			string result = string.Empty;
			switch (seedingTask)
			{
			case SeedingTask.AddMailboxDatabaseCopy:
				result = "Add-MailboxDatabaseCopy";
				break;
			case SeedingTask.UpdateDatabaseCopy:
				result = "Update-DatabaseCopy";
				break;
			default:
				DiagCore.RetailAssert(false, "Unhandled case for SeedingTask '{0}'", new object[]
				{
					seedingTask
				});
				break;
			}
			return result;
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x0016F00C File Offset: 0x0016D20C
		private void InitializeDefaultParameters()
		{
			switch (this.m_seedingTask)
			{
			case SeedingTask.AddMailboxDatabaseCopy:
				this.DeleteExistingFiles = true;
				this.SafeDeleteExistingFiles = false;
				this.AutoSuspend = true;
				this.ManualResume = false;
				this.SeedDatabaseFiles = true;
				this.SeedCiFiles = !this.m_isPublicDB;
				this.Force = true;
				return;
			case SeedingTask.UpdateDatabaseCopy:
				this.AutoSuspend = false;
				return;
			default:
				return;
			}
		}

		// Token: 0x0400328C RID: 12940
		private readonly string m_taskName;

		// Token: 0x0400328D RID: 12941
		private SeedingTask m_seedingTask;

		// Token: 0x0400328E RID: 12942
		private Server m_server;

		// Token: 0x0400328F RID: 12943
		private readonly Guid m_dbGuid;

		// Token: 0x04003290 RID: 12944
		private readonly string m_dbName;

		// Token: 0x04003291 RID: 12945
		private Server m_sourceServer;

		// Token: 0x04003292 RID: 12946
		private Task.TaskVerboseLoggingDelegate m_writeVerbose;

		// Token: 0x04003293 RID: 12947
		private Task.TaskWarningLoggingDelegate m_writeWarning;

		// Token: 0x04003294 RID: 12948
		private Task.TaskErrorLoggingDelegate m_writeError;

		// Token: 0x04003295 RID: 12949
		private Task.TaskProgressLoggingDelegate m_writeProgress;

		// Token: 0x04003296 RID: 12950
		private Task.TaskShouldContinueDelegate m_shouldContinue;

		// Token: 0x04003297 RID: 12951
		private TaskSeeder.TaskIsStoppingDelegate m_stopping;

		// Token: 0x04003298 RID: 12952
		private readonly bool m_isPublicDB;

		// Token: 0x04003299 RID: 12953
		private string m_progressString;

		// Token: 0x0400329A RID: 12954
		private bool m_fDisposed;

		// Token: 0x0400329B RID: 12955
		private SeederClient m_seeder;

		// Token: 0x02000997 RID: 2455
		// (Invoke) Token: 0x060057DA RID: 22490
		public delegate bool TaskIsStoppingDelegate();
	}
}
