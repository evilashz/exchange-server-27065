using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000E5 RID: 229
	internal sealed class SettingOverrideSyncHandler : ExchangeDiagnosableWrapper<XElement>
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00025857 File Offset: 0x00023A57
		protected override string UsageText
		{
			get
			{
				return "Diagnostic info for Variant Configuration.";
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002585E File Offset: 0x00023A5E
		protected override string UsageSample
		{
			get
			{
				return "Get a list of current overrides for MSExchangeMailboxAssistants process:\r\n                         Get-ExchangeDiagnosticInfo -ProcessName MSExchangeMailboxAssistants -Component VariantConfiguration -Argument overrides\r\n\r\n                         Refresh overrides and show up-to-date ones:\r\n                         Get-ExchangeDiagnosticInfo -ProcessName MSExchangeMailboxAssistants -Component VariantConfiguration -Argument refresh\r\n\r\n                         Don't specify any arguments to see a full list of supported arguments:\r\n                         Get-ExchangeDiagnosticInfo -ProcessName MSExchangeMailboxAssistants -Component VariantConfiguration";
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00025868 File Offset: 0x00023A68
		public static SettingOverrideSyncHandler GetInstance()
		{
			if (SettingOverrideSyncHandler.instance == null)
			{
				lock (SettingOverrideSyncHandler.lockObject)
				{
					if (SettingOverrideSyncHandler.instance == null)
					{
						SettingOverrideSyncHandler.instance = new SettingOverrideSyncHandler();
					}
				}
			}
			return SettingOverrideSyncHandler.instance;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000258C0 File Offset: 0x00023AC0
		private SettingOverrideSyncHandler()
		{
			SettingOverrideSync.Instance.Start(true);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000258D3 File Offset: 0x00023AD3
		protected override string ComponentName
		{
			get
			{
				return SettingOverrideSync.Instance.GetDiagnosticComponentName();
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000258DF File Offset: 0x00023ADF
		internal override XElement GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			return SettingOverrideSync.Instance.GetDiagnosticInfo(argument);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000258EC File Offset: 0x00023AEC
		protected override void InternalOnStop()
		{
			SettingOverrideSync.Instance.Stop();
		}

		// Token: 0x04000481 RID: 1153
		private static SettingOverrideSyncHandler instance;

		// Token: 0x04000482 RID: 1154
		private static object lockObject = new object();
	}
}
