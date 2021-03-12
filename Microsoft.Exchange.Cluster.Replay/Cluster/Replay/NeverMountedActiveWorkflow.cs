using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200028E RID: 654
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NeverMountedActiveWorkflow : AutoReseedWorkflow
	{
		// Token: 0x0600197F RID: 6527 RVA: 0x0006A862 File Offset: 0x00068A62
		public NeverMountedActiveWorkflow(AutoReseedContext context, string workflowLaunchReason) : base(AutoReseedWorkflowType.MountNeverMountedActive, workflowLaunchReason, context)
		{
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0006A86D File Offset: 0x00068A6D
		protected override bool IsDisabled
		{
			get
			{
				return RegistryParameters.AutoReseedDbNeverMountedDisabled;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006A874 File Offset: 0x00068A74
		protected override TimeSpan GetThrottlingInterval(AutoReseedWorkflowState state)
		{
			return TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedDbNeverMountedThrottlingIntervalInSecs);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006A884 File Offset: 0x00068A84
		protected override LocalizedString RunPrereqs(AutoReseedWorkflowState state)
		{
			LocalizedString result = base.RunPrereqs(state);
			if (!result.IsEmpty)
			{
				return result;
			}
			result = FailedSuspendedCopyAutoReseedWorkflow.CheckExchangeVolumesPresent(base.Context);
			if (!result.IsEmpty)
			{
				return result;
			}
			return FailedSuspendedCopyAutoReseedWorkflow.CheckDatabaseLogPaths(base.Context);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0006A8C8 File Offset: 0x00068AC8
		protected override Exception ExecuteInternal(AutoReseedWorkflowState state)
		{
			Exception ex = null;
			bool flag = false;
			IVolumeManager volumeManager = base.Context.VolumeManager;
			IADDatabase database = base.Context.Database;
			MountedFolderPath databaseMountedFolderPath = VolumeManager.GetDatabaseMountedFolderPath(base.Context.Dag.AutoDagDatabasesRootFolderPath, database.Name);
			if (MountPointUtil.IsDirectoryAccessibleMountPoint(databaseMountedFolderPath.Path, out ex))
			{
				flag = true;
			}
			else
			{
				ReplayCrimsonEvents.AutoReseedNeverMountedActiveMissingVolume.Log<string>(database.Name);
				if (volumeManager.FixActiveDatabaseMountPoint(database, base.Context.Databases, base.Context.AdConfig, out ex, true))
				{
					flag = true;
					volumeManager.UpdateVolumeInfoCopyState(database.Guid, base.Context.ReplicaInstanceManager);
					ReplayCrimsonEvents.AutoReseedNeverMountedActiveAllocatedVolume.Log<string, string>(database.Name, base.Context.TargetCopyStatus.CopyStatus.LogVolumeName);
				}
				else
				{
					AutoReseedWorkflow.Tracer.TraceError<string, string>((long)this.GetHashCode(), "DiskReclaimer: UpdateVolumeForNeverMountedActives() failed to fix up active database: '{0}' mountpoint. Error: {1}", database.Name, AmExceptionHelper.GetExceptionMessageOrNoneString(ex));
					ReplayCrimsonEvents.AutoReseedFixActiveMountPointError.Log<string, string>(database.Name, AmExceptionHelper.GetExceptionMessageOrNoneString(ex));
				}
			}
			if (!flag)
			{
				AutoReseedWorkflow.Tracer.TraceError<string, string>((long)this.GetHashCode(), "DiskReclaimer: UpdateVolumeForNeverMountedActives() active database: '{0}' does not have a mountpoint. Skip issuing Store Mount. Error: {1}", database.Name, AmExceptionHelper.GetExceptionMessageOrNoneString(ex));
				return ex;
			}
			ex = AmRpcClientHelper.AdminMountDatabaseWrapper(database);
			if (ex != null)
			{
				AutoReseedWorkflow.Tracer.TraceError<string, string>((long)this.GetHashCode(), "DiskReclaimer: UpdateVolumeForNeverMountedActives() failed to mount active database: '{0}' mountpoint. Error: {1}", database.Name, ex.Message);
				ReplayCrimsonEvents.AutoReseedMountActiveDatabaseError.Log<string, string>(database.Name, ex.Message);
			}
			return ex;
		}
	}
}
