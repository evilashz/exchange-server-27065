using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000264 RID: 612
	[Cmdlet("Start", "PreConfiguration", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class StartPreConfiguration : ManageSetupBindingTasks
	{
		// Token: 0x060016C9 RID: 5833 RVA: 0x000612C4 File Offset: 0x0005F4C4
		public StartPreConfiguration()
		{
			base.ImplementsResume = false;
			base.Fields["InstallWindowsComponents"] = false;
			base.Fields["ADToolsNeeded"] = false;
			base.SetFileSearchPath(null);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00061328 File Offset: 0x0005F528
		private bool IsADServerRole(string role)
		{
			return Array.Exists<string>(StartPreConfiguration.ADServerRoles, (string item) => item == role);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00061358 File Offset: 0x0005F558
		private bool IsServerRole(string role)
		{
			return this.IsADServerRole(role) || role == StartPreConfiguration.GatewayRole;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00061370 File Offset: 0x0005F570
		private bool IsAdminToolsRole(string role)
		{
			return role == StartPreConfiguration.AdminToolsRole;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0006137D File Offset: 0x0005F57D
		private bool IsLanguagePacks(string role)
		{
			return role == StartPreConfiguration.LanguagePacks;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0006138A File Offset: 0x0005F58A
		private bool IsSupportedRole(string role)
		{
			return this.IsServerRole(role) || this.IsAdminToolsRole(role) || this.IsLanguagePacks(role);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x000613A7 File Offset: 0x0005F5A7
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartPreConfigurationDescription;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000613AE File Offset: 0x0005F5AE
		// (set) Token: 0x060016D1 RID: 5841 RVA: 0x000613C5 File Offset: 0x0005F5C5
		[Parameter(Mandatory = false)]
		public bool InstallWindowsComponents
		{
			get
			{
				return (bool)base.Fields["InstallWindowsComponents"];
			}
			set
			{
				base.Fields["InstallWindowsComponents"] = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x000613DD File Offset: 0x0005F5DD
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x000613F4 File Offset: 0x0005F5F4
		[Parameter(Mandatory = false)]
		public bool ADToolsNeeded
		{
			get
			{
				return (bool)base.Fields["ADToolsNeeded"];
			}
			set
			{
				base.Fields["ADToolsNeeded"] = value;
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0006140C File Offset: 0x0005F60C
		protected override void PopulateComponentInfoFileNames()
		{
			foreach (string role in this.Roles)
			{
				if (this.IsAdminToolsRole(role))
				{
					base.ComponentInfoFileNames.Add("setup\\data\\AdminToolsPreConfig.xml");
				}
			}
			bool flag = false;
			foreach (string role2 in this.Roles)
			{
				if (this.IsServerRole(role2))
				{
					flag = true;
					base.ComponentInfoFileNames.Add("setup\\data\\AllServerRolesPreConfig.xml");
					break;
				}
			}
			foreach (string role3 in this.Roles)
			{
				if (this.IsADServerRole(role3))
				{
					base.ComponentInfoFileNames.Add("setup\\data\\AllADRolesPreConfig.xml");
					break;
				}
			}
			foreach (string text in this.Roles)
			{
				if (this.IsServerRole(text))
				{
					base.ComponentInfoFileNames.Add(string.Format("setup\\data\\{0}PreConfig.xml", text.Replace("Role", "")));
				}
			}
			foreach (string role4 in this.Roles)
			{
				if (this.IsLanguagePacks(role4))
				{
					base.ComponentInfoFileNames.Add("setup\\data\\LanguagePacksPreConfig.xml");
					break;
				}
			}
			if (flag)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\AllServerRolesPreConfigLast.xml");
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00061574 File Offset: 0x0005F774
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalProcessRecord();
			}
			catch (NoComponentInfoFilesException ex)
			{
				base.WriteVerbose(ex.LocalizedString);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x040009E5 RID: 2533
		private static readonly string[] ADServerRoles = new string[]
		{
			"MailboxRole",
			"UnifiedMessagingRole",
			"ClientAccessRole",
			"BridgeheadRole",
			"FrontendTransportRole",
			"OSPRole",
			"CafeRole"
		};

		// Token: 0x040009E6 RID: 2534
		private static readonly string GatewayRole = "GatewayRole";

		// Token: 0x040009E7 RID: 2535
		private static readonly string AdminToolsRole = "AdminToolsRole";

		// Token: 0x040009E8 RID: 2536
		private static readonly string LanguagePacks = "LanguagePacks";
	}
}
