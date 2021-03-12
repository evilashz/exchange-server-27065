using System;
using System.ServiceProcess;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000636 RID: 1590
	public abstract class ManagePop3Service : ManagePopImapService
	{
		// Token: 0x060037F9 RID: 14329 RVA: 0x000E802E File Offset: 0x000E622E
		protected ManagePop3Service()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.AddFirewallRule(new MSExchangePOP3FirewallRule());
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000E8048 File Offset: 0x000E6248
		protected override string Name
		{
			get
			{
				return "MSExchangePOP3";
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000E804F File Offset: 0x000E624F
		protected override string ServiceDisplayName
		{
			get
			{
				return Strings.Pop3ServiceDisplayName;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000E805B File Offset: 0x000E625B
		protected override string ServiceDescription
		{
			get
			{
				return Strings.Pop3ServiceDescription;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000E8067 File Offset: 0x000E6267
		protected override string ServiceFile
		{
			get
			{
				return "Microsoft.Exchange.Pop3Service.exe";
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000E806E File Offset: 0x000E626E
		protected override string ServiceCategoryName
		{
			get
			{
				return "MSExchange POP3 service";
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000E8075 File Offset: 0x000E6275
		protected override string WorkingProcessFile
		{
			get
			{
				return "Microsoft.Exchange.Pop3.exe";
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x000E807C File Offset: 0x000E627C
		protected override string WorkingProcessEventMessageFile
		{
			get
			{
				return "Microsoft.Exchange.Pop3.EventLog.dll";
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000E8083 File Offset: 0x000E6283
		protected override string RelativeInstallPath
		{
			get
			{
				return "FrontEnd\\PopImap";
			}
		}
	}
}
