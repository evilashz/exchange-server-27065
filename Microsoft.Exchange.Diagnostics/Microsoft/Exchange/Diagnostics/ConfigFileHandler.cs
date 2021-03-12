using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Configuration;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000077 RID: 119
	internal sealed class ConfigFileHandler : IDisposable
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00008CBB File Offset: 0x00006EBB
		public FileHandler FileHandler
		{
			get
			{
				return this.fileHandler;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00008CC3 File Offset: 0x00006EC3
		public string ConfigFilePath
		{
			get
			{
				return this.configFilePath;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008CCB File Offset: 0x00006ECB
		public ConfigFileHandler(string key, string defaultFileName)
		{
			this.key = key;
			this.defaultFileName = defaultFileName;
			this.configFilePath = this.GetFilePath();
			this.fileHandler = new FileHandler(this.configFilePath);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00008CFE File Offset: 0x00006EFE
		public void Dispose()
		{
			this.fileHandler.Dispose();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00008D0B File Offset: 0x00006F0B
		internal void SetConfigSource(string configSource, string siteName)
		{
			this.configSource = configSource;
			this.siteName = siteName;
			this.UpdateConfigFilePath();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008D24 File Offset: 0x00006F24
		internal void UpdateConfigFilePath()
		{
			string filePath = this.GetFilePath();
			if (!StringComparer.OrdinalIgnoreCase.Equals(this.configFilePath, filePath))
			{
				this.configFilePath = filePath;
				this.fileHandler.ChangeFile(filePath);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008D60 File Offset: 0x00006F60
		private string GetFilePath()
		{
			string text = null;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					text = this.ReadKeyFromConfig(this.configSource, this.key);
					break;
				}
				catch (ConfigurationErrorsException ex)
				{
					InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Configsource: {0}, had ConfigurationErrorsException, will retry in 500ms. Exception: {1}", new object[]
					{
						this.configSource,
						ex
					});
					Thread.Sleep(500);
				}
			}
			if (text == null)
			{
				text = ConfigFiles.GetConfigFilePath(this.defaultFileName);
				InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "Using default file path: {0}", new object[]
				{
					text
				});
			}
			else
			{
				InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "Appconfig redirection, using file: {0}", new object[]
				{
					text
				});
			}
			return text;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00008E2C File Offset: 0x0000702C
		private string ReadKeyFromConfig(string configSource, string fileKey)
		{
			if (string.IsNullOrEmpty(configSource))
			{
				ConfigurationManager.RefreshSection("appSettings");
				return ConfigurationManager.AppSettings[fileKey];
			}
			Configuration configuration;
			try
			{
				configuration = this.LoadWebConfiguration(configSource);
			}
			catch (Exception)
			{
				return null;
			}
			AppDomain.CurrentDomain.SetupInformation.ConfigurationFile = configuration.FilePath;
			KeyValueConfigurationElement keyValueConfigurationElement = configuration.AppSettings.Settings[fileKey];
			if (keyValueConfigurationElement != null)
			{
				return keyValueConfigurationElement.Value;
			}
			return null;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008EAC File Offset: 0x000070AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Configuration LoadWebConfiguration(string configSource)
		{
			if (string.IsNullOrEmpty(this.siteName))
			{
				return WebConfigurationManager.OpenWebConfiguration(configSource);
			}
			return WebConfigurationManager.OpenWebConfiguration(configSource, this.siteName);
		}

		// Token: 0x0400025A RID: 602
		private const string AppSettingsSection = "appSettings";

		// Token: 0x0400025B RID: 603
		private string key;

		// Token: 0x0400025C RID: 604
		private string defaultFileName;

		// Token: 0x0400025D RID: 605
		private string configFilePath;

		// Token: 0x0400025E RID: 606
		private FileHandler fileHandler;

		// Token: 0x0400025F RID: 607
		private string configSource;

		// Token: 0x04000260 RID: 608
		private string siteName;
	}
}
