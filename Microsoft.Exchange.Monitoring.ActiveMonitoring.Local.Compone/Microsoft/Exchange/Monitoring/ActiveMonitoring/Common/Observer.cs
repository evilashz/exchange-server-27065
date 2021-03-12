using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000557 RID: 1367
	internal class Observer : IObserver
	{
		// Token: 0x06002200 RID: 8704 RVA: 0x000CD8AC File Offset: 0x000CBAAC
		public Observer(string serverName)
		{
			this.serverName = serverName;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000CD8BB File Offset: 0x000CBABB
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000CD8C3 File Offset: 0x000CBAC3
		public bool IsInMaintenance
		{
			get
			{
				return DirectoryAccessor.Instance.IsMonitoringOffline(this.serverName);
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000CD8F0 File Offset: 0x000CBAF0
		public IEnumerable<ISubject> GetAllSubjects()
		{
			List<Subject> subjects = new List<Subject>();
			Array.ForEach<string>(MonitoringServerManager.GetAllSubjects(), delegate(string svr)
			{
				subjects.Add(new Subject(svr));
			});
			return subjects;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000CD92A File Offset: 0x000CBB2A
		public bool TryAddSubject(ISubject subject)
		{
			return MonitoringServerManager.TryAddSubject(subject.ServerName);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000CD937 File Offset: 0x000CBB37
		public void RemoveSubject(ISubject subject)
		{
			MonitoringServerManager.RemoveSubject(subject.ServerName);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000CD944 File Offset: 0x000CBB44
		public ObserverHeartbeatResponse SendHeartbeat(ISubject subject)
		{
			ObserverHeartbeatResponse result = ObserverHeartbeatResponse.NoResponse;
			try
			{
				RpcObserverHeartbeatImpl.SendObserverHeartbeat(subject.ServerName, out result);
			}
			catch (ActiveMonitoringServerException arg)
			{
				WTFDiagnostics.TraceWarning<string, ActiveMonitoringServerException>(ExTraceGlobals.HeartbeatTracer, TracingContext.Default, "Observer heartbeat RPC to server '{0}' failed with exception '{1}'.", subject.ServerName, arg, null, "SendHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\Observer.cs", 110);
			}
			return result;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000CD9A0 File Offset: 0x000CBBA0
		public override string ToString()
		{
			return this.serverName;
		}

		// Token: 0x040018B1 RID: 6321
		private readonly string serverName;
	}
}
