using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000560 RID: 1376
	internal class RecoveryActionsEnabledEndpoint : IEndpoint
	{
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000D0F47 File Offset: 0x000CF147
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x000D0F4A File Offset: 0x000CF14A
		// (set) Token: 0x06002291 RID: 8849 RVA: 0x000D0F52 File Offset: 0x000CF152
		public Exception Exception { get; set; }

		// Token: 0x06002292 RID: 8850 RVA: 0x000D0F5C File Offset: 0x000CF15C
		public void Initialize()
		{
			try
			{
				this.cachedIsOnline = ServerComponentStateManager.IsOnline(ServerComponentEnum.RecoveryActionsEnabled);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.RecoveryActionsEnabledEndpointTracer, this.traceContext, string.Format("[Initialize] ServerComponentStateManager.IsOnline failed: {0}", ex.ToString()), null, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\RecoveryActionsEnabledEndpoint.cs", 60);
				throw;
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000D0FB8 File Offset: 0x000CF1B8
		public bool DetectChange()
		{
			bool result;
			try
			{
				result = (this.cachedIsOnline != ServerComponentStateManager.IsOnline(ServerComponentEnum.RecoveryActionsEnabled));
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.RecoveryActionsEnabledEndpointTracer, this.traceContext, string.Format("[DetectChange] ServerComponentStateManager.IsOnline failed: {0}", ex.ToString()), null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\RecoveryActionsEnabledEndpoint.cs", 82);
				throw;
			}
			return result;
		}

		// Token: 0x040018E2 RID: 6370
		private bool cachedIsOnline;

		// Token: 0x040018E3 RID: 6371
		private TracingContext traceContext = TracingContext.Default;
	}
}
