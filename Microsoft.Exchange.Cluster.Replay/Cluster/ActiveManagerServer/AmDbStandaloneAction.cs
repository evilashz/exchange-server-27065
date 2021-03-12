using System;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000044 RID: 68
	internal class AmDbStandaloneAction : AmDbAction
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00011602 File Offset: 0x0000F802
		internal AmDbStandaloneAction(AmConfig cfg, IADDatabase db, AmDbActionCode actionCode, string uniqueOperationId) : base(cfg, db, actionCode, uniqueOperationId)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011688 File Offset: 0x0000F888
		protected override void MountInternal(MountFlags storeFlags, AmMountFlags amMountFlags, DatabaseMountDialOverride mountDialoverride, ref AmDbOperationDetailedStatus mountStatus)
		{
			Exception ex = null;
			bool isSuccess = false;
			AmServerName activeServer = base.State.ActiveServer;
			AmServerName serverToMount = AmServerName.LocalComputerName;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				ReplayCrimsonEvents.DirectMountInitiated.LogGeneric(base.PrepareSubactionArgs(new object[]
				{
					serverToMount,
					storeFlags,
					false,
					amMountFlags
				}));
				ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					this.ReportStatus(AmDbActionStatus.StoreMountInitiated);
					this.WriteStateMountStart(serverToMount);
					AmDbAction.MountDatabaseDirect(serverToMount, this.State.LastMountedServer, this.DatabaseGuid, storeFlags, amMountFlags, this.ActionCode);
					isSuccess = true;
				});
			}
			finally
			{
				stopwatch.Stop();
				if (isSuccess)
				{
					base.DbTrace.Debug("Database is now mounted on {0}", new object[]
					{
						serverToMount
					});
					SharedDependencies.WritableADHelper.ResetAllowFileRestoreDsFlag(base.DatabaseGuid, activeServer, serverToMount);
					ReplayCrimsonEvents.DirectMountSuccess.LogGeneric(base.PrepareSubactionArgs(new object[]
					{
						serverToMount,
						stopwatch.Elapsed
					}));
					base.WriteStateMountSuccess();
					base.ReportStatus(AmDbActionStatus.StoreMountSuccessful);
				}
				else
				{
					string text = (ex != null) ? ex.Message : ReplayStrings.UnknownError;
					ReplayCrimsonEvents.DirectMountFailed.LogGeneric(base.PrepareSubactionArgs(new object[]
					{
						serverToMount,
						stopwatch.Elapsed,
						text
					}));
					base.WriteStateMountFailed(true);
					base.ReportStatus(AmDbActionStatus.StoreMountFailed);
				}
			}
			AmHelper.ThrowDbActionWrapperExceptionIfNecessary(ex);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011860 File Offset: 0x0000FA60
		protected override void DismountInternal(UnmountFlags flags)
		{
			base.DismountCommon(flags);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00011869 File Offset: 0x0000FA69
		protected override void MoveInternal(MountFlags mountFlags, UnmountFlags dismountFlags, DatabaseMountDialOverride mountDialoverride, AmServerName fromServer, AmServerName targetServer, bool tryOtherHealthyServers, AmBcsSkipFlags skipValidationChecks, string componentName, ref AmDbOperationDetailedStatus moveStatus)
		{
			throw new AmDbMoveOperationNotSupportedStandaloneException(base.DatabaseName);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00011878 File Offset: 0x0000FA78
		protected override void RemountInternal(MountFlags mountFlags, DatabaseMountDialOverride mountDialoverride, AmServerName fromServer)
		{
			AmDbOperationDetailedStatus amDbOperationDetailedStatus = new AmDbOperationDetailedStatus(base.Database);
			this.DismountInternal(UnmountFlags.SkipCacheFlush);
			this.MountInternal(mountFlags, AmMountFlags.None, mountDialoverride, ref amDbOperationDetailedStatus);
		}
	}
}
