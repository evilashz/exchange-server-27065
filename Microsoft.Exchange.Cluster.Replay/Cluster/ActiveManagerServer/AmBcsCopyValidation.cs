using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A2 RID: 162
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmBcsCopyValidation
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001F654 File Offset: 0x0001D854
		public AmBcsCopyValidation(Guid dbGuid, string dbName, AmBcsChecks checksToRun, AmServerName sourceServer, AmServerName targetServer, RpcDatabaseCopyStatus2 copyStatus, IAmBcsErrorLogger errorLogger, AmBcsSkipFlags skipValidationChecks) : this(dbGuid, dbName, checksToRun, sourceServer, targetServer, copyStatus, errorLogger, skipValidationChecks, null)
		{
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001F678 File Offset: 0x0001D878
		public AmBcsCopyValidation(Guid dbGuid, string dbName, AmBcsChecks checksToRun, AmServerName sourceServer, AmServerName targetServer, RpcDatabaseCopyStatus2 copyStatus, IAmBcsErrorLogger errorLogger, AmBcsSkipFlags skipValidationChecks, ComponentStateWrapper csw)
		{
			this.DbGuid = dbGuid;
			this.DbName = dbName;
			this.ChecksToRun = checksToRun;
			this.SourceServer = sourceServer;
			this.TargetServer = targetServer;
			this.CopyStatus = copyStatus;
			this.ComponentStateWrapper = csw;
			this.ErrorLogger = errorLogger;
			this.SkipValidationChecks = skipValidationChecks;
			AmTrace.Debug("AmBcsCopyValidation: Constructed with SkipValidationChecks='{0}'", new object[]
			{
				skipValidationChecks
			});
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		private Guid DbGuid { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001F6FD File Offset: 0x0001D8FD
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001F705 File Offset: 0x0001D905
		private string DbName { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001F70E File Offset: 0x0001D90E
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001F716 File Offset: 0x0001D916
		private AmServerName SourceServer { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001F71F File Offset: 0x0001D91F
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001F727 File Offset: 0x0001D927
		private AmServerName TargetServer { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001F730 File Offset: 0x0001D930
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001F738 File Offset: 0x0001D938
		private RpcDatabaseCopyStatus2 CopyStatus { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001F741 File Offset: 0x0001D941
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001F749 File Offset: 0x0001D949
		private ComponentStateWrapper ComponentStateWrapper { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001F752 File Offset: 0x0001D952
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001F75A File Offset: 0x0001D95A
		private AmBcsSkipFlags SkipValidationChecks { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001F763 File Offset: 0x0001D963
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001F76B File Offset: 0x0001D96B
		private AmBcsChecks ChecksToRun { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001F774 File Offset: 0x0001D974
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001F77C File Offset: 0x0001D97C
		public AmBcsChecks CompletedChecks { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001F785 File Offset: 0x0001D985
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0001F78D File Offset: 0x0001D98D
		private IAmBcsErrorLogger ErrorLogger { get; set; }

		// Token: 0x06000682 RID: 1666 RVA: 0x0001F798 File Offset: 0x0001D998
		public static bool IsHealthyOrDisconnected(string dbName, RpcDatabaseCopyStatus2 status, AmServerName targetServer, ref LocalizedString error)
		{
			bool result = true;
			if (status.HAComponentOffline)
			{
				result = false;
				error = ReplayStrings.AmBcsDatabaseCopyIsHAComponentOffline(dbName, targetServer.NetbiosName);
			}
			else if (status.ActivationSuspended && status.CopyStatus != CopyStatusEnum.Suspended && status.CopyStatus != CopyStatusEnum.FailedAndSuspended && status.CopyStatus != CopyStatusEnum.Seeding)
			{
				result = false;
				error = ReplayStrings.AmBcsDatabaseCopyActivationSuspended(dbName, targetServer.NetbiosName, string.IsNullOrEmpty(status.SuspendComment) ? ReplayStrings.AmBcsNoneSpecified : status.SuspendComment);
			}
			else if (status.CopyStatus == CopyStatusEnum.Seeding)
			{
				result = false;
				error = ReplayStrings.AmBcsDatabaseCopySeeding(dbName, targetServer.NetbiosName);
			}
			else if (status.CopyStatus == CopyStatusEnum.Failed || status.CopyStatus == CopyStatusEnum.FailedAndSuspended)
			{
				result = false;
				error = ReplayStrings.AmBcsDatabaseCopyFailed(dbName, targetServer.NetbiosName, string.IsNullOrEmpty(status.ErrorMessage) ? ReplayStrings.AmBcsNoneSpecified : status.ErrorMessage);
			}
			else if (status.CopyStatus == CopyStatusEnum.Suspended)
			{
				result = false;
				error = ReplayStrings.AmBcsDatabaseCopySuspended(dbName, targetServer.NetbiosName, string.IsNullOrEmpty(status.SuspendComment) ? ReplayStrings.AmBcsNoneSpecified : status.SuspendComment);
			}
			return result;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001F8CC File Offset: 0x0001DACC
		public static bool IsTotalQueueLengthLessThanMaxThreshold(string dbName, RpcDatabaseCopyStatus2 status, AmServerName targetServer, ref LocalizedString error)
		{
			long num = Math.Max(0L, status.LastLogGenerated - status.LastLogReplayed);
			bool flag = num <= (long)AmBcsCopyValidation.TOTAL_QUEUE_MAX_THRESHOLD;
			if (!flag)
			{
				error = ReplayStrings.AmBcsDatabaseCopyTotalQueueLengthTooHigh(dbName, targetServer.NetbiosName, num, (long)AmBcsCopyValidation.TOTAL_QUEUE_MAX_THRESHOLD);
			}
			return flag;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001F918 File Offset: 0x0001DB18
		public static bool IsRealCopyQueueLengthAcceptable(string dbName, RpcDatabaseCopyStatus2 status, int copyQueueThreshold, AmServerName targetServer, ref LocalizedString error)
		{
			long num = Math.Max(0L, status.LastLogGenerated - status.LastLogCopied);
			bool flag = num <= (long)copyQueueThreshold;
			if (!flag)
			{
				error = ReplayStrings.AmBcsDatabaseCopyQueueLengthTooHigh(dbName, targetServer.NetbiosName, num, (long)copyQueueThreshold);
			}
			return flag;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001F960 File Offset: 0x0001DB60
		public bool RunChecks(ref LocalizedString error)
		{
			bool flag = true;
			AmBcsChecks checksToRun = this.ChecksToRun;
			error = LocalizedString.Empty;
			this.CompletedChecks = AmBcsChecks.None;
			if (flag && this.ShouldRunCheck(AmBcsChecks.IsPassiveCopy))
			{
				this.CompletedChecks |= AmBcsChecks.IsPassiveCopy;
				flag = this.IsPassiveCopy(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.ActivationEnabled))
			{
				this.CompletedChecks |= AmBcsChecks.ActivationEnabled;
				flag = this.IsActivationEnabled(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.MaxActivesUnderHighestLimit))
			{
				this.CompletedChecks |= AmBcsChecks.MaxActivesUnderHighestLimit;
				flag = this.IsMaxActivesUnderHighestLimit(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.IsHealthyOrDisconnected))
			{
				this.CompletedChecks |= AmBcsChecks.IsHealthyOrDisconnected;
				flag = this.IsHealthyOrDisconnected(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.TotalQueueLengthMaxAllowed))
			{
				this.CompletedChecks |= AmBcsChecks.TotalQueueLengthMaxAllowed;
				flag = this.IsTotalQueueLengthLessThanMaxThreshold(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.MaxActivesUnderPreferredLimit))
			{
				this.CompletedChecks |= AmBcsChecks.MaxActivesUnderPreferredLimit;
				flag = this.IsMaxActivesUnderPreferredLimit(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.CopyQueueLength))
			{
				this.CompletedChecks |= AmBcsChecks.CopyQueueLength;
				flag = this.IsCopyQueueLengthAcceptable(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.ReplayQueueLength))
			{
				this.CompletedChecks |= AmBcsChecks.ReplayQueueLength;
				flag = this.IsReplayQueueLengthAcceptable(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.IsSeedingSource))
			{
				this.CompletedChecks |= AmBcsChecks.IsSeedingSource;
				flag = this.IsPassiveSeedingSource(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.IsCatalogStatusHealthy))
			{
				this.CompletedChecks |= AmBcsChecks.IsCatalogStatusHealthy;
				flag = this.IsCatalogStatusHealthy(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.IsCatalogStatusCrawling))
			{
				this.CompletedChecks |= AmBcsChecks.IsCatalogStatusCrawling;
				flag = this.IsCatalogStatusCrawling(ref error);
			}
			if (flag && this.ShouldRunManagedAvailabilityChecks())
			{
				AmBcsChecks amBcsChecks;
				flag = this.IsManagedAvailabilityChecksSucceeded(ref error, out amBcsChecks);
				this.CompletedChecks |= amBcsChecks;
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.MaxActivesUnderHighestLimit))
			{
				this.CompletedChecks |= AmBcsChecks.MaxActivesUnderHighestLimit;
				flag = this.UpdateActiveIfMaxActivesNotExceededHighestLimit(ref error);
			}
			if (flag && this.ShouldRunCheck(AmBcsChecks.MaxActivesUnderPreferredLimit))
			{
				this.CompletedChecks |= AmBcsChecks.MaxActivesUnderPreferredLimit;
				flag = this.UpdateActiveIfMaxActivesNotExceededPreferredLimit(ref error);
			}
			return flag;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		internal bool ShouldRunCheck(AmBcsChecks checkInQuestion)
		{
			bool flag = (this.ChecksToRun & checkInQuestion) == checkInQuestion;
			if (checkInQuestion == AmBcsChecks.IsPassiveCopy)
			{
				return flag;
			}
			return flag && !this.ShouldBeSkipped(checkInQuestion);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		internal bool ShouldRunManagedAvailabilityChecks()
		{
			AmBcsChecks amBcsChecks = AmBcsChecks.ManagedAvailabilityInitiatorBetterThanSource | AmBcsChecks.ManagedAvailabilityAllHealthy | AmBcsChecks.ManagedAvailabilityUptoNormalHealthy | AmBcsChecks.ManagedAvailabilityAllBetterThanSource | AmBcsChecks.ManagedAvailabilitySameAsSource;
			return (this.ChecksToRun & amBcsChecks) > AmBcsChecks.None;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		private bool ShouldBeSkipped(AmBcsChecks checkInQuestion)
		{
			if ((checkInQuestion & (AmBcsChecks)RegistryParameters.BcsCheckToDisable) == checkInQuestion)
			{
				AmTrace.Info("BCS Check {0} skipped since registry parameters is configured to skip it. (Reg.BcsCheckToDisable={1})", new object[]
				{
					checkInQuestion,
					RegistryParameters.BcsCheckToDisable
				});
				return true;
			}
			if (this.IsSkipFlagSpecified(AmBcsSkipFlags.LegacySkipAllChecks))
			{
				return true;
			}
			bool flag = false;
			if (!flag && this.IsSkipFlagSpecified(AmBcsSkipFlags.SkipClientExperienceChecks))
			{
				flag = this.IsCheckInSkippedList(AmBcsSkippedCheckDefinitions.SkipClientExperienceChecks, checkInQuestion);
			}
			if (!flag && this.IsSkipFlagSpecified(AmBcsSkipFlags.SkipHealthChecks))
			{
				flag = this.IsCheckInSkippedList(AmBcsSkippedCheckDefinitions.SkipHealthChecks, checkInQuestion);
			}
			if (!flag && this.IsSkipFlagSpecified(AmBcsSkipFlags.SkipLagChecks))
			{
				flag = this.IsCheckInSkippedList(AmBcsSkippedCheckDefinitions.SkipLagChecks, checkInQuestion);
			}
			if (!flag && this.IsSkipFlagSpecified(AmBcsSkipFlags.SkipMaximumActiveDatabasesChecks))
			{
				flag = this.IsCheckInSkippedList(AmBcsSkippedCheckDefinitions.SkipMaximumActiveDatabasesChecks, checkInQuestion);
			}
			return flag;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		private bool IsSkipFlagSpecified(AmBcsSkipFlags flagInQuestion)
		{
			return (this.SkipValidationChecks & flagInQuestion) == flagInQuestion;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001FCC8 File Offset: 0x0001DEC8
		private bool IsCheckInSkippedList(IEnumerable<AmBcsChecks> checks, AmBcsChecks checkInQuestion)
		{
			return checks.Any((AmBcsChecks check) => (checkInQuestion & check) == check);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001FCF6 File Offset: 0x0001DEF6
		private void ReportCopyStatusFailure(AmBcsChecks checkThatFailed, LocalizedString error)
		{
			this.ErrorLogger.ReportCopyStatusFailure(this.TargetServer, checkThatFailed.ToString(), this.ChecksToRun.ToString(), error);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		private bool IsPassiveCopy(ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			bool flag = AmServerName.IsEqual(this.SourceServer, this.TargetServer);
			if (!flag && copyStatus != null && (copyStatus.CopyStatus == CopyStatusEnum.Mounted || copyStatus.CopyStatus == CopyStatusEnum.Mounting || copyStatus.CopyStatus == CopyStatusEnum.Dismounted || copyStatus.CopyStatus == CopyStatusEnum.Dismounting))
			{
				AmTrace.Error("IsPassiveCopy: Copy status for DB '{0}' has active copy status, but fActive is false! CopyStatus = '{1}'. Changing fActive to 'true'.", new object[]
				{
					this.DbName,
					copyStatus.CopyStatus
				});
				flag = true;
			}
			if (flag)
			{
				error = ReplayStrings.AmBcsDatabaseCopyHostedOnTarget(this.DbName, this.TargetServer.NetbiosName);
				this.ReportCopyStatusFailure(AmBcsChecks.IsPassiveCopy, error);
			}
			return !flag;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
		private bool IsHealthyOrDisconnected(ref LocalizedString error)
		{
			string dbName = this.DbName;
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			AmServerName targetServer = this.TargetServer;
			bool flag = AmBcsCopyValidation.IsHealthyOrDisconnected(dbName, copyStatus, targetServer, ref error);
			if (!flag)
			{
				this.ReportCopyStatusFailure(AmBcsChecks.IsHealthyOrDisconnected, error);
			}
			return flag;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001FE24 File Offset: 0x0001E024
		private bool IsCatalogStatusHealthy(ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			bool flag = copyStatus.ContentIndexStatus == ContentIndexStatusType.Healthy || copyStatus.ContentIndexStatus == ContentIndexStatusType.HealthyAndUpgrading;
			if (!flag)
			{
				error = ReplayStrings.AmBcsDatabaseCopyCatalogUnhealthy(this.DbName, this.TargetServer.NetbiosName, copyStatus.ContentIndexStatus.ToString());
				this.ReportCopyStatusFailure(AmBcsChecks.IsCatalogStatusHealthy, error);
			}
			return flag;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001FE8C File Offset: 0x0001E08C
		private bool IsCopyQueueLengthAcceptable(ref LocalizedString error)
		{
			string dbName = this.DbName;
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			AmServerName targetServer = this.TargetServer;
			bool flag = AmBcsCopyValidation.IsRealCopyQueueLengthAcceptable(dbName, copyStatus, 10, targetServer, ref error);
			if (!flag)
			{
				this.ReportCopyStatusFailure(AmBcsChecks.CopyQueueLength, error);
			}
			return flag;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001FECC File Offset: 0x0001E0CC
		private bool IsReplayQueueLengthAcceptable(ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			long num = Math.Max(0L, copyStatus.LastLogCopied - copyStatus.LastLogReplayed);
			bool flag = num <= 10L;
			if (!flag)
			{
				error = ReplayStrings.AmBcsDatabaseCopyReplayQueueLengthTooHigh(this.DbName, this.TargetServer.NetbiosName, num, 10L);
				this.ReportCopyStatusFailure(AmBcsChecks.ReplayQueueLength, error);
			}
			return flag;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001FF30 File Offset: 0x0001E130
		private bool IsTotalQueueLengthLessThanMaxThreshold(ref LocalizedString error)
		{
			string dbName = this.DbName;
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			AmServerName targetServer = this.TargetServer;
			bool flag = AmBcsCopyValidation.IsTotalQueueLengthLessThanMaxThreshold(dbName, copyStatus, targetServer, ref error);
			if (!flag)
			{
				this.ReportCopyStatusFailure(AmBcsChecks.TotalQueueLengthMaxAllowed, error);
			}
			return flag;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001FF74 File Offset: 0x0001E174
		private bool IsPassiveSeedingSource(ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			bool flag = copyStatus.CopyStatus != CopyStatusEnum.SeedingSource;
			if (!flag)
			{
				error = ReplayStrings.AmBcsDatabaseCopyIsSeedingSource(this.DbName, this.TargetServer.NetbiosName);
				this.ReportCopyStatusFailure(AmBcsChecks.IsSeedingSource, error);
			}
			return flag;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
		private bool IsCatalogStatusCrawling(ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = this.CopyStatus;
			AmServerName targetServer = this.TargetServer;
			LocalizedString localizedString = LocalizedString.Empty;
			bool flag = this.IsCatalogStatusHealthy(ref localizedString);
			if (!flag)
			{
				flag = (copyStatus.ContentIndexStatus == ContentIndexStatusType.Crawling);
				if (!flag)
				{
					localizedString = ReplayStrings.AmBcsDatabaseCopyCatalogUnhealthy(this.DbName, targetServer.NetbiosName, copyStatus.ContentIndexStatus.ToString());
				}
			}
			if (!flag)
			{
				error = localizedString;
				this.ReportCopyStatusFailure(AmBcsChecks.IsCatalogStatusCrawling, error);
			}
			return flag;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00020039 File Offset: 0x0001E239
		private bool IsActivationEnabled(ref LocalizedString error)
		{
			if (AmBestCopySelectionHelper.IsActivationDisabled(this.TargetServer))
			{
				error = ReplayStrings.AmBcsTargetServerActivationDisabled(this.TargetServer.Fqdn);
				this.ReportCopyStatusFailure(AmBcsChecks.ActivationEnabled, error);
				return false;
			}
			return true;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00020074 File Offset: 0x0001E274
		internal bool IsManagedAvailabilityChecksSucceeded(ref LocalizedString error, out AmBcsChecks completedChecks)
		{
			bool flag = true;
			completedChecks = AmBcsChecks.None;
			List<string> list = new List<string>();
			if (this.ComponentStateWrapper == null)
			{
				return true;
			}
			if (this.ShouldRunCheck(AmBcsChecks.ManagedAvailabilityInitiatorBetterThanSource))
			{
				completedChecks |= AmBcsChecks.ManagedAvailabilityInitiatorBetterThanSource;
				flag = this.ComponentStateWrapper.IsInitiatorComponentBetterThanSource(this.TargetServer, list);
				if (!flag && list.Count > 0)
				{
					string failures = string.Join(",", list.ToArray());
					error = ReplayStrings.AmBcsManagedAvailabilityCheckFailed(this.SourceServer.NetbiosName, this.TargetServer.NetbiosName, this.ComponentStateWrapper.InitiatingComponentName, failures);
					this.ReportCopyStatusFailure(AmBcsChecks.ManagedAvailabilityInitiatorBetterThanSource, error);
				}
			}
			if (flag)
			{
				list.Clear();
				AmBcsChecks amBcsChecks = AmBcsChecks.None;
				if (this.ShouldRunCheck(AmBcsChecks.ManagedAvailabilityAllHealthy))
				{
					amBcsChecks = AmBcsChecks.ManagedAvailabilityAllHealthy;
					flag = this.ComponentStateWrapper.IsAllComponentsHealthy(this.TargetServer, list);
				}
				else if (this.ShouldRunCheck(AmBcsChecks.ManagedAvailabilityUptoNormalHealthy))
				{
					amBcsChecks = AmBcsChecks.ManagedAvailabilityUptoNormalHealthy;
					flag = this.ComponentStateWrapper.IsUptoNormalComponentsHealthy(this.TargetServer, list);
				}
				else if (this.ShouldRunCheck(AmBcsChecks.ManagedAvailabilityAllBetterThanSource))
				{
					amBcsChecks = AmBcsChecks.ManagedAvailabilityAllBetterThanSource;
					flag = this.ComponentStateWrapper.IsComponentsBettterThanSource(this.TargetServer, list);
				}
				else if (this.ShouldRunCheck(AmBcsChecks.ManagedAvailabilitySameAsSource))
				{
					amBcsChecks = AmBcsChecks.ManagedAvailabilitySameAsSource;
					flag = this.ComponentStateWrapper.IsComponentsAtleastSameAsSource(this.TargetServer, list);
				}
				if (flag)
				{
					completedChecks |= amBcsChecks;
				}
				else if (list.Count > 0)
				{
					string failures2 = string.Join(",", list.ToArray());
					error = ReplayStrings.AmBcsManagedAvailabilityCheckFailed(this.SourceServer.NetbiosName, this.TargetServer.NetbiosName, this.ComponentStateWrapper.InitiatingComponentName, failures2);
					this.ReportCopyStatusFailure(amBcsChecks, error);
				}
			}
			return flag;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002022C File Offset: 0x0001E42C
		internal bool IsMaxActivesUnderHighestLimit(ref LocalizedString error)
		{
			int? num;
			if (!AmBestCopySelectionHelper.IsMaxActivesUnderHighestLimit(this.TargetServer, out num))
			{
				error = ReplayStrings.AmBcsTargetServerMaxActivesReached(this.TargetServer.Fqdn, (num != null) ? num.Value.ToString() : "<null>");
				this.ReportCopyStatusFailure(AmBcsChecks.MaxActivesUnderHighestLimit, error);
				return false;
			}
			return true;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00020294 File Offset: 0x0001E494
		internal bool IsMaxActivesUnderPreferredLimit(ref LocalizedString error)
		{
			int? num;
			if (!AmBestCopySelectionHelper.IsMaxActivesUnderPreferredLimit(this.TargetServer, out num))
			{
				error = ReplayStrings.AmBcsTargetServerPreferredMaxActivesReached(this.TargetServer.Fqdn, (num != null) ? num.Value.ToString() : "<null>");
				this.ReportCopyStatusFailure(AmBcsChecks.MaxActivesUnderPreferredLimit, error);
				return false;
			}
			return true;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00020304 File Offset: 0x0001E504
		internal bool UpdateActiveIfMaxActivesNotExceededHighestLimit(ref LocalizedString error)
		{
			int? num;
			if (!AmBestCopySelectionHelper.UpdateActiveIfMaxActivesNotExceeded(this.DbGuid, this.TargetServer, (IADServer server) => server.MaximumActiveDatabases, out num))
			{
				error = ReplayStrings.AmBcsTargetServerMaxActivesReached(this.TargetServer.Fqdn, (num != null) ? num.Value.ToString() : "<null>");
				this.ReportCopyStatusFailure(AmBcsChecks.MaxActivesUnderHighestLimit, error);
				return false;
			}
			return true;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00020394 File Offset: 0x0001E594
		internal bool UpdateActiveIfMaxActivesNotExceededPreferredLimit(ref LocalizedString error)
		{
			int? num;
			if (!AmBestCopySelectionHelper.UpdateActiveIfMaxActivesNotExceeded(this.DbGuid, this.TargetServer, (IADServer server) => server.MaximumPreferredActiveDatabases, out num))
			{
				error = ReplayStrings.AmBcsTargetServerPreferredMaxActivesReached(this.TargetServer.Fqdn, (num != null) ? num.Value.ToString() : "<null>");
				this.ReportCopyStatusFailure(AmBcsChecks.MaxActivesUnderPreferredLimit, error);
				return false;
			}
			return true;
		}

		// Token: 0x040002D9 RID: 729
		private const int COPY_QUEUE_THRESHOLD = 10;

		// Token: 0x040002DA RID: 730
		private const int REPLAY_QUEUE_THRESHOLD = 10;

		// Token: 0x040002DB RID: 731
		internal static readonly int TOTAL_QUEUE_MAX_THRESHOLD = RegistryParameters.BcsTotalQueueMaxThreshold;
	}
}
