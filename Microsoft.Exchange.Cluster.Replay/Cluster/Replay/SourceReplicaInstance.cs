using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200031B RID: 795
	internal sealed class SourceReplicaInstance : ReplicaInstance
	{
		// Token: 0x060020B5 RID: 8373 RVA: 0x00096F94 File Offset: 0x00095194
		public SourceReplicaInstance(ReplayConfiguration replayConfiguration, ReplicaInstance previousReplicaInstance, IPerfmonCounters perfCounters) : base(replayConfiguration, false, previousReplicaInstance, perfCounters)
		{
			ReplicaInstance.DisposeIfActionUnsuccessful(delegate
			{
				this.TraceDebug("object created");
				ExTraceGlobals.PFDTracer.TracePfd<int>((long)this.GetHashCode(), "PFD CRS {0} SourceReplicaInstance is created", 32029);
				base.InitializeSuspendState();
				base.CurrentContext.InitializeForSource();
			}, this);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00096FC4 File Offset: 0x000951C4
		internal override bool GetSignatureAndCheckpoint(out JET_SIGNATURE? logfileSignature, out long lowestGenerationRequired, out long highestGenerationRequired, out long lastGenerationBackedUp)
		{
			logfileSignature = base.FileChecker.FileState.LogfileSignature;
			lowestGenerationRequired = 0L;
			highestGenerationRequired = 0L;
			lastGenerationBackedUp = 0L;
			if (logfileSignature == null)
			{
				base.FileChecker.TryUpdateActiveDatabaseLogfileSignature();
				logfileSignature = base.FileChecker.FileState.LogfileSignature;
			}
			return logfileSignature != null;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00097023 File Offset: 0x00095223
		internal override AmAcllReturnStatus AttemptCopyLastLogsRcr(AmAcllArgs acllArgs, AcllPerformanceTracker acllPerf)
		{
			throw new AcllInvalidForActiveCopyException(base.Configuration.DisplayName);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00097038 File Offset: 0x00095238
		protected override bool ConfigurationCheckerInternal()
		{
			this.TraceDebug("ConfigurationCheckerInternal()");
			base.CheckEdbLogDirectoryUnderMountPoint();
			base.CheckEdbLogDirectoriesIfNeeded();
			base.CheckInstanceAbortRequested();
			FileOperations.RemoveDirectory(base.Configuration.LogInspectorPath);
			string[] directoriesToCreate = new string[]
			{
				base.Configuration.SourceLogPath,
				base.Configuration.LogInspectorPath,
				base.Configuration.E00LogBackupPath
			};
			base.CreateDirectories(directoriesToCreate);
			base.CheckInstanceAbortRequested();
			string[] directoriesToCheck = new string[]
			{
				base.Configuration.SourceLogPath,
				base.Configuration.SourceSystemPath
			};
			base.CheckDirectories(directoriesToCheck);
			base.CheckInstanceAbortRequested();
			base.FileChecker.TryUpdateActiveDatabaseLogfileSignature();
			return true;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000970F0 File Offset: 0x000952F0
		protected override void StartComponents()
		{
			ReplayState replayState = base.Configuration.ReplayState;
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "Starting SourceReplicaInstance: {0}", base.Configuration.Identity);
			ExTraceGlobals.PFDTracer.TracePfd<int, string>((long)this.GetHashCode(), "PFD CRS {0} Starting SourceReplicaInstance: {1}", 23837, base.Configuration.Name);
			ReplayEventLogConstants.Tuple_SourceInstanceStart.LogEvent(null, new object[]
			{
				base.Configuration.ServerName,
				base.Configuration.SourceMachine,
				base.Configuration.Type,
				base.Configuration.Name
			});
			this.TraceDebug("started");
			ExTraceGlobals.PFDTracer.TracePfd<int, ReplayConfigType, string>((long)this.GetHashCode(), "PFD CRS {0} SourceReplicaInstance started {1} {2}", 19741, base.Configuration.Type, base.Configuration.Name);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000971DC File Offset: 0x000953DC
		protected override void PrepareToStopInternal()
		{
			this.TraceDebug("PrepareToStopInternal()");
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000971EC File Offset: 0x000953EC
		protected override void StopInternal()
		{
			if (base.StartedComponents)
			{
				ReplayEventLogConstants.Tuple_SourceInstanceStop.LogEvent(null, new object[]
				{
					base.Configuration.ServerName,
					base.Configuration.SourceMachine,
					base.Configuration.Type,
					base.Configuration.Name
				});
			}
			this.TraceDebug("StopInternal()");
			ExTraceGlobals.PFDTracer.TracePfd<int, ReplayConfigType, string>((long)this.GetHashCode(), "PFD CRS {0} SourceReplicaInstance is stopped {1} {2}", 29341, base.Configuration.Type, base.Configuration.Name);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0009728D File Offset: 0x0009548D
		protected override void TraceDebug(string message)
		{
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "SourceReplicaInstance {0}: {1}", base.Configuration.Name, message);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000972B1 File Offset: 0x000954B1
		protected override void TraceError(string message)
		{
			ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, string>((long)this.GetHashCode(), "SourceReplicaInstance {0}: {1}", base.Configuration.Name, message);
		}
	}
}
