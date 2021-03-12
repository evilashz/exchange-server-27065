using System;
using System.Globalization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UninstallFileDataHandler : FileDataHandler
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public UninstallFileDataHandler(ISetupContext context, MsiConfigurationInfo msiConfig, MonadConnection connection) : base(context, "uninstall-msipackage", msiConfig, connection)
		{
			if (msiConfig is DatacenterMsiConfigurationInfo)
			{
				base.WorkUnit.Text = Strings.RemoveDatacenterFileText;
				return;
			}
			base.WorkUnit.Text = Strings.RemoveFileText;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000FE8A File Offset: 0x0000E08A
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000FE92 File Offset: 0x0000E092
		public bool IsUpgrade
		{
			get
			{
				return this.isUpgrade;
			}
			set
			{
				this.isUpgrade = value;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000FE9C File Offset: 0x0000E09C
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("ProductCode", this.MsiConfigurationInfo.ProductCode);
			if (this.IsUpgrade)
			{
				base.Parameters.AddWithValue("PropertyValues", string.Format("BYPASS_CONFIGURED_CHECK=1 DEFAULTLANGUAGENAME={0}", CultureInfo.InstalledUICulture.ThreeLetterWindowsLanguageName));
			}
			else
			{
				base.Parameters.AddWithValue("WarnOnRebootRequests", true);
				int num = 0;
				foreach (string installableUnitName in base.SelectedInstallableUnits)
				{
					if (!InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(installableUnitName))
					{
						num++;
					}
				}
				bool flag = true;
				if (num < base.SetupContext.UnpackedRoles.Count)
				{
					base.Parameters.AddWithValue("features", base.GetFeatures().ToArray());
					flag = false;
				}
				base.Parameters.AddWithValue("PropertyValues", string.Format("DEFAULTLANGUAGENAME={0} COMPLETEUNINSTALLATION={1}", base.SetupContext.ExchangeCulture.ThreeLetterWindowsLanguageName, flag ? 1 : 0));
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x0400018F RID: 399
		private bool isUpgrade;
	}
}
