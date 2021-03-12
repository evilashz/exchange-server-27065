using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public sealed class ADObjectIdXML : PropertyValueBaseXML
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0000BB71 File Offset: 0x00009D71
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0000BB79 File Offset: 0x00009D79
		[XmlElement(ElementName = "ObjectGuid")]
		public Guid ObjectGuid { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000BB82 File Offset: 0x00009D82
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000BB8A File Offset: 0x00009D8A
		[XmlElement(ElementName = "DistinguishedName")]
		public string DistinguishedName { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000BB93 File Offset: 0x00009D93
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0000BB9B File Offset: 0x00009D9B
		[XmlElement(ElementName = "PartitionGuid")]
		public Guid PartitionGuid { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0000BBAC File Offset: 0x00009DAC
		[XmlElement(ElementName = "PartitionFqdn")]
		public string PartitionFqdn { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000BBB5 File Offset: 0x00009DB5
		internal override object RawValue
		{
			get
			{
				return ADObjectIdXML.Deserialize(this);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000BBBD File Offset: 0x00009DBD
		public override string ToString()
		{
			return string.Format("{0} ({1},{2})", this.DistinguishedName, this.ObjectGuid, this.PartitionGuid);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		internal static ADObjectIdXML Serialize(ADObjectId id)
		{
			if (id == null)
			{
				return null;
			}
			return new ADObjectIdXML
			{
				ObjectGuid = id.ObjectGuid,
				PartitionGuid = id.PartitionGuid,
				DistinguishedName = id.DistinguishedName,
				PartitionFqdn = id.PartitionFQDN
			};
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000BC31 File Offset: 0x00009E31
		internal static ADObjectId Deserialize(ADObjectIdXML value)
		{
			if (value == null)
			{
				return null;
			}
			return new ADObjectId(value.DistinguishedName, value.PartitionFqdn, value.ObjectGuid);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000BC4F File Offset: 0x00009E4F
		internal override bool TryGetValue(ProviderPropertyDefinition pdef, out object result)
		{
			result = ADObjectIdXML.Deserialize(this);
			return true;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000BC5A File Offset: 0x00009E5A
		internal override bool HasValue()
		{
			return this.ObjectGuid != Guid.Empty || !string.IsNullOrEmpty(this.DistinguishedName);
		}
	}
}
