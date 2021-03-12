using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	public sealed class OrganizationIdXML : PropertyValueBaseXML
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000BC7E File Offset: 0x00009E7E
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0000BC86 File Offset: 0x00009E86
		[XmlElement(ElementName = "OrganizationalUnit")]
		public ADObjectIdXML OrganizationalUnit { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000BC8F File Offset: 0x00009E8F
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0000BC97 File Offset: 0x00009E97
		[XmlElement(ElementName = "ConfigurationUnit")]
		public ADObjectIdXML ConfigurationUnit { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		internal override object RawValue
		{
			get
			{
				return OrganizationIdXML.Deserialize(this);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		public override string ToString()
		{
			return string.Format("{0}", this.OrganizationalUnit);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000BCBC File Offset: 0x00009EBC
		internal static OrganizationIdXML Serialize(OrganizationId id)
		{
			if (id == null)
			{
				return null;
			}
			return new OrganizationIdXML
			{
				OrganizationalUnit = ADObjectIdXML.Serialize(id.OrganizationalUnit),
				ConfigurationUnit = ADObjectIdXML.Serialize(id.ConfigurationUnit)
			};
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000BCFD File Offset: 0x00009EFD
		internal static OrganizationId Deserialize(OrganizationIdXML value)
		{
			if (value == null)
			{
				return null;
			}
			return OrganizationIdXML.OrganizationIdGetter(ADObjectIdXML.Deserialize(value.OrganizationalUnit), ADObjectIdXML.Deserialize(value.ConfigurationUnit));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000BD1F File Offset: 0x00009F1F
		internal override bool TryGetValue(ProviderPropertyDefinition pdef, out object result)
		{
			result = OrganizationIdXML.Deserialize(this);
			return true;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000BD2A File Offset: 0x00009F2A
		internal override bool HasValue()
		{
			return this.OrganizationalUnit != null || this.ConfigurationUnit != null;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000BD42 File Offset: 0x00009F42
		private static OrganizationId OrganizationIdGetter(ADObjectId orgUnit, ADObjectId configUnit)
		{
			if (orgUnit == null || configUnit == null)
			{
				return OrganizationId.ForestWideOrgId;
			}
			return new OrganizationId(orgUnit, configUnit);
		}
	}
}
