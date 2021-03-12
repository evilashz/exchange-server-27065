using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000EB RID: 235
	internal class ExchangeDiagnostics : DisposeTrackableBase
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00026534 File Offset: 0x00024734
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x0002653C File Offset: 0x0002473C
		private IDiagnosable RegisteredDiagnosable { get; set; }

		// Token: 0x060009CD RID: 2509 RVA: 0x00026545 File Offset: 0x00024745
		public ExchangeDiagnostics(IDiagnosable diagnosable)
		{
			ProcessAccessManager.RegisterComponent(diagnosable);
			this.RegisteredDiagnosable = diagnosable;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002655A File Offset: 0x0002475A
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.RegisteredDiagnosable != null)
				{
					ProcessAccessManager.UnregisterComponent(this.RegisteredDiagnosable);
				}
				this.RegisteredDiagnosable = null;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00026579 File Offset: 0x00024779
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeDiagnostics>(this);
		}
	}
}
