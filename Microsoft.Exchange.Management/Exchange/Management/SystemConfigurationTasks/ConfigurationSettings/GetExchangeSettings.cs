using System;
using System.Collections.Generic;
using System.Configuration;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200093D RID: 2365
	[Cmdlet("Get", "ExchangeSettings", DefaultParameterSetName = "Identity")]
	public sealed class GetExchangeSettings : GetSystemConfigurationObjectTask<ExchangeSettingsIdParameter, ExchangeSettings>
	{
		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x0015B829 File Offset: 0x00159A29
		// (set) Token: 0x06005426 RID: 21542 RVA: 0x0015B84F File Offset: 0x00159A4F
		[Parameter(Mandatory = false)]
		public SwitchParameter Diagnostic
		{
			get
			{
				return (SwitchParameter)(base.Fields["Diagnostic"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Diagnostic"] = value;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x0015B867 File Offset: 0x00159A67
		// (set) Token: 0x06005428 RID: 21544 RVA: 0x0015B87E File Offset: 0x00159A7E
		[Parameter(Mandatory = false)]
		public string DiagnosticArgument
		{
			get
			{
				return (string)base.Fields["DiagnosticArgument"];
			}
			set
			{
				base.Fields["DiagnosticArgument"] = value;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x0015B891 File Offset: 0x00159A91
		// (set) Token: 0x0600542A RID: 21546 RVA: 0x0015B8A8 File Offset: 0x00159AA8
		[Parameter(Mandatory = false)]
		public string ConfigName
		{
			get
			{
				return (string)base.Fields["ConfigName"];
			}
			set
			{
				base.Fields["ConfigName"] = value;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x0015B8BB File Offset: 0x00159ABB
		// (set) Token: 0x0600542C RID: 21548 RVA: 0x0015B8D2 File Offset: 0x00159AD2
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x0015B8E5 File Offset: 0x00159AE5
		// (set) Token: 0x0600542E RID: 21550 RVA: 0x0015B8FC File Offset: 0x00159AFC
		[Parameter(Mandatory = false)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x0015B90F File Offset: 0x00159B0F
		// (set) Token: 0x06005430 RID: 21552 RVA: 0x0015B926 File Offset: 0x00159B26
		[Parameter(Mandatory = false)]
		public string Process
		{
			get
			{
				return (string)base.Fields["Process"];
			}
			set
			{
				base.Fields["Process"] = value;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x0015B939 File Offset: 0x00159B39
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x0015B95E File Offset: 0x00159B5E
		[Parameter(Mandatory = false)]
		public Guid User
		{
			get
			{
				return (Guid)(base.Fields["User"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["User"] = value;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x0015B976 File Offset: 0x00159B76
		// (set) Token: 0x06005434 RID: 21556 RVA: 0x0015B98D File Offset: 0x00159B8D
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x0015B9A0 File Offset: 0x00159BA0
		// (set) Token: 0x06005436 RID: 21558 RVA: 0x0015B9B7 File Offset: 0x00159BB7
		[Parameter(Mandatory = false)]
		public string GenericScopeName
		{
			get
			{
				return (string)base.Fields["GenericName"];
			}
			set
			{
				base.Fields["GenericName"] = value;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x0015B9CA File Offset: 0x00159BCA
		// (set) Token: 0x06005438 RID: 21560 RVA: 0x0015B9E1 File Offset: 0x00159BE1
		[Parameter(Mandatory = false)]
		public string GenericScopeValue
		{
			get
			{
				return (string)base.Fields["GenericValue"];
			}
			set
			{
				base.Fields["GenericValue"] = value;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x0015B9F4 File Offset: 0x00159BF4
		// (set) Token: 0x0600543A RID: 21562 RVA: 0x0015BA0B File Offset: 0x00159C0B
		[Parameter(Mandatory = false)]
		public string[] GenericScopes
		{
			get
			{
				return (string[])base.Fields["GenericScopes"];
			}
			set
			{
				base.Fields["GenericScopes"] = value;
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x0015BA1E File Offset: 0x00159C1E
		// (set) Token: 0x0600543C RID: 21564 RVA: 0x0015BA44 File Offset: 0x00159C44
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x0015BA5C File Offset: 0x00159C5C
		protected override ObjectId RootId
		{
			get
			{
				return ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetDescendantId(InternalExchangeSettings.ContainerRelativePath);
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0015BA6D File Offset: 0x00159C6D
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is ConfigurationSettingsException;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0015BA84 File Offset: 0x00159C84
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ExchangeSettings exchangeSettings = (ExchangeSettings)dataObject;
			ConfigDiagnosticArgument configDiagnosticArgument = new ConfigDiagnosticArgument(this.DiagnosticArgument);
			if (configDiagnosticArgument.HasArgument("force"))
			{
				this.Force = true;
			}
			if (string.IsNullOrEmpty(this.ConfigName) && configDiagnosticArgument.HasArgument("configname"))
			{
				this.ConfigName = configDiagnosticArgument.GetArgument<string>("configname");
			}
			if (exchangeSettings != null)
			{
				ConfigSchemaBase schema = null;
				if (SetExchangeSettings.IsSchemaRegistered(exchangeSettings.Identity.ToString()))
				{
					schema = SetExchangeSettings.GetRegisteredSchema(exchangeSettings.Identity.ToString(), this.Force, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				if (exchangeSettings.Groups != null)
				{
					foreach (SettingsGroup settingsGroup in exchangeSettings.Groups)
					{
						try
						{
							settingsGroup.Validate(schema, null);
						}
						catch (ConfigurationSettingsException ex)
						{
							base.WriteWarning(ex.Message);
						}
					}
				}
				this.ProcessDiagnostic(exchangeSettings, configDiagnosticArgument, schema);
			}
			base.WriteResult(exchangeSettings);
			TaskLogger.LogExit();
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0015BDF8 File Offset: 0x00159FF8
		private void ProcessDiagnostic(ExchangeSettings settings, ConfigDiagnosticArgument argument, IConfigSchema schema)
		{
			XElement xelement = new XElement("config");
			SettingsContextBase context = new DiagnosticSettingsContext(schema, argument);
			Server server = null;
			if (this.Server != null)
			{
				server = (Server)base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			}
			if (server != null || !string.IsNullOrEmpty(this.Process))
			{
				context = new ServerSettingsContext(server, this.Process, context);
			}
			if (this.Database != null)
			{
				Database database = (Database)base.GetDataObject<Database>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())));
				context = new DatabaseSettingsContext(database.Guid, context);
			}
			if (this.Organization != null)
			{
				ExchangeConfigurationUnit org = (ExchangeConfigurationUnit)base.GetDataObject<ExchangeConfigurationUnit>(this.Organization, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				context = new OrganizationSettingsContext(org, context);
			}
			if (this.User != Guid.Empty)
			{
				context = new MailboxSettingsContext(this.User, context);
			}
			if (this.GenericScopeName != null)
			{
				if (schema != null)
				{
					schema.ParseAndValidateScopeValue(this.GenericScopeName, this.GenericScopeValue);
				}
				context = new GenericSettingsContext(this.GenericScopeName, this.GenericScopeValue, context);
			}
			if (this.GenericScopes != null)
			{
				foreach (string text in this.GenericScopes)
				{
					string text2 = null;
					string text3 = null;
					if (text != null)
					{
						int num = (text != null) ? text.IndexOf('=') : -1;
						if (num > 0)
						{
							text2 = text.Substring(0, num);
							text3 = text.Substring(num + 1);
						}
					}
					if (string.IsNullOrWhiteSpace(text2))
					{
						base.WriteError(new ExchangeSettingsBadFormatOfConfigPairException(text), ExchangeErrorCategory.Client, this.GenericScopes);
					}
					if (schema != null)
					{
						schema.ParseAndValidateScopeValue(text2, text3);
					}
					context = new GenericSettingsContext(text2, text3, context);
				}
			}
			if (this.Diagnostic)
			{
				xelement.Add(argument.RunDiagnosticOperation(() => context.GetDiagnosticInfo(this.DiagnosticArgument)));
				if (schema != null)
				{
					xelement.Add(argument.RunDiagnosticOperation(() => schema.GetDiagnosticInfo(this.DiagnosticArgument)));
				}
				xelement.Add(argument.RunDiagnosticOperation(delegate
				{
					XElement xelement2 = new XElement("scopes");
					SettingsScopeFilterSchema schemaInstance = SettingsScopeFilterSchema.GetSchemaInstance(schema);
					foreach (PropertyDefinition propertyDefinition in schemaInstance.AllProperties)
					{
						xelement2.Add(new XElement(propertyDefinition.Name, new XAttribute("type", propertyDefinition.Type)));
					}
					return xelement2;
				}));
			}
			if (!string.IsNullOrEmpty(this.ConfigName))
			{
				string serializedValue = null;
				ConfigurationProperty pdef = null;
				xelement.Add(argument.RunDiagnosticOperation(delegate
				{
					using (context.Activate())
					{
						if (!settings.TryGetConfig(schema, SettingsContextBase.EffectiveContext, this.ConfigName, out serializedValue) && schema != null && schema.TryGetConfigurationProperty(this.ConfigName, out pdef))
						{
							object defaultConfigValue = schema.GetDefaultConfigValue(pdef);
							serializedValue = ((defaultConfigValue != null) ? defaultConfigValue.ToString() : null);
						}
					}
					return new XElement("EffectiveValue", new object[]
					{
						new XAttribute("name", this.ConfigName ?? "null"),
						new XAttribute("value", serializedValue ?? "null")
					});
				}));
				settings.EffectiveSetting = new KeyValuePair<string, object>(this.ConfigName, serializedValue);
				if (serializedValue != null && schema != null)
				{
					settings.EffectiveSetting = new KeyValuePair<string, object>(this.ConfigName, schema.ParseAndValidateConfigValue(this.ConfigName, serializedValue, null));
				}
				if (this.Diagnostic)
				{
					xelement.Add(argument.RunDiagnosticOperation(() => settings.GetDiagnosticInfo(this.DiagnosticArgument)));
				}
			}
			if (this.Diagnostic)
			{
				settings.DiagnosticInfo = xelement.ToString();
			}
		}
	}
}
