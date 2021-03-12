using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F4 RID: 500
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AppConfigDriver : ConfigDriverBase
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0003437F File Offset: 0x0003257F
		public AppConfigDriver(IConfigSchema schema) : this(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval))
		{
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00034392 File Offset: 0x00032592
		public AppConfigDriver(IConfigSchema schema, TimeSpan? errorThresholdInterval) : base(schema, errorThresholdInterval)
		{
			this.section = null;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x000343A3 File Offset: 0x000325A3
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x000343AB File Offset: 0x000325AB
		protected string ConfigFilePath { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x000343E0 File Offset: 0x000325E0
		protected ConfigurationSection Section
		{
			get
			{
				ConfigurationSection result;
				lock (this)
				{
					if (this.section == null)
					{
						this.RunConfigurationOperation(2, delegate
						{
							Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
							this.section = configuration.GetSection(base.Schema.SectionName);
						});
					}
					result = this.section;
				}
				return result;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00034440 File Offset: 0x00032640
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x00034448 File Offset: 0x00032648
		private FileSystemWatcher ConfigWatcher { get; set; }

		// Token: 0x0600114C RID: 4428 RVA: 0x00034454 File Offset: 0x00032654
		public override bool TryGetBoxedSetting(ISettingsContext context, string settingName, Type settingType, out object settingValue)
		{
			ConfigurationSection configurationSection = this.Section;
			if (configurationSection != null)
			{
				ExchangeConfigurationSection exchangeConfigurationSection = configurationSection as ExchangeConfigurationSection;
				if (exchangeConfigurationSection != null)
				{
					object propertyValue = exchangeConfigurationSection.GetPropertyValue(settingName);
					if (propertyValue != exchangeConfigurationSection.GetConfigurationProperty(settingName, null).DefaultValue)
					{
						settingValue = propertyValue;
						return true;
					}
				}
				else
				{
					AppSettingsSection appSettingsSection = configurationSection as AppSettingsSection;
					KeyValueConfigurationElement keyValueConfigurationElement = appSettingsSection.Settings[settingName];
					if (keyValueConfigurationElement != null)
					{
						settingValue = base.ParseAndValidateConfigValue(settingName, keyValueConfigurationElement.Value, settingType);
						return true;
					}
				}
			}
			settingValue = null;
			return false;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000344C8 File Offset: 0x000326C8
		public override XElement GetDiagnosticInfo(string argument)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(argument);
			diagnosticInfo.Add(new XElement("description", "App config contains DEFAULTS.  They may be overrided by updating app.config directly but it's not recommended"));
			ConfigurationSection configurationSection = this.Section;
			if (configurationSection != null)
			{
				XElement xelement = new XElement(base.Schema.SectionName);
				ExchangeConfigurationSection exchangeConfigurationSection = configurationSection as ExchangeConfigurationSection;
				if (exchangeConfigurationSection != null)
				{
					using (IEnumerator<string> enumerator = exchangeConfigurationSection.Settings.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							xelement.Add(new XElement(text, exchangeConfigurationSection.GetPropertyValue(text)));
						}
						goto IL_FC;
					}
				}
				AppSettingsSection appSettingsSection = configurationSection as AppSettingsSection;
				foreach (object obj in appSettingsSection.Settings)
				{
					KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
					xelement.Add(new XElement(keyValueConfigurationElement.Key, keyValueConfigurationElement.Value));
				}
				IL_FC:
				diagnosticInfo.Add(xelement);
			}
			return diagnosticInfo;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000345F8 File Offset: 0x000327F8
		public override void Initialize()
		{
			if (base.IsInitialized)
			{
				return;
			}
			this.ConfigFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
			this.InitializeFileWatcher();
			base.IsInitialized = true;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00034621 File Offset: 0x00032821
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AppConfigDriver>(this);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00034629 File Offset: 0x00032829
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.ConfigWatcher != null)
				{
					this.ConfigWatcher.Dispose();
				}
				this.ConfigWatcher = null;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00034648 File Offset: 0x00032848
		private void RunConfigurationOperation(int maxSeconds, Action operation)
		{
			for (int i = 1; i > 0; i <<= 1)
			{
				try
				{
					operation();
					base.HandleLoadSuccess();
					break;
				}
				catch (Exception ex)
				{
					if (!(ex is IOException) && !(ex is ConfigurationErrorsException))
					{
						throw;
					}
					if (2 * i > maxSeconds)
					{
						this.HandleLoadError(new ConfigurationSettingsAppSettingsException(ex));
					}
					Thread.Sleep(TimeSpan.FromSeconds((double)i));
				}
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000346B4 File Offset: 0x000328B4
		private void InitializeFileWatcher()
		{
			this.ConfigWatcher = new FileSystemWatcher();
			this.ConfigWatcher.Path = Path.GetDirectoryName(this.ConfigFilePath);
			string fileName = Path.GetFileName(this.ConfigFilePath);
			this.ConfigWatcher.Filter = fileName;
			this.ConfigWatcher.NotifyFilter = NotifyFilters.LastWrite;
			this.ConfigWatcher.Changed += this.FileWatcherChanged;
			this.ConfigWatcher.EnableRaisingEvents = true;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00034784 File Offset: 0x00032984
		private void FileWatcherChanged(object sender, FileSystemEventArgs e)
		{
			if (!e.FullPath.EndsWith(this.ConfigFilePath, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			this.RunConfigurationOperation(120, delegate
			{
				ConfigurationManager.RefreshSection(base.Schema.SectionName);
				lock (this)
				{
					this.section = null;
				}
				base.LastUpdated = DateTime.UtcNow;
			});
		}

		// Token: 0x04000A9D RID: 2717
		private const string Description = "App config contains DEFAULTS.  They may be overrided by updating app.config directly but it's not recommended";

		// Token: 0x04000A9E RID: 2718
		private ConfigurationSection section;
	}
}
