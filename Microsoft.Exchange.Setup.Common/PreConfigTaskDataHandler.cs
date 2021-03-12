using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PreConfigTaskDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000B355 File Offset: 0x00009555
		public PreConfigTaskDataHandler(ISetupContext context, MonadConnection connection) : base(context, "Start-PreConfiguration", connection)
		{
			base.WorkUnit.Text = Strings.PreConfigurationDisplayName;
			base.WorkUnit.CanShowExecutedCommand = false;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000B385 File Offset: 0x00009585
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000B38D File Offset: 0x0000958D
		public List<string> SelectedInstallableUnits
		{
			get
			{
				return this.selectedInstallableUnits;
			}
			set
			{
				this.selectedInstallableUnits = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000B398 File Offset: 0x00009598
		public string[] Roles
		{
			get
			{
				List<string> list = new List<string>();
				if (this.SelectedInstallableUnits != null)
				{
					foreach (string text in this.SelectedInstallableUnits)
					{
						if (!InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(text))
						{
							list.Add(text);
						}
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000B408 File Offset: 0x00009608
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			if (base.SetupContext.IsDatacenter)
			{
				base.Parameters.AddWithValue("IsDatacenter", base.SetupContext.IsDatacenter);
			}
			if (base.SetupContext.IsDatacenterDedicated)
			{
				base.Parameters.AddWithValue("IsDatacenterDedicated", base.SetupContext.IsDatacenterDedicated);
			}
			base.Parameters.AddWithValue("Mode", base.SetupContext.InstallationMode);
			base.Parameters.AddWithValue("Roles", this.Roles);
			if (base.SetupContext.InstallWindowsComponents)
			{
				base.Parameters.AddWithValue("InstallWindowsComponents", true);
			}
			if (base.SetupContext.IsSchemaUpdateRequired || base.SetupContext.IsOrgConfigUpdateRequired)
			{
				base.Parameters.AddWithValue("ADToolsNeeded", true);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x040000C6 RID: 198
		private const string installWindowsComponentsArgument = "InstallWindowsComponents";

		// Token: 0x040000C7 RID: 199
		private const string adToolsNeededArgument = "ADToolsNeeded";

		// Token: 0x040000C8 RID: 200
		private List<string> selectedInstallableUnits;
	}
}
