using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmBcsServerValidation
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x000208BA File Offset: 0x0001EABA
		public AmBcsServerValidation(AmServerName serverToCheck, AmServerName sourceServer, IADDatabase database, AmConfig amConfig, IAmBcsErrorLogger errorLogger, IMonitoringADConfig dagConfig)
		{
			this.ServerToCheck = serverToCheck;
			this.SourceServer = sourceServer;
			this.Database = database;
			this.AmConfig = amConfig;
			this.ErrorLogger = errorLogger;
			this.DagConfig = dagConfig;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000208EF File Offset: 0x0001EAEF
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x000208F7 File Offset: 0x0001EAF7
		private AmServerName ServerToCheck { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00020900 File Offset: 0x0001EB00
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00020908 File Offset: 0x0001EB08
		private AmServerName SourceServer { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00020911 File Offset: 0x0001EB11
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00020919 File Offset: 0x0001EB19
		private IADDatabase Database { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00020922 File Offset: 0x0001EB22
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0002092A File Offset: 0x0001EB2A
		private AmConfig AmConfig { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00020933 File Offset: 0x0001EB33
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0002093B File Offset: 0x0001EB3B
		private IMonitoringADConfig DagConfig { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00020944 File Offset: 0x0001EB44
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0002094C File Offset: 0x0001EB4C
		private IAmBcsErrorLogger ErrorLogger { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00020955 File Offset: 0x0001EB55
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0002095D File Offset: 0x0001EB5D
		private string ErrorMessage { get; set; }

		// Token: 0x060006D1 RID: 1745 RVA: 0x00020968 File Offset: 0x0001EB68
		public static AmBcsServerChecks GetServerValidationChecks(AmDbActionCode actionCode, bool isServerSpecifiedByAdmin)
		{
			AmBcsServerChecks result = AmBcsServerChecks.DebugOptionDisabled | AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted | AmBcsServerChecks.AutoActivationAllowed;
			if (actionCode.IsAdminMoveOperation)
			{
				result = (isServerSpecifiedByAdmin ? (AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted) : (AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted | AmBcsServerChecks.AutoActivationAllowed));
			}
			else if (actionCode.IsAdminMountOperation)
			{
				result = (AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted);
			}
			return result;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00020998 File Offset: 0x0001EB98
		public bool RunChecks(AmBcsServerChecks checks, ref LocalizedString error)
		{
			bool flag = true;
			error = LocalizedString.Empty;
			if (flag && AmBcsServerValidation.ShouldRunCheck(checks, AmBcsServerChecks.DebugOptionDisabled))
			{
				flag = this.CheckDebugOption(ref error);
			}
			if (flag && AmBcsServerValidation.ShouldRunCheck(checks, AmBcsServerChecks.DatacenterActivationModeStarted))
			{
				flag = this.IsServerStartedForDACMode(ref error);
			}
			if (flag && AmBcsServerValidation.ShouldRunCheck(checks, AmBcsServerChecks.ClusterNodeUp))
			{
				flag = this.IsClusterNodeUp(ref error);
			}
			if (flag && AmBcsServerValidation.ShouldRunCheck(checks, AmBcsServerChecks.AutoActivationAllowed))
			{
				flag = this.IsAutoActivationAllowed(ref error);
			}
			return flag;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00020A03 File Offset: 0x0001EC03
		private static bool ShouldRunCheck(AmBcsServerChecks checksToRun, AmBcsServerChecks checkInQuestion)
		{
			return (checksToRun & checkInQuestion) == checkInQuestion;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00020A0C File Offset: 0x0001EC0C
		private bool CheckDebugOption(ref LocalizedString error)
		{
			if (this.AmConfig.IsIgnoreServerDebugOptionEnabled(this.ServerToCheck))
			{
				string text = AmDebugOptions.IgnoreServerFromAutomaticActions.ToString();
				AmTrace.Error("AmBcsServerValidation: Rejecting server '{0}' for DB '{1}' because the node is marked in debug option. Debug options: {2}", new object[]
				{
					this.ServerToCheck,
					this.Database.Name,
					text
				});
				error = ReplayStrings.AmBcsTargetNodeDebugOptionEnabled(this.ServerToCheck.Fqdn, text);
				if (this.ErrorLogger != null)
				{
					this.ErrorLogger.ReportServerFailure(this.ServerToCheck, AmBcsServerChecks.DebugOptionDisabled.ToString(), error, ReplayCrimsonEvents.OperationNotPerformedDueToDebugOption, new object[]
					{
						this.ServerToCheck.NetbiosName,
						text,
						"Best copy selection"
					});
				}
				return false;
			}
			return true;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		private bool IsClusterNodeUp(ref LocalizedString error)
		{
			if (this.AmConfig.DagConfig.IsNodePubliclyUp(this.ServerToCheck))
			{
				return true;
			}
			AmTrace.Error("AmBcsServerValidation: Rejecting server '{0}' for DB '{1}' because the node is down.", new object[]
			{
				this.ServerToCheck,
				this.Database.Name
			});
			error = ReplayStrings.AmBcsTargetNodeDownError(this.ServerToCheck.Fqdn);
			this.ReportServerBlocked(AmBcsServerChecks.ClusterNodeUp, error);
			return false;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00020B4C File Offset: 0x0001ED4C
		private bool IsServerStartedForDACMode(ref LocalizedString error)
		{
			string adError = null;
			IADDatabaseAvailabilityGroup iaddatabaseAvailabilityGroup;
			if (this.DagConfig != null)
			{
				iaddatabaseAvailabilityGroup = this.DagConfig.Dag;
				if (iaddatabaseAvailabilityGroup == null)
				{
					CouldNotFindDagObjectForServer couldNotFindDagObjectForServer = new CouldNotFindDagObjectForServer(this.ServerToCheck.NetbiosName);
					adError = couldNotFindDagObjectForServer.Message;
				}
			}
			else
			{
				iaddatabaseAvailabilityGroup = AmBestCopySelectionHelper.GetLocalServerDatabaseAvailabilityGroup(out adError);
			}
			if (iaddatabaseAvailabilityGroup == null)
			{
				error = ReplayStrings.AmBcsDagNotFoundInAd(this.ServerToCheck.Fqdn, adError);
				this.ReportServerBlocked(AmBcsServerChecks.DatacenterActivationModeStarted, error);
				return false;
			}
			if (AmBestCopySelectionHelper.IsServerInDacAndStopped(iaddatabaseAvailabilityGroup, this.ServerToCheck))
			{
				AmTrace.Error("AmBcsServerValidation: Rejecting server '{0}' for DB '{1}' since it is stopped in the DAC mode.", new object[]
				{
					this.ServerToCheck,
					this.Database.Name
				});
				error = ReplayStrings.AmBcsTargetServerIsStoppedOnDAC(this.ServerToCheck.Fqdn);
				this.ReportServerBlocked(AmBcsServerChecks.DatacenterActivationModeStarted, error);
				return false;
			}
			return true;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00020C1C File Offset: 0x0001EE1C
		private bool IsAutoActivationAllowed(ref LocalizedString error)
		{
			if (this.ServerToCheck.Equals(this.SourceServer))
			{
				AmTrace.Debug("AmBcsServerValidation: Skipping 'IsAutoActivationAllowed' check since source == target. TargetServer: {0}", new object[]
				{
					this.ServerToCheck
				});
				return true;
			}
			Exception ex;
			IADServer miniServer = AmBestCopySelectionHelper.GetMiniServer(this.ServerToCheck, out ex);
			if (miniServer == null)
			{
				AmTrace.Error("AmBcsServerValidation: Rejecting server '{0}' for DB '{1}' because target MiniServer could not be read.", new object[]
				{
					this.ServerToCheck,
					this.Database.Name
				});
				error = ReplayStrings.AmBcsTargetServerADError(this.ServerToCheck.Fqdn, ex.ToString());
				this.ReportServerBlocked(AmBcsServerChecks.AutoActivationAllowed, error);
				return false;
			}
			IADServer miniServer2 = AmBestCopySelectionHelper.GetMiniServer(this.SourceServer, out ex);
			if (miniServer2 == null)
			{
				AmTrace.Error("AmBcsServerValidation: Rejecting server '{0}' for DB '{1}' because source MiniServer '{2}' could not be read.", new object[]
				{
					this.ServerToCheck,
					this.Database.Name,
					this.SourceServer
				});
				error = ReplayStrings.AmBcsSourceServerADError(this.SourceServer.Fqdn, ex.ToString());
				this.ReportServerBlocked(AmBcsServerChecks.AutoActivationAllowed, error);
				return false;
			}
			if (!AmBestCopySelectionHelper.IsAutoActivationAllowed(miniServer2, miniServer, out error))
			{
				this.ReportServerBlocked(AmBcsServerChecks.AutoActivationAllowed, error);
				return false;
			}
			return true;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00020D4F File Offset: 0x0001EF4F
		private void ReportServerBlocked(AmBcsServerChecks checkFailed, LocalizedString error)
		{
			if (this.ErrorLogger != null)
			{
				this.ErrorLogger.ReportServerFailure(this.ServerToCheck, checkFailed.ToString(), error);
			}
		}

		// Token: 0x040002F7 RID: 759
		internal const string AmBcsCopyStatusRpcCheckName = "CopyStatusRpcCheck";

		// Token: 0x040002F8 RID: 760
		internal const string AmBcsCopyHasBeenTriedCheckName = "CopyHasBeenTriedCheck";

		// Token: 0x040002F9 RID: 761
		internal const AmBcsServerChecks AllChecks = AmBcsServerChecks.DebugOptionDisabled | AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted | AmBcsServerChecks.AutoActivationAllowed;

		// Token: 0x040002FA RID: 762
		internal const AmBcsServerChecks ChecksForAdminMoveWithTargetSpecified = AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted;

		// Token: 0x040002FB RID: 763
		internal const AmBcsServerChecks ChecksForAdminMoveWithTargetAutomaticallyChosen = AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted | AmBcsServerChecks.AutoActivationAllowed;

		// Token: 0x040002FC RID: 764
		internal const AmBcsServerChecks ChecksForAdminMount = AmBcsServerChecks.ClusterNodeUp | AmBcsServerChecks.DatacenterActivationModeStarted;
	}
}
