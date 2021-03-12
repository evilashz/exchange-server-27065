using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000066 RID: 102
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UninstallOrgCfgDataHandler : OrgCfgDataHandler
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00010FAC File Offset: 0x0000F1AC
		public UninstallOrgCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "Uninstall-ExchangeOrganization", connection)
		{
			base.WorkUnit.Text = Strings.OrganizationInstallText;
			this.setRemoveOrganization = false;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		private void DetermineParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			this.setRemoveOrganization = false;
			SetupLogger.Log(Strings.DeterminingOrgPrepParameters);
			if (base.SetupContext.CanOrgBeRemoved)
			{
				this.setRemoveOrganization = true;
				SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("RemoveOrganization"));
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00011029 File Offset: 0x0000F229
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			this.DetermineParameters();
			if (this.setRemoveOrganization)
			{
				base.Parameters.AddWithValue("RemoveOrganization", true);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00011068 File Offset: 0x0000F268
		public override void UpdatePreCheckTaskDataHandler()
		{
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.AddRole("Global");
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011094 File Offset: 0x0000F294
		public override bool WillDataHandlerDoAnyWork()
		{
			bool result = false;
			this.DetermineParameters();
			if (this.setRemoveOrganization)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0400019A RID: 410
		private const string removeOrganizationArgument = "RemoveOrganization";

		// Token: 0x0400019B RID: 411
		private bool setRemoveOrganization;
	}
}
