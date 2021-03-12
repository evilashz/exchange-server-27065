using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200074B RID: 1867
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PathToIndexedFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DictionaryPropertyUri : DictionaryPropertyUriBase
	{
		// Token: 0x06003806 RID: 14342 RVA: 0x000C67CE File Offset: 0x000C49CE
		public DictionaryPropertyUri()
		{
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000C67E1 File Offset: 0x000C49E1
		internal DictionaryPropertyUri(DictionaryUriEnum fieldUri, string key) : base(fieldUri)
		{
			this.key = key;
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000C67FC File Offset: 0x000C49FC
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x000C6804 File Offset: 0x000C4A04
		[XmlAttribute(AttributeName = "FieldIndex", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "FieldIndex", IsRequired = true)]
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000C680D File Offset: 0x000C4A0D
		internal DictionaryPropertyUriBase GetDictionaryPropertyUriBase()
		{
			return new DictionaryPropertyUriBase(this.FieldUri);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000C681C File Offset: 0x000C4A1C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				PropertyUriMapper.GetXmlEnumValue(this.FieldUri),
				this.key
			});
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000C6858 File Offset: 0x000C4A58
		public override bool Equals(object obj)
		{
			bool result = false;
			DictionaryPropertyUri dictionaryPropertyUri = obj as DictionaryPropertyUri;
			if (dictionaryPropertyUri != null)
			{
				result = (this.FieldUri == dictionaryPropertyUri.FieldUri && this.key == dictionaryPropertyUri.key);
			}
			return result;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000C6895 File Offset: 0x000C4A95
		public override int GetHashCode()
		{
			return this.FieldUri.GetHashCode() + this.key.GetHashCode();
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000C68B3 File Offset: 0x000C4AB3
		internal static bool IsDictionaryPropertyUriXml(XmlElement element)
		{
			return element.LocalName == "IndexedFieldURI";
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000C68C8 File Offset: 0x000C4AC8
		internal new static DictionaryPropertyUri Parse(XmlElement dictionaryUriElement)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)dictionaryUriElement.Attributes.GetNamedItem("FieldURI");
			XmlAttribute xmlAttribute2 = (XmlAttribute)dictionaryUriElement.Attributes.GetNamedItem("FieldIndex");
			DictionaryUriEnum fieldUri;
			PropertyUriMapper.TryGetDictionaryUriEnum(xmlAttribute.Value, out fieldUri);
			return new DictionaryPropertyUri(fieldUri, xmlAttribute2.Value);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000C691C File Offset: 0x000C4B1C
		internal override XmlElement ToXml(XmlElement parentElement)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "IndexedFieldURI", "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateAttribute(xmlElement, "FieldURI", PropertyUriMapper.GetXmlEnumValue(base.Uri));
			ServiceXml.CreateAttribute(xmlElement, "FieldIndex", this.Key);
			return xmlElement;
		}

		// Token: 0x04001F17 RID: 7959
		private const string DictionaryPropertyUriFormat = "{0}:{1}";

		// Token: 0x04001F18 RID: 7960
		private string key = string.Empty;
	}
}
