using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000765 RID: 1893
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PathToExceptionFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExceptionPropertyUri : PropertyPath
	{
		// Token: 0x0600387B RID: 14459 RVA: 0x000C782A File Offset: 0x000C5A2A
		public ExceptionPropertyUri()
		{
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000C7832 File Offset: 0x000C5A32
		internal ExceptionPropertyUri(ExceptionPropertyUriEnum uri)
		{
			this.uri = uri;
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000C7841 File Offset: 0x000C5A41
		// (set) Token: 0x0600387E RID: 14462 RVA: 0x000C7849 File Offset: 0x000C5A49
		[IgnoreDataMember]
		[XmlAttribute(AttributeName = "FieldURI", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ExceptionPropertyUriEnum Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000C7852 File Offset: 0x000C5A52
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x000C785F File Offset: 0x000C5A5F
		[DataMember(Name = "FieldURI", IsRequired = true)]
		[XmlIgnore]
		public string UriString
		{
			get
			{
				return EnumUtilities.ToString<ExceptionPropertyUriEnum>(this.Uri);
			}
			set
			{
				this.Uri = EnumUtilities.Parse<ExceptionPropertyUriEnum>(value);
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000C786D File Offset: 0x000C5A6D
		public override string ToString()
		{
			return PropertyUriMapper.GetXmlEnumValue(this.uri);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000C787C File Offset: 0x000C5A7C
		public override bool Equals(object obj)
		{
			bool result = false;
			ExceptionPropertyUri exceptionPropertyUri = obj as ExceptionPropertyUri;
			if (exceptionPropertyUri != null)
			{
				result = (this.uri == exceptionPropertyUri.Uri);
			}
			return result;
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000C78A5 File Offset: 0x000C5AA5
		public override int GetHashCode()
		{
			return this.uri.GetHashCode();
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		internal override XmlElement ToXml(XmlElement parentElement)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "ExceptionFieldURI", "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateAttribute(xmlElement, "FieldURI", this.ToString());
			return xmlElement;
		}

		// Token: 0x04001F56 RID: 8022
		private ExceptionPropertyUriEnum uri;
	}
}
