using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200039A RID: 922
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetItemType : BaseRequestType
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0002A46E File Offset: 0x0002866E
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0002A476 File Offset: 0x00028676
		public ItemResponseShapeType ItemShape
		{
			get
			{
				return this.itemShapeField;
			}
			set
			{
				this.itemShapeField = value;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0002A47F File Offset: 0x0002867F
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x0002A487 File Offset: 0x00028687
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] ItemIds
		{
			get
			{
				return this.itemIdsField;
			}
			set
			{
				this.itemIdsField = value;
			}
		}

		// Token: 0x04001341 RID: 4929
		private ItemResponseShapeType itemShapeField;

		// Token: 0x04001342 RID: 4930
		private BaseItemIdType[] itemIdsField;
	}
}
