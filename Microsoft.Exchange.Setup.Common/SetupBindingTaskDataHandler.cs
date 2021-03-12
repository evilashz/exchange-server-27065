using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetupBindingTaskDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		public SetupBindingTaskDataHandler(BindingCategory category, ISetupContext context, MonadConnection connection) : base(context, "Start-" + category.ToString(), connection)
		{
			this.SelectedInstallableUnits = null;
			base.ImplementsDatacenterMode = true;
			base.ImplementsDatacenterDedicatedMode = true;
			base.ImplementsPartnerHostedMode = true;
			switch (category)
			{
			case BindingCategory.PreSetup:
				base.WorkUnit.Text = Strings.PreSetupText;
				break;
			case BindingCategory.PreFileCopy:
				base.WorkUnit.Text = Strings.PreFileCopyText;
				break;
			case BindingCategory.MidFileCopy:
				base.WorkUnit.Text = Strings.MidFileCopyText;
				break;
			case BindingCategory.PostFileCopy:
				base.WorkUnit.Text = Strings.PostFileCopyText;
				break;
			case BindingCategory.PostSetup:
				base.WorkUnit.Text = Strings.PostSetupText;
				break;
			}
			base.SetupContext = context;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public InstallationModes Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000D8B9 File Offset: 0x0000BAB9
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000D8C1 File Offset: 0x0000BAC1
		public List<string> SelectedInstallableUnits { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D8CA File Offset: 0x0000BACA
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000D8D2 File Offset: 0x0000BAD2
		public Version PreviousVersion { get; set; }

		// Token: 0x060003E0 RID: 992 RVA: 0x0000D8DC File Offset: 0x0000BADC
		protected override void AddParameters()
		{
			base.AddParameters();
			if (base.SetupContext.IsDatacenter && base.SetupContext.IsFfo)
			{
				base.Parameters.AddWithValue("IsFfo", true);
			}
			if (this.PreviousVersion != null)
			{
				base.Parameters.AddWithValue("PreviousVersion", this.PreviousVersion);
			}
			base.Parameters.AddWithValue("Mode", this.Mode);
			base.Parameters.AddWithValue("Roles", this.GetRoleNames());
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000D978 File Offset: 0x0000BB78
		private string[] GetRoleNames()
		{
			List<string> list = new List<string>();
			foreach (string roleName in this.SelectedInstallableUnits)
			{
				Role roleByName = RoleManager.GetRoleByName(roleName);
				if (roleByName != null && (!(roleByName.RoleName == LanguagePacksRole.ClassRoleName) || (!base.SetupContext.IsCleanMachine && InstallationModes.Install == this.Mode)))
				{
					list.Add(roleByName.RoleName);
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000144 RID: 324
		private InstallationModes mode;
	}
}
