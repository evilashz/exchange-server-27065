using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000201 RID: 513
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SyncFolderHierarchyChangesType
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00025C7F File Offset: 0x00023E7F
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00025C87 File Offset: 0x00023E87
		[XmlElement("Update", typeof(SyncFolderHierarchyCreateOrUpdateType))]
		[XmlElement("Delete", typeof(SyncFolderHierarchyDeleteType))]
		[XmlElement("Create", typeof(SyncFolderHierarchyCreateOrUpdateType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		public object[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00025C90 File Offset: 0x00023E90
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00025C98 File Offset: 0x00023E98
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType1[] ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x04000E1A RID: 3610
		private object[] itemsField;

		// Token: 0x04000E1B RID: 3611
		private ItemsChoiceType1[] itemsElementNameField;
	}
}
