using System;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000639 RID: 1593
	public abstract class ManagePop3BeService : ManagePopImapService
	{
		// Token: 0x06003806 RID: 14342 RVA: 0x000E80D1 File Offset: 0x000E62D1
		protected ManagePop3BeService()
		{
			base.AddFirewallRule(new MSExchangePOP3BeFirewallRule());
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000E80E4 File Offset: 0x000E62E4
		protected override string Name
		{
			get
			{
				return "MSExchangePOP3BE";
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000E80EB File Offset: 0x000E62EB
		protected override string ServiceDisplayName
		{
			get
			{
				return Strings.Pop3BeServiceDisplayName;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000E80F7 File Offset: 0x000E62F7
		protected override string ServiceDescription
		{
			get
			{
				return Strings.Pop3BeServiceDescription;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000E8103 File Offset: 0x000E6303
		protected override string ServiceFile
		{
			get
			{
				return "Microsoft.Exchange.Pop3Service.exe";
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x000E810A File Offset: 0x000E630A
		protected override string ServiceCategoryName
		{
			get
			{
				return "MSExchange POP3 backend service";
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000E8111 File Offset: 0x000E6311
		protected override string WorkingProcessFile
		{
			get
			{
				return "Microsoft.Exchange.Pop3.exe";
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000E8118 File Offset: 0x000E6318
		protected override string WorkingProcessEventMessageFile
		{
			get
			{
				return "Microsoft.Exchange.Pop3.EventLog.dll";
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000E811F File Offset: 0x000E631F
		protected override string RelativeInstallPath
		{
			get
			{
				return "ClientAccess\\PopImap";
			}
		}
	}
}
