using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001A4 RID: 420
	public abstract class ReplicationHealthChecksProbeBase : ProbeWorkItem
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0004E6EA File Offset: 0x0004C8EA
		private string ServerName
		{
			get
			{
				return Environment.MachineName;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0004E6F1 File Offset: 0x0004C8F1
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0004E6F9 File Offset: 0x0004C8F9
		private IADServer ServerObj
		{
			get
			{
				return this.serverObj;
			}
			set
			{
				this.serverObj = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0004E702 File Offset: 0x0004C902
		private string MomEventSource
		{
			get
			{
				return "ActiveMonitoringProbe";
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0004E709 File Offset: 0x0004C909
		private IEventManager EventManager
		{
			get
			{
				return new ReplicationEventManager();
			}
		}

		// Token: 0x06000C1B RID: 3099
		protected abstract Type GetCheckType();

		// Token: 0x06000C1C RID: 3100 RVA: 0x0004E710 File Offset: 0x0004C910
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (HighAvailabilityUtility.CheckCancellationRequested(cancellationToken))
			{
				base.Result.StateAttribute1 = "Cancellation Requested!";
				return;
			}
			this.InitializeEnvironmentForReplicationCheck();
			this.RunReplicationCheck(this.GetCheckType());
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0004E73D File Offset: 0x0004C93D
		private static bool AreConfigBitsSet(ServerConfig configuration, ServerConfig comparisonBits)
		{
			if (comparisonBits == ServerConfig.Unknown)
			{
				throw new ArgumentException("comparisonBits cannot be Unknown.", "comparisonBits");
			}
			return (configuration & comparisonBits) == comparisonBits;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0004E758 File Offset: 0x0004C958
		private void InitializeEnvironmentForReplicationCheck()
		{
			this.ServerObj = CachedAdReader.Instance.LocalServer;
			ReplicationCheckGlobals.Server = this.ServerObj;
			this.BuildServerConfiguration(ReplicationCheckGlobals.Server);
			ReplicationCheckGlobals.ServerConfiguration = this.serverConfigBitfield;
			ReplicationCheckGlobals.ActiveManagerCheckHasRun = false;
			ReplicationCheckGlobals.ReplayServiceCheckHasRun = false;
			ReplicationCheckGlobals.TasksRpcListenerCheckHasRun = false;
			ReplicationCheckGlobals.TcpListenerCheckHasRun = false;
			ReplicationCheckGlobals.ThirdPartyReplCheckHasRun = false;
			ReplicationCheckGlobals.ServerLocatorServiceCheckHasRun = false;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0004E7BC File Offset: 0x0004C9BC
		private void RunReplicationCheck(Type checkType)
		{
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: RunReplicationCheck(): Instantiating Object {0}.", checkType.FullName, null, "RunReplicationCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 181);
			using (ReplicationCheck replicationCheck = (typeof(DagMemberCheck).IsAssignableFrom(checkType) || checkType == typeof(TcpListenerCheck)) ? ((ReplicationCheck)Activator.CreateInstance(checkType, new object[]
			{
				this.ServerName,
				this.EventManager,
				this.MomEventSource,
				CachedAdReader.Instance.LocalDAG
			})) : ((ReplicationCheck)Activator.CreateInstance(checkType, new object[]
			{
				this.ServerName,
				this.EventManager,
				this.MomEventSource
			})))
			{
				Exception ex = null;
				try
				{
					WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: RunReplicationCheck(): Invoke Run() for {0}.", checkType.FullName, null, "RunReplicationCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 207);
					replicationCheck.Run();
					WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: RunReplicationCheck(): Run() for {0} finished without any Exception logged.", checkType.FullName, null, "RunReplicationCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 215);
				}
				catch (Exception ex2)
				{
					WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: RunReplicationCheck(): Run() for {0} threw Exception - {1}.", checkType.FullName, ex2.ToString(), null, "RunReplicationCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 223);
					if (!(ex2 is ReplicationCheckHighPriorityFailedException) && !(ex2 is ReplicationCheckFailedException))
					{
						throw ex2;
					}
					ex = ex2;
				}
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				if (ReplicationHealthChecksProbeBase.DagMemberOnlyChecksTypeNames.Contains(checkType.FullName) && !this.AreConfigBitsSet(ServerConfig.DagMember))
				{
					flag = true;
					stringBuilder.AppendFormat("Suppressed Check '{0}' due to {1} not a DAG Member!{2}", checkType.FullName, this.ServerName, Environment.NewLine);
				}
				else
				{
					if (ex != null)
					{
						flag = false;
						stringBuilder.AppendFormat("Check '{0}' thrown an Exception!{1}Exception - {2}{1}", checkType.FullName, Environment.NewLine, ex.ToString());
					}
					if (!replicationCheck.HasPassed)
					{
						flag = false;
						stringBuilder.AppendFormat("Check '{0}' did not Pass!{1}Detail Message - {2}{1}", checkType.FullName, Environment.NewLine, (replicationCheck.GetCheckOutcome().Error == null) ? "NULL" : replicationCheck.GetCheckOutcome().Error);
					}
					if (!replicationCheck.HasRun)
					{
						flag = false;
						stringBuilder.AppendFormat("Check '{0}' did not Run!{1}Detail Message - {2}{1}", checkType.FullName, Environment.NewLine, (replicationCheck.GetCheckOutcome().Error == null) ? "NULL" : replicationCheck.GetCheckOutcome().Error);
					}
				}
				string text = stringBuilder.ToString();
				base.Result.StateAttribute1 = this.ServerName;
				base.Result.StateAttribute2 = checkType.Name;
				base.Result.StateAttribute3 = (flag ? "Pass" : "Fail");
				base.Result.StateAttribute4 = ((replicationCheck.GetCheckOutcome().Error == null) ? "NULL" : replicationCheck.GetCheckOutcome().Error);
				base.Result.StateAttribute5 = text;
				if (!flag)
				{
					WTFDiagnostics.TraceError<string, string, bool, string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: RunReplicationCheck: Server={0}, Check={1}, CheckRan={2}, Outcome={3}", this.ServerName, checkType.Name, replicationCheck.HasRun, (replicationCheck.GetCheckOutcome().Error == null) ? "NULL" : replicationCheck.GetCheckOutcome().Error, null, "RunReplicationCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 306);
					throw new Exception(text);
				}
				WTFDiagnostics.TraceInformation<string, string, bool, string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: RunReplicationCheck: Server={0}, Check={1}, CheckRan={2}, Outcome={3}", this.ServerName, checkType.Name, replicationCheck.HasRun, (replicationCheck.GetCheckOutcome().Error == null) ? "NULL" : replicationCheck.GetCheckOutcome().Error, null, "RunReplicationCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 317);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0004EB9C File Offset: 0x0004CD9C
		private void BuildServerConfiguration(IADServer serverObj)
		{
			this.serverConfigBitfield = ServerConfig.Unknown;
			if (serverObj.DatabaseAvailabilityGroup != null)
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: BuildReplayConfiguration(): {0} is a DAG Member.", this.ServerName, null, "BuildServerConfiguration", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 340);
				this.serverConfigBitfield |= ServerConfig.DagMember;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "RhChecksProbeBase:: BuildReplayConfiguration(): The following bits are set on localConfigBitfield: {0}", this.serverConfigBitfield.ToString(), null, "BuildServerConfiguration", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ReplicationHealthChecksProbeBase.cs", 348);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0004EC26 File Offset: 0x0004CE26
		private bool AreConfigBitsSet(ServerConfig configBits)
		{
			return ReplicationHealthChecksProbeBase.AreConfigBitsSet(this.serverConfigBitfield, configBits);
		}

		// Token: 0x0400091A RID: 2330
		private static readonly List<string> DagMemberOnlyChecksTypeNames = new List<string>(new string[]
		{
			typeof(ClusterRpcCheck).FullName,
			typeof(TcpListenerCheck).FullName,
			typeof(QuorumGroupCheck).FullName,
			typeof(ClusterNetworkCheck).FullName,
			typeof(ServerLocatorProbe).FullName
		});

		// Token: 0x0400091B RID: 2331
		private ServerConfig serverConfigBitfield;

		// Token: 0x0400091C RID: 2332
		private IADServer serverObj;
	}
}
