using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200007F RID: 127
	public class ServerInfo
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00013B20 File Offset: 0x00011D20
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00013B28 File Offset: 0x00011D28
		public Guid Guid { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00013B31 File Offset: 0x00011D31
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00013B39 File Offset: 0x00011D39
		public string ServerName { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00013B42 File Offset: 0x00011D42
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00013B4A File Offset: 0x00011D4A
		public string ForestName { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00013B53 File Offset: 0x00011D53
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00013B5B File Offset: 0x00011D5B
		public string ExchangeLegacyDN { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00013B64 File Offset: 0x00011D64
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00013B6C File Offset: 0x00011D6C
		public string InstallPath { get; private set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00013B75 File Offset: 0x00011D75
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00013B7D File Offset: 0x00011D7D
		public SecurityDescriptor NTSecurityDescriptor { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00013B86 File Offset: 0x00011D86
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00013B8E File Offset: 0x00011D8E
		public bool IsMultiRole { get; private set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00013B97 File Offset: 0x00011D97
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00013B9F File Offset: 0x00011D9F
		public int? MaxRpcThreads { get; private set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00013BA8 File Offset: 0x00011DA8
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00013BB0 File Offset: 0x00011DB0
		public long ContinuousReplicationMaxMemoryPerDatabase { get; private set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00013BB9 File Offset: 0x00011DB9
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00013BC1 File Offset: 0x00011DC1
		public int? MaxActiveDatabases { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00013BCA File Offset: 0x00011DCA
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00013BD2 File Offset: 0x00011DD2
		public ServerEditionType Edition { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00013BDB File Offset: 0x00011DDB
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00013BE3 File Offset: 0x00011DE3
		public int MaxRecoveryDatabases { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00013BEC File Offset: 0x00011DEC
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public int MaxTotalDatabases { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00013BFD File Offset: 0x00011DFD
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00013C05 File Offset: 0x00011E05
		public bool IsDAGMember { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00013C0E File Offset: 0x00011E0E
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00013C16 File Offset: 0x00011E16
		public DatabaseOptions DatabaseOptions { get; private set; }

		// Token: 0x06000716 RID: 1814 RVA: 0x00013C20 File Offset: 0x00011E20
		public ServerInfo(string serverName, Guid guidServer, string exchangeLegacyDN, string installPath, SecurityDescriptor ntSecurityDescriptor, bool isMultiRole, int? maxRpcThreads, long continuousReplicationMaxMemoryPerDatabase, int? maxActiveDatabases, ServerEditionType edition, int maxRecoveryDatabases, int maxTotalDatabases, bool isDAGMember, string forestName, DatabaseOptions databaseOptions)
		{
			this.ServerName = serverName;
			this.Guid = guidServer;
			this.ExchangeLegacyDN = exchangeLegacyDN;
			this.InstallPath = installPath;
			this.NTSecurityDescriptor = ntSecurityDescriptor;
			this.IsMultiRole = isMultiRole;
			this.MaxRpcThreads = maxRpcThreads;
			this.ContinuousReplicationMaxMemoryPerDatabase = continuousReplicationMaxMemoryPerDatabase;
			this.MaxActiveDatabases = maxActiveDatabases;
			this.Edition = edition;
			this.MaxRecoveryDatabases = maxRecoveryDatabases;
			this.MaxTotalDatabases = maxTotalDatabases;
			this.IsDAGMember = isDAGMember;
			this.ForestName = forestName;
			this.DatabaseOptions = databaseOptions;
		}
	}
}
