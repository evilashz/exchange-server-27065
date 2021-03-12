using System;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000633 RID: 1587
	public abstract class ManageImap4BeService : ManagePopImapService
	{
		// Token: 0x060037EC RID: 14316 RVA: 0x000E7FA5 File Offset: 0x000E61A5
		protected ManageImap4BeService()
		{
			base.AddFirewallRule(new MSExchangeIMAP4BeFirewallRule());
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000E7FB8 File Offset: 0x000E61B8
		protected override string Name
		{
			get
			{
				return "MSExchangeIMAP4BE";
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000E7FBF File Offset: 0x000E61BF
		protected override string ServiceDisplayName
		{
			get
			{
				return Strings.Imap4BeServiceDisplayName;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000E7FCB File Offset: 0x000E61CB
		protected override string ServiceDescription
		{
			get
			{
				return Strings.Imap4BeServiceDescription;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000E7FD7 File Offset: 0x000E61D7
		protected override string ServiceFile
		{
			get
			{
				return "Microsoft.Exchange.Imap4Service.exe";
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000E7FDE File Offset: 0x000E61DE
		protected override string ServiceCategoryName
		{
			get
			{
				return "MSExchange IMAP4 backend service";
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000E7FE5 File Offset: 0x000E61E5
		protected override string WorkingProcessFile
		{
			get
			{
				return "Microsoft.Exchange.Imap4.exe";
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x000E7FEC File Offset: 0x000E61EC
		protected override string WorkingProcessEventMessageFile
		{
			get
			{
				return "Microsoft.Exchange.Imap4.EventLog.dll";
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000E7FF3 File Offset: 0x000E61F3
		protected override string RelativeInstallPath
		{
			get
			{
				return "ClientAccess\\PopImap";
			}
		}
	}
}
