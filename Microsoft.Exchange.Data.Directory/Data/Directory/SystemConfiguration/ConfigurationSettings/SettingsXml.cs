using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000689 RID: 1673
	[Serializable]
	public sealed class SettingsXml : XMLSerializableBase
	{
		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06004DDA RID: 19930 RVA: 0x0011F040 File Offset: 0x0011D240
		// (set) Token: 0x06004DDB RID: 19931 RVA: 0x0011F048 File Offset: 0x0011D248
		[XmlElement(ElementName = "Sts")]
		public SettingsGroupDictionary Settings { get; set; }

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06004DDC RID: 19932 RVA: 0x0011F051 File Offset: 0x0011D251
		// (set) Token: 0x06004DDD RID: 19933 RVA: 0x0011F059 File Offset: 0x0011D259
		[XmlElement(ElementName = "Hst")]
		public SettingsHistoryGroup History { get; set; }

		// Token: 0x06004DDE RID: 19934 RVA: 0x0011F064 File Offset: 0x0011D264
		public static SettingsXml Create()
		{
			return new SettingsXml
			{
				Settings = new SettingsGroupDictionary(),
				History = new SettingsHistoryGroup()
			};
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x0011F08E File Offset: 0x0011D28E
		public void AddSettingsGroup(SettingsGroup settingsGroup)
		{
			if (this.Settings.ContainsKey(settingsGroup.Name))
			{
				throw new ConfigurationSettingsGroupExistsException(settingsGroup.Name);
			}
			this.Settings[settingsGroup.Name] = settingsGroup;
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0011F0C4 File Offset: 0x0011D2C4
		public void UpdateSettingsGroup(SettingsGroup settingsGroup)
		{
			if (!this.Settings.ContainsKey(settingsGroup.Name))
			{
				throw new ConfigurationSettingsGroupNotFoundException(settingsGroup.Name);
			}
			this.AddSettingsToHistory(settingsGroup.Name);
			settingsGroup.LastModified = DateTime.UtcNow;
			this.Settings[settingsGroup.Name] = settingsGroup;
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0011F11C File Offset: 0x0011D31C
		public void RemoveSettingsGroup(SettingsGroup settingsGroup, bool addHistory = true)
		{
			if (!this.Settings.ContainsKey(settingsGroup.Name))
			{
				throw new ConfigurationSettingsGroupNotFoundException(settingsGroup.Name);
			}
			if (addHistory)
			{
				this.AddSettingsToHistory(settingsGroup.Name);
			}
			this.Settings.Remove(settingsGroup.Name);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0011F169 File Offset: 0x0011D369
		public void ResizeHistorySettings(string name, int maxSize)
		{
			if (!this.Settings.ContainsKey(name))
			{
				throw new ConfigurationSettingsGroupNotFoundException(name);
			}
			this.History.ResizeHistorySettings(name, maxSize);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0011F18D File Offset: 0x0011D38D
		private void AddSettingsToHistory(string name)
		{
			if (!this.Settings.ContainsKey(name))
			{
				throw new ConfigurationSettingsGroupNotFoundException(name);
			}
			this.History.AddSettingsToHistory(this.Settings[name]);
		}
	}
}
