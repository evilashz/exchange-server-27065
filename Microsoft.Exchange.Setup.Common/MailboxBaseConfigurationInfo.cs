using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxBaseConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000AFA9 File Offset: 0x000091A9
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000AFB1 File Offset: 0x000091B1
		public string MdbName
		{
			get
			{
				return this.mdbName;
			}
			set
			{
				this.mdbName = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000AFBA File Offset: 0x000091BA
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000AFC2 File Offset: 0x000091C2
		public string DbFilePath
		{
			get
			{
				return this.dbFilePath;
			}
			set
			{
				this.dbFilePath = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000AFCB File Offset: 0x000091CB
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000AFD3 File Offset: 0x000091D3
		public string LogFolderPath
		{
			get
			{
				return this.logFolderPath;
			}
			set
			{
				this.logFolderPath = value;
			}
		}

		// Token: 0x040000BD RID: 189
		private string mdbName;

		// Token: 0x040000BE RID: 190
		private string dbFilePath;

		// Token: 0x040000BF RID: 191
		private string logFolderPath;
	}
}
