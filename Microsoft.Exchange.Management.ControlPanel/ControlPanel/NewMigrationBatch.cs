using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200022E RID: 558
	public class NewMigrationBatch : EcpWizardForm
	{
		// Token: 0x060027BF RID: 10175 RVA: 0x0007CF28 File Offset: 0x0007B128
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			string a = base.Request.QueryString["migration"];
			if (string.Equals(a, "localmove", StringComparison.OrdinalIgnoreCase))
			{
				this.wizardFormSheet.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationBatch&workflow=LocalMoveGetObjectForNew");
				base.Caption = Strings.NewLocalMoveCaption;
				return;
			}
			if (string.Equals(a, "crossforest", StringComparison.OrdinalIgnoreCase))
			{
				this.wizardFormSheet.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationBatch&workflow=OnboardingGetObjectForNew");
				base.Caption = Strings.NewCrossForestMoveCaption;
				return;
			}
			if (string.Equals(a, "onboarding", StringComparison.OrdinalIgnoreCase))
			{
				this.wizardFormSheet.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationBatch&workflow=OnboardingGetObjectForNew");
				base.Caption = Strings.NewMigrationBatchCaption;
				return;
			}
			if (string.Equals(a, "offboarding", StringComparison.OrdinalIgnoreCase))
			{
				this.wizardFormSheet.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationBatch&workflow=OffboardingGetObjectForNew");
				base.Caption = Strings.NewMigrationBatchCaption;
				return;
			}
			throw new BadQueryParameterException("migration");
		}

		// Token: 0x0400201D RID: 8221
		private const string MigrationScenario = "migration";

		// Token: 0x0400201E RID: 8222
		private const string LocalMove = "localmove";

		// Token: 0x0400201F RID: 8223
		private const string CrossForestMove = "crossforest";

		// Token: 0x04002020 RID: 8224
		private const string Onboarding = "onboarding";

		// Token: 0x04002021 RID: 8225
		private const string Offboarding = "offboarding";

		// Token: 0x04002022 RID: 8226
		protected WizardFormSheet wizardFormSheet;
	}
}
