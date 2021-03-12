using System;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200026E RID: 622
	public class UmLanguagePack
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00062ED4 File Offset: 0x000610D4
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x00062EDC File Offset: 0x000610DC
		public string Name { get; private set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00062EE5 File Offset: 0x000610E5
		public Guid ProductCode
		{
			get
			{
				if (UmLanguagePackUtils.CurrentTargetPlatform == TargetPlatform.X64)
				{
					return this.X64ProductCode;
				}
				return this.X86ProductCode;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00062EFC File Offset: 0x000610FC
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00062F04 File Offset: 0x00061104
		public Guid X86ProductCode { get; private set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00062F0D File Offset: 0x0006110D
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x00062F15 File Offset: 0x00061115
		public Guid X64ProductCode { get; private set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00062F1E File Offset: 0x0006111E
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x00062F26 File Offset: 0x00061126
		public Guid TeleProductCode { get; private set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00062F2F File Offset: 0x0006112F
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x00062F37 File Offset: 0x00061137
		public Guid TransProductCode { get; private set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00062F40 File Offset: 0x00061140
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x00062F48 File Offset: 0x00061148
		public Guid TtsProductCode { get; private set; }

		// Token: 0x06001740 RID: 5952 RVA: 0x00062F51 File Offset: 0x00061151
		public UmLanguagePack(string name, Guid x86ProductCode, Guid x64ProductCode, Guid teleProductCode, Guid transProductCode, Guid ttsProductCode)
		{
			this.Name = name;
			this.X86ProductCode = x86ProductCode;
			this.X64ProductCode = x64ProductCode;
			this.TeleProductCode = teleProductCode;
			this.TransProductCode = transProductCode;
			this.TtsProductCode = ttsProductCode;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00062F88 File Offset: 0x00061188
		private static void PerformRegistryOperation(string languagePackType, string culture, GrayException.UserCodeDelegate function)
		{
			try
			{
				function();
			}
			catch (SecurityException innerException)
			{
				string regKeyPath = UmLanguagePack.GetRegKeyPath(true, languagePackType);
				throw new RegistryInsufficientPermissionException(regKeyPath, culture, innerException);
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00063024 File Offset: 0x00061224
		public void AddProductCodesToRegistry()
		{
			UmLanguagePack.PerformRegistryOperation("LanguagePacks", this.Name, delegate
			{
				this.AddRegKeyValue("LanguagePacks", this.Name, this.ProductCode);
			});
			UmLanguagePack.PerformRegistryOperation("TeleLanguagePacks", this.Name, delegate
			{
				this.AddRegKeyValue("TeleLanguagePacks", this.Name, this.TeleProductCode);
			});
			UmLanguagePack.PerformRegistryOperation("TransLanguagePacks", this.Name, delegate
			{
				this.AddRegKeyValue("TransLanguagePacks", this.Name, this.TransProductCode);
			});
			UmLanguagePack.PerformRegistryOperation("TtsLanguagePacks", this.Name, delegate
			{
				this.AddRegKeyValue("TtsLanguagePacks", this.Name, this.TtsProductCode);
			});
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000630F0 File Offset: 0x000612F0
		public void RemoveProductCodesFromRegistry()
		{
			UmLanguagePack.PerformRegistryOperation("LanguagePacks", this.Name, delegate
			{
				this.DeleteRegKeyAndValue("LanguagePacks", this.Name);
			});
			UmLanguagePack.PerformRegistryOperation("TeleLanguagePacks", this.Name, delegate
			{
				this.DeleteRegKeyAndValue("TeleLanguagePacks", this.Name);
			});
			UmLanguagePack.PerformRegistryOperation("TransLanguagePacks", this.Name, delegate
			{
				this.DeleteRegKeyAndValue("TransLanguagePacks", this.Name);
			});
			UmLanguagePack.PerformRegistryOperation("TtsLanguagePacks", this.Name, delegate
			{
				this.DeleteRegKeyAndValue("TtsLanguagePacks", this.Name);
			});
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00063170 File Offset: 0x00061370
		private void AddRegKeyValue(string languagePackType, string culture, Guid productCode)
		{
			string regKeyPath = UmLanguagePack.GetRegKeyPath(true, languagePackType);
			Registry.SetValue(regKeyPath, culture, productCode.ToString(), RegistryValueKind.String);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0006319C File Offset: 0x0006139C
		private void DeleteRegKeyAndValue(string key, string value)
		{
			string regKeyPath = UmLanguagePack.GetRegKeyPath(false, key);
			bool flag;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKeyPath, true))
			{
				registryKey.DeleteValue(value, false);
				string[] valueNames = registryKey.GetValueNames();
				flag = (valueNames.Length > 0);
			}
			if (!flag)
			{
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", true))
				{
					registryKey2.DeleteSubKey(key);
				}
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0006322C File Offset: 0x0006142C
		private static string GetRegKeyPath(bool includeHKLM, string languagePackType)
		{
			if (!includeHKLM)
			{
				return string.Format("{0}\\{1}\\", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", languagePackType);
			}
			return string.Format("HKEY_LOCAL_MACHINE\\{0}\\{1}\\", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", languagePackType);
		}

		// Token: 0x040009EE RID: 2542
		private const string LanguagePacks = "LanguagePacks";

		// Token: 0x040009EF RID: 2543
		private const string TeleLanguagePacks = "TeleLanguagePacks";

		// Token: 0x040009F0 RID: 2544
		private const string TransLanguagePacks = "TransLanguagePacks";

		// Token: 0x040009F1 RID: 2545
		private const string TtsLanguagePacks = "TtsLanguagePacks";
	}
}
