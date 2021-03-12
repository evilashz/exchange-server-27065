using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002F0 RID: 752
	[XmlInclude(typeof(FolderChangeDescriptionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(AppendToFolderFieldType))]
	[XmlInclude(typeof(DeleteFolderFieldType))]
	[XmlInclude(typeof(SetFolderFieldType))]
	[XmlInclude(typeof(ItemChangeDescriptionType))]
	[XmlInclude(typeof(AppendToItemFieldType))]
	[XmlInclude(typeof(DeleteItemFieldType))]
	[XmlInclude(typeof(SetItemFieldType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class ChangeDescriptionType
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x000284A3 File Offset: 0x000266A3
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x000284AB File Offset: 0x000266AB
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		public BasePathToElementType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04001117 RID: 4375
		private BasePathToElementType itemField;
	}
}
