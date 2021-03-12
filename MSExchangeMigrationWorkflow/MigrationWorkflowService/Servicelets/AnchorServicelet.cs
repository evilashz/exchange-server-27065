using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MigrationWorkflowService.Servicelets
{
	// Token: 0x02000006 RID: 6
	internal class AnchorServicelet<TContext, TService> : SimpleAnchorServicelet<TContext> where TContext : AnchorContext, new() where TService : class, IAnchorService, new()
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000027EA File Offset: 0x000009EA
		public AnchorServicelet(ManualResetEvent serviceStoppingHandle) : base(serviceStoppingHandle)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027F4 File Offset: 0x000009F4
		public override bool Initialize()
		{
			base.Initialize();
			this.anchorService = Activator.CreateInstance<TService>();
			if (!this.anchorService.Initialize(this.AnchorContext))
			{
				this.anchorService.Dispose();
				this.anchorService = default(TService);
				return false;
			}
			this.anchorService.Start();
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002862 File Offset: 0x00000A62
		public override IEnumerable<IDiagnosable> GetDiagnosableComponents()
		{
			if (this.anchorService == null)
			{
				return base.GetDiagnosableComponents();
			}
			return this.anchorService.GetDiagnosableComponents();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002889 File Offset: 0x00000A89
		public override void Stop()
		{
			if (this.anchorService != null)
			{
				this.anchorService.Dispose();
				this.anchorService = default(TService);
			}
		}

		// Token: 0x0400000E RID: 14
		private TService anchorService;
	}
}
