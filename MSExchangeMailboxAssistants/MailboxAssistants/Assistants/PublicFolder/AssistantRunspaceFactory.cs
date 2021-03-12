using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000163 RID: 355
	internal class AssistantRunspaceFactory : IAssistantRunspaceFactory, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x00056CA6 File Offset: 0x00054EA6
		public AssistantRunspaceFactory()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00056CBA File Offset: 0x00054EBA
		public IAssistantRunspaceProxy CreateRunspaceForDatacenterAdmin(OrganizationId organizationId)
		{
			if (this.assistantRunspaceProxy == null)
			{
				this.assistantRunspaceProxy = AssistantRunspaceProxy.CreateRunspaceForDatacenterAdmin(organizationId);
			}
			return this.assistantRunspaceProxy;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00056CD6 File Offset: 0x00054ED6
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AssistantRunspaceFactory>(this);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00056CDE File Offset: 0x00054EDE
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00056CF3 File Offset: 0x00054EF3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00056D04 File Offset: 0x00054F04
		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.assistantRunspaceProxy != null)
					{
						this.assistantRunspaceProxy.Dispose();
						this.assistantRunspaceProxy = null;
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000943 RID: 2371
		private IAssistantRunspaceProxy assistantRunspaceProxy;

		// Token: 0x04000944 RID: 2372
		private bool disposed;

		// Token: 0x04000945 RID: 2373
		private DisposeTracker disposeTracker;
	}
}
