using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200065F RID: 1631
	[Serializable]
	public class InternalExchangeSettings : ADConfigurationObject, IDiagnosableObject
	{
		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06004C51 RID: 19537 RVA: 0x0011A31B File Offset: 0x0011851B
		public virtual XMLSerializableDictionary<string, SettingsHistory> History
		{
			get
			{
				return this.Xml.History.Value;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06004C52 RID: 19538 RVA: 0x0011A32D File Offset: 0x0011852D
		// (set) Token: 0x06004C53 RID: 19539 RVA: 0x0011A33F File Offset: 0x0011853F
		public string XmlRaw
		{
			get
			{
				return (string)this[InternalExchangeSettingsSchema.ConfigurationXMLRaw];
			}
			set
			{
				this[InternalExchangeSettingsSchema.ConfigurationXMLRaw] = value;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06004C54 RID: 19540 RVA: 0x0011A34D File Offset: 0x0011854D
		// (set) Token: 0x06004C55 RID: 19541 RVA: 0x0011A35F File Offset: 0x0011855F
		internal SettingsXml Xml
		{
			get
			{
				return (SettingsXml)this[InternalExchangeSettingsSchema.ConfigurationXML];
			}
			set
			{
				this[InternalExchangeSettingsSchema.ConfigurationXML] = value;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06004C56 RID: 19542 RVA: 0x0011A36D File Offset: 0x0011856D
		internal SettingsGroupDictionary Settings
		{
			get
			{
				return this.Xml.Settings;
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x0011A37A File Offset: 0x0011857A
		internal override ADObjectSchema Schema
		{
			get
			{
				return InternalExchangeSettings.schema;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06004C58 RID: 19544 RVA: 0x0011A381 File Offset: 0x00118581
		internal override string MostDerivedObjectClass
		{
			get
			{
				return InternalExchangeSettings.mostDerivedClass;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06004C59 RID: 19545 RVA: 0x0011A388 File Offset: 0x00118588
		internal override ADObjectId ParentPath
		{
			get
			{
				return InternalExchangeSettings.ContainerRelativePath;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06004C5A RID: 19546 RVA: 0x0011A38F File Offset: 0x0011858F
		string IDiagnosableObject.HashableIdentity
		{
			get
			{
				return this.Identity.ToString();
			}
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0011A39C File Offset: 0x0011859C
		public void InitializeSettings()
		{
			this.Xml = SettingsXml.Create();
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x0011A3AC File Offset: 0x001185AC
		public bool IsPriorityInUse(int priority, string groupNameToExclude = null)
		{
			foreach (SettingsGroup settingsGroup in this.Xml.Settings.Values)
			{
				if ((groupNameToExclude == null || !(groupNameToExclude == settingsGroup.Name)) && priority == settingsGroup.Priority)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0011A420 File Offset: 0x00118620
		public SettingsGroup GetSettingsGroupForModification(string name)
		{
			if (!this.Settings.ContainsKey(name))
			{
				throw new ConfigurationSettingsGroupNotFoundException(name);
			}
			return this.Settings[name].Clone();
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0011A460 File Offset: 0x00118660
		public void AddSettingsGroup(SettingsGroup settingsGroup)
		{
			this.OperateOnSettings(delegate(SettingsXml xml)
			{
				xml.AddSettingsGroup(settingsGroup);
			});
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0011A4A4 File Offset: 0x001186A4
		public void UpdateSettingsGroup(SettingsGroup settingsGroup)
		{
			this.OperateOnSettings(delegate(SettingsXml xml)
			{
				xml.UpdateSettingsGroup(settingsGroup);
			});
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0011A4EC File Offset: 0x001186EC
		public void RemoveSettingsGroup(SettingsGroup settingsGroup, bool addHistory = true)
		{
			this.OperateOnSettings(delegate(SettingsXml xml)
			{
				xml.RemoveSettingsGroup(settingsGroup, addHistory);
			});
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0011A548 File Offset: 0x00118748
		public void ClearHistorySettings(string name)
		{
			if (this.History == null || !this.History.ContainsKey(name))
			{
				return;
			}
			this.OperateOnSettings(delegate(SettingsXml xml)
			{
				xml.History.Value[name].ResizeHistorySettings(0);
			});
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0011A590 File Offset: 0x00118790
		public XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement(base.Name);
			ConfigDiagnosticArgument configDiagnosticArgument = new ConfigDiagnosticArgument(argument);
			if (configDiagnosticArgument.HasArgument("configname"))
			{
				string argument2 = configDiagnosticArgument.GetArgument<string>("configname");
				XElement xelement2 = new XElement("setting", new XAttribute("name", argument2));
				foreach (SettingsGroup settingsGroup in this.Settings.Values)
				{
					string value;
					if (settingsGroup.TryGetValue(argument2, out value))
					{
						xelement2.Add(new XElement("group", new object[]
						{
							new XAttribute("name", settingsGroup.Name),
							new XAttribute("enabled", settingsGroup.Enabled),
							new XAttribute("priority", settingsGroup.Priority),
							new XAttribute("value", value)
						}));
					}
				}
				xelement.Add(xelement2);
			}
			else
			{
				foreach (SettingsGroup settingsGroup2 in this.Settings.Values)
				{
					xelement.Add(settingsGroup2.ToDiagnosticInfo(null));
				}
			}
			return xelement;
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0011A730 File Offset: 0x00118930
		internal bool TryGetConfig(IConfigSchema configSchema, ISettingsContext context, string settingName, out string settingValue)
		{
			settingValue = null;
			int num = -1;
			foreach (SettingsGroup settingsGroup in this.Settings.Values)
			{
				string text;
				if (settingsGroup.Priority > num && settingsGroup.TryGetValue(settingName, out text) && settingsGroup.Matches(configSchema, context))
				{
					settingValue = text;
					num = settingsGroup.Priority;
				}
			}
			return num != -1;
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x0011A7B4 File Offset: 0x001189B4
		private void OperateOnSettings(Action<SettingsXml> operation)
		{
			SettingsXml xml = this.Xml;
			operation(xml);
			this.Xml = xml;
		}

		// Token: 0x04003449 RID: 13385
		public const string SettingElement = "setting";

		// Token: 0x0400344A RID: 13386
		public const string GroupElement = "group";

		// Token: 0x0400344B RID: 13387
		public const string NameAttribute = "name";

		// Token: 0x0400344C RID: 13388
		public const string ValueAttribute = "value";

		// Token: 0x0400344D RID: 13389
		public const string EnableAttribute = "enabled";

		// Token: 0x0400344E RID: 13390
		public const string PriorityAttribute = "priority";

		// Token: 0x0400344F RID: 13391
		public static ADObjectId ContainerRelativePath = new ADObjectId("CN=Configuration Settings,CN=Global Settings");

		// Token: 0x04003450 RID: 13392
		private static InternalExchangeSettingsSchema schema = ObjectSchema.GetInstance<InternalExchangeSettingsSchema>();

		// Token: 0x04003451 RID: 13393
		private static string mostDerivedClass = "msExchConfigSettings";
	}
}
