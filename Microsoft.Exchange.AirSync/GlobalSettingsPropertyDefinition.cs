using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C2 RID: 194
	internal class GlobalSettingsPropertyDefinition : PropertyDefinition
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x0003E24E File Offset: 0x0003C44E
		public GlobalSettingsPropertyDefinition(string name, Type type, object defaultValue, PropertyDefinitionConstraint readConstraint, bool logMissingEntries, Func<GlobalSettingsPropertyDefinition, object> getter) : base(name, type)
		{
			this.LogMissingEntries = logMissingEntries;
			this.Getter = (getter ?? new Func<GlobalSettingsPropertyDefinition, object>(this.DefaultGetter));
			this.DefaultValue = defaultValue;
			this.ReadConstraint = readConstraint;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0003E287 File Offset: 0x0003C487
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0003E28F File Offset: 0x0003C48F
		public object DefaultValue { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0003E298 File Offset: 0x0003C498
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0003E2A0 File Offset: 0x0003C4A0
		public PropertyDefinitionConstraint ReadConstraint { get; private set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0003E2A9 File Offset: 0x0003C4A9
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0003E2B1 File Offset: 0x0003C4B1
		public bool LogMissingEntries { get; private set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0003E2BA File Offset: 0x0003C4BA
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0003E2C2 File Offset: 0x0003C4C2
		internal Func<GlobalSettingsPropertyDefinition, object> Getter { get; private set; }

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003E2CC File Offset: 0x0003C4CC
		internal object DefaultGetter(GlobalSettingsPropertyDefinition propDef)
		{
			string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
			if (appSetting == null)
			{
				if (propDef.LogMissingEntries)
				{
					AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueHasBeenDefaulted, new string[]
					{
						propDef.Name,
						propDef.DefaultValue.ToString()
					});
				}
				return propDef.DefaultValue;
			}
			return GlobalSettingsPropertyDefinition.ConvertValueFromString(propDef, appSetting);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0003E322 File Offset: 0x0003C522
		public static object ConvertValueFromString(GlobalSettingsPropertyDefinition propDef, string valueToConvert)
		{
			return GlobalSettingsPropertyDefinition.ConvertValueFromString(valueToConvert, propDef.Type, propDef.Name, propDef.DefaultValue);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003E33C File Offset: 0x0003C53C
		public static object ConvertValueFromString(string valueToConvert, Type destinationType, string propName, object defaultValue)
		{
			if (string.IsNullOrEmpty(valueToConvert))
			{
				return defaultValue;
			}
			bool flag;
			if (destinationType == typeof(bool) && bool.TryParse(valueToConvert, out flag))
			{
				return flag;
			}
			object result;
			if (destinationType.IsEnum && EnumValidator.TryParse(destinationType, valueToConvert, EnumParseOptions.Default, out result))
			{
				return result;
			}
			if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				bool flag2 = valueToConvert == null || "null".Equals(valueToConvert, StringComparison.OrdinalIgnoreCase) || "$null".Equals(valueToConvert, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					return null;
				}
			}
			object result2;
			try
			{
				result2 = LanguagePrimitives.ConvertTo(valueToConvert, destinationType);
			}
			catch (PSInvalidCastException)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueNotParsable, new string[]
				{
					propName,
					destinationType.Name,
					valueToConvert,
					defaultValue.ToString()
				});
				result2 = defaultValue;
			}
			return result2;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003E428 File Offset: 0x0003C628
		public PropertyConstraintViolationError Validate(object value)
		{
			if (this.ReadConstraint == null)
			{
				return null;
			}
			return this.ReadConstraint.Validate(value, this, null);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003E442 File Offset: 0x0003C642
		public override int GetHashCode()
		{
			return base.Name.GetHashCode() ^ base.Type.GetHashCode();
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003E45C File Offset: 0x0003C65C
		public override bool Equals(object obj)
		{
			GlobalSettingsPropertyDefinition globalSettingsPropertyDefinition = obj as GlobalSettingsPropertyDefinition;
			return globalSettingsPropertyDefinition != null && globalSettingsPropertyDefinition.Name == base.Name && globalSettingsPropertyDefinition.Type == base.Type;
		}
	}
}
