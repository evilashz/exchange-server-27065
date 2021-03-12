using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001FB RID: 507
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class ConfigSchemaBase : ExchangeConfigurationSection, IConfigSchema, IDiagnosableObject
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x00035021 File Offset: 0x00033221
		public ConfigSchemaBase()
		{
			this.defaultValueOverrides = new ConcurrentDictionary<string, object>();
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600118D RID: 4493
		public abstract string Name { get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00035034 File Offset: 0x00033234
		public virtual string SectionName
		{
			get
			{
				return "appSettings";
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0003503B File Offset: 0x0003323B
		string IDiagnosableObject.HashableIdentity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00035043 File Offset: 0x00033243
		protected virtual ExchangeConfigurationSection ScopeSchema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00035048 File Offset: 0x00033248
		public static T GetDefaultConfig<T>(IConfigSchema schema, string settingName)
		{
			ConfigurationProperty configurationProperty = schema.GetConfigurationProperty(settingName, typeof(T));
			object defaultConfigValue = schema.GetDefaultConfigValue(configurationProperty);
			return ConfigSchemaBase.ConvertValue<T>(schema, settingName, defaultConfigValue);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0003511C File Offset: 0x0003331C
		public static T ConvertValue<T>(IConfigSchema schema, string settingName, object rawValue)
		{
			if (!(rawValue is T))
			{
				ConfigurationProperty property = schema.GetConfigurationProperty(settingName, typeof(T));
				ExchangeConfigurationSection.RunConfigOperation(delegate
				{
					TypeConverter converter = property.Converter;
					rawValue = converter.ConvertTo(rawValue, typeof(T));
				}, (Exception ex) => new ConfigurationSettingsPropertyBadTypeException(string.Format("{0}:{1}", settingName, (rawValue != null) ? rawValue.GetType().ToString() : "(null)"), typeof(T).ToString(), ex));
			}
			T result;
			try
			{
				result = (T)((object)rawValue);
			}
			catch (InvalidCastException innerException)
			{
				throw new ConfigurationSettingsPropertyBadTypeException(string.Format("{0}:{1}", settingName, (rawValue != null) ? rawValue.GetType().ToString() : "(null)"), typeof(T).ToString(), innerException);
			}
			return result;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00035200 File Offset: 0x00033400
		public void ValidateConfigValue(string settingName, object value)
		{
			ConfigurationProperty configurationProperty = base.GetConfigurationProperty(settingName, null);
			this.ValidateConfigValue(configurationProperty, value);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00035268 File Offset: 0x00033468
		public object ParseAndValidateConfigValue(string settingName, string serializedValue, Type settingType = null)
		{
			ConfigurationProperty property = base.GetConfigurationProperty(settingName, settingType);
			object convertedValue = null;
			ExchangeConfigurationSection.RunConfigOperation(delegate
			{
				TypeConverter converter = property.Converter;
				convertedValue = converter.ConvertFromInvariantString(serializedValue);
			}, (Exception ex) => new ConfigurationSettingsPropertyBadValueException(settingName, serializedValue, ex));
			this.ValidateConfigValue(property, convertedValue);
			return convertedValue;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000352D8 File Offset: 0x000334D8
		public void ValidateScopeName(string scopeName)
		{
			if (string.IsNullOrEmpty(scopeName))
			{
				throw new ArgumentNullException("scopeName");
			}
			ConfigurationProperty configurationProperty;
			if (this.ScopeSchema == null || !this.ScopeSchema.TryGetConfigurationProperty(scopeName, out configurationProperty))
			{
				List<string> list = new List<string>(this.ScopeSchema.Settings);
				list.Sort();
				throw new ConfigurationSettingsScopePropertyNotFoundException(scopeName, string.Join(", ", list));
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000353C0 File Offset: 0x000335C0
		public string ParseAndValidateScopeValue(string scopeName, object value)
		{
			this.ValidateScopeName(scopeName);
			ConfigurationProperty property = this.ScopeSchema.GetConfigurationProperty(scopeName, null);
			ExchangeConfigurationSection.RunConfigOperation(delegate
			{
				if (property.Type != value.GetType())
				{
					property.Converter.ConvertFrom(value);
				}
				property.Validator.Validate(value);
			}, (Exception ex) => new ConfigurationSettingsScopePropertyFailedValidationException(scopeName, (value != null) ? value.ToString() : null, ex));
			if (value == null)
			{
				return null;
			}
			if (value is string)
			{
				return (string)value;
			}
			string result;
			try
			{
				result = property.Converter.ConvertToInvariantString(value);
			}
			catch (NotSupportedException innerException)
			{
				throw new ConfigurationSettingsScopePropertyBadValueException(scopeName, (value != null) ? value.ToString() : null, innerException);
			}
			return result;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00035494 File Offset: 0x00033694
		public void SetDefaultConfigValue<T>(string settingName, T value)
		{
			this.SetDefaultConfigValue<T>(base.GetConfigurationProperty(settingName, typeof(T)), value);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000354AE File Offset: 0x000336AE
		public void SetDefaultConfigValue<T>(ConfigurationProperty property, T value)
		{
			this.defaultValueOverrides[property.Name] = value;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000354C8 File Offset: 0x000336C8
		public XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement(this.SectionName ?? "Application");
			foreach (KeyValuePair<string, ConfigurationProperty> keyValuePair in base.Definitions)
			{
				object defaultConfigValue = ((IConfigSchema)this).GetDefaultConfigValue(keyValuePair.Value);
				xelement.Add(new XElement(keyValuePair.Key, new object[]
				{
					new XAttribute("value", base[keyValuePair.Key] ?? string.Empty),
					new XAttribute("default", (defaultConfigValue != null) ? defaultConfigValue.ToString() : string.Empty),
					new XAttribute("type", keyValuePair.Value.Type.Name)
				}));
			}
			return xelement;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000355D4 File Offset: 0x000337D4
		object IConfigSchema.GetDefaultConfigValue(ConfigurationProperty property)
		{
			object result;
			if (this.defaultValueOverrides.TryGetValue(property.Name, out result))
			{
				return result;
			}
			return property.DefaultValue;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x000355FE File Offset: 0x000337FE
		ExchangeConfigurationSection IConfigSchema.ScopeSchema
		{
			get
			{
				return this.ScopeSchema;
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00035606 File Offset: 0x00033806
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return true;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00035668 File Offset: 0x00033868
		private void ValidateConfigValue(ConfigurationProperty property, object value)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			ExchangeConfigurationSection.RunConfigOperation(delegate
			{
				ConfigurationValidatorBase validator = property.Validator;
				if (validator != null)
				{
					validator.Validate(value);
				}
			}, (Exception ex) => new ConfigurationSettingsPropertyFailedValidationException(property.Name, (value != null) ? value.ToString() : null, ex));
		}

		// Token: 0x04000ABA RID: 2746
		private readonly IDictionary<string, object> defaultValueOverrides;
	}
}
