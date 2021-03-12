using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000682 RID: 1666
	internal abstract class SettingsScopeFilterSchema : SimpleProviderObjectSchema
	{
		// Token: 0x06004D99 RID: 19865 RVA: 0x0011E444 File Offset: 0x0011C644
		public static SettingsScopeFilterSchema GetSchemaInstance(IConfigSchema schema)
		{
			if (schema == null)
			{
				return ObjectSchema.GetInstance<SettingsScopeFilterSchema.UntypedSettingsScopeFilterSchema>();
			}
			Type schemaType = typeof(SettingsScopeFilterSchema.TypedSettingsScopeFilterSchema<>).MakeGenericType(new Type[]
			{
				schema.GetType()
			});
			SettingsScopeFilterSchema settingsScopeFilterSchema = (SettingsScopeFilterSchema)ObjectSchema.GetInstance(schemaType);
			settingsScopeFilterSchema.InitializeScopeProperties(schema);
			return settingsScopeFilterSchema;
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x0011E48F File Offset: 0x0011C68F
		public virtual PropertyDefinition LookupSchemaProperty(string propName)
		{
			return base[propName];
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0011E498 File Offset: 0x0011C698
		protected virtual void InitializeScopeProperties(IConfigSchema schema)
		{
		}

		// Token: 0x040034BF RID: 13503
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition ServerGuid = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<Guid?>("ServerGuid", (ISettingsContext c) => c.ServerGuid);

		// Token: 0x040034C0 RID: 13504
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition ServerName = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<string>("ServerName", (ISettingsContext c) => c.ServerName);

		// Token: 0x040034C1 RID: 13505
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition ServerVersion = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<Version>("ServerVersion", delegate(ISettingsContext c)
		{
			if (!(c.ServerVersion != null))
			{
				return null;
			}
			return c.ServerVersion;
		});

		// Token: 0x040034C2 RID: 13506
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition ServerRole = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<string>("ServerRole", (ISettingsContext c) => c.ServerRole);

		// Token: 0x040034C3 RID: 13507
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition ProcessName = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<string>("ProcessName", (ISettingsContext c) => c.ProcessName);

		// Token: 0x040034C4 RID: 13508
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition DagOrServerGuid = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<Guid?>("DagOrServerGuid", (ISettingsContext c) => c.DagOrServerGuid);

		// Token: 0x040034C5 RID: 13509
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition DatabaseGuid = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<Guid?>("DatabaseGuid", (ISettingsContext c) => c.DatabaseGuid);

		// Token: 0x040034C6 RID: 13510
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition DatabaseName = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<string>("DatabaseName", (ISettingsContext c) => c.DatabaseName);

		// Token: 0x040034C7 RID: 13511
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition DatabaseVersion = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<Version>("DatabaseVersion", (ISettingsContext c) => c.DatabaseVersion);

		// Token: 0x040034C8 RID: 13512
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition OrganizationName = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<string>("OrganizationName", (ISettingsContext c) => c.OrganizationName);

		// Token: 0x040034C9 RID: 13513
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition OrganizationVersion = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<ExchangeObjectVersion>("OrganizationVersion", (ISettingsContext c) => c.OrganizationVersion);

		// Token: 0x040034CA RID: 13514
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition MailboxGuid = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<Guid?>("MailboxGuid", (ISettingsContext c) => c.MailboxGuid);

		// Token: 0x040034CB RID: 13515
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition Hour = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<int>("Hour", (ISettingsContext c) => DateTime.UtcNow.Hour);

		// Token: 0x040034CC RID: 13516
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition Minute = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<int>("Minute", (ISettingsContext c) => DateTime.UtcNow.Minute);

		// Token: 0x040034CD RID: 13517
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition DayOfWeek = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<DayOfWeek>("DayOfWeek", (ISettingsContext c) => DateTime.UtcNow.DayOfWeek);

		// Token: 0x040034CE RID: 13518
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition Day = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<int>("Day", (ISettingsContext c) => DateTime.UtcNow.Day);

		// Token: 0x040034CF RID: 13519
		public static readonly SettingsScopeFilterSchema.ScopeFilterPropertyDefinition Month = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<int>("Month", (ISettingsContext c) => DateTime.UtcNow.Month);

		// Token: 0x02000683 RID: 1667
		private class TypedSettingsScopeFilterSchema<TConfig> : SettingsScopeFilterSchema where TConfig : IConfigSchema
		{
			// Token: 0x06004DAF RID: 19887 RVA: 0x0011E904 File Offset: 0x0011CB04
			protected override void InitializeScopeProperties(IConfigSchema schema)
			{
				if (this.scopePropertiesInitialized)
				{
					return;
				}
				lock (this.locker)
				{
					if (!this.scopePropertiesInitialized)
					{
						if (schema.ScopeSchema != null)
						{
							List<PropertyDefinition> list = new List<PropertyDefinition>(base.AllProperties);
							foreach (string name in schema.ScopeSchema.Settings)
							{
								ConfigurationProperty scopeProperty = schema.ScopeSchema.GetConfigurationProperty(name, null);
								list.Add(new SettingsScopeFilterSchema.ScopeFilterPropertyDefinition(scopeProperty.Name, scopeProperty.Type, scopeProperty.DefaultValue, delegate(ISettingsContext ctx)
								{
									object result;
									if (!ExchangeConfigurationSection.TryConvertFromInvariantString(scopeProperty, ctx.GetGenericProperty(scopeProperty.Name), out result))
									{
										result = null;
									}
									return result;
								}));
							}
							base.AllProperties = new ReadOnlyCollection<PropertyDefinition>(list);
							base.InitializePropertyCollections();
						}
						this.scopePropertiesInitialized = true;
					}
				}
			}

			// Token: 0x040034E1 RID: 13537
			private bool scopePropertiesInitialized;

			// Token: 0x040034E2 RID: 13538
			private object locker = new object();
		}

		// Token: 0x02000684 RID: 1668
		private class UntypedSettingsScopeFilterSchema : SettingsScopeFilterSchema
		{
			// Token: 0x06004DB1 RID: 19889 RVA: 0x0011EA44 File Offset: 0x0011CC44
			public override PropertyDefinition LookupSchemaProperty(string propName)
			{
				PropertyDefinition propertyDefinition = base.LookupSchemaProperty(propName);
				if (propertyDefinition == null && !this.namedPropInstances.TryGetValue(propName, out propertyDefinition))
				{
					lock (this.namedPropInstancesLocker)
					{
						if (!this.namedPropInstances.TryGetValue(propName, out propertyDefinition))
						{
							propertyDefinition = new SettingsScopeFilterSchema.TypedScopeFilterPropertyDefinition<string>(propName, (ISettingsContext ctx) => ctx.GetGenericProperty(propName));
							this.namedPropInstances[propName] = propertyDefinition;
						}
					}
				}
				return propertyDefinition;
			}

			// Token: 0x040034E3 RID: 13539
			private Dictionary<string, PropertyDefinition> namedPropInstances = new Dictionary<string, PropertyDefinition>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x040034E4 RID: 13540
			private object namedPropInstancesLocker = new object();
		}

		// Token: 0x02000685 RID: 1669
		public class ScopeFilterPropertyDefinition : SimpleProviderPropertyDefinition
		{
			// Token: 0x06004DB3 RID: 19891 RVA: 0x0011EB1F File Offset: 0x0011CD1F
			public ScopeFilterPropertyDefinition(string propertyName, Type propertyType, object defaultValue, Func<ISettingsContext, object> retrieveValueDelegate) : base(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, (defaultValue == null) ? PropertyDefinitionFlags.None : PropertyDefinitionFlags.PersistDefaultValue, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None)
			{
				this.retrieveValueDelegate = retrieveValueDelegate;
			}

			// Token: 0x06004DB4 RID: 19892 RVA: 0x0011EB49 File Offset: 0x0011CD49
			public object RetrieveValue(ISettingsContext ctx)
			{
				if (ctx == null)
				{
					return null;
				}
				return this.retrieveValueDelegate(ctx);
			}

			// Token: 0x040034E5 RID: 13541
			private Func<ISettingsContext, object> retrieveValueDelegate;
		}

		// Token: 0x02000686 RID: 1670
		private class TypedScopeFilterPropertyDefinition<T> : SettingsScopeFilterSchema.ScopeFilterPropertyDefinition
		{
			// Token: 0x06004DB5 RID: 19893 RVA: 0x0011EB5C File Offset: 0x0011CD5C
			public TypedScopeFilterPropertyDefinition(string propertyName, Func<ISettingsContext, object> retrieveValueDelegate) : base(propertyName, typeof(T), default(T), retrieveValueDelegate)
			{
			}
		}
	}
}
