using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MigrationWorkflowService.Servicelets
{
	// Token: 0x02000005 RID: 5
	internal class SimpleAnchorServicelet<TContext> : IServicelet where TContext : AnchorContext, new()
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000274C File Offset: 0x0000094C
		public SimpleAnchorServicelet(ManualResetEvent serviceStoppingHandle)
		{
			this.serviceStoppingHandle = serviceStoppingHandle;
			this.AnchorContext = Activator.CreateInstance<TContext>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002768 File Offset: 0x00000968
		public bool IsEnabled
		{
			get
			{
				TContext anchorContext = this.AnchorContext;
				return anchorContext.Config.GetConfig<bool>("IsEnabled");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002794 File Offset: 0x00000994
		public string Name
		{
			get
			{
				TContext anchorContext = this.AnchorContext;
				return anchorContext.ApplicationName;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027B5 File Offset: 0x000009B5
		public virtual bool Initialize()
		{
			this.anchorApplication = new AnchorApplication(this.AnchorContext, this.serviceStoppingHandle);
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027D4 File Offset: 0x000009D4
		public virtual IEnumerable<IDiagnosable> GetDiagnosableComponents()
		{
			return Array<IDiagnosable>.Empty;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027DB File Offset: 0x000009DB
		public void Run()
		{
			this.anchorApplication.Process();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027E8 File Offset: 0x000009E8
		public virtual void Stop()
		{
		}

		// Token: 0x0400000B RID: 11
		protected readonly TContext AnchorContext;

		// Token: 0x0400000C RID: 12
		private readonly ManualResetEvent serviceStoppingHandle;

		// Token: 0x0400000D RID: 13
		private AnchorApplication anchorApplication;
	}
}
