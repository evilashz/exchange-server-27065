using System;
using System.Globalization;
using System.Web.Configuration;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005C9 RID: 1481
	[ClientScriptResource("EcpProfile", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class EcpProfile : ScriptComponent
	{
		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x000CB76A File Offset: 0x000C996A
		public static string CurrentLanguage
		{
			get
			{
				return CultureInfo.CurrentCulture.IetfLanguageTag;
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x000CB776 File Offset: 0x000C9976
		private string DisplayName
		{
			get
			{
				return RbacPrincipal.Current.RbacConfiguration.ExecutingUserDisplayName;
			}
		}

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x000CB788 File Offset: 0x000C9988
		private string UserIDHash
		{
			get
			{
				return RbacPrincipal.Current.Identity.Name.GetHashCode().ToString();
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000CB7B1 File Offset: 0x000C99B1
		private string Theme
		{
			get
			{
				return ThemeResource.Private_GetThemeResource(this, string.Empty);
			}
		}

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x000CB7BE File Offset: 0x000C99BE
		private bool EnableWizardNextOnError
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["EnableWizardNextOnError"]);
			}
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000CB7DE File Offset: 0x000C99DE
		private bool GetResilienceEnabled()
		{
			return StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["ResilienceEnabled"]);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000CB800 File Offset: 0x000C9A00
		private bool GetIsInstrumentationEnabled()
		{
			Random random = new Random();
			double num = (double)random.Next(0, 100000) / 100000.0;
			double configDoubleValue = AppConfigLoader.GetConfigDoubleValue("ClientInstrumentationProbability", 0.0, 1.0, 1.0);
			return num < configDoubleValue;
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000CB858 File Offset: 0x000C9A58
		private int GetInstrumentationUploadDuration()
		{
			return AppConfigLoader.GetConfigIntValue("ClientInstrumentationUploadDuration", 1, int.MaxValue, 55);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000CB87C File Offset: 0x000C9A7C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("DisplayName", this.DisplayName, true);
			descriptor.AddProperty("Language", EcpProfile.CurrentLanguage, true);
			descriptor.AddProperty("UserIDHash", this.UserIDHash, true);
			descriptor.AddProperty("Theme", this.Theme, true);
			descriptor.AddProperty("ScriptPath", ThemeResource.ScriptPath, true);
			descriptor.AddProperty("DecodedDirectionMark", RtlUtil.DecodedDirectionMark);
			descriptor.AddProperty("IsDataCenter", Util.IsDataCenter);
			descriptor.AddProperty("IsCrossPremiseMigration", VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Eac.CrossPremiseMigration.Enabled);
			descriptor.AddProperty("AllowMailboxArchiveOnlyMigration", VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Eac.AllowMailboxArchiveOnlyMigration.Enabled);
			descriptor.AddProperty("AllowRemoteOnboardingMovesOnly", VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Eac.AllowRemoteOnboardingMovesOnly.Enabled);
			descriptor.AddProperty("IsInstrumentationEnabled", this.GetIsInstrumentationEnabled());
			descriptor.AddProperty("InstrumentationUploadDuration", this.GetInstrumentationUploadDuration());
			descriptor.AddProperty("IsResilienceEnabled", this.GetResilienceEnabled());
			descriptor.AddProperty("EnableWizardNextOnError", this.EnableWizardNextOnError, true);
			descriptor.AddProperty("HighContrastCssFile", CssFiles.HighContrastCss.ToUrl(this));
		}
	}
}
