using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B68 RID: 2920
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TenantLicenseAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x060069BC RID: 27068 RVA: 0x001C520E File Offset: 0x001C340E
		public TenantLicenseAsyncResult(RmsClientManagerContext context, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x060069BD RID: 27069 RVA: 0x001C5219 File Offset: 0x001C3419
		// (set) Token: 0x060069BE RID: 27070 RVA: 0x001C5221 File Offset: 0x001C3421
		public XmlNode[] Rac
		{
			get
			{
				return this.rac;
			}
			set
			{
				this.rac = value;
			}
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x060069BF RID: 27071 RVA: 0x001C522A File Offset: 0x001C342A
		// (set) Token: 0x060069C0 RID: 27072 RVA: 0x001C5232 File Offset: 0x001C3432
		public ServerCertificationWSManager ServerCertificationManager
		{
			get
			{
				return this.serverCertificationManager;
			}
			set
			{
				this.serverCertificationManager = value;
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x060069C1 RID: 27073 RVA: 0x001C523B File Offset: 0x001C343B
		// (set) Token: 0x060069C2 RID: 27074 RVA: 0x001C5243 File Offset: 0x001C3443
		public PublishWSManager PublishManager
		{
			get
			{
				return this.publishManager;
			}
			set
			{
				this.publishManager = value;
			}
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x001C524C File Offset: 0x001C344C
		public void ReleaseWsManagers()
		{
			if (this.ServerCertificationManager != null)
			{
				this.ServerCertificationManager.Dispose();
				this.ServerCertificationManager = null;
			}
			if (this.PublishManager != null)
			{
				this.PublishManager.Dispose();
				this.PublishManager = null;
			}
		}

		// Token: 0x04003C25 RID: 15397
		private ServerCertificationWSManager serverCertificationManager;

		// Token: 0x04003C26 RID: 15398
		private PublishWSManager publishManager;

		// Token: 0x04003C27 RID: 15399
		private XmlNode[] rac;
	}
}
