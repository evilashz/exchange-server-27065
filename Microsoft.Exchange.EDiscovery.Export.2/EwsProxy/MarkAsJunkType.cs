using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000330 RID: 816
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MarkAsJunkType : BaseRequestType
	{
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00028E30 File Offset: 0x00027030
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00028E38 File Offset: 0x00027038
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00028E41 File Offset: 0x00027041
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00028E49 File Offset: 0x00027049
		[XmlAttribute]
		public bool IsJunk
		{
			get
			{
				return this.isJunkField;
			}
			set
			{
				this.isJunkField = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00028E52 File Offset: 0x00027052
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x00028E5A File Offset: 0x0002705A
		[XmlAttribute]
		public bool MoveItem
		{
			get
			{
				return this.moveItemField;
			}
			set
			{
				this.moveItemField = value;
			}
		}

		// Token: 0x040011AC RID: 4524
		private BaseItemIdType[] itemIdsField;

		// Token: 0x040011AD RID: 4525
		private bool isJunkField;

		// Token: 0x040011AE RID: 4526
		private bool moveItemField;
	}
}
