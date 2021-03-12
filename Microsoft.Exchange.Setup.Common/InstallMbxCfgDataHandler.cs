using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallMbxCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00007A50 File Offset: 0x00005C50
		public InstallMbxCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "MailboxRole", "Install-MailboxRole", connection)
		{
			this.mailboxRoleConfigurationInfo = (MailboxRoleConfigurationInfo)base.InstallableUnitConfigurationInfo;
			if (context.ParsedArguments.ContainsKey("mdbname"))
			{
				this.MdbName = (string)context.ParsedArguments["mdbname"];
			}
			if (context.ParsedArguments.ContainsKey("dbfilepath"))
			{
				this.DbFilePath = ((EdbFilePath)context.ParsedArguments["dbfilepath"]).PathName;
			}
			if (context.ParsedArguments.ContainsKey("logfolderpath"))
			{
				this.LogFolderPath = ((NonRootLocalLongFullPath)context.ParsedArguments["logfolderpath"]).PathName;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007B14 File Offset: 0x00005D14
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("MdbName", this.MdbName);
			base.Parameters.AddWithValue("DbFilePath", this.DbFilePath);
			base.Parameters.AddWithValue("LogFolderPath", this.LogFolderPath);
			SetupLogger.TraceExit();
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00007B7C File Offset: 0x00005D7C
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00007B89 File Offset: 0x00005D89
		public string MdbName
		{
			get
			{
				return this.mailboxRoleConfigurationInfo.MdbName;
			}
			set
			{
				this.mailboxRoleConfigurationInfo.MdbName = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00007B97 File Offset: 0x00005D97
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public string DbFilePath
		{
			get
			{
				return this.mailboxRoleConfigurationInfo.DbFilePath;
			}
			set
			{
				this.mailboxRoleConfigurationInfo.DbFilePath = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00007BB2 File Offset: 0x00005DB2
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00007BBF File Offset: 0x00005DBF
		public string LogFolderPath
		{
			get
			{
				return this.mailboxRoleConfigurationInfo.LogFolderPath;
			}
			set
			{
				this.mailboxRoleConfigurationInfo.LogFolderPath = value;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007BCD File Offset: 0x00005DCD
		public override void UpdatePreCheckTaskDataHandler()
		{
			base.UpdatePreCheckTaskDataHandler();
			PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
		}

		// Token: 0x04000078 RID: 120
		private MailboxRoleConfigurationInfo mailboxRoleConfigurationInfo;
	}
}
