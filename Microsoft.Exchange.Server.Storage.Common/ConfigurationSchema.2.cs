using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200001E RID: 30
	public class ConfigurationSchema<TConfig> : ConfigurationSchema
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000756A File Offset: 0x0000576A
		public ConfigurationSchema(string name) : base(name)
		{
			this.defaultValueHook = Hookable<TConfig>.Create<ConfigurationSchema<TConfig>>(true, "defaultValue", this);
			this.valueHook = Hookable<TConfig>.Create<ConfigurationSchema<TConfig>>(true, "value", this);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007597 File Offset: 0x00005797
		public ConfigurationSchema(string name, TConfig defaultValue) : this(name, defaultValue, null, null)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000075A3 File Offset: 0x000057A3
		public ConfigurationSchema(string name, TConfig defaultValue, string registryKey, string registryValue) : this(name, defaultValue, null, registryKey, registryValue)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000075B1 File Offset: 0x000057B1
		public ConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse) : this(name, defaultValue, tryParse, null, null)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000075BE File Offset: 0x000057BE
		public ConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, string registryKey, string registryValue) : this(name, defaultValue, tryParse, null, registryKey, registryValue)
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000075CE File Offset: 0x000057CE
		public ConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess) : this(name, defaultValue, postProcess, null, null)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000075DB File Offset: 0x000057DB
		public ConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, string registryKey, string registryValue) : this(name, defaultValue, postProcess, null, registryKey, registryValue)
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000075EB File Offset: 0x000057EB
		public ConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator) : this(name, defaultValue, postProcess, validator, null, null)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000075FA File Offset: 0x000057FA
		public ConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator, string registryKey, string registryValue) : this(name, defaultValue, postProcess, validator, TypeDescriptor.GetConverter(typeof(TConfig)), registryKey, registryValue)
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000761A File Offset: 0x0000581A
		public ConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess) : this(name, defaultValue, tryParse, postProcess, null, null)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007629 File Offset: 0x00005829
		public ConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess, string registryKey, string registryValue) : this(name, defaultValue, tryParse, postProcess, null, registryKey, registryValue)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000763B File Offset: 0x0000583B
		public ConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator) : this(name, defaultValue, tryParse, postProcess, validator, null, null)
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000764C File Offset: 0x0000584C
		public ConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator, string registryKey, string registryValue) : this(name, defaultValue, postProcess, validator, new ConfigurationSchema<TConfig>.GenericConverter(tryParse), registryKey, registryValue)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007664 File Offset: 0x00005864
		private ConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator, TypeConverter typeConverter, string registryKey, string registryValue) : this(name)
		{
			this.defaultValue = defaultValue;
			this.postProcess = postProcess;
			this.typeConverter = typeConverter;
			this.validator = new ConfigurationSchema<TConfig>.GenericValidator(validator);
			this.registrySubkey = registryKey;
			this.registryValueName = registryValue;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000076A0 File Offset: 0x000058A0
		public TConfig DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000076A8 File Offset: 0x000058A8
		// (set) Token: 0x060002AD RID: 685 RVA: 0x000076B0 File Offset: 0x000058B0
		public TConfig Value
		{
			get
			{
				return this.value;
			}
			private set
			{
				this.value = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000076B9 File Offset: 0x000058B9
		public string RegistryValueName
		{
			get
			{
				return this.registryValueName;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000076C1 File Offset: 0x000058C1
		internal override ConfigurationProperty ConfigurationProperty
		{
			get
			{
				return new ConfigurationProperty(base.Name, typeof(TConfig), this.defaultValue, this.typeConverter, this.validator, ConfigurationPropertyOptions.None, string.Empty);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000076F8 File Offset: 0x000058F8
		public TConfig GetConfig(string settingName, TConfig defaultValue)
		{
			if (this.registrySubkey != null)
			{
				string localDatabaseRegistryKey = this.registrySubkey;
				if (localDatabaseRegistryKey == "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\{1}-{2}")
				{
					localDatabaseRegistryKey = ConfigurationSchema.LocalDatabaseRegistryKey;
				}
				if (RegistryReader.Instance.DoesValueExist(Registry.LocalMachine, localDatabaseRegistryKey, this.registryValueName))
				{
					TConfig tconfig = RegistryReader.Instance.GetValue<TConfig>(Registry.LocalMachine, localDatabaseRegistryKey, this.registryValueName, defaultValue);
					string text = tconfig.ToString();
					TConfig tconfig2 = (TConfig)((object)this.typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, text));
					if (this.validator != null)
					{
						this.validator.Validate(tconfig2);
					}
					return tconfig2;
				}
			}
			TConfig result;
			try
			{
				result = StoreConfigContext.Default.GetConfig<TConfig>(settingName, defaultValue);
			}
			catch (ConfigurationSettingsException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_InvalidAppConfig, new object[]
				{
					settingName,
					ex,
					defaultValue
				});
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000077F8 File Offset: 0x000059F8
		public override void Reload()
		{
			this.Value = this.GetConfig(base.Name, this.defaultValue);
			if (this.postProcess != null)
			{
				this.Value = this.postProcess(this.Value);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007834 File Offset: 0x00005A34
		internal IDisposable SetDefaultValueHook(TConfig value)
		{
			DisposeGuard disposeGuard = default(DisposeGuard);
			disposeGuard.Add<IDisposable>(this.defaultValueHook.SetTestHook(value));
			disposeGuard.Add<IDisposable>(this.SetValueHook(value));
			return disposeGuard;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007872 File Offset: 0x00005A72
		internal IDisposable SetValueHook(TConfig value)
		{
			return this.valueHook.SetTestHook(value);
		}

		// Token: 0x040003E0 RID: 992
		private readonly Hookable<TConfig> defaultValueHook;

		// Token: 0x040003E1 RID: 993
		private readonly Hookable<TConfig> valueHook;

		// Token: 0x040003E2 RID: 994
		private readonly TConfig defaultValue;

		// Token: 0x040003E3 RID: 995
		private readonly Func<TConfig, TConfig> postProcess;

		// Token: 0x040003E4 RID: 996
		private readonly TypeConverter typeConverter;

		// Token: 0x040003E5 RID: 997
		private readonly ConfigurationSchema<TConfig>.GenericValidator validator;

		// Token: 0x040003E6 RID: 998
		private readonly string registrySubkey;

		// Token: 0x040003E7 RID: 999
		private readonly string registryValueName;

		// Token: 0x040003E8 RID: 1000
		private TConfig value;

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x060002B5 RID: 693
		public delegate bool TryParse(string data, out TConfig value);

		// Token: 0x02000020 RID: 32
		private class GenericValidator : ConfigurationValidatorBase
		{
			// Token: 0x060002B8 RID: 696 RVA: 0x00007880 File Offset: 0x00005A80
			public GenericValidator(Func<TConfig, bool> validator)
			{
				this.validator = validator;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000788F File Offset: 0x00005A8F
			public override bool CanValidate(Type type)
			{
				return type == typeof(TConfig);
			}

			// Token: 0x060002BA RID: 698 RVA: 0x000078A1 File Offset: 0x00005AA1
			public override void Validate(object o)
			{
				if (this.validator != null && !this.validator((TConfig)((object)o)))
				{
					throw new ArgumentException(o.ToString());
				}
			}

			// Token: 0x040003E9 RID: 1001
			private readonly Func<TConfig, bool> validator;
		}

		// Token: 0x02000021 RID: 33
		private class GenericConverter : TypeConverter
		{
			// Token: 0x060002BB RID: 699 RVA: 0x000078CA File Offset: 0x00005ACA
			public GenericConverter(ConfigurationSchema<TConfig>.TryParse tryParse)
			{
				this.tryParse = tryParse;
			}

			// Token: 0x060002BC RID: 700 RVA: 0x000078D9 File Offset: 0x00005AD9
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string);
			}

			// Token: 0x060002BD RID: 701 RVA: 0x000078EC File Offset: 0x00005AEC
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				TConfig tconfig;
				if (!this.tryParse((string)value, out tconfig))
				{
					throw new NotSupportedException();
				}
				return tconfig;
			}

			// Token: 0x040003EA RID: 1002
			private readonly ConfigurationSchema<TConfig>.TryParse tryParse;
		}
	}
}
