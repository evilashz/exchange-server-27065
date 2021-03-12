using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000066 RID: 102
	public class OwaVdirConfiguration : VdirConfiguration
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000134DC File Offset: 0x000116DC
		private OwaVdirConfiguration(ADOwaVirtualDirectory owaVirtualDirectory) : base(owaVirtualDirectory)
		{
			this.logonFormat = owaVirtualDirectory.LogonFormat;
			this.publicPrivateSelectionEnabled = (owaVirtualDirectory.LogonPagePublicPrivateSelectionEnabled != null && owaVirtualDirectory.LogonPagePublicPrivateSelectionEnabled.Value);
			this.lightSelectionEnabled = (owaVirtualDirectory.LogonPageLightSelectionEnabled != null && owaVirtualDirectory.LogonPageLightSelectionEnabled.Value);
			this.logonAndErrorLanguage = owaVirtualDirectory.LogonAndErrorLanguage;
			this.redirectToOptimalOWAServer = (owaVirtualDirectory.RedirectToOptimalOWAServer ?? true);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00013577 File Offset: 0x00011777
		public new static OwaVdirConfiguration Instance
		{
			get
			{
				return VdirConfiguration.Instance as OwaVdirConfiguration;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00013583 File Offset: 0x00011783
		public LogonFormats LogonFormat
		{
			get
			{
				return this.logonFormat;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0001358B File Offset: 0x0001178B
		public bool PublicPrivateSelectionEnabled
		{
			get
			{
				return this.publicPrivateSelectionEnabled;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00013593 File Offset: 0x00011793
		public bool LightSelectionEnabled
		{
			get
			{
				return this.lightSelectionEnabled;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0001359B File Offset: 0x0001179B
		public int LogonAndErrorLanguage
		{
			get
			{
				return this.logonAndErrorLanguage;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000135A3 File Offset: 0x000117A3
		public bool RedirectToOptimalOWAServer
		{
			get
			{
				return this.redirectToOptimalOWAServer;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000135AC File Offset: 0x000117AC
		internal static OwaVdirConfiguration CreateInstance(ITopologyConfigurationSession session, ADObjectId virtualDirectoryDN)
		{
			ADOwaVirtualDirectory adowaVirtualDirectory = null;
			ADOwaVirtualDirectory[] array = session.Find<ADOwaVirtualDirectory>(virtualDirectoryDN, QueryScope.Base, null, null, 1);
			if (array != null && array.Length == 1)
			{
				adowaVirtualDirectory = array[0];
			}
			if (adowaVirtualDirectory == null)
			{
				throw new ADNoSuchObjectException(LocalizedString.Empty);
			}
			return new OwaVdirConfiguration(adowaVirtualDirectory);
		}

		// Token: 0x04000228 RID: 552
		private readonly bool publicPrivateSelectionEnabled;

		// Token: 0x04000229 RID: 553
		private readonly bool lightSelectionEnabled;

		// Token: 0x0400022A RID: 554
		private readonly bool redirectToOptimalOWAServer;

		// Token: 0x0400022B RID: 555
		private readonly int logonAndErrorLanguage;

		// Token: 0x0400022C RID: 556
		private LogonFormats logonFormat;
	}
}
