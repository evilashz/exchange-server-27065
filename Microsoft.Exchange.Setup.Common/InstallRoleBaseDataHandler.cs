using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallRoleBaseDataHandler : ConfigurationDataHandler
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000072A2 File Offset: 0x000054A2
		public InstallRoleBaseDataHandler(ISetupContext context, string installableUnitName, string commandText, MonadConnection connection) : base(context, installableUnitName, commandText, connection)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000072B0 File Offset: 0x000054B0
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			if (base.SetupContext.IsDatacenter && base.SetupContext.IsFfo)
			{
				base.Parameters.AddWithValue("IsFfo", true);
			}
			base.Parameters.AddWithValue("updatesdir", this.UpdatesDir);
			if (InstallableUnitConfigurationInfoManager.IsServerRoleBasedConfigurableInstallableUnit(base.InstallableUnitName) && base.SetupContext.ServerCustomerFeedbackEnabled != base.SetupContext.OriginalServerCustomerFeedbackEnabled)
			{
				base.Parameters.AddWithValue("CustomerFeedbackEnabled", base.SetupContext.ServerCustomerFeedbackEnabled);
			}
			base.Parameters.AddWithValue("LanguagePacksPath", base.GetMsiSourcePath());
			SetupLogger.TraceExit();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000073A0 File Offset: 0x000055A0
		public override void UpdatePreCheckTaskDataHandler()
		{
			base.UpdatePreCheckTaskDataHandler();
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.CustomerFeedbackEnabled = base.SetupContext.ServerCustomerFeedbackEnabled;
			instance.ActiveDirectorySplitPermissions = base.SetupContext.ActiveDirectorySplitPermissions;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000073E7 File Offset: 0x000055E7
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000073F4 File Offset: 0x000055F4
		private LongPath UpdatesDir
		{
			get
			{
				return base.SetupContext.UpdatesDir;
			}
			set
			{
				base.SetupContext.UpdatesDir = value;
			}
		}
	}
}
