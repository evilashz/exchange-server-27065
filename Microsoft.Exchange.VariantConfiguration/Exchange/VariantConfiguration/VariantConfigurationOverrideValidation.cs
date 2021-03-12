using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.VariantConfiguration.Reflection;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000141 RID: 321
	public sealed class VariantConfigurationOverrideValidation
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002491C File Offset: 0x00022B1C
		public VariantConfigurationOverrideValidation(VariantConfigurationOverride o) : this(o, false)
		{
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00024926 File Offset: 0x00022B26
		public VariantConfigurationOverrideValidation(VariantConfigurationOverride o, bool criticalOnly)
		{
			this.Override = o;
			this.criticalOnly = criticalOnly;
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0002493C File Offset: 0x00022B3C
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00024944 File Offset: 0x00022B44
		public VariantConfigurationOverride Override { get; private set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00024950 File Offset: 0x00022B50
		private bool IncludeInternal
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).VariantConfig.InternalAccess.Enabled;
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002497B File Offset: 0x00022B7B
		public void Validate()
		{
			if (this.Override == null)
			{
				throw new NullOverrideException(null);
			}
			if (!this.criticalOnly)
			{
				if (this.Override.IsFlight)
				{
					this.ValidateFlightOverride();
				}
				else
				{
					this.ValidateSettingOverride();
				}
			}
			this.LoadWithOverride();
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000249BC File Offset: 0x00022BBC
		private void ValidateFlightOverride()
		{
			if (!this.IncludeInternal)
			{
				throw new FlightNameValidationException(this.Override, new string[0], null);
			}
			if (string.IsNullOrEmpty(this.Override.ComponentName))
			{
				throw new FlightNameValidationException(this.Override, VariantConfiguration.Flights.Flights.OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
			}
			if (!VariantConfiguration.Flights.Contains(this.Override.ComponentName))
			{
				throw new FlightNameValidationException(this.Override, VariantConfiguration.Flights.Flights.OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
			}
			this.ValidateParameters(VariantConfiguration.Flights[this.Override.ComponentName].Type);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00024AB4 File Offset: 0x00022CB4
		private void ValidateSettingOverride()
		{
			if (string.IsNullOrEmpty(this.Override.ComponentName))
			{
				throw new ComponentNameValidationException(this.Override, VariantConfiguration.Settings.GetComponents(this.IncludeInternal).OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
			}
			if (!VariantConfiguration.Settings.Contains(this.Override.ComponentName, this.IncludeInternal))
			{
				throw new ComponentNameValidationException(this.Override, VariantConfiguration.Settings.GetComponents(this.IncludeInternal).OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
			}
			VariantConfigurationComponent variantConfigurationComponent = VariantConfiguration.Settings[this.Override.ComponentName];
			if (!variantConfigurationComponent.Contains(this.Override.SectionName, this.IncludeInternal))
			{
				throw new SectionNameValidationException(this.Override, variantConfigurationComponent.GetSections(this.IncludeInternal).OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
			}
			this.ValidateParameters(variantConfigurationComponent[this.Override.SectionName].Type);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00024C24 File Offset: 0x00022E24
		private void ValidateParameters(Type type)
		{
			foreach (string line in this.Override.Parameters)
			{
				string text;
				KeyValuePair<string, string>[] array;
				string value;
				this.ParseParameterLine(line, out text, out array, out value);
				try
				{
					if (type.GetProperty(text) == null)
					{
						HashSet<string> hashSet = new HashSet<string>();
						foreach (Type type2 in type.GetInterfaces().Concat(new Type[]
						{
							type
						}))
						{
							PropertyInfo[] properties = type2.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
							foreach (string item in from name in Array.ConvertAll<PropertyInfo, string>(properties, (PropertyInfo prop) => prop.Name)
							where !name.Equals("Name")
							select name)
							{
								hashSet.Add(item);
							}
						}
						throw new ParameterNameValidationException(this.Override, text, hashSet.OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
					}
				}
				catch (AmbiguousMatchException)
				{
				}
				foreach (KeyValuePair<string, string> keyValuePair in array)
				{
					if (!VariantType.Variants.Contains(keyValuePair.Key, this.IncludeInternal))
					{
						throw new VariantNameValidationException(this.Override, keyValuePair.Key, VariantType.Variants.GetNames(this.IncludeInternal).OrderBy((string name) => name, StringComparer.InvariantCultureIgnoreCase), null);
					}
					VariantType variantByName = VariantType.Variants.GetVariantByName(keyValuePair.Key, this.IncludeInternal);
					if (variantByName.Type == typeof(bool))
					{
						if (!keyValuePair.Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase) && !keyValuePair.Value.Equals(bool.FalseString, StringComparison.InvariantCultureIgnoreCase))
						{
							throw new VariantValueValidationException(this.Override, variantByName, keyValuePair.Value, "true|false", null);
						}
					}
					else if (variantByName.Type == typeof(Version))
					{
						if (!VariantType.VersionRegex.IsMatch(keyValuePair.Value))
						{
							throw new VariantValueValidationException(this.Override, variantByName, keyValuePair.Value, "NN.NN.NNNN.NNN", null);
						}
					}
					else if (variantByName.Type == typeof(Guid) && !VariantType.GuidRegex.IsMatch(keyValuePair.Value))
					{
						throw new VariantValueValidationException(this.Override, variantByName, keyValuePair.Value, "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", null);
					}
				}
				if (type is IFlight && (string.Equals(text, "Ramp", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "Rotate", StringComparison.OrdinalIgnoreCase)) && !VariantConfigurationValidator.IsHashBucketValueParsable(value))
				{
					throw new SyntaxValidationException(this.Override, null);
				}
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00024FA8 File Offset: 0x000231A8
		private void ParseParameterLine(string line, out string parameterName, out KeyValuePair<string, string>[] variants, out string parameterValue)
		{
			Match match = VariantConfigurationOverrideValidation.ParameterRegex.Match(line);
			if (!match.Success)
			{
				throw new ParameterSyntaxValidationException(this.Override, line, null);
			}
			parameterName = match.Groups["PN"].Value;
			Group group = match.Groups["VN"];
			Group group2 = match.Groups["VV"];
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (group.Success && group2.Success)
			{
				for (int i = 0; i < group.Captures.Count; i++)
				{
					list.Add(new KeyValuePair<string, string>(group.Captures[i].Value, group2.Captures[i].Value));
				}
			}
			variants = list.ToArray();
			parameterValue = match.Groups["PV"].Value;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00025090 File Offset: 0x00023290
		private void LoadWithOverride()
		{
			try
			{
				VariantConfiguration.GetProviderForValidation(this.Override);
			}
			catch (Exception innerException)
			{
				throw new SyntaxValidationException(this.Override, innerException);
			}
		}

		// Token: 0x040004E7 RID: 1255
		private static readonly string VariantValueUnquoted = "(?<VV>[\\w\\.\\-\\+\\^]+)";

		// Token: 0x040004E8 RID: 1256
		private static readonly string VariantValueQuoted = "(?:\"(?<VV>(?:\"\"|[^\"])*)\")";

		// Token: 0x040004E9 RID: 1257
		private static readonly string ParameterName = "[\\w\\.\\-\\+\\\\\\/\\{\\}\\(\\)#\\*\\$\\[\\]:]+";

		// Token: 0x040004EA RID: 1258
		private static readonly Regex ParameterRegex = new Regex(string.Format("^\\s*(?<PN>{0})\\s*((&\\s*(?<VN>[\\w\\.\\-\\+\\^]+)\\s*:\\s*(?:{1}|{2})\\s*)+)?=\\s*(?<PV>.*?)\\s*$", VariantConfigurationOverrideValidation.ParameterName, VariantConfigurationOverrideValidation.VariantValueUnquoted, VariantConfigurationOverrideValidation.VariantValueQuoted), RegexOptions.Compiled);

		// Token: 0x040004EB RID: 1259
		private readonly bool criticalOnly;
	}
}
