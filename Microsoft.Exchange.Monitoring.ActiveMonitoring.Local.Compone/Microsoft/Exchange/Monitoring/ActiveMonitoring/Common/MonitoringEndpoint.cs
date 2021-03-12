using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200055F RID: 1375
	internal class MonitoringEndpoint : IEndpoint
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000D0D35 File Offset: 0x000CEF35
		public bool IsOnline
		{
			get
			{
				return this.cachedIsOnline;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000D0D3D File Offset: 0x000CEF3D
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000D0D40 File Offset: 0x000CEF40
		// (set) Token: 0x0600228B RID: 8843 RVA: 0x000D0D48 File Offset: 0x000CEF48
		public Exception Exception { get; set; }

		// Token: 0x0600228C RID: 8844 RVA: 0x000D0D54 File Offset: 0x000CEF54
		public void Initialize()
		{
			try
			{
				this.cachedIsOnline = ServerComponentStateManager.IsOnline(ServerComponentEnum.Monitoring);
				if (DirectoryAccessor.Instance.Server != null)
				{
					this.monitoringGroup = DirectoryAccessor.Instance.Server.MonitoringGroup;
				}
				else if (DirectoryAccessor.Instance.Computer != null)
				{
					this.monitoringGroup = DirectoryAccessor.Instance.Computer.MonitoringGroup;
				}
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceError<Exception>(ExTraceGlobals.MonitoringEndpointTracer, this.traceContext, "[Initialize] ServerComponentStateManager.IsOnline failed: {0}", arg, null, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringEndpoint.cs", 85);
				throw;
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000D0DEC File Offset: 0x000CEFEC
		public bool DetectChange()
		{
			bool result;
			try
			{
				if (this.cachedIsOnline != ServerComponentStateManager.IsOnline(ServerComponentEnum.Monitoring))
				{
					WTFDiagnostics.TraceDebug<bool, bool>(ExTraceGlobals.MonitoringEndpointTracer, this.traceContext, "[DetectChange] ServerComponentStateManager.DetectChange: detected monitoring online state changed from {0} to {1}", this.cachedIsOnline, !this.cachedIsOnline, null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringEndpoint.cs", 106);
					result = true;
				}
				else
				{
					DirectoryAccessor.Instance.RefreshServerOrComputerObject();
					if ((DirectoryAccessor.Instance.Server != null && string.Compare(DirectoryAccessor.Instance.Server.MonitoringGroup, this.monitoringGroup, true) != 0) || (DirectoryAccessor.Instance.Computer != null && string.Compare(DirectoryAccessor.Instance.Computer.MonitoringGroup, this.monitoringGroup, true) != 0))
					{
						WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MonitoringEndpointTracer, this.traceContext, "[DetectChange] ServerComponentStateManager.DetectChange: detected monitoring group changed from {0} to {1}", this.monitoringGroup, (DirectoryAccessor.Instance.Server != null) ? DirectoryAccessor.Instance.Server.MonitoringGroup : DirectoryAccessor.Instance.Computer.MonitoringGroup, null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringEndpoint.cs", 119);
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceError<Exception>(ExTraceGlobals.MonitoringEndpointTracer, this.traceContext, "[DetectChange] ServerComponentStateManager.DetectChange failed: {0}", arg, null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringEndpoint.cs", 132);
				throw;
			}
			return result;
		}

		// Token: 0x040018DE RID: 6366
		private bool cachedIsOnline;

		// Token: 0x040018DF RID: 6367
		private string monitoringGroup;

		// Token: 0x040018E0 RID: 6368
		private TracingContext traceContext = TracingContext.Default;
	}
}
