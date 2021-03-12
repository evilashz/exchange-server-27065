using System;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001A1 RID: 417
	[ClientScriptResource("Navigation", "Microsoft.Exchange.Management.ControlPanel.Client.Navigation.js")]
	public class Navigation : ScriptComponent
	{
		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x0006B288 File Offset: 0x00069488
		private int HybridSyncTimeoutInSeconds
		{
			get
			{
				return Navigation.hybridSyncTimeoutInSeconds.Value;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x0006B294 File Offset: 0x00069494
		private bool DisableHybridSyncCheck
		{
			get
			{
				return Navigation.disableHybridEndDate.Value > DateTime.UtcNow;
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0006B2AA File Offset: 0x000694AA
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x0006B2B2 File Offset: 0x000694B2
		public bool HasHybridParameter { get; set; }

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x0006B2BB File Offset: 0x000694BB
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x0006B2C3 File Offset: 0x000694C3
		public NavigationTreeNode NavigationTree { get; set; }

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0006B2CC File Offset: 0x000694CC
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x0006B2D4 File Offset: 0x000694D4
		public string CloudServer { get; set; }

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0006B2DD File Offset: 0x000694DD
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x0006B2E5 File Offset: 0x000694E5
		public string ServerVersion { get; set; }

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x0006B2F0 File Offset: 0x000694F0
		private bool CloseWindowOnLogout
		{
			get
			{
				if (!this.IsLegacyLogOff || Util.IsDataCenter)
				{
					return false;
				}
				VdirConfiguration instance = VdirConfiguration.Instance;
				return !instance.FormBasedAuthenticationEnabled && !HttpContext.Current.Request.IsAuthenticatedByAdfs() && (instance.BasicAuthenticationEnabled || instance.WindowsAuthenticationEnabled || instance.DigestAuthenticationEnabled);
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x0006B347 File Offset: 0x00069547
		private bool IsLegacyLogOff
		{
			get
			{
				return LogOnSettings.IsLegacyLogOff;
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0006B350 File Offset: 0x00069550
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddScriptProperty("NavTree", this.NavigationTree.ToJsonString(null));
			descriptor.AddProperty("CloudServer", this.CloudServer, true);
			descriptor.AddProperty("ServerVersion", this.ServerVersion);
			if (this.HasHybridParameter)
			{
				descriptor.AddProperty("DisableHybridSyncCheck", this.DisableHybridSyncCheck);
				descriptor.AddProperty("HybridSyncTimeoutInSeconds", this.HybridSyncTimeoutInSeconds);
			}
			descriptor.AddProperty("CmdletLoggingEnabled", EacFlightUtility.GetSnapshotForCurrentUser().Eac.CmdletLogging.Enabled);
			descriptor.AddProperty("ShowPerfConsole", StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["ShowPerformanceConsole"]), true);
			descriptor.AddScriptProperty("RbacData", ClientRbac.GetRbacData().ToJsonString(null));
			descriptor.AddProperty("CloseWindowOnLogout", this.CloseWindowOnLogout, true);
			descriptor.AddProperty("IsLegacyLogOff", this.IsLegacyLogOff, true);
		}

		// Token: 0x04001DBF RID: 7615
		private static Lazy<DateTime> disableHybridEndDate = new Lazy<DateTime>(delegate()
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = utcNow.AddDays(30.0);
			string configStringValue = AppConfigLoader.GetConfigStringValue("DisableHybridSyncCheckEndDate", null);
			DateTime minValue = DateTime.MinValue;
			if (!string.IsNullOrEmpty(configStringValue))
			{
				if (!DateTime.TryParse(configStringValue, out minValue))
				{
					minValue = DateTime.MinValue;
				}
				if (minValue > t || minValue < utcNow)
				{
					EcpEventLogConstants.Tuple_ConfigurableValueOutOfRange.LogEvent(new object[]
					{
						"DisableHybridSyncCheckEndDate",
						utcNow.ToString("s"),
						t.ToString("s")
					});
					minValue = DateTime.MinValue;
				}
			}
			return minValue;
		});

		// Token: 0x04001DC0 RID: 7616
		private static Lazy<int> hybridSyncTimeoutInSeconds = new Lazy<int>(() => AppConfigLoader.GetConfigIntValue("HybridSyncTimeoutInSeconds", 0, int.MaxValue, 90));
	}
}
