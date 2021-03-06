using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200005C RID: 92
	internal class AmPamMonitor : TimerComponent
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x000156A9 File Offset: 0x000138A9
		internal AmPamMonitor(TimeSpan checkPeriod) : base(checkPeriod, checkPeriod, "AmPamMonitor")
		{
			AmTrace.Entering("Constructor of AmPamMonitor", new object[0]);
			this.checkPeriod = checkPeriod;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000156CF File Offset: 0x000138CF
		internal AmPamMonitor() : this(TimeSpan.FromSeconds((double)RegistryParameters.PamMonitorCheckPeriodInSec))
		{
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000156E4 File Offset: 0x000138E4
		protected override void TimerCallbackInternal()
		{
			AmTrace.Entering("AmPamMonitor.TimerCallbackInternal", new object[0]);
			switch (this.phase)
			{
			case AmPamMonitor.PamMonitorPhase.Verification:
				this.PerformVerification();
				break;
			case AmPamMonitor.PamMonitorPhase.Recovery:
				this.PerformRecovery();
				break;
			}
			AmTrace.Leaving("AmPamMonitor.TimerCallbackInternal", new object[0]);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00015764 File Offset: 0x00013964
		private void PerformVerification()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsSAM)
			{
				AmServerName pam = config.DagConfig.CurrentPAM;
				AmRole role = AmRole.Unknown;
				string errorMessage = null;
				Exception ex = null;
				bool flag = false;
				if (this.IsServerDisabled(AmServerName.LocalComputerName))
				{
					AmTrace.Debug("PamMonitor.PerformVerification found local server marked ActivationDisabled", new object[0]);
					return;
				}
				IADServer server = Dependencies.ADConfig.GetServer(pam);
				if (server == null)
				{
					AmTrace.Error("PamMonitor.PerformVerification found pam on {0} but no server data in ADConfig", new object[]
					{
						pam.Fqdn
					});
					return;
				}
				if (this.IsServerDisabled(server))
				{
					AmTrace.Debug("PamMonitor.PerformVerification found PAM server marked ActivationDisabled", new object[0]);
					flag = true;
				}
				if (!flag)
				{
					ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						role = Dependencies.AmRpcClientWrapper.GetActiveManagerRole(pam.Fqdn, out errorMessage);
					});
				}
				if (flag || role != AmRole.PAM)
				{
					string text = string.Empty;
					int num = RegistryParameters.PamMonitorRecoveryDurationInSec;
					if (flag)
					{
						errorMessage = "PAM has been marked ActivationDisabled";
						num = 5;
					}
					else if (ex != null)
					{
						text = ex.Message;
					}
					ReplayCrimsonEvents.PamMonitorServerNotInPamRole.Log<AmServerName, AmRole, string, string>(pam, role, errorMessage, text);
					this.pamServerInVerificationPhase = pam;
					this.phase = AmPamMonitor.PamMonitorPhase.Recovery;
					TimeSpan timeSpan = TimeSpan.FromSeconds((double)num);
					base.ChangeTimer(timeSpan, timeSpan);
				}
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000158D4 File Offset: 0x00013AD4
		private void PerformRecovery()
		{
			try
			{
				Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					this.PerformRecoveryInternal();
				});
				if (ex != null)
				{
					ReplayCrimsonEvents.PamMonitorEncounteredException.Log<string, string>(ex.Message, "Recovery");
				}
			}
			finally
			{
				this.phase = AmPamMonitor.PamMonitorPhase.Verification;
				base.ChangeTimer(this.checkPeriod, this.checkPeriod);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001596C File Offset: 0x00013B6C
		private void PerformRecoveryInternal()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsSAM)
			{
				AmServerName currentPAM = config.DagConfig.CurrentPAM;
				if (!AmServerName.IsEqual(this.pamServerInVerificationPhase, currentPAM))
				{
					ReplayCrimsonEvents.PamMonitorMoveSkippedSinceServerChanged.Log<AmServerName, AmServerName>(this.pamServerInVerificationPhase, currentPAM);
					return;
				}
				List<AmServerName> list = config.DagConfig.MemberServers.ToList<AmServerName>();
				AmMultiNodeRoleFetcher amMultiNodeRoleFetcher = new AmMultiNodeRoleFetcher(list, TimeSpan.FromSeconds((double)RegistryParameters.PamMonitorRoleCheckTimeoutInSec), false);
				amMultiNodeRoleFetcher.Run();
				Dictionary<AmServerName, AmRole> roleMap = amMultiNodeRoleFetcher.RoleMap;
				string text = string.Join("\n", (from kvp in roleMap
				select string.Format("[{0}] => {1}", kvp.Key, kvp.Value)).ToArray<string>());
				ReplayCrimsonEvents.PamMonitorServerRolesView.Log<string>(text);
				int count = list.Count;
				int num = roleMap.Count((KeyValuePair<AmServerName, AmRole> kvp) => kvp.Value == AmRole.SAM);
				int num2 = count / 2 + 1;
				if (num < num2)
				{
					ReplayCrimsonEvents.PamMonitorMoveCancelledSinceMinimumRequiredNotMet.Log<int, int, int>(count, num, num2);
					return;
				}
				List<AmServerName> list2 = new List<AmServerName>(roleMap.Count);
				List<AmServerName> list3 = new List<AmServerName>(roleMap.Count);
				foreach (KeyValuePair<AmServerName, AmRole> keyValuePair in roleMap)
				{
					if (!this.IsServerDisabled(keyValuePair.Key))
					{
						if (keyValuePair.Value == AmRole.PAM)
						{
							ReplayCrimsonEvents.PamMonitorMoveSkippedDueToValidResponseFromPAM.Log<AmServerName>(keyValuePair.Key);
							return;
						}
						if (keyValuePair.Value == AmRole.SAM)
						{
							if (keyValuePair.Key.CompareTo(currentPAM) > 0)
							{
								list2.Add(keyValuePair.Key);
							}
							else
							{
								list3.Add(keyValuePair.Key);
							}
						}
					}
				}
				list2.Sort();
				list3.Sort();
				AmServerName[] array = new AmServerName[list2.Count + list3.Count];
				list2.CopyTo(array);
				list3.CopyTo(array, list2.Count);
				bool flag = false;
				foreach (AmServerName amServerName in array)
				{
					if (flag)
					{
						Thread.Sleep(TimeSpan.FromSeconds(5.0));
						currentPAM = config.DagConfig.CurrentPAM;
						if (!AmServerName.IsEqual(this.pamServerInVerificationPhase, currentPAM))
						{
							ReplayCrimsonEvents.PamMonitorMoveSkippedSinceServerChanged.Log<AmServerName, AmServerName>(this.pamServerInVerificationPhase, currentPAM);
							return;
						}
					}
					try
					{
						ReplayCrimsonEvents.PamMonitorBeginsToMovePAM.Log<AmServerName, AmServerName>(this.pamServerInVerificationPhase, amServerName);
						AmClusterGroup.MoveClusterGroupWithTimeout(AmServerName.LocalComputerName, amServerName, TimeSpan.FromSeconds((double)RegistryParameters.PamMonitorMoveClusterGroupTimeout));
						ReplayCrimsonEvents.PamMonitorSuccessfulyMovedPAM.Log<AmServerName, AmServerName>(this.pamServerInVerificationPhase, amServerName);
						return;
					}
					catch (ClusterException ex)
					{
						flag = true;
						AmTrace.Error("PAM election failed for {0} : {1}", new object[]
						{
							amServerName,
							ex
						});
						ReplayCrimsonEvents.PamMonitorEncounteredException.Log<string, string>(ex.Message, "MoveClusterGroup");
					}
				}
				ReplayCrimsonEvents.PamMonitorCouldNotFindValidServerToMovePAM.Log();
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00015C78 File Offset: 0x00013E78
		private bool IsServerDisabled(AmServerName serverName)
		{
			IADServer server = Dependencies.ADConfig.GetServer(serverName);
			return this.IsServerDisabled(server);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00015C98 File Offset: 0x00013E98
		private bool IsServerDisabled(IADServer server)
		{
			bool result = false;
			if (server == null || server.DatabaseCopyAutoActivationPolicy == DatabaseCopyAutoActivationPolicyType.Blocked || server.DatabaseCopyActivationDisabledAndMoveNow)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x040001CA RID: 458
		private readonly TimeSpan checkPeriod;

		// Token: 0x040001CB RID: 459
		private AmPamMonitor.PamMonitorPhase phase;

		// Token: 0x040001CC RID: 460
		private AmServerName pamServerInVerificationPhase;

		// Token: 0x0200005D RID: 93
		internal enum PamMonitorPhase
		{
			// Token: 0x040001D0 RID: 464
			Verification,
			// Token: 0x040001D1 RID: 465
			Recovery
		}
	}
}
