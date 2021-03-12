using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000850 RID: 2128
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PathToUnindexedFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PropertyUri : PropertyPath
	{
		// Token: 0x06003D57 RID: 15703 RVA: 0x000D74C5 File Offset: 0x000D56C5
		public PropertyUri()
		{
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000D74CD File Offset: 0x000D56CD
		internal PropertyUri(PropertyUriEnum uri)
		{
			this.Uri = uri;
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x000D74DC File Offset: 0x000D56DC
		// (set) Token: 0x06003D5A RID: 15706 RVA: 0x000D74E4 File Offset: 0x000D56E4
		[XmlAttribute(AttributeName = "FieldURI", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[IgnoreDataMember]
		public PropertyUriEnum Uri { get; set; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x000D74ED File Offset: 0x000D56ED
		// (set) Token: 0x06003D5C RID: 15708 RVA: 0x000D74FA File Offset: 0x000D56FA
		[DataMember(Name = "FieldURI", IsRequired = true)]
		[XmlIgnore]
		public string UriString
		{
			get
			{
				return EnumUtilities.ToString<PropertyUriEnum>(this.Uri);
			}
			set
			{
				this.Uri = EnumUtilities.Parse<PropertyUriEnum>(value);
			}
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000D7508 File Offset: 0x000D5708
		public override string ToString()
		{
			return PropertyUriMapper.GetXmlEnumValue(this.Uri);
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000D7518 File Offset: 0x000D5718
		public override bool Equals(object obj)
		{
			bool result = false;
			PropertyUri propertyUri = obj as PropertyUri;
			if (propertyUri != null)
			{
				result = (this.Uri == propertyUri.Uri);
			}
			return result;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000D7541 File Offset: 0x000D5741
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000D7553 File Offset: 0x000D5753
		internal static bool IsPropertyUriXml(XmlElement propertyUriElement)
		{
			return propertyUriElement.LocalName == "FieldURI";
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000D7568 File Offset: 0x000D5768
		internal new static PropertyUri Parse(XmlElement propertyUriElement)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)propertyUriElement.Attributes.GetNamedItem("FieldURI");
			PropertyUriEnum uri;
			PropertyUriMapper.TryGetPropertyUriEnum(xmlAttribute.Value, out uri);
			return new PropertyUri(uri);
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000D75A0 File Offset: 0x000D57A0
		internal override XmlElement ToXml(XmlElement parentElement)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "FieldURI", "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateAttribute(xmlElement, "FieldURI", this.ToString());
			return xmlElement;
		}
	}
}
