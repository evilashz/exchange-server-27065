using System;
using System.ServiceProcess;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000630 RID: 1584
	public abstract class ManageImap4Service : ManagePopImapService
	{
		// Token: 0x060037DF RID: 14303 RVA: 0x000E7EFF File Offset: 0x000E60FF
		protected ManageImap4Service()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.AddFirewallRule(new MSExchangeIMAP4FirewallRule());
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000E7F19 File Offset: 0x000E6119
		protected override string Name
		{
			get
			{
				return "MSExchangeIMAP4";
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x000E7F20 File Offset: 0x000E6120
		protected override string ServiceDisplayName
		{
			get
			{
				return Strings.Imap4ServiceDisplayName;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000E7F2C File Offset: 0x000E612C
		protected override string ServiceDescription
		{
			get
			{
				return Strings.Imap4ServiceDescription;
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000E7F38 File Offset: 0x000E6138
		protected override string ServiceFile
		{
			get
			{
				return "Microsoft.Exchange.Imap4Service.exe";
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000E7F3F File Offset: 0x000E613F
		protected override string ServiceCategoryName
		{
			get
			{
				return "MSExchange IMAP4 service";
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000E7F46 File Offset: 0x000E6146
		protected override string WorkingProcessFile
		{
			get
			{
				return "Microsoft.Exchange.Imap4.exe";
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000E7F4D File Offset: 0x000E614D
		protected override string WorkingProcessEventMessageFile
		{
			get
			{
				return "Microsoft.Exchange.Imap4.EventLog.dll";
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x000E7F54 File Offset: 0x000E6154
		protected override string RelativeInstallPath
		{
			get
			{
				return "FrontEnd\\PopImap";
			}
		}
	}
}
