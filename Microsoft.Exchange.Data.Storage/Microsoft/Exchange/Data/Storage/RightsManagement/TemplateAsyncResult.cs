using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B67 RID: 2919
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TemplateAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x060069B7 RID: 27063 RVA: 0x001C51C6 File Offset: 0x001C33C6
		public TemplateAsyncResult(RmsClientManagerContext context, Guid templateId, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			this.templateId = templateId;
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x060069B8 RID: 27064 RVA: 0x001C51D9 File Offset: 0x001C33D9
		public Guid TemplateId
		{
			get
			{
				return this.templateId;
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x060069B9 RID: 27065 RVA: 0x001C51E1 File Offset: 0x001C33E1
		// (set) Token: 0x060069BA RID: 27066 RVA: 0x001C51E9 File Offset: 0x001C33E9
		public TemplateWSManager TemplateManager
		{
			get
			{
				return this.templateManager;
			}
			set
			{
				this.templateManager = value;
			}
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x001C51F2 File Offset: 0x001C33F2
		public void ReleaseWsManagers()
		{
			if (this.TemplateManager != null)
			{
				this.TemplateManager.Dispose();
				this.TemplateManager = null;
			}
		}

		// Token: 0x04003C23 RID: 15395
		private TemplateWSManager templateManager;

		// Token: 0x04003C24 RID: 15396
		private Guid templateId;
	}
}
