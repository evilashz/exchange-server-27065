using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200038D RID: 909
	[XmlInclude(typeof(CopyItemType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(MoveItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class BaseMoveCopyItemType : BaseRequestType
	{
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0002A1EC File Offset: 0x000283EC
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0002A1F4 File Offset: 0x000283F4
		public TargetFolderIdType ToFolderId
		{
			get
			{
				return this.toFolderIdField;
			}
			set
			{
				this.toFolderIdField = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0002A1FD File Offset: 0x000283FD
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0002A205 File Offset: 0x00028405
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0002A20E File Offset: 0x0002840E
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0002A216 File Offset: 0x00028416
		public bool ReturnNewItemIds
		{
			get
			{
				return this.returnNewItemIdsField;
			}
			set
			{
				this.returnNewItemIdsField = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0002A21F File Offset: 0x0002841F
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0002A227 File Offset: 0x00028427
		[XmlIgnore]
		public bool ReturnNewItemIdsSpecified
		{
			get
			{
				return this.returnNewItemIdsFieldSpecified;
			}
			set
			{
				this.returnNewItemIdsFieldSpecified = value;
			}
		}

		// Token: 0x0400130A RID: 4874
		private TargetFolderIdType toFolderIdField;

		// Token: 0x0400130B RID: 4875
		private BaseItemIdType[] itemIdsField;

		// Token: 0x0400130C RID: 4876
		private bool returnNewItemIdsField;

		// Token: 0x0400130D RID: 4877
		private bool returnNewItemIdsFieldSpecified;
	}
}
