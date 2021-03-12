using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000DB RID: 219
	[Cmdlet("Install", "sqldatabase")]
	[LocDescription(Strings.IDs.InstallCentralAdminServiceTask)]
	public class InstallSqlDatabase : ManageSqlDatabase
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001BD2B File Offset: 0x00019F2B
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001BD42 File Offset: 0x00019F42
		[Parameter(Mandatory = false)]
		public string DatabasePath
		{
			get
			{
				return (string)base.Fields["DatabasePath"];
			}
			set
			{
				base.Fields["DatabasePath"] = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001BD55 File Offset: 0x00019F55
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001BD6C File Offset: 0x00019F6C
		[Parameter(Mandatory = false)]
		public string TransactionLogPath
		{
			get
			{
				return (string)base.Fields["TransactionLogPath"];
			}
			set
			{
				base.Fields["TransactionLogPath"] = value;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001BD7F File Offset: 0x00019F7F
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install(this.DatabasePath, this.TransactionLogPath);
			TaskLogger.LogExit();
		}
	}
}
