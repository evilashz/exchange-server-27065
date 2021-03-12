using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002EF RID: 751
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ItemChangeType
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00028479 File Offset: 0x00026679
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x00028481 File Offset: 0x00026681
		[XmlElement("OccurrenceItemId", typeof(OccurrenceItemIdType))]
		[XmlElement("ItemId", typeof(ItemIdType))]
		[XmlElement("RecurringMasterItemId", typeof(RecurringMasterItemIdType))]
		public BaseItemIdType Item
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

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0002848A File Offset: 0x0002668A
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x00028492 File Offset: 0x00026692
		[XmlArrayItem("DeleteItemField", typeof(DeleteItemFieldType), IsNullable = false)]
		[XmlArrayItem("SetItemField", typeof(SetItemFieldType), IsNullable = false)]
		[XmlArrayItem("AppendToItemField", typeof(AppendToItemFieldType), IsNullable = false)]
		public ItemChangeDescriptionType[] Updates
		{
			get
			{
				return this.updatesField;
			}
			set
			{
				this.updatesField = value;
			}
		}

		// Token: 0x04001115 RID: 4373
		private BaseItemIdType itemField;

		// Token: 0x04001116 RID: 4374
		private ItemChangeDescriptionType[] updatesField;
	}
}
