using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200067E RID: 1662
	[Serializable]
	public sealed class SettingsHistoryGroup : XMLSerializableCompressed<XMLSerializableDictionary<string, SettingsHistory>>
	{
		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x0011E1C8 File Offset: 0x0011C3C8
		protected override PropertyDefinition XmlRawProperty
		{
			get
			{
				return InternalExchangeSettingsSchema.ConfigurationXMLRaw;
			}
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x0011E1CF File Offset: 0x0011C3CF
		public void ResizeHistorySettings(string name, int maxSize)
		{
			if (base.Value == null || !base.Value.ContainsKey(name))
			{
				return;
			}
			base.Value[name].ResizeHistorySettings(maxSize);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0011E1FC File Offset: 0x0011C3FC
		public void AddSettingsToHistory(SettingsGroup group)
		{
			if (base.Value == null)
			{
				base.Value = new XMLSerializableDictionary<string, SettingsHistory>();
			}
			if (!base.Value.ContainsKey(group.Name))
			{
				SettingsHistory value = new SettingsHistory(group.Name);
				base.Value[group.Name] = value;
			}
			base.Value[group.Name].AddSettingsToHistory(group);
		}
	}
}
