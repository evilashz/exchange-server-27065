using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000324 RID: 804
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetImItemsType : BaseRequestType
	{
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00028C62 File Offset: 0x00026E62
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x00028C6A File Offset: 0x00026E6A
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] ContactIds
		{
			get
			{
				return this.contactIdsField;
			}
			set
			{
				this.contactIdsField = value;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00028C73 File Offset: 0x00026E73
		// (set) Token: 0x06001A2A RID: 6698 RVA: 0x00028C7B File Offset: 0x00026E7B
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] GroupIds
		{
			get
			{
				return this.groupIdsField;
			}
			set
			{
				this.groupIdsField = value;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x00028C84 File Offset: 0x00026E84
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x00028C8C File Offset: 0x00026E8C
		[XmlArrayItem("ExtendedProperty", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public PathToExtendedFieldType[] ExtendedProperties
		{
			get
			{
				return this.extendedPropertiesField;
			}
			set
			{
				this.extendedPropertiesField = value;
			}
		}

		// Token: 0x04001191 RID: 4497
		private BaseItemIdType[] contactIdsField;

		// Token: 0x04001192 RID: 4498
		private BaseItemIdType[] groupIdsField;

		// Token: 0x04001193 RID: 4499
		private PathToExtendedFieldType[] extendedPropertiesField;
	}
}
