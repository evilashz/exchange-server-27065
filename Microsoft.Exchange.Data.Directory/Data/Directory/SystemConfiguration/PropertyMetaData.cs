using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000281 RID: 641
	[XmlType(TypeName = "PropertyMetaData")]
	[Serializable]
	public sealed class PropertyMetaData : XMLSerializableBase
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x0008933F File Offset: 0x0008753F
		public PropertyMetaData(string name, DateTime time)
		{
			this.AttributeName = name;
			this.LastWriteTime = time;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00089355 File Offset: 0x00087555
		public PropertyMetaData()
		{
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0008935D File Offset: 0x0008755D
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x00089365 File Offset: 0x00087565
		[XmlElement(ElementName = "AttributeName")]
		public string AttributeName { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0008936E File Offset: 0x0008756E
		// (set) Token: 0x06001E87 RID: 7815 RVA: 0x00089376 File Offset: 0x00087576
		[XmlElement(ElementName = "LastWriteTime")]
		public DateTime LastWriteTime { get; set; }
	}
}
