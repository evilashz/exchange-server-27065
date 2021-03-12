using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200065D RID: 1629
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADConfigDriver : ConfigDriverBase
	{
		// Token: 0x06004C3B RID: 19515 RVA: 0x00119E28 File Offset: 0x00118028
		public ADConfigDriver(IConfigSchema schema) : this(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval))
		{
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x00119E3C File Offset: 0x0011803C
		public ADConfigDriver(IConfigSchema schema, TimeSpan? errorThresholdInterval) : base(schema, errorThresholdInterval)
		{
			this.nameFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.Schema.Name);
			this.ADSettingsCache = new ADObjectCache<InternalExchangeSettings, ConfigurationSettingsException>(new Func<InternalExchangeSettings[], InternalExchangeSettings[]>(this.LoadSettings), "SOFTWARE\\Microsoft\\Exchange_Test\\v15\\ConfigurationSettings");
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x00119E89 File Offset: 0x00118089
		// (set) Token: 0x06004C3E RID: 19518 RVA: 0x00119E91 File Offset: 0x00118091
		private ADObjectCache<InternalExchangeSettings, ConfigurationSettingsException> ADSettingsCache { get; set; }

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x00119E9C File Offset: 0x0011809C
		private InternalExchangeSettings ADSettings
		{
			get
			{
				if (this.ADSettingsCache.Value == null || this.ADSettingsCache.Value.Length <= 0)
				{
					return null;
				}
				if (this.ADSettingsCache.Value.Length > 1)
				{
					this.HandleLoadError(new ConfigurationSettingsNotUniqueException(base.Schema.Name));
				}
				return this.ADSettingsCache.Value[0];
			}
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x00119EFC File Offset: 0x001180FC
		public override bool TryGetBoxedSetting(ISettingsContext context, string settingName, Type settingType, out object settingValue)
		{
			InternalExchangeSettings adsettings = this.ADSettings;
			string serializedValue;
			if (adsettings != null && adsettings.TryGetConfig(base.Schema, context, settingName, out serializedValue))
			{
				settingValue = base.ParseAndValidateConfigValue(settingName, serializedValue, settingType);
				return true;
			}
			settingValue = null;
			return false;
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x00119F38 File Offset: 0x00118138
		public override XElement GetDiagnosticInfo(string argument)
		{
			ConfigDiagnosticArgument configDiagnosticArgument = new ConfigDiagnosticArgument(argument);
			if (configDiagnosticArgument.HasArgument("invokescan"))
			{
				if (!this.ADSettingsCache.IsInitialized)
				{
					throw new ConfigurationSettingsDriverNotInitializedException(base.GetType().ToString());
				}
				this.ADSettingsCache.Refresh(null);
			}
			XElement diagnosticInfo = base.GetDiagnosticInfo(argument);
			diagnosticInfo.Add(new XAttribute("LastModified", this.ADSettingsCache.LastModified));
			diagnosticInfo.Add(new XElement("description", "Contains overrides for values found in AppConfig or more directly in the schema file."));
			InternalExchangeSettings adsettings = this.ADSettings;
			if (adsettings != null)
			{
				diagnosticInfo.Add(adsettings.GetDiagnosticInfo(argument));
			}
			return diagnosticInfo;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x00119FE2 File Offset: 0x001181E2
		public override void Initialize()
		{
			if (base.IsInitialized)
			{
				return;
			}
			if (!this.ADSettingsCache.IsInitialized)
			{
				this.ADSettingsCache.Initialize(true);
			}
			base.IsInitialized = true;
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0011A00D File Offset: 0x0011820D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ADConfigDriver>(this);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0011A015 File Offset: 0x00118215
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.ADSettingsCache != null)
				{
					this.ADSettingsCache.Dispose();
				}
				this.ADSettingsCache = null;
			}
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0011A034 File Offset: 0x00118234
		protected override void HandleLoadError(Exception ex)
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_ConfigurationSettingsLoadError, base.Schema.Name, new object[]
			{
				ex.ToString()
			});
			base.HandleLoadError(ex);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0011A0D0 File Offset: 0x001182D0
		private InternalExchangeSettings[] LoadSettings(InternalExchangeSettings[] existingValue)
		{
			ADOperationResult adoperationResult = null;
			InternalExchangeSettings[] settingsList = null;
			bool flag = false;
			try
			{
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					IConfigurationSession configurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 235, "LoadSettings", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationSettings\\ADConfigDriver.cs");
					settingsList = configurationSession.Find<InternalExchangeSettings>(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetDescendantId(InternalExchangeSettings.ContainerRelativePath), QueryScope.OneLevel, this.nameFilter, null, 2);
				});
			}
			catch (LocalizedException innerException)
			{
				this.HandleLoadError(new ConfigurationSettingsADConfigDriverException(innerException));
				flag = true;
			}
			catch (InvalidOperationException innerException2)
			{
				this.HandleLoadError(new ConfigurationSettingsADConfigDriverException(innerException2));
				flag = true;
			}
			if (!flag)
			{
				if (!adoperationResult.Succeeded)
				{
					this.HandleLoadError(new ConfigurationSettingsADNotificationException(adoperationResult.Exception));
				}
				else
				{
					base.HandleLoadSuccess();
					if (settingsList != null && settingsList.Length > 0 && settingsList[0].WhenChanged != null)
					{
						base.LastUpdated = settingsList[0].WhenChanged.Value;
					}
					else if (settingsList == null && existingValue != null)
					{
						base.LastUpdated = DateTime.UtcNow;
					}
				}
			}
			return settingsList;
		}

		// Token: 0x04003446 RID: 13382
		private const string Description = "Contains overrides for values found in AppConfig or more directly in the schema file.";

		// Token: 0x04003447 RID: 13383
		private readonly QueryFilter nameFilter;
	}
}
