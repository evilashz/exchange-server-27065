using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F8 RID: 504
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigProviderBase : DisposeTrackableBase, IConfigProvider, IDisposable, IDiagnosable
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x000348DE File Offset: 0x00032ADE
		protected ConfigProviderBase(IConfigSchema schema)
		{
			this.schema = schema;
			this.configDrivers = new List<IConfigDriver>(5);
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x000348FC File Offset: 0x00032AFC
		protected static ConfigFlags OverrideFlags
		{
			get
			{
				int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Exchange_Test\\v15\\ConfigurationSettings", "ConfigFlags", 0);
				if (Enum.IsDefined(typeof(ConfigFlags), value))
				{
					return (ConfigFlags)value;
				}
				return ConfigFlags.None;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00034946 File Offset: 0x00032B46
		public DateTime LastUpdated
		{
			get
			{
				return this.configDrivers.Max((IConfigDriver d) => d.LastUpdated);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00034970 File Offset: 0x00032B70
		public bool IsInitialized
		{
			get
			{
				base.CheckDisposed();
				foreach (IConfigDriver configDriver in this.configDrivers)
				{
					if (!configDriver.IsInitialized)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000349D4 File Offset: 0x00032BD4
		protected IEnumerable<IConfigDriver> ConfigDrivers
		{
			get
			{
				base.CheckDisposed();
				return this.configDrivers;
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000349E4 File Offset: 0x00032BE4
		public void Initialize()
		{
			base.CheckDisposed();
			foreach (IConfigDriver configDriver in this.configDrivers)
			{
				if (!configDriver.IsInitialized)
				{
					configDriver.Initialize();
				}
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00034A44 File Offset: 0x00032C44
		public virtual T GetConfig<T>(string settingName)
		{
			return this.GetConfig<T>(null, settingName);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00034A4E File Offset: 0x00032C4E
		public T GetConfig<T>(ISettingsContext context, string settingName)
		{
			base.CheckDisposed();
			return this.GetConfigInternal<T>(context, settingName, new Func<IConfigSchema, string, T>(ConfigSchemaBase.GetDefaultConfig<T>));
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00034A7C File Offset: 0x00032C7C
		public T GetConfig<T>(ISettingsContext context, string settingName, T defaultValue)
		{
			return this.GetConfigInternal<T>(context, settingName, (IConfigSchema schema, string sName) => defaultValue);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00034AAC File Offset: 0x00032CAC
		public bool TryGetConfig<T>(ISettingsContext context, string settingName, out T settingValue)
		{
			base.CheckDisposed();
			object rawValue;
			if (this.TryGetBoxedSettingFromDrivers(context, settingName, typeof(T), out rawValue))
			{
				settingValue = ConfigSchemaBase.ConvertValue<T>(this.schema, settingName, rawValue);
				return true;
			}
			settingValue = default(T);
			return false;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00034AF2 File Offset: 0x00032CF2
		public virtual string GetDiagnosticComponentName()
		{
			return "config";
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00034AFC File Offset: 0x00032CFC
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			base.CheckDisposed();
			XElement xelement = new XElement(this.GetDiagnosticComponentName(), new XAttribute("name", this.schema.Name));
			xelement.Add(new XAttribute("LastUpdated", this.LastUpdated));
			ConfigDiagnosticArgument configDiagnosticArgument = new ConfigDiagnosticArgument(parameters.Argument);
			SettingsContextBase settingsContextBase = new DiagnosticSettingsContext(this.schema, configDiagnosticArgument);
			xelement.Add(settingsContextBase.GetDiagnosticInfo(parameters.Argument));
			if (configDiagnosticArgument.HasArgument("configname"))
			{
				string argument = configDiagnosticArgument.GetArgument<string>("configname");
				xelement.Add(new XElement("EffectiveSetting", new object[]
				{
					new XAttribute("name", argument),
					new XAttribute("value", this.GetConfig<string>(settingsContextBase, argument))
				}));
			}
			for (int i = 0; i < this.configDrivers.Count; i++)
			{
				IConfigDriver configDriver = this.configDrivers[i];
				xelement.Add(configDriver.GetDiagnosticInfo(parameters.Argument));
			}
			return xelement;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00034C2C File Offset: 0x00032E2C
		protected void AddConfigDriver(IConfigDriver configDriver)
		{
			base.CheckDisposed();
			this.configDrivers.Add(configDriver);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00034C40 File Offset: 0x00032E40
		protected void RemoveConfigDriver(IConfigDriver configDriver)
		{
			base.CheckDisposed();
			this.configDrivers.Remove(configDriver);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00034C55 File Offset: 0x00032E55
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ConfigProviderBase>(this);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00034C60 File Offset: 0x00032E60
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				foreach (IConfigDriver configDriver in this.configDrivers)
				{
					configDriver.Dispose();
				}
				this.configDrivers = null;
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00034CBC File Offset: 0x00032EBC
		private T GetConfigInternal<T>(ISettingsContext context, string settingName, Func<IConfigSchema, string, T> defaultValueGetter)
		{
			T result;
			if (this.TryGetConfig<T>(context, settingName, out result))
			{
				return result;
			}
			return defaultValueGetter(this.schema, settingName);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00034CE4 File Offset: 0x00032EE4
		private bool TryGetBoxedSettingFromDrivers(ISettingsContext context, string settingName, Type settingType, out object boxedValue)
		{
			this.Initialize();
			foreach (IConfigDriver configDriver in this.configDrivers)
			{
				if (configDriver.TryGetBoxedSetting(context, settingName, settingType, out boxedValue))
				{
					return true;
				}
			}
			boxedValue = null;
			return false;
		}

		// Token: 0x04000AB3 RID: 2739
		public const string DefaultDiagnosticComponentName = "config";

		// Token: 0x04000AB4 RID: 2740
		public const string RegKeyName = "SOFTWARE\\Microsoft\\Exchange_Test\\v15\\ConfigurationSettings";

		// Token: 0x04000AB5 RID: 2741
		public const string ConfigFlagsRegistryName = "ConfigFlags";

		// Token: 0x04000AB6 RID: 2742
		private List<IConfigDriver> configDrivers;

		// Token: 0x04000AB7 RID: 2743
		private IConfigSchema schema;
	}
}
