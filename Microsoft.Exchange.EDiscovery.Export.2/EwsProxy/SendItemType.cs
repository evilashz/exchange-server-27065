using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200038B RID: 907
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class SendItemType : BaseRequestType
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0002A187 File Offset: 0x00028387
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0002A18F File Offset: 0x0002838F
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0002A198 File Offset: 0x00028398
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0002A1A0 File Offset: 0x000283A0
		public TargetFolderIdType SavedItemFolderId
		{
			get
			{
				return this.savedItemFolderIdField;
			}
			set
			{
				this.savedItemFolderIdField = value;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0002A1A9 File Offset: 0x000283A9
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0002A1B1 File Offset: 0x000283B1
		[XmlAttribute]
		public bool SaveItemToFolder
		{
			get
			{
				return this.saveItemToFolderField;
			}
			set
			{
				this.saveItemToFolderField = value;
			}
		}

		// Token: 0x04001305 RID: 4869
		private BaseItemIdType[] itemIdsField;

		// Token: 0x04001306 RID: 4870
		private TargetFolderIdType savedItemFolderIdField;

		// Token: 0x04001307 RID: 4871
		private bool saveItemToFolderField;
	}
}
