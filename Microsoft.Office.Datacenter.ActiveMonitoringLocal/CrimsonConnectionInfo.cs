using System;
using System.Security;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200006F RID: 111
	internal class CrimsonConnectionInfo
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0001B03F File Offset: 0x0001923F
		internal CrimsonConnectionInfo(string computerName)
		{
			this.ComputerName = computerName;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001B04E File Offset: 0x0001924E
		internal CrimsonConnectionInfo(string computerName, string userDomain, string userName, SecureString password)
		{
			this.ComputerName = computerName;
			this.UserDomain = userDomain;
			this.UserName = userName;
			this.Password = password;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001B073 File Offset: 0x00019273
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0001B07B File Offset: 0x0001927B
		internal string ComputerName { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001B084 File Offset: 0x00019284
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001B08C File Offset: 0x0001928C
		internal string UserDomain { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001B095 File Offset: 0x00019295
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001B09D File Offset: 0x0001929D
		internal string UserName { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001B0A6 File Offset: 0x000192A6
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001B0AE File Offset: 0x000192AE
		internal SecureString Password { get; private set; }
	}
}
