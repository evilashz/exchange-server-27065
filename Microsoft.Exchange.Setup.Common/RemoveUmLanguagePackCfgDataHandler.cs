using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoveUmLanguagePackCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public RemoveUmLanguagePackCfgDataHandler(ISetupContext context, MonadConnection connection, CultureInfo culture) : base(context, "", "remove-umlanguagepack", connection)
		{
			this.Culture = culture;
			string umLanguagePackNameForCultureInfo = UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(this.Culture);
			InstallableUnitConfigurationInfo installableUnitConfigurationInfo = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(umLanguagePackNameForCultureInfo);
			if (installableUnitConfigurationInfo == null)
			{
				installableUnitConfigurationInfo = new UmLanguagePackConfigurationInfo(this.Culture);
				InstallableUnitConfigurationInfoManager.AddInstallableUnit(umLanguagePackNameForCultureInfo, installableUnitConfigurationInfo);
			}
			base.InstallableUnitName = installableUnitConfigurationInfo.Name;
			this.productCode = this.GetProductGuidForCulture("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\LanguagePacks\\", this.Culture, false);
			this.teleProductCode = this.GetProductGuidForCulture("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TeleLanguagePacks\\", this.Culture, false);
			this.transProductCode = this.GetProductGuidForCulture("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TransLanguagePacks\\", this.Culture, true);
			this.ttsProductCode = this.GetProductGuidForCulture("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TtsLanguagePacks\\", this.Culture, false);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.Log(Strings.RemoveUmLanguagePackLogFilePath(this.LogFilePath));
			base.Parameters.AddWithValue("logfilepath", this.LogFilePath);
			base.Parameters.AddWithValue("propertyvalues", "ESE=1");
			base.Parameters.AddWithValue("ProductCode", this.ProductCode);
			base.Parameters.AddWithValue("TeleProductCode", this.TeleProductCode);
			base.Parameters.AddWithValue("TransProductCode", this.TransProductCode);
			base.Parameters.AddWithValue("TtsProductCode", this.TtsProductCode);
			base.Parameters.AddWithValue("Language", this.Culture);
			SetupLogger.TraceExit();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public Guid GetProductGuidForCulture(string keyPath, CultureInfo culture, bool onlyIfInstalled)
		{
			Guid result = Guid.Empty;
			string text = (string)Registry.GetValue(keyPath, culture.ToString(), null);
			if (!string.IsNullOrEmpty(text))
			{
				Guid guid = new Guid(text);
				if (!onlyIfInstalled || MsiUtility.IsInstalled(guid))
				{
					result = guid;
				}
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public override ValidationError[] ValidateConfiguration()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>(base.ValidateConfiguration());
			if (this.ProductCode == Guid.Empty)
			{
				list.Add(new SetupValidationError(Strings.UmLanguagePackNotFoundForCulture(this.Culture.ToString())));
			}
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public override void UpdatePreCheckTaskDataHandler()
		{
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000CA3E File Offset: 0x0000AC3E
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000CA46 File Offset: 0x0000AC46
		public bool WatsonEnabled
		{
			get
			{
				return this.watsonEnabled;
			}
			set
			{
				this.watsonEnabled = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000CA4F File Offset: 0x0000AC4F
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000CA57 File Offset: 0x0000AC57
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			private set
			{
				this.culture = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public string LogFilePath
		{
			get
			{
				string path = "remove-" + RemoveUmLanguagePackCfgDataHandler.msiFilePrefix + this.Culture.ToString() + RemoveUmLanguagePackCfgDataHandler.logExtension;
				return Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, path);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000CA98 File Offset: 0x0000AC98
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		public Guid ProductCode
		{
			get
			{
				return this.productCode;
			}
			set
			{
				this.productCode = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000CAB1 File Offset: 0x0000ACB1
		public Guid TeleProductCode
		{
			get
			{
				return this.teleProductCode;
			}
			set
			{
				this.teleProductCode = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000CABA File Offset: 0x0000ACBA
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		public Guid TransProductCode
		{
			get
			{
				return this.transProductCode;
			}
			set
			{
				this.transProductCode = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000CACB File Offset: 0x0000ACCB
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000CAD3 File Offset: 0x0000ACD3
		public Guid TtsProductCode
		{
			get
			{
				return this.ttsProductCode;
			}
			set
			{
				this.ttsProductCode = value;
			}
		}

		// Token: 0x0400010C RID: 268
		private bool watsonEnabled;

		// Token: 0x0400010D RID: 269
		private CultureInfo culture;

		// Token: 0x0400010E RID: 270
		private static string msiFilePrefix = "UMLanguagePack.";

		// Token: 0x0400010F RID: 271
		private static string logExtension = ".msilog";

		// Token: 0x04000110 RID: 272
		private Guid productCode;

		// Token: 0x04000111 RID: 273
		private Guid teleProductCode;

		// Token: 0x04000112 RID: 274
		private Guid transProductCode;

		// Token: 0x04000113 RID: 275
		private Guid ttsProductCode;
	}
}
