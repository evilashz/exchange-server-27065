using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000559 RID: 1369
	internal class Subject : ISubject
	{
		// Token: 0x0600220A RID: 8714 RVA: 0x000CD9B8 File Offset: 0x000CBBB8
		public Subject(string serverName)
		{
			this.serverName = serverName;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000CD9C7 File Offset: 0x000CBBC7
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x000CD9CF File Offset: 0x000CBBCF
		public bool IsInMaintenance
		{
			get
			{
				return DirectoryAccessor.Instance.IsMonitoringOffline(this.serverName);
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000CD9FC File Offset: 0x000CBBFC
		public IEnumerable<IObserver> GetAllObservers()
		{
			List<Observer> observers = new List<Observer>();
			Array.ForEach<string>(MonitoringServerManager.GetAllObservers(), delegate(string svr)
			{
				observers.Add(new Observer(svr));
			});
			return observers;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000CDA36 File Offset: 0x000CBC36
		public bool TryAddObserver(IObserver observer)
		{
			return MonitoringServerManager.TryAddObserver(observer.ServerName);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000CDA43 File Offset: 0x000CBC43
		public void RemoveObserver(IObserver observer)
		{
			MonitoringServerManager.RemoveObserver(observer.ServerName);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000CDA50 File Offset: 0x000CBC50
		public bool SendRequest(IObserver observer)
		{
			bool result = false;
			try
			{
				RpcRequestObserverImpl.SendRequestObserver(observer.ServerName, out result);
			}
			catch (ActiveMonitoringServerException arg)
			{
				WTFDiagnostics.TraceWarning<string, ActiveMonitoringServerException>(ExTraceGlobals.HeartbeatTracer, TracingContext.Default, "Observer request RPC to server '{0}' failed with exception '{1}'.", observer.ServerName, arg, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\Subject.cs", 112);
			}
			return result;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000CDAAC File Offset: 0x000CBCAC
		public void SendCancel(IObserver observer)
		{
			try
			{
				RpcCancelObserverImpl.SendCancelObserver(observer.ServerName);
			}
			catch (ActiveMonitoringServerException arg)
			{
				WTFDiagnostics.TraceWarning<string, ActiveMonitoringServerException>(ExTraceGlobals.HeartbeatTracer, TracingContext.Default, "Observer cancel RPC to server '{0}' failed with exception '{1}'.", observer.ServerName, arg, null, "SendCancel", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\Subject.cs", 132);
			}
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000CDB04 File Offset: 0x000CBD04
		public DateTime? GetLastResultTimestamp()
		{
			DateTime? result;
			try
			{
				DateTime? dateTime;
				RpcGetCrimsonEventImpl.SendRequest(this.serverName, out dateTime, 30000);
				result = dateTime;
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceWarning<string, Exception>(ExTraceGlobals.HeartbeatTracer, TracingContext.Default, "GetLastResultTimestamp failed over RPC event log query for {0} with exception '{1}'", this.serverName, arg, null, "GetLastResultTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\Subject.cs", 160);
				if (DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(this.serverName))
				{
					result = this.GetLastResultTimestamp<MonitorResult>("HealthManagerHeartbeatMonitor");
				}
				else
				{
					result = this.GetLastResultTimestamp<ResponderResult>("HealthManagerHeartbeatResponder");
				}
			}
			return result;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000CDB94 File Offset: 0x000CBD94
		public IPStatus Ping(TimeSpan timeout)
		{
			IPStatus status;
			using (Ping ping = new Ping())
			{
				PingReply pingReply = ping.Send(this.serverName, (int)timeout.TotalMilliseconds);
				status = pingReply.Status;
			}
			return status;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000CDBE0 File Offset: 0x000CBDE0
		public DateTime? GetLastObserverSelectionTimestamp()
		{
			return MonitoringServerManager.GetLastObserverSelectionTimestamp();
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000CDBE7 File Offset: 0x000CBDE7
		public DateTime? GetObserverHeartbeat(IObserver observer)
		{
			return MonitoringServerManager.GetObserverHeartbeat(observer.ServerName);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000CDBF4 File Offset: 0x000CBDF4
		public override string ToString()
		{
			return this.serverName;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000CDBFC File Offset: 0x000CBDFC
		private DateTime? GetLastResultTimestamp<TResult>(string resultName) where TResult : WorkItemResult, IPersistence, new()
		{
			DateTime? result = null;
			TResult tresult = default(TResult);
			using (CrimsonReader<TResult> crimsonReader = new CrimsonReader<TResult>())
			{
				crimsonReader.ConnectionInfo = new CrimsonConnectionInfo(this.serverName);
				crimsonReader.QueryUserPropertyCondition = string.Format("(ResultName='{0}')", resultName);
				crimsonReader.IsReverseDirection = true;
				tresult = crimsonReader.ReadNext();
			}
			if (tresult != null)
			{
				result = new DateTime?(tresult.ExecutionEndTime);
			}
			return result;
		}

		// Token: 0x040018B2 RID: 6322
		private readonly string serverName;
	}
}
