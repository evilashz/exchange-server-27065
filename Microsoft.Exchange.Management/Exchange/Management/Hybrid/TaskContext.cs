using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000907 RID: 2311
	internal class TaskContext : ITaskContext
	{
		// Token: 0x060051FE RID: 20990 RVA: 0x001533DC File Offset: 0x001515DC
		public TaskContext(IUserInterface ui, ILogger logger, HybridConfiguration hybridConfigurationObject, IOnPremisesSession onPremisesSession, ITenantSession tenantSession)
		{
			this.UI = ui;
			this.Logger = logger;
			this.HybridConfigurationObject = hybridConfigurationObject;
			this.OnPremisesSession = onPremisesSession;
			this.TenantSession = tenantSession;
			this.Parameters = new ContextParameters();
			this.Errors = new List<LocalizedString>();
			this.Warnings = new List<LocalizedString>();
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x00153435 File Offset: 0x00151635
		// (set) Token: 0x06005200 RID: 20992 RVA: 0x0015343D File Offset: 0x0015163D
		public HybridConfiguration HybridConfigurationObject { get; private set; }

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x00153446 File Offset: 0x00151646
		// (set) Token: 0x06005202 RID: 20994 RVA: 0x0015344E File Offset: 0x0015164E
		public IContextParameters Parameters { get; private set; }

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06005203 RID: 20995 RVA: 0x00153457 File Offset: 0x00151657
		// (set) Token: 0x06005204 RID: 20996 RVA: 0x0015345F File Offset: 0x0015165F
		public ILogger Logger { get; private set; }

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x00153468 File Offset: 0x00151668
		// (set) Token: 0x06005206 RID: 20998 RVA: 0x00153470 File Offset: 0x00151670
		public IUserInterface UI { get; private set; }

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x00153479 File Offset: 0x00151679
		// (set) Token: 0x06005208 RID: 21000 RVA: 0x00153481 File Offset: 0x00151681
		public IOnPremisesSession OnPremisesSession { get; private set; }

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06005209 RID: 21001 RVA: 0x0015348A File Offset: 0x0015168A
		// (set) Token: 0x0600520A RID: 21002 RVA: 0x00153492 File Offset: 0x00151692
		public ITenantSession TenantSession { get; private set; }

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x0015349B File Offset: 0x0015169B
		// (set) Token: 0x0600520C RID: 21004 RVA: 0x001534A3 File Offset: 0x001516A3
		public IList<LocalizedString> Errors { get; private set; }

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x0600520D RID: 21005 RVA: 0x001534AC File Offset: 0x001516AC
		// (set) Token: 0x0600520E RID: 21006 RVA: 0x001534B4 File Offset: 0x001516B4
		public IList<LocalizedString> Warnings { get; private set; }
	}
}
