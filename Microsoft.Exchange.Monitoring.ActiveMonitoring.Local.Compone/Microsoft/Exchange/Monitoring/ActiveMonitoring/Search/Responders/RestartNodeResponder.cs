using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Ceres.CoreServices.Services.HealthCheck;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders
{
	// Token: 0x02000469 RID: 1129
	public class RestartNodeResponder : ResponderWorkItem
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x000A7FAC File Offset: 0x000A61AC
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x000A7FB4 File Offset: 0x000A61B4
		internal string NodeNames { get; set; }

		// Token: 0x06001C9D RID: 7325 RVA: 0x000A7FC0 File Offset: 0x000A61C0
		internal static ResponderDefinition CreateDefinition(string responderName, string alertMask, ServiceHealthStatus responderTargetState, string nodeNames = "", int nodeStopTimeoutInSeconds = 0, string throttleGroupName = null)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = RestartNodeResponder.AssemblyPath;
			responderDefinition.TypeName = RestartNodeResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = ExchangeComponent.Search.Name;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = alertMask;
			responderDefinition.RecurrenceIntervalSeconds = 0;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = true;
			responderDefinition.Attributes["NodeNames"] = ((nodeNames == null) ? string.Empty : nodeNames);
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.RestartFastNode, "HostControllerService", null);
			return responderDefinition;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000A8070 File Offset: 0x000A6270
		internal static List<HealthCheckInfo> GetUnhealthyNodes()
		{
			List<HealthCheckInfo> nodeStates = RestartNodeResponder.GetNodeStates();
			return RestartNodeResponder.GetUnhealthyNodes(nodeStates);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000A808C File Offset: 0x000A628C
		internal static List<HealthCheckInfo> GetUnhealthyNodes(List<HealthCheckInfo> nodeStates)
		{
			List<HealthCheckInfo> list = new List<HealthCheckInfo>();
			foreach (HealthCheckInfo healthCheckInfo in nodeStates)
			{
				if (!NodeManagementClient.Instance.IsNodeHealthy(healthCheckInfo.Name))
				{
					list.Add(healthCheckInfo);
				}
			}
			return list;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000A80F4 File Offset: 0x000A62F4
		internal static List<HealthCheckInfo> GetNodeStates()
		{
			return new List<HealthCheckInfo>(NodeManagementClient.Instance.GetSystemInfo());
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000A8105 File Offset: 0x000A6305
		protected void InitializeServiceAttributes(AttributeHelper attributeHelper)
		{
			this.NodeNames = attributeHelper.GetString("NodeNames", false, string.Empty);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000A8154 File Offset: 0x000A6354
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			SearchMonitoringHelper.LogRecoveryAction(this, "Invoked.", new object[0]);
			Exception ex = null;
			bool flag = false;
			this.InitializeAttributes();
			this.CheckServiceRestartThrottling(cancellationToken);
			List<string> list = null;
			if (!string.IsNullOrWhiteSpace(this.NodeNames))
			{
				list = new List<string>(this.NodeNames.Split(new char[]
				{
					',',
					';'
				}));
			}
			else
			{
				list = new List<string>();
				ProbeResult probeResult = null;
				try
				{
					probeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
				}
				catch (Exception ex2)
				{
					SearchMonitoringHelper.LogRecoveryAction(this, "Caught exception reading last failed probe result: '{0}'.", new object[]
					{
						ex2
					});
				}
				if (probeResult != null && !string.IsNullOrWhiteSpace(probeResult.StateAttribute15))
				{
					string[] array = probeResult.StateAttribute15.Split(new char[]
					{
						',',
						';'
					});
					foreach (string text in array)
					{
						string text2 = text.Trim();
						if (SearchMonitoringHelper.FastNodeNames.IsNodeNameValid(text2))
						{
							SearchMonitoringHelper.LogRecoveryAction(this, "Got unhealthy node '{0}' from last failed probe result.", new object[]
							{
								text2
							});
							list.Add(text2);
						}
					}
				}
				if (list.Count == 0)
				{
					try
					{
						List<HealthCheckInfo> unhealthyNodes = RestartNodeResponder.GetUnhealthyNodes();
						foreach (HealthCheckInfo healthCheckInfo in unhealthyNodes)
						{
							SearchMonitoringHelper.LogRecoveryAction(this, "Detected unhealthy node '{0}' with State: '{1}', Description: '{2}'.", new object[]
							{
								healthCheckInfo.Name,
								healthCheckInfo.State,
								healthCheckInfo.Description
							});
							if (healthCheckInfo.State == null)
							{
								flag = true;
							}
							list.Add(healthCheckInfo.Name);
						}
					}
					catch (PerformingFastOperationException ex3)
					{
						SearchMonitoringHelper.LogRecoveryAction(this, "Exception caught getting unhealthy nodes: '{0}'", new object[]
						{
							ex3
						});
						flag = true;
						list.Add("AdminNode1");
						ex = ex3;
					}
				}
			}
			if (flag)
			{
				try
				{
					SearchMonitoringHelper.CleanUpOrphanedWerProcesses();
				}
				catch (Exception ex4)
				{
					SearchMonitoringHelper.LogRecoveryAction(this, "Exception caught cleaning up orphaned WER processes: '{0}'", new object[]
					{
						ex4
					});
					ex = ex4;
				}
			}
			foreach (string text3 in list)
			{
				RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RestartFastNode, text3, this, true, cancellationToken, null);
				string tmpNodeName = text3;
				try
				{
					recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
					{
						this.InternalRestartNode(tmpNodeName, cancellationToken);
					});
					if (string.IsNullOrEmpty(base.Result.StateAttribute1))
					{
						base.Result.StateAttribute1 = tmpNodeName;
					}
				}
				catch (ThrottlingRejectedOperationException ex5)
				{
					ex = ex5;
					SearchMonitoringHelper.LogRecoveryAction(this, "Restarting '{0}' is throttled.", new object[]
					{
						text3
					});
				}
			}
			if (ex != null)
			{
				SearchMonitoringHelper.LogRecoveryAction(this, "Failed.", new object[0]);
				throw ex;
			}
			SearchMonitoringHelper.LogRecoveryAction(this, "Completed.", new object[0]);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000A84CC File Offset: 0x000A66CC
		protected virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.InitializeServiceAttributes(attributeHelper);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000A84EC File Offset: 0x000A66EC
		private void InternalRestartNode(string nodeName, CancellationToken cancellationToken)
		{
			nodeName = nodeName.Trim();
			if (!SearchMonitoringHelper.FastNodeNames.IsNodeNameValid(nodeName))
			{
				throw new ArgumentException("nodeName");
			}
			SearchMonitoringHelper.LogRecoveryAction(this, "Restarting node '{0}'.", new object[]
			{
				nodeName
			});
			try
			{
				NodeManagementClient.Instance.KillAndRestartNode(nodeName);
			}
			catch (Exception ex)
			{
				SearchMonitoringHelper.LogRecoveryAction(this, "Restarting node '{0}' failed with exception: '{1}'.", new object[]
				{
					nodeName,
					ex
				});
				throw ex;
			}
			SearchMonitoringHelper.LogRecoveryAction(this, "Restarting node '{0}' completed.", new object[]
			{
				nodeName
			});
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000A8580 File Offset: 0x000A6780
		private void CheckServiceRestartThrottling(CancellationToken cancellationToken)
		{
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RestartService, "HostControllerService", this, true, cancellationToken, null);
			try
			{
				recoveryActionRunner.VerifyThrottleLimitsNotExceeded();
			}
			catch (ThrottlingRejectedOperationException)
			{
				SearchMonitoringHelper.LogRecoveryAction(this, "Throttled by Host Controller Service restart.", new object[0]);
				throw;
			}
		}

		// Token: 0x040013B8 RID: 5048
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040013B9 RID: 5049
		private static readonly string TypeName = typeof(RestartNodeResponder).FullName;

		// Token: 0x0200046A RID: 1130
		internal static class AttributeNames
		{
			// Token: 0x040013BB RID: 5051
			internal const string NodeNames = "NodeNames";

			// Token: 0x040013BC RID: 5052
			internal const string NodeStopTimeoutSeconds = "NodeStopTimeoutSeconds";

			// Token: 0x040013BD RID: 5053
			internal const string throttleGroupName = "throttleGroupName";
		}

		// Token: 0x0200046B RID: 1131
		internal static class DefaultValues
		{
			// Token: 0x040013BE RID: 5054
			internal const string NodeNames = "";
		}
	}
}
