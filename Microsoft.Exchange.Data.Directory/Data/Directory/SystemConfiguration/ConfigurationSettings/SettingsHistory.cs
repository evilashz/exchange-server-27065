using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200067C RID: 1660
	[Serializable]
	public sealed class SettingsHistory : XMLSerializableBase
	{
		// Token: 0x06004D79 RID: 19833 RVA: 0x0011DE4E File Offset: 0x0011C04E
		public SettingsHistory()
		{
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0011DE56 File Offset: 0x0011C056
		public SettingsHistory(string name)
		{
			this.Name = name;
			this.Entries = new List<SettingsGroup>();
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06004D7B RID: 19835 RVA: 0x0011DE70 File Offset: 0x0011C070
		// (set) Token: 0x06004D7C RID: 19836 RVA: 0x0011DE78 File Offset: 0x0011C078
		[XmlAttribute(AttributeName = "Nm")]
		public string Name { get; set; }

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06004D7D RID: 19837 RVA: 0x0011DE81 File Offset: 0x0011C081
		// (set) Token: 0x06004D7E RID: 19838 RVA: 0x0011DE89 File Offset: 0x0011C089
		[XmlElement(ElementName = "StsG")]
		public List<SettingsGroup> Entries { get; set; }

		// Token: 0x06004D7F RID: 19839 RVA: 0x0011DE92 File Offset: 0x0011C092
		public void AddSettingsToHistory(SettingsGroup group)
		{
			this.Entries.Add(group);
			this.ResizeHistorySettings(10);
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0011DEA8 File Offset: 0x0011C0A8
		public void ResizeHistorySettings(int maxSize)
		{
			if (this.Entries.Count > maxSize)
			{
				this.Entries.RemoveRange(0, this.Entries.Count - maxSize);
			}
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x0011DED1 File Offset: 0x0011C0D1
		public override string ToString()
		{
			return DirectoryStrings.ConfigurationSettingsHistorySummary(this.Name, this.Entries.Count);
		}

		// Token: 0x040034B0 RID: 13488
		private const int MaximumEntryCount = 10;
	}
}
