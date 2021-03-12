using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000002 RID: 2
	internal class AnchoredRoutingTarget
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AnchoredRoutingTarget(AnchorMailbox anchorMailbox, BackEndServer backendServer)
		{
			if (anchorMailbox == null)
			{
				throw new ArgumentNullException("anchorMailbox");
			}
			if (backendServer == null)
			{
				throw new ArgumentNullException("backendServer");
			}
			this.AnchorMailbox = anchorMailbox;
			this.BackEndServer = backendServer;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002102 File Offset: 0x00000302
		public AnchoredRoutingTarget(ServerInfoAnchorMailbox serverInfoAnchorMailbox)
		{
			if (serverInfoAnchorMailbox == null)
			{
				throw new ArgumentNullException("serverAnchorMailbox");
			}
			this.AnchorMailbox = serverInfoAnchorMailbox;
			this.BackEndServer = serverInfoAnchorMailbox.BackEndServer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002133 File Offset: 0x00000333
		public AnchorMailbox AnchorMailbox { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002144 File Offset: 0x00000344
		public BackEndServer BackEndServer { get; private set; }

		// Token: 0x06000007 RID: 7 RVA: 0x0000214D File Offset: 0x0000034D
		public override string ToString()
		{
			return string.Format("{0}~{1}", this.AnchorMailbox, this.BackEndServer.Fqdn);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		public string GetSiteName()
		{
			string empty = string.Empty;
			if (this.BackEndServer != null && !string.IsNullOrEmpty(this.BackEndServer.Fqdn))
			{
				Utilities.TryGetSiteNameFromServerFqdn(this.BackEndServer.Fqdn, out empty);
			}
			return empty;
		}
	}
}
