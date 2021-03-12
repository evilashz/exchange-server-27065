using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F9 RID: 505
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeConfigurationSection : ConfigurationSection
	{
		// Token: 0x06001172 RID: 4466 RVA: 0x00034D50 File Offset: 0x00032F50
		public ExchangeConfigurationSection()
		{
			ConfigurationPropertyCollection properties = this.Properties;
			this.Definitions = new Dictionary<string, ConfigurationProperty>(properties.Count, StringComparer.InvariantCultureIgnoreCase);
			foreach (object obj in properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				this.Definitions[configurationProperty.Name] = configurationProperty;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00034DD4 File Offset: 0x00032FD4
		public IEnumerable<string> Settings
		{
			get
			{
				return this.Definitions.Keys;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00034DE1 File Offset: 0x00032FE1
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00034DE9 File Offset: 0x00032FE9
		private protected Dictionary<string, ConfigurationProperty> Definitions { protected get; private set; }

		// Token: 0x06001176 RID: 4470 RVA: 0x00034DF4 File Offset: 0x00032FF4
		public bool CheckSettingExists(string name)
		{
			ConfigurationProperty configurationProperty;
			return this.Definitions.TryGetValue(name, out configurationProperty);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00034E0F File Offset: 0x0003300F
		public bool TryGetConfigurationProperty(string name, out ConfigurationProperty property)
		{
			return this.Definitions.TryGetValue(name, out property);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00034E20 File Offset: 0x00033020
		public ConfigurationProperty GetConfigurationProperty(string name, Type type = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ConfigurationProperty configurationProperty;
			if (!this.TryGetConfigurationProperty(name, out configurationProperty))
			{
				List<string> list = new List<string>(this.Settings);
				list.Sort();
				throw new ConfigurationSettingsPropertyNotFoundException(name, string.Join(", ", list));
			}
			if (type != null && configurationProperty.Type != type && !configurationProperty.Converter.CanConvertTo(type))
			{
				throw new ConfigurationSettingsPropertyBadTypeException(name, type.ToString());
			}
			return configurationProperty;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00034EA2 File Offset: 0x000330A2
		public object GetPropertyValue(string propertyName)
		{
			return base[propertyName];
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00034EC4 File Offset: 0x000330C4
		public static void RunConfigOperation(Action configOperation, Func<Exception, ConfigurationSettingsException> errorHandler)
		{
			ExchangeConfigurationSection.InternalRunConfigOperation(configOperation, delegate(Exception ex)
			{
				throw errorHandler(ex);
			});
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00034F20 File Offset: 0x00033120
		public static bool TryConvertFromInvariantString(ConfigurationProperty configProperty, string toConvert, out object converted)
		{
			object convertedValue = null;
			ExchangeConfigurationSection.InternalRunConfigOperation(delegate
			{
				convertedValue = configProperty.Converter.ConvertFromInvariantString(toConvert);
			}, delegate(Exception ex)
			{
				convertedValue = null;
			});
			converted = convertedValue;
			return converted != null;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00034F78 File Offset: 0x00033178
		protected static void InternalRunConfigOperation(Action configOperation, Action<Exception> errorHandler)
		{
			try
			{
				configOperation();
			}
			catch (ArgumentException obj)
			{
				errorHandler(obj);
			}
			catch (FormatException obj2)
			{
				errorHandler(obj2);
			}
			catch (NotSupportedException obj3)
			{
				errorHandler(obj3);
			}
			catch (Exception ex)
			{
				if (ex.InnerException == null || !(ex.InnerException is FormatException))
				{
					throw;
				}
				errorHandler(ex);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00035004 File Offset: 0x00033204
		protected virtual T InternalGetConfig<T>([CallerMemberName] string key = null)
		{
			return (T)((object)base[key]);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00035012 File Offset: 0x00033212
		protected virtual void InternalSetConfig<T>(T value, [CallerMemberName] string key = null)
		{
			base[key] = value;
		}
	}
}
