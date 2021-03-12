using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200074A RID: 1866
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DictionaryPropertyUriBase : PropertyPath
	{
		// Token: 0x060037FC RID: 14332 RVA: 0x000C673F File Offset: 0x000C493F
		public DictionaryPropertyUriBase()
		{
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000C6747 File Offset: 0x000C4947
		internal DictionaryPropertyUriBase(DictionaryUriEnum fieldUri)
		{
			this.FieldUri = fieldUri;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000C6756 File Offset: 0x000C4956
		// (set) Token: 0x060037FF RID: 14335 RVA: 0x000C675E File Offset: 0x000C495E
		[IgnoreDataMember]
		[XmlAttribute(AttributeName = "FieldURI", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public DictionaryUriEnum Uri
		{
			get
			{
				return this.FieldUri;
			}
			set
			{
				this.FieldUri = value;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x000C6767 File Offset: 0x000C4967
		// (set) Token: 0x06003801 RID: 14337 RVA: 0x000C6774 File Offset: 0x000C4974
		[XmlIgnore]
		[DataMember(Name = "FieldURI", IsRequired = true)]
		public string UriString
		{
			get
			{
				return EnumUtilities.ToString<DictionaryUriEnum>(this.FieldUri);
			}
			set
			{
				this.FieldUri = EnumUtilities.Parse<DictionaryUriEnum>(value);
			}
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000C6782 File Offset: 0x000C4982
		public override string ToString()
		{
			return PropertyUriMapper.GetXmlEnumValue(this.FieldUri);
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000C6790 File Offset: 0x000C4990
		public override bool Equals(object obj)
		{
			bool result = false;
			DictionaryPropertyUriBase dictionaryPropertyUriBase = obj as DictionaryPropertyUriBase;
			if (dictionaryPropertyUriBase != null)
			{
				result = (this.FieldUri == dictionaryPropertyUriBase.FieldUri);
			}
			return result;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000C67B9 File Offset: 0x000C49B9
		public override int GetHashCode()
		{
			return this.FieldUri.GetHashCode();
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000C67CB File Offset: 0x000C49CB
		internal override XmlElement ToXml(XmlElement parentElement)
		{
			return null;
		}

		// Token: 0x04001F16 RID: 7958
		internal DictionaryUriEnum FieldUri;
	}
}
