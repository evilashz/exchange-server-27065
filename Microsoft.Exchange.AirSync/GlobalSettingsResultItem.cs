using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000083 RID: 131
	public class GlobalSettingsResultItem
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x00026705 File Offset: 0x00024905
		public GlobalSettingsResultItem()
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0002670D File Offset: 0x0002490D
		public GlobalSettingsResultItem(string name, string type, string value, string defaultValue)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
			this.DefaultValue = defaultValue;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00026732 File Offset: 0x00024932
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0002673A File Offset: 0x0002493A
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00026743 File Offset: 0x00024943
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0002674B File Offset: 0x0002494B
		[XmlAttribute]
		public string Type { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00026754 File Offset: 0x00024954
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0002675C File Offset: 0x0002495C
		public string Value { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00026765 File Offset: 0x00024965
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0002676D File Offset: 0x0002496D
		public string DefaultValue { get; set; }
	}
}
