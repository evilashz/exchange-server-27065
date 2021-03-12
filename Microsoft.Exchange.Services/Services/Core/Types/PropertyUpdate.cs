using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B1 RID: 1713
	[XmlInclude(typeof(AppendItemPropertyUpdate))]
	[XmlType(TypeName = "ChangeDescriptionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(AppendFolderPropertyUpdate))]
	[KnownType(typeof(SetItemPropertyUpdate))]
	[XmlInclude(typeof(AppendFolderPropertyUpdate))]
	[XmlInclude(typeof(DeleteItemPropertyUpdate))]
	[KnownType(typeof(DeleteItemPropertyUpdate))]
	[XmlInclude(typeof(DeleteFolderPropertyUpdate))]
	[XmlInclude(typeof(SetItemPropertyUpdate))]
	[KnownType(typeof(AppendItemPropertyUpdate))]
	[XmlInclude(typeof(SetFolderPropertyUpdate))]
	[KnownType(typeof(DeleteFolderPropertyUpdate))]
	[KnownType(typeof(SetFolderPropertyUpdate))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public abstract class PropertyUpdate
	{
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x000BE2D6 File Offset: 0x000BC4D6
		// (set) Token: 0x060034D9 RID: 13529 RVA: 0x000BE2DE File Offset: 0x000BC4DE
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		[XmlElement("Path")]
		[DataMember(IsRequired = true, Name = "Path")]
		public PropertyPath PropertyPath { get; set; }
	}
}
